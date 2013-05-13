using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.BridgePattern.Sample1
{
    public class ConcreteImplementorB : Implementor
    {
        public override void Operation()
        {
            Console.WriteLine("ConcreteImplementorB.Operation");
        }
    }
}
