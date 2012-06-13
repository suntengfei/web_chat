



/*
 * 
 * 版    本： Ver 1.0
 * 类    名： DataSource 
 * 文 件 名： DataSource.cs 
 * 
 * 作    者：
 * 
 * 日    期： 
 * 
 * 描    述： DataSource是一个全局对象，实现数据源的管理.

 *			  实现数据源的管理操作，在程序运行时该类被初始化成全局对象，其中使用静态成员. 
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

namespace DataObject
{
    public class DataSource
    {
        #region  数据连接信息

        //DataBase Name.
        public static string DataBaseName = "Chat";

        //User Name.
        public static string UserName = "stf";

        //Password.
        public static string Password = "123123";

        //Host.
        public static string HostAddress = "localhost";

     

        #endregion
    }
}
