using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtherTestProject.Console
{
    internal abstract class ScriptBase
    {
        /// <summary>
        /// 定义事件
        /// </summary>
        protected event EventHandler<ScriptEventArgs> OnError;
        /// <summary>
        /// 脚本检测方法
        /// </summary>
        protected abstract void Detection();
        /// <summary>
        /// 取消事件登记
        /// </summary>
        protected abstract void UnRegister();

        /// <summary>
        /// 脚本检测事件
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnDetection(ScriptEventArgs e)
        {
            // 脚本检测
            Detection();
            // 触发脚本错误事件
            EventHandler<ScriptEventArgs> temp = System.Threading.Interlocked.CompareExchange(ref OnError, null, null);
            if (temp != null)
            {
                temp(this, e);

                // 取消事件登记
                UnRegister();
            }
        }
    }
   
    internal enum ScriptType
    {
        /// <summary>
        /// Web脚本
        /// </summary>
        Web,
        /// <summary>
        /// 数据库脚本
        /// </summary>
        Sql,
        /// <summary>
        /// 其它脚本
        /// </summary>
        Other
    }
}
