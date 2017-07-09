using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tdf.CSharp.Cognitive.EventBus.Chapter1
{
    /// <summary>
    /// 场景类
    /// </summary>
    public static class FishingScene
    {
        public static void StartFishing()
        {
            // 1、初始化鱼竿
            var fishingRod = new FishingRod();

            // 2、声明垂钓者
            var bobby = new FishingMan("Bobby");

            // 3.分配鱼竿
            bobby.FishingRod = fishingRod;

            // 4、注册观察者
            fishingRod.FishingEvent += bobby.Update;

            // 5、循环钓鱼
            while (bobby.FishCount < 5)
            {
                bobby.Fishing();
                Console.WriteLine("-------------------");
                // 睡眠1s
                Thread.Sleep(1000);
            }
        }

    }
}
