using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Tdf.CSharp.Helper.JsonHelper
{
    /// <summary>
    /// 前台Ajax请求的统一返回结果类
    /// </summary>
    public class AjaxResult
    {
        private AjaxResult() { }


        /// <summary>
        /// 是否产生错误
        /// </summary>
        public bool IsError { get; private set; } = false;

        /// <summary>
        /// 错误信息，或者成功信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 成功可能时返回的数据
        /// </summary>
        public object Data { get; set; }

        #region Error
        public static AjaxResult Error()
        {
            return new AjaxResult()
            {
                IsError = true
            };
        }

        public static AjaxResult Error(string message)
        {
            return new AjaxResult()
            {
                IsError = true,
                Message = message
            };
        }
        #endregion

        #region Success
        public static AjaxResult Success()
        {
            return new AjaxResult()
            {
                IsError = false
            };
        }
        public static AjaxResult Success(string message)
        {
            return new AjaxResult()
            {
                IsError = false,
                Message = message
            };
        }
        public static AjaxResult Success(object data)
        {
            return new AjaxResult()
            {
                IsError = false,
                Data = data
            };
        }
        public static AjaxResult Success(object data, string message)
        {
            return new AjaxResult()
            {
                IsError = false,
                Data = data,
                Message = message
            };
        }
        #endregion

        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
    }
}
