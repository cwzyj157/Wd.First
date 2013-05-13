using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.StrategyPattern.FixAa
{
    /// <summary>
    /// 工作量法
    /// </summary>
    class WorkLoadDepreciation : IDepreciation
    {
        public double calculate(FixedAssetsInfo fa)
        {
            Console.WriteLine("固定资产批次折旧--------工作量法");
            return 0;
        }
    }
}
