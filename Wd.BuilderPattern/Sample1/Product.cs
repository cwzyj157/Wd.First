using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.BuilderPattern.Sample1
{
    class Product
    {
        private List<string> parts = new List<string>();
        public void Add(string part)
        {
            parts.Add(part);
        }
        public void Show()
        {
            Console.WriteLine("\nProduct Parts-----");
            foreach (string part in parts)
            {
                Console.WriteLine(part);
            }
        }
    }
}
