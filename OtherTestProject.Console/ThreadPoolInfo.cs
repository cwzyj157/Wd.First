using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace OtherTestProject.Console
{
    public class ThreadPoolInfo
    {
        public void Display()
        {
            int intAvailableThreads, intAvailableIoAsynThreds;
            int workerThreads, completionPortThreads;
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            System.Console.WriteLine("当前线程池中总可用线程数:" + workerThreads);


            int interations = 1000;
            for (int i = 0; i < interations; i++)
            {
                Action a = () =>
                {
                    DoWork();
                };
                a.BeginInvoke(new AsyncCallback(CallBack), null);
            }

            
            while (true)
            {
                //  取得线程池内的可用线程数目，我们只关心第一个参数即可
                ThreadPool.GetAvailableThreads(out intAvailableThreads,
                        out intAvailableIoAsynThreds);

                System.Console.WriteLine("当前线程池中可用线程数:" + intAvailableThreads);

                Thread.Sleep(1000);
            }
        }
        public void DoWork()
        {
            Thread.Sleep(10000);
        }
        public void CallBack(IAsyncResult ar)
        {
            System.Console.WriteLine("当前线程{0}执行完毕", Thread.CurrentThread.ManagedThreadId);
        }

        private void Sleep()
        {
            int intAvailableThreads, intAvailableIoAsynThreds;

            //  取得线程池内的可用线程数目，我们只关心第一个参数即可
            ThreadPool.GetAvailableThreads(out intAvailableThreads,
                    out intAvailableIoAsynThreds);

            //  线程信息
            string strMessage =
                String.Format("是否是线程池线程：{0},线程托管ID：{1},可用线程数：{2}",
                    Thread.CurrentThread.IsThreadPoolThread.ToString(),
                    Thread.CurrentThread.GetHashCode(),
                    intAvailableThreads);

            System.Console.WriteLine(strMessage);
        }

        private void CallAsyncSleep30Times()
        {
            //  创建包含Sleep函数的委托对象
            Action invoker = () => { Sleep(); };

            for (int i = 0; i < 30; i++)
            {
                //  以异步的形式，调用Sleep函数30次
                invoker.BeginInvoke(null, null);
            }
        }
    }
}
