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
using BlogDataObject;
using Logic;

namespace NetWorkChat
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {}

        protected void LoginBtn_click(object sender, EventArgs e)
        {

            ChatLogin mlogin = new ChatLogin();
            int mFlag1 = mlogin.CheckLogin(TextBox1.Text, TextBox2.Text);
            if (mFlag1==1)
            {
                
                Response.Redirect("Perhome.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "<script Language='javascript'>alert('用户名或密码错误,请重新输入!')</script>");
               
            }
        }

        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
             Response.Redirect("Register.aspx");
        }
  
    }
}