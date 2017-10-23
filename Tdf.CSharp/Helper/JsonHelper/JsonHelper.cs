using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Helper.JsonHelper
{
    /// <summary>
    /// 提供了一个关于json的辅助类
    /// </summary>
    public class JsonHelper
    {
        #region Method
        /// <summary>
        /// 类对象转换成json格式
        /// </summary> 
        /// <returns></returns>
        public static string ToJson(object t)
        {
            return JsonConvert.SerializeObject(t, Newtonsoft.Json.Formatting.Indented,
                new JsonSerializerSettings {NullValueHandling = NullValueHandling.Include});
        }

        /// <summary>
        /// 类对象转换成json格式
        /// </summary>
        /// <param name="t"></param>
        /// <param name="hasNullIgnore">是否忽略NULL值</param>
        /// <returns></returns>
        public static string ToJson(object t, bool hasNullIgnore)
        {
            if (hasNullIgnore)
                return JsonConvert.SerializeObject(t, Newtonsoft.Json.Formatting.Indented,
                    new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
            else
                return ToJson(t);
        }

        /// <summary>
        /// json格式转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T FromJson<T>(string strJson) where T : class
        {
            if (!string.IsNullOrEmpty(strJson))
                return JsonConvert.DeserializeObject<T>(strJson);

            return null;
        }

        /// <summary>
        /// 功能描述：将List转换为Json
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static string ListToJson(IList<object> a)
        {
            var json = new DataContractJsonSerializer(a.GetType());
            var szJson = "";

            // 序列化
            using (var stream = new MemoryStream())
            {
                json.WriteObject(stream, a);
                szJson = Encoding.UTF8.GetString(stream.ToArray());
            }
            return szJson;
        }
        #endregion

        #region Property
        /// <summary>
        /// 数据状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 回传URL
        /// </summary>
        public string ReUrl { get; set; }

        /// <summary>
        /// 数据包
        /// </summary>
        public object Data { get; set; }
        #endregion

    }
}
