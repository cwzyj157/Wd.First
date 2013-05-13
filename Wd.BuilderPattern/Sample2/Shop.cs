using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.BuilderPattern.Sample2
{
    class Shop
    {
        public void Contruct(VehicleBuilder builder)
        {
            builder.BuildFrame();
            builder.BuildEngine();
            builder.BuildWheels();
            builder.BuildDoors();
        }
    }
}
