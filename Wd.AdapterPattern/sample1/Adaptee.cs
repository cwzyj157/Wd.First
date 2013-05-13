using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.AdapterPattern.sample1
{
    class Adaptee
    {
        public void SpecificRequest()
        {
            Console.WriteLine("Called SpecificRequest()");
        }
    }
}
