namespace L0606_ObjectInitializers
{
    class Department
    {
        public string Name { get; set; }
        public int Number { get; set; }
    }

    class Employee
    {
        public string Name { set; get; }
        public string Genre { get; set; }
        public Department Department { set; get; }

        // b = 0 je nepovinny parametr
        // kdyz tuto metodu pretizim, bude zavolana metoda bez nepovinneho parametru
        public void M1(int a, int b = 0) { }
        public void M1(int a) { }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Employee e = new Employee()
            {
                Name = "Martin Hromek",
                Genre = "Male",
                Department = new Department()
                {
                    Name = "IT",
                    Number = 008
                }
            };

            e.M1(5);
        }
    }
}
