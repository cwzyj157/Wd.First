using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.BridgePattern.Sample2
{
    public class Customers : CustomerBase
    {
        public Customers(string group)
            : base(group)
        {
        }
        public override void ShowAll()
        {
            Console.WriteLine();
            Console.WriteLine("-------------");
            base.ShowAll();
            Console.WriteLine("-------------");
        }
    }
}
