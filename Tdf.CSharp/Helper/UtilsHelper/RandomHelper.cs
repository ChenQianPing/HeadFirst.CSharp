using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Helper.UtilsHelper
{
    public static class RandomHelper 
    {
        #region 随机布尔值
        /// <summary>
        /// 随机布尔值
        /// </summary>
        /// <param name="random"></param>
        /// <returns>随机布尔值</returns>
        public static bool NextBoolean(this Random random)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            return random.NextDouble() > 0.5;
        }
        #endregion

        #region 指定枚举类型的随机枚举值
        /// <summary>
        /// 指定枚举类型的随机枚举值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random"></param>
        /// <returns>指定枚举类型的随机枚举值</returns>
        public static T NextEnum<T>(this Random random) where T : struct
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }
            var array = System.Enum.GetValues(type);
            var index = random.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
            return (T)array.GetValue(index);
        }
        #endregion

        #region 随机数填充的指定长度的数组
        /// <summary>
        /// 随机数填充的指定长度的数组
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length">数组长度</param>
        /// <returns>>随机数填充的指定长度的数组</returns>
        public static byte[] NextBytes(this Random random, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            var data = new byte[length];
            random.NextBytes(data);
            return data;
        }
        #endregion

        #region 数组中的随机元素
        public static T NextItem<T>(this Random random, T[] items)
        {
            return items[random.Next(0, items.Length)];
        }
        #endregion

        #region 指定时间段内的随机时间值
        public static DateTime NextDateTime(this Random random, DateTime minValue, DateTime maxValue)
        {
            var ticks = minValue.Ticks + (long)((maxValue.Ticks - minValue.Ticks) * random.NextDouble());
            return new DateTime(ticks);
        }
        #endregion

        #region 随机时间值
        public static DateTime NextDateTime(this Random random)
        {
            return NextDateTime(random, DateTime.MinValue, DateTime.MaxValue);
        }
        #endregion

        #region 获取指定的长度的随机数字字符串
        public static string GetRandomNumberString(this Random random, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            char[] pattern = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var result = "";
            var n = pattern.Length;
            for (var i = 0; i < length; i++)
            {
                var rnd = random.Next(0, n);
                result += pattern[rnd];
            }
            return result;
        }
        #endregion

        #region 获取指定的长度的随机字母字符串
        public static string GetRandomLetterString(this Random random, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            char[] pattern =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
                'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };
            var result = "";
            var n = pattern.Length;
            for (var i = 0; i < length; i++)
            {
                var rnd = random.Next(0, n);
                result += pattern[rnd];
            }
            return result;
        }
        #endregion

        #region 获取指定的长度的随机字母和数字字符串
        public static string GetRandomLetterAndNumberString(this Random random, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            char[] pattern =
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
                'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };

            var result = "";
            var n = pattern.Length;
            for (var i = 0; i < length; i++)
            {
                var rnd = random.Next(0, n);
                result += pattern[rnd];
            }
            return result;
        }
        #endregion

    }
}
