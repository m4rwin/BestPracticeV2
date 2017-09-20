using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace L0703_LINQ
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

    class Trainings : IEnumerable<Training>
    {
        Training[] trainings =
        {
            new Training("C#"),
            new Training("ASP.NET"),
            new Training("JavaScript")
        };

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Training>)this).GetEnumerator();
        }

        IEnumerator<Training> IEnumerable<Training>.GetEnumerator()
        {
            return new TrainingsEnumerator(this.trainings);
        }
    }

    class TrainingsEnumerator : IEnumerator<Training>
    {
        int index = -1;
        Training[] trainings;

        public TrainingsEnumerator(Training[] tr) { trainings = tr; }

        public object Current
        {
            get { return this.trainings[index]; }
        }

        Training IEnumerator<Training>.Current
        {
            get
            {
                return this.trainings[this.index];
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
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
            Trainings t = new Trainings();
            var list = t.Where(a => a.Name.Contains("ASP")).Select(b => new
            {
                name = b.Name,
                date = DateTime.Now
            });

            //list.ToList().ForEach(i => Console.WriteLine(i));

            var items = new[] { 1M, 2M, 3M, 4M};
            Array.ForEach(items.Select(i => i * 10).ToArray(), x => Console.WriteLine("Number: " +x));
        }
    }
}
