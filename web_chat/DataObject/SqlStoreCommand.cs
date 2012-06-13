﻿



/*
 * 
 * 版    本： Ver 1.0
 * 
 * 类    名： SqlStoreCommand 
 * 
 * 文 件 名： SqlStoreCommand.cs 
 * 
 * 作    者： 
 * 
 * 日    期： 
 * 
 * 描    述： SqlStoreCommand 将执行存储过程的操作进行封装.
 * 
 * 修 改 者：
 * 
 * 修改历史： 
 * 
 * 
 * */


using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Configuration;
namespace DataObject
{
    public class SqlStoreCommand
    {
        #region 全局变量,属性.

        public SqlConnection Connection
        {
            get
            {
                return mCommand.Connection;
            }
            set
            {
                mCommand.Connection = value;
            }
        }


        private SqlCommand mCommand;
        public SqlCommand Command
        {
            get
            {
                return mCommand;
            }
            set
            {
                mCommand = value;
            }
        }


        /// <summary>
        /// 存储过程名称 可读写.
        /// </summary>
        public string StoreProdureName
        {
            get
            {
                return mCommand.CommandText;
            }
            set
            {
                mCommand.CommandText = value;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public SqlParameterCollection Parameters
        {
            get
            {
                return mCommand.Parameters;
            }
        }


        #endregion

        #region 自定义函数.

        /// <summary>
        /// 字符串连接函数.
        /// </summary>
        /// <param name="IpAddress">数据库地址.</param>
        /// <param name="DBName">数据库名称.</param>
        /// <param name="UserName">用户名.</param>
        /// <param name="UserPwd">密码.</param>
        /// <returns></returns>
        private string CombinationConnectionStr(string IpAddress, string DBName, string UserName, string UserPwd)
        {
            string pConnectStr = "";
            //pConnectStr = "user " + "id=" + UserName + ";" + "password=" + UserPwd + ";"
            //    + "initial catalog=" + DBName + ";"
            //    + "data source=" + IpAddress + ";"
            //    + "Connect Timeout=" + "30";

            pConnectStr = " Data Source=SUNTENGFEI-PC\\SQLEXPRESS;Initial Catalog=Chat;Integrated Security=True";
            //pConnectStr = "server = (local); Integrated Security = True; database = " + DBName + "; Connection Timeout=15";
           
            return pConnectStr;

        }


        /// <summary>
        /// 开启事务.
        /// </summary>
        public void BeginTrans()
        {
            try
            {
                if (Connection.State.ToString().ToLower() == "closed")  //Modify by LiuJun  2008/04/25
                {
                    this.Connection.Open();
                }

                this.mCommand.Transaction = this.Connection.BeginTransaction();
            }
            catch (Exception err)
            {
                throw (err);
            }
        }


        /// <summary>
        /// 提交事务.
        /// </summary>
        public void CommitTrans()
        {
            try
            {
                this.mCommand.Transaction.Commit();
                if (this.Connection.State.ToString() == "Open")
                {
                    this.Connection.Close();
                }
            }
            catch (Exception err)
            {
                throw (err);
            }
        }


        /// <summary>
        /// 事务回滚.
        /// </summary>
        public void RollbackTrans()
        {
            try
            {
                this.mCommand.Transaction.Rollback();
                if (this.Connection.State.ToString() == "Open")
                {
                    this.Connection.Close();
                }
            }
            catch (Exception err)
            {
                throw (err);
            }
        }


        /// <summary>
        /// 为存储过程添加参数.
        /// </summary>
        /// <param name="paramersName">参数名称.</param>
        /// <param name="value">参数值.</param>
        /// <returns></returns>
        public SqlParameter AddParameters(string paramersName, object value)
        {
            return mCommand.Parameters.AddWithValue(paramersName, value);
        }


        /// <summary>
        /// 清空存储过程参数.
        /// </summary>
        public void ClearParameters()
        {
            mCommand.Parameters.Clear();
        }


        /// <summary>
        /// 执行操作,返回DataSet.
        /// </summary>
        /// <returns></returns>
        public DataSet ExecuteDataSet()
        {
            DataSet result = new DataSet();

            SqlDataAdapter dataAdapter = null;
            try
            {
                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = mCommand;
                dataAdapter.Fill(result);
            }
            catch (SqlException e)
            {
                throw (e);
            }
            finally
            {
                dataAdapter.Dispose();
            }

            return result;
        }

        /// <summary>
        ///  执行数据操作,返回DataTable.
        ///  WebServices 端口不要使用,WebServices 端尽量使用DataSet.
        /// </summary>
        /// <returns></returns>
        public DataTable ExecuteDataTable()
        {
            DataTable result;

            SqlDataAdapter dataAdapter = null;
            try
            {
                dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = mCommand;
                result = new DataTable();
                dataAdapter.Fill(result);
            }
            catch (SqlException e)
            {
                throw (e);
            }
            finally
            {
                dataAdapter.Dispose();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object ExecuteFunction()
        {
            object result;

            try
            {
                if (mCommand.Connection.State == ConnectionState.Closed)
                {
                    mCommand.Connection.Open();
                }
                result = mCommand.ExecuteScalar();
            }
            catch (SqlException e)
            {
                throw (e);
            }
            finally
            {
                mCommand.Connection.Close();
            }

            return result;
        }


        /// <summary>
        /// 执行数据操作，返回受影响的行数.
        /// </summary>
        /// <returns></returns>
        public int ExecuteNonQuery()
        {
            int result;

            try
            {
                if (mCommand.Connection.State == ConnectionState.Closed)
                {
                    mCommand.Connection.Open();
                }
                result = mCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                mCommand.Connection.Close();
            }

            return result;
        }


        /// <summary>
        /// 关闭数据库连接,防止重复关闭数据库连接.
        /// </summary>
        public void Close()
        {
            if (this.Connection.State.ToString() == "Open")
            {
                this.Connection.Close();
            }
        }


        /// <summary>
        ///打开数据库连接，防止重复打开数据库.
        /// </summary>
        public void Open()
        {
            if (Connection.State.ToString().ToLower() == "closed")
            {
                this.Connection.Open();
            }
        }



        #endregion

        #region 构造函数.
        public SqlStoreCommand()
        {
            this.mCommand = new SqlCommand();
            this.Connection = new SqlConnection(CombinationConnectionStr(DataSource.HostAddress, DataSource.DataBaseName, DataSource.UserName, DataSource.Password));
            mCommand.CommandType = CommandType.StoredProcedure;
        
        }
        //this.Connection = new SqlConnection(CombinationConnectionStr(ConfigurationManager.AppSettings["DBHost"].ToString(), ConfigurationManager.AppSettings["DBName"].ToString(), ConfigurationManager.AppSettings["DBUser"].ToString(), ConfigurationManager.AppSettings["DBPsw"].ToString()));
        #endregion

    }
}
