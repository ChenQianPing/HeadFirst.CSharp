using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.LinkedListLibrary
{
    /// <summary>
    /// 节点的类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeNode<T>
    {
        public T Element { get; set; }
        public TreeNode<T> LeftNode { get; set; }
        public TreeNode<T> RightNode { get; set; }

        public TreeNode(T element)
        {
            this.Element = element;
            LeftNode = RightNode = null;
        }

        public override string ToString()
        {
            var nodeString = "[" + this.Element + " ";
            if (this.LeftNode == null && this.RightNode == null)
            {
                nodeString += " (叶节点) ";
            }
            if (this.LeftNode != null)
            {
                nodeString += "左节点：" + this.LeftNode.ToString();
            }
            if (this.RightNode != null)
            {
                nodeString += "右节点：" + this.RightNode.ToString();
            }
            nodeString += "]";
            return nodeString;
        }
    }

    /// <summary>
    /// 创建一个泛型二叉树查找类，维护着一个根节点，并提供各种对节点的操作方法。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinarySearchTree<T>
    {
        public TreeNode<T> Root { get; set; }

        public BinarySearchTree()
        {
            this.Root = null;
        }

        // 把某个数据项插入到二叉树
        public void Insert(T x)
        {
            this.Root = Insert(x, this.Root);
        }

        // 把某个数据项从二叉树中删除
        public void Remove(T x)
        {
            this.Root = Remove(x, this.Root);
        }

        // 删除二叉树中的最小数据项
        public void RemoveMin()
        {
            this.Root = RemoveMin(this.Root);
        }

        // 获取二叉树中的最小数据项
        public T FindMin()
        {
            return ElemntAt(FindMin(this.Root));
        }

        // 获取二叉树中的最大数据项
        public T FindMax()
        {
            return ElemntAt(FindMax(this.Root));
        }

        // 获取二叉树中的某个数据项
        public T Find(T x)
        {
            return ElemntAt(Find(x, this.Root));
        }

        // 清空
        public void MakeEmpty()
        {
            this.Root = null;
        }

        // 判断二叉树是否为空，是否存在
        public bool IsEmpty()
        {
            return this.Root == null;
        }

        // 获取某个节点的数据项
        protected virtual T ElemntAt(TreeNode<T> t)
        {
            return t == null ? default(T) : t.Element;
        }

        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="x">要查找数据项</param>
        /// <param name="t">已存在的节点</param>
        /// <returns>返回节点</returns>
        protected virtual TreeNode<T> Find(T x, TreeNode<T> t)
        {
            while (t != null) // 当没有找到匹配数据项，不断调整查找范围，即t的值
            {
                var comparable = x as IComparable;
                if (comparable != null && comparable.CompareTo(t.Element) < 0)
                {
                    t = t.LeftNode;
                }
                else
                {
                    var o = x as IComparable;
                    if (o != null && o.CompareTo(t.Element) > 0)
                    {
                        t = t.RightNode;
                    }
                    else // 如果找到数据项，就返回当前t的值
                    {
                        return t;
                    }
                }
            }
            return null;
        }

        // 获取最小的节点，
        protected virtual TreeNode<T> FindMin(TreeNode<T> t)
        {
            if (t != null)
            {
                while (t.LeftNode != null) // 不断循环二叉树的左半边树
                {
                    t = t.LeftNode;        // 不断设置t的值
                }
            }
            return t;
        }

        // 获取最大的节点
        protected virtual TreeNode<T> FindMax(TreeNode<T> t)
        {
            if (t != null)
            {
                while (t.RightNode != null)
                {
                    t = t.RightNode;
                }
            }
            return t;
        }

        /// <summary>
        /// 插入节点
        /// </summary>
        /// <param name="x">要插入的数据项</param>
        /// <param name="t">已经存在的节点</param>
        /// <returns>返回已存在的节点</returns>
        protected TreeNode<T> Insert(T x, TreeNode<T> t)
        {
            if (t == null)
            {
                t = new TreeNode<T>(x);
            }
            else
            {
                var comparable = x as IComparable;
                if (comparable != null && comparable.CompareTo(t.Element) < 0)
                {
                    // 等号右边的t.LeftNode是null，因此会创建一个TreeNode实例给t.LeftNode
                    t.LeftNode = Insert(x, t.LeftNode);
                }
                else
                {
                    var o = x as IComparable;
                    if (o != null && o.CompareTo(t.Element) > 0)
                    {
                        t.RightNode = Insert(x, t.RightNode);
                    }
                    else
                    {
                        throw new Exception("插入了相同元素~~");
                    }
                }
            }
            return t;
        }

        // 删除最小的节点，返回当前根节点
        protected TreeNode<T> RemoveMin(TreeNode<T> t)
        {
            if (t == null)
            {
                throw new Exception("节点不存在~~");
            }
            else if (t.LeftNode != null)
            {
                // 通过递归不断设置t.LeftNode，直到t.LeftNode=null
                t.LeftNode = RemoveMin(t.LeftNode);
                return t;
            }
            else // 当t.LeftNode=null的时候，就把t.RightNode当作最小节点返回
            {
                return t.RightNode;
            }
        }

        // 删除某数据项，返回当前根节点
        protected TreeNode<T> Remove(T x, TreeNode<T> t)
        {
            if (t == null)
            {
                throw new Exception("节点不存在~~");
            }
            else
            {
                var comparable = x as IComparable;
                if (comparable != null && comparable.CompareTo(t.Element) < 0)
                {
                    t.LeftNode = Remove(x, t.LeftNode);
                }
                else
                {
                    var o = x as IComparable;
                    if (o != null && o.CompareTo(t.Element) > 0)
                    {
                        t.RightNode = Remove(x, t.RightNode);
                    }
                    else if (t.LeftNode != null && t.RightNode != null)
                    {
                        t.Element = FindMin(t.RightNode).Element;
                        t.RightNode = RemoveMin(t.RightNode);
                    }
                    else
                    {
                        t = t.LeftNode ?? t.RightNode;
                    }
                }
            }
            return t;
        }

        public override string ToString()
        {
            return this.Root.ToString();
        }
    }

    public class TestBinarySearchTree
    {
        public static void TestMethod()
        {
            var intTree = new BinarySearchTree<int>();
            var r = new Random(DateTime.Now.Millisecond);
            var trace = "";
            // 插入5个随机数
            for (var i = 0; i < 5; i++)
            {
                var randomInt = r.Next(1, 500);
                intTree.Insert(randomInt);
                trace += randomInt + " ";
            }

            Console.WriteLine("最大的节点：" + intTree.FindMax());
            Console.WriteLine("最小的节点：" + intTree.FindMin());
            Console.WriteLine("根节点：" + intTree.Root.Element);
            Console.WriteLine("插入节点的依次顺序是：" + trace);
            Console.WriteLine("打印树为：" + intTree);
            Console.ReadKey();
        }

    }
}
