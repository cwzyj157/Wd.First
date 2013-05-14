using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.Algorithm.Test
{
    partial class Program
    {
        static void KMP_Test()
        {
            KMPTestor.Test();
        }
    }
    public class KMPTestor
    {
        public static void Test()
        {
            string search = "BBC ABCDAB ABCDABCDABDE";
            string word = "ABCDAB";
            KMP kmp = new KMP(search, word);
            //int index = kmp.Match();
            IEnumerable<int> lstIndex = kmp.Matches();

            //Console.WriteLine("search:" + search + "\tword:" + word);
            //Console.WriteLine("match index:" + index);
            //Console.WriteLine("Is Right:" + (index != -1 && search.Substring(index, word.Length) == word ? "Y" : "N"));


            foreach (var tmpIndex in lstIndex)
            {
                Console.WriteLine("index:" + tmpIndex);
            }

            Console.ReadLine();
        }
    }
}
