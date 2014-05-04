using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FetchXMLBuilder.Business
{
    public class FetchXMLElement
    {
       // FetchXMLElement _parent = null;
        string _name = string.Empty;

     
        [XmlAttribute(AttributeName="name")]
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        } 
       
        /*
        public FetchXMLElement Parent 
        {
            get { return _parent;}
            set { _parent = value; }
        }
     */

        public string Description
        {
            get { return this.ToString(); }
        }

        public virtual string ToXML()
        {            
            return this.ToString();
        }


        /*
        public int Index
        {
            get { return this.}
        }*/
    }
}
