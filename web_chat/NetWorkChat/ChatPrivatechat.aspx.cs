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
    public partial class ChatPrivatechat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null&&Session["UserID"] != null)
            {
                AddFriend addf = new AddFriend();
                lbUserName.Text = Session["UserName"].ToString();
                string auserid = Session["UserID"].ToString();
                System.Data.DataTable datatable = addf.FriendsList(auserid);
                this.Label5.InnerHtml = friendstring(datatable);
                this.currentfriendID.Text = auserid;
            }
            else
            {
                Response.Redirect("ChatLogin.aspx");
            }
        }
        private string friendstring(System.Data.DataTable datatable)
        {
            string htmlStr = "<table id='tbFriendList'>";
            foreach (System.Data.DataRow therow in datatable.Rows)
            {
                string liStr = "<tr>" + "<td>" + therow["UserEmail"].ToString() + "</td>" +
                    "<td>" + therow["UserName"].ToString() + "</td></tr>";
                htmlStr += liStr;
            }
            htmlStr += "</table>";
            return htmlStr;
        }

        protected void BtnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChatPerhome.aspx");//跳转到个人主页

        }

       

       
      
        
    }
}
