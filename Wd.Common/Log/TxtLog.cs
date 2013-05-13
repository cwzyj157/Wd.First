using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Wd.Common
{
    public class TxtLog : ILog
    {
        private string path;
        private string fileName;
        /// <summary>
        /// 文本日志构造函数
        /// </summary>
        /// <param name="path">日志存放路径（默认文件名为TxtLog.txt）</param>
        public TxtLog(string path)
            : this(path, "TxtLog")
        {
        }
        /// <summary>
        /// 文本日志构造函数
        /// </summary>
        /// <param name="path">日志存放路径</param>
        /// <param name="fileName">日志文件名</param>
        public TxtLog(string path, string fileName)
        {
            this.path = path;
            this.fileName = fileName;
        }
        /// <summary>
        /// 写本地文件日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public void WriteLog(string message)
        {
            WriteTxtLog(message, true);
        }
        /// <summary>
        /// 写本地文件日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="writeTrace">是否写Trace日志</param>
        private void WriteTxtLog(string message, bool writeTrace)
        {
            // StreamWriter 在某些系统下不会自动创建文件
            FileHelper.CreateDir(path);

            FileHelper.ExecuteAction(LogFilePath, () =>
            {
                WriteTxtLog(message);
            });

            if (writeTrace)
            {
                System.Diagnostics.Trace.WriteLine(message);
            }
        }

        private object obj = new object();
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message">日志内容</param>
        private void WriteTxtLog(string message)
        {
            System.Threading.Monitor.Enter(obj);

            System.IO.StreamWriter fp = new System.IO.StreamWriter(LogFilePath, true, System.Text.Encoding.UTF8);
            fp.WriteLine(message);
            fp.Close();

            System.Threading.Monitor.Exit(obj);
        }
        /// <summary>
        /// 日志文件全路径
        /// </summary>
        private string LogFilePath
        {
            get
            {
                if (!path.EndsWith("//"))
                    path += "/";
                return path + fileName + ".txt";
            }
        }
    }

    public class LogHelper
    {
        private LogHelper() { }

        private static ILog[] instanceAry = new ILog[1] { new TxtLog("")
        };
        public static ILog InstanceTxt
        {
            get { return instanceAry[0]; }
        }

        public static ILog InstanceSql
        {
            get { return instanceAry[1]; }
        }
    }
}
