using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.Extensions.ObjectExtensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 对象是否为空
        /// </summary>
        /// <param name="Object">待检查对象</param>
        /// <returns>[True:对象为空，False:对象不为空]</returns>
        public static bool IsNull(this object o)
        {
            return o == null;
        }

    }
}
