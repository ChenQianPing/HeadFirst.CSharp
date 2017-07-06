using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tdf.CSharp.Helper.SerializerHelper
{

    /*
     * 强类型对象情况：
     * JavaScriptSerializer序列化方式序列化50000个对象耗时：9921毫秒
     * JavaScriptSerializer序列化方式反序列化50000个对象耗时：2861毫秒
     * DataContract序列化方式序列化50000个对象耗时：2095毫秒
     * DataContract序列化方式反序列化50000个对象耗时：6218毫秒
     * Newtonsoft.Json方式序列化50000个对象耗时：1962毫秒
     * Newtonsoft.Json方式反序列化50000个对象耗时：2254毫秒
     * 
     * 弱类型DataTable情况：
     * JavaScriptSerializer序列化方式序列化0个对象耗时：1005毫秒
     * JavaScriptSerializer序列化方式反序列化0个对象耗时：9毫秒
     * DataContract序列化方式序列化0个对象耗时：333毫秒
     * DataContract序列化方式反序列化0个对象耗时：208毫秒
     * Newtonsoft.Json方式序列化0个对象耗时：391毫秒
     * Newtonsoft.Json方式反序列化0个对象耗时：146毫秒

     */
    public class TestSerializerHelper
    {

        public static List<Person> GetPersons()
        {
            var lstRes = new List<Person>();
            for (var i = 0; i < 50000; i++)
            {
                var oPerson = new Person
                {
                    Name = "Bobby" + i,
                    Age = 20,
                    IsChild = i%5 == 0 ? true : false,
                    Test1 = "Test1",
                    Test2 = i.ToString(),
                    Test3 = i.ToString(),
                    Test4 = i.ToString(),
                    Test5 = i.ToString(),
                    Test6 = i.ToString(),
                    Test7 = i.ToString(),
                    Test8 = i.ToString(),
                    Test9 = i.ToString(),
                    Test10 = i.ToString()
                };

                lstRes.Add(oPerson);
            }

            return lstRes;
        }

        public static DataTable GetDataTable()
        {
            var dt = new DataTable("dt");
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Sex", typeof(string));
            dt.Columns.Add("IsChild", typeof(bool));

            for (var i = 0; i < 1000; i++)
            {
                var dr = dt.NewRow();
                dr["Age"] = i + 1;
                dr["Name"] = "Name" + i;
                dr["Sex"] = i % 2 == 0 ? "男" : "女";
                dr["IsChild"] = i % 5 > 0 ? true : false;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// JavaScriptSerializer序列化方式，强类型对象
        /// </summary>
        public static void TestJavaScriptByStrong()
        {
            var lstRes = GetPersons();
            var lstScriptSerializeObj = new List<string>();

            var spScript = new Stopwatch();
            spScript.Start();

            foreach (var oPerson in lstRes)
            {
                lstScriptSerializeObj.Add(oPerson.ToScriptJsonString<Person>());
            }

            spScript.Stop();

            Console.WriteLine("JavaScriptSerializer序列化方式序列化" + lstScriptSerializeObj.Count + "个对象耗时：" + spScript.ElapsedMilliseconds + "毫秒");
            lstRes.Clear();

            var spScript1 = new Stopwatch();
            spScript1.Start();

            foreach (var oFrameSerializeObj in lstScriptSerializeObj)
            {
                lstRes.Add(oFrameSerializeObj.ToScriptJsonObject<Person>());
            }

            spScript1.Stop();
            Console.WriteLine("JavaScriptSerializer序列化方式反序列化" + lstScriptSerializeObj.Count + "个对象耗时：" + spScript1.ElapsedMilliseconds + "毫秒");
        }

        public static void TestDataContractByStrong()
        {
            var lstRes = GetPersons();
            var lstFrameSerializeObj = new List<string>();
            var sp = new Stopwatch();
            sp.Start();

            foreach (var oPerson in lstRes)
            {
                lstFrameSerializeObj.Add(oPerson.ToJsonString<Person>());
            }
            sp.Stop();
            Console.WriteLine("DataContract序列化方式序列化" + lstFrameSerializeObj.Count + "个对象耗时：" + sp.ElapsedMilliseconds + "毫秒");

            lstRes.Clear();

            var sp1 = new Stopwatch();
            sp1.Start();
            foreach (var oFrameSerializeObj in lstFrameSerializeObj)
            {
                lstRes.Add(oFrameSerializeObj.ToJsonObject<Person>());
            }
            sp1.Stop();
            Console.WriteLine("DataContract序列化方式反序列化" + lstFrameSerializeObj.Count + "个对象耗时：" + sp1.ElapsedMilliseconds + "毫秒");
        }

        public static void TestNewtonsoftByStrong()
        {
            var lstRes = GetPersons();
            var lstNewtonsoftSerialize = new List<string>();
            var sp2 = new Stopwatch();
            sp2.Start();
            foreach (var oPerson in lstRes)
            {
                lstNewtonsoftSerialize.Add(JsonConvert.SerializeObject(oPerson));
            }
            sp2.Stop();
            Console.WriteLine("Newtonsoft.Json方式序列化" + lstNewtonsoftSerialize.Count + "个对象耗时：" + sp2.ElapsedMilliseconds + "毫秒");

            lstRes.Clear();
            var sp3 = new Stopwatch();
            sp3.Start();
            foreach (var oNewtonsoft in lstNewtonsoftSerialize)
            {
                lstRes.Add(JsonConvert.DeserializeObject<Person>(oNewtonsoft));
            }
            sp3.Stop();
            Console.WriteLine("Newtonsoft.Json方式反序列化" + lstNewtonsoftSerialize.Count + "个对象耗时：" + sp3.ElapsedMilliseconds + "毫秒");
        }

        public static void TestJavaScriptByWeak()
        {
            var dt = GetDataTable();
            var lstScriptSerializeObj = new List<string>();

            var spScript = new Stopwatch();
            spScript.Start();
            var strRes = dt.ToScriptJsonString<DataTable>();
            spScript.Stop();
            Console.WriteLine("JavaScriptSerializer序列化方式序列化" + lstScriptSerializeObj.Count + "个对象耗时：" + spScript.ElapsedMilliseconds + "毫秒");

            dt.Clear();

            var spScript1 = new Stopwatch();
            spScript1.Start();
            dt = strRes.ToScriptJsonObject<DataTable>();
            spScript1.Stop();
            Console.WriteLine("JavaScriptSerializer序列化方式反序列化" + lstScriptSerializeObj.Count + "个对象耗时：" + spScript1.ElapsedMilliseconds + "毫秒");
        }

        public static void TestDataContractByWeak()
        {
            var dt = GetDataTable();
            var lstFrameSerializeObj = new List<string>();

            var sp = new Stopwatch();
            sp.Start();
            var strRes = dt.ToJsonString<DataTable>();
            sp.Stop();
            Console.WriteLine("DataContract序列化方式序列化" + lstFrameSerializeObj.Count + "个对象耗时：" + sp.ElapsedMilliseconds + "毫秒");

            dt.Clear();
            var sp1 = new Stopwatch();
            sp1.Start();
            dt = strRes.ToJsonObject<DataTable>();
            sp1.Stop();
            Console.WriteLine("DataContract序列化方式反序列化" + lstFrameSerializeObj.Count + "个对象耗时：" + sp1.ElapsedMilliseconds + "毫秒");
        }

        public static void TestNewtonsoftByWeak()
        {
            var dt = GetDataTable();
            var lstNewtonsoftSerialize = new List<string>();
            var sp2 = new Stopwatch();
            sp2.Start();
            var strRes = JsonConvert.SerializeObject(dt);
            sp2.Stop();
            Console.WriteLine("Newtonsoft.Json方式序列化" + lstNewtonsoftSerialize.Count + "个对象耗时：" + sp2.ElapsedMilliseconds + "毫秒");

            dt.Clear();
            var sp3 = new Stopwatch();
            sp3.Start();
            dt = JsonConvert.DeserializeObject<DataTable>(strRes);
            sp3.Stop();
            Console.WriteLine("Newtonsoft.Json方式反序列化" + lstNewtonsoftSerialize.Count + "个对象耗时：" + sp3.ElapsedMilliseconds + "毫秒");
        }

    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsChild { get; set; }
        public string Test1 { get; set; }
        public string Test2 { get; set; }
        public string Test3 { get; set; }
        public string Test4 { get; set; }
        public string Test5 { get; set; }
        public string Test6 { get; set; }
        public string Test7 { get; set; }
        public string Test8 { get; set; }
        public string Test9 { get; set; }
        public string Test10 { get; set; }
    }
}
