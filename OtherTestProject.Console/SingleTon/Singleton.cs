using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtherTestProject.Console
{
    /// <summary>
    /// 单例
    /// </summary>
    public sealed class Singleton
    {
        // 私有构造函数，阻止通过new方式创建对象
        private Singleton()
        {
            /* 如果同时有多个线程同时创建对象，可能依然会创建多个实例对象 */
            System.Console.WriteLine("new Singleton");
        }

        /* 单例创建方式1:非线程安全 */
        //public static Singleton instance;
        //public static Singleton Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new Singleton();
        //        }
        //        return instance;
        //    }
        //}

        /* 单例创建方式2 */
        // 当字段声明包括 readonly 修饰符时，该声明引入的字段赋值只能作为声明的一部分出现，或者出现在同一类的构造函数中。

        //private static readonly Singleton instance = new Singleton();
        //public static Singleton Instance
        //{
        //    get { return instance; }
        //}

        /* 单例创建方式3 */
        // 依然是静态自动hold实例
        // volatile 关键字表示字段可能被多个并发执行线程修改。声明为 volatile 的字段不受编译器优化
        //（假定由单个线程访问）的限制。这样可以确保该字段在任何时间呈现的都是最新的值。

        //private static volatile Singleton instance = null;
        //// Lock对象，线程安全所用        
        //private static object obj = new Object();

        //public static Singleton Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            lock (obj)
        //            {
        //                if (instance == null)
        //                    instance = new Singleton();
        //            }
        //        }
        //        return instance;
        //    }
        //}



        /* 
         * readonly 关键字与 const 关键字不同。const 字段只能在该字段的声明中初始化。
         * readonly 字段可以在声明或构造函数中初始化。因此，根据所使用的构造函数，
         * readonly 字段可能具有不同的值。另外，const 字段为编译时常数，
         * 而 readonly 字段可用于运行时常数， 
         */


        /* 单例创建方式4 */
        // 因为下面声明了静态构造函数，所以在第一次访问该类之前，new Singleton()语句不会执行        
        //private static readonly Singleton instance = new Singleton();
        //public static Singleton Instance
        //{
        //    get { return instance; }
        //}
        //// 声明静态构造函数就是为了删除IL里的BeforeFieldInit标记        
        //// 以去北欧静态自动在使用之前被初始化       
        //static Singleton()
        //{
        //    System.Console.WriteLine(" static Singleton");
        //}


        // 因为构造函数是私有的，所以需要使用lambda        
        //private static readonly Lazy<Singleton> _instance = new Lazy<Singleton>(() => new Singleton());
        //// new Lazy<Singleton>(() => new Singleton(), LazyThreadSafetyMode.ExecutionAndPublication);       
        //public static Singleton Instance
        //{
        //    get { return _instance.Value; }
        //}
    }
}
