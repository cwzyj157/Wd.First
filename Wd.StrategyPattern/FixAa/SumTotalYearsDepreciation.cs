using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.StrategyPattern.FixAa
{
    /// <summary>
    /// 年限总和法
    /// </summary>
    class SumTotalYearsDepreciation : IDepreciation
    {
        public double calculate(FixedAssetsInfo fa)
        {
            Console.WriteLine("固定资产批次折旧--------年限总和法");
            return 0;
        }
    }
}
