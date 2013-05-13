using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.AdapterPattern.sample1
{
    class Adapter:Target
    {
        private Adaptee adaptee = new Adaptee();
        public override void Request()
        {
            adaptee.SpecificRequest();
        }
    }
}
