using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Data;
using System.IO;

using CustomizationsComparator.FormXml;

namespace CustomizationsComparator
{
    enum CompareItemsType { attr = 0, field = 1, form = 2};
    enum CustomizationNumber { first = 1, second = 2 };

    /// <summary>
    /// Class to store pair of values ( of certain type)
    /// </summary>
    public class CompareItem
    {
        object _firstvalue;
        object _secondvalue;

        public object Firstvalue
        {
            get { return _firstvalue; }
            set { _firstvalue = value; }
        }
        public object Secondvalue
        {
            get { return _secondvalue; }
            set { _secondvalue = value; }
        }

        public CompareItem(string firstvalue, string secondvalue)
        {
            Firstvalue = firstvalue;
            Secondvalue = secondvalue;
        }

        public CompareItem(object firstvalue, object secondvalue)
        {
            Firstvalue = firstvalue;
            Secondvalue = secondvalue;
        }
    }

    /// <summary>
    /// Class to store pairs of properties differ by value for two
    /// </summary>
    public class DifferPropertiesDictionary
    {        
        Dictionary<string, CompareItem> _items;

        public CompareItem this[string propertyName]
        {
            get { return _items[propertyName]; }
            set { _items[propertyName] = value; }
        }

        public bool IsEmpty
        {
            get {
                if((_items != null)||(_items.Count == 0))
                    return true;
                return false;
                }
        }

        public Dictionary<string, CompareItem> Items
        {
            get
            {
                return _items;
            }
        }

        /*
        /// <summary>
        /// Differ Names
        /// </summary>
        public IEnumerator<string> PropertiesNames
        {
            get
            {
                return _items.Keys.GetEnumerator();
            }
        }
        */

        /// <summary>
        /// Differ Properties of two elements are being compared
        /// </summary>
        public List<string> Properties
        {
            get
            {
                List<string> cmpProps = new List<string>();

                IEnumerator ien = _items.Keys.GetEnumerator();
                while (ien.MoveNext())
                {
                    string propname = ien.Current.ToString();
                    cmpProps.Add(propname);//, _items[propname]);
                }
                return cmpProps;
            }
        }

        public DifferPropertiesDictionary()
        {
            _items = new Dictionary<string,CompareItem>();
        }

        public DifferPropertiesDictionary(Dictionary<string, CompareItem> items)
        {
            _items = items;
        }

        /// <summary>
        /// GENERATE List of differ props of first and second objects
        /// </summary>
        /// <param name="compItem1"></param>
        /// <param name="compItem2"></param>
        public DifferPropertiesDictionary(object cmp1, object cmp2)
        {
            _items = new Dictionary<string, CompareItem>();
            
            Type itemsType = null;

            if (cmp1 != null) itemsType = cmp1.GetType();
            else if (cmp2 != null) itemsType = cmp2.GetType();

            if (itemsType != null)
            {
                PropertyInfo[] props = itemsType.GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    object cmpProp1 = (cmp1 != null)? prop.GetValue(cmp1, null): null;
                    object cmpProp2 = (cmp2 != null)? prop.GetValue(cmp2, null):null;
                    CompareItem cmpProp = new CompareItem(cmpProp1, cmpProp2);
                    _items.Add(prop.Name, cmpProp);
                }
            }
        }


        public void Add(string propertyName, CompareItem cmpItem)
        {
            Items[propertyName] = cmpItem;
        }

        public void Remove(string propertyName)
        {
            Items.Remove(propertyName);
        }

    }

    public class CompareHelper
    {

        public static bool CompareAttributes(CustomizationAttribute attr1, CustomizationAttribute attr2, out DifferPropertiesDictionary diffPropsList)
        {
            return GetDifferProperties(attr1, attr2, typeof(CustomizationAttribute), out diffPropsList);
        }

        public static bool CompareFields(object fld1, object fld2, string attrTypeName1, string attrTypeName2, out DifferPropertiesDictionary diffPropsList)
        {
            if (attrTypeName1.Equals(attrTypeName2))
            {
                Type fieldAttrType = GetFieldTypeByAttributeTypeName(attrTypeName1);
                return GetDifferProperties(fld1, fld2, fieldAttrType, out diffPropsList);
            }
            else
            {
                //Get differ original properties
                diffPropsList = new DifferPropertiesDictionary();
                DifferPropertiesDictionary diffPropsList1;
                DifferPropertiesDictionary diffPropsList2;
               
                Type fieldAttrType1 = GetFieldTypeByAttributeTypeName(attrTypeName1);
                    GetDifferProperties(fld1, fld2, fieldAttrType1, out diffPropsList1);

                Type fieldAttrType2 = GetFieldTypeByAttributeTypeName(attrTypeName2);
                    GetDifferProperties(fld1, fld2, fieldAttrType2, out diffPropsList2);

                    foreach (string propName in diffPropsList1.Items.Keys)
                    {
                        if(!diffPropsList.Items.ContainsKey(propName))
                            diffPropsList.Add(propName, diffPropsList1.Items[propName]);
                    }

                    foreach (string propName in diffPropsList2.Items.Keys)
                    {
                        if (!diffPropsList.Items.ContainsKey(propName))
                           diffPropsList.Add(propName, diffPropsList2.Items[propName]);
                    }
            }
                return true;
            
        }

        public static bool CompareCells(Cell ctrl1, Cell ctrl2, out DifferPropertiesDictionary diffPropsList)
        {
            return GetDifferProperties(ctrl1, ctrl2, typeof(Cell), out diffPropsList);
        }

        public static bool CompareFormEvents(FormEvent event1, FormEvent event2, out DifferPropertiesDictionary diffPropsList)
        {
            return GetDifferProperties(event1, event2, typeof(FormEvent), out diffPropsList);
        }

        public static bool CompareFormData(FormData data1, FormData data2, out DifferPropertiesDictionary diffPropsList)
        {
            return GetDifferProperties(data1, data2, typeof(FormData), out diffPropsList);
        }

        
        public static bool EqualOptionalLists(IList lst1, IList lst2)
        {
            bool isEqual = true;

            // Compare options of first and second lists
            if (lst1.Count == lst2.Count)
            {
                if (lst1.Count == 0)
                    isEqual = true;
                else
                {
                    if ((lst1[0] is DisplayName) || (lst1[0] is Option) || (lst1[0] is State) || (lst1[0] is Status))
                    {
                        //Compare items of first and second lists
                        foreach (object item in lst1)
                        {
                            if (!lst2.Contains(item))
                                isEqual = false;
                        }
                    }
                    else// No way to compare items of unknown type
                    {
                        //Implement !!!!!!!!!!!!!!!!1
                        isEqual = false;
                    }
                }
            }

            return isEqual;
        }

        public static bool CompareLabels(List<DisplayName> lst1, List<DisplayName> lst2)
        {
            if (lst1.Count == lst2.Count)
            {
                foreach (DisplayName label in lst1)
                    if (!lst2.Contains(label)) return false;

                return true;
            }
            return false;
     }

        public static bool CompareOptionalProperties(string diffPropName, IList lst1, IList lst2, ref DifferPropertiesDictionary diffList)
        {
            bool isEqual = true;

            // Compare options of first and second lists
            if ((lst1 != null) && (lst2 != null))
            {
                if (lst1.Count == lst2.Count)
                {
                    if (lst1.Count == 0)
                        isEqual = true;
                    else
                    {
                        if ((lst1[0] is DisplayName) || (lst1[0] is Option) || (lst1[0] is State) || (lst1[0] is Status)
                            || (lst1[0] is FormEvent) || (lst1[0] is Dependency))
                        {
                            //Compare items of first and second lists
                            foreach (object item in lst1)
                            {
                                if (!lst2.Contains(item))
                                    isEqual = false;
                            }
                        }
                        else// No way to compare items of unknown type
                        {
                            //Implement !!!!!!!!!!!!!!!!1
                            isEqual = false;
                        }
                    }
                }
            }
            else if ((lst1 == null) && (lst2 == null))
            {
                isEqual = true;
            }
            else
                isEqual = false;

            if(!isEqual)
                diffList.Add(diffPropName, new CompareItem(lst1, lst2));

            return isEqual;
       }


        enum SubElementType { attribute = 0, property = 2, unknown =3 };

        public static bool CompareCustomAttrsPropsLists(IDictionary lst1, IDictionary lst2, ref DifferPropertiesDictionary diffList)
        {
            bool areEqual = true;

            //1. First and second list contain CustomAttributes/ Properties =>          
            SubElementType itemType = SubElementType.unknown;

            //1. Detect type of compareed items
            IEnumerator ien = lst1.Values.GetEnumerator();
            ien.MoveNext();

            if (ien.Current is CustomAttribute) 
                itemType = SubElementType.attribute;
            else if (ien.Current is CustomProperty) 
                itemType = SubElementType.property;
            

            //2.More detail analyze Compare lists of items
                if (lst2 != null)
                {                              
                    string[] remainKeys = new string[lst2.Keys.Count];
                    if (remainKeys != null)
                    {
                        lst2.Keys.CopyTo(remainKeys, 0);
                        List<string> remainKeysList = new List<string>();
                        remainKeysList.AddRange(remainKeys);

                        //1.2.1 First list of props
                        foreach (string key in lst1.Keys)
                        {
                            string custValue1 = (itemType == SubElementType.attribute)? (lst1[key] as CustomAttribute).Value
                                             : ((itemType == SubElementType.property) ? (lst1[key] as CustomProperty).XMLContent 
                                             : lst1[key].ToString());

                            if (lst2.Contains(key))
                            {
                                string custValue2 = (itemType == SubElementType.attribute) ? (lst2[key] as CustomAttribute).Value
                                                    : ((itemType == SubElementType.property) ? (lst2[key] as CustomProperty).XMLContent
                                                    : lst2[key].ToString());
                                if (!custValue1.Equals(custValue2))
                                {
                                    areEqual = false;
                                    diffList.Add(key, new CompareItem(custValue1, custValue2));
                                }
                                remainKeysList.Remove(key);
                            }
                            else
                            {
                                areEqual = false;
                                diffList.Add(key, new CompareItem(custValue1, "null"));
                            }

                        }

                        //1.2.2 Second list contains attributes wich absent in first list
                        foreach (string key in remainKeysList)
                        {
                            areEqual = false;
                            string custValue = (itemType == SubElementType.attribute) ? (lst2[key] as CustomAttribute).Value
                                                : ((itemType == SubElementType.property) ? (lst2[key] as CustomProperty).XMLContent
                                                : lst2[key].ToString());
                            diffList.Add(key, new CompareItem("null", custValue));
                        }
                    }                
            }

            //3 Only for Grid Views - NOT FOR REPORT  !!!!!!!!!! $+
            if (!areEqual)
            {
                if (itemType == SubElementType.attribute)
                    diffList.Add(simplefield.GetCustomAttributeCollectionName(), new CompareItem(lst1, lst2));
                else if (itemType == SubElementType.property)
                    diffList.Add(simplefield.GetCustomPropertyCollectionName(), new CompareItem(lst1, lst2));
            }

            return areEqual;
        }

        public static bool GetDifferProperties(object firstElem, object secondElem, Type elemType, out DifferPropertiesDictionary diffList)
        {
            diffList = new DifferPropertiesDictionary();

            PropertyInfo[] props = elemType.GetProperties();

            foreach (PropertyInfo fieldProperty in props)
            {
                string diffPropName = fieldProperty.Name;

                object firstPropValue = null;
                    try {firstPropValue = fieldProperty.GetValue(firstElem, null);}
                    catch { firstPropValue = null; }
                object secondPropValue = null;
                    try { secondPropValue = fieldProperty.GetValue(secondElem, null); }
                    catch { secondPropValue = null; }
                /*
                if ((firstPropValue == null) && (secondPropValue != null))
                {
                    diffList.Add(diffPropName, new CompareItem(null, secondPropValue));
                }
                else if ((secondPropValue == null) && (firstPropValue != null))
                {
                    diffList.Add(diffPropName, new CompareItem(firstPropValue, null));
                }
                else if ((firstPropValue != null) && (secondPropValue != null))
                {*/
                    bool isArray = firstPropValue is ICollection;

                    if (!isArray)
                    {
                        if (!object.Equals(firstPropValue, secondPropValue))
                            diffList.Add(diffPropName, new CompareItem(firstPropValue, secondPropValue));
                    }
                    else if (((ICollection)firstPropValue).Count > 0)
                    {
                        if (firstPropValue is IDictionary)
                        {
                            CompareCustomAttrsPropsLists((IDictionary)firstPropValue, (IDictionary)secondPropValue, ref diffList);
                         }
                        else if (firstPropValue is IList)// compound (optional) properties
                        {
                            //2. Compare subitems of  property of the element(attribute|field)                    
                            CompareOptionalProperties(diffPropName, (IList)firstPropValue, (IList)secondPropValue, ref diffList);
                        }
                        else
                        { 
                            //implement !!!!!!!!!!
                        }
                    }
                }
           // }

            if (diffList.Items.Count > 0)
                return true;

            return false;
        }
      
        public static void DetectCommonAndOriginalNames(IEnumerator firstIEn, IEnumerator secondIEn,
        out List<string> firstOriginalList, out List<string> secondOriginalList,
        out List<string> commonList)
        {
            List<string> lst1 = new List<string>();
            List<string> lst2 = new List<string>();

            while (firstIEn.MoveNext())
            {
                string name = firstIEn.Current.ToString();
                lst1.Add(name);
            }

            while (secondIEn.MoveNext())
            {
               string name = secondIEn.Current.ToString();
               lst2.Add(name);
            }

             DetectCommonAndOriginalNames(lst1, lst2, out firstOriginalList, out secondOriginalList, out commonList);
        }

        public static void DetectCommonAndOriginalNames(List<string> firstLst, List<string> secondLst,
          out List<string> firstOriginalLst, out List<string> secondOriginalLst,
          out List<string> commonLst)
        {
            firstOriginalLst = firstLst.FindAll(delegate(string name) { return !secondLst.Contains(name); }); ;
            secondOriginalLst = secondLst.FindAll(delegate(string name) { return !firstLst.Contains(name); });
            commonLst = firstLst.FindAll(delegate(string name) { return secondLst.Contains(name); });          
        }

        /// <summary>
        /// Special for events
        /// </summary>
        /// <param name="firstLst"></param>
        /// <param name="secondLst"></param>
        /// <param name="firstOriginalLst"></param>
        /// <param name="secondOriginalLst"></param>
        /// <param name="commonLst"></param>
        public static void DetectCommonAndOriginalNames(IEnumerator firstIEn, IEnumerator secondIEn,
            out List<KeyValuePair<string, bool>> firstOriginalLst, out List<KeyValuePair<string, bool>> secondOriginalLst,
            out List<KeyValuePair<string, bool>> commonLst)
        {
            firstOriginalLst = new List<KeyValuePair<string, bool>>();
            secondOriginalLst = new List<KeyValuePair<string, bool>>();
            commonLst = new List<KeyValuePair<string, bool>>();

            List<KeyValuePair<string, bool>> lst1 = new List<KeyValuePair<string, bool>>();
            List<KeyValuePair<string, bool>> lst2 = new List<KeyValuePair<string, bool>>();

            while (firstIEn.MoveNext())
            {
                KeyValuePair<string, bool> formevent = (KeyValuePair<string, bool>)firstIEn.Current;
                lst1.Add(formevent);
            }

            while (secondIEn.MoveNext())
            {
                KeyValuePair<string, bool> formevent = (KeyValuePair<string, bool>)secondIEn.Current;
                lst2.Add(formevent);
            }

            //Dictionary<string, bool>.KeyCollection
            foreach (KeyValuePair<string, bool> formevent in lst1)
            {
                if (!lst2.Contains(formevent))
                    firstOriginalLst.Add(formevent);
                else
                    commonLst.Add(formevent);
            }


            foreach (KeyValuePair<string, bool> formevent in lst2)
            {
                if (!lst1.Contains(formevent))
                    secondOriginalLst.Add(formevent);
            }
        }

        public static Type GetFieldTypeByAttributeTypeName(string attrType)
        {           
            switch (attrType)
            {
                case AttributeType.None:
                case AttributeType.Lookup:
                case AttributeType.PrimaryKey:
                case AttributeType.UniqueIdentifier:
                    return typeof(simplefield);

                case AttributeType.NText:
                case AttributeType.NVarChar:
                case AttributeType.Memo:
                    return typeof(TextField);

                case AttributeType.Decimal:
                case AttributeType.Integer:
                case AttributeType.Float:
                case AttributeType.Money:
                    return typeof(DecimalField);

                case AttributeType.DateTime:
                    return typeof(DateTimeField);

                case AttributeType.Bit:
                case AttributeType.Boolean:                
                case AttributeType.Picklist:
                    return typeof(PickListField);

                case AttributeType.PartyList:
                    return typeof(PartyListField);

                case AttributeType.State:
                    return typeof(StateField);

                case AttributeType.Status:
                    return typeof(StatusField);

            }
            return typeof(simplefield);
        }


        public static string ToString(object obj)
        {
            string convertedObj = string.Empty;
            if ((obj is IList) || (obj is IDictionary))
            {
                IEnumerator ien = (obj is IList) ? ((IList)obj).GetEnumerator() : ((IDictionary)obj).Values.GetEnumerator();
                while (ien.MoveNext())
                    convertedObj += ien.Current.ToString() + "\r\n";
            }
            else
            {
                if (obj != null)
                     convertedObj = obj.ToString();
            }

            return convertedObj;
        }

    }
}
