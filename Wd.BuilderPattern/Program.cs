using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wd.BuilderPattern.Sample1;
using Wd.BuilderPattern.Sample2;

namespace Wd.BuilderPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            // sample1
            Director director = new Director();
            Builder b1 = new ConcreteBuilder1();
            Builder b2 = new ConcreteBuilder2();

            director.Construct(b1);
            Product p1 = b1.GetResult();
            p1.Show();

            director.Construct(b2);
            Product p2 = b2.GetResult();
            p2.Show();


            // sample2
            Shop shop = new Shop();
            
            VehicleBuilder vb1 = new CarBuilder();
            shop.Contruct(vb1);
            vb1.Vehicle.Show();

            VehicleBuilder vb2 = new MotorCycleBuilder();
            shop.Contruct(vb2);
            vb2.Vehicle.Show();

            var vb3 = new ScooterBuilder();
            shop.Contruct(vb3);
            vb3.Vehicle.Show();

            Console.ReadLine();
        }
    }
}
