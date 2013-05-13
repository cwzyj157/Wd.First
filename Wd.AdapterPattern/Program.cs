using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wd.AdapterPattern.sample1;
using Wd.AdapterPattern.sample2;

namespace Wd.AdapterPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            // sample1
            Target target = new Adapter();
            target.Request();

            // sample2
            Compound c = new Compound("Unknown");
            c.Display();

            c = new RichCompound("Water");
            c.Display();

            c = new RichCompound("Benzene");
            c.Display();

            c = new RichCompound("Ethanol");
            c.Display();

            Console.ReadLine();
        }
    }
}
