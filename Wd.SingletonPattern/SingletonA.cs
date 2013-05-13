using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.SingletonPattern
{
    public class SingletonA
    {
        private static SingletonA instance;
        private SingletonA()
        {
        }
        public static SingletonA Instance
        {
            get
            {
                if (instance == null) instance = new SingletonA();
                return instance;
            }
            private set { }
        }
    }
}
