using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tdf.CSharp;
using Tdf.CSharp.Helper.SerializerHelper;
using Tdf.CSharp.Helper.UtilsHelper;

namespace CSharpDayConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            string strTest = "Hello";
            var strRes = strTest.GetNotNullStr();

            Console.WriteLine(strRes);
            Console.ReadLine();
        }
    }
}
