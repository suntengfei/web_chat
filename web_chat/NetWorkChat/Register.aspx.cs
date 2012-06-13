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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitRegisterBtn_Click(object sender, EventArgs e)
        {
            Regist mregister = new Regist();
            
           
          
               CheckRegist mckregist = new CheckRegist();
               int mFlag4 = mckregist.CheckUserRegist(TextBox3.Text);
               if (mFlag4 == 0)
               {

                      int mFlag3 = mregister.UserRegist(TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox6.Text);
                      Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "<script Language='javascript'>alert('注册成功！')</script>");
                      if (mFlag3 == 0)
                      {
                          Response.Redirect("Login.aspx");
                      }
                      else
                      {
                          Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "<script Language='javascript'>alert('注册失败')</script>");
                      }
               }
               else
               {
                   Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "<script Language='javascript'>alert('该邮箱已被注册！请重新注册！')</script>");
                  
               }
           }
           

        }
    }
