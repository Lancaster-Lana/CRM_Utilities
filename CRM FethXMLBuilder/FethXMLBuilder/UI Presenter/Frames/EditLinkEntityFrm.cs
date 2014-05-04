using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FetchXMLBuilder.UI
{
    public partial class EditLinkEntityFrm : Form
    {
        public EditLinkEntityFrm()
        {
            InitializeComponent();
        }

        public EditLinkEntityFrm(Business.LinkedEntity entityToEdit):this()
        {
            entityCtrl.OnLoadData(entityToEdit);
        }
    }
}