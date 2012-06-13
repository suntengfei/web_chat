/*
 * 
 * 版    本： Ver 1.0
 * 
 * 类    名： Regist 
 * 
 * 文 件 名： Regist.cs 
 * 
 * 作    者： 
 * 
 * 日    期： 
 * 
 * 描    述： Regist 将执行注册账号及检测账号是否重复的存储过程的操作进行封装.
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
using DataObject;

namespace Logic
{
    public class Regist
    {
        #region 检测注册账号是否重复


        /// <summary>
        /// 检测注册账号是否重复

        /// </summary>
        /// <param name="aUserEmail">用户邮箱</param>
        /// <returns></returns>

        public int CheckUserRegist(string aUserEmail)
        {
            SqlStoreCommand mSqlStoreCommand = new SqlStoreCommand();
            mSqlStoreCommand.StoreProdureName = "Chat_User";
            mSqlStoreCommand.ClearParameters();
            mSqlStoreCommand.AddParameters("@mFunctionName", "CheckInsert");
            mSqlStoreCommand.AddParameters("@mUserEmail", aUserEmail);
            int mCheckUserRegist = Convert.ToInt32(mSqlStoreCommand.ExecuteFunction());
            return mCheckUserRegist;

        }

        #endregion

        #region 注册

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="userEmail">用户邮箱</param>
        /// <param name="userName">用户名</param>
        /// <param name="userPassWord">密码</param>
        public int UserRegist(string aUserName, string aUserEmail, string aUserPassWord, string aRemark)
        {
            SqlStoreCommand mSqlStoreCommand = new SqlStoreCommand();
            mSqlStoreCommand.StoreProdureName = "Chat_User";
            mSqlStoreCommand.ClearParameters();
            mSqlStoreCommand.AddParameters("@mFunctionName", "Insert");
            mSqlStoreCommand.AddParameters("@mUserName", aUserName);
            mSqlStoreCommand.AddParameters("@mUserEmail", aUserEmail);
            mSqlStoreCommand.AddParameters("@mUserPassWord", Encryption.EncryptString(aUserPassWord, "jingyetankey"));//密码加密后存到数据库
            mSqlStoreCommand.AddParameters("@mRemark", aRemark);
            mSqlStoreCommand.AddParameters("@mCreateTime", System.DateTime.Now);
            mSqlStoreCommand.AddParameters("@mModifyTime", System.DateTime.Now);
            int mUserRegist= Convert.ToInt32(mSqlStoreCommand.ExecuteFunction());
            return mUserRegist;
        }

        #endregion     
    }
}
