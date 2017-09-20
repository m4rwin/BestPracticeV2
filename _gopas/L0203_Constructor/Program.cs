using System;

namespace L0203_Constructor
{
    public class Car
    {
        private static int NumberOfCars = 1;
        public readonly int CarId;
        public string Manufacturer { set; get; }
        public int Price { set; get; }

        private Car(string Manufacturer, int PurchasePrice)
        {
            this.Manufacturer = Manufacturer;
            this.Price = PurchasePrice;
            this.CarId = NumberOfCars++;
        }

        public static Car CreateInstance(string Manufacturer, int PurchasePrice)
        {
            if (PurchasePrice < 0) { throw new Exception("Price has to be bigger than 0."); }
            return new Car(Manufacturer, PurchasePrice);
        }

        public override string ToString()
        {
            return string.Format("Manufacturer: {0}, Price: {1}, Id: {2}", this.Manufacturer, this.Price, this.CarId);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Car c = Car.CreateInstance("Skoda", 550000);
            Console.WriteLine(c.ToString());

            Car c2 = Car.CreateInstance("BMW", 1200000);
            Console.WriteLine(c2.ToString());

            //Car c3 = new Car("ABC", -10);
            //Console.WriteLine(c3.ToString());

            Console.ReadLine();
        }
    }
}
