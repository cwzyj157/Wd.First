using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Wd.Extensions
{
    public static class ByteExtenstion
    {
        /// <summary>
        /// 将字节转换为十六进制格式
        /// </summary>
        /// <param name="b">字节</param>
        /// <returns>字节十六进制表示</returns>
        public static string ToHex(this byte b)
        {
            return b.ToString("X2");
        }
        /// <summary>
        /// 将字节数组转换为十六进制格式
        /// </summary>
        /// <param name="bytes">字组数组</param>
        /// <returns>十六进制表示</returns>
        public static string ToHex(this IEnumerable<byte> bytes)
        {
            return bytes.ToHex("");
        }
        /// <summary>
        /// 将字节数组转换为十六进制格式
        /// </summary>
        /// <param name="bytes">字组数组</param>
        /// <returns>十六进制表示</returns>
        public static string ToHex(this IEnumerable<byte> bytes, string splitChar)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToHex())
                    .Append(splitChar);
            }
            if (sb.Length > 0 && !String.IsNullOrEmpty(splitChar))
                sb = sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
        /// <summary>
        /// 字节数组转换为哈希字节数组
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="hashName">哈希名称</param>
        /// <returns>哈希字节数组</returns>
        public static byte[] Hash(this byte[] bytes, string hashName)
        {
            HashAlgorithm algorithm;
            if (!String.IsNullOrEmpty(hashName))
                algorithm = HashAlgorithm.Create(hashName);
            else
                algorithm = HashAlgorithm.Create();
            return algorithm.ComputeHash(bytes);
        }/// <summary>
        /// 字节数组转换为哈希字节数组
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>哈希字节数组</returns>
        public static byte[] Hash(this byte[] bytes)
        {
            return bytes.Hash("");
        }
    }
}
