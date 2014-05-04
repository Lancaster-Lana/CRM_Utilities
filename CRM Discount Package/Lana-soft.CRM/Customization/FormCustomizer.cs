using System;
using System.Globalization;
using System.Text;
using System.Xml;

namespace LanaSoftCRM
{
    public class FormCustomizer
    {
        #region Constants
        private int ENGLISH_LANGUAGE_CODE = 1033;

        private int DEFAULT_LOCK_LEVEL = 0;

        // <section> attributes
        private int DEFAULT_SECTION_COLUMNS = 11;
        private string DEFAULT_SECTION_LAYOUT = "varwidth";

        // <cell> attributes
        private bool DEFAULT_CELL_AUTO = false;
        private int DEFAULT_CELL_COLSPAN = 1;
        private bool DEFAULT_CELL_DISABLED = false;
        private int DEFAULT_CELL_ROWSPAN = 1;
        private bool DEFAULT_CELL_SHOW_LABEL = true;
        #endregion

        #region Properties

        private string _addedBy;

        private XmlDocument _formXml;
        public XmlDocument Xml
        {
            get
            {
                return _formXml;
            }
        }
        #endregion

        #region Constructor
        public FormCustomizer(XmlDocument formXml, string addedBy)
        {
            _formXml = formXml;
            _addedBy = addedBy;
        }
        #endregion

        #region Methods: Form
        public FormNavigator AddTab(string entityName, string title)
        {
            return AddTab(entityName, title, DEFAULT_LOCK_LEVEL, ENGLISH_LANGUAGE_CODE);
        }

        public FormNavigator AddTab(string entityName, string title, int lockLevel, int languageCode)
        {
            // retrieve <form>
            XmlNode tabsNode = _formXml.SelectSingleNode(GetTabsXPath(entityName));

            // check <form> exists
            if (tabsNode == null)
            {
                throw new NullReferenceException();
            }
            // create <tab>
            Guid tabId = Guid.NewGuid();
            XmlElement tabElement = _formXml.CreateElement("tab");
            tabElement.SetAttribute("addedby", _addedBy);
            tabElement.SetAttribute("id", tabId.ToString("b"));
            tabElement.SetAttribute("IsUserDefined", "1");	// will always be user defined
            tabElement.SetAttribute("locklevel", lockLevel.ToString(CultureInfo.InvariantCulture));

            // create & add <labels> element
            CreateAndAppendLabelsElement(tabElement, title, languageCode);

            tabsNode.AppendChild(tabElement);

            return new FormNavigator(tabElement, tabId.ToString("b"), true);
        }


        private FormNavigator AddSections(string entityName, Guid tabId)
        {
            // retrieve <sections>
            var tabNode = _formXml.SelectSingleNode(GetTabXPath(entityName, tabId));

            // check <tab> exists
            if (tabNode == null)
            {
                throw new NullReferenceException();
            }
            // check if <sections> exists
            var sectionsNode = _formXml.SelectSingleNode(GetSectionsXPath(entityName, tabId));

            // create <sections> if not found
            if (sectionsNode == null)
            {
                sectionsNode = _formXml.CreateElement("sections");
                tabNode.AppendChild(sectionsNode);
            }

            return new FormNavigator(sectionsNode, tabId.ToString("b"), true);
        }


        public FormNavigator AddSection(string entityName, Guid tabId, string title, bool showLabel, bool showBar)
        {
            return AddSection(entityName, tabId, Guid.NewGuid(), title, showLabel, showBar,
                DEFAULT_LOCK_LEVEL, DEFAULT_SECTION_LAYOUT, DEFAULT_SECTION_COLUMNS, ENGLISH_LANGUAGE_CODE);
        }


        public FormNavigator AddSection(string entityName, Guid tabId, Guid sectionId, string title, bool showLabel, bool showBar, int lockLevel,
            string layout, int columns, int languageCode)
        {
            // retrieve the specified tab
            XmlNode tabNode = _formXml.SelectSingleNode(GetTabXPath(entityName, tabId));

            // check the tab exists
            if (tabNode == null)
            {
                throw new NullReferenceException();
            }
            // retrieve <section>
            var sectionElement = _formXml.SelectSingleNode(GetSectionXPath(entityName, tabId, sectionId)) as XmlElement;

            // if <section> does not exist, create & add <section> to <sections>
            if (sectionElement == null)
            {
                #region <sections>
                // retrieve <sections>
                var sectionsNode = _formXml.SelectSingleNode(GetSectionsXPath(entityName, tabId));

                // if <sections> does not exist, add <sections> to <tab>
                // NOTE: can be added b/c <sections> is a generic element node
                if (sectionsNode == null)
                {
                    FormNavigator sectionsNavigator = AddSections(entityName, tabId);
                    sectionsNode = sectionsNavigator.Element;
                }
                #endregion

                #region <section>
                // create <section>
                sectionElement = _formXml.CreateElement("section");
                sectionElement.SetAttribute("addedby", _addedBy);
                sectionElement.SetAttribute("showlabel", showLabel.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
                sectionElement.SetAttribute("showbar", showBar.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
                sectionElement.SetAttribute("locklevel", lockLevel.ToString(CultureInfo.InvariantCulture));
                sectionElement.SetAttribute("id", sectionId.ToString("b"));
                sectionElement.SetAttribute("IsUserDefined", "1");	// always user defined
                sectionElement.SetAttribute("layout", layout);
                sectionElement.SetAttribute("columns", columns.ToString(CultureInfo.InvariantCulture));
                CreateAndAppendLabelsElement(sectionElement, title, languageCode);	// create & add <labels> to <section>

                // add <section> to <sections>
                sectionsNode.AppendChild(sectionElement);
                #endregion
            }

            return new FormNavigator(sectionElement, sectionId.ToString("b"), true);
        }


        private FormNavigator AddRows(string entityName, Guid tabId, Guid sectionId)
        {
            // retrieve <section>
            XmlNode sectionNode = _formXml.SelectSingleNode(GetSectionXPath(entityName, tabId, sectionId));

            // check <section> exists
            if (sectionNode == null)
            {
                throw new NullReferenceException();
            }
            // retrieve <rows>
            var rowsElement = _formXml.SelectSingleNode(GetRowsXPath(entityName, tabId, sectionId)) as XmlElement;

            // if <rows> does not exist, add <rows> to <section>
            if (rowsElement == null)
            {
                rowsElement = _formXml.CreateElement("rows");
                rowsElement.SetAttribute("addedby", _addedBy);

                sectionNode.AppendChild(rowsElement);
            }

            return new FormNavigator(rowsElement, sectionId.ToString("b"), true);
        }


        public FormNavigator AddRow(string entityName, Guid tabId, Guid sectionId)
        {
            // retrieve <section>
            XmlNode sectionNode = _formXml.SelectSingleNode(GetSectionXPath(entityName, tabId, sectionId));

            // check <section> exists
            if (sectionNode == null)
            {
                throw new NullReferenceException();
            }

            #region <rows>
            // retrieve <rows>
            var rowsNode = _formXml.SelectSingleNode(GetRowsXPath(entityName, tabId, sectionId));

            // if <rows> does not exist, add <rows> to <section>
            // NOTE: can be added b/c <rows> is a generic element node
            if (rowsNode == null)
            {
                FormNavigator rowsNavigator = AddRows(entityName, tabId, sectionId);
                rowsNode = rowsNavigator.Element;
            }
            #endregion

            #region <row>
            // create <row>
            var rowElement = _formXml.CreateElement("row");
            rowElement.SetAttribute("addedby", _addedBy);

            // add <row> to <rows>
            rowsNode.AppendChild(rowElement);
            #endregion

            return new FormNavigator(rowElement, sectionId.ToString("b"), true);
        }


        public FormNavigator AddCellToNewRow(string entityName, Guid tabId, Guid sectionId, string description, string id, FieldDataType dataType, string dataFieldName)
        {
            return AddCellToNewRow(entityName, tabId, sectionId, description, id, dataType, dataFieldName, DEFAULT_CELL_DISABLED, DEFAULT_CELL_SHOW_LABEL,
                DEFAULT_CELL_AUTO, DEFAULT_CELL_COLSPAN, DEFAULT_CELL_ROWSPAN, ENGLISH_LANGUAGE_CODE);
        }


        public FormNavigator AddCellToNewRow(string entityName, Guid tabId, Guid sectionId, string description, string id, FieldDataType dataType, string dataFieldName, bool disabled,
            bool showLabel, bool auto, int colspan, int rowspan, int languageCode)
        {
            // rerieve <section>
            XmlNode sectionNode = _formXml.SelectSingleNode(GetSectionXPath(entityName, tabId, sectionId));

            // check <section> exists
            if (sectionNode == null)
            {
                throw new NullReferenceException();
            }
            // retrieved <rows>
            XmlNode rowsNode = _formXml.SelectSingleNode(GetRowsXPath(entityName, tabId, sectionId));

            // if <rows> does not exist, create & add <rows> to <section>
            // NOTE: can be added b\c <rows> is a generic element
            if (rowsNode == null)
            {
                AddRows(entityName, tabId, sectionId);
            }

            // add <row>
            var rowNavigator = AddRow(entityName, tabId, sectionId);

            // add <cell>
            var controlElement = _formXml.SelectSingleNode(GetControlXPath(entityName, tabId, sectionId, id)) as XmlElement;

            // if <control> of <cell> does not exist, create & add <cell>
            if (controlElement == null)
            {
                // create <cell>
                var cellElement = CreateCellElement(entityName, description, id, dataType, dataFieldName, disabled, showLabel, auto, colspan, rowspan, languageCode);

                // add <cell> to <row>
                rowNavigator.Element.AppendChild(cellElement);

                return new FormNavigator(cellElement, id, false);
            }
            // retrieve "id" attribute from <control> (child of <cell>)
            string controlId = controlElement.GetAttribute("id");
            return new FormNavigator(controlElement.ParentNode, controlId, false);
        }


        public FormNavigator AddCellToExistingRow(string entityName, string description, string id, FieldDataType dataType, string dataFieldName, FormNavigator rowNavigator)
        {
            return AddCellToExistingRow(entityName, description, id, dataType, dataFieldName, rowNavigator.Element);
        }

        public FormNavigator AddCellToExistingRow(string entityName, string description, string id, FieldDataType dataType, string dataFieldName, XmlNode rowNode)
        {
            return AddCellToExistingRow(entityName, description, id, dataType, dataFieldName, DEFAULT_CELL_DISABLED, DEFAULT_CELL_SHOW_LABEL, DEFAULT_CELL_AUTO,
                DEFAULT_CELL_COLSPAN, DEFAULT_CELL_ROWSPAN, rowNode, ENGLISH_LANGUAGE_CODE);
        }

        public FormNavigator AddCellToExistingRow(string entityName, string description, string id, FieldDataType dataType, string dataFieldName, bool disabled,
            bool showLabel, bool auto, int colspan, int rowspan, XmlNode rowNode, int languageCode)
        {
            // check <row>
            if (rowNode == null)
            {
                throw new NullReferenceException();
            }
            var cellElement = GetLastRowCell(rowNode);
            if (cellElement.SelectSingleNode("control") != null)//last cell is not empty 
            {
                // create <cell>
                cellElement = CreateCellElement(entityName, description, id, dataType, dataFieldName, disabled, showLabel, auto, colspan, rowspan, languageCode);
                // add <cell> to <row>
                rowNode.AppendChild(cellElement);
            }
            else
            {
                //last cell is empty: create for it control element
                CreateControlElementForCell(cellElement, description, id, dataType, dataFieldName, disabled, showLabel, auto, colspan, rowspan, languageCode);
            }

            return new FormNavigator(cellElement, id, false);
        }


        private XmlNode GetLastRowCell(XmlNode rowNode)
        {
            //XmlNodeList lst = rowNode.SelectNodes("cell");
            return rowNode.ChildNodes[rowNode.ChildNodes.Count - 1];
        }

        private XmlElement CreateCellElement(string entityName, string description, string id, FieldDataType dataType, string dataFieldName, bool disabled,
            bool showLabel, bool auto, int colspan, int rowspan, int languageCode)
        {
            // create <cell>
            var cellElement = _formXml.CreateElement("cell");

            // set <cell> attributes
            cellElement.SetAttribute("addedby", _addedBy);
            if (colspan > 0)
            {
                cellElement.SetAttribute("colspan", colspan.ToString(CultureInfo.InvariantCulture));
            }
            if (!showLabel)
            {
                cellElement.SetAttribute("showlabel", showLabel.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            }
            if (rowspan > 0)
            {
                cellElement.SetAttribute("rowspan", rowspan.ToString(CultureInfo.InvariantCulture));
            }
            if (auto)
            {
                cellElement.SetAttribute("auto", auto.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            }

            #region <cell> child nodes
            // create & add <labels> to <cell>
            CreateAndAppendLabelsElement(cellElement, description, languageCode);

            // create <control>
            XmlElement controlElement = _formXml.CreateElement("control");

            // set <control> attributes
            controlElement.SetAttribute("id", id);
            controlElement.SetAttribute("classid", Utility.GetDescriptionAttribute(dataType));
            if (dataFieldName != null)
                controlElement.SetAttribute("datafieldname", dataFieldName);
            if (disabled)
            {
                controlElement.SetAttribute("disabled", disabled.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            }

            // add <control> to <cell>
            cellElement.AppendChild(controlElement);
            #endregion

            return cellElement;
        }


        private XmlNode CreateControlElementForCell(XmlNode cellElement, string description, string id, FieldDataType dataType, string dataFieldName, bool disabled,
            bool showLabel, bool auto, int colspan, int rowspan, int languageCode)
        {

            #region <cell> child nodes
            // create & add <labels> to <cell>
            CreateAndAppendLabelsElement(cellElement, description, languageCode);
            //SetLabel(cellElement, description, languageCode);

            // create <control>
            XmlElement controlElement = _formXml.CreateElement("control");

            // set <control> attributes
            controlElement.SetAttribute("id", id);
            controlElement.SetAttribute("classid", Utility.GetDescriptionAttribute(dataType));
            if (dataFieldName != null)
                controlElement.SetAttribute("datafieldname", dataFieldName);
            if (disabled)
            {
                controlElement.SetAttribute("disabled", disabled.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            }

            // add <control> to <cell>
            cellElement.AppendChild(controlElement);

            #endregion

            return cellElement;
        }

        #endregion

        #region Methods: JScript Injector
        public FormNavigator AddFormEventJScript(string entityName, string script, JScriptType type, InsertionMode mode, string[] dependencies)
        {
            // retrieve <form> and <events>
            XmlNode formNode = _formXml.SelectSingleNode(GetFormXPath(entityName));
            XmlNode eventsNode = _formXml.SelectSingleNode(GetFormEventsXPath(entityName));

            // check if <form> exists
            if (formNode == null)
            {
                throw new NullReferenceException();
            }
            // if <events> does not exist, create & add <events> to <form>
            if (eventsNode == null)
            {
                eventsNode = _formXml.CreateElement("events");
                formNode.AppendChild(eventsNode);
            }

            // retrieve <event>
            XmlNode eventNode = _formXml.SelectSingleNode(GetFormEventXPath(entityName, type));

            // check if <event> exists
            if (eventNode == null)
            {
                // create & append <event> to <events>
                eventNode = CreateEventElement(type.ToString(CultureInfo.InvariantCulture));
                eventsNode.AppendChild(eventNode);
            }

            //Activate event !!!
            if (eventNode.Attributes != null) eventNode.Attributes.GetNamedItem("active").InnerText = "true";
            // retrieve <script>
            var scriptElement = eventNode.SelectSingleNode("./script") as XmlElement;

            // check if <script> exists
            if (scriptElement == null)
            {
                eventNode.AppendChild(CreateScriptElement(script));
            }
            else
            {
                // retrieve the existing JScript
                string originalScript = scriptElement.InnerText;

                // create & replace <event> from <events>
                var newScriptNode = CreateScriptElement(script, originalScript, mode);
                eventNode.ReplaceChild(newScriptNode, scriptElement);
            }

            // retrieve <dependencies>
            var dependenciesNode = eventNode.SelectSingleNode("./dependencies");

            // check if <dependencies> exists
            if (dependenciesNode == null)
            {
                // create & append <dependencies> to <event>
                dependenciesNode = _formXml.CreateElement("dependencies");
                eventNode.AppendChild(dependenciesNode);
            }

            // append <dependency> to <dependencies>
            if (dependencies != null && dependencies.Length != 0)
            {
                foreach (string dependency in dependencies)
                {
                    string dependencyXPath = string.Format("./dependency[@id=\"{0}\"]", dependency);

                    var dependencyElement = dependenciesNode.SelectSingleNode(dependencyXPath) as XmlElement;
                    if (dependencyElement == null)
                    {
                        dependencyElement = _formXml.CreateElement("dependency");
                        dependencyElement.SetAttribute("id", dependency);

                        dependenciesNode.AppendChild(dependencyElement);
                    }
                }
            }

            return new FormNavigator(eventNode, string.Empty, false);
        }


        public FormNavigator AddOnChangeJScript(string entityName, Guid tabId, Guid sectionId, string id, string script, InsertionMode mode, string[] dependencies)
        {
            // retrieve <control>
            XmlNode controlNode = _formXml.SelectSingleNode(GetControlXPath(entityName, tabId, sectionId, id));

            // check <control> exists
            if (controlNode == null)
            {
                throw new NullReferenceException();
            }
            // retrieve <cell> & <labels>
            XmlNode cellNode = controlNode.ParentNode;

            // retrieve <events> & <event> from <cell>
            XmlNode eventsNode = cellNode.SelectSingleNode("./events");
            XmlElement eventElement = cellNode.SelectSingleNode("./events/event[@name=\"onchange\"]") as XmlElement;

            // check if <events> exists
            if (eventsNode == null)
            {
                // create <events>
                eventsNode = _formXml.CreateElement("events");

                // insert <events> into <cell> after <labels>
                XmlNode labelsNode = cellNode.SelectSingleNode("./labels");
                cellNode.InsertAfter(eventsNode, labelsNode);
            }

            // check if <event> exists
            if (eventElement == null)
            {
                // create & append <event> into <events>
                eventElement = CreateEventElement("onchange");
                eventsNode.AppendChild(eventElement);
            }

            //Activate event !!!
            try
            {
                eventsNode.Attributes.GetNamedItem("active").InnerText = "true";
            }
            catch { }

            // check if <script> exists
            XmlNode scriptNode = eventElement.SelectSingleNode("./script");
            if (scriptNode == null)
            {
                // create & append <script> to <event>
                eventElement.AppendChild(CreateScriptElement(script));
            }
            else
            {
                string originalScript = eventElement.SelectSingleNode("./script").InnerText;

                // create & replace <script> in <event>
                XmlNode newScriptNode = CreateScriptElement(script, originalScript, mode);
                eventElement.ReplaceChild(newScriptNode, scriptNode);
            }

            // retrieve <dependencies>
            XmlNode dependenciesNode = eventElement.SelectSingleNode("./dependencies");

            // check if <dependencies> exists
            if (dependenciesNode == null)
            {
                // create & append <dependencies> to <event>
                dependenciesNode = _formXml.CreateElement("dependencies");
                eventElement.AppendChild(dependenciesNode);
            }

            // append <dependency> to <dependencies>
            if (dependencies != null && dependencies.Length != 0)
            {
                foreach (string dependency in dependencies)
                {
                    string dependencyXPath = string.Format("./dependency[@id=\"{0}\"]", dependency);

                    var dependencyElement = dependenciesNode.SelectSingleNode(dependencyXPath) as XmlElement;
                    if (dependencyElement == null)
                    {
                        dependencyElement = _formXml.CreateElement("dependency");
                        dependencyElement.SetAttribute("id", dependency);

                        dependenciesNode.AppendChild(dependencyElement);
                    }
                }
            }
            return new FormNavigator(eventElement, string.Empty, false);
        }

        private XmlElement CreateEventElement(string eventType)
        {
            // create <event>
            XmlElement eventElement = _formXml.CreateElement("event");
            eventElement.SetAttribute("name", eventType);
            eventElement.SetAttribute("application", "false");
            eventElement.SetAttribute("active", "true");

            return eventElement;
        }

        private XmlElement CreateScriptElement(string script)
        {
            return CreateScriptElement(script, string.Empty, InsertionMode.Overwrite);
        }

        private XmlElement CreateScriptElement(string script, string originalScript, InsertionMode mode)
        {
            // create <script>
            XmlElement scriptElement = _formXml.CreateElement("script");

            // create the proper CData element
            XmlCDataSection xmlCData;
            StringBuilder formattedScript;

            if (string.IsNullOrEmpty(originalScript))
            {
                // encapsulate the script
                formattedScript = new StringBuilder();
                formattedScript.Append("// Start JScript event");
                formattedScript.Append(Environment.NewLine);
                formattedScript.Append(script);
                formattedScript.Append(Environment.NewLine);
                //formattedScript.AppendFormat("// End -- JScript event added by: {0} ", _addedBy);

                xmlCData = _formXml.CreateCDataSection(formattedScript.ToString());
            }
            else
            {
                // determines how to format the scripts
                switch (mode)
                {
                    case InsertionMode.Overwrite:
                        // encapsulate the script
                        formattedScript = new StringBuilder();
                        formattedScript.Append("// Start JScript event ");
                        formattedScript.Append(Environment.NewLine);
                        formattedScript.Append(script);
                        formattedScript.Append(Environment.NewLine);
                        //formattedScript.AppendFormat("// End -- JScript event added by: {0} ", _addedBy);

                        xmlCData = _formXml.CreateCDataSection(formattedScript.ToString());
                        break;

                    case InsertionMode.InsertBefore:
                        // create combined script
                        formattedScript = new StringBuilder();
                        formattedScript.Append("// Start JScript event");
                        formattedScript.Append(Environment.NewLine);
                        formattedScript.Append(script);
                        formattedScript.Append(Environment.NewLine);
                        //formattedScript.AppendFormat("// End -- JScript event added by: {0} ", _addedBy);
                        formattedScript.Append(Environment.NewLine);
                        formattedScript.Append(Environment.NewLine);
                        formattedScript.Append(originalScript);

                        xmlCData = _formXml.CreateCDataSection(formattedScript.ToString());
                        break;

                    case InsertionMode.InsertAfter:
                        // create combined script
                        formattedScript = new StringBuilder(originalScript);
                        formattedScript.Append(Environment.NewLine);
                        formattedScript.Append(Environment.NewLine);
                        formattedScript.Append("// Start JScript event");
                        formattedScript.Append(Environment.NewLine);
                        formattedScript.Append(script);
                        formattedScript.Append(Environment.NewLine);
                        //formattedScript.AppendFormat("// End -- JScript event added by: {0} ", _addedBy);

                        xmlCData = _formXml.CreateCDataSection(formattedScript.ToString());
                        break;

                    default:
                        throw new ArgumentException();
                }
            }

            // append CData element to <script>
            scriptElement.AppendChild(xmlCData);

            return scriptElement;
        }
        #endregion

        #region Methods: Helper
        private void CreateAndAppendLabelsElement(XmlNode parentNode, string description, int languageCode)
        {
            // create the <labels> element node
            XmlNode labelsNode = parentNode.SelectSingleNode("labels");
            if (labelsNode == null)
            {
                labelsNode = _formXml.CreateElement("labels");
                parentNode.AppendChild(labelsNode);
            }

            //XmlNode labelsNode = _formXml.CreateElement("labels");
            //parentNode.AppendChild(labelsNode);

            // create the <label> element node
            XmlElement labelElement = _formXml.CreateElement("label");
            labelElement.SetAttribute("description", description);
            labelElement.SetAttribute("languagecode", languageCode.ToString(CultureInfo.InvariantCulture));
            labelsNode.AppendChild(labelElement);
        }

        /// <summary>
        /// Gets the XPath to the form element node
        /// </summary>
        /// <param name="entityName">Schema name of the entity in CRM</param>
        /// <returns>Returns the XPath as a string</returns>
        private string GetFormXPath(string entityName)
        {
            return string.Format("/ImportExportXml/Entities/Entity/FormXml/forms/entity[@name=\"{0}\"]/form[@type=\"main\"]", entityName);
        }


        private string GetFormEventsXPath(string entityName)
        {
            return string.Format("{0}/events", GetFormXPath(entityName));
        }


        private string GetFormEventXPath(string entityName, JScriptType type)
        {
            string eventsXPath = GetFormEventsXPath(entityName);

            return string.Format("{0}/event[@name=\"{1}\"]", eventsXPath, type.ToString(CultureInfo.InvariantCulture));
        }


        private string GetTabsXPath(string entityName)
        {
            return string.Format("{0}/tabs", GetFormXPath(entityName));
        }

        /// <summary>
        /// Gets the XPath to the specified tab element node
        /// </summary>
        /// <param name="entityName">Schema name of the entity in CRM</param>
        /// <param name="tabId">Id of the tab</param>
        /// <returns>Returns the XPath as a string</returns>
        private string GetTabXPath(string entityName, Guid tabId)
        {
            string tabsXPath = GetTabsXPath(entityName);

            return string.Format("{0}/tab[@id=\"{1}\"]", tabsXPath, tabId.ToString("b"));
        }

        /// <summary>
        /// Gets the XPath to the specified sections element node
        /// </summary>
        /// <param name="entityName">Schema name of the entity in CRM</param>
        /// <param name="tabId">Id of the tab</param>
        /// <returns>Returns the XPath as a string</returns>
        private string GetSectionsXPath(string entityName, Guid tabId)
        {
            return string.Format("{0}/sections", GetTabXPath(entityName, tabId));
        }

        /// <summary>
        /// Gets the XPath to the specified section element node
        /// </summary>
        /// <param name="entityName">Schema name of the entity in CRM</param>
        /// <param name="tabId">Id of the tab</param>
        /// <param name="sectionId">Id of the section</param>
        /// <returns>Returns the XPath as a string</returns>
        private string GetSectionXPath(string entityName, Guid tabId, Guid sectionId)
        {
            string sectionsXPath = GetSectionsXPath(entityName, tabId);

            return string.Format("{0}/section[@id=\"{1}\"]", sectionsXPath, sectionId.ToString("b"));
        }


        private string GetRowsXPath(string entityName, Guid tabId, Guid sectionId)
        {
            string sectionXPath = GetSectionXPath(entityName, tabId, sectionId);

            return string.Format("{0}/rows", sectionXPath);
        }


        private string GetControlXPath(string entityName, Guid tabId, Guid sectionId, string id)
        {
            string rowsXPath = GetRowsXPath(entityName, tabId, sectionId);

            return string.Format("{0}/row/cell/control[@id=\"{1}\"]", rowsXPath, id);
        }
        #endregion
    }
}