using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wd.BridgePattern.Sample1;
using Wd.BridgePattern.Sample2;

namespace Wd.BridgePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            // sample1
            Abstraction ab = new RefinedAbstraction();
            ab.Implementor = new ConcreteImplementorA();
            ab.Operation();

            ab.Implementor = new ConcreteImplementorB();
            ab.Operation();

            // sample2 
            CustomerBase c = new Customers("Chicago");
            c.Data = new CustomerData();
            c.Show();
            c.Next();
            c.Show();
            c.Next();
            c.Show();
            c.Add("henry velasquez");
            c.ShowAll();
            Console.ReadLine();
        }
    }
}
