using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OtherTestProject.Console.ReflectionSample;
using System.Reflection;
using System.Diagnostics;

namespace OtherTestProject.Console
{
    partial class Program
    {
        static void ReflectionTest()
        {
            //List<string> lstTbActionType = GetEnums<TBActionType>();
            //lstTbActionType.ForEach(m => System.Console.WriteLine(m));

            //ReflectionSample.ReflectionTest.GetAssembly();

            ReflectionEfficiency();
        }

        static void ReflectionEfficiency()
        {
            int times = 1000000;
            ReflectionEfficiency re = new ReflectionEfficiency();
            re.Call(null, null, null);  // force jit compile

            object[] parameters = new object[] { new object(), new object(), new object() };
            BindingFlags bf = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic;


            Stopwatch watch1 = new Stopwatch();
            watch1.Start();
            for (int i = 0; i < times; i++)
            {
                re.Call(parameters[0], parameters[1], parameters[2]);
            }
            watch1.Stop();
            System.Console.WriteLine("directly invoke:{0}", watch1.Elapsed);

            MethodInfo mi = typeof(ReflectionEfficiency).GetMethod("Call", bf);
            Stopwatch watch2 = new Stopwatch();
            watch2.Start();
            for (int i = 0; i < times; i++)
            {
                mi.Invoke(re, parameters);
            }
            watch2.Stop();
            System.Console.WriteLine("reflection invoke:{0}", watch2.Elapsed);


            MethodInfo mi1 = typeof(ReflectionEfficiency).GetMethod("Call", bf);
            var invoker = (Func<object, object, object, object>)Delegate.CreateDelegate(typeof(Func<object, object, object, object>), re, mi1);
            Stopwatch watch3 = new Stopwatch();
            watch3.Start();
            for (int i = 0; i < times; i++)
            {
                invoker(parameters[0], parameters[1], parameters[2]);
            }
            watch3.Stop();
            System.Console.WriteLine("Delegate invoke:{0}", watch3.Elapsed);
        }

    }
}

namespace OtherTestProject.Console.ReflectionSample
{
    internal sealed class ReflectionTest
    {
        internal static void GetAssembly()
        {
            StringBuilder sb = new StringBuilder();
            Assembly assembly = Assembly.Load("Wd.CommonUtil");

            sb.AppendFormat(" assembly fullname:{0}", assembly.FullName).AppendLine()
                .AppendFormat(" location:{0}", assembly.Location).AppendLine();

            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                sb.AppendFormat(" type:{0}", type.FullName)
                    .AppendLine();
            }
            System.Console.WriteLine(sb.ToString());
        }
    }

    internal sealed class ReflectionEfficiency
    {
        public object Call(object o1, object o2, object o3) { return null; }
    }
}
