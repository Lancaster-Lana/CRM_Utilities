using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FetchXMLBuilder.UI
{
    public partial class FilterFrm : Form
    {
        public FilterFrm()
        {
            InitializeComponent();
        }

        public FilterFrm(Business.Filter editedFilter)
            : this()
        {
            filterCtrl.OnLoadData(editedFilter);
        }
    }
}