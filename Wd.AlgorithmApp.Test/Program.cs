using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wd.AlgorithmApp;
using Wd.Common;

namespace Wd.AlgorithmApp.Test
{
    partial class Program
    {
        static void FAQ1Test()
        {
            // 买鸡
            FAQ1.BuyChick();


            // 算桃子
            CodeTimer.Initialize();
            CodeTimer.Time("递归", 1, () =>
            {
                FAQ1.GetPeachNumber(10);
            });

            int i = 0;

            CodeTimer.Time("尾递归", 1, () =>
            {
                i = FAQ1.GetPeachNumber(10, 1);
            });
            System.Console.WriteLine(i);


            int[] intAry = { 2, 3, 4, 98, 3, 3, 5, 9, 10, 47, 21, 15, 150, 18, 45 };
            int secondNum = FAQ1.Get2ndMax(intAry);
            System.Console.WriteLine("the second num:{0}", secondNum);

            //System.Console.WriteLine(Wd.Algorithm.class1.GetPeachNumber(10));
            //System.Console.WriteLine(Wd.Algorithm.class1.GetPeachNumber(10, 1));
        }
    }

    partial class Program
    {
        static void Main(string[] args)
        {

            int xor = 0 ^ 1 ^ 0 ^ 3 ^ 1 ^ 7 ^ 2 ^ 9 ^ 3 ^ 2 ^ 4 ^ 8 ^ 5;
            Console.WriteLine(xor);

            //FAQ1Test();
            //Ouput100Test();
            FindSameNum1Test();

            Console.ReadLine();
        }

        static void Ouput100Test()
        {
            FAQ1.Ouput100();
        }
        static void FindSameNum1Test()
        {
            int[] args = FAQ1.PrepareData();
            Console.WriteLine("find:" + FAQ1.FindSameNum3(args));
            Console.WriteLine("find:" + FAQ1.FindSameNum1(args));

            Console.WriteLine("find0:" + FAQ1.FindSameNum(args));
        }

    }
}
