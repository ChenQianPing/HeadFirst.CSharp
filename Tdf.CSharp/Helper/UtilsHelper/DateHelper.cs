using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Helper.UtilsHelper
{
    /// <summary>
    /// 月份枚举
    /// </summary>
    public enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    /// <summary>
    /// 季度枚举
    /// </summary>
    public enum Quarter
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4
    }

    public class DateHelper
    {
        /// <summary>
        /// 获取指定年月的天数
        /// </summary>
        /// <param name="month">月</param>
        /// <param name="year">年</param>
        /// <returns>返回指定年月的天数</returns>
        public static int GetDayCountOfMonth(int month, int year)
        {
            return GetEndOfMonth(month, year).Day;
        }

        /// <summary>
        /// 获取当前月份的最后时间
        /// </summary>
        /// <returns>返回当前月份的最后时间</returns>
        public static DateTime GetEndOfCurrentMonth()
        {
            return GetEndOfMonth(DateTime.Now.Month, DateTime.Now.Year);
        }

        /// <summary>
        /// 获取本季度的最后时间
        /// </summary>
        /// <returns>返回本季度的最后时间</returns>
        public static DateTime GetEndOfCurrentQuarter()
        {
            return GetEndOfQuarter(DateTime.Now.Year, GetQuarter((Month) DateTime.Now.Month));
        }

        /// <summary>
        /// 获取本周的最后时间
        /// </summary>
        /// <returns>返回本周的最后时间</returns>
        public static DateTime GetEndOfCurrentWeek()
        {
            var time = GetStartOfCurrentWeek().AddDays(6.0);
            return new DateTime(time.Year, time.Month, time.Day, 23, 59, 59, 999);
        }

        /// <summary>
        /// 获取当前年度的最后时间
        /// </summary>
        /// <returns>返回当前年度的最后时间</returns>
        public static DateTime GetEndOfCurrentYear()
        {
            return GetEndOfYear(DateTime.Now.Year);
        }

        /// <summary>
        /// 获取一天的最后时间
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>返回一天的最后时间</returns>
        public static DateTime GetEndOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }

        /// <summary>
        /// 获取指定月份的第一周的最后时间
        /// </summary>
        /// <param name="month">月</param>
        /// <param name="year">年</param>
        /// <returns>返回指定月份的第一周的最后时间</returns>
        public static DateTime GetEndOfFirstWeekOfMonth(int month, int year)
        {
            var time3 = new DateTime(year, month, 1);
            var dayOfWeek = (int) time3.DayOfWeek;
            return GetStartOfMonth(month, year).AddDays(6.0 - dayOfWeek);
        }

        /// <summary>
        /// 获取上月的最后时间
        /// </summary>
        /// <returns>返回上月的最后时间</returns>
        public static DateTime GetEndOfLastMonth()
        {
            return DateTime.Now.Month == 1 ? GetEndOfMonth(12, DateTime.Now.Year - 1) : GetEndOfMonth(DateTime.Now.Month - 1, DateTime.Now.Year);
        }

        /// <summary>
        /// 获取上一季度的最后时间
        /// </summary>
        /// <returns>返回上一季度的最后时间</returns>
        public static DateTime GetEndOfLastQuarter()
        {
            return DateTime.Now.Month <= 3 ? GetEndOfQuarter(DateTime.Now.Year - 1, Quarter.Fourth) : GetEndOfQuarter(DateTime.Now.Year, GetQuarter((Month) DateTime.Now.Month));
        }

        /// <summary>
        /// 获取上周的最后时间
        /// </summary>
        /// <returns>返回上周的最后时间</returns>
        public static DateTime GetEndOfLastWeek()
        {
            var time = GetStartOfLastWeek().AddDays(6.0);
            return new DateTime(time.Year, time.Month, time.Day, 23, 59, 59, 999);
        }

        /// <summary>
        /// 获取去年的最后时间
        /// </summary>
        /// <returns>返回去年的最后时间</returns>
        public static DateTime GetEndOfLastYear()
        {
            return GetEndOfYear(DateTime.Now.Year - 1);
        }

        /// <summary>
        /// 获取指定月份的最后时间
        /// </summary>
        /// <param name="month">月</param>
        /// <param name="year">年</param>
        /// <returns>返回指定月份的最后时间</returns>
        public static DateTime GetEndOfMonth(int month, int year)
        {
            return new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59, 999);
        }

        /// <summary>
        /// 获取指定日期当月的最后日期
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>返回指定日期当月的最后日期</returns>
        public static DateTime GetEndOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999);
        }

        /// <summary>
        /// 获取指定季度的最后时间
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="qtr">第几季度</param>
        /// <returns>返回指定季度的最后时间</returns>
        public static DateTime GetEndOfQuarter(int year, Quarter qtr)
        {
            if (qtr == Quarter.First)
            {
                return new DateTime(year, 3, DateTime.DaysInMonth(year, 3), 23, 59, 59, 999);
            }
            if (qtr == Quarter.Second)
            {
                return new DateTime(year, 6, DateTime.DaysInMonth(year, 6), 23, 59, 59, 999);
            }
            if (qtr == Quarter.Third)
            {
                return new DateTime(year, 9, DateTime.DaysInMonth(year, 9), 23, 59, 59, 999);
            }
            return new DateTime(year, 12, DateTime.DaysInMonth(year, 12), 23, 59, 59, 999);
        }

        /// <summary>
        /// 获取指定月份指定周的最后时间
        /// </summary>
        /// <param name="weekIndex">星期</param>
        /// <param name="month">月</param>
        /// <param name="year">年</param>
        /// <returns>返回指定月份指定周的最后时间</returns>
        public static DateTime GetEndOfWeekOfMonth(int weekIndex, int month, int year)
        {
            int weekCountOfMonth = GetWeekCountOfMonth(month, year);
            if ((weekIndex < 1) || (weekIndex > weekCountOfMonth))
            {
                throw new Exception("周数非法。周数不能小于1或者大于" + weekCountOfMonth);
            }
            if (weekIndex == 1)
            {
                return GetEndOfFirstWeekOfMonth(month, year);
            }
            if (weekIndex == GetWeekCountOfMonth(month, year))
            {
                return GetEndOfMonth(month, year);
            }
            return GetStartOfWeekOfMonth(weekIndex, month, year).AddDays(7.0).AddMilliseconds(-1.0);
        }

        /// <summary>
        /// 获取指定年度的最后时间
        /// </summary>
        /// <param name="year">年</param>
        /// <returns>返回指定年度的最后时间</returns>
        public static DateTime GetEndOfYear(int year)
        {
            return new DateTime(year, 12, DateTime.DaysInMonth(year, 12), 23, 59, 59, 999);
        }

        /// <summary>
        /// 获取指定月份所处的季度
        /// </summary>
        /// <param name="month">月</param>
        /// <returns>返回指定月份所处的季度</returns>
        public static Quarter GetQuarter(Month month)
        {
            if (month <= Month.March)
            {
                return Quarter.First;
            }
            if ((month >= Month.April) && (month <= Month.June))
            {
                return Quarter.Second;
            }
            if ((month >= Month.July) && (month <= Month.September))
            {
                return Quarter.Third;
            }
            return Quarter.Fourth;
        }

        /// <summary>
        /// 获取当前月份的开始时间
        /// </summary>
        /// <returns>返回当前月份的开始时间</returns>
        public static DateTime GetStartOfCurrentMonth()
        {
            return GetStartOfMonth(DateTime.Now.Month, DateTime.Now.Year);
        }

        /// <summary>
        /// 获取当前季度的开始时间
        /// </summary>
        /// <returns>返回当前季度的开始时间</returns>
        public static DateTime GetStartOfCurrentQuarter()
        {
            return GetStartOfQuarter(DateTime.Now.Year, GetQuarter((Month) DateTime.Now.Month));
        }

        /// <summary>
        /// 获取本周的开始时间
        /// </summary>
        /// <returns>返回本周的开始时间</returns>
        public static DateTime GetStartOfCurrentWeek()
        {
            var dayOfWeek = (int) DateTime.Now.DayOfWeek;
            var time = DateTime.Now.Subtract(TimeSpan.FromDays((double) dayOfWeek));
            return new DateTime(time.Year, time.Month, time.Day, 0, 0, 0, 0);
        }

        /// <summary>
        /// 获取今年的开始时间
        /// </summary>
        /// <returns>返回今年的开始时间</returns>
        public static DateTime GetStartOfCurrentYear()
        {
            return GetStartOfYear(DateTime.Now.Year);
        }

        /// <summary>
        /// 获取今天的开始时间
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>返回今天的开始时间</returns>
        public static DateTime GetStartOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        /// <summary>
        /// 获取上月的开始时间
        /// </summary>
        /// <returns>返回上月的开始时间</returns>
        public static DateTime GetStartOfLastMonth()
        {
            if (DateTime.Now.Month == 1)
            {
                return GetStartOfMonth(12, DateTime.Now.Year - 1);
            }
            return GetStartOfMonth(DateTime.Now.Month - 1, DateTime.Now.Year);
        }

        /// <summary>
        /// 获取上一季度的开始时间
        /// </summary>
        /// <returns>返回上一季度的开始时间</returns>
        public static DateTime GetStartOfLastQuarter()
        {
            if (DateTime.Now.Month <= 3)
            {
                return GetStartOfQuarter(DateTime.Now.Year - 1, Quarter.Fourth);
            }
            return GetStartOfQuarter(DateTime.Now.Year, GetQuarter((Month) DateTime.Now.Month));
        }

        /// <summary>
        /// 获取上周的开始时间
        /// </summary>
        /// <returns>返回上周的开始时间</returns>
        public static DateTime GetStartOfLastWeek()
        {
            var num = ((int) DateTime.Now.DayOfWeek) + 7;
            var time = DateTime.Now.Subtract(TimeSpan.FromDays((double) num));
            return new DateTime(time.Year, time.Month, time.Day, 0, 0, 0, 0);
        }

        /// <summary>
        /// 获取去年的开始时间
        /// </summary>
        /// <returns>返回去年的开始时间</returns>
        public static DateTime GetStartOfLastYear()
        {
            return GetStartOfYear(DateTime.Now.Year - 1);
        }

        /// <summary>
        /// 获取指定月份的开始时间
        /// </summary>
        /// <param name="month">月</param>
        /// <param name="year">年</param>
        /// <returns>返回指定月份的开始时间</returns>
        public static DateTime GetStartOfMonth(int month, int year)
        {
            return new DateTime(year, month, 1, 0, 0, 0, 0);
        }

        /// <summary>
        /// 获取指定日期当月的开始日期
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>返回指定日期当月的开始日期</returns>
        public static DateTime GetStartOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0, 0);
        }

        /// <summary>
        /// 获取指定季度的开始时间
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="qtr">第几季度</param>
        /// <returns>返回指定季度的开始时间</returns>
        public static DateTime GetStartOfQuarter(int year, Quarter qtr)
        {
            if (qtr == Quarter.First)
            {
                return new DateTime(year, 1, 1, 0, 0, 0, 0);
            }
            if (qtr == Quarter.Second)
            {
                return new DateTime(year, 4, 1, 0, 0, 0, 0);
            }
            if (qtr == Quarter.Third)
            {
                return new DateTime(year, 7, 1, 0, 0, 0, 0);
            }
            return new DateTime(year, 10, 1, 0, 0, 0, 0);
        }

        /// <summary>
        /// 获取指定月份指定周的开始时间
        /// </summary>
        /// <param name="weekIndex">星期</param>
        /// <param name="month">月</param>
        /// <param name="year">年</param>
        /// <returns>返回指定月份指定周的开始时间</returns>
        public static DateTime GetStartOfWeekOfMonth(int weekIndex, int month, int year)
        {
            int weekCountOfMonth = GetWeekCountOfMonth(month, year);
            if ((weekIndex < 1) || (weekIndex > weekCountOfMonth))
            {
                throw new Exception("周数非法。周数不能小于1或者大于" + weekCountOfMonth);
            }
            if (weekIndex == 1)
            {
                return GetStartOfMonth(month, year);
            }
            return GetEndOfFirstWeekOfMonth(month, year).AddDays(((weekIndex - 2)*7.0) + 1.0);
        }

        /// <summary>
        /// 获取指定年度的开始时间
        /// </summary>
        /// <param name="year">年</param>
        /// <returns>返回指定年度的开始时间</returns>
        public static DateTime GetStartOfYear(int year)
        {
            return new DateTime(year, 1, 1, 0, 0, 0, 0);
        }

        /// <summary>
        /// 获取指定月份的周数
        /// </summary>
        /// <param name="month">月</param>
        /// <param name="year">年</param>
        /// <returns>返回指定月份的周数</returns>
        public static int GetWeekCountOfMonth(int month, int year)
        {
            var time2 = new DateTime(year, month, 1);
            int dayOfWeek = (int) time2.DayOfWeek;
            var day = GetStartOfMonth(month, year).AddDays(6.0 - dayOfWeek).Day;
            var num3 = GetDayCountOfMonth(month, year) - day;
            if (dayOfWeek == 0)
            {
                return (int) Math.Ceiling((decimal) (GetDayCountOfMonth(month, year)/7M));
            }
            return (((int) Math.Ceiling((decimal) (num3/7M))) + 1);
        }

        /// <summary>
        /// 获取月份时间差
        /// </summary>
        /// <param name="dateA">日期A</param>
        /// <param name="dateB">日期B</param>
        /// <returns>返回月份时间差</returns>
        public static int MonthsDifference(DateTime dateA, DateTime dateB)
        {
            if (dateA == dateB)
            {
                return 0;
            }
            if (dateA > dateB)
            {
                var num = dateA.Year - dateB.Year;
                var months = (num*12) + (dateA.Month - dateB.Month);
                if (months == 0)
                {
                    return months;
                }
                var time = dateB.AddMonths(months);
                var span = (TimeSpan) (dateA - time);
                if (span.Ticks >= 0L)
                {
                    return months;
                }
                return (months - 1);
            }
            return -MonthsDifference(dateB, dateA);
        }

        /// <summary>
        /// 获取年度时间差
        /// </summary>
        /// <param name="dateA">时间A</param>
        /// <param name="dateB">时间B</param>
        /// <returns>返回年度时间差</returns>
        public static int YearsDifference(DateTime dateA, DateTime dateB)
        {
            return (MonthsDifference(dateA, dateB)/12);
        }

        public static void TestDateHelper()
        {

            /*
             * GetStartOfYear:2017/1/1 0:00:00
             * GetStartOfMonth:2017/7/1 0:00:00
             * */

            Console.WriteLine("GetStartOfYear:" + GetStartOfYear(2017));

            Console.WriteLine("GetStartOfMonth:" + GetStartOfMonth(DateTime.Now));

            
        }

    }
}
