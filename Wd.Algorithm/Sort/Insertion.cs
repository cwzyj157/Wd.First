using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.Algorithm
{
    /// <summary>
    /// 插入排序：工作原理是通过构建有序序列，对于未排序数据，在已排序序列中
    /// 从后向前扫描，找到相应位置并插入。
    /// </summary>
    public class Insertion
    {
        /// <summary>
        /// 插入排序都采用in-place在数组上实现。具体算法描述如下：
        /// 1：从第一个元素开始，该元素可以认为已经被排序
        /// 2：取出下一个元素，在已经排序的元素序列中从后向前扫描
        /// 3：如果该元素（已排序）大于新元素，将新元素移到下一个位置
        /// 4：重复步骤3，直到找到已排序的元素小于或者等于新元素的位置
        /// 5：将新元素插入到该位置后
        /// 6：重复步骤2-5
        /// </summary>
        /// <param name="args"></param>
        public static void Sort(int[] args)
        {
            int length = args.Length;
            for (int i = 1; i < length; i++)
            {
                int key = args[i];
                int position = i;
                while (position > 0 && args[position - 1] > key)
                {
                    args[position] = args[position - 1];
                    position--;
                }
                args[position] = key;
            }
        }
    }
}
