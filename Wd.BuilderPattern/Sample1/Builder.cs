﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.BuilderPattern.Sample1
{
    abstract class Builder
    {
        public abstract void BuildPartA();
        public abstract void BuildPartB();
        public abstract Product GetResult();
    }
}
