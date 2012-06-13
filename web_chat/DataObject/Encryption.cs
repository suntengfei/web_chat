


/*
 * 
 * 版    本： Ver 1.0 
 * 类    名： Encryption 
 * 文 件 名： Encryption.cs 
 * 
 * 作    者： 刘俊.
 * 
 * 日    期： 2009年2月15日 .
 * 
 * 描    述： 用于数据库连接串的加密解密.
 * 
 * 修改历史：  
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObject
{
    public class Encryption
    {
        #region 根据密钥进行字符串加密.

        /// <summary>
        /// 根据密钥进行字符串加密.
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>机密后的字符串</returns>
    public static string EncryptString(string str, string key)
        {
            try
            {
                //根据UTF8编码规则取得二进制流..
                byte[] bytesIn = System.Text.Encoding.UTF8.GetBytes(str);

                //访问数据加密标准 (DES) 算法的加密服务提供程序.
                System.Security.Cryptography.DESCryptoServiceProvider des =
                    new System.Security.Cryptography.DESCryptoServiceProvider();

                //对密钥进行编码.
                byte[] bytesKey = System.Text.Encoding.UTF8.GetBytes(key);

                //初始化机密适配器..
                des.Key = ResizeBytesArray(bytesKey, des.Key.Length);
                des.IV = ResizeBytesArray(bytesKey, des.IV.Length);

                System.IO.MemoryStream msOut = new System.IO.MemoryStream();

                //基本的加密转换运算.
                System.Security.Cryptography.ICryptoTransform desdecrypt =
                    des.CreateEncryptor();

                //定义将数据流链接到加密转换的流.
                System.Security.Cryptography.CryptoStream cryptStreem =
                    new System.Security.Cryptography.CryptoStream(msOut,
                    desdecrypt,
                    System.Security.Cryptography.CryptoStreamMode.Write);

                //二进制流输出.
                cryptStreem.Write(bytesIn, 0, bytesIn.Length);
                cryptStreem.FlushFinalBlock();

                //取得加密后的二进制流取得.
                byte[] bytesOut = msOut.ToArray();

                //关闭流.
                cryptStreem.Close();
                msOut.Close();

                //生成由以 64 为基的二进制数组.
                return System.Convert.ToBase64String(bytesOut);
            }
            catch (Exception e)
            {
                throw (e);
                //				return "";
            }
        }

        #endregion

        #region 从加密的密钥交换数据中提取机密信息



        /// <summary>
        /// 从加密的密钥交换数据中提取机密信息<br/>
        /// </summary>
        /// <param name="str">被加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>还原的字符串</returns>
        public static string DecryptString(string str, string key)
        {
            try
            {
                //义访问数据加密标准 (DES) 算法的加密服务提供程序 (CSP) 版本的包装对象.
                System.Security.Cryptography.DESCryptoServiceProvider des =
                    new System.Security.Cryptography.DESCryptoServiceProvider();

                //对密钥进行编码.
                byte[] bytesKey = System.Text.Encoding.UTF8.GetBytes(key);

                //初始化机密适配器..
                des.Key = ResizeBytesArray(bytesKey, des.Key.Length);
                des.IV = ResizeBytesArray(bytesKey, des.IV.Length);

                //返回由以 64 为基的二进制数组
                byte[] bytesIn = System.Convert.FromBase64String(str);

                System.IO.MemoryStream msIn =
                    new System.IO.MemoryStream(bytesIn);

                //定义基本的加密转换运算.
                System.Security.Cryptography.ICryptoTransform desdecrypt =
                    des.CreateDecryptor();

                //定义将数据流链接到加密转换的流。.
                System.Security.Cryptography.CryptoStream cryptStreem =
                    new System.Security.Cryptography.CryptoStream(msIn,
                    desdecrypt,
                    System.Security.Cryptography.CryptoStreamMode.Read);

                //以UTF8编码从字节流中读取字符.
                System.IO.StreamReader srOut =
                    new System.IO.StreamReader(cryptStreem, System.Text.Encoding.UTF8);

                //以UTF8编码从字节流中读取字符.
                string result = srOut.ReadToEnd();

                srOut.Close();
                cryptStreem.Close();
                msIn.Close();

                return result;
            }
            catch (Exception ep)
            {
                throw (ep);
                //				return "";
            }

        }

        #endregion

        #region 重新统一格式化二进制流



        /// <summary>
        /// 重新统一格式化二进制流<br/>
        /// </summary>
        /// <param name="bytes">二进制数组</param>
        /// <param name="newSize">格式化的大小</param>
        /// <returns>返回格式化后的大小</returns>
        private static byte[] ResizeBytesArray(byte[] bytes, int newSize)
        {
            byte[] newBytes = new byte[newSize];
            if (bytes.Length <= newSize)
            {
                for (int i = 0; i < bytes.Length; i++)
                    newBytes[i] = bytes[i];
            }
            else
            {
                int pos = 0;
                for (int i = newSize; i < bytes.Length; i++)
                {
                    newBytes[pos++] ^= bytes[i];
                    if (pos >= newBytes.Length)
                        pos = 0;
                }
            }
            return newBytes;
        }

        #endregion

    }
}
