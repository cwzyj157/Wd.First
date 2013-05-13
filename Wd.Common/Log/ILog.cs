using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.Common
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message"></param>
        void WriteLog(string message);
    }
}
