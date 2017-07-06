using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Tdf.CSharp.Helper.SerializerHelper
{
    /*
     * JavaScriptSerializer方式序列化；
     * avaScriptSerializer这个类是.Net内置的，
     * 属于System.Web.Script.Serialization这个命名空间下面。
     * 需要引用System.Web.Extensions这个dll。
     */
    public static class JavascriptExtentions
    {
        public static string ToScriptJsonString<T>(this T instanse)
        {
            try
            {
                var js = new JavaScriptSerializer();
                return js.Serialize(instanse);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static T ToScriptJsonObject<T>(this string s)
        {
            try
            {
                var js = new JavaScriptSerializer();
                return js.Deserialize<T>(s);
            }
            catch
            {
                return default(T);
            }
        }

    }
}
