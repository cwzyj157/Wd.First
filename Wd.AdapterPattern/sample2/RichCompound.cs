using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.AdapterPattern.sample2
{
    class RichCompound : Compound
    {
        private ChemicalDatabank bank;
        public RichCompound(string name) : base(name) { }

        public override void Display()
        {
            bank = new ChemicalDatabank();
            this.boilingPoint = bank.GetCriticalPoint(this.chemical, "B");
            this.meltingPoint = bank.GetCriticalPoint(this.chemical, "M");
            this.molecularFormula = bank.GetMolecularStructure(this.chemical);
            this.molecularWeight = bank.GetMolecularWeight(this.chemical);

            base.Display();

            Console.WriteLine("Formula:{0}", molecularFormula);
            Console.WriteLine("Weight:{0}", molecularWeight);
            Console.WriteLine("Melting Pt:{0}", meltingPoint);
            Console.WriteLine("Boiling Pt:{0}", boilingPoint);
        }
    }
}
