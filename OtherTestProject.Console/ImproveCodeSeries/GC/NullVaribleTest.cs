using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtherTestProject.Console
{
    /// <summary>
    /// 
    /// </summary>
    partial class Program
    {
        static void NullVaribleTest()
        {

            System.Console.WriteLine(DateTime.UtcNow.Add(TimeSpan.FromMinutes(3)));

            OtherTestProject.Console.ImproveCodeSeries.NullVarible obj = new OtherTestProject.Console.ImproveCodeSeries.NullVarible();
            Action a = () =>
            {
                System.Console.WriteLine("in:" + System.Threading.Thread.CurrentThread.ManagedThreadId);

                //obj.MethodClearNull();
                obj.MethodNotClearNull();
            };
            a.BeginInvoke(null, null);

            // 强制垃圾回收       
            System.Console.WriteLine("outer:" + System.Threading.Thread.CurrentThread.ManagedThreadId);
            obj.ForceGarbageCollection();
        }
    }
}

namespace OtherTestProject.Console.ImproveCodeSeries
{
    /// <summary>
    /// 该类旨在验证将局部变量设置为null是无关意义的
    /// </summary>
    internal class NullVarible
    {
        public void MethodNotClearNull()
        {
            ManagedObj mo = new ManagedObj();

            System.Threading.Thread.Sleep(10000);

            System.Console.WriteLine("MethodNotClearNull over");
        }
        public void MethodClearNull()
        {
            ManagedObj mo = new ManagedObj();
            mo = null;

            System.Threading.Thread.Sleep(10000);

            System.Console.WriteLine("MethodClearNull over");
        }
        public void ForceGarbageCollection()
        {
            System.Threading.Thread.Sleep(5000);

            System.Console.WriteLine("start garbageColection");
            System.GC.Collect();
        }
    }
    internal class ManagedObj
    {
        private readonly List<Person> lstPerson;

        public ManagedObj()
        {
            lstPerson = new List<Person>();

            for (int i = 0; i < 100000; i++)
            {
                lstPerson.Add(new Person("zhang" + i, "san" + i));
            }
        }

        ~ManagedObj()
        {
            System.Console.WriteLine("ManagedObj has been cleared");
        }
    }
    internal class Person
    {
        private string firstName;
        private string secondName;

        public Person(string firstName, string secondName)
        {
            this.firstName = firstName; this.secondName = secondName;
        }

        ~Person()
        {
            //System.Console.WriteLine("Current Person is {0} {1}", firstName, secondName);
        }
    }
}
