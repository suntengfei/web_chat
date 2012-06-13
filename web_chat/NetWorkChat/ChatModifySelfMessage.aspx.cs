using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataObject;
using Logic;

namespace NetWorkChat
{
    public partial class ChatModifySelfMessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            SelfMessage mselfmessage = new SelfMessage();

           int mFlag6=mselfmessage.UpdateSelfMessage(Session["UserID"].ToString(), txtUserName.Text, txtRemark.Text);
           int mFlag5 = mselfmessage.UpdatePassword(Session["UserID"].ToString(), txtOPassword.Text, txtNPassword1.Text);
            
            //判断原密码是否有误
            if (mFlag5 == 1&&mFlag6==1)
            {

                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "<script Language='javascript'>alert('密码和信息修改成功，请重新登录！')</script>");
                Response.Redirect("ChatLogin.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "<script Language='javascript'>alert('原密码有误，请重新输入！')</script>");
            }

         
        }
    }
}
