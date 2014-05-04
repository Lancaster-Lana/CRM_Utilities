using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FetchXMLBuilder.Business
{
    [Serializable, XmlRoot("type")]
    public enum FilterType : int { and = 0, or = 1 }
}
    /*
    public class FilterType
    {
        public static string  and= "and";
        public static string  or= "or";

        public FilterType()
        {
 
        }
    }
    */

