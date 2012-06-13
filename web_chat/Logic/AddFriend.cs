/*
 * 
 * 版    本： Ver 1.0
 * 
 * 类    名： AddFriend 
 * 
 * 文 件 名： AddFriend.cs 
 * 
 * 作    者： 
 * 
 * 日    期： 
 * 
 * 描    述： AddFriend 将执行搜索、添加、响应添加好友及验证是否重复添加好友的存储过程的操作进行封装.
 * 
 * 修 改 者：
 * 
 * 修改历史： 
 * 
 * 
 * */


using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObject;

namespace Logic
{
    public class AddFriend
    {      

        #region 以邮箱方式搜索好友
        /// <summary>
        /// 以邮箱方式搜索好友
        /// </summary>
        /// <param name="userEmail">用户邮箱</param>
        public DataTable SearchUserEmail(string aUserEmail)
        {
            SqlStoreCommand mSqlStoreCommand = new SqlStoreCommand();
            mSqlStoreCommand.StoreProdureName = "Chat_User";
            mSqlStoreCommand.ClearParameters();
            mSqlStoreCommand.AddParameters("@mFunctionName", "SearchOne");
            mSqlStoreCommand.AddParameters("@mUserEmail", aUserEmail);
            DataTable mSearchUserEmail = mSqlStoreCommand.ExecuteDataTable();
            return mSearchUserEmail;
        }

        #endregion

        #region 验证是否重复添加好友

        /// <summary>
        /// 验证是否重复添加好友
        /// </summary
        public int CheckAddFriendMessage(string aUserEmail,string aFriendEmail)
        {
            SqlStoreCommand mSqlStoreCommand = new SqlStoreCommand();
            mSqlStoreCommand.StoreProdureName = "Chat_Friend";
            mSqlStoreCommand.ClearParameters();
            mSqlStoreCommand.AddParameters("@mFunctionName", "CheckInsertToFr");
            mSqlStoreCommand.AddParameters("@mHostEmail", aUserEmail);
            mSqlStoreCommand.AddParameters("@mFriendEmail", aFriendEmail);
            int mCheckAddFriendMessage = Convert.ToInt32(mSqlStoreCommand.ExecuteFunction());
            return mCheckAddFriendMessage;
        }

        #endregion  

        #region 添加好友

        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="userEmail">好友邮箱</param>
        public int AddFriends(string aUserEmail,string aFriendEmail)
        {
            SqlStoreCommand mSqlStoreCommand = new SqlStoreCommand();
            mSqlStoreCommand.StoreProdureName = "Chat_Friend";
            mSqlStoreCommand.ClearParameters();
            mSqlStoreCommand.AddParameters("@mFunctionName", "InsertIntoFr");
            mSqlStoreCommand.AddParameters("@mHostEmail",aUserEmail);
            mSqlStoreCommand.AddParameters("@mFriendEmail",aFriendEmail);
            int mAddFriends = mSqlStoreCommand.ExecuteNonQuery();
            return mAddFriends;
        }

        #endregion

        #region 获取添加信息

        /// <summary>
        /// 获取添加信息
        /// </summary>
        /// <param name="aHostEmail">用户邮箱</param>
        /// <returns></returns>

        public DataTable GetAddMessage(string aHostEmail)
        {
            SqlStoreCommand mSqlStoreCommand = new SqlStoreCommand();
            mSqlStoreCommand.StoreProdureName = "Chat_Friend";
            mSqlStoreCommand.ClearParameters();
            mSqlStoreCommand.AddParameters("@mFunctionName", "GetAddMessage");
            mSqlStoreCommand.AddParameters("@mHostEmail", aHostEmail);
            DataTable mGetAddMessage = mSqlStoreCommand.ExecuteDataTable();
            return mGetAddMessage;
        }

        #endregion

        #region 响应添加好友请求

        /// <summary>
        /// 同意添加好友
        /// </summary>
        public int AgreeAddFriend(string aUserEmail,string aFriendEmail)
        {
            SqlStoreCommand mSqlStoreCommand = new SqlStoreCommand();
            mSqlStoreCommand.StoreProdureName = "Chat_Friend";
            mSqlStoreCommand.ClearParameters();
            mSqlStoreCommand.AddParameters("@mFunctionName","AdmmitOrNot1");
            mSqlStoreCommand.AddParameters("@mHostEmail", aUserEmail);
            mSqlStoreCommand.AddParameters("@mFriendEmail",aFriendEmail);
            mSqlStoreCommand.ExecuteNonQuery();
            return 1;
            
        }

        /// <summary>
        /// 拒绝添加好友
        /// </summary>
        /// <returns></returns>
        public int RefuseAddfriend(string aUserEmail, string aFriendEmail)
        {
            SqlStoreCommand mSqlStoreCommand = new SqlStoreCommand();
            mSqlStoreCommand.StoreProdureName = "Chat_Friend";
            mSqlStoreCommand.ClearParameters();
            mSqlStoreCommand.AddParameters("@mFunctionName","AdmmitOrNot2");
            mSqlStoreCommand.AddParameters("@mHostEmail", aUserEmail);
            mSqlStoreCommand.AddParameters("@mFriendEmail",aFriendEmail);
            mSqlStoreCommand.ExecuteNonQuery();
            return 0;
        }

        #endregion

        #region 获取好友列表

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="aUserEmail">用户账号</param>
        /// <returns></returns>
        public DataTable FriendsList(string aUserEmail)
        {
            SqlStoreCommand mSqlStoreCommand = new SqlStoreCommand();
            mSqlStoreCommand.StoreProdureName = "Chat_Friend";      //motified by stf & pzb
            mSqlStoreCommand.ClearParameters();
            mSqlStoreCommand.AddParameters("@mFunctionName", "GetFriends");
            mSqlStoreCommand.AddParameters("@mHostEmail", aUserEmail);
            DataTable mFriendsList = mSqlStoreCommand.ExecuteDataTable();
            return mFriendsList;
        }
        #endregion

        #region 获取用户名

        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <param name="aUserEmail">用户邮箱</param>
        /// <returns></returns>

        public string SearchName(string aUserEmail)
        {
            SqlStoreCommand mSqlStoreCommand = new SqlStoreCommand();
            mSqlStoreCommand .StoreProdureName = "Chat_User";
            mSqlStoreCommand.ClearParameters();
            mSqlStoreCommand.AddParameters("@mFunctionName", "SearchName");
            mSqlStoreCommand.AddParameters("@mUserEmail", aUserEmail);
            string mSearchName = Convert.ToString(mSqlStoreCommand.ExecuteFunction());
            return mSearchName;
        }

        #endregion
    }
}
