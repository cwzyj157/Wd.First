using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtherTestProject.Console
{
    internal sealed class Pager
    {
        public Pager(MailManager mailManager)
        {
            //添加事件关注
            mailManager.NewMail += mailManager_NewMail;
        }
        void mailManager_NewMail(object sender, NewMailEventArgs e)
        {
            System.Console.WriteLine(" Pagering mail message:");
            System.Console.WriteLine(" From={0}, To={1}, Subject={2} ", e.From, e.To, e.Subject);
        }
        public void UnRegister(MailManager mailManager)
        {
            mailManager.NewMail -= mailManager_NewMail;
        }
    }
}
