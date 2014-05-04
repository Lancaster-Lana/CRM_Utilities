using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace FetchXMLBuilder.Business
{
    [Serializable, XmlRoot("attribute")]
    public class Attribute: FetchXMLElement
    {
        #region Fields

        //Entity _parentEntity = null;
        string _aggregate = string.Empty;
        string _attributeAlias = string.Empty;

        //public string Name;

        #endregion

        #region Properties
        /*
        public new Entity Parent
        {
            get { return _parentEntity; }
            set { _parentEntity = value; }
        }
        */

        [XmlAttribute(AttributeName = "aggregate")]
        public string Aggregate
        {
            get { return _aggregate; }
            set { _aggregate = value; }
        }

        [XmlAttribute(AttributeName = "alias")]
        public string AttributeAlias
        {
            get { return _attributeAlias; }
            set { _attributeAlias = value; }
        }

        #endregion

        #region Methods

        public Attribute():this(string.Empty)
        { 
        }

        public Attribute(string name):this( name, null)
        {
        }

        public Attribute(string name, Entity parentEntity)
        {
            Aggregate = string.Empty;
            AttributeAlias = string.Empty;
            Name = name;
           // Parent = parentEntity;
        }          

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("attribute name='" + this.Name + "' ");
           if (this.AttributeAlias.Length > 0)
            sb.Append("alias='" + this.AttributeAlias + "' ");

            return sb.ToString();
        }

        public override string ToXML()
        {
            string text = "<attribute name='" + this.Name + "' ";
            if (this.AttributeAlias.Length > 0)
            {
                text = text + "alias='" + this.AttributeAlias + "' ";
            }
            if (this.Aggregate.Length > 0)
            {
                text = text + "aggregate='" + this.Aggregate + "'";
            }
            return (text + "/>");
        }

        #endregion
    }
 
}

/*
      public Attribute(XmlNode AttributeNode) : this(AttributeNode.Attributes["name"].Value)
        {
            if (AttributeNode.Attributes["aggregate"] != null)
            {
                this.Aggregate = AttributeNode.Attributes["aggregate"].Value;
            }
            if (AttributeNode.Attributes["alias"] != null)
            {
                this.AttributeAlias = AttributeNode.Attributes["alias"].Value;
            }
        }        
*/