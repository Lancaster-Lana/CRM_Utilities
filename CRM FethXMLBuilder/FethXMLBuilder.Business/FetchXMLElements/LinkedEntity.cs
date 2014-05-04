using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using Microsoft.VisualBasic.CompilerServices;

namespace FetchXMLBuilder.Business
{
    [Serializable, XmlRoot("link-entity")]
    public class LinkedEntity : Entity
    {
        #region Fields

       // Entity _parentEntity = null; // Entity or Linked Entity - we don't know

        string _linkAlias = string.Empty;
        string _linkFrom = string.Empty;
        string _linkTo = string.Empty;
        string _linkType = string.Empty;

        #endregion

        #region Properties   

        [XmlAttribute(AttributeName = "alias")]
        public string LinkAlias
        {
            get { return _linkAlias; }
            set { _linkAlias = value; }
        }

        [XmlAttribute(AttributeName = "from")]
        public string LinkFrom
        {
            get { return _linkFrom; }
            set { _linkFrom = value; }
        }

        [XmlAttribute(AttributeName = "to")]
        public string LinkTo
        {
            get { return _linkTo; }
            set { _linkTo = value; }
        }

        [XmlAttribute(AttributeName = "link-type")]
        public string LinkType
        {
            get { return _linkType; }
            set { _linkType = value; }
        }

        /*
        public new Entity Parent
        {
            get { return _parentEntity; }
            set { _parentEntity = value; }
        }
        */

        #endregion

        #region Methods

        public LinkedEntity():this(string.Empty)
        { 
        }

        public LinkedEntity(string Name)
            : base(Name)
        {
        }


        public override string ToString()
        {
           StringBuilder sb = new StringBuilder();
            sb.Append("link-entity name='" + this.Name + "'");
            sb.Append(" from='" + this.LinkFrom + "'");
            sb.Append(" to='" + this.LinkTo + "'");
            sb.Append(" link-type='" + this.LinkType + "'");

            return sb.ToString();
            /*
            if ((this.LinkAlias != null) && (this.LinkAlias.Length > 0))
            {
                return this.LinkAlias;
            }
            return this.Name;
            */
        }

        public override string ToXML()
        {
            string text = "<link-entity name='" + this.Name + "' ";
            if ((this.LinkFrom != null) && (this.LinkFrom.Length > 0))
            {
                text = text + "from='" + this.LinkFrom + "' ";
            }
            if ((this.LinkTo != null) && (this.LinkTo.Length > 0))
            {
                text = text + "to='" + this.LinkTo + "' ";
            }
            if ((this.LinkAlias != null) && (this.LinkAlias.Length > 0))
            {
                text = text + "alias='" + this.LinkAlias + "' ";
            }
            string linkType = this.LinkType;
            if (Operators.CompareString(linkType, "natural", false) == 0)
            {
                text = text + "link-type='natural' ";
            }
            else if (Operators.CompareString(linkType, "inner", false) == 0)
            {
                text = text + "link-type='inner' ";
            }
            else if (Operators.CompareString(linkType, "outer", false) == 0)
            {
                text = text + "link-type='outer' ";
            }
            text = text + ">";
            if (base.AllAttributes)
            {
                text = text + "<all-attributes />";
            }
            if (!base.AllAttributes & base.NoAttributes)
            {
                text = text + "<no-attrs />";
            }
            if (!base.AllAttributes & !base.NoAttributes)
            {
                foreach (Attribute attribute in this.Attributes)
                {
                    text = text + attribute.ToXML();
                }
            }
            try
            {
                foreach (Filter filter in this.Filters)
                {
                    text = text + filter.ToXML();
                }
            }
            finally
            {
                /*
                IEnumerator<Filter> enumerator2 = Filters.GetEnumerator();
                if (enumerator2 != null)
                {
                    enumerator2.Dispose();
                }*/
            }
            try
            {
                foreach (Order order in this.Orders)
                {
                    text = text + order.ToXML();
                }
            }
            finally
            {
                /*
                IEnumerator<Order> enumerator3 = Orders.GetEnumerator();
                if (enumerator3 != null)
                {
                    enumerator3.Dispose();
                }*/
            }
            foreach (LinkedEntity entity in this.LinkedEntities)
            {
                text = text + entity.ToXML();
            }
            return (text + "</link-entity>");
        } 

        #endregion

    }
      
}

/*
        public LinkedEntity(XmlNode LinkedEntityNode)
            : base(LinkedEntityNode)
        {
            if (LinkedEntityNode.Attributes["to"] != null)
            {
                this.LinkTo = LinkedEntityNode.Attributes["to"].Value;
            }
            if (LinkedEntityNode.Attributes["from"] != null)
            {
                this.LinkFrom = LinkedEntityNode.Attributes["from"].Value;
            }
            if (LinkedEntityNode.Attributes["alias"] != null)
            {
                this.LinkAlias = LinkedEntityNode.Attributes["alias"].Value;
            }
            if (LinkedEntityNode.Attributes["link-type"] != null)
            {
                this.LinkTo = LinkedEntityNode.Attributes["link-type"].Value;
            }
        }
*/