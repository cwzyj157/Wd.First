using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace OtherTestProject.Console
{
    public class ThreadPoolApp
    {
        public void ExecutionContextTest()
        {
            CallContext.LogicalSetData("name", "w.chen");

            ThreadPool.QueueUserWorkItem(m =>
            {
                System.Threading.Thread.Sleep(1000);
                System.Console.WriteLine("Name={0}", CallContext.LogicalGetData("name"));
            });

            ExecutionContext.SuppressFlow();

            ThreadPool.QueueUserWorkItem(m =>
            {
                System.Threading.Thread.Sleep(1000);
                System.Console.WriteLine("Name={0}", CallContext.LogicalGetData("name"));
            });

            ExecutionContext.RestoreFlow();
        }
        public void CancellationTokenTest()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            cts.Token.Register(() => { throw new DivideByZeroException("aaa"); System.Console.WriteLine("aaa"); }, true);
            cts.Token.Register(() => { System.Console.WriteLine("bbb"); }, true);
            CancellationTokenRegistration ctr =
                cts.Token.Register(() => { throw new OverflowException("ccc"); System.Console.WriteLine("ccc"); }, true);

            //ctr.Dispose();

            ThreadPool.QueueUserWorkItem(m =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    if (cts.Token.IsCancellationRequested)
                    {
                        // 在注册取消事件后，再调用dispose方法是没有办法真正的取消事件的
                        cts.Token.Register(() =>
                        {
                            Thread.Sleep(500);
                            System.Console.WriteLine("ddd");
                        }, false);

                        System.Console.WriteLine("Count is cancelled");
                        break;
                    }

                    System.Console.WriteLine(i);
                    Thread.Sleep(500);
                }
            });

            System.Console.WriteLine("Press <Enter> to cancel the operations.");

            System.Console.ReadLine();


            try
            {

                cts.Cancel(false);
            }
            catch (AggregateException ex)
            {
                try
                {
                    ex.Handle(m =>
                    {
                        if (m.GetType() == typeof(System.DivideByZeroException))
                        {
                            System.Console.WriteLine("handle DivideByZeroException");
                            return true;
                        }
                        return false;
                    });
                }
                catch (AggregateException ex1)
                {
                    ex1.Handle(m =>
                    {
                        if (m.GetType() == typeof(System.OverflowException))
                        {
                            System.Console.WriteLine("handle OverflowException");
                            return true;
                        }
                        return false;
                    });
                }
            }
            System.Console.WriteLine("operations has been  cancelled");
        }

        public void LinkedTokenTest()
        {
            var cts1 = new CancellationTokenSource();
            cts1.Token.Register(() => { System.Console.WriteLine("cts1 canceled"); });

            var cts2 = new CancellationTokenSource();
            cts2.Token.Register(() => { System.Console.WriteLine("cts2 canceled"); });

            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts1.Token, cts2.Token);
            linkedCts.Token.Register(() => { System.Console.WriteLine("linkedCts canceled"); });

            cts2.Cancel();

            System.Console.WriteLine("cts1 canceled ={0},cts2 canceled={1},linkedCts canceled={2}",
                cts1.IsCancellationRequested, cts2.IsCancellationRequested, linkedCts.IsCancellationRequested);
        }
    }

    public class TaskApp
    {
        public Int32 Sum(int n)
        {
            int sum = 0;
            for (; n > 0; n--)
            {
                checked { sum += n; }
            }
            return sum;
        }
        public void TaskTest()
        {
            Task<Int32> task = new Task<Int32>(n => Sum((int)n), 10000000);

            task.Start();



            //try
            //{
            //    task.Wait();
            //}
            //catch (AggregateException ex)
            //{
            //    ex.Handle(m =>
            //    {
            //        System.Console.WriteLine(m.Message);
            //        return true;
            //    });
            //}
            //System.Console.WriteLine("The Sum is :" + task.Result);


        }


        public Int32 Sum(CancellationToken ct, int n)
        {
            //Thread.Sleep(10000);

            int sum = 0;
            for (; n > 0; n--)
            {
                ct.ThrowIfCancellationRequested();

                checked { sum += n; }
            }
            return sum;
        }
        public void TaskTest1()
        {
            CancellationTokenSource cts = new CancellationTokenSource();


            Task<Int32> task = new Task<Int32>(n => Sum(cts.Token, (int)n), 10, cts.Token);
            task.Start();

            cts.Cancel();


            try
            {
                task.ContinueWith(m => System.Console.WriteLine("The Sum is :" + m.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
                task.ContinueWith(m => System.Console.WriteLine("Sum throw :" + m.Exception), TaskContinuationOptions.OnlyOnFaulted);
                task.ContinueWith(m => System.Console.WriteLine("The Sum was canceled :"), TaskContinuationOptions.OnlyOnCanceled);

                System.Console.WriteLine(" has end");
            }
            catch (AggregateException ex)
            {
                ex.Handle(m =>
                {
                    System.Console.WriteLine(m.Message);
                    return true;
                });
            }
        }

        public void TaskParentTest()
        {
            Task<int[]> taskParent = new Task<int[]>(() =>
            {
                var results = new int[3];
                new Task(() => results[0] = Sum(10000), TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[1] = Sum(20000), TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[2] = Sum(30000), TaskCreationOptions.AttachedToParent).Start();

                return results;
            });
            var cwt = taskParent.ContinueWith(m => Array.ForEach(m.Result, System.Console.WriteLine));

            taskParent.Start();
        }
    }
}
