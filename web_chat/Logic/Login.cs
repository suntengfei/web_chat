/*
 * 
 * 版    本： Ver 1.0
 * 
 * 类    名： Login 
 * 
 * 文 件 名： Loign.cs 
 * 
 * 作    者： 
 * 
 * 日    期： 
 * 
 * 描    述： Login 将执行验证登陆的存储过程的操作进行封装.
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
    public class Login
    {
        #region 登陆

        /// <summary>
        /// 验证登陆
        /// </summary>
        /// <param name="aUserEmail">用户邮箱</param>
        /// <param name="aUserPassword">密码</param>
        public int CheckLogin(string aUserEmail, string aUserPassword)
        {

            SqlStoreCommand mSqlStoreCommand = new SqlStoreCommand();
            mSqlStoreCommand.StoreProdureName = "Chat_User";
            mSqlStoreCommand.ClearParameters();
            mSqlStoreCommand.AddParameters("@mFunctionName", "Check");
            mSqlStoreCommand.AddParameters("@mUserEmail", aUserEmail);
            mSqlStoreCommand.AddParameters("@mUserPassword", Encryption.EncryptString(aUserPassword, "jingyetankey"));
            int mCheckLogin = Convert.ToInt32(mSqlStoreCommand.ExecuteFunction());
            return mCheckLogin;
            
        }

        #endregion
    }
}
