using System;

namespace Data
{
    public enum GENRE
    {
        Male,
        Female
    }

    public class Human
    {
        #region Properties
        public string Name { private set; get; }
        public int Age { private set; get; }
        public GENRE Genre { private set; get; }
        #endregion

        #region Fields
        private int ID;
        public string Nick;
        #endregion

        #region C-tor
        public Human(string name, int age, GENRE genre)
        {
            Name = name;
            Age = age;
            Genre = genre;
            ID = Convert.ToInt16(DateTime.Now.ToString("yyyyMMddmmhhss"));
        }
        #endregion

        #region Methods
        public int GetBirthYear() { return DateTime.Now.Year - Age; }
        private string Today() { return DateTime.Now.ToString("yyyy-mm-dd"); }
        #endregion

        #region Events
        public delegate void GetInfo(string name);
        public event GetInfo MyEvent;
        #endregion
    }
}
