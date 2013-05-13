using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

namespace OtherTestProject.Console
{
    internal class MailManager
    {
        /// <summary>
        /// 事件成员
        /// </summary>
        public event EventHandler<NewMailEventArgs> NewMail;

        public delegate void MyEventDelegate(object sender, EventArgs e);

        /// <summary>
        /// 引发事件的受保护的虚方法
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnNewMail(NewMailEventArgs e)
        {
            //EventHandler<NewMailEventArgs> temp = Interlocked.CompareExchange(ref NewMail, null, null);

            //if (temp != null) temp(this, e);

            e.Raise<NewMailEventArgs>(this, ref NewMail);
        }
        /// <summary>
        /// 定义方法将输入转化为事件
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        public void SimulateNewMail(string from, string to, string subject)
        {
            NewMailEventArgs e = new NewMailEventArgs(from, to, subject);

            OnNewMail(e);
        }
    }
    /// <summary>
    /// 事件附加信息
    /// </summary>
    internal class NewMailEventArgs : EventArgs
    {
        private readonly string m_from, m_to, m_subject;

        public NewMailEventArgs(string m_from, string m_to, string m_subject)
        {
            this.m_from = m_from;
            this.m_to = m_to;
            this.m_subject = m_subject;
        }

        public string From { get { return m_from; } }
        public string To { get { return m_to; } }
        public string Subject { get { return m_subject; } }
    }
}
