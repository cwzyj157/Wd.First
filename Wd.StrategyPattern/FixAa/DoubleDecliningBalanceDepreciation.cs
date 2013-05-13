using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.StrategyPattern.FixAa
{
    /// <summary>
    /// 双倍余额递减法
    /// </summary>
    class DoubleDecliningBalanceDepreciation : IDepreciation
    {
        public double calculate(FixedAssetsInfo fa)
        {
            Console.WriteLine("固定资产批次折旧--------双倍余额递减法");
            return 0;
        }
    }
}
