/*
 * 
 * 版    本： Ver 1.0
 * 
 * 类    名： SelfMessage 
 * 
 * 文 件 名： SelfMessage.cs 
 * 
 * 作    者： 
 * 
 * 日    期： 
 * 
 * 描    述： SelfMessage 将执行显示个人信息的存储过程的操作进行封装.
 * 
 * 修 改 者：
 * 
 * 修改历史： 
 * 
 * 
 * */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataObject;

namespace Logic
{
    public class SelfMessage
    {
        #region 个人主页显示个人信息

        /// <summary>
        /// 个人主页显示个人信息
        /// </summary>
        public DataTable ShowSelfMessage(string aUserEmail)
        {
            SqlStoreCommand mSqlStoreCommand = new SqlStoreCommand();
            mSqlStoreCommand.StoreProdureName = "Chat_User";
            mSqlStoreCommand.ClearParameters();
            mSqlStoreCommand.AddParameters("@mFunctionName", "SearchOne");
            mSqlStoreCommand.AddParameters("@mUserEmail", aUserEmail);
            DataTable mShowSelfMessage = mSqlStoreCommand.ExecuteDataTable();
            return mShowSelfMessage;
        }

        #endregion

        #region 修改个人信息
        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="aUserEmail">用户邮箱</param>
        /// <param name="aUserName">用户昵称</param>
        /// <param name="aRemark">备注</param>
       public int UpdateSelfMessage(string aUserEmail,string aUserName,string aRemark)
       {
            SqlStoreCommand mSqlStoreCommand = new SqlStoreCommand();
            mSqlStoreCommand.StoreProdureName = "Chat_User";
            mSqlStoreCommand.ClearParameters();
            mSqlStoreCommand.AddParameters("mFunctionName", "Update");
            mSqlStoreCommand.AddParameters("@mUserEmail",aUserEmail);
            mSqlStoreCommand.AddParameters("@mUserName",aUserName);
            mSqlStoreCommand.AddParameters("@mRemark", aRemark);
            mSqlStoreCommand.AddParameters("@mModifyTime",System.DateTime.Now);
          int mUpdateSelfMessage=mSqlStoreCommand.ExecuteNonQuery();
          return mUpdateSelfMessage;
           
       }
      #endregion

        #region 修改密码
       /// <summary>
       /// 修改密码
       /// </summary>
       /// <param name="aUserEmail">用户邮箱</param>
       /// <param name="aUserPassword">原密码</param>
       /// <param name="aUserNewPassword">所要修改的密码</param>
       /// <returns></returns>

        public int UpdatePassword(string aUserEmail,string aUserPassword,string aUserNewPassword)
        {
            SqlStoreCommand mSqlStoreCommand = new SqlStoreCommand();
            mSqlStoreCommand.StoreProdureName = "Chat_User";
            mSqlStoreCommand.ClearParameters();
            mSqlStoreCommand.AddParameters("mFunctionName", "GetPassword");
            mSqlStoreCommand.AddParameters("@mUserEmail", aUserEmail);
            if (Encryption.EncryptString(aUserPassword, "jingyetankey").Equals(Convert.ToString(mSqlStoreCommand.ExecuteFunction())))
            {
                mSqlStoreCommand.ClearParameters();
                mSqlStoreCommand.AddParameters("mFunctionName", "UpdatePassword");
                mSqlStoreCommand.AddParameters("@mUserEmail", aUserEmail);
                mSqlStoreCommand.AddParameters("@mNuserPassword", Encryption.EncryptString(aUserNewPassword, "jingyetankey"));

            }
            
            int mUpdatePassword = mSqlStoreCommand.ExecuteNonQuery();
            return  mUpdatePassword;
         }

        #endregion

    }
}
