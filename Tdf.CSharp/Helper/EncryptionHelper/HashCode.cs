﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Helper.EncryptionHelper
{
    /// <summary>
    /// 得到随机安全码（哈希加密）。
    /// </summary>
    public class HashEncode
    {
        public HashEncode()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 得到随机哈希加密字符串
        /// </summary>
        /// <returns></returns>
        public static string GetSecurity()
        {
            var security = HashEncoding(GetRandomValue());
            return security;
        }

        /// <summary>
        /// 得到一个随机数值
        /// </summary>
        /// <returns></returns>
        public static string GetRandomValue()
        {
            var seed = new Random();
            var randomVaule = seed.Next(1, int.MaxValue).ToString();
            return randomVaule;
        }

        /// <summary>
        /// 哈希加密一个字符串
        /// </summary>
        /// <param name="security"></param>
        /// <returns></returns>
        public static string HashEncoding(string security)
        {
            var code = new UnicodeEncoding();
            var message = code.GetBytes(security);
            var arithmetic = new SHA512Managed();
            var value = arithmetic.ComputeHash(message);
            security = "";
            foreach (var o in value)
            {
                security += (int)o + "O";
            }
            return security;
        }

        public static void TestHashEncode()
        {
            // 得到随机哈希加密字符串
            var s1 = GetSecurity();
            Console.Write("得到随机哈希加密字符串:" + s1 + "\n");

            // 得到一个随机数值
            var s2 = GetRandomValue();
            Console.Write("得到一个随机数值:" + s2 + "\n");

            // 哈希加密一个字符串
            var s3 = HashEncoding("Bobby");
            Console.Write("哈希加密一个字符串:" + s3 + "\n");
        }
    }
}
