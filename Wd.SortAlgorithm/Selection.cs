using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.SortAlgorithm
{
    /// <summary>
    /// 选择排序是一种简单直观的排序算法，它的工作原理如下：首先在未排序序列中找到最小（最大）无素，
    /// 存放在排序的超始位置，然后，再在剩余未排序元素中继续最小（最大）元素，然后放在已排序序列的未尾。
    /// 以此类推，直到所有元素均排序完毕
    /// </summary>
    public class Selection
    {
        public static void Sort(List<int> args)
        {
            int length = args.Count;
            int minIndex;
            for (int i = 0; i < length - 1; i++)
            {
                minIndex = GetMin(args, i);
                Swap(args, i, minIndex);
            }
        }
        private static void Swap(List<int> args, int left, int right)
        {
            int temp = args[left];
            args[left] = args[right];
            args[right] = temp;
        }
        private static int GetMin(List<int> args, int start)
        {
            int minIndex = start;
            for (int j = start + 1; j < args.Count; j++)
            {
                if (args[j] < args[minIndex]) { minIndex = j; }
            }
            return minIndex;
        }
    }
}
