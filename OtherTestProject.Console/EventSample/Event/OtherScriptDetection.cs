using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtherTestProject.Console
{
    internal sealed class OtherScriptDetection : ScriptBase
    {
        public OtherScriptDetection()
        {
            this.OnError += new EventHandler<ScriptEventArgs>(OtherScriptDetection_OnError);
        }
        /// <summary>
        /// 当脚本发生错误时处理方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OtherScriptDetection_OnError(object sender, ScriptEventArgs e)
        {
            System.Console.WriteLine("Mobile:{0}\tEmail:{1}\tRem:{2}", e.Mobile, e.Email, e.Rem);
        }
        /// <summary>
        /// 脚本检测方法
        /// </summary>
        protected override void Detection()
        {
            System.Console.WriteLine("OtherScriptDetection..Detection");
        }
        /// <summary>
        /// 取消事件登记
        /// </summary>
        protected override void UnRegister()
        {
            this.OnError -= new EventHandler<ScriptEventArgs>(OtherScriptDetection_OnError);
        }
    }
}
