/*
 * 
 * 版    本： Ver 1.0
 * 
 * 类    名： OfflineMessage 
 * 
 * 文 件 名： OfflineMessage.cs 
 * 
 * 作    者： 
 * 
 * 日    期： 
 * 
 * 描    述： OfflineMessage 将执行插入、显示、删除离线好友及显示发送离线消息好友的存储过程的操作进行封装.
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
    public class OfflineMessage
    {

        #region 插入离线消息

        /// <summary>
        /// 插入离线消息
        /// </summary>
        /// <param name="mMessage"></param>
        /// <returns></returns>
        public void OffMessage(string aHostEmail, string aFriendEmail, string aMessage)
        {
            SqlStoreCommand mSqlStoreCommand = new SqlStoreCommand();
            mSqlStoreCommand.StoreProdureName = "Chat_Message";
            mSqlStoreCommand.ClearParameters();
            mSqlStoreCommand.AddParameters("@mFunctionName", "InsertInToOff");
            mSqlStoreCommand.AddParameters("@mSenderEmail", aHostEmail);
            mSqlStoreCommand.AddParameters("@mReceiverEmail", aFriendEmail);
            mSqlStoreCommand.AddParameters("@mMessage", aMessage);
            mSqlStoreCommand.AddParameters("@mTime", System.DateTime.Now);
            mSqlStoreCommand.ExecuteNonQuery();
        }
        #endregion

        #region 显示发送离线消息好友


        /// <summary>
        /// 显示发送离线消息好友
        /// </summary>
        /// <returns></returns>
        public DataTable ShowOffMessageSender(string aHostEmail)
        {
            SqlStoreCommand mSqlStoreCommand = new SqlStoreCommand();
            mSqlStoreCommand.StoreProdureName = "Chat_Message";
            mSqlStoreCommand.ClearParameters();
            mSqlStoreCommand.AddParameters("@mFunctionName", "GetOffFriends");
            mSqlStoreCommand.AddParameters("@mReceiverEmail", aHostEmail);
            DataTable mShowOffMessageSender = mSqlStoreCommand.ExecuteDataTable();
            return mShowOffMessageSender;

        }

        #endregion

        #region 获取离线消息


        /// <summary>
        /// 获取离线消息

        /// </summary>
        /// <param name="aHostEmail">接受者邮箱</param>
        /// <param name="aFriendEmail">发送者邮箱</param>
        /// <returns></returns>

        public DataTable GetOffMessage(string aHostEmail)
        {
            SqlStoreCommand mSqlStoreCommand = new SqlStoreCommand();
            mSqlStoreCommand.StoreProdureName = "Chat_Message";
            mSqlStoreCommand.ClearParameters();
            mSqlStoreCommand.AddParameters("@mFunctionName", "GetOffMessage");
            mSqlStoreCommand.AddParameters("@mReceiverEmail", aHostEmail);
            DataTable mGetOffMessage = mSqlStoreCommand.ExecuteDataTable();
            return mGetOffMessage;

        }
        #endregion

        #region 删除离线消息


        /// <summary>
        /// 删除离线消息

        /// </summary>
        /// <param name="aHostEmail">接受者邮箱</param>
        /// <param name="aFriendEmail">发送者邮箱</param>
        /// <returns></returns>

        public void DeleteOffMessage(string aHostEmail, string aFriendEmail)
        {
            SqlStoreCommand mSqlStoreCommand = new SqlStoreCommand();
            mSqlStoreCommand.StoreProdureName = "Chat_Message";
            mSqlStoreCommand.ClearParameters();
            mSqlStoreCommand.AddParameters("@mFunctionName", "DeleteOffMessage");
            mSqlStoreCommand.AddParameters("@mReceiverEmail", aHostEmail);
            mSqlStoreCommand.AddParameters("@mSenderEmail", aFriendEmail);
            mSqlStoreCommand.ExecuteNonQuery();
           
        }
        #endregion
    }
}
