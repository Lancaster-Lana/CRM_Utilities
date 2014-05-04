using System.Collections;

namespace LanaSoft.LicenseGenerator
{
	/// <summary>
	/// The license container picker form.
	/// </summary>
	public class LicenseContainerPickerForm : System.Windows.Forms.Form
	{
		private ArrayList					_licenseContainers = new ArrayList();
		private LicenseContainer			_selectedLicenseContainer;

		private System.Windows.Forms.ListBox _containersListBox;
		private System.Windows.Forms.Label _label;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.Button _okButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public LicenseContainerPickerForm()
		{
			InitializeComponent();

			this.Load += new System.EventHandler(LicenseContainerPickerForm_Load);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		public ArrayList LicenseContainers
		{
			get
			{
				return _licenseContainers;
			}
		}

		public LicenseContainer SelectedLicenseContainer
		{
			get
			{
				return _selectedLicenseContainer;
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._label = new System.Windows.Forms.Label();
			this._containersListBox = new System.Windows.Forms.ListBox();
			this._cancelButton = new System.Windows.Forms.Button();
			this._okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// _label
			// 
			this._label.Location = new System.Drawing.Point(4, 4);
			this._label.Name = "_label";
			this._label.Size = new System.Drawing.Size(284, 16);
			this._label.TabIndex = 0;
			this._label.Text = "Pick the license container to load";
			// 
			// _containersListBox
			// 
			this._containersListBox.Location = new System.Drawing.Point(4, 24);
			this._containersListBox.Name = "_containersListBox";
			this._containersListBox.Size = new System.Drawing.Size(284, 199);
			this._containersListBox.TabIndex = 1;
			this._containersListBox.DoubleClick += new System.EventHandler(this.ContainersListBox_DoubleClick);
			this._containersListBox.SelectedIndexChanged += new System.EventHandler(this.ContainersListBox_SelectedIndexChanged);
			// 
			// _cancelButton
			// 
			this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancelButton.Location = new System.Drawing.Point(212, 232);
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.TabIndex = 3;
			this._cancelButton.Text = "Cancel";
			// 
			// _okButton
			// 
			this._okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._okButton.Enabled = false;
			this._okButton.Location = new System.Drawing.Point(128, 232);
			this._okButton.Name = "_okButton";
			this._okButton.TabIndex = 2;
			this._okButton.Text = "O&K";
			this._okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// LicenseContainerPickerForm
			// 
			this.AcceptButton = this._okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this._cancelButton;
			this.ClientSize = new System.Drawing.Size(292, 261);
			this.Controls.Add(this._okButton);
			this.Controls.Add(this._cancelButton);
			this.Controls.Add(this._containersListBox);
			this.Controls.Add(this._label);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "LicenseContainerPickerForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "License Container Picker";
			this.Load += new System.EventHandler(this.LicenseContainerPickerForm_Load_1);
			this.ResumeLayout(false);

		}
		#endregion

		#region Private Stuff
		private void SelectContainer()
		{
			_selectedLicenseContainer = (LicenseContainer)LicenseContainers[_containersListBox.SelectedIndex];

			DialogResult = System.Windows.Forms.DialogResult.OK;
		}
		#endregion

		#region Event Handlers
		private void LicenseContainerPickerForm_Load(object sender, System.EventArgs e)
		{
			if (null == LicenseContainers)
				return;

			foreach (LicenseContainer licenseContainer in LicenseContainers)
			{
				if (null != licenseContainer)
					_containersListBox.Items.Add(licenseContainer);
			}
		}

		private void ContainersListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			_okButton.Enabled = (_containersListBox.SelectedIndex > -1);
		}

		private void OkButton_Click(object sender, System.EventArgs e)
		{
			SelectContainer();
		}

		private void ContainersListBox_DoubleClick(object sender, System.EventArgs e)
		{
			if (_containersListBox.SelectedIndex > -1)
				SelectContainer();
		}
		#endregion

		private void LicenseContainerPickerForm_Load_1(object sender, System.EventArgs e)
		{
		
		}
	}
}
