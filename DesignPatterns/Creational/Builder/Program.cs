using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    // Created object -- car
    public class Car
    {
        Dictionary<string, string> car = new Dictionary<string, string>();

        public void Add(string key, string value) => car.Add(key, value);

        public void CarSummary() => car.ToList().ForEach(i => Console.WriteLine($"{i.Key}: {i.Value}"));
    }

    // Builder interface
    public interface IBuilder
    {
        void AddCubicCapacity();
        void AddDoors();
        void AddColor();
        Car GetResult();
    }

    // Builder implementation
    public class Builder1 : IBuilder
    {
        private Car newCar = new Car();

        public void AddColor() => newCar.Add("color", "red");

        public void AddCubicCapacity() => newCar.Add("cubic", "1.5");

        public void AddDoors() => newCar.Add("doors", "5");

        public Car GetResult() => newCar;
    }

    // Director implementation
    public class Director1
    {
        public Car Construct(IBuilder builder)
        {
            builder.AddColor();
            builder.AddCubicCapacity();
            return builder.GetResult();
        }
    }

    // Director implementation
    public class Director2
    {
        public Car Construct(IBuilder builder)
        {
            builder.AddColor();
            builder.AddCubicCapacity();
            builder.AddDoors();
            return builder.GetResult();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Director1 dir1 = new Director1();
            Director2 dir2 = new Director2();

            Car car1 = dir1.Construct(new Builder1());
            car1.CarSummary();

            Car car2 = dir2.Construct(new Builder1());
            car2.CarSummary();

            Console.ReadKey();
        }
    }
}
