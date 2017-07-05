using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp
{
    /*
     * 事件的应用 （Observer设计模式--热水器）
     * 假设我们有个高档的热水器，我们给它通上电，当水温超过95度的时候：
     * 1、扬声器会开始发出语音，告诉你水的温度；
     * 2、液晶屏也会改变水温的显示，来提示水已经快烧开了。
     * 
     * 现在我们需要写个程序来模拟这个烧水的过程，
     * 我们将定义一个类来代表热水器，我们管它叫：Heater，
     * 它有代表水温的字段，叫做temperature；
     * 当然，还有必不可少的给水加热方法BoilWater()，
     * 一个发出语音警报的方法MakeAlert()，
     * 一个显示水温的方法，ShowMsg()。
     * 
     * 参考：
     * http://www.cnblogs.com/JimmyZhang/archive/2007/09/23/903360.html
     */

    // 热水器
    public class Heater
    {
        private int _temperature;
        public string Type = "RealFire 001";       // 添加型号作为演示
        public string Area = "China Xian";         // 添加产地作为演示

        // 声明委托
        public delegate void BoiledEventHandler(object sender, BoiledEventArgs e);
        // 声明事件
        public event BoiledEventHandler Boiled; 

        // 定义BoiledEventArgs类，传递给Observer所感兴趣的信息
        public class BoiledEventArgs : EventArgs
        {
            public readonly int Temperature;
            public BoiledEventArgs(int temperature)
            {
                this.Temperature = temperature;
            }
        }

        // 可以供继承自 Heater 的类重写，以便继承类拒绝其他对象对它的监视
        protected virtual void OnBoiled(BoiledEventArgs e)
        {
            if (Boiled != null)
            { 
                // 如果有对象注册
                Boiled(this, e);  // 调用所有注册对象的方法
            }
        }

        // 烧水。
        public void BoilWater()
        {
            for (var i = 0; i <= 100; i++)
            {
                _temperature = i;
                if (_temperature > 95)
                {
                    // 建立BoiledEventArgs 对象。
                    var e = new BoiledEventArgs(_temperature);
                    // 调用 OnBolied方法
                    OnBoiled(e);  
                }
            }
        }
    }

    // 警报器
    public class Alarm
    {
        public void MakeAlert(object sender, Heater.BoiledEventArgs e)
        {
            var heater = (Heater)sender;     // 这里是不是很熟悉呢？

            // 访问 sender 中的公共字段                                 
            Console.WriteLine($"Alarm：{heater.Area} - {heater.Type}: ");
            Console.WriteLine($"Alarm: 嘀嘀嘀，水已经 {e.Temperature} 度了：");
            Console.WriteLine();
        }
    }

    // 显示器
    public class Display
    {
        // 静态方法
        public static void ShowMsg(object sender, Heater.BoiledEventArgs e)
        {   
            
            var heater = (Heater)sender;
            Console.WriteLine($"Display：{heater.Area} - {heater.Type}: ");
            Console.WriteLine($"Display：水快烧开了，当前温度：{e.Temperature}度。");
            Console.WriteLine();
        }
    }

    public class Day3
    {
        public void TestMethod1()
        {
            var heater = new Heater();
            var alarm = new Alarm();

            // 注册方法
            heater.Boiled += alarm.MakeAlert;
            // 给匿名对象注册方法
            heater.Boiled += (new Alarm()).MakeAlert;      
            heater.Boiled += new Heater.BoiledEventHandler(alarm.MakeAlert);    //也可以这么注册
            // 注册静态方法
            heater.Boiled += Display.ShowMsg;
            // 烧水，会自动调用注册过对象的方法
            heater.BoilWater();   
        }
    }



}
