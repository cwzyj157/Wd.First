using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.BridgePattern.Sample1
{
    public class Abstraction
    {
        public Implementor Implementor
        {
            get;
            set;
        }

        public virtual void Operation()
        {
            Implementor.Operation();
        }
    }
}
