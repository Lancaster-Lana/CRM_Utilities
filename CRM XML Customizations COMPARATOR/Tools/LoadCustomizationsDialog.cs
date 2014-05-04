using System;
using System.Windows.Forms;

namespace CustomizationsComparator
{
    public partial class LoadCustomizationsDialog : Form
    {

        public string firstCustFilePath;
        public string secondCustFilePath;

        public LoadCustomizationsDialog()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void firstCustLoadButton_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog();
            //fd.InitialDirectory = Assembly.GetExecutingAssembly().Location;
            fd.Filter = "XML(*.xml)|*.xml";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                firstCustFilePath = fd.FileName;
                firstCustTextBox.Text = firstCustFilePath;
            }
        }

        private void secondCustLoadButton_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog();
            //fd.InitialDirectory = Assembly.GetExecutingAssembly().Location;
            fd.Filter = "XML(*.xml)|*.xml";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                secondCustFilePath = fd.FileName;
                secondCustTextBox.Text = secondCustFilePath;
            }
        }
    }
}