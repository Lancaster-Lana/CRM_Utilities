using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FetchXMLBuilder.Business
{
    [XmlRoot("value")]
    public class Value
    {
        public static implicit operator string(Value fromValue)
        {
            return fromValue.Content;
        }
        /*
        List<string> _values = new List<string>();

        [XmlElement("value")]
        public List<string> Values
        {
            get { return _values; }
            set { _values = value; }

        }
        public Value()
        { 
        }*/

        string _content = string.Empty;

        [XmlElement("value")]
        public string Content
        {
            get { return _content; }
            set { _content = value; }

        }
        public Value()
        {
        }
    }
}
