using System;

namespace Serialization
{
    [Serializable]
    public class Human
    {
        public string Genre;
        public string Forename;
        public string Surname;
        public int Age;
        public string Proffesion;
        public Human Partner;

        public Human() { }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} Partner: {5}", this.Genre, this.Forename, this.Surname, this.Age, this.Proffesion, this.Partner);
        }
    }
}
