using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.LinkedListLibrary
{
    public class QueueInheritance : List
    {
        public QueueInheritance() : base("queue") { }
        // 入队：到最后面
        public void Enqueue(object dataValue)
        {
            InsertAtBack(dataValue);
        }

        // 出队：在最前面删除
        public object Dequeue()
        {
            return RemoveFromFront();
        }
    }

    public class QueueTest
    {
        public static void TestMethod()
        {
            var queue = new QueueInheritance();
            var aBoolean = true;
            var aChar = 'a';
            var anInt = 1;
            var aStr = "hello";

            queue.Enqueue(aBoolean);
            queue.Display();

            queue.Enqueue(aChar);
            queue.Display();

            queue.Enqueue(anInt);
            queue.Display();

            queue.Enqueue(aStr);
            queue.Display();

            try
            {
                while (true)
                {
                    var removedObject = queue.Dequeue();
                    Console.WriteLine(removedObject + "出队列~~");
                    queue.Display();
                }
            }
            catch (EmptyListException emptyListException)
            {
                Console.Error.WriteLine(emptyListException.StackTrace);
            }
            Console.ReadKey();
        }

    }
}
