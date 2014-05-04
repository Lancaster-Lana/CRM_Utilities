using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace FetchXMLBuilder.Business
{
    [Serializable, XmlRoot("entity")]
    public class Entity : FetchXMLElement
    {
        #region Fields

        string _name = string.Empty;
        List<Attribute> _attributes = new List<Attribute>();
        List<Filter> _filters = new List<Filter>();
        List<LinkedEntity> _linkedEntities = new List<LinkedEntity>();
        List<Order> _orders = new List<Order>();

        #endregion

        #region Properties

        public bool NoAttributes;
        public bool AllAttributes;

/*
        [XmlAttribute(AttributeName="name")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        
        public new string Name
        {
            get { return _name; }
            set { _name = value; }
        }*/


        //[XmlArray("attribute")]//ElementName = "Attributes")]
       // [XmlArrayItem(typeof(Attribute), ElementName = "attribute")]
        [XmlElement("attribute")]
        public List<Attribute> Attributes
        {
            get { return _attributes; }
            set {_attributes = value; }
        }

        //[XmlArray(ElementName = "Filters")]
        //[XmlArrayItem(typeof(Filter), ElementName = "filter")]
        [XmlElement("filter")]
        public List<Filter> Filters
        {
            get
            {
                return _filters;
            }
            set { _filters = value; }
        }

        //[XmlArray(ElementName = "Link-Entities")]
        //[XmlArrayItem(typeof(LinkedEntity), ElementName = "link-entity")]
        [XmlElement("link-entity")]
        public List<LinkedEntity> LinkedEntities
        {
            get { return _linkedEntities;}
            set { _linkedEntities = value; }
        }

        //[XmlArray(ElementName = "Orders")]
        //[XmlArrayItem(typeof(Order), ElementName = "order")]
        [XmlElement("order")]
        public List<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

        #endregion

        #region Methods

        public Entity():this(string.Empty)
        {
        }

        public Entity(string EntityName)
        {
            this.AllAttributes = false;
            this.NoAttributes = false;
            this.Name = EntityName;
            this.Attributes = new List<Attribute>();
            this.Orders = new List<Order>();
            this.LinkedEntities = new List<LinkedEntity>();
            this.Filters = new List<Filter>();
        }

        /// <summary>
        /// To Show description on tree
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //return this.ToXML();
            StringBuilder sb = new StringBuilder();
            sb.Append("<Query : entity name='" + this.Name + "' >");          
            return sb.ToString();
        }

        /*
        public override string ToXML()
        {
            return Entity.Serialize(this);
        }*/
    


        #region Entity Elements Methods

        public void AddAttribute(Attribute attribute)
        {
            //attribute.Parent = this;
            this.Attributes.Add(attribute);
        }

        public void RemoveAttribute(Attribute attribute)
        {
            this.Attributes.Remove(attribute);
        }        


        public void AddFilter(Filter filter)
        {
            //filter.Parent = this;
            this.Filters.Add(filter);
        }

        public void RemoveFilter(Filter filter)
        {
            this.Filters.Remove(filter);
        }  


        public void AddLinkEntity(LinkedEntity entity)
        {
            //StringBuilder sb = new StringBuilder(FetchXML);
            //sb.Append(filter.ToString());
            //entity.Parent = this;
            this.LinkedEntities.Add(entity);
        }

        public void RemoveLinkedEntity(LinkedEntity linkedEntity)
        {
            this.LinkedEntities.Remove(linkedEntity);
        }

        public void AddOrder(Order order)
        {            
            //order.Parent = this;
            this.Orders.Add(order);
        }

        public void RemoveOrder(Order order)
        {
            this.Orders.Remove(order);
        } 
        

        #endregion

        #endregion


        public static Entity Deserialize(string XmlString)
        {
            //XmlString = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" + XmlString;
            TextReader sr = new StringReader(XmlString);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Entity));
            

            xmlSerializer.UnknownElement += new XmlElementEventHandler(xmlSerializer_UnknownElement);
            xmlSerializer.UnknownAttribute += new XmlAttributeEventHandler(xmlSerializer_UnknownAttribute);

            Entity entity = (Entity)xmlSerializer.Deserialize(sr);
            sr.Close();       

            return entity;
        }

        static void xmlSerializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            try
            { }
            catch (System.Security.SecurityException)
            {
            }
            catch (Exception)
            { }
            //_listOfCustAttrs.Add(e.Attr.Name, new CustomAttribute(e.Attr.Name, e.Attr.GetType(), e.Attr.Value));
        }

        private static void xmlSerializer_UnknownElement(object sender, XmlElementEventArgs e)
        {
           // _listOfCustProps.Add(e.Element.Name, new CustomProperty(e.Element.Name, e.Element.OuterXml));
        }

        public static void Serialize(Entity entity, string fileName )
        {
            string XmlString = string.Empty;//"<?xml version=\"1.0\" encoding=\"utf-16\"?>" + XmlString;
            TextWriter sr = new StreamWriter(fileName);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Entity));            
             xmlSerializer.Serialize(sr, entity);
             sr.Close();
        }
    }


}
/*

      public string ToXML()
        {
            string text = "<entity name='" + this.Name + "'>";
            if (this.AllAttributes)
            {
                text = text + "<all-attributes />";
            }
            if (this.NoAttributes)
            {
                text = text + "<no-attrs />";
            }
            if (!this.AllAttributes & !this.NoAttributes)
            {
                foreach (Attribute attribute in this.Attributes)
                {
                    text = text + attribute.ToXML();
                }
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
                IEnumerator<Order> enumerator2 = Order.g;
                if (enumerator2 != null)
                {
                    enumerator2.Dispose();
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
                
                IEnumerator<Filter> enumerator3 = Filters.GetEnumerator();
                if (enumerator3 != null)
                {
                    enumerator3.Dispose();
                }
            }
            foreach (LinkedEntity entity in this.LinkedEntities)
            {
                text = text + entity.ToXML();
            }
            return (text + "</entity>");
        }
*/

/*
public class Entity
{
    string _name;
    ArrayList _attributes;//List<Attributes>
    Filter _filter;
    ArrayList _linkEntities;//List<Entity>

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public ArrayList Attributes
    {
        get { return _attributes; }
        set { _attributes = value; }
    }

    public Filter Filter
    {
        get { return _filter; }
        set { _filter = value; }
    }

    public ArrayList LinkEntities
    {
        get { return _linkEntities; }
        set { _linkEntities = value; }
    }
        
    public string Entity(string name)
    {
        Name = name;
    }

       
}*/

/*

        public Entity(XmlNode EntityNode)
            : this(EntityNode.Attributes["name"].Value)
        {
            if (EntityNode.SelectSingleNode("./all-attributes") != null)
            {
                this.AllAttributes = true;
            }
            if (EntityNode.SelectSingleNode("./no-attributes") != null)
            {
                this.NoAttributes = true;
            }
            XmlNodeList list = EntityNode.SelectNodes("./attribute");
            try
            {
                foreach (XmlNode node in list)
                {
                    this.Attributes.Add(new Attribute(node));
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
            XmlNodeList list4 = EntityNode.SelectNodes("./order");
            try
            {
                foreach (XmlNode node4 in list4)
                {
                    this.Orders.Add(new Order(node4));
                }
            }
            finally
            {
                IEnumerator enumerator2 = list4.GetEnumerator();
                if (enumerator2 is IDisposable)
                {
                    (enumerator2 as IDisposable).Dispose();
                }
            }
            XmlNodeList list3 = EntityNode.SelectNodes("./link-entity");
            try
            {
                foreach (XmlNode node3 in list3)
                {
                    this.LinkedEntities.Add(new LinkedEntity(node3));
                }
            }
            finally
            {
                IEnumerator enumerator3 = list3.GetEnumerator();
                if (enumerator3 is IDisposable)
                {
                    (enumerator3 as IDisposable).Dispose();
                }
            }
            XmlNodeList list2 = EntityNode.SelectNodes("./filter");
            try
            {
                foreach (XmlNode node2 in list2)
                {
                    this.Filters.Add(new Filter(node2));
                }
            }
            finally
            {
                IEnumerator enumerator4 = list2.GetEnumerator();
                if (enumerator4 is IDisposable)
                {
                    (enumerator4 as IDisposable).Dispose();
                }
            }
        }
*/