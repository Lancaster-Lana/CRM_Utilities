using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FetchXMLBuilder.UI
{

    public partial class FetchXMLForm : Form
    {
        public FetchXMLForm()
        {
            InitializeComponent();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "*.xml|*.xml";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(fd.OpenFile());
                string fetchXMLStr = sr.ReadToEnd();
                this.fetchXMLControl1.LoadFetchXML(fetchXMLStr);
                sr.Close();
            }

        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "*.xml|*.xml";
            if (fd.ShowDialog() == DialogResult.OK)
                this.fetchXMLControl1.SaveToXMLFile(fd.FileName);// SaveToXML();           

        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                fetchXMLControl1.NewTemplate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}