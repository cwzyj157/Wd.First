using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtherTestProject.Console
{
    /// <summary>
    /// 测试代码
    /// </summary>
    partial class Program
    {
        static void ObserveGc()
        {
            //System.Threading.Thread.Sleep(10000);

            //OtherTestProject.Console.ImproveCodeSeries.ObserveGcTest.TestGcUseStatus();

            //System.Console.WriteLine("Program end");

            //GcTest();

            //GcTest1();

            GcTest2();
        }
        private static void GcTest()
        {
            int start = Environment.TickCount;
            for (int i = 0; i < 1000; i++)
            {
                string s = "";
                for (int j = 0; j < 100; j++)
                {
                    s += "Outer index = ";
                    s += i;
                    s += " Inner index = ";
                    s += j;
                    s += " ";
                }
            }
            System.Console.WriteLine("Program ran for {0} seconds",
                0.001 * (Environment.TickCount - start));
        }


        private static void GcTest1()
        {
            int start = Environment.TickCount;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    sb.Append("Outer index = ")
                        .Append(i)
                        .Append(" Inner index = ")
                        .Append(j)
                        .Append(" ");
                }
            }
            System.Console.WriteLine("Program ran for {0} seconds",
                0.001 * (Environment.TickCount - start));
        }
        private static void GcTest2()
        {
            System.Console.WriteLine("int: " + MyClass<int>.Key);
            System.Console.WriteLine("string: " + MyClass<string>.Key);

            System.Console.ReadLine();
        }
    }
    public static class MyClass<T>
    {
        public static readonly Guid Key = Guid.NewGuid();
    }
}

namespace OtherTestProject.Console.ImproveCodeSeries
{
    internal sealed class ObserveGcTest
    {
        // 测试ObserveGc对内存使用状况
        public static void TestGcUseStatus()
        {
            for (int i = 0; i < 2000; i++)
            {
                using (ObserveGc gc = new ObserveGc())
                {
                }
            }
        }
    }

    internal sealed class ObserveGc : IDisposable
    {
        private byte[] buffer;

        public ObserveGc()
        {
            buffer = new byte[1024 * 1024 * 2];
        }

        ~ObserveGc()
        {
            Dispose();
        }

        #region IDisposable Members

        public void Dispose()
        {
            buffer = null;
        }

        #endregion
    }
}
