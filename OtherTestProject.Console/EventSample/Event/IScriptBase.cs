using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtherTestProject.Console
{
    internal interface IScriptBase
    {
        event EventHandler<ScriptEventArgs> OnError;
    }
    internal class ScriptProvider : IScriptBase
    {
        #region IScriptBase Members
        event EventHandler<ScriptEventArgs> ErrorEvent;
        #endregion


        event EventHandler<ScriptEventArgs> IScriptBase.OnError
        {
            add { ErrorEvent += value; }
            remove { ErrorEvent -= value; }
        }

        public void OnError(ScriptEventArgs e)
        {  // 触发脚本错误事件
            EventHandler<ScriptEventArgs> temp = System.Threading.Interlocked.CompareExchange(ref ErrorEvent, null, null);
            if (temp != null)
                temp(this, e);
        }
    }
    internal class ScriptEventArgs : EventArgs
    {
        private readonly string mobile, email, rem;


        public ScriptEventArgs(string mobile, string email, string rem)
        {
            this.mobile = mobile;
            this.email = email;
            this.rem = rem;
        }

        public string Mobile { get { return mobile; } }
        public string Email { get { return email; } }
        public string Rem { get { return rem; } }
    }
}
