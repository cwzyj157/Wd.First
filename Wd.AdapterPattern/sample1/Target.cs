using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.AdapterPattern.sample1
{
    class Target
    {
        public virtual void Request()
        {
            Console.WriteLine("Called Target Request()");
        }
    }
}
