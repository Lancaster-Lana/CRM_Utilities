using System;
using System.Collections.Generic;
using System.Text;

namespace FetchXMLBuilder.Business
{
    public interface IFetchXMLElement
    {

        IFetchXMLElement Parent { get; }
    }
}
