using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OtherTestProject.Console.ImproveCodeSeries
{
    internal class FileDisposeTest : IDisposable
    {
        private FileStream fs;
        private List<string> lstFiles;
        private string fileName;

        public FileDisposeTest(string fileName)
        {
            fs = new FileStream(fileName, FileMode.Create);
            lstFiles = new List<string>();
            this.fileName = fileName;
        }
        // 自身方法
        public long GetFileLength()
        {
            return fs.Length;
        }

        // 假设当类包含非托管资源或者本地文件时,当使用该类时可能发生异常导致资源泄露，除非关闭进程
        // 所以对于这样的类就得提供IDisposable实现

        #region IDisposable Members

        private bool disposed = false;

        // Dispose方法最好与Close方法配套出现
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Close()
        {
            Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                // 释放托管资源（托管资源中可能包含非托管资源） e.g OtherClassObj.Close();

                lstFiles = null;
                fs.Close();
            }

            // 释放非托管资源
            disposed = true;
        }

        // 既然添加了Dispose方法，就得添加终结操作，因为终结操作确保对象在任何情况下都会对资源进行释放
        // 但是对于终结操作而言，它的调用时间，调用顺序是无法保证的，并且它不是公共方法，类的用户无法显式调用它
        // 所以使用Dispose模式强制对象资源清理
        ~FileDisposeTest()
        {
            Dispose(false);
            // 注意类型的终结器无论在什么情况下都会执行，所以不应假设对象处于良好、一致的状态
            // e.g 如下语句 需要进行判断fileName是否为空，因为对于实例构造器可能抛出异常，所以不确定fileName一定不为空
            // if (fileName != null) File.Delete(fileName);
        }
        #endregion
    }
}
