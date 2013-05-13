using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.StrategyPattern.FixAa
{
    /// <summary>
    /// 定率法
    /// </summary>
    class FixedPercentageDepreciation : IDepreciation
    {
        public double calculate(FixedAssetsInfo fa)
        {
            Console.WriteLine("固定资产批次折旧--------定率法");
            return 0;
        }
    }
}
