using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wd.ObserverPattern.MailSend;

namespace Wd.ObserverPattern
{
    class Program
    {
        /*
         *  观察者模式定义：定义了对象之间的一对多依赖关系，当一个对象状态改变时，它的所有依赖对象都会收到通知并自动更新
         *  1:具有状态的对象称为“主题”或“可观察者”，依赖对象称为“观察者”
         *  所以一般具有以下三步：
         *  1.建立“主题”对象
         *  2.添加“观察者”对象以及与"主题"之间的依赖
         *  3."主题"对象状态变化
         */

        static void Main(string[] args)
        {
            // 创建邮件管理器
            MailManager mailManager = new MailManager();
            // 给传真添加邮件事件关注
            Fax fax = new Fax(mailManager);
            Pager pager = new Pager(mailManager);
            // 接受邮件动作
            mailManager.SimulateNewMail("cwzyj157@sina.com", "w.chen@sunlike.com.cn", "测试");
            // 注销事件关注
            fax.UnRegister(mailManager);
            pager.UnRegister(mailManager);

            Console.ReadLine();
        }
    }
}
