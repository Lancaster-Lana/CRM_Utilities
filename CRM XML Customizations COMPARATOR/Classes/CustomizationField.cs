using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace CustomizationsComparator
{
    [SerializableAttribute(), XmlRoot()]
    public enum FieldDataFormat
    {
        Lookup, Owner,
        Decimal, Integer, Float, Money,
        //DateTime, TimeZone, 
        date, time,
        Boolean, BooleanRadio, BooleanCheckbox,
        //PartyList:
        faxpartylist, emailpartylist,
        Picklist, Status, State, Option,
        Regarding, Text, TextArea, Memo,
        TickerSymbol,
        Email, IFrame, Url
    }

    #region Additional Fields Propreties (Items/Options)   Classes

    [XmlRoot()]
    public class DisplayName//: IComparable, IComparer
    {
        string _languagecode = "1033";
        string _description = "";

        [XmlAttribute("description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        [XmlAttribute("languagecode")]
        public string LanguageCode
        {
            get { return _languagecode; }
            set { _languagecode = value; }
        }

        DisplayName()
        {
        }

        DisplayName(string languagecode, string description)
        {
            LanguageCode = languagecode;
            Description = description;
        }

       /*
        public int CompareTo(object obj)
        {
            DisplayName displname = obj as DisplayName;
            return Compare(this, displname);
        }

        public int Compare(Object obj1, Object obj2)
        {
            DisplayName displname1 = obj1 as DisplayName;
            DisplayName displname2 = obj2 as DisplayName;
            
            int codesCMP = displname1.LanguageCode.CompareTo(displname2.LanguageCode);
            if(codesCMP == 0)
            {
                    if (CompareHelper.CompareLabels(displname1.Description, displname2.Description))
                        return 0;
                    return displname1.Description.CompareTo(displname2.Description);
            }
            return -1;
        }
        */

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            DisplayName displname1 = this;
            DisplayName displname2 = obj as DisplayName;
            if ((displname1 != null) && (displname2 != null))
            return (displname1.Description.Equals(displname2.Description) &&
                    displname1.LanguageCode.Equals(displname2.LanguageCode));
        return false;
        }

        public override string ToString()
        {
            string xmlString = "<displayname description=\"" + this.Description +"\" languagecode=\"" + this.LanguageCode +"\" />";
            return xmlString;
        }

        /// <summary>
        /// Return Xml string of tag
        /// </summary>
        /// <param name="tagname">"displayname"|"label"</param>
        /// <returns></returns>
        public string ToString(string tagname)
        {
            string xmlString = "<" + tagname + " description=\"" + this.Description + "\" languagecode=\"" + this.LanguageCode + "\" />";
            return xmlString;
        }
    }

    [XmlRoot("option")]
    public class Option //: IComparable, IComparer
    {
        int _value = 0;
        List<DisplayName> _labels = new List<DisplayName>();
        //SortedList<string, int> _labels = new  SortedList<string, int>();

        [XmlAttribute(AttributeName = "value")]
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        [XmlArray(ElementName = "labels")]
        [XmlArrayItem(typeof(DisplayName), ElementName = "label")]
        public List<DisplayName> Labels
        {
            get { return _labels; }
            set { _labels = value; }
        }

        Option()
        {
        }

        Option(int value, List<DisplayName> labels)
        {
            Value = value;
            Labels = labels;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Option opt1 = this;
            Option opt2 = obj as Option;
            if ((opt1 != null) && (opt2 != null))
                return (CompareHelper.CompareLabels(opt1.Labels, opt2.Labels)
                    && opt1.Value.Equals(opt2.Value));
            else if ((opt1 == null) && (opt2 == null))
                return true;
            return false;
        }


        public override string ToString()
        {
            string xmlString = "<option>";
            foreach (DisplayName label in Labels)
            {
                xmlString += label.ToString("label");                
            }
            xmlString += "</option>";                           
                               
            return xmlString;
        }

    /*
        public int CompareTo(object obj)
        {
            Option opt = obj as Option;
            if ((this.Value == opt.Value) && (this.Labels.Equals(opt.Labels)))
                return 0;
            if (this.Value > opt.Value)
                return 1;
            return -1;
        }

        public int Compare(Object obj1, Object obj2)
        {
            Option opt1 = obj1 as Option;
            Option opt2 = obj2 as Option;

            int valuesCMP = opt1.Value.CompareTo(opt2.Value);
            if (valuesCMP == 0)
            {
                if (opt1.Labels.ToArray().Equals(opt2.Labels.ToArray()))
                    return 0;
                else
                {
                    Comparer cmp = new Comparer(CultureInfo.InvariantCulture);
                    int isCMP = cmp.Compare(opt1, opt2);
                    return isCMP;
                }
            }


            return valuesCMP;
        }
 */ 

    }

    [XmlRoot()]
    public class Status
    {
        int _value = 0;
        int _state = 0;
        List<DisplayName> _labels = new List<DisplayName>();

        [XmlAttribute(AttributeName = "value")]
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        [XmlAttribute(AttributeName = "state")]
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }

        [XmlArray(ElementName = "labels")]
        [XmlArrayItem(typeof(DisplayName), ElementName = "label")]
        public List<DisplayName> Labels
        {
            get { return _labels; }
            set { _labels = value; }
        }

        Status() { }
        Status(int value, int state, List<DisplayName> labels)
        {
            Value = value;
            State = state;
            Labels = labels;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Status status1 = this;
            Status status2 = obj as Status;

            return (CompareHelper.CompareLabels(status1.Labels, status2.Labels) &&
                    status1.Value.Equals(status2.Value) &&
                    status1.State.Equals(status2.State));
        }

        public override string ToString()
        {
            string xmlString = "<status value=\"" + this.Value + "\" state=\"" + this.State +"\">";
            foreach (DisplayName label in Labels)
            {
                xmlString += label.ToString("label");
            }
            xmlString += "</status>";

            return xmlString;
        }
    }

    [XmlRoot]
    public class State
    {
        int _value = 0;
        int _defaultstatus = 0;
        string _invariantname = "";
        List<DisplayName> _labels = new List<DisplayName>();

        [XmlAttribute(AttributeName = "value")]
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        [XmlAttribute(AttributeName = "defaultstatus")]
        public int DefaultStatus
        {
            get { return _defaultstatus; }
            set { _defaultstatus = value; }
        }

        [XmlAttribute(AttributeName = "invariantname")]
        public string Invariantname
        {
            get { return _invariantname; }
            set { _invariantname = value; }
        }

        [XmlArray(ElementName = "labels")]
        [XmlArrayItem(typeof(DisplayName), ElementName = "label")]
        public List<DisplayName> Labels
        {
            get { return _labels; }
            set { _labels = value; }
        }
        State() { }

        State(int value, int defaultstatus, string invariantname, List<DisplayName> labels)
        {
            Value = value;
            DefaultStatus = defaultstatus;
            Invariantname = invariantname;
            Labels = labels;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            State state1 = this;
            State state2 = obj as State;

            return (CompareHelper.CompareLabels(state1.Labels, state2.Labels) &&
                    state1.Value.Equals(state2.Value) &&
                    state1.Invariantname.Equals(state2.Invariantname) &&
                    state1.DefaultStatus.Equals(state2.DefaultStatus));
        }

        public override string ToString()
        {
            string xmlString = "<state value=\"" + this.Value + "\" defaultstatus=\"" + this.DefaultStatus + "\" invariantname=\"" + this.Invariantname + "\" >";
            foreach (DisplayName label in Labels)
            {
                xmlString += label.ToString("label");
            }
            xmlString += "</state>";

            return xmlString;
        }
    }


    /// <summary>
    /// Class to list unknown attributes of analizing element(attribute|field).
    /// </summary>
    public class CustomAttribute
    {
        string _attrName;
        string _value;
        Type _attrType;

        public string Name
        {
            get { return _attrName; }
            set { _attrName = value; }
        }

        public Type AttributeType
        {
            get { return _attrType; }
            set { _attrType = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public CustomAttribute()
        {
            Name = "";
            Value = "";
            AttributeType = typeof(string);
        }

        public CustomAttribute(string name, Type type, string value)
        {
            Name = name;
            AttributeType = type;
            Value = value;
        }

        public override string ToString()
        {
            return this.Name + " = " + this.Value;
        }

    }

    /// <summary>
    /// Class to list unknown properties of analizing element(attribute|field).
    /// </summary>
    public class CustomProperty
    {
        string _cstname;
        string _xmlstring;

        public string Name
        {
            get { return _cstname; }
            set { _cstname = value; }
        }
        public string XMLContent
        {
            get { return _xmlstring; }
            set { _xmlstring = value; }
        }

        public CustomProperty()
        {
            Name = String.Empty;
            XMLContent = String.Empty;
        }

        public CustomProperty(string name, string xml)
        {
            Name = name;
            XMLContent = xml;
        }

        public override bool Equals(object obj)
        {
            CustomProperty cstProp2 = obj as CustomProperty;
            if((cstProp2 == null)&&(this != null))
                return false;
            return (this.XMLContent.Equals(cstProp2.XMLContent)&& this.Name.Equals(cstProp2.Name));
        }

        public override string ToString()
        {
            return Name + " = " + XMLContent;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

     #endregion

     /// <summary>
    ///Field class corresponding to attributes types: owner, lookup,  customer,primarykey, uniqueidentifier,
    /// </summary>
    [Serializable, XmlRoot("field")]
    public class simplefield
    {
        protected string _name = "";
        protected string _requiredlevel = "";
        protected string _defaultvalue = "";//for int, float, etc. number system types        
        protected List<DisplayName> _displaynames = new List<DisplayName>();        
       
        [XmlAttribute(AttributeName="name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        [XmlAttribute(AttributeName = "requiredlevel")]
        public string Requiredlevel
        {
            get { return _requiredlevel; }
            set { _requiredlevel = value; }
        }
        
        [XmlAttribute(AttributeName = "defaultvalue")]        
        public string DefaultValue
        {
            get { return _defaultvalue; }
            set { _defaultvalue = value; }
        }

        [XmlArray(ElementName ="displaynames")]
        [XmlArrayItem(typeof(DisplayName), ElementName = "displayname")]//, 
        public List<DisplayName> DisplayNames        
        {
            get { return _displaynames; }
            set { _displaynames = value; }
        }

        #region  Non serializable variables and properties.

        protected SortedList<string, CustomProperty> _customProperties = new SortedList<string, CustomProperty>();
        protected SortedList<string, CustomAttribute> _customAttributes = new SortedList<string, CustomAttribute>();

        private static SortedList<string, CustomProperty> _listOfCustProps = new SortedList<string, CustomProperty>();
        private static SortedList<string, CustomAttribute> _listOfCustAttrs = new SortedList<string, CustomAttribute>();

        /// <summary>
        /// Get type from attributes node
        /// </summary>
        /*  
         * //protected string _attributetype = "NText";
        private string AttributeType
        {
            get { return _attributetype; }
            set { _attributetype = value; }
        }*/

        /// <summary>
        ///List of unknown attributes of the analizing field.
        /// </summary>
        [XmlIgnore]
        public SortedList<string, CustomAttribute> CustomAttributes
        {
            get { return _customAttributes; }
            set { _customAttributes = value; }
        }

        /// <summary>
        ///List of unknown properties of the analizing field.
        /// </summary>
        [XmlIgnore]
        public SortedList<string, CustomProperty> CustomProperties
        {
            get { return _customProperties; }
            set { _customProperties = value; }
        }


        /// <summary>
        /// It is property name for all custom attributes of field (a list of custom attributes)
        /// </summary>
        /// <returns></returns>
        public static string GetCustomAttributeCollectionName()
        {
            return "CustomAttributes";
        }

        /// <summary>
        /// It is property name for all custom properties of field (a list of custom properties )
        /// </summary>
        /// <returns></returns>
        public static string GetCustomPropertyCollectionName()
        {
            return "CustomProperties";
        }

        #endregion

        public simplefield()
        {
            
        }
      
        public simplefield(string name, string requiredlevel, string defaultvalue, List<DisplayName> displaynames)
        {
            Name = name;
            Requiredlevel = requiredlevel;
            DefaultValue = defaultvalue;
            DisplayNames = displaynames;
        }

        public virtual bool CompareTo(simplefield toField, out DifferPropertiesDictionary diffList)
        {               
            Type tp = typeof(simplefield);
            return CompareHelper.GetDifferProperties(this, toField, tp, out diffList);
        }

        public static object Load(string XmlString, string attrTypeName)
        {
            Type fieldType = CompareHelper.GetFieldTypeByAttributeTypeName(attrTypeName);
            return Deserialize(XmlString, fieldType);
        }        

        public static object Deserialize(string XmlString, Type fieldAttrType)
        {           
            try{
                XmlString = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" + XmlString;
                TextReader sr = new StringReader(XmlString);
                var xmlSerializer = new XmlSerializer(fieldAttrType);

                //2. Handle custom nodes
                _listOfCustAttrs.Clear();
                _listOfCustProps.Clear();
                xmlSerializer.UnknownElement += new XmlElementEventHandler(xmlSerializer_UnknownElement);
                xmlSerializer.UnknownAttribute += new XmlAttributeEventHandler(xmlSerializer_UnknownAttribute);

                object field = xmlSerializer.Deserialize(sr);
                foreach (string propName in _listOfCustProps.Keys)
                    ((simplefield)field).CustomProperties.Add(propName, _listOfCustProps[propName]);
                foreach (string attrName in _listOfCustAttrs.Keys)
                    ((simplefield)field).CustomAttributes.Add(attrName, _listOfCustAttrs[attrName]);
                return field;            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            return null;
        }

        static void xmlSerializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            _listOfCustAttrs.Add(e.Attr.Name, new CustomAttribute(e.Attr.Name, e.Attr.GetType(), e.Attr.Value));     
        }

        private static void xmlSerializer_UnknownElement(object sender, XmlElementEventArgs e)
        {            
            _listOfCustProps.Add(e.Element.Name, new CustomProperty(e.Element.Name, e.Element.OuterXml));            
        }

    }

    /// <summary>
    /// Field class corresponding to attribute datetime
    /// </summary>
    [Serializable, XmlRoot("field")]
    public class DateTimeField : simplefield
    {
        protected string _format;//date, time, long, short

        [XmlAttribute(AttributeName = "format")]
        public string Format
        {
            get { return _format; }
            set { _format = value; }
        }


        public DateTimeField() : base()
        {
        }

        public DateTimeField(string name, string requiredlevel, string defaultvalue,  List<DisplayName> displaynames, string format)
            : base(name, requiredlevel, defaultvalue, displaynames)
        {
            _format = format;
        }

    }

    /// <summary>
    /// Field class corresponding to attributes: nvarchar, ntext
    /// </summary>
    [Serializable, XmlRoot("field")]
    public class TextField : simplefield
    {
        protected int _maxlength = 0;
        protected /*FieldDataType*/string _format;

        [XmlAttribute("maxlength")]
        public int Maxlength
        {
            get { return _maxlength; }
            set { _maxlength = value; }
        }
        [XmlAttribute(AttributeName = "format")]
        public string Format
        {
            get { return _format; }
            set { _format = value; }
        }// text, textarea, memo


        public TextField() : base()
        {
        }
        public TextField(string name, string requiredlevel, string defaultvalue, List<DisplayName> displaynames, int maxlength, string format)
            : base(name, requiredlevel, defaultvalue, displaynames)
        {
            _maxlength = maxlength;
            _format = format;
        }
    }

    /// <summary>
    /// Field class corresponding to attributes: decimal, int, float
    /// </summary>
    [Serializable, XmlRoot("field")]
    public class DecimalField : simplefield
    {
        private decimal _minvalue = 0;
        private decimal _maxvalue = 0;
        private decimal _accuracy = 0;
        private decimal _source = 0;

        [XmlAttribute(AttributeName = "minvalue")]
        public decimal MinValue
        {
            get { return _minvalue; }
            set { _minvalue = value; }
        }

        [XmlAttribute(AttributeName = "maxvalue")]
        public decimal MaxValue
        {
            get { return _maxvalue; }
            set { _maxvalue = value; }
        }

        [XmlAttribute(AttributeName = "accuracy")]
        public decimal Accuracy
        {
            get { return _accuracy; }
            set { _accuracy = value; }
        }

        [XmlAttribute(AttributeName = "source")]
        public decimal Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public DecimalField(): base()
        { }
        public DecimalField(string name, string requiredlevel, string defaultvalue, List<DisplayName> displaynames, decimal minvalue, decimal maxvalue, decimal accuracy, decimal source)
            : base(name, requiredlevel, defaultvalue, displaynames)
        {
            MinValue = minvalue;
            MaxValue = maxvalue;
            Accuracy = accuracy;
            Source = source;
        }
    }

    /// <summary>
    /// Field class corresponding to attribute type: partylist
    /// </summary>
    [Serializable, XmlRoot("field")]
    public class PartyListField : simplefield
    {
        private string _lookupclass;
        private string _lookupstyle = "multi";
        private string _lookuptypes; //List< enum{faxpicklist, emailpicklist}>

        [XmlAttribute(AttributeName = "lookupclass")]
        public string LookupClass
        {
            get { return _lookupclass; }
            set { _lookupclass = value; }
        }
        [XmlAttribute(AttributeName = "lookupstyle")]
        public string LookupStyle
        {
            get { return _lookupstyle; }
            set { _lookupstyle = value; }
        }
        [XmlAttribute(AttributeName = "lookuptypes")]
        public string LookupTypes
        {
            get { return _lookuptypes; }
            set { _lookuptypes = value; }
        }

        public PartyListField() : base()
        {
        }

        public PartyListField(string name, string requiredlevel, string defaultvalue, List<DisplayName> displaynames, string lookupclass, string lookupstyle, string lookuptypes)
            : base(name, requiredlevel, defaultvalue, displaynames)
        {
            LookupClass = lookupclass;
            LookupStyle = lookupstyle;
            LookupTypes = lookuptypes;
        }
    }

    #region Optional Fields Classes

    [SerializableAttribute()]
    public class OptionsList 
    {
        protected int _nextvalue = 0;
        protected List<Option> _options = new List<Option>();

        [XmlAttribute(AttributeName = "nextvalue")]
        public int NextValue
        {
            get { return _nextvalue; }
            set { _nextvalue = value; }
        }
             
        [XmlElement("option")]
        public List<Option> Options
        {
           get{ return _options;}
            set{_options = value;}
        }
  
        public OptionsList()
        { 
        }

        public OptionsList(int nextvalue, List<Option> options)
        {
            NextValue = nextvalue;
            Options = options;
        }

        public IEnumerator GetEnumerator()
        {
            return Options.GetEnumerator();            
        }

        public override bool Equals(object obj)
        {
            OptionsList secondOptLst = obj as OptionsList;
            if (secondOptLst != null)
                return (this.NextValue.Equals(secondOptLst.NextValue)&&
                    CompareHelper.EqualOptionalLists(this.Options, secondOptLst.Options));
            if ((this == null) && (secondOptLst == null)) 
                return true;

            return false;
        }

        public override string ToString()
        {
            string optionsStr = "nextvalue=" + this.NextValue.ToString() + "\r\n";          
            IEnumerator ien = this.GetEnumerator();
            while (ien.MoveNext())
                optionsStr += ien.Current.ToString() + "\r\n";

            return optionsStr;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

    [SerializableAttribute()]
    public class StatusesList
    {
        protected int _nextvalue = 0;
        protected List<Status> _statuses = new List<Status>();

        [XmlAttribute(AttributeName = "nextvalue")]
        public int NextValue
        {
            get { return _nextvalue; }
            set { _nextvalue = value; }
        }

        [XmlElement("status")]
        public List<Status> Statuses
        {
            get { return _statuses; }
            set { _statuses = value; }
        }

        public StatusesList()
        {
        }

        public StatusesList(int nextvalue, List<Status> statuses)
        {
            NextValue = nextvalue;
            Statuses = statuses;
        }

        public IEnumerator GetEnumerator()
        {
            return Statuses.GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            StatusesList secondStatusesLst = obj as StatusesList;
            if (secondStatusesLst != null)
                return (this.NextValue.Equals(secondStatusesLst.NextValue) &&
                    CompareHelper.EqualOptionalLists(this.Statuses, secondStatusesLst.Statuses));
            if ((this == null) && (secondStatusesLst == null))
                return true;

            return false;
        }

        public override string ToString()
        {
            string statusesStr = "nextvalue=" + this.NextValue.ToString() + "\r\n";
            IEnumerator ien = this.GetEnumerator();
            while (ien.MoveNext())
                statusesStr += ien.Current.ToString() + "\r\n";

            return statusesStr;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

    [Serializable, XmlRoot("field")]
    public class PickListField : simplefield
    {                    
        private OptionsList _options = new OptionsList();
        
        [XmlElement(typeof(OptionsList), ElementName="options")]
        public OptionsList Options
        {
            get { return _options; }
            set { _options = value; }
        }
                
        public PickListField() : base()
        {
        }

        public PickListField(string name, string requiredlevel, string defaultvalue, List<DisplayName> displaynames, OptionsList options)
            : base(name, requiredlevel, defaultvalue, displaynames)
        {          
            Options = options;
        }
    }

    /// <summary>
    /// Field class corresponding to attribute type: status
    /// </summary>
    [Serializable, XmlRoot("field")]
    public class StatusField : simplefield
    {
        
        private /*List<Status>*/ StatusesList _statuses;

       //[XmlArray(ElementName = "statuses")]
       // [XmlArrayItem(typeof(Status), ElementName = "status")]
        [XmlElement(typeof(StatusesList), ElementName = "statuses")]
        public StatusesList Statuses
        {
            get { return _statuses; }
            set { _statuses = value; }
        }

        public StatusField(): base()
        {
        }
        public StatusField(string name, string requiredlevel, string defaultvalue, int nextvalue, List<DisplayName> displaynames, /*List<Status>*/StatusesList statuses)
            : base(name, requiredlevel, defaultvalue, displaynames)
        {
            //NextValue = nextvalue;
            Statuses = statuses;
        }
    }

    /// <summary>
    /// Field class corresponding to attribute type: state
    /// </summary>
    [Serializable, XmlRoot("field")]
    public class StateField : simplefield
    {
        private List<State> _states;

        [XmlArray(ElementName = "states")]
        [XmlArrayItem(typeof(State), ElementName = "state")]
        public List<State> States
        {
            get { return _states; }
            set { _states = value; }
        }

        public StateField() : base()
        {
        }
        public StateField(string name, string requiredlevel, string defaultvalue, List<DisplayName> displaynames, List<State> states)
            : base(name, requiredlevel, defaultvalue, displaynames)
        {
            States = states;
        }
    }

    #endregion 
}
