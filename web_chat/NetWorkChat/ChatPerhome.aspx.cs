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
    public partial class Perhome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)    
        {
            Response.Buffer = true;                                             //退出无法返回
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            if (Session["UserID"] == null)
            {
                Response.Redirect("ChatLogin.aspx");
            }
            if (!IsPostBack)
            {
                SelfMessage mSelfMessage = new SelfMessage();                    //显示个人信息
                DataTable mMess = mSelfMessage.ShowSelfMessage(Session["UserID"].ToString());
                this.text1.Text = mMess.Rows[0]["UserName"].ToString();
                this.text2.Text =Session["UserID"].ToString();
                this.text3.Text = mMess.Rows[0]["Remark"].ToString();
                AddFriend mAddFriend = new AddFriend();                         //显示添加信息
                DataTable mGetM = mAddFriend.GetAddMessage(Session["UserID"].ToString());
                if (mGetM.Rows.Count > 0)
                {
                    this.Button7.Visible = true;
                    DropDownList1.DataSource = mGetM;
                    DropDownList1.DataTextField = "UserEmail";
                    DropDownList1.DataValueField = "UserEmail";
                    DropDownList1.DataBind();
                }
           
                OfflineMessage mOfflineMessage = new OfflineMessage();          //显示离线信息
                DataTable mGOffM = mOfflineMessage.GetOffMessage(Session["UserID"].ToString());

                if (mGOffM.Rows.Count > 0)
                {
                    this.Button6.Visible = true;
                    this.TextBox3.Text = null;
                    for (int i = 0; i < mGOffM.Rows.Count; i++)
                    {
                        
                        this.TextBox3.Text += mGOffM.Rows[i]["SenderEmail"].ToString() +mGOffM.Rows[i]["Time"].ToString() + "说：\n" + mGOffM.Rows[i]["Message"].ToString() + "\n";
                        mOfflineMessage.DeleteOffMessage(Session["UserID"].ToString(), mGOffM.Rows[i]["SenderEmail"].ToString());
                    }
                }
                
            }
        }
            

        protected void Button5_Click(object sender, EventArgs e)            //查找好友
        {
            this.Label5.Visible = true;
            this.TextBox1.Visible = true;
            this.ImageButton1.Visible = true;

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            
            Regist mregist = new Regist();
            int mFlag4 = mregist.CheckUserRegist(TextBox1.Text);
            
            if (mFlag4 == 1)                                              //判断是否存在此用户
            {
                AddFriend mAddFriend = new AddFriend();
                int mAgine = mAddFriend.CheckAddFriendMessage(Session["UserID"].ToString(), TextBox1.Text);
                if (mAgine == 1)                                          //判断是否已为好友
                {
                    
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "<script Language='javascript'>alert('该用户已为好友')</script>");
                    
                }
                else                                                       
                {
                    int mWork = mAddFriend.AddFriends(Session["UserID"].ToString(), TextBox1.Text);
                    if (mWork == 1)                                       //添加好友
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "<script Language='javascript'>alert('等待对方回应')</script>");
                    }
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "<script Language='javascript'>alert('您输入的邮箱不存在,请重新输入!')</script>");
               
            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            this.TextBox3.Visible = true;
            
        }

        protected void Button8_Click(object sender, EventArgs e)              //加为好友
        {

            AddFriend mAddFriend = new AddFriend();
            mAddFriend.AgreeAddFriend(Session["UserID"].ToString(), DropDownList1.SelectedValue);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "<script Language='javascript'>alert('已加为好友')</script>");
            DropDownList1.Items.Remove(DropDownList1.Items.FindByValue(DropDownList1.SelectedValue));//按值删除 
            if (DropDownList1.Items.Count ==0)
            {
                this.Button7.Visible = false;
                this.DropDownList1.Visible = false;
                this.Button8.Visible = false;
                this.Button9.Visible = false;

            }
        }
        protected void Button9_Click(object sender, EventArgs e)                //拒绝添加
        {
            AddFriend mAddFriend = new AddFriend();
            mAddFriend.RefuseAddfriend(Session["UserID"].ToString(), DropDownList1.SelectedValue);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message", "<script Language='javascript'>alert('已拒绝加为好友')</script>");
            DropDownList1.Items.Remove(DropDownList1.Items.FindByValue(DropDownList1.SelectedValue));//按值删除 
            if (DropDownList1.Items.Count == 0)
            {
                this.Button7.Visible = false;
                this.DropDownList1.Visible = false;
                this.Button8.Visible = false;
                this.Button9.Visible = false;

            }
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            this.DropDownList1.Visible = true;
            this.Button8.Visible = true;
            this.Button9.Visible = true;
        }

        protected void Button5_Click1(object sender, EventArgs e)
        {
            this.TextBox1.Visible = true;
            this.ImageButton1.Visible = true;
            this.Label5.Visible = true;
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            Session["UserID"] = null;
            Response.Redirect("ChatLogin.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChatModifySelfMessage.aspx");
        }
    }
}
