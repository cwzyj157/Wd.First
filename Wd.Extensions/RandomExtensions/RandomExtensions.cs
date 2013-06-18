using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.Extensions.RandomExtensions
{
    public static class RandomExtensions
    {
        /// <summary>
        /// 随机填充指定长度的字节数组
        /// </summary>
        /// <param name="random">Random实例</param>
        /// <param name="length">字节长度</param>
        /// <returns>字节数组</returns>
        public static byte[] NextBytes(this Random random, int length)
        {
            byte[] bytes = new byte[length];
            random.NextBytes(bytes);
            return bytes;
        }
    }
}
