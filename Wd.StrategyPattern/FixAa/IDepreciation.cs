using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.StrategyPattern.FixAa
{
    #region 抽象策略角色
    /// <summary>
    /// 折旧接口
    /// </summary>
    interface IDepreciation
    {
        double calculate(FixedAssetsInfo fa);
    }
    #endregion
}
