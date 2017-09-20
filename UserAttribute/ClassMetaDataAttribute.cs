using System;

namespace UserAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, AllowMultiple = true)]
    public class ClassMetaDataAttribute : Attribute
    {
        private string author;
        public string Author { get { return author; } }

        public ClassMetaDataAttribute(string name) { author = name; }
    }
}
