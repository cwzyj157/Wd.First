using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace OtherTestProject.Console
{
    public class ActionExceptionTest
    {
        public bool TestActionException()
        {
            bool result = true;
            //for (int i = 0; i < 10; i++)
            //{
            Action<int> action = (m) =>
            {
                System.Threading.Thread.Sleep(3000);
                System.Console.WriteLine(m);
                if (m > 5)
                {
                    throw new Exception(m + "错误了");
                }
            };
            action.BeginInvoke(10, (ar) =>
            {
                try
                {
                    action.EndInvoke(ar);
                }
                catch (Exception ex)
                {
                    result = false;
                    System.Console.WriteLine(ex.Message);
                }
            }, null);
            //}
            return result;
        }

        public void TestActionException1()
        {
            Action<int> action = (m) =>
            {
                System.Threading.Thread.Sleep(3000);
                System.Console.WriteLine(m);
            };
            IAsyncResult ar = action.BeginInvoke(10, null, null);

            bool signalled = ar.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(4));
            if (signalled)
            {
                try
                {
                    action.EndInvoke(ar);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }

    public class IAsyncResultTest
    {
    }
    public class BlockUntilOperationCompletes
    {
        public static void Test()
        {
            // Start the asynchronous request for DNS information.
            // This example does not use a delegate or user-supplied object
            // so the last two arguments are null.
            IAsyncResult result = Dns.BeginGetHostEntry(IPAddress.Parse("192.168.21.107"), null, null);
            System.Console.WriteLine("Processing your request for information...");
            // Do any additional work that can be done here.
            try
            {
                // EndGetHostByName blocks until the process completes.
                IPHostEntry host = Dns.EndGetHostEntry(result);
                string[] aliases = host.Aliases;
                IPAddress[] addresses = host.AddressList;
                if (aliases.Length > 0)
                {
                    System.Console.WriteLine("Aliases");
                    for (int i = 0; i < aliases.Length; i++)
                    {
                        System.Console.WriteLine("{0}", aliases[i]);
                    }
                }
                if (addresses.Length > 0)
                {
                    System.Console.WriteLine("Addresses");
                    for (int i = 0; i < addresses.Length; i++)
                    {
                        System.Console.WriteLine("{0}", addresses[i].ToString());
                    }
                }
            }
            catch (SocketException e)
            {
                System.Console.WriteLine("An exception occurred while processing the request: {0}", e.Message);
            }
        }

        static void UpdateUserInterface()
        {
            // Print a period to indicate that the application
            // is still working on the request.
            System.Console.Write(".");
        }
        public static void Test1()
        {
            // Start the asychronous request for DNS information.
            IAsyncResult result = Dns.BeginGetHostEntry(IPAddress.Parse("192.168.21.107"), null, null);
            System.Console.WriteLine("Processing request for information...");

            // Poll for completion information.
            // Print periods (".") until the operation completes.
            while (result.IsCompleted != true)
            {
                UpdateUserInterface();
            }
            // The operation is complete. Process the results.
            // Print a new line.
            System.Console.WriteLine();
            try
            {
                IPHostEntry host = Dns.EndGetHostEntry(result);
                string[] aliases = host.Aliases;
                IPAddress[] addresses = host.AddressList;
                if (aliases.Length > 0)
                {
                    System.Console.WriteLine("Aliases");
                    for (int i = 0; i < aliases.Length; i++)
                    {
                        System.Console.WriteLine("{0}", aliases[i]);
                    }
                }
                if (addresses.Length > 0)
                {
                    System.Console.WriteLine("Addresses");
                    for (int i = 0; i < addresses.Length; i++)
                    {
                        System.Console.WriteLine("{0}", addresses[i].ToString());
                    }
                }
            }
            catch (SocketException e)
            {
                System.Console.WriteLine("An exception occurred while processing the request: {0}", e.Message);
            }
        }



        static int requestCounter;
        static ArrayList hostData = new ArrayList();
        static StringCollection hostNames = new StringCollection();
        static void UpdateUserInterface1()
        {
            // Print a message to indicate that the application
            // is still working on the remaining requests.
            System.Console.WriteLine("{0} requests remaining.", requestCounter);
        }
        public static void Test2()
        {
            // Create the delegate that will process the results of the 
            // asynchronous request.
            AsyncCallback callBack = new AsyncCallback(ProcessDnsInformation);
            string host;
            do
            {
                System.Console.Write(" Enter the name of a host computer or <enter> to finish: ");
                host = System.Console.ReadLine();
                if (host.Length > 0)
                {
                    // Increment the request counter in a thread safe manner.
                    Interlocked.Increment(ref requestCounter);
                    // Start the asynchronous request for DNS information.
                    Dns.BeginGetHostEntry(host, callBack, host);
                }
            } while (host.Length > 0);
            // The user has entered all of the host names for lookup.
            // Now wait until the threads complete.
            while (requestCounter > 0)
            {
                UpdateUserInterface1();
            }
            // Display the results.
            for (int i = 0; i < hostNames.Count; i++)
            {
                object data = hostData[i];
                string message = data as string;
                // A SocketException was thrown.
                if (message != null)
                {
                    System.Console.WriteLine("Request for {0} returned message: {1}",
                        hostNames[i], message);
                    continue;
                }
                // Get the results.
                IPHostEntry h = (IPHostEntry)data;
                string[] aliases = h.Aliases;
                IPAddress[] addresses = h.AddressList;
                if (aliases.Length > 0)
                {
                    System.Console.WriteLine("Aliases for {0}", hostNames[i]);
                    for (int j = 0; j < aliases.Length; j++)
                    {
                        System.Console.WriteLine("{0}", aliases[j]);
                    }
                }
                if (addresses.Length > 0)
                {
                    System.Console.WriteLine("Addresses for {0}", hostNames[i]);
                    for (int k = 0; k < addresses.Length; k++)
                    {
                        System.Console.WriteLine("{0}", addresses[k].ToString());
                    }
                }
            }
        }

        // The following method is called when each asynchronous operation completes.
        static void ProcessDnsInformation(IAsyncResult result)
        {
            string hostName = (string)result.AsyncState;
            hostNames.Add(hostName);
            try
            {
                // Get the results.
                IPHostEntry host = Dns.EndGetHostEntry(result);
                hostData.Add(host);
            }
            // Store the exception message.
            catch (SocketException e)
            {
                hostData.Add(e.Message);
            }
            finally
            {
                // Decrement the request counter in a thread-safe manner.
                Interlocked.Decrement(ref requestCounter);
            }
        }


    }

    public delegate string AsyncMethodCaller(int callDuration, out int threadId);

    public class DelegateAsyncTest
    {
        public string TestMethod(int callDuration, out int threadId)
        {
            System.Console.WriteLine("Test method begins.");
            Thread.Sleep(callDuration);
            threadId = Thread.CurrentThread.ManagedThreadId;
            return String.Format("My call time was {0}.", callDuration.ToString());
        }
    }


    // Create a class that factors a number.
    public class PrimeFactorFinder
    {
        public static bool Factorize(
                     int number,
                     ref int primefactor1,
                     ref int primefactor2)
        {
            Thread.Sleep(10000);

            primefactor1 = 1;
            primefactor2 = number;

            // Factorize using a low-tech approach.
            for (int i = 2; i < number; i++)
            {
                if (0 == (number % i))
                {
                    primefactor1 = i;
                    primefactor2 = number / i;
                    break;
                }
            }
            if (1 == primefactor1)
                return false;
            else
                return true;
        }
    }

    // Create an asynchronous delegate that matches the Factorize method.
    public delegate bool AsyncFactorCaller(
             int number,
             ref int primefactor1,
             ref int primefactor2);

    public class DemonstrateAsyncPattern
    {
        // The waiter object used to keep the main application thread
        // from terminating before the callback method completes.
        ManualResetEvent waiter;

        // Define the method that receives a callback when the results are available.
        public void FactorizedResults(IAsyncResult result)
        {
            int factor1 = 0;
            int factor2 = 0;

            // Extract the delegate from the 
            // System.Runtime.Remoting.Messaging.AsyncResult.
            AsyncFactorCaller factorDelegate = (AsyncFactorCaller)((AsyncResult)result).AsyncDelegate;
            int number = (int)result.AsyncState;
            // Obtain the result.
            bool answer = factorDelegate.EndInvoke(ref factor1, ref factor2, result);
            // Output the results.
            System.Console.WriteLine("On CallBack: Factors of {0} : {1} {2} - {3}",
                 number, factor1, factor2, answer);
            waiter.Set();
        }

        // The following method demonstrates the asynchronous pattern using a callback method.
        public void FactorizeNumberUsingCallback()
        {
            AsyncFactorCaller factorDelegate = new AsyncFactorCaller(PrimeFactorFinder.Factorize);
            int number = 1000589023;
            int temp = 0;
            // Waiter will keep the main application thread from 
            // ending before the callback completes because
            // the main thread blocks until the waiter is signaled
            // in the callback.
            waiter = new ManualResetEvent(false);

            // Define the AsyncCallback delegate.
            AsyncCallback callBack = new AsyncCallback(this.FactorizedResults);

            // Asynchronously invoke the Factorize method.
            IAsyncResult result = factorDelegate.BeginInvoke(
                                 number,
                                 ref temp,
                                 ref temp,
                                 callBack,
                                 number);

            // Do some other useful work while 
            // waiting for the asynchronous operation to complete.
            int i = 0;


            // When no more work can be done, wait.
            waiter.WaitOne();
        }

        // The following method demonstrates the asynchronous pattern 
        // using a BeginInvoke, followed by waiting with a time-out.
        public void FactorizeNumberAndWait()
        {
            AsyncFactorCaller factorDelegate = new AsyncFactorCaller(PrimeFactorFinder.Factorize);

            int number = 1000589023;
            int temp = 0;

            // Asynchronously invoke the Factorize method.
            IAsyncResult result = factorDelegate.BeginInvoke(
                              number,
                              ref temp,
                              ref temp,
                              null,
                              null);

            while (!result.IsCompleted)
            {
                // Do any work you can do before waiting.
                result.AsyncWaitHandle.WaitOne(10000, false);
            }
            result.AsyncWaitHandle.Close();

            // The asynchronous operation has completed.
            int factor1 = 0;
            int factor2 = 0;

            // Obtain the result.
            bool answer = factorDelegate.EndInvoke(ref factor1, ref factor2, result);

            // Output the results.
            System.Console.WriteLine("Sequential : Factors of {0} : {1} {2} - {3}",
                              number, factor1, factor2, answer);
        }
    }

}
