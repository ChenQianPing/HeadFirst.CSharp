using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Tdf.CSharp.Helper.JsonHelper
{
    /*
     * 自定义查询对象转换动态类、object动态类转换json包、
     * json转换object动态类、DataReader转换为Json、
     * DataSet转换为Json、DataTable转成Json、
     * Datatable转换为Json 、格式化字符型日期型布尔型、过滤特殊字符等
     */
    public class JsonConverter
    {
        #region 自定义查询对象转换动态类
        /// <summary>
        /// 自定义查询对象转换动态类
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static dynamic JsonClass(object obj)
        {
            return ConvertJson(Serialize(obj, true));
        }
        #endregion

        #region object动态类转换json包
        /// <summary>
        /// object动态类转换json包
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="dateConvert">时间戳是否转换成日期类型</param>
        /// <returns></returns>
        public static string Serialize(object obj, bool dateConvert = false)
        {
            var jss = new JavaScriptSerializer();
            var str = jss.Serialize(obj);
            if (dateConvert)
            {
                str = System.Text.RegularExpressions.Regex.Replace(str, @"\\/Date\((\d+)\)\\/", match =>
                {
                    var dt = new DateTime(1970, 1, 1);
                    dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                    dt = dt.ToLocalTime();
                    return dt.ToString("yyyy-MM-dd HH:mm:ss");
                });
            }
            return str;
        }
        #endregion

        #region json转换object动态类
        /// <summary>
        /// json转换object动态类
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static dynamic ConvertJson(string json)
        {
            var jss = new JavaScriptSerializer();
            jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });
            dynamic dy = jss.Deserialize(json, typeof(object)) as dynamic;
            return dy;
        }
        #endregion

        #region DataReader转换为Json
        /// <summary>   
        /// DataReader转换为Json   
        /// </summary>   
        /// <param name="dataReader">DataReader对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(IDataReader dataReader)
        {
            /*
             * DataReader和DataSet最大的区别在于,
             * DataReader使用时始终占用SqlConnection(俗称：非断开式连接),
             * 在线操作数据库时，任何对SqlConnection的操作都会引发DataReader的异常。
             * 因为DataReader每次只在内存中加载一条数据,所以占用的内存是很小的。
             * 由于DataReader的特殊性和高性能，所以DataReader是只进的，
             * 你读了第一条后就不能再去读取第一条了。
             * 
             * DataSet则是将数据一次性加载在内存中，抛弃数据库连接(俗称：断开式连接)。
             * 读取完毕即放弃数据库连接，
             * 因为DataSet将数据全部加载在内存中，所以比较消耗内存。
             * 但是确比DataReader要灵活，可以动态的添加行,列,数据，
             * 对数据库进行回传，更新操作等。
             */

            try
            {
                var jsonString = new StringBuilder();
                jsonString.Append("[");

                while (dataReader.Read())
                {
                    jsonString.Append("{");
                    for (var i = 0; i < dataReader.FieldCount; i++)
                    {
                        var type = dataReader.GetFieldType(i);
                        var strKey = dataReader.GetName(i);
                        var strValue = dataReader[i].ToString();
                        jsonString.Append("\"" + strKey + "\":");
                        strValue = StringFormat(strValue, type);
                        if (i < dataReader.FieldCount - 1)
                        {
                            jsonString.Append(strValue + ",");
                        }
                        else
                        {
                            jsonString.Append(strValue);
                        }
                    }
                    jsonString.Append("},");
                }
                if (!dataReader.IsClosed)
                {
                    dataReader.Close();
                }
                jsonString.Remove(jsonString.Length - 1, 1);
                jsonString.Append("]");
                if (jsonString.Length == 1)
                {
                    return "[]";
                }
                return jsonString.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DataSet转换为Json
        /// <summary>   
        /// DataSet转换为Json   
        /// </summary>   
        /// <param name="dataSet">DataSet对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(DataSet dataSet)
        {
            var jsonString = "{";
            foreach (DataTable table in dataSet.Tables)
            {
                jsonString += "\"" + table.TableName + "\":" + ToJson(table) + ",";
            }
            jsonString = jsonString.TrimEnd(',');
            return jsonString + "}";
        }
        #endregion

        #region DataTable转成Json
        /// <summary>  
        /// DataTable转成Json   
        /// </summary>  
        /// <param name="jsonName"></param>  
        /// <param name="dt"></param>  
        /// <returns></returns>  
        public static string ToJson(DataTable dt, string jsonName)
        {
            var json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
                jsonName = dt.TableName;

            json.Append("{\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    json.Append("{");
                    for (var j = 0; j < dt.Columns.Count; j++)
                    {
                        var type = dt.Rows[i][j].GetType();
                        json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dt.Rows[i][j] is DBNull ? string.Empty : dt.Rows[i][j].ToString(), type));
                        if (j < dt.Columns.Count - 1)
                        {
                            json.Append(",");
                        }
                    }
                    json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        json.Append(",");
                    }
                }
            }
            json.Append("]}");
            return json.ToString();
        }
        #endregion

        #region Datatable转换为Json
        /// <summary>   
        /// Datatable转换为Json   
        /// </summary>   
        /// <param name="dt">Datatable对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(DataTable dt)
        {
            var jsonString = new StringBuilder();
            jsonString.Append("[");
            var drc = dt.Rows;
            for (var i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (var j = 0; j < dt.Columns.Count; j++)
                {
                    var strKey = dt.Columns[j].ColumnName;
                    var strValue = drc[i][j].ToString();
                    var type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = StringFormat(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            if (jsonString.Length == 1)
            {
                return "[]";
            }
            return jsonString.ToString();
        }
        #endregion

        #region 格式化字符型、日期型、布尔型
        /// <summary>  
        /// 格式化字符型、日期型、布尔型  
        /// add yuangang by 2015-05-19
        /// </summary>  
        /// <param name="str"></param>  
        /// <param name="type"></param>  
        /// <returns></returns>  
        private static string StringFormat(string str, Type type)
        {
            if (type != typeof(string) && string.IsNullOrEmpty(str))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(string))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            else if (type == typeof(byte[]))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(Guid))
            {
                str = "\"" + str + "\"";
            }
            return str;
        }
        #endregion

        #region 过滤特殊字符
        /// <summary>  
        /// 过滤特殊字符  
        /// add yuangang by 2015-05-19
        /// </summary>  
        /// <param name="s"></param>  
        /// <returns></returns>  
        public static string String2Json(string s)
        {
            if (s == null) throw new ArgumentNullException(nameof(s));
            var sb = new StringBuilder();
            for (var i = 0; i < s.Length; i++)
            {
                var c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    case '\v':
                        sb.Append("\\v"); break;
                    case '\0':
                        sb.Append("\\0"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }
        #endregion

        #region GetDataGridJsonByDataSet
        public static string GetDataGridJsonByDataSet(DataSet ds, string totalProperty, string root)
        {
            return GetDataGridJsonByDataTable(ds.Tables[0], totalProperty, root);
        }
        #endregion

        #region GetDataGridJsonByDataTable
        public static string GetDataGridJsonByDataTable(DataTable dt, string totalProperty, string root)
        {
            var jsonBuilder = new StringBuilder();
            jsonBuilder.Append("({\"" + totalProperty + "\":\"" + dt.Rows.Count + "\",");
            jsonBuilder.Append("\"");
            jsonBuilder.Append(root);
            jsonBuilder.Append("\":[");
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (var j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("})");
            return jsonBuilder.ToString();
        }
        #endregion

        #region GetTreeJsonByDataSet
        public static string GetTreeJsonByDataSet(DataSet ds)
        {
            return GetTreeJsonByDataTable(ds.Tables[0]);
        }
        #endregion

        #region GetTreeJsonByDataTable
        public static string GetTreeJsonByDataTable(DataTable dataTable)
        {
            var dt = FormatDataTableForTree(dataTable);
            var jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (var j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\'");

                    if (dt.Columns[j].ColumnName == "leaf")
                    {
                        var leafValue = dt.Rows[i][j].ToString();

                        if (!string.IsNullOrEmpty(leafValue))
                        {
                            jsonBuilder.Append(dt.Columns[j].ColumnName);
                            jsonBuilder.Append("\':\'");
                            jsonBuilder.Append(dt.Rows[i][j].ToString());
                            jsonBuilder.Append("\',");
                        }
                        else
                        {
                            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                        }
                    }
                    else if (dt.Columns[j].ColumnName == "customUrl")
                    {
                        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                        jsonBuilder.Append(dt.Columns[j].ColumnName);
                        jsonBuilder.Append(":\'");
                        jsonBuilder.Append(dt.Rows[i][j].ToString());
                        jsonBuilder.Append("\',");
                    }
                    else
                    {
                        jsonBuilder.Append(dt.Columns[j].ColumnName);
                        jsonBuilder.Append("\':\'");
                        jsonBuilder.Append(dt.Rows[i][j].ToString());
                        jsonBuilder.Append("\',");
                    }

                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }
        #endregion

        #region FormatDataTableForTree
        private static DataTable FormatDataTableForTree(DataTable dt)
        {
            var dtTree = new DataTable();
            dtTree.Columns.Add("id", typeof(string));
            dtTree.Columns.Add("text", typeof(string));
            dtTree.Columns.Add("leaf", typeof(string));
            dtTree.Columns.Add("cls", typeof(string));
            dtTree.Columns.Add("customUrl", typeof(string));
            dtTree.AcceptChanges();

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var drTree = dtTree.NewRow();
                drTree["id"] = dt.Rows[i]["id"].ToString();
                drTree["text"] = dt.Rows[i]["text"].ToString();
                if (dt.Rows[i]["leaf"].ToString() == "Y")
                {
                    drTree["leaf"] = "true";
                    drTree["cls"] = "file";
                }
                else
                {
                    drTree["cls"] = "folder";
                }
                drTree["customUrl"] = dt.Rows[i]["customUrl"].ToString();
                dtTree.Rows.Add(drTree);
            }
            return dtTree;
        }
        #endregion
    }

    /// <summary>
    /// 动态JSON解析
    /// </summary>
    public class DynamicJsonObject : System.Dynamic.DynamicObject
    {
        private IDictionary<string, object> Dictionary { get; set; }

        public DynamicJsonObject(IDictionary<string, object> dictionary)
        {
            this.Dictionary = dictionary;
        }

        public override bool TryGetMember(System.Dynamic.GetMemberBinder binder, out object result)
        {
            result = this.Dictionary[binder.Name];

            if (result is IDictionary<string, object>)
            {
                result = new DynamicJsonObject(result as IDictionary<string, object>);
            }
            else if (result is ArrayList)
            {
                result = new List<object>((result as ArrayList).ToArray());
            }

            return this.Dictionary.ContainsKey(binder.Name);
        }
    }

    /// <summary>
    /// 动态JSON转换
    /// add yuangang by 2015-05-19
    /// </summary>
    public class DynamicJsonConverter : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            if (type == typeof(object))
            {
                return new DynamicJsonObject(dictionary);
            }

            return null;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Type> SupportedTypes => new System.Collections.ObjectModel.ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(object) }));
    }

}
