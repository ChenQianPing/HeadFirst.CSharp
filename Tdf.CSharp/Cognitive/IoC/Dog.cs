using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Cognitive.IoC
{
    public class Dog : IAnimal
    {
        public void Shout()
        {
            Console.WriteLine("汪汪汪");
        }
    }
}
