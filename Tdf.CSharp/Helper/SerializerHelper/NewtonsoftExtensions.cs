using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tdf.CSharp.Helper.SerializerHelper
{
    public class NewtonsoftExtensions
    {
        #region 序列化
        /// <summary>
        /// 将对象(包含集合对象)序列化为Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObjToJson(object obj)
        {
            var strRes = string.Empty;
            try
            {
                strRes = JsonConvert.SerializeObject(obj);
            }
            catch
            { }

            return strRes;
        }

        /// <summary>
        /// 将xml转换为json
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string SerializeXmlToJson(System.Xml.XmlNode node)
        {
            var strRes = string.Empty;
            try
            {
                strRes = JsonConvert.SerializeXmlNode(node);
            }
            catch
            { }

            return strRes;
        }

        /// <summary>
        /// 支持Linq格式的xml转换
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string SerializeXmlToJson(System.Xml.Linq.XNode node)
        {
            var strRes = string.Empty;
            try
            {
                strRes = JsonConvert.SerializeXNode(node);
            }
            catch
            { }

            return strRes;
        }
        #endregion

        #region 反序列化
        /// <summary>
        /// 将json反序列化为实体对象(包含DataTable和List<>集合对象)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T DeserializeJsonToObj<T>(string strJson)
        {
            T oRes = default(T);
            try
            {
                oRes = JsonConvert.DeserializeObject<T>(strJson);
            }
            catch
            { }

            return oRes;
        }

        /// <summary>
        /// 将Json数组转换为实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lstJson"></param>
        /// <returns></returns>
        public static List<T> JsonLstToObjs<T>(List<string> lstJson)
        {
            var lstRes = new List<T>();
            try
            {
                foreach (var strObj in lstJson)
                {
                    // 将json反序列化为对象
                    var oRes = JsonConvert.DeserializeObject<T>(strObj);
                    lstRes.Add(oRes);
                }
            }
            catch
            { }

            return lstRes;
        }
        #endregion
    }
}
