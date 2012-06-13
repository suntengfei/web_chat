using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using Logic;

namespace NetWorkChat
{
    public partial class Chat_PriServer : System.Web.UI.Page
    {
        PriXmlDataManager xdm; //xml数据管理对象
        protected void Page_Load(object sender, EventArgs e)
        {
            ServerPriManager();
        }

        #region 服务管理方法
        private void ServerPriManager()
        {
            if (Request["serverType"] != null)
            {
                Application.Lock(); //加锁
                //实例化Xml数据管理对象
                string userID = Session["UserID"].ToString();
                xdm = new PriXmlDataManager((XmlDocument)Application["userList"], (XmlDocument)Application[userID]);
                string type = Request["serverType"].ToString().ToLower();
                switch (type)
                {
                    case "userlogin": userLoginManager();   //用户登陆处理方法
                        break;
                    case "onlinerenew": OnlineRenewManager();//在线更新处理方法
                        break;
                    case "messagesend": MessageSendManager();  //信息发送处理方法                        break;
                    case "closePri": ClosePriSign();
                        break;
                    default:
                        Response.Write("Error: 未定义的服务类型：" + type);
                        Application.UnLock();   //解锁
                        break;
                }
            }
            else
            {
                Response.Write("Error: 未指定服务类型。");
            }
        }
        #endregion

        #region 用户登陆管理方法
        /// <summary>
        /// 用户登陆管理方法
        /// </summary>
        private void userLoginManager()
        {
            string userName = Request["userName"];
            string userID = Session["UserID"].ToString();
            xdm.AddUser(userID, userName);
            string res = xdm.GetResponseText(userID, DateTime.MinValue);
            Application["userList"] = xdm.XmlUserList;  //保存当前更新的用户列表
            Application[userID] = xdm.InitialChatPrivateMessageXml;
            Application.UnLock();    //解锁 
            Response.Write(res);
        }
        #endregion

        #region 在线更新管理方法
        /// <summary>
        /// 在线更新管理方法
        /// </summary>
        private void OnlineRenewManager()
        {
            string userID = Session["UserID"].ToString();
            //string username = Request["userName"];
            DateTime userListRenewTime = DateTime.Parse(Request["userListRenewTime"]);
            string res = xdm.GetResponseText(userID, userListRenewTime);
            Application["userList"] = xdm.XmlUserList;  //保存当前更新的用户列表
            Application[userID] = xdm.InitialChatPrivateMessageXml;
            Application.UnLock();
            Response.Write(res);
        }
        #endregion

        #region 信息发送管理方法

        /// <summary>
        /// 信息发送管理方法

        /// </summary>
        private void MessageSendManager()
        {
            string sendMsg = Request["userMessage"].ToString();
            string name = Request["userName"].ToString();
            string userID = Request["userID"].ToString();
            string receiverID = Request["ReceiverID"].ToString();
            DateTime renewTime = DateTime.Parse(Request["userListRenewTime"].ToString());
            string time = DateTime.Now.TimeOfDay.ToString();
            int pos = time.IndexOf('.');
            if (pos > -1)
                time = time.Substring(0, pos);
            string temp = "【" + name + "(" + userID + ")】说：" + time + "\n " + sendMsg;
            if (Application[receiverID] != null)
            {
                Application[receiverID] = xdm.AddMessage(userID, (XmlDocument)Application[receiverID], temp);    //新添加用户发送的信息
            }
            else
            {
                OfflineMessage ofm = new OfflineMessage();
                ofm.OffMessage(userID, receiverID, sendMsg);
            }
            string res = xdm.GetResponseText(userID, renewTime);
            Application["userList"] = xdm.XmlUserList;
            Application[userID] = xdm.InitialChatPrivateMessageXml;
            //Application["chatMessageList"] = xdm.XmlChatMessage;
            Application.UnLock();
            Response.Write(res);
        }
        #endregion
        private void ClosePriSign() {
            xdm.XmlPrivateMessage.Attributes["loginPri"].Value = "false";
            Application[Session["UserID"].ToString()] = xdm.XmlPrivateMessage;
            Application.UnLock();
            Response.Write(null);
        }
    }
}
