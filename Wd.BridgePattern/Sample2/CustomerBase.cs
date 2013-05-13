using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.BridgePattern.Sample2
{
    public class CustomerBase
    {
        private string group;
        public DataObject Data
        {
            get;
            set;
        }
        public CustomerBase(string group)
        {
            this.group = group;
        }

        public virtual void Next()
        {
            Data.NextRecord();
        }
        public virtual void Prior()
        {
            Data.PriorRecord();
        }
        public virtual void Add(string customer)
        {
            Data.AddRecord(customer);
        }
        public virtual void Delete(string customer)
        {
            Data.DeleteRecord(customer);
        }
        public virtual void Show()
        {
            Data.ShowRecord();
        }
        public virtual void ShowAll()
        {
            Console.WriteLine("Customer Group:" + group);
            Data.ShowAllRecords();
        }
    }
}
