using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FetchXMLBuilder.UI
{
    public partial class ConditionFrm : Form
    {
        public ConditionFrm()
        {
            InitializeComponent();
        }

        public ConditionFrm(FetchXMLControl parentCtrl, Business.Filter editedFilter)
            : this()
        {

            conditionCtrl.OnLoadData(parentCtrl, editedFilter);
        }

        public ConditionFrm(FetchXMLControl parentCtrl, Business.Condition editedCondition):this()
        {            
            conditionCtrl.OnLoadData(parentCtrl, editedCondition);
        }

        
    }
}