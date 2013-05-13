using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.AdapterPattern.sample2
{
    class Compound
    {
        protected string chemical;
        protected float boilingPoint;
        protected float meltingPoint;
        protected double molecularWeight;
        protected string molecularFormula;

        public Compound(string chemical)
        {
            this.chemical = chemical;
        }
        public virtual void Display()
        {
            Console.WriteLine("\nCompound:{0}-----", chemical);
        }
    }
}
