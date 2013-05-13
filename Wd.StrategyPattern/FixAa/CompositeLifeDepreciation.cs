using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.StrategyPattern.FixAa
{
    /// <summary>
    /// 平均年限折旧法
    /// </summary>
    class CompositeLifeDepreciation : IDepreciation
    {
        public double calculate(FixedAssetsInfo fa)
        {
            Console.WriteLine("固定资产批次折旧--------平均年限折旧法");
            return 0;
        }
    }
}
