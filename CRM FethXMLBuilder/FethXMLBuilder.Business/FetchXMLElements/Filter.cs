using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using Microsoft.VisualBasic.CompilerServices;

namespace FetchXMLBuilder.Business
{
    [Serializable, XmlRoot("filter")]
    public class Filter :FetchXMLElement
    {
        #region Fields
        
        //FetchXMLElement _parent = null; // Filter or Entity - we don't know

        List<Condition> _conditions = new List<Condition>();
        List<Filter> _filters = new List<Filter>();

        FilterType _type = new FilterType();

        #endregion

        #region Properties

        /*
        public new FetchXMLElement Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }*/

        /*
        public override string Name
        {
            get {
                if (this.Parent is Entity)
                   return ((Entity)this.Parent).Filters.IndexOf(this).ToString();
                else if(this.Parent is Filter)
                   return ((Filter)this.Parent).Filters.IndexOf(this).ToString();
               return base.Name;
             }
            set
            {
                base.Name = value;
            }
        }
        */

        //[XmlAttribute("type")]//[XmlEnum("FilterType")]
        [XmlAttribute(AttributeName = "type")]//[XmlEnum("FilterType")]
       // [XmlEnum("FilterType")]
        public FilterType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        [XmlElement("condition")]
        public List<Condition> Conditions
        {
            get
            {
                return _conditions;
            }
            set
            {
                _conditions = value;
            }
        }

        [XmlElement("filter")]
        public List<Filter> Filters
        {
            get
            {
                return _filters;
            }
            set
            {
                _filters = value;
            }
        }

        #endregion

        #region Methods

        public Filter()
        {            
        }

        public Filter(Entity entity, FilterType type):this()
        {
            //Parent = entity;
            Type = type;
        }


        private string SubFiltersToString()
        {
            string text = string.Empty;
            foreach (Filter filter in this.Filters)
            {
                text += "(" + filter.ToString() + ")";
            }
            return text;
        }

        public override string ToString()
        {
            return "filter type='" + this.Type.ToString();
            /*
            string text = string.Empty;
            foreach (Condition condition in this.Conditions)
            {
                if (text.Length > 0)
                {
                    if (this.Type == FilterType.and)
                    {
                        text = text + " and ";
                    }
                    else
                    {
                        text = text + " or ";
                    }
                }
                text = text + condition.ToString();
            }
            if (text.Length != 0)
            {
                text = text + " ";
            }
            if (this.Filters.Count <= 0)
            {
                return text;
            }
            if (this.Type == FilterType.and)
            {
                return (text + "and (" + this.SubFiltersToString() + ") ");
            }
            return (text + "or (" + this.SubFiltersToString() + ") ");
             */ 
        }

        

        public override string ToXML()
        {
            string text = "<filter type='";
            if (this.Type.Equals(FilterType.and))
            {
                text = text + "and'>";
            }
            else
            {
                text = text + "or'>";
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
               /*IEnumerator<Filter> enumerator = Filters.GetEnumerator();
                if (enumerator != null)
                {
                    enumerator.Dispose();
                }*/
            }
            foreach (Condition condition in this.Conditions)
            {
                text = text + condition.ToXML();
            }
            return (text + "</filter>");
        }


        public void AddCondition(Condition condition)
        {
           // condition.Parent = this;
            this.Conditions.Add(condition);
        }

        public void AddFilter(Filter filter)
        {
            //filter.Parent = this;
            this.Filters.Add(filter);
        }

        public void RemoveCondition(Condition condition)
        {
            this.Conditions.Remove(condition);
        }


        #endregion
    }

}

/*
        public Filter(XmlNode FilterNode) : this()
        {
            if ((FilterNode.Attributes["type"] != null) && (Operators.CompareString(FilterNode.Attributes["type"].Value, "or", false) == 0))
            {
                this.Type = FilterType.or;
            }
            XmlNodeList list = FilterNode.SelectNodes("./condition");
            try
            {
                foreach (XmlNode node in list)
                {
                    this.Conditions.Add(new Condition(node));
                }
            }
            finally
            {
              
                IEnumerator enumerator = list.GetEnumerator();
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }
            XmlNodeList list2 = FilterNode.SelectNodes("./filter");
            try
            {
                foreach (XmlNode node2 in list2)
                {
                    this.Filters.Add(new Filter(node2));
                }
            }
            finally
            {
              
                IEnumerator enumerator2 = list2.GetEnumerator();
                if (enumerator2 is IDisposable)
                {
                    (enumerator2 as IDisposable).Dispose();
                }
            }
        }
*/