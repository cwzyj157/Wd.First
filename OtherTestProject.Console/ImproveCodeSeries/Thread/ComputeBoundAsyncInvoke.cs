using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace OtherTestProject.Console
{
    partial class Program
    {
        static void ComputeBoundAsyncInvokeTest()
        {
            //OtherTestProject.Console.ImproveCodeSeries.Thread.ComputeBoundAsyncInvoke.WorkerItemTest();

            //OtherTestProject.Console.ImproveCodeSeries.Thread.ComputeBoundAsyncInvoke.ExecutionContextTest();

            OtherTestProject.Console.ImproveCodeSeries.Thread.ComputeBoundAsyncInvoke.CancellationTokenTest();
        }
    }
}

namespace OtherTestProject.Console.ImproveCodeSeries.Thread
{
    /// <summary>
    /// 计算限制异步调用测试
    /// </summary>
    internal sealed class ComputeBoundAsyncInvoke
    {
        // 1.使用
        public static void WorkerItemTest()
        {
            System.Console.WriteLine("Main Thread: quening an aysnchronous operation.");

            // 创建一个工作者线程执行计算限制的异步操作
            ThreadPool.QueueUserWorkItem(new WaitCallback(WorkerItemCallback), "running");

            System.Console.WriteLine("Main Thread: doing other work here.");
            System.Threading.Thread.Sleep(10000);

            System.Console.WriteLine("Hit <ENTER> to end this program.");
            System.Console.ReadLine();
        }
        static void WorkerItemCallback(object state)
        {
            if (state == null)
                throw new ArgumentNullException("state");

            System.Console.WriteLine("In  WorkerItemCallback: state={0}", state);
            System.Threading.Thread.Sleep(1000);
        }

        // 2.ExecutionContext 执行上下文测试
        public static void ExecutionContextTest()
        {
            int test = 0;

            CallContext.LogicalSetData("Name", "chenwei");

            CallContext.SetData("FirstName", "chen");

            ThreadPool.QueueUserWorkItem((m) =>
            {
                System.Console.WriteLine("Name:{0}", CallContext.LogicalGetData("Name"));

                System.Console.WriteLine("Test:{0}", test);
            });

            ExecutionContext.SuppressFlow();

            ThreadPool.QueueUserWorkItem((m) =>
            {
                System.Console.WriteLine("Name:{0}", CallContext.LogicalGetData("Name"));
            });

            ExecutionContext.RestoreFlow();
        }

        // 3.协作式取消
        public static void CancellationTokenTest()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.Token.Register(() => { System.Console.WriteLine("Count before cancel."); throw new Exception("aaaaa"); });
            cts.Token.Register(() => { throw new ArgumentException("bbbbb"); });

            // 创建一个工作者线程执行计算限制的异步操作
            ThreadPool.QueueUserWorkItem((m) =>
            {
                Count(cts.Token, 100000);
            });

            System.Console.WriteLine("Hit <ENTER> to cancel this program.");
            System.Console.ReadLine();
            try
            {
                cts.Cancel();
            }
            catch (System.AggregateException ex)
            {
                System.Console.WriteLine(ex.Message);
            }

        }
        static void Count(CancellationToken ct, int countTo)
        {
            for (var i = 0; i < countTo; i++)
            {
                if (ct.IsCancellationRequested)
                {
                    System.Console.WriteLine("Count is cancelled.");
                    break;
                }

                System.Console.WriteLine(i);

                System.Threading.Thread.Sleep(200);
            }

            System.Console.WriteLine("Count is done.");
        }


    }
}
