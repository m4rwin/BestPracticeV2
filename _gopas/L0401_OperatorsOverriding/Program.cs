using System;

namespace L0401_OperatorsOverloading
{
    class Date
    {
        public int Year, Month, Day;

        public Date(int d, int m, int y)
        {
            Day = d;
            Month = m;
            Year = y;
        }

        public override string ToString()
        {
            return string.Format("{0:D2}.{1:D2}.{2:D4}", Day, Month, Year);
        }

        public Date AddDays(int days)
        {
            Day += days;
            return this;
        }

        public static Date operator +(Date d, int days)
        {
            d.Day += days;

            if (d.Day > 31) { d.Month++; d.Day = d.Day-31; }

            return d;
        }

        public static Date operator ++(Date date)
        {
            // date.Day++; // spatne reseni, unarni operator musi vracet novou instanci a ne upravovat tu stavajici
            return new Date(date.Day+1, date.Month, date.Year);
        }

        public static bool operator ==(Date d1, Date d2)
        {
            if (d1.Day == d2.Day && d1.Month == d2.Month && d1.Year == d2.Year) return true;
            return false;
        }

        public static bool operator !=(Date d1, Date d2)
        {
            if (d1.Day != d2.Day || d1.Month != d2.Month || d1.Year != d2.Year) return true;
            return false;
        }

        //public static explicit operator string(Date d)
        //{
        //    return d.ToString();
        //}

        public static implicit operator string (Date d)
        {
            return d.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Date d = new Date(25, 10, 2015);
            Console.WriteLine(d);

            d = d + 14;
            Console.WriteLine(d);

            //d++;
            Console.WriteLine((++d).Day);
            Console.WriteLine((d++).Day);

            // ==
            Date d1 = new Date(25, 10, 2015);
            Date d2 = new Date(25, 10, 2015);
            Console.WriteLine("{0} == {1} ----> {2}", d1, d2, d1==d2);

            string s = d1;

            Console.ReadLine();
        }
    }
}
