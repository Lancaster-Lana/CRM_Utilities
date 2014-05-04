using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using System.Xml;
using System.IO;
using CustomizationsComparator.Properties;
using CustomizationsComparator.Report;

using CustomizationsComparator.FormXml;

namespace CustomizationsComparator
{

    public enum ComparisonType { attributes = 0, fields = 1, forms = 2 }
    public enum ComparatorView { tree = 0, analyze = 1 }

    public delegate void CustomizationComparatorHandler();

    public partial class ComparatorForm : Form
    {     

        #region Constants

        static string TAG_ATTRIBUTE = "attribute";
        static string TAG_FIELD = "field";
        //static string TAG_FORM = "form";

        static string TAG_NAME = "Name";
        static string TAG_ORIGINALNAME = "OriginalName";

        static string attributeKeyProperty = "PhysicalName";
        static string fieldKeyProperty = "name";
        //static string fieldIdProperty = "field_id";        
        static string formKeyProperty = "type";

        static string eventFormKey1Property = "Name"; static string eventFormKey2Property = "Application";
        static string dataFormKeyProperty = "id";
        static string cellControlKeyProperty = "datafieldname";       
                
        static string commonEntitiesXMLPath = "Entities/Entity";
  
        static string attributesEntitySubXMLPath = "EntityInfo/entity/attributes";        
        static string fieldsEntitySubXMLPath = "FieldXml/entity/fields";
        static string formsEntitySubXMLPath = "FormXml/forms/entity/form";

        static Dictionary<string, string> TablesKeys = new Dictionary<string,string> ();
        
        #endregion

        #region Variables

        public event CustomizationComparatorHandler ComparationFinishedEvent;

        bool _showOnlyUpdated = false;
        string firstCustFilePath;
        string secondCustFilePath;
        XmlDocument _firstCustXML;
        XmlDocument _secoundCustXML;
        string _currentEntity = "";

        string _formType = FormType.Main;
        string _formAnalyzingPart = FormAnalyzingPart.Cells;


        List<string> firstOriginalEntitiesList = new List<string>();
        List<string> secondOriginEntitiesList = new List<string>();
        List<string> commonEntitiesList = new List<string>();


        Dictionary<string, CustomizationAttribute> firstAttributes = new Dictionary<string, CustomizationAttribute>();
        Dictionary<string, CustomizationAttribute> secondAttributes = new Dictionary<string, CustomizationAttribute>();

        Dictionary<string, object> firstFields = new Dictionary<string, object>();
        Dictionary<string, object> secondFields = new Dictionary<string, object>();

        Dictionary<string, CustomizationForm> firstForms = new Dictionary<string, CustomizationForm>();
        Dictionary<string, CustomizationForm> secondForms = new Dictionary<string, CustomizationForm>();

        DataSet firstAttrsFieldsFormsDS;//firstAttrsFieldsFormsDS
        DataSet secondAttrsFieldsFormsDS;

        List<string> firstOriginalAttrsNames;
        List<string> secondOriginalAttrsNames;

        List<string> firstOriginalFieldsNames;
        List<string> secondOriginalFieldsNames;

        List<string> firstFormOriginalDataIds;
        List<string> secondFormOriginalDataIds;
        List<string> firstFormOriginalControlsNames;
        List<string> secondFormOriginalControlsNames;
        List<KeyValuePair<string, bool>> firstFormOriginalEventsNames;
        List<KeyValuePair<string, bool>> secondFormOriginalEventsNames;

        Dictionary<string, DifferPropertiesDictionary> diffAttrs;
        Dictionary<string, DifferPropertiesDictionary> diffFields;

        Dictionary<string, DifferPropertiesDictionary> diffFormsData;
        Dictionary<KeyValuePair<string, bool>, /*FormEvent*/DifferPropertiesDictionary> diffFormsEvents;          
        Dictionary<string, DifferPropertiesDictionary> diffFormsCells;

        static string[] wordpadpath = new string[] { };

        #endregion

        #region Methods

        public ComparatorForm()
        {
            InitializeComponent();

        }

        public ComparatorForm(XmlDocument firstCustXML, XmlDocument secoundCustXML)
        {
            InitializeComponent();
            LoadCustomizations(firstCustXML, secoundCustXML);
        }

        #region Loading

        private void StartLoadingCustomizations()
        {
            //___________Show Status
            SetStatusMessage(Strings.loadingBegin);

            LoadCustomizationsDialog loadDial = new LoadCustomizationsDialog();
            if (loadDial.ShowDialog() == DialogResult.OK)
            {
                //1. If Loading XMLDocuments success
                if (LoadCustomizations(loadDial.firstCustFilePath, loadDial.secondCustFilePath))
                {
                    firstCustFilePath = loadDial.firstCustFilePath;
                    secondCustFilePath = loadDial.firstCustFilePath;

                    //2. Detect original names
                    XmlNodeList entitiesLstXML1 = _firstCustXML.DocumentElement.SelectNodes(commonEntitiesXMLPath + "/" + TAG_NAME);
                    XmlNodeList entitiesLstXML2 = _secoundCustXML.DocumentElement.SelectNodes(commonEntitiesXMLPath + "/" + TAG_NAME);

                    List<string> entLst1 = GetEntitiesNames(_firstCustXML);
                    List<string> entLst2 = GetEntitiesNames(_secoundCustXML);

                    CompareHelper.DetectCommonAndOriginalNames(entLst1, entLst2, out firstOriginalEntitiesList, out secondOriginEntitiesList, out commonEntitiesList);

                    //3.Load list of entities
                    LoadEntitiesToList();

                    //4.Switch Views
                    ChangeComparatorView(ComparatorView.tree.ToString());

                    //5. Status show
                    SetStatusMessage(Strings.loadingSuccess);
                }
            }
            else
                SetStatusMessage(String.Empty);

        }

        private bool LoadCustomizations(string firstxmlfile, string secondxmlfile)
        {
            bool areLoaded = true;

            if ((firstxmlfile == null) || (secondxmlfile == null))
            {
                MessageBox.Show(Strings.customizationFilePathFailed);
                areLoaded = false;
            }
            else
            {
                try
                {
                    //1. Load documents to compare
                    _firstCustXML = new XmlDocument();
                    _firstCustXML.Load(firstxmlfile);

                    _secoundCustXML = new XmlDocument();
                    _secoundCustXML.Load(secondxmlfile);

                    //2. 
                    LoadTrees(_firstCustXML, _secoundCustXML);
                }
                catch (Exception ex)
                {
                    areLoaded = false;
                    MessageBox.Show(ex.Message);
                }
            }
            return areLoaded;
        }

        private bool LoadCustomizations(XmlDocument firstxmlDoc, XmlDocument secondxmlDoc)
        {
            bool areLoaded = true;
            try
            {
                //1
                _firstCustXML = firstxmlDoc;
                _secoundCustXML = secondxmlDoc;
                //2
                LoadTrees(_firstCustXML, _secoundCustXML);
            }
            catch (Exception ex)
            {
                areLoaded = false;
                MessageBox.Show(ex.Message);
            }
            return areLoaded;
        }

        private void LoadTrees(XmlDocument firstCustXML, XmlDocument secoundCustXML)
        {
            BuildEntitiesTree(firstCustXML, firstTreeView);
            BuildEntitiesTree(secoundCustXML, secondTreeView);
        }

        private void LoadEntitiesToList()
        {
            entitiesListBox.Items.Clear();
            foreach (string entname in firstOriginalEntitiesList)
            {
                ListViewItem entItem = new ListViewItem(entname);
                entItem.Group = entitiesListBox.Groups[0];
                //entItem.ForeColor = Settings.Default.FirstColor;
                entItem.UseItemStyleForSubItems = true;
                entitiesListBox.Items.Add(entItem);
            }

            foreach (string entname in secondOriginEntitiesList)
            {
                ListViewItem entItem = new ListViewItem(entname);
                entItem.Group = entitiesListBox.Groups[1];
                //entItem.ForeColor = Settings.Default.SecondColor;
                entItem.UseItemStyleForSubItems = true;
                entitiesListBox.Items.Add(entItem);
            }

            foreach (string entname in commonEntitiesList)
            {
                ListViewItem entItem = new ListViewItem(entname);
                entItem.Group = entitiesListBox.Groups[2];
                //entItem.ForeColor = Settings.Default.CommonColor;
                entItem.UseItemStyleForSubItems = true;
                entitiesListBox.Items.Add(entItem);
            }
        }

        private void LoadSettings()
        {
            firstTreeView.BackColor = Settings.Default.FirstColor;
            secondTreeView.BackColor = Settings.Default.SecondColor;

            diffPropsPictureBox.BackColor = Settings.Default.DifferColor;
            absentPropsPictureBox.BackColor = Settings.Default.AbsentColor;
            currentCellTextBox.Visible = Settings.Default.ShowCurrentCellContentOnForm;

            LoadEntitiesToList();
        }

        private List<string> GetEntitiesNames(XmlDocument custXML)
        {
            XmlNodeList entitiesLstXML1 = custXML.DocumentElement.SelectNodes(commonEntitiesXMLPath + "/" + TAG_NAME);

            List<string> entLst = new List<string>();
            foreach (XmlNode entityNode in entitiesLstXML1)
            {
                string entityName = entityNode.Attributes.GetNamedItem(TAG_ORIGINALNAME).Value.ToString();//.InnerText;
                entLst.Add(entityName);
            }
            return entLst;
        }

        public void SetStatusMessage(string statusString)
        {
            statusLabel.Text = statusString;
        }
        
        private void LoadFormsToCompare()
        {
            _formAnalyzingPart = GetFormAnalyzingPart();

            LoadFormsToCompare(_formAnalyzingPart);
        }

        private void LoadFormsToCompare(string analyzingPart)
        {
            _formType = FormType.Main/* mainRadioButton.Checked ? FormType.Main : FormType.PreView*/;
            //diffFormsControls = 
            CompareForms(_formType, firstForms, secondForms,
                               out diffFormsData,
                               out diffFormsEvents,
                               out diffFormsCells,
                               out firstFormOriginalDataIds, out secondFormOriginalDataIds,
                               out firstFormOriginalControlsNames, out secondFormOriginalControlsNames,
                               out firstFormOriginalEventsNames, out secondFormOriginalEventsNames);

            //1.3.2. Forms elements Grids
            switch (_formAnalyzingPart)
            {
                case FormAnalyzingPart.Data:
                    LoadFormDataDiffGrid(firstFormDataGridView, firstForms[_formType], diffFormsData, firstFormOriginalDataIds);
                    LoadFormDataDiffGrid(secondFormDataGridView, secondForms[_formType], diffFormsData, secondFormOriginalDataIds);
                    break;
                case FormAnalyzingPart.Cells:
                    LoadFormCellsDiffGrid(firstFormDataGridView, firstForms[_formType], diffFormsCells, firstFormOriginalControlsNames);
                    LoadFormCellsDiffGrid(secondFormDataGridView, secondForms[_formType], diffFormsCells, secondFormOriginalControlsNames);
                    break;
                case FormAnalyzingPart.Events:
                    LoadFormEventsDiffGrid(firstFormDataGridView, firstForms[_formType], diffFormsEvents, firstFormOriginalEventsNames);
                    LoadFormEventsDiffGrid(secondFormDataGridView, secondForms[_formType], diffFormsEvents, secondFormOriginalEventsNames);
                    break;
            }
        }

        private string GetFormAnalyzingPart()
        {
            if (eventsRadioButton.Checked)
                return FormAnalyzingPart.Events;
            else if (dataRadioButton.Checked)
                return FormAnalyzingPart.Data;
            return FormAnalyzingPart.Cells;
        }

        #endregion
        
        #region Parse(Analyze) Methods

        private bool AnalyzeCustomizations()
        {
            SetStatusMessage(Strings.comparisonStarted);

            bool isFinished = true;

            if (String.IsNullOrEmpty(_currentEntity))
            {
                MessageBox.Show(Strings.selectEntity);
                isFinished = false;
            }
            else
            {
                //1. Detect Diffrence in Attributes, Fields, Forms properties                
                //Thread diffThread = new Thread(new ThreadStart(GetDifference));
                //diffThread.Start();

                //1.1                
                bool firstIsParsed = ParseCurrentEntityCustomizations(_firstCustXML, out firstAttributes, out firstFields, out firstForms, out firstAttrsFieldsFormsDS);
                bool secondIsParsed = ParseCurrentEntityCustomizations(_secoundCustXML, out secondAttributes, out secondFields, out secondForms, out secondAttrsFieldsFormsDS);

                if (firstIsParsed && secondIsParsed)
                {
                    //1.2 Detect Difference
                    diffAttrs = CompareAttributes(firstAttributes, secondAttributes,
                                                out firstOriginalAttrsNames, out secondOriginalAttrsNames);
                    diffFields = CompareFields(firstFields, secondFields,
                                               firstAttributes, secondAttributes,
                                               out firstOriginalFieldsNames, out secondOriginalFieldsNames);

                    ComparationFinishedEvent();

                    //1.3 Load Grids
                    SwitchGrids(analyzeTabControl);
                }
                else
                {
                    isFinished = false;
                    MessageBox.Show(String.Format(Strings.absentEntity, _currentEntity), Strings.warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    viewsElemsTabControl.SelectedIndex = 0;
                }
            }
            SetStatusMessage(Strings.comparisonFinish);

            return isFinished;
        }

        private DataSet AnalyzeEntityAttributes(XmlNode node, ref Dictionary<string, CustomizationAttribute> attributes)
        {
            //1. Attributes           
            if (node != null)
            {
                XmlNodeList attributesList = node.ChildNodes;
                foreach (XmlNode attrNode in attributesList)
                {
                    CustomizationAttribute attr = CustomizationAttribute.Load(attrNode.OuterXml);
                    string attrName = attr.PhysicalName.ToLower();//ToLower(), because field name is in lowcase
                    attributes.Add(attrName, attr);
                }

                //2. return DataSet with table["field"]
                DataSet attrs_fieldsDS = new DataSet();
                TextReader tr = new StringReader(node.OuterXml);
                attrs_fieldsDS.ReadXml(tr);
                return attrs_fieldsDS;
            }
            else
                return null;
           
        }

        private DataSet AnalyzeFields(XmlNode node, Dictionary<string, CustomizationAttribute> attributes, ref Dictionary<string, object/*simplefield*/> fields)
        {
            //1. Get fields dictionary
            if (node != null)
            {
                XmlNodeList fieldsList = node.ChildNodes;
                foreach (XmlNode fieldNode in fieldsList)
                {
                    string fldName = fieldNode.Attributes.GetNamedItem(fieldKeyProperty).Value.ToString().ToLower();
                    string attrType = attributes.ContainsKey(fldName) ? attributes[fldName].Type.ToLower() : AttributeType.None;
                    object fld = simplefield.Load(fieldNode.OuterXml, attrType);
                    //fld.Name.ToLower();                               
                    //fld.AttributeType = attrType;                            
                    if (fld != null)
                        fields.Add(fldName, fld);
                    else
                        MessageBox.Show(String.Format(Strings.fieldIncorrectFormat, fldName));
                }

                //2. return DataSet with table["field"]
                DataSet fieldsDS = new DataSet();
                TextReader tr = new StringReader(node.OuterXml);
                fieldsDS.ReadXml(tr);
                return fieldsDS;
            }
            return null;
        }

        private /*List<DataSet>*/ void AnalyzeForms(XmlNodeList formsnodes, ref Dictionary<string, CustomizationForm> forms)
        {
            List<DataSet> custForms = new List<DataSet>();

            //1. Analyze each form 
            foreach (XmlNode formNode in formsnodes)
            {
                string formType = formNode.Attributes.GetNamedItem(formKeyProperty).Value.ToString().ToLower();
                CustomizationForm form = CustomizationForm.Deserialize(formNode.OuterXml, formType);
                if (form != null)
                {
                    forms.Add(formType, form);

                    /*
                    DataSet formsDS = new DataSet();
                    TextReader tr = new StringReader("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + formNode.OuterXml);
                    formsDS.ReadXml(tr);
                    formsDS.Tables[0].TableName = formType;
                    custForms.Add(formsDS);
                   */
                }
                else
                    MessageBox.Show(Strings.formXMLIncorrect);
            }


           // return custForms;
        }
 
        private bool ParseCurrentEntityCustomizations(XmlDocument customizationDoc, 
                        out Dictionary<string, CustomizationAttribute> attributes, 
                        out Dictionary<string, object> fields,
                        out Dictionary<string, CustomizationForm> forms,
                        out DataSet attrs_fields_DS)
        {
            bool isParcedCustomization = true;

            attributes = new Dictionary<string, CustomizationAttribute>();
            fields = new Dictionary<string, object>();
            forms = new Dictionary<string, CustomizationForm>();

            attrs_fields_DS = new DataSet();

            XmlNode entityNode = customizationDoc.DocumentElement.SelectSingleNode(commonEntitiesXMLPath + "[" + TAG_NAME + "[@" + TAG_ORIGINALNAME + "='" + _currentEntity + "']]");

            if (entityNode != null)
            {
                //1. Attributes     
                XmlNode attrsNode = entityNode.SelectSingleNode(attributesEntitySubXMLPath);
                DataSet attrDS = AnalyzeEntityAttributes(attrsNode, ref attributes);

                //2. Fields
                XmlNode fieldsNode = entityNode.SelectSingleNode(fieldsEntitySubXMLPath);
                DataSet fieldsDS = AnalyzeFields(fieldsNode, attributes, ref fields);
                //AnalyzeFields(fieldsNode, ref fields);

                //3. Forms
                XmlNodeList formsNodes = entityNode.SelectNodes(formsEntitySubXMLPath);
                //List<DataSet> formsDS = 
                    AnalyzeForms(formsNodes, ref forms);

                //4. Create common DataSet:     attribute <=> field  <=> forms ???
                if ((attrDS != null) && (fieldsDS != null))
                {
                    attrs_fields_DS.Tables.Add(attrDS.Tables[TAG_ATTRIBUTE].Copy());
                    attrs_fields_DS.Tables[TAG_ATTRIBUTE].PrimaryKey = new DataColumn[] { attrs_fields_DS.Tables[TAG_ATTRIBUTE].Columns[attributeKeyProperty] };

                    foreach (DataTable fieldsTable in fieldsDS.Tables)
                    {
                        string tableName = fieldsTable.TableName;
                        attrs_fields_DS.Tables.Add(fieldsTable.Copy());
                        attrs_fields_DS.Tables[tableName].PrimaryKey = new DataColumn[] { attrs_fields_DS.Tables[tableName].Columns[fieldKeyProperty] };
                    }
                }
            }
            else
            {
                isParcedCustomization = false;
            }
            return isParcedCustomization;
        }

        #endregion

        #region Compare Methods

        /// <summary>
        /// Returns attributes, which have properties, differ by value
        /// </summary>
        /// <param name="firstList">fist list of attributes</param>
        /// <param name="secondList">second list of attributes<</param>
        /// <param name="firstOriginalAttrsNames">out</param>
        /// <param name="secondOriginalAttrsNames">out</param>
        /// <returns>{nameOfAttribute, DifferPropertiesDictionary} </returns>
        private Dictionary<string, DifferPropertiesDictionary> CompareAttributes(Dictionary<string, CustomizationAttribute> firstList, Dictionary<string, CustomizationAttribute> secondList,
                    out List<string> firstOriginalAttrsNames, out List<string> secondOriginalAttrsNames)
        {
            //1. Detect attributes do not have analogs in first/second list
            List<string> commonAttrsNames;
            CompareHelper.DetectCommonAndOriginalNames(firstList.Keys.GetEnumerator(), secondList.Keys.GetEnumerator(), 
                                out firstOriginalAttrsNames,
                                out secondOriginalAttrsNames,
                                out commonAttrsNames);

            //2. Detect attributes, differ by some properties value
            Dictionary<string, DifferPropertiesDictionary> differAttrs = new Dictionary<string, DifferPropertiesDictionary>();

            foreach (string attrname in commonAttrsNames)//secondList.Keys)
            {
                CustomizationAttribute firstAttr = firstList.ContainsKey(attrname) ? firstList[attrname] : null;
                CustomizationAttribute secondAttr = secondList.ContainsKey(attrname) ? secondList[attrname] : null;
        
                if (firstAttr != null)
                {                   
                    DifferPropertiesDictionary diffPropsList;
                    bool areDifferProps = CompareHelper.CompareAttributes(firstAttr, secondAttr, out diffPropsList);
                    if (areDifferProps)
                        differAttrs.Add(attrname, diffPropsList);
                }
            }
            return differAttrs;
        }



        private bool CompareForms(string formtype, Dictionary<string, CustomizationForm> firstForms, Dictionary<string, CustomizationForm> secondForms,
                                out Dictionary<string, DifferPropertiesDictionary> diffFormsData,
                                out Dictionary<KeyValuePair<string, bool>, DifferPropertiesDictionary> differFormEvents,
                                out Dictionary<string, DifferPropertiesDictionary> differFormCells,
                                out List<string> firstFormOriginalDataIds, out List<string> secondFormOriginalDataIds,
                                out List<string> firstFormOriginalControlsNames, out List<string> secondFormOriginalControlsNames,
                                out List<KeyValuePair<string, bool>> firstFormOriginalEventsNames, out List<KeyValuePair<string, bool>> secondFormOriginalEventsNames)
        {
            differFormEvents = new Dictionary<KeyValuePair<string, bool>, DifferPropertiesDictionary>();
            differFormCells = new Dictionary<string, DifferPropertiesDictionary>();

            bool areEqual = CompareForms(firstForms[formtype], secondForms[formtype],
                                    out diffFormsData,
                                    out diffFormsEvents,
                                    out diffFormsCells,
                                    out firstFormOriginalDataIds, out secondFormOriginalDataIds,
                                    out firstFormOriginalControlsNames, out secondFormOriginalControlsNames,
                                    out firstFormOriginalEventsNames, out secondFormOriginalEventsNames);
            return areEqual;
        }
        /// <summary>
        /// Returns forms events and controls are different: absent or have differ properties values
        /// </summary>
        /// <param name="formtype"></param>
        /// <param name="firstForm"></param>
        /// <param name="secondForm"></param>
        /// <param name="differFormEvents"></param>
        /// <param name="differFormCells"></param>
        /// <param name="firstFormOriginalControlsNames"></param>
        /// <param name="secondFormOriginalControlsNames"></param>
        /// <returns></returns>
        private /*Dictionary<string, DifferPropertiesDictionary>*/ bool CompareForms(
                                CustomizationForm firstForm, CustomizationForm secondForm,
                                out Dictionary<string, DifferPropertiesDictionary> diffFormData,
                                out Dictionary<KeyValuePair<string, bool>, DifferPropertiesDictionary> differFormEvents,
                                out Dictionary<string, DifferPropertiesDictionary> differFormCells,
                                out List<string> firstFormOriginalDataIds, out List<string> secondFormOriginalDataIds,
                                out List<string> firstFormOriginalControlsNames, out List<string> secondFormOriginalControlsNames,
                                out List<KeyValuePair<string, bool>> firstFormOriginalEventsNames, out List<KeyValuePair<string, bool>> secondFormOriginalEventsNames)
        {
            //1. Compare Forms Data lines
            List<string> commonDataIds;

            //1.1 Detect all form data (by controls id, included to them)
            Dictionary<string, FormData> firstDataList = new Dictionary<string, FormData>();
            Dictionary<string, FormData> secondDataList = new Dictionary<string, FormData>();

            foreach (FormData data in firstForm.Data)
            {
                if (!String.IsNullOrEmpty(data.ID))
                    firstDataList.Add(data.ID, data);
            }

            foreach (FormData data in secondForm.Data)
            {
                if (!String.IsNullOrEmpty(data.ID))
                    secondDataList.Add(data.ID, data);
            }

            CompareHelper.DetectCommonAndOriginalNames(firstDataList.Keys.GetEnumerator(), secondDataList.Keys.GetEnumerator(),
                                out firstFormOriginalDataIds,
                                out secondFormOriginalDataIds,
                                out commonDataIds);

            //1.2 Compare data by id => data properties difference
            diffFormData = new Dictionary<string, DifferPropertiesDictionary>();

            foreach (string dataid in commonDataIds)
            {
                FormData firstdata = firstDataList.ContainsKey(dataid) ? firstDataList[dataid] : null;
                FormData seconddata = secondDataList.ContainsKey(dataid) ? secondDataList[dataid] : null;

                if ((firstdata != null) && (seconddata != null))
                {
                    //2.2 Detect cell labels and scripts are different by value  
                    DifferPropertiesDictionary diffPropsList;
                    bool areDiffProps = CompareHelper.CompareFormData(firstdata, seconddata, out diffPropsList);
                    if (areDiffProps)
                        diffFormData.Add(dataid, diffPropsList);
                }
            }

            //2. Compare Forms Events scripts  
            List<KeyValuePair<string, bool>> commonEventsNames;
            //2.1
            differFormEvents = new Dictionary<KeyValuePair<string, bool>, DifferPropertiesDictionary>();

            Dictionary<KeyValuePair<string, bool>, FormEvent> firstFormEventsList = new Dictionary<KeyValuePair<string, bool>, FormEvent>();
            Dictionary<KeyValuePair<string, bool>, FormEvent> secondFormEventsList = new Dictionary<KeyValuePair<string, bool>, FormEvent>();

            foreach (FormEvent formevent in firstForm.Events)
            {
                if (!String.IsNullOrEmpty(formevent.Name))
                    // firstFormEventsList.Add(formevent.Name + "_" + formevent.Application, formevent);
                    firstFormEventsList.Add(new KeyValuePair<string, bool>(formevent.Name, formevent.Application), formevent);
            }

            foreach (FormEvent formevent in secondForm.Events)
            {
                if (!String.IsNullOrEmpty(formevent.Name))
                    secondFormEventsList.Add(new KeyValuePair<string, bool>(formevent.Name, formevent.Application), formevent);
            }

            CompareHelper.DetectCommonAndOriginalNames(firstFormEventsList.Keys.GetEnumerator(), secondFormEventsList.Keys.GetEnumerator(),
                               out firstFormOriginalEventsNames,
                               out secondFormOriginalEventsNames,
                               out commonEventsNames);


            foreach (KeyValuePair<string, bool> formevent_name_appl in commonEventsNames)
            {
                //FormEvent firstEvent = firstFormEventsList.ContainsKey(formeventname) ? firstFormEventsList[formeventname] : null;
                //FormEvent secondEvent = secondFormEventsList.ContainsKey(formeventname) ? secondFormEventsList[formeventname] : null;

                FormEvent firstEvent = firstFormEventsList.ContainsKey(formevent_name_appl) ? firstFormEventsList[formevent_name_appl] : null;
                FormEvent secondEvent = secondFormEventsList.ContainsKey(formevent_name_appl) ? secondFormEventsList[formevent_name_appl] : null;

                if ((firstEvent != null) && (secondEvent != null))
                {
                    //2.2 Detect forms events properties are different by value  
                    DifferPropertiesDictionary diffPropsList;
                    bool areDiffProps = CompareHelper.CompareFormEvents(firstEvent, secondEvent, out diffPropsList);
                    if (areDiffProps)
                        differFormEvents.Add(formevent_name_appl, diffPropsList);
                }
            }

            //2. Compare forms cells
            //2.1 Detect all form cells (by controls id, included to them)
            List<string> commonControlsNames;
            differFormCells = new Dictionary<string, DifferPropertiesDictionary>();

            Dictionary<string, Cell> firstCellsList = new Dictionary<string, Cell>();
            Dictionary<string, Cell> secondCellsList = new Dictionary<string, Cell>();


            foreach (Tab tab in firstForm.Tabs)
                foreach (Section section in tab.Sections)
                    foreach (Row row in section.Rows)
                        foreach (Cell cell in row.Cells)
                        {
                            if (!String.IsNullOrEmpty(cell.Control.DataFieldName))
                                firstCellsList.Add(cell.Control.DataFieldName, cell);
                        }

            foreach (Tab tab in secondForm.Tabs)
                foreach (Section section in tab.Sections)
                    foreach (Row row in section.Rows)
                        foreach (Cell cell in row.Cells)
                        {
                            if (!String.IsNullOrEmpty(cell.Control.DataFieldName))
                                secondCellsList.Add(cell.Control.DataFieldName, cell);
                        }

            //2.2 Detect (cells) controls do not have analogs in first/second list
            CompareHelper.DetectCommonAndOriginalNames(firstCellsList.Keys.GetEnumerator(), secondCellsList.Keys.GetEnumerator(),
                                out firstFormOriginalControlsNames,
                                out secondFormOriginalControlsNames,
                                out commonControlsNames);

            //2.3. Compare cells by control id => detect cells difference
            //2.3.1 Labels
            //2.3.2 OnChange event
            foreach (string ctrlid in commonControlsNames)
            {
                Cell firstCell = firstCellsList.ContainsKey(ctrlid) ? firstCellsList[ctrlid] : null;
                Cell secondCell = secondCellsList.ContainsKey(ctrlid) ? secondCellsList[ctrlid] : null;

                if ((firstCell != null) && (secondCell != null))
                {
                    //2.2 Detect cell labels and scripts are different by value  
                    DifferPropertiesDictionary diffPropsList;
                    bool areDiffProps = CompareHelper.CompareCells(firstCell, secondCell, out diffPropsList);
                    if (areDiffProps)
                        differFormCells.Add(ctrlid, diffPropsList);
                }
            }

            return (diffFormData.Count + differFormEvents.Count + differFormCells.Count > 0) ? true : false;
        }


        private Dictionary<string, DifferPropertiesDictionary> CompareFields(
                        Dictionary<string, object> firstList, Dictionary<string, object> secondList,
                        Dictionary<string, CustomizationAttribute> attributesFirstList, Dictionary<string, CustomizationAttribute> attributesSecondList,
                        out List<string> firstOriginalFieldsNames,
                        out List<string> secondOriginalFieldsNames)
        {
            //1. Detect fields do not have analogs in first/second list
            List<string> commonFieldsNames;

            CompareHelper.DetectCommonAndOriginalNames(firstList.Keys.GetEnumerator(), secondList.Keys.GetEnumerator(),
                                out firstOriginalFieldsNames,
                                out secondOriginalFieldsNames,
                                out commonFieldsNames);

            Dictionary<string, DifferPropertiesDictionary> differFields = new Dictionary<string, DifferPropertiesDictionary>();

            //2. Detect fields properties, which are different by value  
            foreach (string fldname in commonFieldsNames)
            {
                object firstFld = firstList.ContainsKey(fldname) ? firstList[fldname] : null;
                object/*simplefield*/ secondFld = secondList.ContainsKey(fldname) ? secondList[fldname] : null;

                if ((firstFld != null) && (secondFld != null))
                {
                    //2.1 Get field (corresponding attribute) type
                    CustomizationAttribute correspondAttr1 = attributesFirstList.ContainsKey(fldname) ? attributesFirstList[fldname] : null;
                    CustomizationAttribute correspondAttr2 = attributesSecondList.ContainsKey(fldname) ? attributesSecondList[fldname] : null;
                    string attrTypeName1 = (correspondAttr1 != null)? correspondAttr1.Type: AttributeType.None;
                    string attrTypeName2 = (correspondAttr2 != null) ? correspondAttr2.Type : AttributeType.None;
                    

                    //2.2 Detect field properties are different by value  
                    DifferPropertiesDictionary diffPropsList;
                    bool areDiffProps = CompareHelper.CompareFields(firstFld, secondFld, attrTypeName1, attrTypeName2, out diffPropsList);
                    if (areDiffProps)
                        differFields.Add(fldname, diffPropsList);
                }
            }

            return differFields;
        }

        private void OnCompareFinished()
        {
            reportButton.Enabled = true;
            diffButton.Enabled = true;
        }

        #endregion

        #region Load Grids

        private void LoadAttributesDiffGrid(DataGridView grid, DataSet attr_field_DS,
                        Dictionary<string, DifferPropertiesDictionary> diffElemsProps,
                        List<string> originalElemsNames)
        {
            string tablename = TAG_ATTRIBUTE;
            string key = attributeKeyProperty;
            if (attr_field_DS.Tables[tablename] != null)
            {
                attr_field_DS.Tables[tablename].PrimaryKey = new DataColumn[] { attr_field_DS.Tables[tablename].Columns[key] };
                attr_field_DS.Tables[tablename].Columns[key].Unique = true;
                attr_field_DS.Tables[tablename].Columns[key].SetOrdinal(0);

                //3. Load ds to DataGrid
                DataView dv = new DataView(attr_field_DS.Tables[tablename]);
                dv.Sort = key + " ASC";

                //Filter values          
                if (_showOnlyUpdated)
                {                    
                    string filter = GetFilterString(key, diffElemsProps, originalElemsNames);
                    if (!String.IsNullOrEmpty(filter))
                        dv.RowFilter = filter;
                    else
                        dv = null;
                }

                grid.DataSource = dv;
                ColorCells(dv, grid, diffElemsProps, originalElemsNames);
                grid.Refresh();
            }
        }

      
        private void LoadFieldsDiffGrid(DataGridView grid, Dictionary<string, CustomizationAttribute> attributes,
                     Dictionary<string, object> fields,                   
                     Dictionary<string, DifferPropertiesDictionary> diffElemsProps,
                     List<string> originalElemsNames)
        {
            
                string tablename = TAG_FIELD;
                string key = fieldKeyProperty;

                DataSet fieldsDS = new DataSet();
                fieldsDS.Tables.Add(tablename);

                //1. Create DS Columns
                foreach (string fieldName in fields.Keys)
                {
                    string attrTypeName = attributes.ContainsKey(fieldName)?attributes[fieldName].Type: AttributeType.None;
                    Type fieldType = CompareHelper.GetFieldTypeByAttributeTypeName(attrTypeName);
                    PropertyInfo[] props = fieldType.GetProperties();
                    foreach (PropertyInfo prop in props)
                    {
                        if (!fieldsDS.Tables[tablename].Columns.Contains(prop.Name))
                        {
                            DataColumn newCol = new DataColumn(prop.Name);
                            fieldsDS.Tables[tablename].Columns.Add(newCol);
                        }
                    }
                }
        
                //2. Load fields data         
                foreach (string fieldName in fields.Keys)
                {
                        object field = fields[fieldName];
                        if (field != null)
                        {                           
                            DataRow row = fieldsDS.Tables[tablename].NewRow();
                            string attrTypeName = attributes.ContainsKey(fieldName)?attributes[fieldName].Type: AttributeType.None;
                            Type fieldType = CompareHelper.GetFieldTypeByAttributeTypeName(attrTypeName);
                            PropertyInfo[] props = fieldType.GetProperties();
                            foreach (PropertyInfo prop in props)
                            {                              
                                object fldVal = prop.GetValue(field, null);
                                row[prop.Name] = CompareHelper.ToString(fldVal);
                            }
                            fieldsDS.Tables[tablename].Rows.Add(row);
                        }
                        else
                            MessageBox.Show(fieldName);

                }

                fieldsDS.Tables[tablename].PrimaryKey = new DataColumn[] { fieldsDS.Tables[tablename].Columns[key] };
                fieldsDS.Tables[tablename].Columns[key].Unique = true;
                fieldsDS.Tables[tablename].Columns[key].SetOrdinal(0);

                //3. Load ds to DataGrid
                DataView dv = new DataView(fieldsDS.Tables[tablename]);
                dv.Sort = key + " ASC";
                if (_showOnlyUpdated)
                {                 
                    string filter = GetFilterString(key, diffElemsProps, originalElemsNames);
                    if (!String.IsNullOrEmpty(filter))
                        dv.RowFilter = filter;
                    else
                        dv = null;
                }
                        //|| (originalElemsNames.Contains(fieldName) || (diffElemsProps.ContainsKey(fieldName) && _showOnlyUpdated)))
                    
                grid.DataSource = dv;
                ColorCells(dv, grid, diffElemsProps, originalElemsNames);
                grid.Refresh();
               
        }


        private void LoadFormEventsDiffGrid(
                        DataGridView grid,
                        CustomizationForm form,
                        Dictionary<KeyValuePair<string, bool>, DifferPropertiesDictionary> diffElemsProps,
                        List<KeyValuePair<string, bool>> originalEventsNames)
        {
            string tablename = FormAnalyzingPart.Events;
            string key_first = eventFormKey1Property;
            string key_second = eventFormKey2Property;

            DataSet formsDS = new DataSet();
            formsDS.Tables.Add(tablename);

            //1. Create  Columns of DS
            PropertyInfo[] props = typeof(FormEvent).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (!formsDS.Tables[tablename].Columns.Contains(prop.Name))
                {
                    DataColumn newCol = new DataColumn(prop.Name);
                    formsDS.Tables[tablename].Columns.Add(newCol);
                }
            }

            //2. Load data to DataSet    
            foreach (FormEvent formevent in form.Events)
            {
                DataRow row = formsDS.Tables[tablename].NewRow();
                foreach (PropertyInfo prop in props)
                {
                    object eventPropVal = prop.GetValue(formevent, null);
                    row[prop.Name] = CompareHelper.ToString(eventPropVal);
                }
                //row[key] = formsDS.Tables[tablename].Rows.Count.ToString() + ": " + row[key];
                formsDS.Tables[tablename].Rows.Add(row);
            }

            formsDS.Tables[tablename].PrimaryKey = new DataColumn[] { formsDS.Tables[tablename].Columns[key_first], formsDS.Tables[tablename].Columns[key_second] };
            //formsDS.Tables[tablename].Columns[key].Unique = true;
            formsDS.Tables[tablename].Columns[key_first].SetOrdinal(0);

            //3. Load ds to DataGrid
            DataView dv = new DataView(formsDS.Tables[tablename]);
            dv.Sort = key_first + " ASC, " + key_second + " ASC";
            if (_showOnlyUpdated)
            {
                string filter = GetFilterString(key_first, key_second, diffElemsProps, originalEventsNames);
                if (!String.IsNullOrEmpty(filter))
                    dv.RowFilter = filter;
                else
                    dv = null;
            }

            grid.DataSource = dv;
            ColorCells(dv, grid, diffElemsProps, originalEventsNames);
            grid.Refresh();
        }


        private void LoadFormDataDiffGrid(
                 DataGridView grid,
                 CustomizationForm form,
                 Dictionary<string, DifferPropertiesDictionary> diffElemsProps,
                 List<string> originalDataIds)
        {
            string tablename = FormAnalyzingPart.Data;
            string key = dataFormKeyProperty;

            DataSet formsDS = new DataSet();
            formsDS.Tables.Add(tablename);

            //1. Create  Columns of DS
            PropertyInfo[] props = typeof(FormData).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (!formsDS.Tables[tablename].Columns.Contains(prop.Name))
                {
                    DataColumn newCol = new DataColumn(prop.Name);
                    formsDS.Tables[tablename].Columns.Add(newCol);
                }
            }

            //2. Load data to DataSet    
            foreach (FormData dataline in form.Data)
            {
                DataRow row = formsDS.Tables[tablename].NewRow();                
                //And for other props
                foreach (PropertyInfo prop in props)
                {
                    object dataPropVal = prop.GetValue(dataline, null);
                    row[prop.Name] = CompareHelper.ToString(dataPropVal);
                }
                formsDS.Tables[tablename].Rows.Add(row);
            }

            formsDS.Tables[tablename].PrimaryKey = new DataColumn[] { formsDS.Tables[tablename].Columns[key] };
            formsDS.Tables[tablename].Columns[key].Unique = true;
            formsDS.Tables[tablename].Columns[key].SetOrdinal(0);

            //3. Load ds to DataGrid
            DataView dv = new DataView(formsDS.Tables[tablename]);
            dv.Sort = key + " ASC";
            if (_showOnlyUpdated)
            {
                string filter = GetFilterString(key, diffElemsProps, originalDataIds);
                if (!String.IsNullOrEmpty(filter))
                    dv.RowFilter = filter;
                else
                    dv = null;
            }              

            grid.DataSource = dv;
            ColorCells(dv, grid, diffElemsProps, originalDataIds);
            grid.Refresh();
        }      


        private void LoadFormCellsDiffGrid(
                        DataGridView grid, 
                        CustomizationForm form,                          
                        Dictionary<string, DifferPropertiesDictionary> diffElemsProps,
                        List<string> originalControls)
        {
            string tablename = FormAnalyzingPart.Cells;
            string key = cellControlKeyProperty;

            DataSet formsDS = new DataSet();
            formsDS.Tables.Add(tablename);

            //1. Create  Columns of DS
            PropertyInfo[] props = typeof(Cell).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (!formsDS.Tables[tablename].Columns.Contains(prop.Name))
                {
                    DataColumn newCol = new DataColumn(prop.Name);
                    formsDS.Tables[tablename].Columns.Add(newCol);
                }
            }
            //Add column with control included to cell !!!!!!!!!!!!!!!!!!!!
            formsDS.Tables[tablename].Columns.Add(key);
        
            // Accumulate all cells of the form
            List<Cell> cells = new List<Cell>();
            foreach (Tab tab in form.Tabs)
                foreach (Section section in tab.Sections)
                    foreach (Row row in section.Rows)
                        foreach (Cell cell in row.Cells)
                        {
                            if (!String.IsNullOrEmpty(cell.Control.DataFieldName))
                                cells.Add(cell);
                        }

            //2. Load data to DataSet    
            foreach (Cell cell in cells)
            { 
                    DataRow row = formsDS.Tables[tablename].NewRow();
                    row[key] = cell.Control.DataFieldName; //Denote cell with control !!!!!!!!!!!!!!
                    //And for other props
                    foreach (PropertyInfo prop in props)
                    {                      
                        object cellPropVal = prop.GetValue(cell, null);
                        row[prop.Name] = CompareHelper.ToString(cellPropVal);
                    }
                    formsDS.Tables[tablename].Rows.Add(row);                
            }

            formsDS.Tables[tablename].PrimaryKey = new DataColumn[] { formsDS.Tables[tablename].Columns[key] };
            formsDS.Tables[tablename].Columns[key].Unique = true;
            formsDS.Tables[tablename].Columns[key].SetOrdinal(0);    

            //3. Load ds to DataGrid
            DataView dv = new DataView(formsDS.Tables[tablename]);
            dv.Sort = key + " ASC";
            if (_showOnlyUpdated)
            {
                string filter = GetFilterString(key, diffElemsProps, originalControls);
                if (!String.IsNullOrEmpty(filter))
                    dv.RowFilter = filter;
                else
                    dv = null;
            }             

            grid.DataSource = dv;
            ColorCells(dv, grid, diffElemsProps, originalControls);
            grid.Refresh();
        }      


        private string GetFilterString(string key, Dictionary<string, DifferPropertiesDictionary> diffElemsProps, List<string> originalElemsNames)
        {
                string filterExpr = "";
                int counter = 0;
                List<string> diffElemsList = new List<string>();
                diffElemsList.AddRange(diffElemsProps.Keys);
                diffElemsList.AddRange(originalElemsNames);
                foreach (string name in diffElemsList)
                {
                    filterExpr += " '" + name + "'";
                    if (counter < diffElemsList.Count - 1) filterExpr += ", ";                    
                    counter++;
                }
                if (!String.IsNullOrEmpty (filterExpr))
                    filterExpr = key + " IN (" + filterExpr + ")";
            return filterExpr;
        }

        /// <summary>
        /// For Form Events
        /// </summary>
        /// <param name="first_key"></param>
        /// <param name="second_key"></param>
        /// <param name="diffElemsProps"></param>
        /// <param name="originalElemsNames"></param>
        /// <returns></returns>
        private string GetFilterString(string first_key, string second_key, Dictionary<KeyValuePair<string, bool>, DifferPropertiesDictionary> diffElemsProps, List<KeyValuePair<string, bool>> originalElemsNames)
        {
            string filterExpr = "";
            int counter = 0;

            foreach (KeyValuePair<string, bool> formevent in diffElemsProps.Keys)
            {
                filterExpr += " (" + first_key + "='" + formevent.Key + "' AND " + second_key + "='" + formevent.Value + "')";
                if (counter < diffElemsProps.Keys.Count - 1) filterExpr += "OR ";
                counter++;
            }

            return filterExpr;
        }



        private void ColorCells(DataView dv, DataGridView gridView, 
                                Dictionary<string, DifferPropertiesDictionary> diffElems,
                                List<string> originalElemsNames)
        {
            //1. Color original elements(attributes|fields)
            ColorOriginalCells(dv, gridView, originalElemsNames);

            //2. Color cells, - element(attribute|field) properties, - which are differ by values             
            ColorDifferPropertiesCells(dv, gridView, diffElems);
        }


        /// <summary>
        /// For Form events
        /// </summary>
        /// <param name="dv"></param>
        /// <param name="gridView"></param>
        /// <param name="diffElems"></param>
        /// <param name="originalElemsNames"></param>
        private void ColorCells(DataView dv, DataGridView gridView,
                        Dictionary<KeyValuePair<string, bool>, DifferPropertiesDictionary> diffElems,
                        List<KeyValuePair<string, bool>> originalElemsNames)
        {
            //1. Color original elements(attributes|fields)
            ColorOriginalCells(dv, gridView, originalElemsNames);

            //2. Color cells, - element(attribute|field) properties, - which are differ by values             
            ColorDifferPropertiesCells(dv, gridView, diffElems);
        }

        private void ColorOriginalCells(DataView dv, DataGridView gridView, List<string> originalElemsNames)
        {
            foreach (string originalElemKey in originalElemsNames)
            {
                int originalRowIndex = dv.Find(originalElemKey);
                if (originalRowIndex > -1)
                    gridView.Rows[originalRowIndex].DefaultCellStyle.BackColor = Settings.Default.AbsentColor;
            }
        }

        /// <summary>
        /// Form events colorize
        /// </summary>
        /// <param name="dv"></param>
        /// <param name="gridView"></param>
        /// <param name="originalElemsNames"></param>
        private void ColorOriginalCells(DataView dv, DataGridView gridView, List<KeyValuePair<string, bool>> originalElemsNames)
        {
            if (dv != null)
            {
                foreach (KeyValuePair<string, bool> originalElemKey in originalElemsNames)
                {
                    int originalRowIndex = dv.Find(new object[2] { originalElemKey.Key, originalElemKey.Value });
                    if (originalRowIndex > -1)
                        gridView.Rows[originalRowIndex].DefaultCellStyle.BackColor = Settings.Default.AbsentColor;
                }
            }
        }


        private void ColorDifferPropertiesCells(DataView dv, DataGridView gridView,
                        Dictionary<string, DifferPropertiesDictionary> diffElems)
        {
            //Color cells, - element(attribute|field) properties, - which are differ by values             
            foreach (string diffElemKey in diffElems.Keys)
            {
                DifferPropertiesDictionary props = diffElems[diffElemKey];
                int diffRowIndex = dv.Find(diffElemKey);               

                if ((diffRowIndex > -1) && (props != null))
                {
                    IEnumerator ien = props.Items.Keys.GetEnumerator();
                    while (ien.MoveNext())
                    {
                        string propName = ien.Current.ToString();
                        //
                        if (gridView.Columns.Contains(propName))
                        {
                            //Collorize differ property cell
                            if (gridView[propName, diffRowIndex] != null)
                                gridView[propName, diffRowIndex].Style.BackColor = Settings.Default.DifferColor;
                        }
                    }

                }

            }
        }


        /// <summary>
        /// Form events proprties colorize
        /// </summary>
        /// <param name="dv"></param>
        /// <param name="gridView"></param>
        /// <param name="diffElems"></param>
        private void ColorDifferPropertiesCells(DataView dv, DataGridView gridView,
                        Dictionary<KeyValuePair<string, bool>, DifferPropertiesDictionary> diffElems)
        {
            //Color cells, - element(attribute|field) properties, - which are differ by values             
            foreach (KeyValuePair<string, bool> diffElemKey in diffElems.Keys)
            {
                DifferPropertiesDictionary props = diffElems[diffElemKey];
                int diffRowIndex = dv.Find(new object[2] { diffElemKey.Key, diffElemKey.Value });

                if ((diffRowIndex > -1) && (props != null))
                {
                    IEnumerator ien = props.Items.Keys.GetEnumerator();
                    while (ien.MoveNext())
                    {
                        string propName = ien.Current.ToString();
                        //
                        if (gridView.Columns.Contains(propName))
                        {
                            //Collorize differ property cell
                            if (gridView[propName, diffRowIndex] != null)
                                gridView[propName, diffRowIndex].Style.BackColor = Settings.Default.DifferColor;
                        }
                    }

                }

            }
        }


        private void ReColorDataGridView(DataGridView gridView, string keyProperty, Dictionary<string, DifferPropertiesDictionary> diffElems, List<string> originalElems)
        {
            DataGridViewRowCollection rows = gridView.Rows;
            foreach (DataGridViewRow row in rows)
            {
                string elemname = row.Cells[keyProperty].Value.ToString();
                if (originalElems.Contains(elemname))
                {
                    row.DefaultCellStyle.BackColor = Settings.Default.AbsentColor;
                }
                else if (diffElems.ContainsKey(elemname.ToLower()))
                {
                    int diffRowIndex = row.Index;
                    DifferPropertiesDictionary props = diffElems[elemname.ToLower()];
                    IEnumerator ien = props.Items.Keys.GetEnumerator();
                    while (ien.MoveNext())
                    {
                        string propName = ien.Current.ToString();
                        if (gridView.Columns.Contains(propName))
                        {
                            //Collorize differ property cell
                            if (gridView[propName, diffRowIndex] != null)
                                gridView[propName, diffRowIndex].Style.BackColor = Settings.Default.DifferColor;
                        }
                    }

                }

            }

        }

        private void AutoNavigateCrid(string key, DataGridView fromDGV, DataGridView toDGV)
        {
            if (Settings.Default.AutoNavigateSelectedRow && (fromDGV.SelectedRows.Count > 0))
            {
                //1. Get first key =[attribute name]
                string compareFieldKey = fromDGV.SelectedRows[0].Cells[key].Value as string;

                if (toDGV.DataSource != null)
                {
                    int currentRowIndexOffset = fromDGV.SelectedRows[0].Index - fromDGV.FirstDisplayedScrollingRowIndex;

                    //2. Navigate to correspond row in second grid
                    DataView secondDV = toDGV.DataSource as DataView;
                    if (secondDV != null)
                    {
                        int secondRowIndex = secondDV.Find(compareFieldKey);
                        if (secondRowIndex == -1)
                        {
                            if (toDGV.SelectedRows.Count > 0) toDGV.SelectedRows[0].Selected = false;
                        }
                        else
                        {
                            toDGV.Rows[secondRowIndex].Selected = true; //Select correspond row
                            toDGV.FirstDisplayedScrollingRowIndex = secondRowIndex;
                            /*
                            //3. Set first row opposite second row
                            int secondDisplIndex = secondRowIndex;
                            if (secondRowIndex >= currentRowIndexOffset)
                                secondDisplIndex -= currentRowIndexOffset;
                            toDGV.FirstDisplayedScrollingRowIndex = secondDisplIndex;
                            */
                        }
                    }
                }

            }
        }

        private void SwitchGrids(TabControl tabControl)
        {
            if ((firstAttrsFieldsFormsDS != null) && (secondAttrsFieldsFormsDS != null) && (tabControl.SelectedTab.Tag != null))
            {
                if (tabControl.SelectedTab.Tag.Equals(ComparisonType.attributes.ToString()))
                {
                    LoadAttributesDiffGrid(firstAttributesDataGridView, firstAttrsFieldsFormsDS, diffAttrs, firstOriginalAttrsNames);
                    LoadAttributesDiffGrid(secondAttributesDataGridView, secondAttrsFieldsFormsDS, diffAttrs, secondOriginalAttrsNames);
                }
                else if (tabControl.SelectedTab.Tag.Equals(ComparisonType.fields.ToString()))
                {
                    LoadFieldsDiffGrid(firstFieldsDataGridView, firstAttributes, firstFields, diffFields, firstOriginalFieldsNames);
                    LoadFieldsDiffGrid(secondFieldsDataGridView, secondAttributes, secondFields, diffFields, secondOriginalFieldsNames);
                }
                else if (tabControl.SelectedTab.Tag.Equals(ComparisonType.forms.ToString()))
                {
                    LoadFormsToCompare();
                }
            }

        }

        private void LoadWordPad(object sender)
        {
            try
            {
                DataGridView currentGrid = sender as DataGridView;

                if (currentGrid != null)
                {
                    string path = Path.GetFullPath("currCell.txt");
                    if (!File.Exists(path))
                    {
                        FileStream fs = File.Create(path);
                        fs.Close();
                    }

                    StreamWriter file = new StreamWriter(path);
                    file.WriteLine(currentGrid.CurrentCell.Value.ToString());
                    file.Close();

                    //2. Start wordpad
                    System.Diagnostics.Process wordPad = new System.Diagnostics.Process();
                    if (!((wordpadpath != null) && (wordpadpath.Length > 0)))
                    {
                        string[] localDirs = Directory.GetLogicalDrives();
                        foreach (string drive in localDirs)
                        {
                            wordpadpath = Directory.GetFiles(drive + "Program Files", "WORDPAD.EXE", SearchOption.AllDirectories);
                            if (wordpadpath.Length > 0)
                                break;
                        }
                    }

                    if (wordpadpath.Length > 0)
                    {
                        wordPad.StartInfo.FileName = wordpadpath[0];
                        //@"C:\Program Files\Windows NT\Accessories\WORDPAD.EXE";
                        wordPad.StartInfo.Arguments = path;
                        wordPad.StartInfo.UseShellExecute = false;
                        wordPad.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowCurrentCellContent(DataGridView dataGridView)
        {
            if ((Settings.Default.ShowCurrentCellContentOnForm) && (dataGridView.CurrentCell != null) && (dataGridView.CurrentCell.Value != null))
                currentCellTextBox.Text = dataGridView.CurrentCell.Value.ToString();
        }

        #endregion

        #region Tree Methods
        /// <summary>
        /// For all entities
        /// </summary>
        /// <param name="customizationDoc"></param>
        /// <param name="tree"></param>
        private void BuildEntitiesTree(XmlDocument customizationDoc, TreeView tree)
        {
            tree.Nodes.Clear();
            
            XmlNodeList entitiesNodes = customizationDoc.DocumentElement.SelectNodes(commonEntitiesXMLPath);            
            foreach (XmlNode entityNode in entitiesNodes)
            {
                string entityName = entityNode.SelectSingleNode(TAG_NAME).Attributes[TAG_ORIGINALNAME].Value.ToString();//.InnerText;
                TreeNode rootNode = new TreeNode(entityName);
                tree.Nodes.Add(rootNode);
                rootNode.Name = entityName;
                rootNode.Tag = entityName;

                BuildSubTree(rootNode, entityNode);               
            }
            tree.Sort();
        }


        private TreeNode BuildSubTree(TreeNode parentTreeNode, XmlNode xmlNode)
        {
            XmlNodeList mainNodes = xmlNode.ChildNodes;
            foreach (XmlNode node in mainNodes)
            {
                TreeNode treeNode = new TreeNode(GetNodeAttributesText(node));
                if (node.ChildNodes.Count > 0)
                {
                    treeNode.Name = parentTreeNode.Name + "/" + node.LocalName;
                    treeNode.Tag = xmlNode;

                    if (node.ChildNodes[0].NodeType != XmlNodeType.Text)
                        BuildSubTree(treeNode, node);
                    else
                        treeNode.Text = treeNode.Text + node.ChildNodes[0].Value + "</" + node.Name + ">";
                }
                parentTreeNode.Nodes.Add(treeNode);
            }

            return parentTreeNode;
        }

        private string GetNodeAttributesText(XmlNode node)
        {
            string attrStr = "";

            if (node.NodeType == XmlNodeType.Text)
                attrStr = node.Value;
            else
            {
                attrStr = "<" + node.LocalName;

                if (node.Attributes != null)
                    foreach (XmlAttribute attr in node.Attributes)
                    {
                        attrStr += (" " + attr.Name + "=" + attr.Value);
                    }
                attrStr += (node.ChildNodes.Count > 0) ? ">" : "/>";
            }
            return attrStr;
        }

        private void NavigateToEntityOnTree(TreeView tree, string entityName)
        {
            tree.CollapseAll();
            tree.SelectedNode = FindTreeNodeByName(tree, entityName);
            if (tree.SelectedNode != null)
            {
                tree.SelectedNode.Expand();              
            }
        }

        private TreeNode FindTreeNodeByName(TreeView tree, string name)
        {
            TreeNode searchNode = null;
            foreach (TreeNode node in tree.Nodes)
            {
                if (node.Name.CompareTo(name) == 0)
                {
                    searchNode = node;
                    break;
                }
                else
                {
                    searchNode = FindTreeNodeByName(node, name);
                    if (searchNode != null) break;
                }
            }
            return searchNode;
        }

        private TreeNode FindTreeNodeByName(TreeNode mainnode, string name)
        {
            TreeNode searchNode = null;
            foreach (TreeNode node in mainnode.Nodes)
            {
                if (node.Name.CompareTo(name) == 0)
                {
                    searchNode = node;
                    break;
                }
                else
                {
                    searchNode = FindTreeNodeByName(node, name);
                    if (searchNode != null) break;
                }
            }
            return searchNode;
        }

        #endregion      

        #region Reporting 

        /// <summary>
        ///  Load different elemements(attributes|fields|forms) data to report
        /// </summary>
        /// <param name="repType"></param>
        private void OpenReport(string repTypeStr)
        {
            ComparisonType repType = repTypeStr.Equals(ComparisonType.attributes.ToString()) ? ComparisonType.attributes :
                (repTypeStr.Equals(ComparisonType.fields.ToString()) ? ComparisonType.fields : ComparisonType.forms);
            try
            {
                switch (repType)
                {
                    case ComparisonType.attributes:
                        //1.1. Generate DataSet
                        DataSet diffAttrsDS = GenerateReportDataSet(diffAttrs);

                        //1.2. Fill report data
                        if ((diffAttrsDS != null) || (firstOriginalAttrsNames.Count > 0) || (secondOriginalAttrsNames.Count > 0))
                        {
                            ReportDialog repForm = new ReportDialog(ReportHeader.Attributes, diffAttrsDS, firstOriginalAttrsNames, secondOriginalAttrsNames);
                            repForm.ShowDialog();
                        }
                        break;

                    case ComparisonType.fields:
                        //2.1 Generate DataSet 
                        //___$+________CLEAR(NOT INCLUDE) columns "CustomProperties" and "CustomAttributes" from report
                        string custAttrsName = simplefield.GetCustomAttributeCollectionName();
                        string custPropsName = simplefield.GetCustomPropertyCollectionName();

                        Dictionary<string, DifferPropertiesDictionary> diffFieldsWithoutCustom = new Dictionary<string, DifferPropertiesDictionary>();
                        foreach (string fldName in diffFields.Keys)
                        {
                            Dictionary<string, CompareItem> diffItems = diffFields[fldName].Items;
                            DifferPropertiesDictionary diffPropsClear = new DifferPropertiesDictionary();

                            foreach (string propName in diffItems.Keys)
                            {
                                if ((!propName.Equals(custAttrsName)) && (!propName.Equals(custPropsName)))
                                    diffPropsClear.Add(propName, diffItems[propName]);
                            }

                            diffFieldsWithoutCustom.Add(fldName, diffPropsClear);
                        }
                        //_____$+
                        DataSet diffFieldsDS = GenerateReportDataSet(diffFieldsWithoutCustom);
                        //2.2. Fill report data
                        if ((diffFieldsDS != null) || (firstOriginalFieldsNames.Count > 0) || (secondOriginalFieldsNames.Count > 0))
                        {
                            ReportDialog repForm = new ReportDialog(ReportHeader.Fields, diffFieldsDS, firstOriginalFieldsNames, secondOriginalFieldsNames);
                            repForm.ShowDialog();
                        }
                        break;

                    case ComparisonType.forms:
                        //3.1.1_Cells
                        DataSet diffCellsDS = GenerateReportDataSet(diffFormsCells);

                        //3.1.2_Events
                        DataSet diffEventsDS = GenerateReportDataSet(diffFormsEvents);

                        //3.1.3_Data
                        DataSet diffDataDS = GenerateReportDataSet(diffFormsData);

                        //3.2. Fill report data                      
                        if ((_formAnalyzingPart.Equals(FormAnalyzingPart.Cells)) &&
                            ((diffCellsDS != null) || (firstFormOriginalControlsNames.Count > 0) || (secondFormOriginalControlsNames.Count > 0)))
                        {
                            ReportDialog repForm = new ReportDialog(ReportHeader.FormCells, diffCellsDS, firstFormOriginalControlsNames, secondFormOriginalControlsNames);
                            //repForm.ShowDialog();
                        }
                        else if ((_formAnalyzingPart.Equals(FormAnalyzingPart.Events)) &&
                            ((diffEventsDS != null) || (firstFormOriginalEventsNames.Count > 0) || (secondFormOriginalEventsNames.Count > 0)))
                        {
                            //ReportDialog repForm = new ReportDialog(ReportHeader.FormEvents, diffEventsDS, firstFormOriginalEventsNames, secondFormOriginalEventsNames);
                            // repForm.ShowDialog();
                        }
                        else if ((_formAnalyzingPart.Equals(FormAnalyzingPart.Data)) &&
                            ((diffDataDS != null) || (firstFormOriginalDataIds.Count > 0) || (secondFormOriginalDataIds.Count > 0)))
                        {
                            ReportDialog repForm = new ReportDialog(ReportHeader.FormData, diffDataDS, firstFormOriginalDataIds, secondFormOriginalDataIds);                           
                            repForm.ShowDialog();
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DataSet GenerateReportDataSet(Dictionary<string, DifferPropertiesDictionary> diffElems)
        {
            DataSet diffDS = null;
            if (diffElems != null)
            {              
                diffDS = new DataSet();
                //2.1 Generate columns
                diffDS.Tables.Add(RepDataSet.DataTable.Name);
                diffDS.Tables[0].Columns.Add(RepDataSet.DataTable.ColumnElementName);
                diffDS.Tables[0].Columns.Add(RepDataSet.DataTable.ColumnPropertyName);
                diffDS.Tables[0].Columns.Add(RepDataSet.DataTable.ColumnFirstValue);
                diffDS.Tables[0].Columns.Add(RepDataSet.DataTable.ColumnSecondValue);

                //2.2 Fill rows
                foreach (string elemName in diffElems.Keys)
                {
                    Dictionary<string, CompareItem> diffProps = diffElems[elemName].Items;
                    foreach (string propName in diffProps.Keys)
                    {
                        DataRow row = diffDS.Tables[RepDataSet.DataTable.Name].NewRow();
                        //row["Element Name"]
                        row[RepDataSet.DataTable.ColumnElementName] = elemName;
                        row[RepDataSet.DataTable.ColumnPropertyName] = propName;
                        row[RepDataSet.DataTable.ColumnFirstValue] = (diffProps[propName].Firstvalue != null) ? CompareHelper.ToString(diffProps[propName].Firstvalue) : "null";
                        row[RepDataSet.DataTable.ColumnSecondValue] = (diffProps[propName].Secondvalue != null) ? CompareHelper.ToString(diffProps[propName].Secondvalue) : "null";
                        diffDS.Tables[0].Rows.Add(row);
                    }
                }
            }

            return diffDS;
        }

        /// <summary>
        ///Special For events
        /// </summary>
        /// <param name="diffElems"></param>
        /// <returns></returns>
        private DataSet GenerateReportDataSet(Dictionary<KeyValuePair<string, bool>, DifferPropertiesDictionary> diffElems)
        {
            DataSet diffDS = null;
            if (diffElems != null)
            {               
                diffDS = new DataSet();
                //2.1 Generate columns
                diffDS.Tables.Add(RepDataSet.DataTable.Name);
                diffDS.Tables[0].Columns.Add(RepDataSet.DataTable.ColumnElementName);
                diffDS.Tables[0].Columns.Add(RepDataSet.DataTable.ColumnPropertyName);
                diffDS.Tables[0].Columns.Add(RepDataSet.DataTable.ColumnFirstValue);
                diffDS.Tables[0].Columns.Add(RepDataSet.DataTable.ColumnSecondValue);

                //2.2 Fill rows
                foreach (KeyValuePair<string, bool> elem in diffElems.Keys)
                {
                    string elemName = elem.Key;

                    Dictionary<string, CompareItem> diffProps = diffElems[elem].Items;
                    foreach (string propName in diffProps.Keys)
                    {
                        DataRow row = diffDS.Tables[RepDataSet.DataTable.Name].NewRow();
                        //row["Element Name"]
                        row[RepDataSet.DataTable.ColumnElementName] = elemName;
                        row[RepDataSet.DataTable.ColumnPropertyName] = propName;
                        row[RepDataSet.DataTable.ColumnFirstValue] = (diffProps[propName].Firstvalue != null) ? CompareHelper.ToString(diffProps[propName].Firstvalue) : "null";
                        row[RepDataSet.DataTable.ColumnSecondValue] = (diffProps[propName].Secondvalue != null) ? CompareHelper.ToString(diffProps[propName].Secondvalue) : "null";
                        diffDS.Tables[0].Rows.Add(row);
                    }
                }
            }

            return diffDS;
        }

        #endregion

        #endregion


        #region Form Events

        private void ComparatorForm_Load(object sender, EventArgs e)
        {         
            LoadSettings();
            ComparationFinishedEvent += () => OnCompareFinished();
            //entitiesListBox.Invalidated += new InvalidateEventHandler(RedrawEntitiesList);
        }

        #region Menu_ToolBars

        private void loadCustomizationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartLoadingCustomizations();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsDialog sd = new SettingsDialog();
            if (sd.ShowDialog() == DialogResult.OK) LoadSettings();
        }

        private void analyzeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeComparatorView(ComparatorView.analyze.ToString());
        }

        private void treeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeComparatorView(ComparatorView.tree.ToString());
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void firstTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Node.Name))
            {
                string nodeName = e.Node.Name;
                secondTreeView.SelectedNode = FindTreeNodeByName(secondTreeView, nodeName);
            }
        }

        #endregion

        private void entitiesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ListView)sender).SelectedItems.Count == 1)
            {
                _currentEntity = ((ListView)sender).SelectedItems[0].Text.Trim();
                currentEntityLabel.Text = _currentEntity;

                NavigateToEntityOnTree(firstTreeView, _currentEntity);
                NavigateToEntityOnTree(secondTreeView, _currentEntity);
            }

            //2. If 
            if (viewsElemsTabControl.SelectedTab.Tag.Equals(ComparatorView.analyze.ToString()))
            {
                AnalyzeCustomizations();
            }
        }


        private void firstAttributesDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            AutoNavigateCrid(attributeKeyProperty, (DataGridView)sender, secondAttributesDataGridView);
            ShowCurrentCellContent((DataGridView)sender);
        }

        private void firstFieldsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            AutoNavigateCrid(fieldKeyProperty, (DataGridView)sender, secondFieldsDataGridView);
            ShowCurrentCellContent((DataGridView)sender);
        }

        private void firstFormDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            AutoNavigateCrid(cellControlKeyProperty, (DataGridView)sender, secondFormDataGridView);
            ShowCurrentCellContent((DataGridView)sender);
        }

        private void analyzeTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            SwitchGrids((TabControl)sender);
        }

        private void showDifferRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                _showOnlyUpdated = true;
                SwitchGrids(analyzeTabControl);
            }
        }

        private void showAllRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                _showOnlyUpdated = false;
                SwitchGrids(analyzeTabControl);
            }
        }

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           if(Settings.Default.LoadCellToWordPad)
                LoadWordPad(sender);
        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            ShowCurrentCellContent((DataGridView)sender);
        }


        private void controlsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            LoadFormsToCompare();
        }

        private void eventsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            LoadFormsToCompare();
        }

        private void dataRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            LoadFormsToCompare();
        }    
      
        private void ComparatorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }
      
        
        private void diffButton_Click(object sender, EventArgs e)
        {
            AnalyzeCustomizations();
        }


        private void viewsElemsTabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Tag != null)
                ChangeComparatorView(e.TabPage.Tag.ToString());
        }

        private void ChangeComparatorView(string pageTag)
        {
            if (pageTag.Equals(ComparatorView.analyze.ToString()))
            {
                bool isAnalyzed = AnalyzeCustomizations();
                if (isAnalyzed)
                {
                    viewsElemsTabControl.SelectedIndex = 1;                                       

                    diffButton.Enabled = true;

                    reportButton.Enabled = true;
                    
                }
                else
                    viewsElemsTabControl.SelectedIndex = 0;
            }
            else
            {
                viewsElemsTabControl.SelectedIndex = 0;
                SetStatusMessage("");          

                diffButton.Enabled = false;

                reportButton.Enabled = false;
            }
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            OpenReport(analyzeTabControl.SelectedTab.Tag.ToString());
        }

        #region AutoScroll

        private void firstFormDataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            secondFormDataGridView.HorizontalScrollingOffset = e.NewValue;
        }

        private void secondFormDataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            if(e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                firstFormDataGridView.HorizontalScrollingOffset = e.NewValue;
        }

        private void firstAttributesDataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            secondAttributesDataGridView.HorizontalScrollingOffset = e.NewValue;
        }

        private void secondAttributesDataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            firstAttributesDataGridView.HorizontalScrollingOffset = e.NewValue;
        }

        private void firstFieldsDataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            secondFieldsDataGridView.HorizontalScrollingOffset = e.NewValue;
        }

        private void secondFieldsDataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            firstFieldsDataGridView.HorizontalScrollingOffset = e.NewValue;
        }

         #endregion

        private void firstTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                ShowPathToolTips(firstTreeView, firstCustFilePath);
        }

        private void secondTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
                ShowPathToolTips(secondTreeView, secondCustFilePath);
            else
                customizationFilePathToolTip.Active = false;
        }

        private void ShowPathToolTips(TreeView owner, string filepath)
        {
            customizationFilePathToolTip.Active = true;
            customizationFilePathToolTip.Show(filepath, owner);
        }


        private void firstAttributesDataGridView_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        {
            DataView dv = ((DataView)firstAttributesDataGridView.DataSource);

            ColorCells(dv, firstAttributesDataGridView, diffAttrs, firstOriginalAttrsNames);
        }

        private void firstAttributesDataGridView_Sorted(object sender, EventArgs e)
        {                     
            ReColorDataGridView((DataGridView)sender, attributeKeyProperty, diffAttrs, firstOriginalAttrsNames);
        }

        private void secondAttributesDataGridView_Sorted(object sender, EventArgs e)
        {
            ReColorDataGridView((DataGridView)sender, attributeKeyProperty, diffAttrs, secondOriginalAttrsNames);
        }

        private void firstFieldsDataGridView_Sorted(object sender, EventArgs e)
        {
            ReColorDataGridView((DataGridView)sender, fieldKeyProperty, diffFields, firstOriginalFieldsNames);
        }

        private void secondFieldsDataGridView_Sorted(object sender, EventArgs e)
        {
            ReColorDataGridView((DataGridView)sender, fieldKeyProperty, diffFields, secondOriginalFieldsNames);
        }

        private void firstFormDataGridView_Sorted(object sender, EventArgs e)
        {
            if(controlsRadioButton.Checked)
                ReColorDataGridView((DataGridView)sender, cellControlKeyProperty, diffFormsCells, firstFormOriginalControlsNames);
            else if(dataRadioButton.Checked)
                ReColorDataGridView((DataGridView)sender, dataFormKeyProperty, diffFormsData, firstFormOriginalDataIds);
        }

        private void secondFormDataGridView_Sorted(object sender, EventArgs e)
        {
            if (controlsRadioButton.Checked)
                ReColorDataGridView((DataGridView)sender, cellControlKeyProperty, diffFormsCells, secondFormOriginalControlsNames);
            else if(dataRadioButton.Checked)
                ReColorDataGridView((DataGridView)sender, dataFormKeyProperty, diffFormsData, secondFormOriginalDataIds);
        }

        #endregion


    }
}



/*
/// <summary>
/// //
/// </summary>
/// <param name="gridView"></param>
/// <param name="keycolumn"></param>
/// <param name="originalElemsNames"></param>

private void ColorOriginalCells(DataGridView gridView, string keycolumn, List<string> originalElemsNames)
{
    foreach (string originalElemKey in originalElemsNames)
    {
        int originalRowIndex = FindRowIndexByKey(gridView, keycolumn, originalElemKey);
        if (originalRowIndex > -1)
            gridView.Rows[originalRowIndex].DefaultCellStyle.BackColor = Settings.Default.AbsentColor;
    }
}

private void ColorDifferPropertiesCells(DataGridView gridView, string keycolumn,
          Dictionary<string, DifferPropertiesDictionary> diffElems)
{
    //Color cells, - element(attribute|field) properties, - which are differ by values             
    foreach (string diffElemKey in diffElems.Keys)
    {
        DifferPropertiesDictionary props = diffElems[diffElemKey];
        int diffRowIndex = FindRowIndexByKey(gridView, keycolumn, diffElemKey);                

        if ((diffRowIndex > -1) && (props != null))
        {
            IEnumerator ien = props.Items.Keys.GetEnumerator();
            while (ien.MoveNext())
            {
                string propName = ien.Current.ToString();
                //
                if (gridView.Columns.Contains(propName))
                {
                    //Collorize differ property cell
                    if (gridView[propName, diffRowIndex] != null)
                        gridView[propName, diffRowIndex].Style.BackColor = Settings.Default.DifferColor;
                }
            }

        }

    }
}


private int FindRowIndexByKey(DataGridView gridView, string key, string keyvalue)
{
    int index = -1;
    DataGridViewRowCollection rows = gridView.Rows;
    foreach (DataGridViewRow row in rows)
    {
        if (row.Cells[key].Equals(keyvalue))
        {
            index = row.Index;
            break;
        }
    }
    return index;
}
*/
