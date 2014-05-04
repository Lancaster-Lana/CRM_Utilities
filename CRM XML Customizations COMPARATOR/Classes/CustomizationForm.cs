using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

using CustomizationsComparator;

namespace CustomizationsComparator.FormXml
{
    public enum EventType { OnLoad = 0, OnSave = 1, OnChange = 2 }

    public class FormType
    {
        public static string Main = "main";
        public static string PreView = "preview";
    }

    public class FormAnalyzingPart
    {
        public const string Events = "events";
        public const string Data = "data";
        public const string Cells = "cells";
    }


    [XmlRoot("dependency")]
    public class Dependency
    {
        string _id = string.Empty;

        [XmlAttribute("id")]
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public override bool Equals(object obj)
        {
            Dependency cmpDepency = obj as Dependency;
            return this.ID.Equals(cmpDepency.ID);
        }
        public override string ToString()
        {
            string depStr = "ID=" + this.ID;
            return depStr;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    [XmlRoot("event")]
    public class FormEvent
    {            
        string _eventtype = String.Empty;
        bool _application = false;
        bool _active = true;

        string _script = string.Empty;
        List<Dependency> _dependencies = new List<Dependency>();

        [XmlAttribute("name")]
        public string Name
        {
            get { return _eventtype; }
            set { _eventtype = value; }
        }

        [XmlAttribute("application")]
        public bool Application
        {
            get { return _application; }
            set { _application = value; }
        }

        [XmlAttribute("active")]
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        [XmlElement(ElementName = "script")]
        public string Script
        {
            get { return _script; }
            set { _script = value; }
        }

        [XmlArray(ElementName = "dependencies")]
        [XmlArrayItem(typeof(Dependency), ElementName = "dependency")]
        public List<Dependency> Dependencies
        {
            get { return _dependencies; }
            set { _dependencies = value; }       
        }

        public FormEvent()
        {
        }

        public FormEvent(string type, string script)
        {
            Name = type;
            Script = script;
        }

        public override bool Equals(object obj)
        {
            FormEvent cmpEvent = obj as FormEvent;
            if (cmpEvent != null)
                return (this.Name.Equals(cmpEvent.Name)
                        && this.Script.Equals(cmpEvent.Script)
                        && this.Active.Equals(cmpEvent.Active)
                        && this.Application.Equals(cmpEvent.Application));
            if ((this == null) && (cmpEvent == null))
                return true;

            return false;
        }      

        public override string ToString()
        {
            return Script;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    [XmlRoot("data")]
    public class FormData
    {
        string _id = string.Empty;
        string _datafieldname = string.Empty;
        Guid _classid = Guid.Empty;

        [XmlAttribute("id")]
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        [XmlAttribute("datafieldname")]
        public string DataFieldName
        {
            get { return _datafieldname; }
            set { _datafieldname = value; }
        }    

        [XmlAttribute("classid")]
        public Guid ClassId
        {
            get { return _classid; }
            set { _classid = value; }
        }

        public FormData()
        { }
    }

    [XmlRoot("tab")]
    public class Tab
    {        
        string _name = String.Empty;
        bool _verticallayout = false;
        bool _isuserdefined = false;
        int _locklevel = 0;
        Guid _id = Guid.Empty;

        List<DisplayName> _labels = new List<DisplayName>();
        List<Section> _sections = new List<Section>();

        [XmlAttribute("name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [XmlAttribute("id")]
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }

        [XmlAttribute("verticallayout")]
        public bool VerticalLayout
        {
            get { return _verticallayout; }
            set { _verticallayout = value; }
        }

        [XmlAttribute("IsUserDefined")]
        public bool IsUserDefined
        {
            get { return _isuserdefined; }
            set { _isuserdefined = value; }
        }

        [XmlAttribute("locklevel")]
        public int LockLevel
        {
            get { return _locklevel; }
            set { _locklevel = value; }
        }

        [XmlArray(ElementName = "labels")]
        [XmlArrayItem(typeof(DisplayName), ElementName = "label")]
        public List<DisplayName> Labels
        {
            get { return _labels; }
            set { _labels = value; }
        }

        [XmlArray(ElementName = "sections")]
        [XmlArrayItem(typeof(Section), ElementName = "section")]
        public List<Section> Sections
        {
            get { return _sections; }
            set { _sections = value; }
        }

        public Tab()
        { 
        }

        public Tab(List<DisplayName> labels, List<Section> sections)
        {
            Labels = labels;
            Sections = sections;
        }

    }    
    
    [XmlRoot("section")]
    public class Section
    {
        string _name = String.Empty;
        Guid _id = Guid.Empty;
        bool _isuserdefined = false;
        bool _showlabel = false;
        bool _showbar = false;

        List<DisplayName> _labels = new List<DisplayName>();
        List<Row> _rows = new List<Row>();       

        [XmlAttribute("name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [XmlAttribute("relatedInformationCollapsed")]
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }

        [XmlAttribute("IsUserDefined")]
        public bool IsUserDefined
        {
            get { return _isuserdefined; }
            set { _isuserdefined = value; }
        }

        [XmlAttribute("showlabel")]
        public bool ShowLabel
        {
            get { return _showlabel; }
            set { _showlabel = value; }
        }

        [XmlAttribute("showbar")]
        public bool ShowBar
        {
            get { return _showbar; }
            set { _showbar = value; }
        }

        [XmlArray(ElementName = "labels")]
        [XmlArrayItem(typeof(DisplayName), ElementName = "label")]
        public List<DisplayName> Labels
        {
            get { return _labels; }
            set { _labels = value; }
        }

        [XmlArray(ElementName = "rows")]
        [XmlArrayItem(typeof(Row), ElementName = "row")]
        public List<Row> Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }

        public Section()
        { 
        }

        public Section(List<DisplayName> labels, List<Row> rows)
        {
            Labels = labels;
            Rows = rows;
        }
    }

    [XmlRoot("row")]
    public class Row
    {
        List<Cell> _cells = new List<Cell>();

        [XmlElement(ElementName = "cell")]
        public List<Cell> Cells
        {
            get { return _cells; }
            set { _cells = value; }
        }

        public Row()
        {            
        }
    }

    [XmlRoot("cell")]
    public class Cell
    {
        CustomizationControl _control = new CustomizationControl();
        List<DisplayName> _labels = new List<DisplayName>();
        List<FormEvent> _events = new List<FormEvent>();
        bool _showlabel = true;
        int _locklevel = 0;

        [XmlAttribute("showlabel")]
        public bool ShowLabel
        {
            get { return _showlabel; }
            set { _showlabel = value; }
        }

        [XmlAttribute("locklevel")]
        public int LockLevel
        {
            get { return _locklevel; }
            set { _locklevel = value; }
        }

        [XmlArray(ElementName = "labels")]
        [XmlArrayItem(typeof(DisplayName), ElementName = "label")]
        public List<DisplayName> Labels
        {
            get { return _labels; }
            set { _labels = value; }
        }

        [XmlArray(ElementName = "events")]
        [XmlArrayItem(typeof(FormEvent), ElementName = "event")]
        public List<FormEvent> Events
        {
            get { return _events; }
            set { _events = value; }
        }

        [XmlElement(ElementName = "control")]
        public CustomizationControl Control
        {
            get { return _control; }
            set { _control = value; }
        }

        public Cell()
        {
        }

    }

    [XmlRoot("control")]
    public class CustomizationControl
    {
        string _datafieldname = String.Empty;       
        Guid _classid = Guid.Empty;
        string _id = string.Empty;
        bool _disabled = false;

        [XmlAttribute("id")]
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        [XmlAttribute("datafieldname")]
        public string DataFieldName
        {
            get { return _datafieldname; }
            set { _datafieldname = value; }
        }

        [XmlAttribute("classid")]
        public Guid ClassID
        {
            get { return _classid; }
            set { _classid = value; }
        }

        [XmlAttribute("disabled")]
        public bool Disabled
        {
            get { return _disabled; }
            set { _disabled = value; }
        }

        public CustomizationControl()
        {
        }

        public override bool Equals(object obj)
        {
            CustomizationControl cmpCtrl = obj as CustomizationControl;
            if (cmpCtrl != null)
                return (this.ID.Equals(cmpCtrl.ID)
                        && this.DataFieldName.Equals(cmpCtrl.DataFieldName)
                        && this.ClassID.Equals(cmpCtrl.ClassID)                        
                        && this.Disabled.Equals(cmpCtrl.Disabled));

            if ((this == null) && (cmpCtrl == null)) return true;

            return false;
        }

        public override string ToString()
        {
            string descrStr = "id=" + this.ID + " \r\n"
                             + " datafieldname=" + this.DataFieldName + " \r\n"
                             + " classid=" + this.ClassID.ToString() + " \r\n" 
                             + " disabled=" + this.Disabled.ToString();
            return descrStr;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }


    /// <summary>
    /// Has no attribute and field format(correspondence in FieldsXml)=> only at FormXml
    /// </summary>
    //[Serializable, XmlRoot("field")]
    public class IFrameField
    {
        private string _url;
        private string _scrolling;//auto, none
        private bool _security;
        private bool _passparameters;
        private bool _border;


        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string Scrolling
        {
            get { return _scrolling; }
            set { _scrolling = value; }
        }

        public bool Security
        {
            get { return _security; }
            set { _security = value; }
        }

        public bool PassParameters
        {
            get { return _passparameters; }
            set { _passparameters = value; }
        }

        public bool Border
        {
            get { return _border; }
            set { _border = value; }
        }

        public IFrameField(string url, string scrolling, bool security, bool passparameters, bool border)
        {
            Url = url;
            Scrolling = scrolling;
            Security = security;
            PassParameters = passparameters;
            Border = border;
        }

        public static object Deserialize(string FormXML, FormType formType)
        {
                FormXML = "<?xml version=\"1.0\"?>" + FormXML;
                TextReader sr = new StringReader(FormXML);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomizationForm));

                //2. Handle custom nodes                         
                object form = xmlSerializer.Deserialize(sr);

                return form;
        
           // return null;
        }
   }


    [XmlRoot("form")]
    public class CustomizationForm
    {
        string _formType = FormType.Main;
        Guid _id = Guid.Empty;
        bool _enablerelatedinformation = false;
        bool _relatedInformationCollapsed = false;

        List<FormData> _data = new List<FormData>();
        List<Tab> _formTabs = new List<Tab>();
        List<FormEvent> _formEvents = new List<FormEvent>();

        [XmlAttribute("type")]
        public string Type
        {
            get { return _formType; }
            set { _formType = value; }
        }
        [XmlAttribute("id")]
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }
        [XmlAttribute("enablerelatedinformation")]
        public bool EnableRelatedInformation
        {
            get { return _enablerelatedinformation; }
            set { _enablerelatedinformation = value; }
        }
        [XmlAttribute("relatedInformationCollapsed")]
        public bool RelatedInformationCollapsed
        {
            get { return _relatedInformationCollapsed; }
            set { _relatedInformationCollapsed = value; }
        }
        
        [XmlElement("data")] //!!!!!!!!!!!!!! ENUM
        public List<FormData> Data
        {
            get { return _data; }
            set { _data = value; }
        }
        
        [XmlArray("tabs")]
        [XmlArrayItem(typeof(Tab), ElementName = "tab")]
        public List<Tab> Tabs
        {
            get { return _formTabs; }
            set { _formTabs = value; }
        }
        

        [XmlArray("events")]
        [XmlArrayItem(typeof(FormEvent), ElementName = "event")]
        public List<FormEvent> Events
        {
            get { return _formEvents; }
            set { _formEvents = value; }
        }
        

        CustomizationForm()
        {
        }

        CustomizationForm(List<Tab> tabs, List<FormEvent> events)
        {
            Tabs = tabs;
            Events = events;
        }

        public static CustomizationForm Deserialize(string FormXml, string formType)
        {
            try
            {
                FormXml = "<?xml version=\"1.0\" ?>" + FormXml;
                TextReader sr = new StringReader(FormXml);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomizationForm));

                CustomizationForm form = (CustomizationForm)xmlSerializer.Deserialize(sr);

                return form;
            }
            catch (Exception ex)
            { 



            }
            return null;
        }


    }
}
