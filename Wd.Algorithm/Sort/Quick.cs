using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.Algorithm
{

    /// <summary>
    /// 快速排序算法
    /// 快排由东尼.霍尔所发展的一种排序算法，在平均情况下，排序n个项目要O（NLogN）次比较，在最坏情况下则需要O(N2)次比较，但是这种情况并不常见
    /// 步骤：
    ///     1.从数列中挑出一个无素，称为“基准”（Pivot）
    ///     2.重新排序数列，所有元素比基准值小的摆放在基准前面，所有元素比基准值大的摆在基准的后面（相同的数可以放在任何一边）。在这个分区退出之后
    ///     该基准就处于数列的中间位置，这个称为分区（Partition）操作
    ///     3.递归地（Recursive）把小于基准值元素的子数列和大于基准值元素的子数列排序
    /// 参考：http://www.cnblogs.com/wangfupeng1988/archive/2011/12/26/2302216.html
    /// </summary>
    public class Quick
    {
        /// <summary>
        /// 设要排序的数组是A[0]……A[N-1]，首先任意选取一个数据（通常选用第一个数据）作为关键数据，然后将所有比它小的数都放到它前面，所有比它大的数都放到它后面，这个过程称为一趟快速排序。值得注意的是，快速排序不是一种稳定的排序算法，也就是说，多个相同的值的相对位置也许会在算法结束时产生变动。
        /// 一趟快速排序的算法是：
        /// 1）设置两个变量i、j，排序开始的时候：i=0，j=N-1；
        /// 2）以第一个数组元素作为关键数据，赋值给key，即 key=A[0]；
        /// 3）从j开始向前搜索，即由后开始向前搜索（j -- ），找到第一个小于key的值A[j]，A[i]与A[j]交换；
        /// 4）从i开始向后搜索，即由前开始向后搜索（i ++ ），找到第一个大于key的A[i]，A[i]与A[j]交换；
        /// 5）重复第3、4、5步，直到 I=J； (3,4步是在程序中没找到时候j=j-1，i=i+1，直至找到为止。找到并交换的时候i， j指针位置不变。另外当i=j这过程一定正好是i+或j-完成的最后令循环结束。）
        /// 参考：http://zh.wikipedia.org/zh/%E5%BF%AB%E9%80%9F%E6%8E%92%E5%BA%8F
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static void Sort(List<int> args, int left, int right)
        {
            if (left < right)
            {
                int i = Division(args, left, right);

                Sort(args, left, i - 1);
                Sort(args, i + 1, right);
            }
        }
        private static int Division(List<int> args, int left, int right)
        {
            // 挑选基准
            int pivot = args[left];
            // 进行一趟快排
            while (left < right)
            {
                // 从右边找到第一个小于基准值的值
                while (left < right && args[right] >= pivot)
                    right = right - 1;

                args[left] = args[right];

                // 从左边找到第一个大于基准值的值
                while (left < right && args[left] <= pivot)
                    left = left + 1;

                args[right] = args[left];
            }
            args[left] = pivot;
            return left;
        }
        private static void Swap(List<int> args, int left, int right)
        {
            int temp = args[left];
            args[left] = args[right];
            args[right] = temp;
        }
        public static void Sort(List<int> args)
        {
            Sort(args, 0, args.Count - 1);
        }
    }
}
