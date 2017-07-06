using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Helper.UtilsHelper
{
    public static class ExtensionHelper
    {
        /*
         * C#扩展方法：
         * 1、.Net内置对象的扩展方法；其中this string就表示给string对象添加扩展方法；
         * 2、一般对象的扩展方法；
         * 
         * Tips：
         * 扩展方法不能和调用的方法放到同一个类中；
         * 第一个参数必须要，并且必须是this，这是扩展方法的标识。如果方法里面还要传入其他参数，可以在后面追加参数；
         * 扩展方法所在的类必须是静态类；
         * 最好保证扩展方法和调用方法在同一个命名空间下；
         */
        public static string GetNotNullStr(this string strRes)
        {
            return strRes ?? string.Empty;
        }
    }
}
