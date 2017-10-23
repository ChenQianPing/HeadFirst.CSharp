using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.CSharpDay
{

    public class People
    {
        private int _age;
        public string Name = "杨不悔";        // 姓名
        public string Gander = "女";          // 性别

        #region 构造方法
        public People(string name, string gander)
        {
            Name = name;
            Gander = gander;
        }
        public People() { }
        #endregion

        // 声明委托
        public delegate void LifedEventHandler(object sender, LifedEventArgs e);

        // 声明事件
        public event LifedEventHandler Liefd; 

        /// <summary>
        /// 定义LifedEventArgs类， 传递给Observer所感兴趣的信息
        /// </summary>
        public class LifedEventArgs : EventArgs
        {
            public readonly int Temperature;
            public LifedEventArgs(int age)
            {
                this.Temperature = age;
            }
        }

        /// <summary>
        /// 可以供继承自 People 的类重写
        /// 以便继承类拒绝其他对象对它的监视
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnLife(LifedEventArgs e)
        {
            if (Liefd != null)
            {
                Liefd(this, e);  // 调用所有注册对象的方法
            }
        }

        /// <summary>
        /// 生活
        /// </summary>
        public void Life()
        {
            for (var i = 0; i <= 70; i++)
            {
                _age = i;
                if (_age > 60)
                {
                    var e = new LifedEventArgs(_age);
                    OnLife(e);
                }
            }
        }
    }

    /// <summary>
    /// 警报您了
    /// </summary>
    public class Alarm01
    {
        public void MakeAlert(object sender, People.LifedEventArgs e)
        {
            var heater = (People)sender;
            // 访问 sender 中的公共字段
            Console.WriteLine($"Alarm：{heater.Gander} - {heater.Name}: ");
            Console.WriteLine($"Alarm: 您已经 {e.Temperature} 岁了：");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 显示信息
    /// </summary>
    public class Display01
    {
        public static void ShowMsg(object sender, People.LifedEventArgs e)
        {   
            // 静态方法
            var heater = (People)sender;
            Console.WriteLine($"Display：{heater.Gander} - {heater.Name}: ");
            Console.WriteLine($@"Display：您已经进行晚年了，{e.Temperature}岁要注意身体了。");
            Console.WriteLine();
        }
    }


    public class Delegate01
    {
        public static void TestMethod()
        {
            var heater = new People("杨不悔", "女");
            var alarm = new Alarm01();
            heater.Liefd += alarm.MakeAlert;         // 注册方法
            heater.Liefd += Display01.ShowMsg;       // 注册静态方法
            heater.Life();                           // 会自动调用注册过对象的方法
            Console.ReadKey();
        }
    }
}


/*
 * 一个人（people)，在他60岁之后，就宣布进入晚年了，在这个期间要多注意身体，多体检。
 */ 