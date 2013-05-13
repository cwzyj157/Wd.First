using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

namespace Wd.ObserverPattern.MailSend
{
    public static class EventArgExtensions
    {
        public static void Raise<TEventArgs>(this TEventArgs e, object sender, ref EventHandler<TEventArgs> eventDelegate)
            where TEventArgs : EventArgs
        {
            EventHandler<TEventArgs> temp = Interlocked.CompareExchange(ref eventDelegate, null, null);

            if (temp != null) temp(sender, e);
        }
    }
}
