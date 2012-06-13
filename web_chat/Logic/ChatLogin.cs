using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogDataObject;

namespace Logic
{
    public class ChatLogin
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
            mSqlStoreCommand.AddParameters("@mUserPassword", aUserPassword);
            int mCheckNum = Convert.ToInt32(mSqlStoreCommand.ExecuteFunction());

            return mCheckNum;
            
        }

        #endregion
    }
}
