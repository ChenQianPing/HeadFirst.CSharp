using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.LinkedListLibrary
{
    public class StackInheritance : List
    {
        public StackInheritance() : base("stack") { }
        public void Push(object dataValue)
        {
            InsertAtFront(dataValue);
        }

        public object Pop()
        {
            return RemoveFromFront();
        }
    }

    public class StackTest
    {
        public static void TestMethod()
        {
            var stack = new StackInheritance();
            var aBoolean = true;
            var aChar = 'a';
            var anInt = 12;
            var aStr = "hello";

            stack.Push(aBoolean);
            stack.Display();

            stack.Push(aChar);
            stack.Display();

            stack.Push(anInt);
            stack.Display();

            stack.Push(aStr);
            stack.Display();

            try
            {
                while (true)
                {
                    var removedObject = stack.Pop();
                    Console.WriteLine(removedObject + "被弹出~~");
                    stack.Display();
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
