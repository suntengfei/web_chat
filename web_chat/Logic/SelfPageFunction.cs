using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogDataObject;
namespace Logic
{
    class SelfPageFunction
    {
        #region 个人主页选择相应功能

        /// <summary>
        /// 主页选择相应功能
        /// </summary>
        /// <param name="mFunctionName"></param>
        public void SelcetFunction(string mFunctionName)
        {
            if (mFunctionName = "Insert")
            {
                new Regist().UserRegist(Textbox1.Text, TextBox2.Text, Textbox3.Text);
            }
            else if (mFunctionName = "SearchOne")
            {
                SearchAllUsers();
                SearchUserEmail(Textbox1.Text);

            }

        }
        #endregion

        #region 登录页面显示个人信息

        /// <summary>
        /// 登录页面显示个人信息
        /// </summary>
        public void ShowSelfMessage()
        {
            SqlStoreCommand sqlStoreCommand = new SqlStoreCommand();
            sqlStoreCommand.StoreProdureName = "？";
            sqlStoreCommand.ExecuteDataTable();
            sqlStoreCommand.ExecuteNonQuery();
            
        }

        #endregion

        #region 登陆页面显示添加好友信息

        /// <summary>
        /// 登陆页面显示添加好友信息
        /// </summary>
        public static void ShowAddFriendMessage()
        {
            SqlStoreCommand sqlStoreCommand = new SqlStoreCommand();
            sqlStoreCommand.StoreProdureName = "？";
            sqlStoreCommand.ExecuteNonQuery();
        }

        #endregion

        #region 登陆界面显示离线消息

        /// <summary>
        /// 登陆界面显示离线消息
        /// </summary>
        public static void ShowOfflineMessage()
        {
            SqlStoreCommand sqlStoreCommand = new SqlStoreCommand();
            sqlStoreCommand.StoreProdureName = "？";
            sqlStoreCommand.ExecuteNonQuery();
        }

        #endregion

        #region 以邮箱方式搜寻好友


        /// <summary>
        /// 以邮箱方式搜寻好友

        /// </summary>
        /// <param name="userEmail">用户邮箱</param>
        public static void SearchUserEmail(string userEmail)
        {
            SqlStoreCommand sqlStoreCommand = new SqlStoreCommand();
            sqlStoreCommand.StoreProdureName = "Chat_User";
            sqlStoreCommand.ClearParameters();
            sqlStoreCommand.AddParameters("@userEmail", userEmail);
            sqlStoreCommand.ExecuteNonQuery();
        }

        #endregion

    }
}
