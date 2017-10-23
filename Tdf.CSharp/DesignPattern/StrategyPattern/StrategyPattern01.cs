using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.DesignPattern.StrategyPattern
{
    public interface IBall
    {
        void Play();
    }

    public class Football : IBall
    {
        public void Play()
        {
            Console.WriteLine("我喜欢足球");
        }
    }

    public class Basketball : IBall
    {
        public void Play()
        {
            Console.WriteLine("我喜欢篮球");
        }
    }
    public class Volleyball : IBall
    {
        public void Play()
        {
            Console.WriteLine("我喜欢排球");
        }
    }

    /// <summary>
    /// 还有一个类专门用来选择哪种球类，并执行接口方法。
    /// </summary>
    public class SportsMan
    {
        private IBall _ball;
        public void SetHobby(IBall myBall)
        {
            _ball = myBall;
        }

        public void StartPlay()
        {
            _ball.Play();
        }
    }

    public class StrategyPattern01
    {
        /// <summary>
        /// 客户端需要让用户作出选择，根据不同的选择实例化具体类。
        /// </summary>
        public static void TestMethod()
        {
            IBall ball = null;
            var man = new SportsMan();
            while (true)
            {
                Console.WriteLine("选择你喜欢的球类项目(1=足球， 2=篮球，3=排球)");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ball = new Football();
                        break;
                    case "2":
                        ball = new Basketball();
                        break;
                    case "3":
                        ball = new Volleyball();
                        break;
                }
                man.SetHobby(ball);
                man.StartPlay();
            }
        }


    }
}


/*
 * 策略模式（Strategy Pattern）
 * * 模式定义
策略模式(Strategy Pattern)：定义一系列算法，将每一个算法封装起来，
并让它们可以相互替换。
策略模式让算法独立于使用它的客户而变化，也称为政策模式(Policy)。
策略模式是一种对象行为型模式。

Strategy Pattern: Define a family of algorithms, encapsulate each one, and make them interchangeable. Strategy lets the algorithm vary independently from clients that use it. 
Frequency of use: medium high
 
 */
