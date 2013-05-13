using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.AlgorithmApp
{
    /// <summary>
    /// 算法应用中常问题
    /// </summary>
    public class FAQ1
    {
        /// <summary>
        /// 百钱买百鸡的问题算是一套非常经典的不定方程的问题，题目很简单：公鸡5文钱一只，母鸡3文钱一只，小鸡3只一文钱，
        /// 用100文钱买一百只鸡,其中公鸡，母鸡，小鸡都必须要有，问公鸡，母鸡，小鸡要买多少只刚好凑足100文钱。
        /// </summary>
        public static void BuyChick()
        {
            int k = 0;
            // 公鸡数量范围
            for (int i = 1; i < 20; i++)
            {
                // 母鸡数量范围
                for (int j = 1; j < 33; j++)
                {
                    k = 3 * (100 - 5 * i - 3 * j);

                    if (k > 0 && k % 3 == 0 && i + j + k == 100)
                    {
                        Console.WriteLine("公鸡：{0}个，母鸡：{1}个，小鸡：{2}个", i.ToString(), j.ToString(), k.ToString());
                    }
                }
            }
        }
        /// <summary>
        ///   猴子第一天摘下若干个桃子，当即吃了一半，还不过瘾就多吃了一个。第二天早上又将剩下的桃子吃了一半，还是不过瘾又多
        ///   吃了一个。以后每天都吃前一天剩下的一半再加一个。到第10天刚好剩一个。问猴子第一天摘了多少个桃子？
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static int GetPeachNumber(int day)
        {
            if (day == 1) return 1;
            return 2 * GetPeachNumber(day - 1) + 2;
        }
        /// <summary>
        /// 尾递归，将上一次的结果带入下一次计算
        /// </summary>
        /// <param name="day"></param>
        /// <param name="lastResult"></param>
        /// <returns></returns>
        public static int GetPeachNumber(int day, int lastResult)
        {
            if (day == 1) return lastResult;
            return GetPeachNumber(day - 1, 2 * lastResult + 2);
        }

        /// <summary>
        /// 取得数组中第二大数
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>第二大数</returns>
        public static int Get2ndMax(IEnumerable<int> input)
        {
            int maxNum = 0, secondNum = 0;

            foreach (int i in input)
            {
                if (maxNum < i) { secondNum = maxNum; maxNum = i; }
                else if (secondNum < i) secondNum = i;
            }

            return secondNum;
        }
        /// <summary>
        /// 1-100个数，按照特定步伐打印出来
        /// </summary>
        public static void Ouput100()
        {
            // 基础数据
            int[] Nums = new int[100];
            for (var i = 0; i < 100; i++) Nums[i] = i + 1;

            for (var i = 0; i < 10; i++)
            {
                Console.Write("{0}\t", Nums[i]);
            }

            // 方向，向右+1，向下+10，向左-1，向上-10
            int[] direction = new int[] { 1, 10, -1, -10 };
            // 步伐
            int step = 9;
            int currentAction = 1;
            int pos = 9;
            while (step > 0)
            {
                for (int loop = 0; loop < 2; loop++)
                {
                    for (int i = 0; i < step; i++)
                    {
                        pos += direction[currentAction];
                        Console.Write("{0}\t", Nums[pos]);
                    }
                    currentAction = (currentAction + 1) % 4;
                    Console.WriteLine();
                }
                step--;
            }
        }
        #region 1001个数中找出重复的两个数
        /// <summary>
        /// 1-1000放在含有1001个元素的数组中，只有唯一的一个元素值重复，其它均只出现 
        /// 一次。每个数组元素只能访问一次，设计一个算法，将它找出来；不用辅助存储空 
        /// 间，能否设计一个算法实现？
        /// 
        /// 以下有多种方法
        /// 方法1：直接遍历整个数组 时间复杂度为O(n)
        /// </summary>
        public static int FindSameNum1(int[] args)
        {
            if (args == null || args.Length == 0) throw new ArgumentNullException("args");
            // 思路：直接遍历数组 对于当前选中的两个进行比较，如果相同则直接返回
            // 算法最优只用遍历1次，最差得遍楞1000次，平均为500次
            int preNum = args[0];
            int current;
            for (var i = 1; i < args.Length; i++)
            {
                if (args[i] == preNum) break;
                else preNum = args[i];
            }
            return preNum;
        }
        private static int FindSameNum(int[] args, int left, int right)
        {
            int middle = (left + right) / 2;

            if (args[middle] == args[middle - 1] || args[middle] == args[middle + 1])
                return args[middle];

            if (args[middle] < middle + 1)
            {
                return FindSameNum(args, left, middle);
            }
            else
            {
                return FindSameNum(args, middle, right);
            }
        }
        private static int FindSameNum2(int[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[args[i]] == -1)
                    return args[i];
                args[args[i] - 1] = -1;
            }
            return 0;
        }
        public static int FindSameNum3(int[] args)
        {
            int xor = 0;
            for (int i = 0; i < args.Length; i++)
            {
                xor ^= args[i];
                xor ^= i;
            }
            return xor;
        }
        public static int FindSameNum(int[] args)
        {
            return FindSameNum(args, 0, args.Length - 1);
        }

        public static int[] PrepareData()
        {
            int[] args = new int[1001];
            for (var i = 0; i < 1000; i++)
            {
                args[i] = i + 1;
            }

            Random random = new Random();
            int feed = random.Next(1000);

            Console.WriteLine("feed:" + feed);

            for (var i = 1000; i > feed; i--)
            {
                args[i] = args[i - 1];
            }
            args[feed] = feed;

            return args;
        }
        #endregion
    }
}
