using System;
using System.Drawing;
using System.Windows.Forms;
using CustomizationsComparator.Properties;

namespace CustomizationsComparator
{
    public partial class SettingsDialog : Form
    {
        public Color FirstColor
        {
            get { return firstColorBox.BackColor; }
            set { firstColorBox.BackColor = value; }
        }
        public Color SecondColor
        {
            get { return secondColorBox.BackColor; }
            set { secondColorBox.BackColor = value; }
        }
        public Color CommonColor
        {
            get { return commonColorBox.BackColor; }
            set { commonColorBox.BackColor = value; }
        }
        public Color DifferColor
        {
            get { return differColorBox.BackColor; }
            set { differColorBox.BackColor = value; }
        }
        public Color AbsentColor
        {
            get { return absentColorBox.BackColor; }
            set { absentColorBox.BackColor = value; }
        }
        public bool AutoNavigateSelectedRow
        {
            get { return autoNavigateCheckBox.Checked; }
            set { autoNavigateCheckBox.Checked = value; }
        }
        public bool ShowCurrentCellContentOnForm
        {
            get { return showCellContentOnFormRadioButton.Checked; }
            set
            {
                showCellContentOnFormRadioButton.Checked = value;
                //showCurrentCellContentCheckBox.Checked = showCellContentOnFormRadioButton.Checked;
            }
        }

        public bool LoadCellToWordPad
        {
            get { return wordPadRadioButton.Checked; }
            set
            {
                wordPadRadioButton.Checked = value;
                //showCurrentCellContentCheckBox.Checked = wordPadRadioButton.Checked;
            }
        }

        public SettingsDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Settings.Default.FirstColor = FirstColor;
            Settings.Default.SecondColor = SecondColor;
            Settings.Default.CommonColor = CommonColor;
            Settings.Default.DifferColor = DifferColor;
            Settings.Default.AbsentColor = AbsentColor;
            Settings.Default.AutoNavigateSelectedRow = AutoNavigateSelectedRow;

            Settings.Default.ShowCurrentCellContentOnForm = ShowCurrentCellContentOnForm;
            Settings.Default.LoadCellToWordPad = LoadCellToWordPad;
            Settings.Default.LoadCellToWordPad = LoadCellToWordPad;
        }

        private void colorFirstButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                FirstColor = cd.Color;
            }
        }

        private void colorSecondButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                SecondColor = cd.Color;
            }
        }

        private void colorCommonButton_Click(object sender, EventArgs e)
        {
            var cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                CommonColor = cd.Color;
            }
        }

        private void differColorButton_Click(object sender, EventArgs e)
        {
            var cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                DifferColor = cd.Color;
            }
        }

        private void absentButton_Click(object sender, EventArgs e)
        {
            var cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                AbsentColor = cd.Color;
            }
        }

        private void autoNavigateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AutoNavigateSelectedRow = autoNavigateCheckBox.Checked;
        }

        private void showCurrentCellContentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            showCellGroupBox.Enabled = ((CheckBox)sender).Checked;

            ShowCurrentCellContentOnForm = showCellContentOnFormRadioButton.Checked && showCurrentCellContentCheckBox.Checked;
            LoadCellToWordPad = wordPadRadioButton.Checked && showCurrentCellContentCheckBox.Checked;
        }

        private void showCellContentOnFormRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ShowCurrentCellContentOnForm = showCellContentOnFormRadioButton.Checked;
            LoadCellToWordPad = wordPadRadioButton.Checked;
        }

        private void wordPadRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ShowCurrentCellContentOnForm = showCellContentOnFormRadioButton.Checked;
            LoadCellToWordPad = wordPadRadioButton.Checked;
        }

        private void SettingsDialog_Load(object sender, EventArgs e)
        {
            FirstColor = Settings.Default.FirstColor;
            SecondColor = Settings.Default.SecondColor;
            CommonColor = Settings.Default.CommonColor;
            DifferColor = Settings.Default.DifferColor;
            AbsentColor = Settings.Default.AbsentColor;
            AutoNavigateSelectedRow = Settings.Default.AutoNavigateSelectedRow;

            ShowCurrentCellContentOnForm = Settings.Default.ShowCurrentCellContentOnForm;
            LoadCellToWordPad = Settings.Default.LoadCellToWordPad;
            showCurrentCellContentCheckBox.Checked = ShowCurrentCellContentOnForm || LoadCellToWordPad;
        }
    }
}