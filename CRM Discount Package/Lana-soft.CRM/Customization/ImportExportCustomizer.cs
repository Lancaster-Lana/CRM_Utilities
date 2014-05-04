using System;
using System.Globalization;
using System.Xml;
using LanaSoftCRM.MicrosoftCrmService;

namespace LanaSoftCRM
{
    /// <summary>
    /// Export, import, and publish Microsoft CRM
    /// customizations.  The scenario is that you are an ISV that would like to import
    /// two custom entities into an existing system.  
    public class ImportExportCustomizer
    {

        #region Constants
        private const string VALID = "1";
        private const string INVALID = "0";

        public const int ENGLISH_LANGUAGE_CODE = 1033;

        // attribute names
        private const string ATTRIBUTE_DESCRIPTION = "description";
        private const string ATTRIBUTE_LANGUAGE_CODE = "languagecode";
        private const string ATTRIBUTE_REQUIRED_LEVEL = "requiredlevel";

        // default attribute values
        public const string DEFAULT_DISPLAY_MASK = "ValidForAdvancedFind|ValidForForm|ValidForGrid";
        public const int DEFAULT_BIT_NO = 0;
        public const int DEFAULT_BIT_NEXT_VALUE = 1;
        public const int DEFAULT_BIT_YES = 1;
        public const int DEFAULT_FLOAT_ACCURACY = 2;
        public const float DEFAULT_FLOAT_MAX_VALUE = 1000000000;
        public const float DEFAULT_FLOAT_MIN_VALUE = 0;
        public const int DEFAULT_INTEGER_ACCURACY = 0;
        public const int DEFAULT_INTEGER_MAX_VALUE = 2147483647;
        public const int DEFAULT_INTEGER_MIN_VALUE = -2147483648;
        public const int DEFAULT_MONEY_ACCURACY = 2;
        public const decimal DEFAULT_MONEY_MAX_VALUE = 1000000000;
        public const decimal DEFAULT_MONEY_MIN_VALUE = 0;
        public const int DEFAULT_NTEXT_MAX_LENGTH = 2000;
        public const int DEFAULT_NVARCHAR_MAX_LENGTH = 100;

        public const string DEFAULT_BIT_NO_DESCRIPTION = "No";
        public const string DEFAULT_BIT_YES_DESCRIPTION = "Yes";
        public const string DEFAULT_REQUIRED_LEVEL = "na";
        #endregion

        // Field
        private XmlDocument _importExportXml;

        #region Property

        public XmlDocument Xml
        {
            get
            {
                return _importExportXml;
            }
        }

        #endregion

        #region Constructor
        public ImportExportCustomizer(XmlDocument importExportXml)
        {
            // instantiate variables/fields
            _importExportXml = importExportXml;
            _importExportXml.PreserveWhitespace = true;
        }
        #endregion


        public static string ExportCurrentCustomization()
        {
            var service = CRMHelper.GetCrmService();
            var request = new ExportAllXmlRequest();
            var response = (ExportAllXmlResponse)service.Execute(request);
            return response.ExportXml;
        }

        public static string ExportNeccessaryCustomization(string parameterXml)
        {
            var service = CRMHelper.GetCrmService();
            var request = new ExportXmlRequest();
            request.ParameterXml = parameterXml;
            var response = (ExportXmlResponse)service.Execute(request);
            return response.ExportXml;
        }

        public static string ApplyCustomizationString(string xmlCustomization)
        {
            try
            {
                // Set up the CRM Service.
                var service = CRMHelper.GetCrmService();

                #region Import New Customizations

                var importRequest = new ImportAllXmlRequest();
                importRequest.CustomizationXml = xmlCustomization;

                // Execute the import.
                var importResponse = (ImportAllXmlResponse)service.Execute(importRequest);

                #endregion

                #region Publish New Customizations

                // Create the request.
                var publishRequest = new PublishAllXmlRequest();
                var publishResponse = (PublishAllXmlResponse)service.Execute(publishRequest);

                #endregion
            }
            catch (System.Web.Services.Protocols.SoapException e)
            {
                return e.Message;
            }

            return " Import and Publish success.";
        }


        public static string ApplyCustomizationFile(string file)
        {
            //Load new customization
            var doc = new XmlDocument();
            doc.Load(file);
            string xmlCustomization = doc.OuterXml;
            return ApplyCustomizationString(xmlCustomization);
        }

        #region Methods: Adding Attributes
        // Note: The method names correspond to the enum "AttributeType" values (originally taken from the RC1 import/export XML values)

        /// <summary>
        /// Used to add an "attribute" element node to the XML document
        /// </summary>
        /// <param name="attributeType">Specifies which attribute is being added</param>
        /// <param name="entityName">Name of the entity to add the attribute element node to</param>
        /// <param name="attributeName">Name of the attribute element node to add</param>
        /// <param name="description">Description tag on the attribute element node</param>
        /// <param name="displayMask"></param>
        protected void AddAttributeElementToEntityInfo(AttributeType attributeType, string entityName, string attributeName, string description, string displayMask)
        {
            // XPaths			
            string fieldXmlPath = string.Format("/ImportExportXml/Entities/Entity/FieldXml/entity[@name=\"{0}\"]", entityName);
            string entityInfoXPath = string.Format("./EntityInfo/entity/attributes");
            string entityXPath = string.Format("./EntityInfo/entity/attributes/attribute[@PhysicalName=\"{0}\"]", attributeName);

            // XML element nodes retrieved by the XPaths
            XmlNode fieldXmlNode = _importExportXml.SelectSingleNode(fieldXmlPath) as XmlNode;
            XmlNode entityNode = fieldXmlNode.ParentNode.ParentNode; // navigates up because the schema name is not what is used to import/export

            XmlElement entityInfoElement = entityNode.SelectSingleNode(entityInfoXPath) as XmlElement;
            XmlElement entityElement = entityNode.SelectSingleNode(entityXPath) as XmlElement;

            // <entities> must exist and the attribute that is being added must not exist
            if (entityInfoElement != null && entityElement == null)
            {
                // <attribute> element
                XmlElement newEntityElement = _importExportXml.CreateElement("attribute");
                newEntityElement.SetAttribute("PhysicalName", attributeName);

                // adding child elements
                AddChildElement(newEntityElement, "Type", Utility.GetDescriptionAttribute(attributeType));
                if (attributeType == AttributeType.NVarchar)
                {
                    AddChildElement(newEntityElement, "Length", DEFAULT_NVARCHAR_MAX_LENGTH.ToString(CultureInfo.InvariantCulture));
                }
                AddChildElement(newEntityElement, "ValidForCreateApi", VALID);	//TODO: additional options can be added
                AddChildElement(newEntityElement, "ValidForUpdateApi", VALID);	//TODO: additional options can be added
                AddChildElement(newEntityElement, "ValidForReadApi", VALID);	//TODO: additional options can be added
                AddChildElement(newEntityElement, "IsCustomField", "1");	// every attribute that is added is custom by definition
                AddChildElement(newEntityElement, "DisplayMask", displayMask);	//TODO: additional options can be added
                AddChildElement(newEntityElement, "Description", description);

                // add <attribute> to the XML document
                entityInfoElement.AppendChild(newEntityElement);

                // bits and picklists have an additional virtual XML element associated with them
                if (attributeType == AttributeType.Bit || attributeType == AttributeType.Picklist)
                {
                    // <attribute> element
                    XmlElement bitHelperAttribute = _importExportXml.CreateElement("attribute");
                    bitHelperAttribute.SetAttribute("PhysicalName", attributeName.ToLower(CultureInfo.InvariantCulture) + "Name");

                    // adding child elements
                    AddChildElement(bitHelperAttribute, "Type", "virtual");			// every bit attribute has an additional virutal attribute
                    AddChildElement(bitHelperAttribute, "ValidForReadApi", VALID);	//TODO: additional options can be added
                    AddChildElement(bitHelperAttribute, "IsCustomField", "1");		// every attribute that is added is custom by definition
                    AddChildElement(bitHelperAttribute, "IsLogical", "1");
                    AddChildElement(bitHelperAttribute, "AttributeOf", attributeName);
                    AddChildElement(bitHelperAttribute, "XmlAbbreviation", "name");

                    // add <attribute> to the XML document
                    entityInfoElement.AppendChild(bitHelperAttribute);
                }
            }
            else if (entityInfoElement != null && entityElement != null)
            {
                // do nothing since the attribute element node already exists
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// Used to add a bit attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        /// <remarks>The default value will be "Yes" ("1") to mimic MSCRM v3.0</remarks>
        public void AddBitElement(string entityName, string attributeName, string attributeDescription, string fieldDescription)
        {
            AddBitElement(entityName, attributeName, attributeDescription, fieldDescription,
                DEFAULT_BIT_YES, DEFAULT_BIT_NO_DESCRIPTION, DEFAULT_BIT_YES_DESCRIPTION, DEFAULT_DISPLAY_MASK, ENGLISH_LANGUAGE_CODE);
        }

        /// <summary>
        /// Used to add a bit attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        /// <param name="defaultValue"></param>
        /// <param name="noDescription"></param>
        /// <param name="yesDescription"></param>
        /// <param name="displayMask"></param>
        /// <param name="languageCode">Integer value corresponding to a language</param>
        /// <remarks>The default value will be "Yes" ("1") to mimic MSCRM v3.0</remarks>
        public void AddBitElement(string entityName, string attributeName, string attributeDescription, string fieldDescription,
            int defaultValue, string noDescription, string yesDescription, string displayMask, int languageCode)
        {
            // adding the <attribute> element node
            AddAttributeElementToEntityInfo(AttributeType.Bit, entityName, attributeName, attributeDescription, displayMask);

            // adding the <field> element node
            string fieldXmlXPath = GetFieldXmlXPath(entityName);	// XPath

            XmlElement fieldXmlElement = _importExportXml.SelectSingleNode(fieldXmlXPath) as XmlElement;
            XmlElement fieldElement = CreateFieldElement(entityName, attributeName, fieldDescription, languageCode);

            if (fieldElement != null)
            {
                // set attribute
                fieldElement.SetAttribute("defaultvalue", defaultValue.ToString(CultureInfo.InvariantCulture));

                // <options>
                XmlElement optionsElement = AddChildElement(fieldElement, "options");
                optionsElement.SetAttribute("nextvalue", DEFAULT_BIT_NEXT_VALUE.ToString(CultureInfo.InvariantCulture));

                // <option value="0"> -- default "No" value
                XmlElement noOptionElement = AddChildElement(optionsElement, "option");
                noOptionElement.SetAttribute("value", DEFAULT_BIT_NO.ToString(CultureInfo.InvariantCulture));

                XmlElement noLabelsElement = AddChildElement(noOptionElement, "labels");	// <labels>
                XmlElement noLabelElement = AddChildElement(noLabelsElement, "label");		// <label>
                noLabelElement.SetAttribute(ATTRIBUTE_DESCRIPTION, noDescription);
                noLabelElement.SetAttribute(ATTRIBUTE_LANGUAGE_CODE, languageCode.ToString(CultureInfo.InvariantCulture));

                // <option value="1"> -- default "Yes" value
                XmlElement yesOptionElement = AddChildElement(optionsElement, "option");
                yesOptionElement.SetAttribute("value", DEFAULT_BIT_YES.ToString(CultureInfo.InvariantCulture));

                XmlElement yesLabelsElement = AddChildElement(yesOptionElement, "labels");	// <labels>
                XmlElement yesLabelElement = AddChildElement(yesLabelsElement, "label");	// <label>
                yesLabelElement.SetAttribute(ATTRIBUTE_DESCRIPTION, yesDescription);
                yesLabelElement.SetAttribute(ATTRIBUTE_LANGUAGE_CODE, languageCode.ToString(CultureInfo.InvariantCulture));


                // add to XML document
                fieldXmlElement.AppendChild(fieldElement);
            }
        }

        /// <summary>
        /// Used to add a date/time attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        public void AddDateTimeElement(string entityName, string attributeName, string attributeDescription, string fieldDescription)
        {
            AddDateTimeElement(entityName, attributeName, attributeDescription, fieldDescription,
                DEFAULT_DISPLAY_MASK, ENGLISH_LANGUAGE_CODE);
        }

        /// <summary>
        /// Used to add a date/time attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        /// <param name="displayMask"></param>
        /// <param name="languageCode">Integer value corresponding to a language</param>
        public void AddDateTimeElement(string entityName, string attributeName, string attributeDescription, string fieldDescription,
            string displayMask, int languageCode)
        {
            // adding the <attribute> element node
            AddAttributeElementToEntityInfo(AttributeType.DateTime, entityName, attributeName, attributeDescription, displayMask);

            // adding the <field> element node
            string fieldXmlPath = GetFieldXmlXPath(entityName);	// XPath

            XmlElement fieldXmlElement = _importExportXml.SelectSingleNode(fieldXmlPath) as XmlElement;
            XmlElement fieldElement = CreateFieldElement(entityName, attributeName, fieldDescription, languageCode);

            if (fieldElement != null)
            {
                // set attribute
                fieldElement.SetAttribute("format", "datetime");	//TODO: additional options can be added

                // add to XML document
                fieldXmlElement.AppendChild(fieldElement);
            }
        }

        /// <summary>
        /// Used to add a float attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        public void AddFloatElement(string entityName, string attributeName, string attributeDescription, string fieldDescription)
        {
            AddFloatElement(entityName, attributeName, attributeDescription, fieldDescription,
                DEFAULT_FLOAT_MIN_VALUE, DEFAULT_FLOAT_MAX_VALUE);
        }

        /// <summary>
        /// Used to add a float attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        /// <param name="minValue">Minimum float value the user can choose</param>
        /// <param name="maxValue">Maximum float value the user can choose</param>
        public void AddFloatElement(string entityName, string attributeName, string attributeDescription, string fieldDescription,
            float minValue, float maxValue)
        {
            AddFloatElement(entityName, attributeName, attributeDescription, fieldDescription,
                minValue, maxValue, DEFAULT_FLOAT_ACCURACY, DEFAULT_DISPLAY_MASK, ENGLISH_LANGUAGE_CODE);
        }

        /// <summary>
        /// Used to add a float attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        /// <param name="minValue">Minimum float value the user can choose</param>
        /// <param name="maxValue">Maximum float value the user can choose</param>
        /// <param name="accuracy">Precision of the decimal point</param>
        /// <param name="displayMask"></param>
        /// <param name="languageCode">Integer value corresponding to a language</param>
        public void AddFloatElement(string entityName, string attributeName, string attributeDescription, string fieldDescription,
            float minValue, float maxValue, int accuracy, string displayMask, int languageCode)
        {
            // adding the <attribute> element node
            AddAttributeElementToEntityInfo(AttributeType.Float, entityName, attributeName, attributeDescription, displayMask);

            // adding the <field> element node
            string fieldXmlXPath = GetFieldXmlXPath(entityName);	// XPath

            XmlElement fieldXmlElement = _importExportXml.SelectSingleNode(fieldXmlXPath) as XmlElement;
            XmlElement fieldElement = CreateFieldElement(entityName, attributeName, fieldDescription, languageCode);

            if (fieldElement != null)
            {
                // used to format floats without commas or shorthand (i.e. Ex09)
                NumberFormatInfo formatter = new NumberFormatInfo();
                formatter.NumberDecimalDigits = 0;
                formatter.NumberGroupSeparator = string.Empty;

                // set attributes
                fieldElement.SetAttribute("minvalue", minValue.ToString("n", formatter));
                fieldElement.SetAttribute("maxvalue", maxValue.ToString("n", formatter));
                fieldElement.SetAttribute("accuracy", accuracy.ToString(CultureInfo.InvariantCulture));

                // add to XML document
                fieldXmlElement.AppendChild(fieldElement);
            }
        }

        /// <summary>
        /// Used to add an integer attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        public void AddIntegerElement(string entityName, string attributeName, string attributeDescription, string fieldDescription)
        {
            AddIntegerElement(entityName, attributeName, attributeDescription, fieldDescription,
                DEFAULT_INTEGER_MIN_VALUE, DEFAULT_INTEGER_MAX_VALUE);
        }

        /// <summary>
        /// Used to add an integer attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        /// <param name="minValue">Minimum integer value the user can choose</param>
        /// <param name="maxValue">Maximum integer value the user can choose</param>
        public void AddIntegerElement(string entityName, string attributeName, string attributeDescription, string fieldDescription,
            int minValue, int maxValue)
        {
            AddIntegerElement(entityName, attributeName, attributeDescription, fieldDescription,
                minValue, maxValue, DEFAULT_INTEGER_ACCURACY, DEFAULT_DISPLAY_MASK, ENGLISH_LANGUAGE_CODE);
        }

        /// <summary>
        /// Used to add an integer attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        /// <param name="minValue">Minimum integer value the user can choose</param>
        /// <param name="maxValue">Maximum integer value the user can choose</param>
        /// <param name="accuracy">Precision of the decimal point</param>
        /// <param name="displayMask"></param>
        /// <param name="languageCode">Integer value corresponding to a language</param>
        public void AddIntegerElement(string entityName, string attributeName, string attributeDescription, string fieldDescription,
            int minValue, int maxValue, int accuracy, string displayMask, int languageCode)
        {
            // adding the <attribute> element node
            AddAttributeElementToEntityInfo(AttributeType.Integer, entityName, attributeName, attributeDescription, displayMask);

            // adding the <field> element node
            string fieldXmlXPath = GetFieldXmlXPath(entityName);	// XPath

            XmlElement fieldXmlElement = _importExportXml.SelectSingleNode(fieldXmlXPath) as XmlElement;
            XmlElement fieldElement = CreateFieldElement(entityName, attributeName, fieldDescription, languageCode);

            if (fieldElement != null)
            {
                // set attributes
                fieldElement.SetAttribute("minvalue", minValue.ToString(CultureInfo.InvariantCulture));
                fieldElement.SetAttribute("maxvalue", maxValue.ToString(CultureInfo.InvariantCulture));
                fieldElement.SetAttribute("accuracy", accuracy.ToString(CultureInfo.InvariantCulture));

                // add to XML document
                fieldXmlElement.AppendChild(fieldElement);
            }
        }

        /// <summary>
        /// Used to add a money attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        public void AddMoneyElement(string entityName, string attributeName, string attributeDescription, string fieldDescription)
        {
            AddMoneyElement(entityName, attributeName, attributeDescription, fieldDescription,
                DEFAULT_MONEY_MIN_VALUE, DEFAULT_MONEY_MAX_VALUE);
        }

        /// <summary>
        /// Used to add a money attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        /// <param name="minValue">Minimum money (decimal) value the user can choose</param>
        /// <param name="maxValue">Maximum money (decimal) value the user can choose</param>
        public void AddMoneyElement(string entityName, string attributeName, string attributeDescription, string fieldDescription,
            decimal minValue, decimal maxValue)
        {
            AddMoneyElement(entityName, attributeName, attributeDescription, fieldDescription,
                minValue, maxValue, DEFAULT_MONEY_ACCURACY, DEFAULT_DISPLAY_MASK, ENGLISH_LANGUAGE_CODE);
        }

        /// <summary>
        /// Used to add a money attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        /// <param name="minValue">Minimum money (decimal) value the user can choose</param>
        /// <param name="maxValue">Maximum money (decimal) value the user can choose</param>
        /// <param name="accuracy">Precision of the decimal point</param>
        /// <param name="displayMask"></param>
        /// <param name="languageCode">Integer value corresponding to a language</param>
        public void AddMoneyElement(string entityName, string attributeName, string attributeDescription, string fieldDescription,
            decimal minValue, decimal maxValue, int accuracy, string displayMask, int languageCode)
        {
            // adding the <attribute> element node
            AddAttributeElementToEntityInfo(AttributeType.Money, entityName, attributeName, attributeDescription, displayMask);

            // adding the <field> element node
            string fieldXmlXPath = GetFieldXmlXPath(entityName);	// XPath

            XmlElement fieldXmlElement = _importExportXml.SelectSingleNode(fieldXmlXPath) as XmlElement;
            XmlElement fieldElement = CreateFieldElement(entityName, attributeName, fieldDescription, languageCode);

            if (fieldElement != null)
            {
                // used to format decimals without commas or shorthand (i.e. Ex09)
                NumberFormatInfo formatter = new NumberFormatInfo();
                formatter.NumberDecimalDigits = 0;
                formatter.NumberGroupSeparator = string.Empty;

                // set attributes
                fieldElement.SetAttribute("minvalue", minValue.ToString("n", formatter));
                fieldElement.SetAttribute("maxvalue", maxValue.ToString("n", formatter));
                fieldElement.SetAttribute("accuracy", accuracy.ToString(CultureInfo.InvariantCulture));

                // add to XML document
                fieldXmlElement.AppendChild(fieldElement);
            }
        }

        /// <summary>
        /// Used to add a ntext attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        public void AddNTextElement(string entityName, string attributeName, string attributeDescription, string fieldDescription)
        {
            AddNTextElement(entityName, attributeName, attributeDescription, fieldDescription,
                DEFAULT_NTEXT_MAX_LENGTH, DEFAULT_DISPLAY_MASK, ENGLISH_LANGUAGE_CODE);
        }

        /// <summary>
        /// Used to add a ntext attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        /// <param name="maxLength">Maximum number of characters the user can enter</param>
        /// <param name="displayMask"></param>
        /// <param name="languageCode">Integer value corresponding to a language</param>
        public void AddNTextElement(string entityName, string attributeName, string attributeDescription, string fieldDescription,
            int maxLength, string displayMask, int languageCode)
        {
            // adding the <attribute> element node
            AddAttributeElementToEntityInfo(AttributeType.NText, entityName, attributeName, attributeDescription, displayMask);

            // adding the <field> element node
            string fieldXmlPath = GetFieldXmlXPath(entityName);	// XPath

            XmlElement fieldXmlElement = _importExportXml.SelectSingleNode(fieldXmlPath) as XmlElement;
            XmlElement fieldElement = CreateFieldElement(entityName, attributeName, fieldDescription, languageCode);

            if (fieldElement != null)
            {
                // set attribute
                fieldElement.SetAttribute("maxlength", maxLength.ToString(CultureInfo.InvariantCulture));

                // add to XML document
                fieldXmlElement.AppendChild(fieldElement);
            }
        }

        /// <summary>
        /// Used to add a nvarchar attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        public void AddNVarcharElement(string entityName, string attributeName, string attributeDescription, string fieldDescription)
        {
            AddNVarcharElement(entityName, attributeName, attributeDescription, fieldDescription,
                DEFAULT_NVARCHAR_MAX_LENGTH, DEFAULT_DISPLAY_MASK, ENGLISH_LANGUAGE_CODE);
        }

        /// <summary>
        /// Used to add a nvarchar attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        /// <param name="maxLength">Maximum number of characters the user can enter</param>
        /// <param name="displayMask"></param>
        /// <param name="languageCode">Integer value corresponding to a language</param>
        public void AddNVarcharElement(string entityName, string attributeName, string attributeDescription, string fieldDescription,
            int maxLength, string displayMask, int languageCode)
        {
            // adding the <attribute> element node
            AddAttributeElementToEntityInfo(AttributeType.NVarchar, entityName, attributeName, attributeDescription, displayMask);

            // adding the <field> element node
            string fieldXmlXPath = GetFieldXmlXPath(entityName);	// XPath

            XmlElement fieldXmlElement = _importExportXml.SelectSingleNode(fieldXmlXPath) as XmlElement;
            XmlElement fieldElement = CreateFieldElement(entityName, attributeName, fieldDescription, languageCode);

            if (fieldElement != null)
            {
                // set attributes
                fieldElement.SetAttribute("maxlength", maxLength.ToString(CultureInfo.InvariantCulture));
                fieldElement.SetAttribute("format", "text");	//TODO: additional options can be added

                // add to XML document
                fieldXmlElement.AppendChild(fieldElement);
            }
        }

        /// <summary>
        /// Used to add a picklist attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        /// <param name="picklistOptions">Corresponds to picklist values</param>
        public void AddPicklistElement(string entityName, string attributeName, string attributeDescription, string fieldDescription,
            PicklistOption[] picklistOptions)
        {
            AddPicklistElement(entityName, attributeName, attributeDescription, fieldDescription, picklistOptions, string.Empty);
        }

        /// <summary>
        /// Used to add a picklist attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        /// <param name="picklistOptions">Corresponds to picklist values</param>
        /// <param name="defaultValue">Default value of the picklist attribute</param>
        public void AddPicklistElement(string entityName, string attributeName, string attributeDescription, string fieldDescription,
            PicklistOption[] picklistOptions, string defaultValue)
        {
            AddPicklistElement(entityName, attributeName, attributeDescription, fieldDescription, picklistOptions,
                defaultValue, GetNextValueForPicklist(picklistOptions), DEFAULT_DISPLAY_MASK, ENGLISH_LANGUAGE_CODE);
        }

        /// <summary>
        /// Used to add a picklist attribute to an entity
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute to add</param>
        /// <param name="attributeDescription">Description for the "attribute" element node</param>
        /// <param name="fieldDescription">Description for the "field" element node</param>
        /// <param name="picklistOptions">Corresponds to picklist values</param>
        /// <param name="defaultValue">Default value of the picklist attribute</param>
        /// <param name="nextValue">Next value of the picklist attribute</param>
        /// <param name="displayMask"></param>
        /// <param name="languageCode">Integer value corresponding to a language</param>
        public void AddPicklistElement(string entityName, string attributeName, string attributeDescription, string fieldDescription,
            PicklistOption[] picklistOptions, string defaultValue, int nextValue, string displayMask, int languageCode)
        {
            // adding the <attribute> element node
            AddAttributeElementToEntityInfo(AttributeType.Picklist, entityName, attributeName, attributeDescription, displayMask);

            // adding the <field> element node
            string fieldXmlXPath = GetFieldXmlXPath(entityName);	// XPath

            XmlElement fieldXmlElement = _importExportXml.SelectSingleNode(fieldXmlXPath) as XmlElement;
            XmlElement fieldElement = CreateFieldElement(entityName, attributeName, fieldDescription, languageCode);

            if (fieldElement != null)
            {
                // set attributes
                fieldElement.SetAttribute("defaultvalue", defaultValue);

                // adding the <options> element node
                XmlElement optionsElement = AddChildElement(fieldElement, "options");
                optionsElement.SetAttribute("nextvalue", nextValue.ToString(CultureInfo.InvariantCulture));

                foreach (PicklistOption option in picklistOptions)
                {
                    // adding the <option> element node
                    XmlElement optionElement = AddChildElement(optionsElement, "option");
                    optionElement.SetAttribute("value", option.Value.ToString(CultureInfo.InvariantCulture));

                    // adding the <labels> element node
                    XmlElement labelsElement = AddChildElement(optionElement, "labels");

                    // adding the <label> element node
                    XmlElement labelElement = AddChildElement(labelsElement, "label");
                    labelElement.SetAttribute("description", option.Description);
                    labelElement.SetAttribute("languagecode", languageCode.ToString(CultureInfo.InvariantCulture));
                }

                // add to XML document
                fieldXmlElement.AppendChild(fieldElement);
            }
        }

        #endregion

        #region Methods: Helper Methods
        /// <summary>
        /// Used to create the base element node that will be added to the "fields" element node
        /// </summary>
        /// <param name="entityName">Name of the entity</param>
        /// <param name="attributeName">Name of the attribute being added to the entity</param>
        /// <param name="description"></param>
        /// <param name="languageCode">Integer value corresponding to a language</param>
        /// <returns>Returns a base XML element node used for adding a "field" element node to the XML document</returns>
        private XmlElement CreateFieldElement(string entityName, string attributeName, string description, int languageCode)
        {
            // cached lower-cased attribute name
            string lowerCasedAttributeName = attributeName.ToLower(CultureInfo.InvariantCulture);

            // XPaths
            string fieldXmlXPath = GetFieldXmlXPath(entityName);
            string fieldXPath = string.Format("/ImportExportXml/Entities/Entity/FieldXml/entity[@name=\"{0}\"]/fields/field[@name=\"{1}\"]",
                entityName, lowerCasedAttributeName);

            // XML element nodes retrieved by the XPaths
            var fieldXmlElement = _importExportXml.SelectSingleNode(fieldXmlXPath) as XmlElement;
            var fieldElement = _importExportXml.SelectSingleNode(fieldXPath) as XmlElement;

            // <fields> must exist and the field that is being added must not exist
            if (fieldXmlElement != null && fieldElement == null)
            {
                // <field> element
                XmlElement newFieldElement = _importExportXml.CreateElement("field");
                newFieldElement.SetAttribute("name", lowerCasedAttributeName);
                newFieldElement.SetAttribute(ATTRIBUTE_REQUIRED_LEVEL, DEFAULT_REQUIRED_LEVEL); //TODO: additional options can be added (by changing into an enum)

                // adding child elements
                XmlElement displayNamesElement = AddChildElement(newFieldElement, "displaynames");
                XmlElement displayNameElement = AddChildElement(displayNamesElement, "displayname");
                displayNameElement.SetAttribute(ATTRIBUTE_DESCRIPTION, description);
                displayNameElement.SetAttribute(ATTRIBUTE_LANGUAGE_CODE, languageCode.ToString(CultureInfo.InvariantCulture));

                return newFieldElement;
            }
            else if (fieldXmlElement != null && fieldElement != null)
            {
                // return null since field element node already exists
                return null;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// Gets the XPath needed to navigate to the "fields" element node in the import/export XML document
        /// </summary>
        /// <param name="entityName">Name of the entity (must be lower-cased)</param>
        /// <returns>Returns the XPath as a string to reach the "fields" element node</returns>
        private string GetFieldXmlXPath(string entityName)
        {
            return string.Format("/ImportExportXml/Entities/Entity/FieldXml/entity[@name=\"{0}\"]/fields", entityName);
        }

        /// <summary>
        /// Returns the next logical value from the picklist array (largest value + 1)
        /// </summary>
        /// <param name="pickListOptions">An array of values representing picklist option element nodes in the field XML</param>
        /// <returns>Returns the next value for the picklist</returns>
        private int GetNextValueForPicklist(PicklistOption[] picklistOptions)
        {
            // null check
            if (picklistOptions.Length > 1)
            {
                // set default largest value
                int largestValue = picklistOptions[0].Value;

                // iterate through for the largest value
                for (int index = 1; index < picklistOptions.Length; index++)
                {
                    if (largestValue < picklistOptions[index].Value)
                    {
                        largestValue = picklistOptions[index].Value;
                    }
                }

                // return the largest value plus one
                return ++largestValue;
            }
            else if (picklistOptions.Length == 1)
            {
                return ++picklistOptions[0].Value;	// should always be "1"
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Adds a child element to a parent element
        /// </summary>
        /// <param name="parentElement">Parent element node</param>
        /// <param name="childElementName">Child element node name</param>
        /// <returns>Returns the child element node parented to the parent element node</returns>
        private XmlElement AddChildElement(XmlElement parentElement, string childElementName)
        {
            // create a child element
            XmlElement childElement = _importExportXml.CreateElement(childElementName);

            // parent the child element
            parentElement.AppendChild(childElement);

            return childElement;
        }

        /// <summary>
        /// Adds a child element to a parent element with the specified value
        /// </summary>
        /// <param name="parentElement">Parent element node</param>
        /// <param name="childElementName">Child element node name</param>
        /// <param name="childElementValue">Childe element node value</param>
        /// <returns>Returns the child element node parented to the parent element node</returns>
        private XmlElement AddChildElement(XmlElement parentElement, string childElementName, string childElementValue)
        {
            // create a parented child element
            XmlElement childElement = AddChildElement(parentElement, childElementName);

            // set the value
            childElement.InnerText = childElementValue;

            return childElement;
        }
        #endregion
    }

}
