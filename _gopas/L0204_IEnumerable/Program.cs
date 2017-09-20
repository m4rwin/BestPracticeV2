using System;
using System.Collections;

namespace L0204_IEnumerable
{
    class Training
    {
        public string Name;
        public Training(string name) { Name = name; }

        public override string ToString()
        {
            return Name;
        }
    }

    class Trainings : IEnumerable
    {
        Training[] trainings =
        {
            new Training("C#"),
            new Training("ASP.NET"),
            new Training("JavaScript")
        };

        public IEnumerator GetEnumerator()
        {
            return new TrainingsEnumerator(this.trainings);
        }
    }

    class TrainingsEnumerator : IEnumerator
    {
        int index = -1;
        Training[] trainings;

        public TrainingsEnumerator(Training[] tr) { trainings = tr; }

        public object Current
        {
            get{ return this.trainings[index]; }
        }

        public bool MoveNext()
        {
            index++;
            if (index >= trainings.Length) { return false; }
            return true;
        }

        public void Reset()
        {
            index = -1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Trainings trainings = new Trainings();
            foreach (var item in trainings)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}
