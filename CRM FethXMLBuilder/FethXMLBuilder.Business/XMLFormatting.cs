using System;
using System.Collections.Generic;
using System.Text;
//using System.Web.Services

namespace FetchXMLBuilder.Business
{
    public class XMLFormatting
    {
        // Methods
        public static string Format(string Input)
        {
            return Input.Replace("'", "&#x27;");
        }
    }

 

}
