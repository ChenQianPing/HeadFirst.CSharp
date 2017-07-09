using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Cognitive.IoC
{
    public class Woman : IPeople
    {
        private readonly IAnimal _animal;

        public Woman(IAnimal animal)
        {
            _animal = animal;
        }

        public void Pet()
        {
            Console.WriteLine("[女]我有一个宠物，它在：");
            _animal.Shout();
        }
    }
}
