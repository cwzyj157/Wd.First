using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.SingletonPattern
{
    public class SingletonC
    {
        private static readonly SingletonC instance = new SingletonC();
        private SingletonC() { }
        public static SingletonC Instance { get { return instance; } private set { } }
    }
}
