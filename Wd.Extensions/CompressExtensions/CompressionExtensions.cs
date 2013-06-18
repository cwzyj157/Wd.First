using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Wd.Extensions.TypeConversionExtensions;
using Wd.Extensions.StringExtensions;
using Wd.Extensions.ValueTypeExtensions;

namespace Wd.Extensions.CompressionExtensions
{
    /// <summary>
    /// 压缩扩展
    /// </summary>
    public static class CompressionExtensions
    {
        private const int BYTE_LENGTH = 1024 * 4;
        #region Compress
        /// <summary>
        /// 用指定的压缩格式压缩byte[]数据
        /// </summary>
        /// <param name="Data">待压缩的数据byte[]</param>
        /// <param name="compressionType">压缩类别</param>
        /// <returns>压缩后的数据</returns>
        public static byte[] Compress(this byte[] data, CompressionType compressionType = CompressionType.Deflate)
        {
            data.ThrowIfNull("data");
            using (MemoryStream stream = new MemoryStream())
            {
                using (Stream zipStream = GetStream(stream, CompressionMode.Compress, compressionType))
                {
                    zipStream.Write(data, 0, data.Length);
                    zipStream.Close();
                    return stream.ToArray();
                }
            }
        }
        /// <summary>
        /// 用指定的压缩格式压缩字符串数据
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="encoding">指定编码</param>
        /// <param name="compressionType">压缩格式</param>
        /// <returns>压缩后的字符串</returns>
        public static string Compress(this string data, Encoding encoding = null, CompressionType compressionType = CompressionType.Deflate)
        {
            data.ThrowIfNullOrEmpty("data");
            return data.ToByteArray(encoding).Compress(compressionType).ToBase64String();
        }
        #endregion

        #region Decompress
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="compressionType"></param>
        /// <returns></returns>
        public static byte[] Decompress(this byte[] data, CompressionType compressionType = CompressionType.Deflate)
        {
            data.ThrowIfNull("data");
            using (MemoryStream stream = new MemoryStream())
            {
                using (Stream zipStream = GetStream(stream, CompressionMode.Decompress, compressionType))
                {
                    byte[] buffer = new byte[BYTE_LENGTH];
                    int size = 0;
                    do
                    {
                        size = zipStream.Read(buffer, 0, BYTE_LENGTH);
                        if (size > 0) stream.Write(buffer, 0, size);
                    }
                    while (size > 0);
                    zipStream.Close();
                    return stream.ToArray();
                }
            }
        }
        #endregion

        private static Stream GetStream(MemoryStream Stream, CompressionMode Mode, CompressionType CompressionType)
        {
            if (CompressionType == CompressionType.Deflate)
                return new DeflateStream(Stream, Mode, true);
            else
                return new GZipStream(Stream, Mode, true);
        }
    }
}
