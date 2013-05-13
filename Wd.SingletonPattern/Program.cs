using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.SingletonPattern
{
    /// <summary>
    /// 单例模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            SingletonA s1 = SingletonA.Instance;
            SingletonA s2 = SingletonA.Instance;
            if (s1 == s2)
                System.Console.WriteLine(" the same object");


            SingletonC s3 = SingletonC.Instance;

            System.Console.ReadLine();
        }
    }
}
