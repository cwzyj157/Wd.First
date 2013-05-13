using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.BuilderPattern.Sample1
{
    class ConcreteBuilder2:Builder
    {
        private Product product = new Product();
        public override void BuildPartA()
        {
            product.Add("PartX");
        }

        public override void BuildPartB()
        {
            product.Add("PartY");
        }

        public override Product GetResult()
        {
            return product;
        }
    }
}
