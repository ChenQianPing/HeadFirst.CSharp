using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tdf.CSharp.Helper.UtilsHelper
{
    public static class ValidationHelper
    {
        #region 检查是否为IP地址
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIp(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        #endregion

        #region Url有效性
        /// <summary>
        /// Url有效性
        /// </summary>
        /// <param name="url">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsValidUrl(string url)
        {
            return Regex.IsMatch(url,
                @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&%\$#\=~])*[^\.\,\)\(\s]$");
        }
        #endregion

        #region Domain有效性
        /// <summary>
        /// Domain有效性
        /// </summary>
        /// <param name="host">域名</param>
        /// <returns>返回值</returns>
        public static bool IsValidDomain(string host)
        {
            var r = new Regex(@"^\d+$");
            if (host.IndexOf(".", StringComparison.Ordinal) == -1)
            {
                return false;
            }
            return !r.IsMatch(host.Replace(".", string.Empty));
        }
        #endregion

        #region 验证字符串是否是GUID
        /// <summary>
        /// 验证字符串是否是GUID
        /// </summary>
        /// <param name="guid">字符串</param>
        /// <returns>返回值</returns>
        public static bool IsGuid(string guid)
        {
            return !string.IsNullOrEmpty(guid) && Regex.IsMatch(guid, "[A-F0-9]{8}(-[A-F0-9]{4}){3}-[A-F0-9]{12}|[A-F0-9]{32}", RegexOptions.IgnoreCase);
        }
        #endregion

        #region 判断对象是否为Int32类型的数字
        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object expression)
        {
            return expression != null && IsNumeric(expression.ToString());
        }
        #endregion

        #region 判断对象是否为Int32类型的数字
        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(string expression)
        {
            var str = expression;
            if (!(str?.Length > 0) || str.Length > 11 || !Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$")) return false;
            return (str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1');
        }
        #endregion

        #region 是否为Double类型
        /// <summary>
        /// 是否为Double类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsDouble(object expression)
        {
            return expression != null && Regex.IsMatch(expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$");
        }
        #endregion

        #region 是否数字字符串
        /// <summary>
        /// 是否数字字符串
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            var regNumber = new Regex("^[0-9]+$");
            var m = regNumber.Match(inputData);
            return m.Success;
        }
        #endregion

        #region 是否数字字符串，可带正负号
        /// <summary>
        /// 是否数字字符串 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumberSign(string inputData)
        {
            var regNumberSign = new Regex("^[+-]?[0-9]+$");
            var m = regNumberSign.Match(inputData);
            return m.Success;
        }
        #endregion

        #region 是否是浮点数
        /// <summary>
        /// 是否是浮点数
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimal(string inputData)
        {
            var regDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
            var m = regDecimal.Match(inputData);
            return m.Success;
        }
        #endregion

        #region 是否是浮点数，可带正负号
        /// <summary>
        /// 是否是浮点数 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimalSign(string inputData)
        {
            // 等价于^[+-]?\d+[.]?\d+$
            var regDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); 
            var m = regDecimalSign.Match(inputData);
            return m.Success;
        }
        #endregion

        #region 判断用户输入是否为日期
        /// <summary>
        /// 判断用户输入是否为日期
        /// </summary>
        /// <param name="strln"></param>
        /// <returns></returns>
        /// <remarks>
        /// 可判断格式如下（其中-可替换为/，不影响验证)
        /// YYYY | YYYY-MM | YYYY-MM-DD | YYYY-MM-DD HH:MM:SS | YYYY-MM-DD HH:MM:SS.FFF
        /// </remarks>
        public static bool IsDateTime(string strln)
        {
            if (null == strln)
            {
                return false;
            }
            var regexDate = @"[1-2]{1}[0-9]{3}((-|\/|\.){1}(([0]?[1-9]{1})|(1[0-2]{1}))((-|\/|\.){1}((([0]?[1-9]{1})|([1-2]{1}[0-9]{1})|(3[0-1]{1})))( (([0-1]{1}[0-9]{1})|2[0-3]{1}):([0-5]{1}[0-9]{1}):([0-5]{1}[0-9]{1})(\.[0-9]{3})?)?)?)?$";
            if (Regex.IsMatch(strln, regexDate))
            {
                // 以下各月份日期验证，保证验证的完整性
                var indexY = -1;
                var indexM = -1;
                var indexD = -1;
                if (-1 != (indexY = strln.IndexOf("-", StringComparison.Ordinal)))
                {
                    indexM = strln.IndexOf("-", indexY + 1, StringComparison.Ordinal);
                    indexD = strln.IndexOf(":", StringComparison.Ordinal);
                }
                else
                {
                    indexY = strln.IndexOf("/", StringComparison.Ordinal);
                    indexM = strln.IndexOf("/", indexY + 1, StringComparison.Ordinal);
                    indexD = strln.IndexOf(":", StringComparison.Ordinal);
                }
                // 不包含日期部分，直接返回true
                if (-1 == indexM)
                    return true;
                if (-1 == indexD)
                {
                    indexD = strln.Length + 3;
                }
                var iYear = Convert.ToInt32(strln.Substring(0, indexY));
                var iMonth = Convert.ToInt32(strln.Substring(indexY + 1, indexM - indexY - 1));
                var iDate = Convert.ToInt32(strln.Substring(indexM + 1, indexD - indexM - 4));
                // 判断月份日期
                if ((iMonth < 8 && 1 == iMonth % 2) || (iMonth > 8 && 0 == iMonth % 2))
                {
                    if (iDate < 32)
                        return true;
                }
                else
                {
                    if (iMonth != 2)
                    {
                        if (iDate < 31)
                            return true;
                    }
                    else
                    {
                        // 闰年
                        if ((0 == iYear % 400) || (0 == iYear % 4 && 0 < iYear % 100))
                        {
                            if (iDate < 30)
                                return true;
                        }
                        else
                        {
                            if (iDate < 29)
                                return true;
                        }
                    }
                }
            }
            return false;
        }
        #endregion

        #region 判断是否为日期型变量
        /// <summary>
        /// 判断是否为日期型变量
        /// </summary>
        /// <param name="str">日期变量</param>
        /// <returns>是否为日期</returns>
        public static bool IsDateTimeV2(string str)
        {
            try
            {
                var dateTime = Convert.ToDateTime(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 中文检测
        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsHasChzn(string inputData)
        {
            var regChzn = new Regex("[\u4e00-\u9fa5]");
            var m = regChzn.Match(inputData);
            return m.Success;
        }
        #endregion

        #region 验证是否只含有汉字
        /// <summary>
        /// 验证是否只含有汉字
        /// </summary>
        /// <param name="strln">输入的字符</param>
        /// <returns></returns>
        public static bool IsOnllyChinese(string strln)
        {
            return Regex.IsMatch(strln, @"^[\u4e00-\u9fa5]+$");
        }
        #endregion

        #region 邮件地址
        public static bool IsEmail(string inputData)
        {
            /*
            var regEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$"); // w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 
            var m = regEmail.Match(inputData);
            return m.Success;
            */
            return Regex.IsMatch(inputData,
                @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        #endregion

        #region 验证手机号
        /// <summary>
        /// 验证输入字符串为11位的手机号码
        /// </summary>
        /// <param name="strln"></param>
        /// <returns></returns>
        public static bool IsMobileV2(string strln)
        {
            return Regex.IsMatch(strln, @"^1[0123456789]\d{9}$", RegexOptions.IgnoreCase);
        }

        #endregion

        #region 验证是否为移动号码
        /// <summary>
        /// 验证是否为移动号码
        /// </summary>
        /// <param name="number">手机号</param>
        /// <returns>Boolean</returns>
        public static bool IsMobile(string number)
        {
            var regMobile = new Regex("^1(3[4-9]|5[012789]|8[7-8])\\d{8}$");
            var m = regMobile.Match(number);
            return m.Success;
        }
        #endregion

        #region 验证是否为联通号码
        /// <summary>
        /// 验证是否为联通号码
        /// </summary>
        /// <param name="number">手机号</param>
        /// <returns></returns>
        public static bool IsUnicom(string number)
        {
            var regUnicom = new Regex("^1(3[012]|5[56]|8[5-6])\\d{8}$");
            var m = regUnicom.Match(number);
            return m.Success;
        }
        #endregion

        #region 验证身份证是否有效
        /// <summary>
        /// 验证身份证是否有效
        /// </summary>
        /// <param name="strln"></param>
        /// <returns></returns>
        public static bool IsIdCard(string strln)
        {
            switch (strln.Length)
            {
                case 18:
                {
                    var check = IsIdCard18(strln);
                    return check;
                }
                case 15:
                {
                    var check = IsIdCard15(strln);
                    return check;
                }
                default:
                    return false;
            }
        }

        /// <summary>
        /// 验证输入字符串为18位的身份证号码
        /// </summary>
        /// <param name="strln">输入的字符</param>
        /// <returns></returns>
        public static bool IsIdCard18(string strln)
        {
            long n = 0;
            if (long.TryParse(strln.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(strln.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false; // 数字验证
            }

            var address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(strln.Remove(2), StringComparison.Ordinal) == -1)
            {
                return false; // 省份验证
            }

            var birth = strln.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time;
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false; // 生日验证
            }

            var arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            var wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            var ai = strln.Remove(17).ToCharArray();
            var sum = 0;
            for (var i = 0; i < 17; i++)
            {
                sum += int.Parse(wi[i]) * int.Parse(ai[i].ToString());
            }

            var y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != strln.Substring(17, 1).ToLower())
            {
                return false; // 校验码验证
            }
            return true;      // 符合GB11643-1999标准
        }

        /// <summary>
        /// 验证输入字符串为15位的身份证号码
        /// </summary>
        /// <param name="strln">输入的字符</param>
        /// <returns></returns>
        public static bool IsIdCard15(string strln)
        {
            long n = 0;
            if (long.TryParse(strln, out n) == false || n < Math.Pow(10, 14))
            {
                return false; // 数字验证
            }

            var address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(strln.Remove(2), StringComparison.Ordinal) == -1)
            {
                return false; // 省份验证
            }

            var birth = strln.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time;
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false; // 生日验证
            }
            return true; // 符合15位身份证标准
        }
        #endregion

        #region 验证输入字符串为电话号码
        /// <summary>
        /// 验证输入字符串为电话号码
        /// </summary>
        /// <param name="strln">输入字符串</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool IsPhone(string strln)
        {
            return Regex.IsMatch(strln, @"(^(\d{2,4}[-_－—]?)?\d{3,8}([-_－—]?\d{3,8})?([-_－—]?\d{1,7})?$)|(^0?1[35]\d{9}$)");
            // 弱一点的验证：  @"\d{3,4}-\d{7,8}"         
        }
        #endregion

        #region 验证是否是有效传真号码
        /// <summary>
        /// 验证是否是有效传真号码
        /// </summary>
        /// <param name="strln">输入的字符</param>
        /// <returns></returns>
        public static bool IsFax(string strln)
        {
            return Regex.IsMatch(strln, @"^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$");
        }
        #endregion

        #region 邮编有效性
        /// <summary>
        /// 邮编有效性
        /// </summary>
        /// <param name="zip">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsValidZip(string zip)
        {
            var rx = new Regex(@"^\d{6}$", RegexOptions.None);
            var m = rx.Match(zip);
            return m.Success;
        }
        #endregion

        #region 检查是否为空
        /// <summary>
        /// 检查是否为空（null 或是""）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(object obj)
        {
            if (obj == null)
            {
                return true;
            }

            var typeName = obj.GetType().Name;
            switch (typeName)
            {
                case "String[]":
                    var list = (string[])obj;
                    return list.Length == 0;
                default:
                    var str = obj.ToString();
                    return string.IsNullOrEmpty(str);
            }
        }

        /// <summary>
        /// 检查是否为空（null 或是""）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(object obj)
        {
            return (!IsNull(obj));
        }
        #endregion

        #region 判断传入的数字是否为0
        /// <summary>
        /// 判断传入的数字是否为0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNullZero(object obj)
        {
            return !IsNotNullZero(obj);
        }

        /// <summary>
        /// 判断传入的数字是否不为0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNullZero(object obj)
        {
            if (IsNull(obj))
            {
                return false;
            }
            else
            {
                try
                {
                    var d = Convert.ToDouble(obj);
                    return d != 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        #endregion

        #region 判断是否为base64字符串
        /// <summary>
        /// 判断是否为base64字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBase64String(string str)
        {
            /*
             * Base64是一种基于64个可打印字符来表示二进制数据的表示方法。
             * 通常是52个大小字母和10个数字，以及+，/两个字符，还有个=用于补缺。
             * A-Z, a-z, 0-9, +, /, =
             * 
             */
            return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
        }
        #endregion

        #region 检查是否包涵SQL关键字
        /// <summary>
        /// 检查sWord是否包涵SQL关键字
        /// </summary>
        /// <param name="sWord">被检查的字符串</param>
        /// <returns>存在SQL关键字返回true，不存在返回false</returns>
        public static bool CheckSqlKeyWord(string sWord)
        {
            var strKeyWord = @"select|insert|delete|from|count(|drop table|update|truncate|asc(|mid(|char(|xp_cmdshell|exec master|netlocalgroup administrators|:|net user|""|or|and";
            var StrRegex = @"[-|;|,|/|(|)|[|]|}|{|%|@|*|!|']";
            return Regex.IsMatch(sWord, strKeyWord, RegexOptions.IgnoreCase) || Regex.IsMatch(sWord, StrRegex);
        }

        public static bool HasKeywords(string contents)
        {
            var bReturnValue = false;
            var sRxStr = @"(\sand\s)|(\sand\s)|(\slike\s)|(select\s)|(insert\s)|(delete\s)|(update\s[\s\S].*\sset)|(create\s)|(\stable)|(<[iframe|/iframe|script|/script])|(')|(\sexec)|(declare)|(\struncate)|(\smaster)|(\sbackup)|(\smid)|(\scount)|(cast)|(%)|(\sadd\s)|(\salter\s)|(\sdrop\s)|(\sfrom\s)|(\struncate\s)|(\sxp_cmdshell\s)";
            if (contents.Length > 0)
            {
                var sLowerStr = contents.ToLower();
                var sRx = new Regex(sRxStr);
                bReturnValue = sRx.IsMatch(sLowerStr, 0);
            }

            return bReturnValue;
        }

        #endregion

        #region Test Method

        public static void TestValidationHelper()
        {
            Console.WriteLine(ValidationHelper.IsHasChzn(@"中文字符"));
            Console.WriteLine(ValidationHelper.IsHasChzn(@"Bobby"));

            Console.WriteLine(ValidationHelper.IsEmail("pingkeke@gmail.com"));
            Console.WriteLine(ValidationHelper.IsEmail("pingkeke@163.com"));

            Console.WriteLine("IsMobile：" + ValidationHelper.IsMobile("13700673590"));
            Console.WriteLine("IsMobileV2：" + ValidationHelper.IsMobileV2("13700673590"));
        }
        #endregion

    }
}
