using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wd.Extensions;

namespace Wd.Extensions.Test
{
    partial class Program
    {
        private static void StringToDateTimeExtensionTest()
        {
            // AST 已测试
            string[] timeZoneAry = new string[] { "AST", "BT", "CET", "CST", "EET", "EST", "GMT", "GST", "HST", "ACDT" };
            foreach (string timeZone in timeZoneAry)
            {
                string datetime = "2013-01-28 16:06:50";
                string toTimeZone = timeZone;
                DateTime outDatetime;
                datetime.TryParseToDatetime(toTimeZone, out outDatetime);
                System.Console.WriteLine("currentTimeZone:{0} ===> {1}", TimeZone.CurrentTimeZone.StandardName, toTimeZone);
                System.Console.WriteLine("currentDateTime:{0} ===> {1}", datetime, outDatetime);

                string outDateTimeStr = outDatetime.ToString();
                bool result = (outDateTimeStr + " " + toTimeZone).TryParseToDatetime(out outDatetime);
                if (result)
                {
                    System.Console.WriteLine("currentTimeZone:{0} ===> {1}", toTimeZone, TimeZone.CurrentTimeZone.StandardName);
                    System.Console.WriteLine("currentDateTime:{0} ===> {1}", outDateTimeStr, datetime);
                }

                System.Console.WriteLine();
            }
        }
    }
}
