using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tdf.CSharp.Helper.UtilsHelper
{
    public static class RmbHelper
    {
        #region 数字转大写金额
        /// <summary>
        /// 数字转大写金额(壹、贰、叁)
        /// </summary>
        /// <param name="value">能转换成数字或者小数的任意类型</param>
        /// <returns>返回大写金额</returns>
        public static string ToRmb(object value)
        {
            try
            {
                var hash = double.Parse(value.ToString()).ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
                var results = Regex.Replace(hash, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[.]|$))))", "${b}${z}");
                hash = Regex.Replace(results, ".", m => "负圆空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万億兆京垓秭穰"[m.Value[0] - '-'].ToString());
                if (hash.Substring(hash.Length - 1, 1) == "圆") { hash += "整"; }
                return hash;
            }
            catch (Exception)
            {
                return "零";
            }
        }
        #endregion

        #region 数字转大写数字
        /// <summary>
        /// 数字转大写数字(一、二、三)
        /// </summary>
        /// <param name="value">能转换成数字或者小数的任意类型</param>
        /// <returns>返回大写数字</returns>
        public static string ToUpper(object value)
        {
            try
            {
                var hash = double.Parse(value.ToString()).ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
                var results = Regex.Replace(hash, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[.]|$))))", "${b}${z}");
                hash = Regex.Replace(results, ".", m => "负点空〇一二三四五六七八九空空空空空空空分角十百千万亿兆京垓秭穰"[m.Value[0] - '-'].ToString());
                if (hash.Substring(hash.Length - 1, 1) == "点")
                {
                    hash = hash.Replace("点", "");
                    return hash;
                }
                else
                {
                    hash = hash.Replace("角", "").Replace("分", "");
                    return hash;
                }
            }
            catch (Exception)
            {
                return "〇";
            }
        }
        #endregion

        #region TestMethod
        public static void TestMethod()
        {
            Console.WriteLine(RmbHelper.ToRmb(12345.12));
            Console.WriteLine(RmbHelper.ToRmb(12345));
            Console.WriteLine(RmbHelper.ToUpper(12345.12));
        }
        #endregion


    }
}
