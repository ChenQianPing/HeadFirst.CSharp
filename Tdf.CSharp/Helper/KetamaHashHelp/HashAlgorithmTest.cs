using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Helper.KetamaHashHelp
{
    public class HashAlgorithmTest
    {
        private static readonly Random Ran = new Random();

        /** key's count */
        private const int ExeTimes = 100000;

        private const int NodeCount = 5;

        private const int VirtualNodeCount = 160;

        /*
         * 分布平均性测试:
         * 测试随机生成的众多key是否会平均分布到各个结点上测试结果如下: 
         * 
         * Nodes count : 5, Keys count : 100000, Normal percent : 20%
         * -------------------- boundary  ----------------------
         * Node name :node4 - Times : 20585 - Percent : 20.585%
         * Node name :node3 - Times : 19369 - Percent : 19.369%
         * Node name :node2 - Times : 19770 - Percent : 19.77%
         * Node name :node1 - Times : 20385 - Percent : 20.385%
         * Node name :node5 - Times : 19891 - Percent : 19.891%
         * 
         *  最上面一行是参数说明，节点数目，总共有多少key，
         *  每个节点应该分配key的比例是多少。
         *  下面是每个结点分配到key的数目和比例。 
         *  多次测试后发现，这个Hash算法的节点分布都在标准比例左右徘徊。

         * 
         */
        public static void Test()
        {
            var test = new HashAlgorithmTest();

            /* Records the times of locating node*/
            var nodeRecord = new Dictionary<string, int>();

            var allNodes = test.GetNodes(NodeCount);

            var locator = new KetamaNodeLocator(allNodes, VirtualNodeCount);

            var allKeys = test.AllStrings;

            foreach (var key in allKeys)
            {
                var node = locator.GetPrimary(key);
                if (!nodeRecord.ContainsKey(node))
                {
                    nodeRecord[node] = 1;
                }
                else
                {
                    nodeRecord[node] = nodeRecord[node] + 1;
                }
            }

            Console.WriteLine("Nodes count : " + NodeCount + ", Keys count : " + ExeTimes + ", Normal percent : " + (float)100 / NodeCount + "%");
            Console.WriteLine("-------------------- boundary  ----------------------");
            foreach (var key in nodeRecord.Keys)
            {
                Console.WriteLine("Node name :" + key + " - Times : " + nodeRecord[key] + " - Percent : " + (float)nodeRecord[key] / ExeTimes * 100 + "%");
            }
            Console.ReadLine();
        }


        /**
         * Gets the mock node by the material parameter
         * 
         * @param nodeCount 
         * 		the count of node wanted
         * @return
         * 		the node list
         */

        protected virtual List<string> GetNodes(int nodeCount)
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
        private List<string> AllStrings
        {
            get
            {
                var allStrings = new List<string>(ExeTimes);

                for (var i = 0; i < ExeTimes; i++)
                {
                    allStrings.Add(GenerateRandomString(Ran.Next(50)));
                }

                return allStrings;
            }
        }

        /*
         * To generate the random string by the random algorithm
         * <br>
         * The char between 32 and 127 is normal char
         * 
         * @param length
         * @return
         */
        protected virtual string GenerateRandomString(int length)
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
