﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.BridgePattern.Sample1
{
    public class RefinedAbstraction : Abstraction
    {
        public override void Operation()
        {
            Implementor.Operation();
        }
    }
}
