using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Helper.KetamaHashHelp
{
    public class HashAlgorithmPercentTest
    {
        public static readonly Random Ran = new Random();

        /** key's count */
        private const int ExeTimes = 100000;

        private const int NodeCount = 100;

        private const int VirtualNodeCount = 500;

        public static List<string> AllKeys = null;

        public static void Test()
        {
            AllKeys = GetAllStrings();
            var mapData = GenerateRecord();

            var allNodes = GetNodes(NodeCount);
            Console.WriteLine("Normal case : nodes count : " + allNodes.Count());
            Call(allNodes, mapData);

            allNodes = GetNodes(NodeCount + 8);
            Console.WriteLine("Added case : nodes count : " + allNodes.Count());
            Call(allNodes, mapData);

            allNodes = GetNodes(NodeCount - 10);
            Console.WriteLine("Reduced case : nodes count : " + allNodes.Count());
            Call(allNodes, mapData);

            var addCount = 0;
            var reduceCount = 0;
            foreach (var key in mapData.Keys)
            {
                var list = mapData[key];
                if (list.Count == 3)
                {
                    if (list[0] == list[1])
                    {
                        addCount++;
                    }
                    if (list[0] == list[2])
                    {
                        reduceCount++;
                    }
                }
                else
                {
                    Console.WriteLine("It's wrong size of list, key is " + key + ", size is " + list.Count);
                }
            }

            Console.WriteLine(addCount + "   ---   " + reduceCount);

            // 上面三行分别是正常情况，节点增加，节点删除情况下的节点数目。下面两行表示在节点增加和删除情况下，同一个key分配在相同节点上的比例(命中率)。 
            // 多次测试后发现，命中率与结点数目和增减的节点数量有关。同样增删结点数目情况下，结点多时命中率高。同样节点数目，增删结点越少，命中率越高。这些都与实际情况相符。 
            Console.WriteLine("Same percent in added case : " + (float)addCount * 100 / ExeTimes + "%");
            Console.WriteLine("Same percent in reduced case : " + (float)reduceCount * 100 / ExeTimes + "%");
            Console.ReadLine();
        }

        private static void Call(List<string> nodes, Dictionary<string, List<string>> map)
        {
            var locator = new KetamaNodeLocator(nodes, VirtualNodeCount);

            foreach (var key in map.Keys)
            {
                var node = locator.GetPrimary(key);

                if (node != null)
                {
                    var list = map[key];
                    list.Add(node);
                }
            }
        }

        private static Dictionary<string, List<string>> GenerateRecord()
        {
            var record = new Dictionary<string, List<string>>(ExeTimes);

            foreach (var key in AllKeys)
            {
                var list = new List<string>();
                record[key] = list;
            }

            return record;
        }


        /**
         * Gets the mock node by the material parameter
         * 
         * @param nodeCount 
         * 		the count of node wanted
         * @return
         * 		the node list
         */
        private static List<string> GetNodes(int nodeCount)
        {
            var nodes = new List<string>();

            for (var k = 1; k <= nodeCount; k++)
            {
                var node = "node" + k;
                nodes.Add(node);
            }

            // 在应用时，这里会添入memcached server的IP端口地址
            // nodes.Add("10.0.4.114:11211");
            // nodes.Add("10.0.4.114:11212");
            // nodes.Add("10.0.4.114:11213");
            // nodes.Add("10.0.4.114:11214");
            // nodes.Add("10.0.4.114:11215");

            return nodes;
        }

        /**
         *	All the keys	
         */
        private static List<string> GetAllStrings()
        {
            var allStrings = new List<string>(ExeTimes);

            for (var i = 0; i < ExeTimes; i++)
            {
                allStrings.Add(GenerateRandomString(Ran.Next(50)));
            }

            return allStrings;
        }

        /**
         * To generate the random string by the random algorithm
         * <br>
         * The char between 32 and 127 is normal char
         * 
         * @param length
         * @return
         */
        private static string GenerateRandomString(int length)
        {
            var sb = new StringBuilder(length);

            for (var i = 0; i < length; i++)
            {
                sb.Append((char)(Ran.Next(95) + 32));
            }

            return sb.ToString();
        }
    }
}
