using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Wd.Common
{
    public class FileHelper
    {
        /// <summary>
        /// 对文件执行操作（常用于向文件写日志，修改XML等文件操作.）
        /// 避免文件因为只读导致无法操作的问题
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="action">执行Action</param>
        /// <remarks>author:cw 2012-06-07</remarks>
        public static void ExecuteAction(string filePath, Action action)
        {
            if (!File.Exists(filePath)) action();
            else
            {
                //获取文件属性  
                FileAttributes attributes = File.GetAttributes(filePath);
                if ((attributes & FileAttributes.ReadOnly) != 0)
                {
                    File.SetAttributes(filePath, attributes ^ FileAttributes.ReadOnly);
                }
                action();
                File.SetAttributes(filePath, attributes);
            }
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path">文件全路径或文件夹路径</param>
        /// <remarks>author:cw 2012-04-23</remarks>
        public static void CreateDir(string path)
        {
            if (Path.HasExtension(path))
                path = Path.GetDirectoryName(path);

            DirectoryInfo di = new DirectoryInfo(path);
            if (!di.Exists)
                di.Create();
        }
    }
}
