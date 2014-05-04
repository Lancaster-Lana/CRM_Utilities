using System;

namespace CustomizationsComparator
{
    [System.SerializableAttribute()]
    public class OriginalElement
    {
        string _name = String.Empty;

        public string Name
        {
            get { return _name; }
            set {_name = value; }
        }

        public OriginalElement()
        { 
        }

        public OriginalElement(string name)
        {
            Name = name;
        }
    }
}
