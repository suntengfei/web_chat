using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Logic
{
    public class PriXmlDataManager
    {
        #region 类私有字段
        private XmlDocument xmldocUserList;    //用户列表Xml对象
        private XmlDocument xmldocPrivateMessage;//私聊信息xml对象
        #endregion

        #region 类构造函数
        public PriXmlDataManager() { }

        public PriXmlDataManager(XmlDocument userlistxmldoc, XmlDocument privatemessage)//构造函数
        {
            xmldocUserList = userlistxmldoc;
            xmldocPrivateMessage = privatemessage;
        }
        #endregion

        #region 初始化用户列表Xml对象
        /// <summary>
        /// 初始化用户列表Xml数据存储对象
        /// </summary>
        public XmlDocument InitialUserListXml
        {
            get
            {
                string[] userListAt = new string[4];
                userListAt[0] = "listRenewTime";
                userListAt[1] = DateTime.Now.ToString();
                userListAt[2] = "count";
                userListAt[3] = "0";
                XmlDocument xmldocUser = CreateEmptyXmlDoc("userList", userListAt);
                return xmldocUser;
            }
        }
        #endregion

        #region 初始化私聊信息Xml对象
        public XmlDocument InitialChatPrivateMessageXml
        {
            get
            {
                string[] userListAt = new string[4];
                userListAt[0] = "count";
                userListAt[1] = "0";
                userListAt[2] = "loginPri";
                userListAt[3] = "true";
                XmlDocument privateChat = CreateEmptyXmlDoc("privateChatList", userListAt);
                return privateChat;
            }
        }
        #endregion

        #region 添加新用户   !!!用在登录时  ---已改
        /// <summary>
        /// 添加聊天用户
        /// </summary>
        /// <param name="userName">新添加的用户名</param>
        public void AddUser(string userID, string userName)
        {
            XmlNodeList childs = xmldocUserList.GetElementsByTagName("user");   //用户列表结点集
            for (int i = childs.Count - 1; i >= 0; i--)
            {
                string id = childs[i].Attributes["id"].Value;
                if (userID == id) return;
            }
            XmlAttributeCollection rootAtC = xmldocUserList.DocumentElement.Attributes;
            DateTime timeNow = DateTime.Now;          //获取当前更新时间
            rootAtC["listRenewTime"].Value = timeNow.ToString(); //设置用户列表更新时间
            int count = int.Parse(rootAtC["count"].Value) + 1;
            rootAtC["count"].Value = count.ToString();      //更新当前在线用户数量      

            string[] userElementAts = new string[6];    //用户节点属性数组
            userElementAts[0] = "name";
            userElementAts[1] = userName;
            userElementAts[2] = "id";
            userElementAts[3] = userID.ToString();
            userElementAts[4] = "lastVisitTime";
            userElementAts[5] = timeNow.ToString();
            XmlElement userElement = CreateElement(xmldocUserList, "user", userElementAts, "");//创建新用户记录节点
            xmldocUserList.DocumentElement.AppendChild(userElement);            //将新用户添加至用户列表

        }
        #endregion

        #region 返回客户端Xml信息
        /// <summary>
        /// 返回客户端Xml信息
        /// </summary>
        /// <param name="userID">当前请求用户ID</param>
        /// <param name="messageStartIndex">获取聊天信息开始位置</param>
        /// <param name="userListRenewTime">当前请求用户的用户列表更新时间</param>
        /// <returns></returns>
        public string GetResponseText(string userID, DateTime userListRenewTime)
        {
            System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
            System.Xml.XmlDeclaration xd = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            xmldoc.AppendChild(xd);

            //创建根节点
            XmlAttribute xa;
            XmlNode root = xmldoc.CreateNode(System.Xml.XmlNodeType.Element, "response", "");
            xa = xmldoc.CreateAttribute("userID");
            xa.Value = userID;
            root.Attributes.Append(xa);
            //root.AppendChild(xmldoc.ImportNode(xmldocChatMessage.DocumentElement, true));//方法把一个节点从另一个文档复制到该文档以便应用
            //DeleteNodeByLastID(root.FirstChild, lastMsgID);

            XmlAttributeCollection userlistAtC = xmldocUserList.DocumentElement.Attributes;
            RenewUserList(userID);
            DateTime oldtime = DateTime.Parse(userlistAtC["listRenewTime"].Value);
            if (oldtime != userListRenewTime)
            {
                root.AppendChild(xmldoc.ImportNode(xmldocUserList.DocumentElement, true));
            }
            if (xmldocPrivateMessage.DocumentElement.Attributes["count"].ToString() != "0")
            {
                root.AppendChild(xmldoc.ImportNode(xmldocPrivateMessage.DocumentElement, true));//加入用户私聊信息
                //Application[userID] = new XmlDataDocument().InitialChatPrivateMessageXml;
            }
            xmldoc.AppendChild(root);
            return xmldoc.OuterXml;
        }
        #endregion

        #region 用户列表更新方法
        private void RenewUserList(string userID) //用户列表更新
        {
            XmlElement root = xmldocUserList.DocumentElement;   //用户列表根结点
            XmlNodeList childs = xmldocUserList.GetElementsByTagName("user");   //用户列表结点集
            XmlAttribute xa = root.Attributes["count"];
            int count = int.Parse(xa.Value);        //当前在线用户数量
            DateTime timeNow = DateTime.Now;        //获取当前时间
            for (int i = childs.Count - 1; i >= 0; i--)
            {
                string id = childs[i].Attributes["id"].Value;
                DateTime lastVisitTime = DateTime.Parse(childs[i].Attributes["lastVisitTime"].Value);  //用户列表中某用户最后访问时间
                if (id == userID)
                {
                    childs[i].Attributes["lastVisitTime"].Value = timeNow.ToString();       //更新当前访问用户最后访问时间  
                }
                TimeSpan ts = timeNow.Subtract(lastVisitTime);                              //计算当前遍历用户最后访问时间间隔   
                if (ts.TotalSeconds > 3)                                                   //时间间隔大于一定值则判定该用户已下线
                {
                    root.RemoveChild(childs[i]);    //移除当前下线用户
                    count--;
                    xa.Value = count.ToString();
                    root.Attributes["listRenewTime"].Value = DateTime.Now.ToString();       //更新用户列表更新时间
                }
            }
        }
        #endregion

        #region 创建元素节点方法
        private XmlElement CreateElement(XmlDocument xmldoc, string name, string[] at, string text)
        {
            XmlElement child = xmldoc.CreateElement(name);
            for (int i = 0; i < at.Length; i += 2)
            {
                XmlAttribute xa = xmldoc.CreateAttribute(at[i]);    //属性名
                xa.Value = at[i + 1];                               //属性值
                child.Attributes.Append(xa);
            }
            XmlText xmltext = xmldoc.CreateTextNode(text);
            child.AppendChild(xmltext);
            return child;
        }
        #endregion


        #region 创建带有根节点的Xml对象方法
        private XmlDocument CreateEmptyXmlDoc(string rootName, string[] at)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlDeclaration xd = xmldoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            xmldoc.AppendChild(xd);
            XmlNode root = xmldoc.CreateNode(XmlNodeType.Element, rootName, "");
            for (int i = 0; i < at.Length; i += 2)
            {
                XmlAttribute xa = xmldoc.CreateAttribute(at[i]);    //属性名
                xa.Value = at[i + 1];                       //属性值
                root.Attributes.Append(xa);
            }
            xmldoc.AppendChild(root);
            return xmldoc;
        }
        #endregion

        #region  添加聊天信息
        /// <summary>
        /// 添加聊天信息
        /// </summary>
        /// <param name="msg"></param>
        public XmlDocument AddMessage(string senderID,XmlDocument receiverXml, string msg)
        {
            if (receiverXml != null)
            {
                XmlAttribute xacount = receiverXml.DocumentElement.Attributes["count"];
                int id = int.Parse(xacount.Value) + 1;
                xacount.Value = id.ToString();
                string[] at = new string[2];
                at[0] = "senderID";
                at[1] = senderID;
                XmlElement elem = CreateElement(receiverXml, "message", at, msg);
                receiverXml.DocumentElement.AppendChild(elem);    //将新聊天信息节点添加至聊天信息表中
                return receiverXml;
            }
            else return null;
        }
        #endregion

        //#region 删除Xml子节点至指定尾处
        //private void DeleteNodeByLastID(XmlNode node, int lastID)
        //{
        //    XmlNodeList nodes = node.ChildNodes;
        //    for (int i = 0; i < lastID; i++)
        //    {
        //        node.RemoveChild(nodes[0]);
        //    }
        //}
        //#endregion



        #region 返回当前聊天信息Xml对象
        /// <summary>
        /// 返回当前用户聊天信息Xml对象
        /// </summary>
        public XmlDocument XmlPrivateMessage
        {
            get { return xmldocPrivateMessage; }
        }
        #endregion

        #region 返回当前用户列表Xml对象
        /// <summary>
        /// 返回当前用户列表Xml对象
        /// </summary>
        public XmlDocument XmlUserList
        {
            get { return xmldocUserList; }
        }
        #endregion
    }
}

