using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.AlgorithmDay
{
    #region 循环链表解决约瑟夫问题
    /// <summary>
    /// 循环链表节点的定义实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CirNode<T>
    {
        public T Item { get; set; }
        public CirNode<T> Next { get; set; }

        public CirNode()
        {
        }

        public CirNode(T item)
        {
            this.Item = item;
        }
    }

    /// <summary>
    /// 单向循环链表的模拟实现
    /// </summary>
    public class MyCircularLinkedList<T>
    {
        private CirNode<T> _tail;        // 字段：记录尾节点的指针
        private CirNode<T> _currentPrev; // 字段：使用前驱节点标识当前节点

        // 属性：指示链表中元素的个数
        public int Count { get; private set; }

        // 属性：指示当前节点中的元素值
        public T CurrentItem => this._currentPrev.Next.Item;

        public MyCircularLinkedList()
        {
            this.Count = 0;
            this._tail = null;
        }

        public bool IsEmpty()
        {
            return this._tail == null;
        }

        // Method01:根据索引获取节点
        private CirNode<T> GetNodeByIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException($"index", "索引超出范围");
            }

            var tempNode = this._tail.Next;
            for (var i = 0; i < index; i++)
            {
                tempNode = tempNode.Next;
            }

            return tempNode;
        }

        // Method02:在尾节点后插入新节点
        public void Add(T value)
        {
            var newNode = new CirNode<T>(value);
            if (this._tail == null)
            {
                // 如果链表当前为空则新元素既是尾头结点也是头结点
                this._tail = newNode;
                this._tail.Next = newNode;
                this._currentPrev = newNode;
            }
            else
            {
                // 插入到链表末尾处
                newNode.Next = this._tail.Next;
                this._tail.Next = newNode;
                // 改变当前节点
                if (this._currentPrev == this._tail)
                {
                    this._currentPrev = newNode;
                }
                // 重新指向新的尾节点
                this._tail = newNode;
            }

            this.Count++;
        }

        // Method03:移除当前所在节点
        public void Remove()
        {
            if (this._tail == null)
            {
                throw new NullReferenceException("链表中没有任何元素");
            }
            else if (this.Count == 1)
            {
                // 只有一个元素时将两个指针置为空
                this._tail = null;
                this._currentPrev = null;
            }
            else
            {
                if (this._currentPrev.Next == this._tail)
                {
                    // 当删除的是尾指针所指向的节点时
                    this._tail = this._currentPrev;
                }
                // 移除当前节点
                this._currentPrev.Next = this._currentPrev.Next.Next;
            }

            this.Count--;
        }

        // Method04:获取所有节点信息
        public string GetAllNodes()
        {
            if (this.Count == 0)
            {
                throw new NullReferenceException("链表中没有任何元素");
            }
            else
            {
                var tempNode = this._tail.Next;
                var result = string.Empty;
                for (var i = 0; i < this.Count; i++)
                {
                    result += tempNode.Item + " ";
                    tempNode = tempNode.Next;
                }

                return result;
            }
        }

        public void Move(int step = 1)
        {
            if (step < 1)
            {
                throw new ArgumentOutOfRangeException($"step", "移动步数不能小于1");
            }

            for (var i = 1; i < step; i++)
            {
                _currentPrev = _currentPrev.Next;
            }
        }
    }

    #endregion

    #region 使用LinkedList<T>解决约瑟夫问题
    /// <summary>
    /// 定义一个Person类
    /// </summary>
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        
    }
    #endregion

    public class Josephus
    {
        /// <summary>
        /// 循环链表解决约瑟夫问题
        /// </summary>
        public static void TestMethod1()
        {
            var linkedList = new MyCircularLinkedList<int>();

            Console.WriteLine("Step1:请输入人数N");
            var n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Step2:请输入数字M");
            var m = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Step3:报数游戏开始");
            // 添加参与人员元素
            for (var i = 1; i <= n; i++)
            {
                linkedList.Add(i);
            }
            // 打印所有参与人员
            Console.Write($"所有参与人员：{linkedList.GetAllNodes()}");
            Console.WriteLine("\r\n" + "-------------------------------------");

            var result = string.Empty;

            while (linkedList.Count > 1)
            {
                // 依次报数：移动
                linkedList.Move(m);
                // 记录出队人员
                result += linkedList.CurrentItem + " ";
                // 移除人员出队
                linkedList.Remove();
                Console.WriteLine();
                Console.Write($"剩余报数人员：{linkedList.GetAllNodes()}");
                Console.Write($"  开始报数人员：{linkedList.CurrentItem}");
            }
            Console.WriteLine("\r\n" + "Step4:报数游戏结束");
            Console.WriteLine($@"出队人员顺序：{result + linkedList.CurrentItem}");
        }

        /// <summary>
        /// 使用LinkedList<T>解决约瑟夫问题
        /// </summary>
        public static void TestMethod2()
        {
            Console.WriteLine("请输入人数N");
            var n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("请输入数字M");
            var m = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("-------------------------------------");

            var linkedList = InitPersonList(n);

            var startNode = linkedList.First;

            while (linkedList.Count >= 1)
            {
                for (var i = 1; i < m; i++)
                {
                    if (startNode != linkedList.Last)
                    {
                        startNode = startNode?.Next;
                    }
                    else
                    {
                        startNode = linkedList.First;
                    }
                }

                // 记录出队人员节点
                var removeNode = startNode;
                // 打印出队人员ID号
                Console.Write(removeNode.Value.Id + " ");
                // 确定下一个开始报数人员
                if (startNode == linkedList.Last)
                {
                    startNode = linkedList.First;
                }
                else
                {
                    startNode = startNode.Next;
                }
                // 移除出队人员节点
                linkedList.Remove(removeNode);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// 数学公式法
        /// </summary>
        public static void TestMethod3()
        {
            Console.WriteLine("请输入人数N");
            var n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("请输入数字M");
            var m = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("-------------------------------------");

            var s = 0;

            for (var i = 2; i <= n; i++)
            {
                s = (s + m)%i;
                Console.WriteLine("s:" + s);
            }

            var winner = s + 1;

            Console.WriteLine("The winner is:" + winner);
        }


        /// <summary>
        /// 初始化LinkedList集合
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private static LinkedList<Person> InitPersonList(int count)
        {
            var personList = new LinkedList<Person>();
            for (var i = 1; i <= count; i++)
            {
                var person = new Person
                {
                    Id = i,
                    Name = "Counter-" + i.ToString()
                };

                personList.AddLast(person);
            }

            return personList;
        }

    }
}

/*
 * 何为约瑟夫问题
 * 据说著名犹太历史学家 Josephus 有过以下的故事：
 * 在罗马人占领乔塔帕特后，39 个犹太人与Josephus及他的朋友躲到一个洞中，
 * 39个犹太人决定宁愿死也不要被敌人抓到，于是决定了一个自杀方式，
 * 41个人排成一个圆圈，由第1个人开始报数，每报数到第3人该人就必须自杀，
 * 然后再由下一个重新报数，直到所有人都自杀身亡为止。
 * 然而 Josephus 和他的朋友并不想遵从，Josephus 要他的朋友先假装遵从，
 * 他将朋友与自己安排在第16个与第31个位置，于是逃过了这场死亡游戏。
 * 
 * 以上就是著名的约瑟夫问题：N个人围成一圈，从第一个开始报数，第M个将被杀掉，
 * 最后剩下Q个。从围成一圈这里就启发了我们可以使用循环链表来解决该问题。

 */
