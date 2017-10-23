using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tdf.CSharp;
using Tdf.CSharp.AlgorithmDay;
using Tdf.CSharp.Cognitive.EventBus.Chapter1;
using Tdf.CSharp.Cognitive.EventBus.Chapter2;
using Tdf.CSharp.Cognitive.IoC;
using Tdf.CSharp.CSharpDay;
using Tdf.CSharp.DesignPattern.AdapterPattern;
using Tdf.CSharp.DesignPattern.FacadePattern;
using Tdf.CSharp.DesignPattern.ObserverPattern;
using Tdf.CSharp.DesignPattern.SingletonPattern;
using Tdf.CSharp.DesignPattern.StrategyPattern;
using Tdf.CSharp.Helper.EncryptionHelper;
using Tdf.CSharp.Helper.KetamaHashHelp;
using Tdf.CSharp.Helper.SerializerHelper;
using Tdf.CSharp.Helper.UtilsHelper;
using Tdf.CSharp.LinkedListLibrary;

namespace CSharpDayConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            Delegate01.TestMethod();

            Console.ReadLine();
        }
    }
}
