﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Helper.UtilsHelper
{
    /*
     * 分布式系统唯一ID生成方案汇总
     * http://www.cnblogs.com/haoxinyue/p/5208136.html
     */
    public static class GuidHelper
    {
        #region 根据GUID获取16位的唯一字符串
        /// <summary>
        /// 根据GUID获取16位的唯一字符串  
        /// </summary>
        /// <returns></returns>
        public static string GuidTo16String()
        {
            long i = 1;
            foreach (var b in Guid.NewGuid().ToByteArray())
                i *= ((int) b + 1);
            return $"{i - DateTime.Now.Ticks:x}";
        }
        #endregion

        #region 根据GUID获取19位的唯一数字序列
        /// <summary>
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>
        /// <returns></returns>
        public static long GuidToLongId()
        {
            var bytes = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(bytes, 0);
        }
        #endregion

        #region NHibernate的Comb算法
        /*
         * 为了解决UUID无序的问题，
         * NHibernate在其主键生成方式中提供了Comb算法（combined guid/timestamp）。
         * 保留GUID的10个字节，用另6个字节表示GUID生成的时间（DateTime）。
         * 
         * 用上面的算法测试一下，得到如下的结果：作为比较，
         * 前面3个是使用COMB算法得出的结果，
         * 最后12个字符串是时间序（统一毫秒生成的3个UUID），
         * 过段时间如果再次生成，则12个字符串会比图示的要大。
         * 后面3个是直接生成的GUID。
         * 
         * 49279fab-666e-4c6a-9d72-a7aa014ca980
         * ecd6b259-e379-4868-ba3e-a7aa014ca981
         * facb4b7d-0147-40b1-839a-a7aa014ca981
         * 3206b5db-0aff-494d-b56a-a7aa014ca981
         * d859da46-74d7-487e-bc7f-a7aa014ca981
         * b0008c3f-1e62-408f-b88e-a7aa014ca981
         * 
         */
        public static Guid GenerateComb()
        {
            var guidArray = Guid.NewGuid().ToByteArray();

            var baseDate = new DateTime(1900, 1, 1);
            var now = DateTime.Now;

            // Get the days and milliseconds which will be used to build    
            // the byte string    
            var days = new TimeSpan(now.Ticks - baseDate.Ticks);
            var msecs = now.TimeOfDay;

            // Convert to a byte array        
            // Note that SQL Server is accurate to 1/300th of a    
            // millisecond so we divide by 3.333333    
            var daysArray = BitConverter.GetBytes(days.Days);
            var msecsArray = BitConverter.GetBytes((long)
                (msecs.TotalMilliseconds/3.333333));

            // Reverse the bytes to match SQL Servers ordering    
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Copy the bytes into the guid    
            Array.Copy(daysArray, daysArray.Length - 2, guidArray,
                guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray,
                guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }
        #endregion

        #region 创建唯一的订单号, 考虑时间因素
        /// <summary>
        /// 创建唯一的订单号, 考虑时间因素
        /// </summary>
        /// <returns></returns>
        private static string GetUniqueKey()
        {
            var maxSize = 8;
            var a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var chars = a.ToCharArray();
            var data = new byte[1];

            // 使用RNGCryptoServiceProvider类创建唯一的最多8位数字符串；
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            var size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }
        #endregion

        #region Oracle格式的SYS_GUID()
        public static string GenerateNewId()
        {
            /*
             * 234E45F0077881AAE0430AA3034681AA；
             */
            return Guid.NewGuid().ToString("N").ToUpper();
        }
        #endregion

        public static void TestGuidHelper()
        {

            /*
             * 22位：14位时间串+8位随机串；
             * 20170712235513 p7HB7Eb9  
             * 20170712235513iigw1hFL
             * 20170712235513ZTLcc6RN
            20170712235513JfZyZnne
            20170712235513gzB7jumu
            20170712235513aqi4bgTs
            20170712235513zGkPvtfF
            20170712235513tjmGXki4
            20170712235513QdIu1ZII
            20170712235513cln5pUrC
            */

            for (var i = 0; i < 10; i++)
            {
                var str = $"{DateTime.Now:yyyyMMddHHmmss}{GetUniqueKey()}";
                Console.WriteLine(str);
            }
            Console.ReadKey();
        }

    }
}
