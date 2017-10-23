using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.LinkedListLibrary
{
    #region ListNode
    /// <summary>
    /// 首先创建一个节点，是一个自引用类；
    /// </summary>
    public class ListNode
    {
        // 当前节点对象
        public object Data { get; private set; }
        // Next属性也称为链，指向另一个ListNode对象实例，这样就把2个ListNode对象实例链接起来了
        public ListNode Next { get; set; }

        public ListNode(object dataValue) : this(dataValue, null) { }
        
        public ListNode(object dataValue, ListNode nextNode)
        {
            Data = dataValue;
            Next = nextNode;
        }
    }
    #endregion

    #region List
    /// <summary>
    /// 再模拟一个链表
    /// </summary>
    public class List
    {
        #region Fields
        private ListNode _firstNode;
        private ListNode _lastNode;
        private readonly string _name;
        #endregion

        #region Ctor
        public List(string listName)
        {
            _name = listName;
            _firstNode = _lastNode = null;
        }
        

        public List() : this("list") { }
        #endregion

        #region IsEmpty
        /// <summary>
        /// 如果第一个节点是null，那就说明集合为空
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return _firstNode == null;
        }
        #endregion

        #region InsertAtFront
        /// <summary>
        /// 在最前面插入元素、节点
        /// </summary>
        /// <param name="insertItem"></param>
        public void InsertAtFront(object insertItem)
        {
            if (IsEmpty()) // 如果集合为空，加进来一个元素，相当于第一个节点和第二个节点相同，都是新加的元素
            {
                _firstNode = _lastNode = new ListNode(insertItem);
            }
            else // 如果集合不为空，第一个节点就是新加的元素，原先的第一个节点变为下一个节点
            {
                _firstNode = new ListNode(insertItem, _firstNode);
            }
        }
        #endregion

        #region InsertAtBack
        /// <summary>
        /// 在链表的最后一个节点后插入
        /// </summary>
        /// <param name="insertItem"></param>
        public void InsertAtBack(object insertItem)
        {
            if (IsEmpty())// 如果原先集合为空，第一个节点和最后一个节点就是新加的节点
            {
                _firstNode = _lastNode = new ListNode(insertItem);
            }
            else // 如果原先的集合不为空，最后一个节点的属性值就是新加的节点
            {
                _lastNode = _lastNode.Next = new ListNode(insertItem);
            }
        }
        #endregion

        #region RemoveFromFront
        /*
         * 移除最前面的元素、节点；
         * 即重新设置第一个节点的Next属性；
         * 本质是把原先排在第二位置的节点设置成第一个节点。
         */
        public object RemoveFromFront()
        {
            if (IsEmpty())
                throw new EmptyListException(_name);

            // 从第一个节点中取出节点对象
            var removeItem = _firstNode.Data;
            if (_firstNode == _lastNode) // 如果集合中只有一个元素
            {
                _firstNode = _lastNode = null;
            }
            else // 正常情况下，把firstNode的Next属性所指向的节点赋值给第一个节点
            {
                _firstNode = _firstNode.Next;
            }
            return removeItem;
        }
        #endregion

        #region RemoveFromBack
        /*
         * 移除最后面的元素、节点；
         * 从第一个节点开始，一直循环到倒数第二个节点，
         * current就像一个指针，每指到一个节点，
         * 就把该节点的下面一个节点设置为当前节点。
         * 最后，把倒数第二个节点设置为最后一个节点。 
         * 把Current的引用链设置为null，让其能被垃圾回收机制回收。
         */
        public object RemoveFromBack()
        {
            if (IsEmpty())
            {
                throw new EmptyListException();
            }

            // 从最后一个节点中获取节点对象
            var removeItem = _lastNode.Data;
            if (_firstNode == _lastNode)// 如果当前集合只有一个节点
            {
                _firstNode = _lastNode = null;
            }
            else
            {
                // 先把第一个节点作为当前节点
                var current = _firstNode;
                // 改变除最后一个节点之外的节点的值
                while (current.Next != _lastNode)
                {
                    current = current.Next;
                }
                // 最后current变成倒数第二个节点
                _lastNode = current;
                current.Next = null; // 最后一个节点的Next属性为null，即没有指向另一个节点
            }
            return removeItem;
        }
        #endregion

        #region Display
        /*
         * 打印显示
         * 从第一个节点开始，一直循环到最后一个节点，current就像一个指针，
         * 每打印一个节点，就把当前节点设置为下一个节点，一直循环下去。
         */
        public void Display()
        {
            if (IsEmpty())
            {
                Console.WriteLine("集合" + _name + "为空");
            }
            else
            {
                Console.WriteLine("集合的名称是：" + _name);

                // 先把第一个节点作为当前节点
                var current = _firstNode;
                while (current != null)
                {
                    // 把当前节点对象打印出来
                    Console.Write(current.Data + " ");
                    // 把下一个节点设置为当前节点
                    current = current.Next;
                }
                Console.WriteLine("\n");
            }
        }
        #endregion

    }
    #endregion

    #region EmptyListException
    public class EmptyListException : Exception
    {
        public EmptyListException() : base("当前集合为空") { }
        public EmptyListException(string name) : base("集合" + name + "为空") { }
        public EmptyListException(string exception, Exception inner) : base(exception, inner) { }
    }
    #endregion

    public class ListTest
    {
        public static void TestMethod()
        {
            var list = new List();
            var aBoolean = true;
            var aChar = 'a';
            var anInt = 12;
            var aStr = "hi";

            list.InsertAtFront(aBoolean);
            list.Display();

            list.InsertAtFront(aChar);
            list.Display();

            list.InsertAtBack(anInt);
            list.Display();

            list.InsertAtBack(aStr);
            list.Display();

            try
            {
                var removeObject = list.RemoveFromFront();
                Console.WriteLine(removeObject + "被删除了...");
                list.Display();

                removeObject = list.RemoveFromFront();
                Console.WriteLine(removeObject + "被删除了...");
                list.Display();

                removeObject = list.RemoveFromBack();
                Console.WriteLine(removeObject + "被删除了...");
                list.Display();

                removeObject = list.RemoveFromBack();
                Console.WriteLine(removeObject + "被删除了...");
                list.Display();
            }
            catch (EmptyListException emptyListException)
            {
                Console.Error.WriteLine("\n" + emptyListException);
            }
            Console.ReadKey();
        }
    }
}


/*
 * 数据结构和算法系列
 * 图解数据结构——使用C#
 * 
 * 如果想对集合(系列)有本质的了解，链表是一个必须了解的概念。
 * 
 * 链表的由来和定义
 * 在现实生活中，我们把不同的商品放在一个购物车中。
 *   
 * 而在面向对象的世界里，有时候，也需要把不同类型的数据放到一起，组成一个集合。
 * 集合中的元素并不是彼此孤立的，在C#中，如何表达集合元素间的关系呢？
 *   
 * 链表就是自引用类对象的线性集合，即序列。
 * 由于每个自引用对象是由引用链链接起来，所以叫链表。
 * 堆栈与队列是约束版的链表，而二叉查找数是非线性数据结构。
 *   
 * 链表的节点或元素虽然在逻辑上是连续的、线性的，当其内存不是连续存储的；
 * 数组元素在内存中是连续的，所以我们才可以通过索引来访问数组元素。
 * 
 * 以上，创建的是单向链表，其特点是第一个节点开始包含引用链，
 * 每个节点的引用链指向下一个节点，最后一个节点的引用链为null。
 * 单向链表只能从一个方向遍历。
 * 
 * 环形单向链表与单向链表的区别是：其最后一个节点的引用链指向第一个节点。
 * 环形单向链表也只能从一个方向遍历，只不过遍历到最后一个节点后，
 * 又回到第一个节点重新开始遍历。
 *   
 * 双向链表的第一个节点只包含指向下一个节点的引用链，最后一个节点只包含指向上一个节点的引用链，其它节点同时包含指向前一个节点和后一个节点的引用链。双向链表支持向前和向后遍历。
 * 环形双向链表与双向链表的区别是：第一个节点向后引用链指向最后一个节点，而最后一个节点的向前引用链指向第一个节点。
 
 *   
 *   
 * 了解集合本质必须要知晓的概念01-链表
 * http://www.cnblogs.com/darrenji/p/3885635.html
 
 */
