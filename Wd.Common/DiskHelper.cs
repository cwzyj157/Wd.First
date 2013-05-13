using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Wd.Common
{
    public class DiskHelper
    {
        [DllImport("kernel32.dll")]
        private static extern bool GetDiskFreeSpaceEx(
            string lpDirectoryName, out ulong lpFreeBytesAvailable, out ulong lpTotalNumberOfBytes, out ulong lpTotalNumberOfFreeBytes);

        /// <summary>
        /// 取得磁盘剩余空间
        /// </summary>
        /// <param name="driveDirectoryName">驱动器名</param>
        /// <returns>剩余空间</returns>
        public static ulong GetFreeSpace(string driveDirectoryName)
        {
            ulong freeBytesAvailable, totalNumberOfBytes, totalNumberOfFreeBytes;
            GetDiskFreeSpaceEx(GetDriveName(driveDirectoryName), out freeBytesAvailable, out totalNumberOfBytes, out totalNumberOfFreeBytes);
            return freeBytesAvailable;
        }
        private static string GetDriveName(string path)
        {
            return path.Substring(0, path.IndexOf(":\\") + 2);
        }
    }
}
