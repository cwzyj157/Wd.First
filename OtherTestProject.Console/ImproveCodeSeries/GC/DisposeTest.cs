using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;

namespace OtherTestProject.Console.ImproveCodeSeries
{
    internal class ManagedClass
    {
    }
    /// <summary>
    /// 演示Dispose模式
    /// </summary>
    internal class SampleClass : IDisposable
    {
        // allocate unmanaged memeroy
        private IntPtr nativeResource = Marshal.AllocHGlobal(100);
        // allocate managed memeroy
        private ManagedClass mc = new ManagedClass();
        // disposed
        private bool disposed = false;

        /// <summary>
        /// 析构函数
        /// </summary>
        ~SampleClass()
        {
            // 必须，防止程序员忘记显示调用Dispose方法.并且只能为false
            // 隐式垃圾回收时，只需要处理非托管资源
            Dispose(false);
        }

        #region IDisposable Members
        /// <summary>
        /// 实现IDisposable中的接口
        /// </summary>
        public void Dispose()
        {
            // true代表显示清理资源时，需要处理托管及非托管的资源
            Dispose(true);
            // 通知垃圾回收机制不再调用终结器
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 提供Close方法更符合语言规范
        /// </summary>
        public void Close()
        {
            Dispose();
        }
        #endregion

        /// <summary>
        /// 提供Dispose具体实现，可重写该方法
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                // clear managed memeroy
                if (mc != null)
                {
                    //mc.Dispose()
                    mc = null;
                }
            }
            if (nativeResource != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(nativeResource);
                nativeResource = IntPtr.Zero;
            }
            disposed = true;
        }

    }
}
