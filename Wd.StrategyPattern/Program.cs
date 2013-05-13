using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wd.StrategyPattern.FixAa;

namespace Wd.StrategyPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            FixedAssetsInfo fai = new FixedAssetsInfo();// 固定资产信息

            IDepreciation depreciation = new CompositeLifeDepreciation();
            FixedAssetsDepreciation fad = new FixedAssetsDepreciation(depreciation);
            fad.calculate(fai); //平均年限折旧法

            depreciation = new FixedPercentageDepreciation();
            fad = new FixedAssetsDepreciation(depreciation);
            fad.calculate(fai); //定率法

            fad = new FixedAssetsDepreciation(DepreciationFactory.Create("2"));
            fad.calculate(fai); //定率法

            Console.ReadLine();
        }
    }
}
