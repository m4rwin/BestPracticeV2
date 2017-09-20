using System;
using System.Collections.Generic;

namespace SortComparableCompare
{
    public class Robot : IComparable<Robot>
    {
        public int NumOfProcessors;
        public string Type;
        public string Name;

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", Name, Type, NumOfProcessors);
        }

        public int CompareTo(Robot other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }

    public class CompareByProcessors : IComparer<Robot>
    {
        public int Compare(Robot x, Robot y)
        {
            return x.NumOfProcessors.CompareTo(y.NumOfProcessors);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            List<Robot> robots = new List<Robot>()
            {
                new Robot() { Name = "T500 Deluxe", Type = "Humanoid v1", NumOfProcessors = 120000 },
                new Robot() { Name = "Johnny C.", Type = "Humanoid v2", NumOfProcessors = 550000 },
                new Robot() { Name = "A15 Superstar", Type = "Humanoid v3", NumOfProcessors = 330000000 },
                new Robot() { Name = "R2D2", Type = "Robot v0", NumOfProcessors = 100 }
            };

            robots.Sort(new CompareByProcessors());
            robots.ForEach(Console.WriteLine);

            Console.ReadLine();
        }
    }
}
