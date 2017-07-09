using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Cognitive.IoC
{
    public class Man : IPeople
    {
        private readonly IAnimal _animal;

        public Man(IAnimal animal)
        {
            _animal = animal;
        }

        public void Pet()
        {
            Console.WriteLine("[男]我有一个宠物，它在：");
            _animal.Shout();
        }
    }
}
