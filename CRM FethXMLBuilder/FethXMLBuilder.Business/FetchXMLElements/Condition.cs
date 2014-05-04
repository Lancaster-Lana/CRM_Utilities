using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace FetchXMLBuilder.Business
{
    [Serializable, XmlRoot("condition")]
    public class Condition : FetchXMLElement
    {
        #region Fields

         bool _aggregate = false;
         //Attribute _attribute;
         string _attributeName = string.Empty;
         string _attributeAlias = string.Empty;
         string _conditionOperator = string.Empty;

         //string _singleValue = string.Empty;
         List<string> _values = new List<string>();

       //  Filter _parentFilter = new Filter();

        /*
        public new Filter Parent
        {
            get { return _parentFilter; }
            set { _parentFilter = value; }
        }
        */

        #endregion

        #region Properties

        [XmlAttribute(AttributeName = "attribute")]
        public string AttributeName
        {
            /*
            get { return _attribute.Name; }
            set {_attribute.Name = value; }*/
            get { return _attributeName; }
            set { _attributeName = value; }
        }

        /*[XmlAttribute(AttributeName = "attribute")]//!!!!!!!!!1
        public Attribute Attribute
        {
            get { return _attribute; }
            set { _attribute = value; }
        }*/

        [XmlAttribute(AttributeName = "operator")]
        public string Operator
        {
            get { return _conditionOperator; }
            set { _conditionOperator = value; }
        }

        //[XmlArray(ElementName = "Values")]
        //[XmlText(typeof(Value), ElementName = "value")]

        [XmlElement("value")]       
        public List<string> ValuesList// List<Value>
        {
            get {                
                return _values; 
                }
            set {
                _values = value; 
            }
        }

        [XmlAttribute("value")]
        public string SingleValue// List<Value>
        {
            get
            {
                if (GetValueType(this.Operator) == ValueType.OneValue)
                    return ValuesList[0]; 
                return null;
            }
            set 
            {
                ValuesList = new List<string>();
                ValuesList.Add(value); 
            }
        }

        [XmlAttribute(AttributeName = "aggregate")]//!!!!!!!!!1
        public bool Aggregate
        {
            get { return _aggregate; }
            set { _aggregate = value; }
        }

        [XmlAttribute(AttributeName = "alias")]
        public string AttributeAlias
        {
            get { return _attributeAlias; }
            set { _attributeAlias = value; }
        }


        #endregion

        #region Methods

        public Condition()
        {
            Operator = ConditionOperator.Eq;// "eq";
            Aggregate = false;
            AttributeAlias = string.Empty; ;
            ValuesList = new List<string>();
            //Parent = null;        
        }

        public Condition(Filter filter):this()
        { 
            /*ConditionOperator = "eq";
            Aggregate = false;
            AttributeAlias = string.Empty; ;
            Values = new ArrayList(); */
            //Parent = filter;            
        }       
        

        public static ValueType GetValueType(string conditionOperator)
        {
            string left = conditionOperator;
            if (((((Operators.CompareString(left, ConditionOperator.IsNull, false) == 0) || (Operators.CompareString(left, "not-null", false) == 0)) || ((Operators.CompareString(left, "yesterday", false) == 0) || (Operators.CompareString(left, "today", false) == 0))) || (((Operators.CompareString(left, "tomorrow", false) == 0) || (Operators.CompareString(left, "next-seven-days", false) == 0)) || ((Operators.CompareString(left, "last-seven-days", false) == 0) || (Operators.CompareString(left, "next-week", false) == 0)))) || (((((Operators.CompareString(left, "last-week", false) == 0) || (Operators.CompareString(left, "this-week", false) == 0)) || ((Operators.CompareString(left, "this-month", false) == 0) || (Operators.CompareString(left, "last-month", false) == 0))) || (((Operators.CompareString(left, "next-month", false) == 0) || (Operators.CompareString(left, "this-year", false) == 0)) || ((Operators.CompareString(left, "last-year", false) == 0) || (Operators.CompareString(left, "next-year", false) == 0)))) || (((Operators.CompareString(left, "eq-userid", false) == 0) || (Operators.CompareString(left, "ne-userid", false) == 0)) || ((Operators.CompareString(left, "eq-businessid", false) == 0) || (Operators.CompareString(left, "ne-businessid", false) == 0)))))
            {
                return ValueType.NoValue;
            }
            if (((((Operators.CompareString(left, ConditionOperator.Eq, false) == 0) || (Operators.CompareString(left, "ne", false) == 0)) || ((Operators.CompareString(left, "lt", false) == 0) || (Operators.CompareString(left, "gt", false) == 0))) || (((Operators.CompareString(left, "le", false) == 0) || (Operators.CompareString(left, "ge", false) == 0)) || ((Operators.CompareString(left, "like", false) == 0) || (Operators.CompareString(left, "not-like", false) == 0)))) || (((((Operators.CompareString(left, "on", false) == 0) || (Operators.CompareString(left, "on-or-before", false) == 0)) || ((Operators.CompareString(left, "on-or-after", false) == 0) || (Operators.CompareString(left, "last-x-hours", false) == 0))) || (((Operators.CompareString(left, "next-x-hours", false) == 0) || (Operators.CompareString(left, "last-x-days", false) == 0)) || ((Operators.CompareString(left, "next-x-days", false) == 0) || (Operators.CompareString(left, "last-x-weeks", false) == 0)))) || (((Operators.CompareString(left, "next-x-weeks", false) == 0) || (Operators.CompareString(left, "last-x-months", false) == 0)) || (((Operators.CompareString(left, "next-x-months", false) == 0) || (Operators.CompareString(left, "last-x-years", false) == 0)) || (Operators.CompareString(left, "next-x-years", false) == 0)))))
            {
                return ValueType.OneValue;
            }
            if ((Operators.CompareString(left, ConditionOperator.Between, false) == 0) || (Operators.CompareString(left, "not-between", false) == 0))
            {
                return ValueType.TwoValues;
            }
            if ((Operators.CompareString(left, ConditionOperator.In, false) != 0) && (Operators.CompareString(left, "not-in", false) != 0))
            {               
                return ValueType.MultipleValues;
            }
            return ValueType.MultipleValues;
        }

        public override string ToString()
        {
            string valuesStr = string.Empty;

            ValueType valType = GetValueType(this.Operator);

            switch (valType)//this.ValuesList.Count)
            {
                case ValueType.NoValue:
                    break;

                case  ValueType.OneValue:
                    valuesStr = " value='" + this.ValuesList[0].ToString() + "'";
                    break;

                case  ValueType.TwoValues: //Between operator
                    valuesStr = " between '" + this.ValuesList[0] + "' and '" + this.ValuesList[1] +"' ";
                    break;

                case ValueType.MultipleValues:
                    valuesStr = " values in {";
                    foreach(string val in this.ValuesList)
                    {
                        valuesStr += "'" + val + "' ";
                    }
                    valuesStr += " }";
                    break;                

            }

            StringBuilder sb = new StringBuilder();
            sb.Append("condition attribute='" + this.AttributeName + "'");
            sb.Append(" operator='" + this.Operator + "'");
            sb.Append(valuesStr);
            return sb.ToString();
                         
                         
            /*
            string attributeAlias;
            if ((this.AttributeAlias != null) && (this.AttributeAlias.Length > 0))
            {
                attributeAlias = this.AttributeAlias;
            }
            else
            {
                attributeAlias = this.AttributeName; //this.Attribute.Name;
            }
            attributeAlias = attributeAlias + " " + this.Operator;
            switch (GetValueType(this.Operator))
            {
                case ValueType.OneValue:
                    return (attributeAlias + " " + this.ValuesList[0]);

                case ValueType.TwoValues:
                    return (attributeAlias + " " + this.ValuesList[0] + " and " + this.ValuesList[1]);

                case ValueType.MultipleValues:
                    {
                        attributeAlias = attributeAlias + " (";
                        string text4 = "";
                        foreach (string text3 in this.ValuesList)
                        {
                            if (text4.Length > 0)
                            {
                                text4 = text4 + ",";
                            }
                            text4 = text4 + text3;
                        }
                        return (attributeAlias + text4 + ")");
                    }
            }
            return attributeAlias;
             */ 
        }

        public override string ToXML()
        {
            string text = ("<condition attribute='" + /*this.Attribute*/ this.AttributeName + "' ") + "operator='" + Strings.Trim(this.Operator) + "' ";
            if (this.AttributeAlias.Length > 0)
            {
                text = text + "alias='" + Strings.Trim(this.AttributeAlias) + "' ";
            }
            if (this.Aggregate)
            {
                text = text + "aggretage='true'";
            }
            switch (GetValueType(this.Operator))
            {
                case ValueType.NoValue:
                    return (text + "/>");

                case ValueType.OneValue:
                    return (text + "value='" + XMLFormatting.Format(this.ValuesList[0].ToString()) + "'/>");

                case ValueType.TwoValues:
                    return (text + "><value>" + XMLFormatting.Format(this.ValuesList[0].ToString()) + "</value><value>" + XMLFormatting.Format(this.ValuesList[1].ToString()) + "</value></condition>");

                case ValueType.MultipleValues:
                    text = text + ">";
                    foreach (string text3 in this.ValuesList)
                    {
                        text = text + "<value>" + XMLFormatting.Format(text3) + "</value>";
                    }
                    return (text + "</condition>");
            }
            return text;
        }

        #endregion

    }


}

/*
         public Condition(XmlNode conditionNode): this()
        {
            //this.Attribute.Name = conditionNode.Attributes["attribute"].Value;
            this.AttributeName = conditionNode.Attributes["attribute"].Value;
            this.Operator = conditionNode.Attributes["operator"].Value;
            if (conditionNode.Attributes["alias"] != null)
            {
                this.AttributeAlias = conditionNode.Attributes["alias"].Value;
            }

            ValueType valueType = GetValueType(Operator);
            switch (valueType)
            {
                case ValueType.OneValue:
                    this.ValuesList.Add(conditionNode.Attributes["value"].Value);
                    break;

                case ValueType.TwoValues:
                case ValueType.MultipleValues:
                    {
                        XmlNodeList list = conditionNode.SelectNodes("./value");
                        try
                        {
                            foreach (XmlNode node in list)
                            {
                                this.ValuesList.Add(node.InnerText);
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
                        break;
                    }
            }
        }
*/