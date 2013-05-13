using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.BridgePattern.Sample2
{
    public class CustomerData : DataObject
    {
        private List<string> customers = new List<string>();
        private int current = 0;

        public CustomerData()
        {
            // loaded from a database
            customers.Add("Jim Jones");
            customers.Add("Samual Jackson");
            customers.Add("Allen Good");
            customers.Add("Ann Stills");
            customers.Add("Lisa Giolani");
        }

        public override void NextRecord()
        {
            if (current <= customers.Count - 1) current++;
        }

        public override void PriorRecord()
        {
            if (current > 0) current--;
        }

        public override void AddRecord(string name)
        {
            customers.Add(name);
        }

        public override void DeleteRecord(string name)
        {
            customers.Remove(name);
        }

        public override void ShowRecord()
        {
            Console.WriteLine(customers[current]);
        }

        public override void ShowAllRecords()
        {
            foreach (var customer in customers)
                Console.WriteLine(" " + customer);
        }
    }
}
