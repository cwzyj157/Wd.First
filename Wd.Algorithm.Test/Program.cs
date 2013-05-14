using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wd.Extensions;
using Wd.Algorithm;

namespace Wd.Algorithm.Test
{
    /// <summary>
    /// 参考：http://www.cnblogs.com/wangfupeng1988/archive/2011/12/26/2302216.html
    /// </summary>
    partial class Program
    {
        static void Main(string[] args)
        {
            //QuickSortTest();
            //SelectionSortTest();
            //InsertionSortTest();

            // stringmatch test
            KMP_Test();
        }
        static void QuickSortTest()
        {
            int[] arys = { 6, 5, 2, 9, 7, 4, 0 };
            List<int> lst = arys.ToList();

            Quick.Sort(lst);

            Console.WriteLine(lst.Join(","));
            Console.ReadLine();
        }
        static void SelectionSortTest()
        {
            int[] arys = { 49, 38, 65, 97, 76, 13, 27 };
            List<int> lst = arys.ToList();

            Selection.Sort(lst);

            Console.WriteLine(lst.Join(","));
            Console.ReadLine();
        }
        static void InsertionSortTest()
        {
            int[] arys = { 6, 5, 3, 1, 8, 7, 2, 4 };

            Insertion.Sort(arys);

            Console.WriteLine(arys.Join(","));
            Console.ReadLine();
        }
    }
}
