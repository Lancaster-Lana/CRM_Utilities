using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using Microsoft.VisualBasic.CompilerServices;

namespace FetchXMLBuilder.Business
{
    [Serializable, XmlRoot("order")]
    public class Order : FetchXMLElement
    {
        #region Fields

        //Entity _parent = null;
        
        string _attributeName = string.Empty;
        bool _descending = false;
        
        #endregion

        #region Properties
     
        /*
        public new Entity Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
        */

        [XmlAttribute(/*Type = typeof(bool),*/ AttributeName = "descending")]
        public bool Descending
        {
            get { return _descending; }
            set { _descending = value; }
        }

        [XmlAttribute(AttributeName = "attribute")]       
        public string AttributeName
        {
            get { return _attributeName; }
            set { _attributeName = value; }
        }

        #endregion

        #region Methods

        public Order()
        {
            this.Descending = false;
        }

        public Order(string attributeName, bool descending)
        {            
            AttributeName = attributeName;
            Descending = descending;
        }


        public override string ToString()
        {
            if (this.Descending)
            {
                return ("order <D> attribute='" + AttributeName + "'" );
            }
            return ("order <A> attribute='" + AttributeName + "'");
            // "order attribute='" + order.AttributeName + "'" + " descending='" + order.Descending.ToString() + "'"
        }

        public string ToXML()
        {
            string text = "<order attribute='" + AttributeName /*this.Attribute*/ + "' ";
            if (this.Descending)
            {
                text = text + "descending='true' ";
            }
            return (text + "/>");
        }

        #endregion

    }

}

/*
 
        public Order(XmlNode OrderNode)
        {
            Descending = false;
            //this.Attribute
            //Parent.Name = OrderNode.Attributes["attribute"].Value;
            AttributeName = OrderNode.Attributes["attribute"].Value;
            if ((OrderNode.Attributes["descending"] != null) && ((Operators.CompareString(OrderNode.Attributes["descending"].Value, "true", false) == 0) | (Operators.CompareString(OrderNode.Attributes["descending"].Value, "1", false) == 0)))
            {
                this.Descending = true;
            }
        }
*/