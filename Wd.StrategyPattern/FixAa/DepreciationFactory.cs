using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.StrategyPattern.FixAa
{
    /// <summary>
    /// 固定资产折旧工厂
    /// </summary>
    class DepreciationFactory
    {
        /// <summary>
        /// 创建折旧算法类
        /// </summary>
        /// <param name="type">折旧算法类别【"0":平均年限折旧法 "1":定率法 "2":年限总和法 "3":双倍余额递减法 "4":工作量法】</param>
        /// <returns></returns>
        public static IDepreciation Create(string type)
        {
            if (type == "0") return new CompositeLifeDepreciation();
            if (type == "1") return new FixedPercentageDepreciation();
            if (type == "2") return new SumTotalYearsDepreciation();
            if (type == "3") return new DoubleDecliningBalanceDepreciation();
            if (type == "4") return new WorkLoadDepreciation();
            return new CompositeLifeDepreciation();
        }
    }
}
