using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.AlgorithmDay
{

    /*
     * 实现洗牌游戏的一种算法是：遍历每个位置上的牌，然后与随机位置上的牌交换。
     */
    public class PokerAlgorithm
    {
        private static readonly Card[] AllCards = new Card[52];
        private static readonly string[] Ms = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        private static readonly string[] Ls = { "红桃", "方块", "梅花", "黑桃" };

        public static void TestMethod()
        {
            Init();
            ShowAllCards();
            Console.WriteLine("\r\n");

            Shuffle();
            ShowAllCards();
            Console.ReadKey();
        }

        /// <summary>
        /// 使用算法洗牌。
        /// </summary>
        private static void Shuffle()
        {
            var r = new Random();
            // 依次遍历所有牌与随机位置上的牌交换位置
            for (var i = 0; i < AllCards.Length; i++)
            {
                var ran = r.Next(52);
                var tempCard = AllCards[i];
                AllCards[i] = AllCards[ran];
                AllCards[ran] = tempCard;
            }
        }

        /// <summary>
        /// 初始化牌的数组
        /// </summary>
        private static void Init()
        {
            for (var i = 0; i < AllCards.Length; i++)
            {
                AllCards[i] = new Card(Ms[i % 13], Ls[i % 4]);
            }
        }

        /// <summary>
        /// 遍历所有牌并显示
        /// </summary>
        private static void ShowAllCards()
        {
            foreach (var item in AllCards)
            {
                Console.Write(item.ToString() + " ");
            }
        }
    }

    /*
     * 对于牌来讲，2个关键的因素是面值和类型(红桃、梅花等)。
     */ 
    public class Card
    {
        private readonly string _mianzhi;
        private readonly string _leixin;
        public Card(string m, string l)
        {
            _mianzhi = m;
            _leixin = l;
        }
        public override string ToString()
        {
            return _leixin + " " + _mianzhi;
        }
    }
}
