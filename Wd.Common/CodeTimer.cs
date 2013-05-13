using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace Wd.Common
{
    public class CodeTimer
    {
        public static void Initialize()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
        }
        public static void Time(string name, int interation, Action action)
        {
            if (String.IsNullOrEmpty(name)) return;
            // 1.
            ConsoleColor currentForeColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine(name);

            // 2.
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            int[] gcCounts = new int[GC.MaxGeneration + 1];
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                gcCounts[i] = GC.CollectionCount(i);
            }

            // 3.
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            ulong cycleCount = GetCycleCount();
            for (int i = 0; i < interation; i++) action();
            ulong cpuCycle = GetCycleCount() - cycleCount;

            stopWatch.Stop();

            // 4.
            System.Console.ForegroundColor = currentForeColor;
            System.Console.WriteLine("\tTime Elapsed:\t" + stopWatch.ElapsedMilliseconds.ToString("N0") + "ms");
            System.Console.WriteLine("\tCPU Cycles:\t" + cpuCycle.ToString("N0"));

            // 5.
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                int count = GC.CollectionCount(i) - gcCounts[i];
                System.Console.WriteLine("\tGen " + i + "\t\t" + count);
            }
            System.Console.WriteLine();
        }
        private static ulong GetCycleCount()
        {
            ulong cycleCount = 0;
            QueryThreadCycleTime(GetCurrentThread(), ref cycleCount);
            return cycleCount;
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool QueryThreadCycleTime(IntPtr threadHandle, ref ulong cycleTime);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentThread();
    }
}
