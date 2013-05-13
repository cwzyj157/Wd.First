using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.SingletonPattern
{
    public class SingletonB
    {
        private static SingletonB instance;
        private static object objLock = new object();
        private SingletonB()
        {
        }
        public static SingletonB Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (objLock)
                    {
                        if (instance == null) instance = new SingletonB();
                    }
                }
                return instance;
            }
            private set { }
        }
    }
}
