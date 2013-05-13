using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.StrategyPattern.FixAa
{
    /// <summary>
    /// 固定资产
    /// </summary>
    class FixedAssetsDepreciation
    {
        private IDepreciation depreciation;
        public FixedAssetsDepreciation(IDepreciation depreciation)
        {
            if (depreciation == null) throw new ArgumentNullException("depreciation");
            this.depreciation = depreciation;
        }
        public double calculate(FixedAssetsInfo fa)
        {
            return depreciation.calculate(fa);
        }
    }
}
