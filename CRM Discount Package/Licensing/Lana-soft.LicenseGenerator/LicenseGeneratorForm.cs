using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using LanaSoft.Licensing;

namespace LanaSoft.LicenseGenerator
{
	public class LicenseGeneratorForm : System.Windows.Forms.Form
	{
		#region Private Constants	
		private const string		AllowedRoleName			= @"SVGCRM\Administrators";

		#endregion

		#region Private Variables
		private static LicenseLoader _licenseLoader;
		private string _defaultLicensePath;
		private System.Windows.Forms.OpenFileDialog _assemblyOpenFileDialog;
		private bool _templateOn;
		private System.Windows.Forms.PropertyGrid _propertyGrid;
		private System.Windows.Forms.SaveFileDialog _saveLicenseFileDialog;
		private System.Windows.Forms.MainMenu _mainMenu;
		private System.Windows.Forms.MenuItem _actionMI;
		private System.Windows.Forms.MenuItem _generateLicenseMI;
		private System.Windows.Forms.MenuItem _validateLicenseMI;
		private System.Windows.Forms.OpenFileDialog _licenseOpenFileDialog;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem _exitMI;
		private System.Windows.Forms.Button btnReloadTemplate;
		#endregion
		private System.Windows.Forms.ToolTip _toolTip;
		private System.ComponentModel.IContainer components;

		#region Construction
		public LicenseGeneratorForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_templateOn = (_licenseLoader.LicenseContainer.LicenseTemplateFilename != null &&
				_licenseLoader.LicenseContainer.LicenseTemplateFilename != string.Empty);
			btnReloadTemplate.Visible = _templateOn;

			_defaultLicensePath = ConfigurationSettings.AppSettings["LicenseDefaultPath"];
			_licenseOpenFileDialog.InitialDirectory = _defaultLicensePath;
			_saveLicenseFileDialog.InitialDirectory = _defaultLicensePath;

			LoadTemplate();
		}
		#endregion

		#region Protected Stuff
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(LicenseGeneratorForm));
			this._assemblyOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this._propertyGrid = new System.Windows.Forms.PropertyGrid();
			this._saveLicenseFileDialog = new System.Windows.Forms.SaveFileDialog();
			this._mainMenu = new System.Windows.Forms.MainMenu();
			this._actionMI = new System.Windows.Forms.MenuItem();
			this._generateLicenseMI = new System.Windows.Forms.MenuItem();
			this._validateLicenseMI = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this._exitMI = new System.Windows.Forms.MenuItem();
			this._licenseOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.btnReloadTemplate = new System.Windows.Forms.Button();
			this._toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// _assemblyOpenFileDialog
			// 
			this._assemblyOpenFileDialog.Filter = "Executable files (*.dll;*.exe)|*.dll;*.exe|All files (*.*)|*.*";
			this._assemblyOpenFileDialog.Title = "Select .NET Assembly Containing a License";
			// 
			// _propertyGrid
			// 
			this._propertyGrid.CommandsVisibleIfAvailable = true;
			this._propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this._propertyGrid.LargeButtons = false;
			this._propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this._propertyGrid.Location = new System.Drawing.Point(0, 0);
			this._propertyGrid.Name = "_propertyGrid";
			this._propertyGrid.Size = new System.Drawing.Size(360, 369);
			this._propertyGrid.TabIndex = 1;
			this._propertyGrid.Text = "PropertyGrid";
			this._propertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this._propertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// _saveLicenseFileDialog
			// 
			this._saveLicenseFileDialog.Filter = "License files (*.cli)|*.cli|All files (*.*)|*.*";
			this._saveLicenseFileDialog.Title = "Save Generated License As";
			// 
			// _mainMenu
			// 
			this._mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this._actionMI});
			// 
			// _actionMI
			// 
			this._actionMI.Index = 0;
			this._actionMI.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this._generateLicenseMI,
																					  this._validateLicenseMI,
																					  this.menuItem1,
																					  this._exitMI});
			this._actionMI.Text = "&Action";
			// 
			// _generateLicenseMI
			// 
			this._generateLicenseMI.Index = 0;
			this._generateLicenseMI.Text = "&Generate License";
			this._generateLicenseMI.Click += new System.EventHandler(this.MenuItem_Click);
			// 
			// _validateLicenseMI
			// 
			this._validateLicenseMI.Index = 1;
			this._validateLicenseMI.Text = "&Validate License";
			this._validateLicenseMI.Click += new System.EventHandler(this.MenuItem_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 2;
			this.menuItem1.Text = "-";
			// 
			// _exitMI
			// 
			this._exitMI.Index = 3;
			this._exitMI.Text = "E&xit";
			// 
			// _licenseOpenFileDialog
			// 
			this._licenseOpenFileDialog.Filter = "License files (*.cli)|*.cli|All files (*.*)|*.*";
			this._licenseOpenFileDialog.Title = "Validate License";
			// 
			// btnReloadTemplate
			// 
			this.btnReloadTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnReloadTemplate.Image")));
			this.btnReloadTemplate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnReloadTemplate.Location = new System.Drawing.Point(76, 4);
			this.btnReloadTemplate.Name = "btnReloadTemplate";
			this.btnReloadTemplate.Size = new System.Drawing.Size(28, 20);
			this.btnReloadTemplate.TabIndex = 2;
			this.btnReloadTemplate.Click += new System.EventHandler(this.btnReloadTemplate_Click);
			// 
			// _toolTip
			// 
			this._toolTip.AutoPopDelay = 5000;
			this._toolTip.InitialDelay = 100;
			this._toolTip.ReshowDelay = 100;
			this._toolTip.ShowAlways = true;
			// 
			// LicenseGeneratorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(360, 369);
			this.Controls.Add(this.btnReloadTemplate);
			this.Controls.Add(this._propertyGrid);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Menu = this._mainMenu;
			this.Name = "LicenseGeneratorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Lana-soft License Generator";
			this.Load += new System.EventHandler(this.LicenseGeneratorForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			System.Windows.Forms.Application.EnableVisualStyles();
			System.Windows.Forms.Application.DoEvents();

			try
			{
				_licenseLoader = new LicenseLoader();
			}
			catch (ApplicationException ex)
			{
				MessageBox.Show(ex.Message, "An error has occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (null == _licenseLoader.LicenseValidator)
				return;

			using (var licenseGeneratorForm = new LicenseGeneratorForm())
			{
				Application.Run(licenseGeneratorForm);
			}
		}

		#region Event Handlers
		private void MenuItem_Click(object sender, System.EventArgs e)
		{
			if (ReferenceEquals(sender, _generateLicenseMI))
				GenerateLicense();
			else if (Object.ReferenceEquals(sender, _validateLicenseMI))
				ValidateLicense();
			else if (Object.ReferenceEquals(sender, _exitMI))
				Close();
		}

		private void LicenseGeneratorForm_Load(object sender, System.EventArgs e)
		{
			_propertyGrid.SelectedObject = _licenseLoader.LicenseValidator;
			_toolTip.SetToolTip(this.btnReloadTemplate, "Reload template");
		}

		private void btnReloadTemplate_Click(object sender, System.EventArgs e)
		{
			LoadTemplate();
		}
		#endregion

		#region Private Stuff
		private void GenerateLicense()
		{
			// Checking if a user is valid
			bool validUser = true;
			string userName = string.Empty;
			/*
			try
			{
				WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
				userName = windowsIdentity.Name;
			
				WindowsPrincipal windowsPrincipal = new WindowsPrincipal(windowsIdentity);
				validUser = windowsPrincipal.IsInRole(AllowedRoleName);
			}
			catch
			{
				validUser = false;
			}

			if (!validUser)
			{
				MessageBox.Show(string.Format("A user '{0}' is not allowed to run application.", userName),
					"Security Check Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			*/

			if (_saveLicenseFileDialog.ShowDialog() == DialogResult.OK)
				LicenseGenerator.Generate(_licenseLoader.LicenseValidator, _saveLicenseFileDialog.FileName);
		}

		private static bool ContainsArgument(string[] arguments, string sourceArgument, bool ignoreCase)
		{
			bool result = false;

			foreach (string destArgument in arguments)
			{
				if (string.Compare(destArgument, sourceArgument, ignoreCase) == 0)
				{
					result = true;
					break;
				}
			}

			return result;
		}

		private void LoadTemplate()
		{
			if (_templateOn)
			{
				try
				{
					// Set executables path. Because in configuration file for template there cound be specified relational path.
					string appPath = Path.GetDirectoryName(Application.ExecutablePath);
					System.IO.Directory.SetCurrentDirectory(appPath);
					string licenseTemplateFilename = _licenseLoader.LicenseContainer.LicenseTemplateFilename;
				    if (licenseTemplateFilename == null) throw new ArgumentNullException("licenseTemplateFilename");
				    ((LicenseValidator)_licenseLoader.LicenseValidator).Validate(licenseTemplateFilename);
					_propertyGrid.Refresh();
				}
				catch (FileNotFoundException ex)
				{
					((LicenseValidator)_licenseLoader.LicenseValidator).Initialize();
					MessageBox.Show(this, ex.Message, "Template loading error",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				catch (LicenseValidationException ex)
				{
					((LicenseValidator)_licenseLoader.LicenseValidator).Initialize();
					MessageBox.Show(this, ex.Message, "License validation error",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void ValidateLicense()
		{
			if (_licenseOpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					((LicenseValidator)_licenseLoader.LicenseValidator).Validate(_licenseOpenFileDialog.FileName);
					_propertyGrid.Refresh();
				}
				catch (LicenseValidationException ex)
				{
					((LicenseValidator)_licenseLoader.LicenseValidator).Initialize();
					MessageBox.Show(this, ex.Message, "License validation error",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		#endregion

	}
}
