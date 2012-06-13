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
using Logic;

namespace NetWorkChat
{
    public partial class ChatRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitRegisterBtn_Click(object sender, EventArgs e)
        {
           
           
          
               Regist mregist = new Regist();
               int mFlag4 = mregist.CheckUserRegist(txtEmail.Text);
               if (mFlag4 == 0)
               {

                   int mFlag3 = mregist.UserRegist(txtName.Text, txtEmail.Text, txtPassword1.Text, txtRemark.Text);
                      
                      if (mFlag3 == 0)
                      {
                          Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "<script Language='javascript'>alert('注册成功！')</script>");
                          Response.Redirect("ChatLogin.aspx");
                      }
                      else
                      {
                          Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "<script Language='javascript'>alert('注册失败')</script>");
                          return;
                      }
               }
               else
               {
                   Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "<script Language='javascript'>alert('该邮箱已被注册！请重新注册！')</script>");
                   return;
                  
               }
           }

       

       
           

        }
    }
