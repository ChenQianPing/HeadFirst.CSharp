using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Helper.UtilsHelper
{
    public class RandomIdHelper : IFormattable
    {
        public const string Allwords = "1234567890qwertyuiopasdfghjklzxcvbnm1234567890QWERTYUIOPASDFGHJKLZXCVBNM";

        public const string Simplewords = "2345678wertyuipasdfghjkzxcvbnm2345678WERTYUPASDFGHJKLZXCVBNM";

        #region 私有对象

        public const string One = "{0}";
        public static RandomIdHelper Rid = new RandomIdHelper(0);
        public static readonly Random Rand = new Random();
        private static string ToFormat(int length)
        {
            var sb = new StringBuilder(length * 3);
            for (var i = 0; i < length; i++)
            {
                sb.Append(One);
            }
            return sb.ToString();
        }

        public readonly string Dict;
        private readonly int _rMax;
        private readonly string _format;
        #endregion

        /// <summary> 构造函数
        /// </summary>
        /// <param name="length">生成Id长度</param>
        /// <param name="dict">随机字符字典,默认字典为0-9a-zA-Z</param>
        public RandomIdHelper(int length, string dict = Allwords)
            : this(RandomIdHelper.ToFormat(length), dict)
        { }

        /// <summary> 构造函数
        /// </summary>
        /// <param name="format">生成Id格式</param>
        /// <param name="dict">随机字符字典,默认字典为0-9a-zA-Z</param>
        public RandomIdHelper(string format, string dict = Allwords)
        {
            Dict = dict;
            _format = format;
            _rMax = dict.Length;
        }

        /// <summary>
        /// 生成Id
        /// </summary>
        /// <returns></returns>
        public string Create()
        {
            return string.Format(_format, this);
        }

        /// <summary> 
        /// 生成Id
        /// </summary>
        /// <param name="length">生成Id长度</param>
        /// <param name="dict">随机字符字典,默认字典为0-9a-zA-Z</param>
        public static string Create(int length, string dict = Allwords)
        {
            return new RandomIdHelper(length, dict).Create();
        }

        /// <summary> 
        /// 生成Id
        /// </summary>
        /// <param name="format">生成Id格式</param>
        /// <param name="dict">随机字符字典,默认字典为0-9a-zA-Z</param>
        public static string Create(string format, string dict = Allwords)
        {
            return new RandomIdHelper(format, dict).Create();
        }

        public static void TestMethod()
        {
            // 使用默认字典生成4位随机字符串,默认字典中不包含l,1,O,0,q,9等容易混淆字符
            Console.WriteLine(RandomIdHelper.Create(4));

            // 使用完整字典(0-9a-zA-Z),生成4位随机字符
            Console.WriteLine(RandomIdHelper.Create(4, RandomIdHelper.Allwords));

            // 使用指定中文字典,生成4位随机字符
            Console.WriteLine(RandomIdHelper.Create(10, "多少级开发和贷款撒了花费大量时间好快理发店撒娇哦就开放了的撒酒阿克里福德就是卡看了就分开的世界里分开家里的事"));

            // 使用指定字典生成特定格式的随机字符
            Console.WriteLine(RandomIdHelper.Create("SN:{0}{0}{0}{0}-{0}{0}{0}-{0}{0}{0}.{0}{0}", "123456abcdef"));
        }

        #region IFormattable 成员

        string IFormattable.ToString(string format, IFormatProvider formatProvider)
        {
            return Dict[Rand.Next(0, _rMax)].ToString();
        }

        #endregion

    }
}


/*
 * CodeMonkey
 */
