using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.DesignPattern.AdapterPattern
{
    public class PlayWithLeft
    {
        public void Play()
        {
            Console.WriteLine("我是左脚选手");
        }
    }

    public class PlayWitRight
    {
        public void Play()
        {
            Console.WriteLine("我是右脚选手");
        }
    }

    public interface IPlay
    {
        void PlayGame();
    }

    public class Play : IPlay
    {
        public void PlayGame()
        {
            var left = new PlayWithLeft();
            var right = new PlayWitRight();
            left.Play();
            right.Play();
        }
    }

    public class AdapterPattern01
    {
        public static void TestMethod()
        {
            var p = new Play();
            p.PlayGame();
            Console.ReadKey();
        }

    }
}


/*
 * 总结：当一个类实现某个接口方法，但仅凭自己无法独立完成该方法，
 * 于是这个类会引用另外的类或组件，把他们"适配"进来最终完成接口方法。
 */ 