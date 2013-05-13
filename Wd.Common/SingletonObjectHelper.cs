using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.Common
{
    /// <summary>
    /// 单例对象辅助类
    /// </summary>
    public class SingletonObjectHelper
    {
        #region 变量
        private static AClass aclass;
        private static BClass bclass;
        #endregion

        // 类型构造器
        /// <summary>
        /// 由于CLR保证一个类型构造器在每个AppDomain中只执行一次，而且这种执行是线程安全的，
        /// 所以非常适合在类型构造器中初始化类型需要的任何单实例（Singleton）对象
        /// </summary>
        static SingletonObjectHelper()
        {
            Console.WriteLine("Init::SingletonObjectHelper");
            aclass = new AClass();
            bclass = new BClass();
        }

        #region 属性
        public static AClass AClassInstance
        {
            get { return aclass; }
        }
        public static BClass BClassInstance
        {
            get { return bclass; }
        }
        #endregion
    }

    public class AClass
    {
    }
    public class BClass
    {
    }
}
