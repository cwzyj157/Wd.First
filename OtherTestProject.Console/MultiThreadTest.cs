using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace OtherTestProject.Console
{
    class MultiThreadTest
    {
    }
    public class Search
    {
        // Maintain state information to pass to FindCallback.
        class State
        {
            public AutoResetEvent autoEvent;
            public string fileName;

            public State(AutoResetEvent autoEvent, string fileName)
            {
                this.autoEvent = autoEvent;
                this.fileName = fileName;
            }
        }

        AutoResetEvent[] autoEvents;
        String[] diskLetters;

        public Search()
        {
            // Retrieve an array of disk letters.
            diskLetters = Environment.GetLogicalDrives();

            autoEvents = new AutoResetEvent[diskLetters.Length];
            for (int i = 0; i < diskLetters.Length; i++)
            {
                autoEvents[i] = new AutoResetEvent(false);
            }
        }

        // Search for fileName in the root directory of all disks.
        public void FindFile(string fileName)
        {
            for (int i = 0; i < diskLetters.Length; i++)
            {
                System.Console.WriteLine("Searching for {0} on {1}.",
                     fileName, diskLetters[i]);
                ThreadPool.QueueUserWorkItem(
                    new WaitCallback(FindCallback),
                    new State(autoEvents[i], diskLetters[i] + fileName));
            }

            // Wait for the first instance of the file to be found.
            int index = WaitHandle.WaitAny(autoEvents, 100000, false);
            if (index == WaitHandle.WaitTimeout)
            {
                System.Console.WriteLine("\n{0} not found.", fileName);
            }
            else
            {
                System.Console.WriteLine("\n{0} found on {1}.", fileName,
                    diskLetters[index]);
            }
        }

        // Search for stateInfo.fileName.
        void FindCallback(object state)
        {
            State stateInfo = (State)state;

            // Signal if the file is found.
            if (File.Exists(stateInfo.fileName))
            {
                stateInfo.autoEvent.Set();
            }
        }
    }



    public sealed class App
    {
        // Define an array with two AutoResetEvent WaitHandles.
        static WaitHandle[] waitHandles = new WaitHandle[] 
        {
            new AutoResetEvent(false),
            new AutoResetEvent(false)
        };

        // Define a random number generator for testing.
        static Random r = new Random();

        public static void Test()
        {
            // Queue up two tasks on two different threads; 
            // wait until all tasks are completed.
            DateTime dt = DateTime.Now;
            System.Console.WriteLine("Main thread is waiting for BOTH tasks to complete.");
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoTask), waitHandles[0]);
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoTask), waitHandles[1]);
            WaitHandle.WaitAll(waitHandles);
            // The time shown below should match the longest task.
            System.Console.WriteLine("Both tasks are completed (time waited={0})",
                (DateTime.Now - dt).TotalMilliseconds);

            // Queue up two tasks on two different threads; 
            // wait until any tasks are completed.
            dt = DateTime.Now;
            System.Console.WriteLine();
            System.Console.WriteLine("The main thread is waiting for either task to complete.");
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoTask), waitHandles[0]);
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoTask), waitHandles[1]);
            int index = WaitHandle.WaitAny(waitHandles);
            // The time shown below should match the shortest task.
            System.Console.WriteLine("Task {0} finished first (time waited={1}).",
                index + 1, (DateTime.Now - dt).TotalMilliseconds);
        }

        static void DoTask(Object state)
        {
            AutoResetEvent are = (AutoResetEvent)state;
            int time = 1000 * r.Next(2, 10);
            System.Console.WriteLine("Performing a task for {0} milliseconds.", time);
            Thread.Sleep(time);
            are.Set();
        }
    }

    internal sealed class MultiThreadTest1
    {
        class ArgsStateIncludeReset
        {
            public AutoResetEvent resetEvent;
            public ArgsState argsState;
            public ArgsStateIncludeReset(ArgsState argsState)
            {
                this.resetEvent = new AutoResetEvent(false);
                this.argsState = argsState;

            }
        }

        private const int MAX_THREAD_NUMS = 4;
        // 申明一个队列
        private static Queue<ArgsStateIncludeReset> queueArgsIncludeReset;
        private static List<AutoResetEvent> lstAutoResetEvent;

        public MultiThreadTest1(List<ArgsState> lstArgs)
        {
            if (queueArgsIncludeReset == null) queueArgsIncludeReset = new Queue<ArgsStateIncludeReset>();
            if (lstAutoResetEvent == null) lstAutoResetEvent = new List<AutoResetEvent>();

            foreach (var args in lstArgs)
            {
                queueArgsIncludeReset.Enqueue(new ArgsStateIncludeReset(args));
            }

        }

        public void Start()
        {
            while (true)
            {

                for (int i = 0; i < MAX_THREAD_NUMS; i++)
                {
                    ArgsStateIncludeReset argsStateIncludeReset = queueArgsIncludeReset.Dequeue();
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadWaitCallBack), argsStateIncludeReset);

                    lstAutoResetEvent.Add(argsStateIncludeReset.resetEvent);
                }
            }
        }
        void ThreadWaitCallBack(object state)
        {
            ArgsStateIncludeReset argsStateIncludeReset = state as ArgsStateIncludeReset;
            if (argsStateIncludeReset != null)
            {
                argsStateIncludeReset.resetEvent.Set();
            }
        }
    }
    sealed class ArgsState{

    }
}
