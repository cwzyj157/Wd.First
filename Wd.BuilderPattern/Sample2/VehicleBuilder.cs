using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.BuilderPattern.Sample2
{
    abstract class VehicleBuilder
    {
        protected Vehicle vehicle;
        public Vehicle Vehicle { get { return vehicle; } }

        public abstract void BuildFrame();
        public abstract void BuildEngine();
        public abstract void BuildWheels();
        public abstract void BuildDoors();
    }
}
