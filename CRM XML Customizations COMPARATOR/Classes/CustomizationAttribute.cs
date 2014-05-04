using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

namespace CustomizationsComparator
{    
    [System.SerializableAttribute()]
    public class AttributeType
    {
        //Attribute not exist for field
        public const string None = "none";

        //Without fields analogs:
        public const string  Internal = "internal";
        public const string  Virtual = "virtual";
         
        //Simple:   
        public const string  Owner = "owner";
        public const string  Customer = "customer";//???
        public const string  Lookup = "lookup";
        public const string  PrimaryKey = "primarykey";
        public const string  UniqueIdentifier = "uniqueidentifier";

        //For date field:
        public const string  DateTime = "datetime";

        //Decimal types:
        public const string  Decimal = "decimal";
        public const string  Float = "float";
        public const string  Integer = "integer";
        public const string  Money = "money";

       //Text types:
        public const string  String = "string";
        public const string  Memo = "memo";
        public const string  NVarChar = "nvarchar";
        public const string  NText = "ntext";

        //Optional types:
        public const string  Boolean = "boolean";
        public const string  Bit = "bit";
        public const string  Picklist = "picklist";
        public const string  PartyList = "partylist";
        public const string  State = "state";
        public const string  Status = "status";
      
        //Additional:
        public const string  CalendarRules = "calendarrules";
    }

    [System.SerializableAttribute(), XmlRoot]
    public enum DisplayMasks
    {
        /// <remarks/>
        None = 1,

        /// <remarks/>
        PrimaryName = 2,

        /// <remarks/>
        ObjectTypeCode = 4,

        /// <remarks/>
        ValidForAdvancedFind = 8,

        /// <remarks/>
        ValidForForm = 16,

        /// <remarks/>
        ValidForGrid = 32,

        /// <remarks/>
        RequiredForForm = 64,

        /// <remarks/>
        RequiredForGrid = 128,
    }

    [System.SerializableAttribute(), XmlRoot]
    public enum AttributeRequiredLevel{  None,SystemRequired,Required, Recommended,ReadOnly}

   [Serializable, XmlRoot("attribute")]
    public class CustomizationAttribute //: Attribute, IElement
    {
        private string _physicalName = "";
        private string _attributeOf = "";
        private string _description = "";
        private string _defaultValue = "";

        private AttributeRequiredLevel _requiredLevel = AttributeRequiredLevel.None;

        private string _type = "boolean";        //private AttributeType _type = AttributeType.boolean;
        private string _displayMask = "";

        private int _isCustomField = 0;
        private int _islogical = 0;
        private int _isSortAttribute = 0;

        private int _validForCreate = 0;
        private int _validForRead = 0;
        private int _validForUpdate = 0;


        [XmlAttribute()]
        public string PhysicalName
        {
            get
            {
                return this._physicalName;
            }
            set
            {
                this._physicalName = value;
            }
        }

        [XmlElement()]
        public string Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

        [XmlElement()]
        public string DisplayMask
        {
            get
            {
                return this._displayMask;
            }
            set
            {
                this._displayMask = value;
            }
        }

        [XmlElement()]
        public string AttributeOf
        {
            get
            {
                return this._attributeOf;
            }
            set
            {
                this._attributeOf = value;
            }
        }


        [XmlElement()]
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        [XmlElement()]
        public int IsCustomField
        {
            get
            {
                return this._isCustomField;
            }
            set
            {
                this._isCustomField = value;
            }
        }

        [XmlElement()]
        public int IsLogical
        {
            get
            {
                return this._islogical;
            }
            set
            {
                this._islogical = value;
            }
        }

        [XmlElement()]
        public int IsSortAttribute
        {
            get
            {
                return this._isSortAttribute;
            }
            set
            {
                this._isSortAttribute = value;
            }
        }

        [XmlElement()]
        public AttributeRequiredLevel RequiredLevel
        {
            get
            {
                return this._requiredLevel;
            }
            set
            {
                this._requiredLevel = value;
            }
        }

        [XmlElement()]
        public string DefaultValue
        {
            get
            {
                return this._defaultValue;
            }
            set
            {
                this._defaultValue = value;
            }
        }

        [XmlElement()]
        public int ValidForCreateApi
        {
            get
            {
                return this._validForCreate;
            }
            set
            {
                this._validForCreate = value;
            }
        }

        [XmlElement()]
        public int ValidForReadApi
        {
            get
            {
                return this._validForRead;
            }
            set
            {
                this._validForRead = value;
            }
        }

        [XmlElement()]
        public int ValidForUpdateApi
        {
            get
            {
                return this._validForUpdate;
            }
            set
            {
                this._validForUpdate = value;
            }
        }


        public CustomizationAttribute()
        {
        }


        public CustomizationAttribute(string physicalName, string typeName)
        {
            PhysicalName = physicalName;
            Type = typeName;
        }

        public CustomizationAttribute(string physicalName, string description,string attributeOf,
                                         string typeName, string defaultValue,
                                         int islogical, int isSortAttribute, int isCustomField,
                                         int validForCreate, int validForRead, int validForUpdate,
                                         string displayMask, AttributeRequiredLevel requiredLevel)
        {
            PhysicalName = physicalName;
            Description = description;
            AttributeOf = attributeOf;

            Type = typeName;
            DefaultValue = defaultValue;

            IsLogical = islogical;
            IsCustomField = isCustomField;
            IsSortAttribute = isSortAttribute;

            ValidForCreateApi = validForCreate;
            ValidForReadApi = validForRead;
            ValidForUpdateApi = validForUpdate;

            DisplayMask = displayMask;
            RequiredLevel = requiredLevel;

        }

        public static CustomizationAttribute Load(string XmlString)
        {
            CustomizationAttribute attr = Deserialize(XmlString);
            return attr;
        }

        public static CustomizationAttribute Deserialize(string XmlString)
        {
            try
            {
                XmlString = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" + XmlString;
                TextReader sr = new StringReader(XmlString);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomizationAttribute));
                CustomizationAttribute attr = (CustomizationAttribute)xmlSerializer.Deserialize(sr);
                return attr;
            }
            catch(Exception ex)
            { 
            }
            return null;
        }


    }

}

/*
        public static CustomizationAttribute Load(DataRow data)
        {
            CustomizationAttribute attr = new CustomizationAttribute();
            //foreach(DataColumn col in data.Table.Columns)
            attr.PhysicalName = data["PhysicalName"] as string;
            //attr.DisplayName = (data["DisplayName"] is string)? data["DisplayName"].ToString(): String.Empty;
            
            attr.Type = (data["Type"] is string)? data["Type"].ToString():  String.Empty;

            attr.DisplayMask = !(data["DisplayMask"] is System.DBNull) ? data["DisplayMask"].ToString() : String.Empty;
            attr.Description = !(data["Description"] is System.DBNull) ? data["Description"].ToString() : String.Empty;
            attr.DefaultValue = !(data["DefaultValue"] is System.DBNull) ? data["DefaultValue"].ToString() : null;

            attr.AttributeOf = !(data["AttributeOf"] is System.DBNull) ? data["AttributeOf"].ToString() : String.Empty;

            attr.IsCustomField = (data["IsCustomField"] is System.DBNull) ? 0 : Convert.ToInt32(data["IsCustomField"]);
            attr.IsSortAttribute = (data["IsSortAttribute"] is System.DBNull) ? 0 : Convert.ToInt32(data["IsSortAttribute"]);
            attr.IsLogical = (data["IsCustomField"] is System.DBNull) ? 0 : Convert.ToInt32(data["IsCustomField"]);

            attr.ValidForCreateApi = (data["ValidForCreateApi"] is System.DBNull)? 0: Convert.ToInt32(data["ValidForCreateApi"]);
            attr.ValidForReadApi = (data["ValidForReadApi"] is System.DBNull)? 0: Convert.ToInt32(data["ValidForReadApi"]);
            attr.ValidForUpdateApi = (data["ValidForReadApi"] is System.DBNull)? 0 : Convert.ToInt32(data["ValidForUpdateApi"]);

            attr.RequiredLevel = (data["RequiredLevel"] is System.DBNull) ? AttributeRequiredLevel.None : (AttributeRequiredLevel)data["RequiredLevel"];
            
            return attr;
        } */