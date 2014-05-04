using System;
using System.Collections.Generic;
using System.Text;
using FetchXMLBuilder.Business;

namespace FetchXMLBuilder.UI
{
    public interface IEditManager
    {
        void LoadDataToCtrl(Business.FetchXMLElement elementData);        
        Business.FetchXMLElement GetDataFromCtrl();

    }
}
