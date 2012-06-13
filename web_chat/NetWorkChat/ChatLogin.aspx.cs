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
using DataObject;
using Logic;

namespace NetWorkChat
{
    public partial class ChatLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        protected void LoginBtn_Click(object sender, EventArgs e)
        {

            Logic.Login mlogin = new Logic.Login();
            int mFlag1 = mlogin.CheckLogin(txtUserEmail.Text, txtUserPassword.Text);
            //判断邮箱是否存在
            if (mFlag1 == 1)//如果存在,跳转到个人主页


            {
                    AddFriend adf = new AddFriend();
                    string userid = txtUserEmail.Text.ToString();
                    Session["UserID"] = userid;
                    Session["UserName"] = adf.SearchName(userid);
                    if (Application[userid] == null)
                    {
                        XmlDataManager xdm = new XmlDataManager();
                        Application[userid] = xdm.InitialChatPrivateMessageXml;
                        Response.Redirect("ChatPerhome.aspx");//跳转到个人主页
                    }
                else
                        Response.Redirect("ChatPerhome.aspx");//跳转到个人主页
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "<script Language='javascript'>alert('用户已登录,不可以重复登录!')</script>");
            }
            else//如果不存在，输出用户名或密码错误
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "<script Language='javascript'>alert('用户名或密码错误,请重新输入!')</script>");

            }
        }

        protected void RegisterBtn_Click(object sender, EventArgs e)//跳转到注册界面

        {
            Response.Redirect("ChatRegister.aspx");
        }

     
  
    }
}