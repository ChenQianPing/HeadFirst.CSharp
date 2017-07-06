using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Helper.SerializerHelper
{
    /*
     * DataContract序列化
     * DataContract方式也是.net内置的，
     * 主要使用的DataContractJsonSerializer这个类，
     * 属于System.Runtime.Serialization.Json这个命名空间。
     * 需要引用System.Runtime.Serialization这个dll。
     */
    public static class DataContractExtensions
    {
        /// <summary>
        /// 将对象转化为Json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instanse"></param>
        /// <returns></returns>
        public static string ToJsonString<T>(this T instanse)
        {
            try
            {
                var js = new DataContractJsonSerializer(typeof(T));
                using (var ms = new MemoryStream())
                {
                    js.WriteObject(ms, instanse);
                    ms.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    var sr = new StreamReader(ms);
                    return sr.ReadToEnd();
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 将字符串转化为JSON对象，如果转换失败，返回default(T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T ToJsonObject<T>(this string s)
        {
            try
            {
                var js = new DataContractJsonSerializer(typeof(T));
                using (var ms = new MemoryStream())
                {
                    var sw = new StreamWriter(ms);
                    sw.Write(s);
                    sw.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    return (T)js.ReadObject(ms);
                }
            }
            catch
            {
                return default(T);
            }
        }


    }
}
