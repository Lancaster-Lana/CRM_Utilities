using System;
using System.Xml;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using LanaSoftCRM;

namespace WinDiscountSetup
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class DiscountPackageSetupForm : System.Windows.Forms.Form
    {
        #region Design

        private System.Windows.Forms.Timer processTimer;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem17;
        private System.Windows.Forms.TabPage AutoTabPage;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button AutomaticApplyButton;
        private System.Windows.Forms.GroupBox DiscountFullCusomizationGroupBox;
        private System.Windows.Forms.CheckBox NewEntitiesGheckBox;
        private System.Windows.Forms.CheckBox SystemEntitiesCustomizationCheckBox;
        private System.Windows.Forms.GroupBox NewEntitiesGroupBox;
        private System.Windows.Forms.CheckBox ImportNewEntitiesCheckBox;
        private System.Windows.Forms.CheckBox CorrectISVCheckBox;
        private System.Windows.Forms.CheckBox CorrectSiteMapCheckBox;
        private System.Windows.Forms.TabPage ManualTabPage;
        private System.Windows.Forms.GroupBox AdditionalGroupBox;
        private System.Windows.Forms.RadioButton ViewCustomizationFilesRadioButton;
        private System.Windows.Forms.Panel GetCodesPanel;
        private System.Windows.Forms.Button GetEntityCodeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox EntityCodeTextBox;
        private System.Windows.Forms.ComboBox EntitiesComboBox;
        private System.Windows.Forms.RadioButton GetCodesRadioButton;
        private System.Windows.Forms.RadioButton CorrectExistingCustFilesRadioButton;
        private System.Windows.Forms.Button AdditionalButton;
        private System.Windows.Forms.TabControl SetupTabControl;
        private System.Windows.Forms.ProgressBar customizatinProgressBar;
        private System.Windows.Forms.ListBox StateListBox;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Label InitialPathCustLabel;
        private System.Windows.Forms.MenuItem SaveCustLogFileMenuItem;
        private System.Windows.Forms.MenuItem AutoCustMenuItem;
        private System.Windows.Forms.MenuItem OpenMenuItem;
        private System.Windows.Forms.MenuItem NewEntitiesMenuItem6;
        private System.Windows.Forms.Button ViewCustomizationsButton;
        private System.Windows.Forms.MenuItem CustomizationMenuItem;
        private System.Windows.Forms.MenuItem ExitMenuItem;
        private System.Windows.Forms.MenuItem ImportNewEntitiesMenuItem;
        private System.Windows.Forms.MenuItem CorrectCustomizationFiles;
        private System.Windows.Forms.MenuItem AllCustomizationsMenuItem;
        private System.Windows.Forms.MenuItem AccountMenuItem;
        private System.Windows.Forms.MenuItem QuoteMenuItem;
        private System.Windows.Forms.MenuItem ProductMenuItem;
        private System.Windows.Forms.MenuItem QuoteProductMenuItem;

        private System.Windows.Forms.MenuItem ISVMenuItem;
        private System.Windows.Forms.MenuItem QuoteproductOnLoadMenuItem;
        private System.Windows.Forms.MenuItem QuoteproductOnChangePCMenuItem;
        private System.Windows.Forms.MenuItem onChangeProductMenuItem;
        private System.Windows.Forms.MenuItem OnChangePercMenuItem;
        private System.Windows.Forms.RadioButton EditRolesRadioButton;
        private System.Windows.Forms.Button EditRoledButton;
        private System.ComponentModel.IContainer components;

        public DiscountPackageSetupForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
              CRMDiscountPackageLicenseApplier _license = new Celenia.Licensing.Usage.CRMDiscountPackageLicenseApplier();
              if(_license.LicenseStatus == CRMDiscountPackageLicenseApplier.ErrorLicenseCode.licenseisefficient)		
                Application.Run(new DiscountPackageSetupForm());	
              else if(_license.LicenseStatus == CRMDiscountPackageLicenseApplier.ErrorLicenseCode.filenotfount)
                   MessageBox.Show("License file not fount! Generate License and put it to current folder.");
            */
            Application.Run(new DiscountPackageSetupForm());
        }
        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.processTimer = new System.Windows.Forms.Timer(this.components);
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.OpenMenuItem = new System.Windows.Forms.MenuItem();
            this.NewEntitiesMenuItem6 = new System.Windows.Forms.MenuItem();
            this.AllCustomizationsMenuItem = new System.Windows.Forms.MenuItem();
            this.AccountMenuItem = new System.Windows.Forms.MenuItem();
            this.QuoteMenuItem = new System.Windows.Forms.MenuItem();
            this.ProductMenuItem = new System.Windows.Forms.MenuItem();
            this.QuoteProductMenuItem = new System.Windows.Forms.MenuItem();
            this.QuoteproductOnLoadMenuItem = new System.Windows.Forms.MenuItem();
            this.QuoteproductOnChangePCMenuItem = new System.Windows.Forms.MenuItem();
            this.OnChangePercMenuItem = new System.Windows.Forms.MenuItem();
            this.onChangeProductMenuItem = new System.Windows.Forms.MenuItem();
            this.ISVMenuItem = new System.Windows.Forms.MenuItem();
            this.SaveCustLogFileMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.ExitMenuItem = new System.Windows.Forms.MenuItem();
            this.CustomizationMenuItem = new System.Windows.Forms.MenuItem();
            this.AutoCustMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.ImportNewEntitiesMenuItem = new System.Windows.Forms.MenuItem();
            this.CorrectCustomizationFiles = new System.Windows.Forms.MenuItem();
            this.AutoTabPage = new System.Windows.Forms.TabPage();
            this.StopButton = new System.Windows.Forms.Button();
            this.AutomaticApplyButton = new System.Windows.Forms.Button();
            this.DiscountFullCusomizationGroupBox = new System.Windows.Forms.GroupBox();
            this.NewEntitiesGheckBox = new System.Windows.Forms.CheckBox();
            this.SystemEntitiesCustomizationCheckBox = new System.Windows.Forms.CheckBox();
            this.NewEntitiesGroupBox = new System.Windows.Forms.GroupBox();
            this.ImportNewEntitiesCheckBox = new System.Windows.Forms.CheckBox();
            this.CorrectISVCheckBox = new System.Windows.Forms.CheckBox();
            this.CorrectSiteMapCheckBox = new System.Windows.Forms.CheckBox();
            this.ManualTabPage = new System.Windows.Forms.TabPage();
            this.AdditionalGroupBox = new System.Windows.Forms.GroupBox();
            this.EditRolesRadioButton = new System.Windows.Forms.RadioButton();
            this.EditRoledButton = new System.Windows.Forms.Button();
            this.ViewCustomizationsButton = new System.Windows.Forms.Button();
            this.InitialPathCustLabel = new System.Windows.Forms.Label();
            this.ViewCustomizationFilesRadioButton = new System.Windows.Forms.RadioButton();
            this.GetCodesPanel = new System.Windows.Forms.Panel();
            this.GetEntityCodeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.EntityCodeTextBox = new System.Windows.Forms.TextBox();
            this.EntitiesComboBox = new System.Windows.Forms.ComboBox();
            this.GetCodesRadioButton = new System.Windows.Forms.RadioButton();
            this.CorrectExistingCustFilesRadioButton = new System.Windows.Forms.RadioButton();
            this.AdditionalButton = new System.Windows.Forms.Button();
            this.SetupTabControl = new System.Windows.Forms.TabControl();
            this.customizatinProgressBar = new System.Windows.Forms.ProgressBar();
            this.StateListBox = new System.Windows.Forms.ListBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.AutoTabPage.SuspendLayout();
            this.DiscountFullCusomizationGroupBox.SuspendLayout();
            this.NewEntitiesGroupBox.SuspendLayout();
            this.ManualTabPage.SuspendLayout();
            this.AdditionalGroupBox.SuspendLayout();
            this.GetCodesPanel.SuspendLayout();
            this.SetupTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // processTimer
            // 
            this.processTimer.Enabled = true;
            this.processTimer.Tick += new System.EventHandler(this.processTimer_Tick);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.CustomizationMenuItem});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.OpenMenuItem,
            this.SaveCustLogFileMenuItem,
            this.menuItem17,
            this.ExitMenuItem});
            this.menuItem1.Text = "File";
            // 
            // OpenMenuItem
            // 
            this.OpenMenuItem.Index = 0;
            this.OpenMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.NewEntitiesMenuItem6,
            this.AllCustomizationsMenuItem,
            this.ISVMenuItem});
            this.OpenMenuItem.Text = "Open customization file";
            // 
            // NewEntitiesMenuItem6
            // 
            this.NewEntitiesMenuItem6.Index = 0;
            this.NewEntitiesMenuItem6.Text = "New Entities";
            this.NewEntitiesMenuItem6.Click += new System.EventHandler(this.NewEntitiesOpen_Click);
            // 
            // AllCustomizationsMenuItem
            // 
            this.AllCustomizationsMenuItem.Index = 1;
            this.AllCustomizationsMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.AccountMenuItem,
            this.QuoteMenuItem,
            this.ProductMenuItem,
            this.QuoteProductMenuItem});
            this.AllCustomizationsMenuItem.Text = "System Entities forms events ";
            // 
            // AccountMenuItem
            // 
            this.AccountMenuItem.Index = 0;
            this.AccountMenuItem.Text = "Account  (onLoad)";
            this.AccountMenuItem.Click += new System.EventHandler(this.AccountMenuItem_Click);
            // 
            // QuoteMenuItem
            // 
            this.QuoteMenuItem.Index = 1;
            this.QuoteMenuItem.Text = "Quote (onLoad)";
            this.QuoteMenuItem.Click += new System.EventHandler(this.QuoteMenuItem_Click);
            // 
            // ProductMenuItem
            // 
            this.ProductMenuItem.Index = 2;
            this.ProductMenuItem.Text = "Product (onLoad)";
            this.ProductMenuItem.Click += new System.EventHandler(this.ProductMenuItem_Click);
            // 
            // QuoteProductMenuItem
            // 
            this.QuoteProductMenuItem.Index = 3;
            this.QuoteProductMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.QuoteproductOnLoadMenuItem,
            this.QuoteproductOnChangePCMenuItem});
            this.QuoteProductMenuItem.Text = "Quote Product";
            // 
            // QuoteproductOnLoadMenuItem
            // 
            this.QuoteproductOnLoadMenuItem.Index = 0;
            this.QuoteproductOnLoadMenuItem.Text = "onLoad";
            this.QuoteproductOnLoadMenuItem.Click += new System.EventHandler(this.QuoteproductOnLoadMenuItem_Click);
            // 
            // QuoteproductOnChangePCMenuItem
            // 
            this.QuoteproductOnChangePCMenuItem.Index = 1;
            this.QuoteproductOnChangePCMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.OnChangePercMenuItem,
            this.onChangeProductMenuItem});
            this.QuoteproductOnChangePCMenuItem.Text = "onChange fileld";
            // 
            // OnChangePercMenuItem
            // 
            this.OnChangePercMenuItem.Index = 0;
            this.OnChangePercMenuItem.Text = "Discount(%)";
            this.OnChangePercMenuItem.Click += new System.EventHandler(this.OnChangePercMenuItem_Click);
            // 
            // onChangeProductMenuItem
            // 
            this.onChangeProductMenuItem.Index = 1;
            this.onChangeProductMenuItem.Text = "Product";
            this.onChangeProductMenuItem.Click += new System.EventHandler(this.onChangeProductMenuItem_Click);
            // 
            // ISVMenuItem
            // 
            this.ISVMenuItem.Index = 2;
            this.ISVMenuItem.Text = "ISV.config.xml";
            this.ISVMenuItem.Click += new System.EventHandler(this.ISVMenuItem_Click);
            // 
            // SaveCustLogFileMenuItem
            // 
            this.SaveCustLogFileMenuItem.Index = 1;
            this.SaveCustLogFileMenuItem.Text = "Save (Customization Log)";
            this.SaveCustLogFileMenuItem.Click += new System.EventHandler(this.SaveCustLogFileMenuItem_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 2;
            this.menuItem17.Text = "-";
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Index = 3;
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // CustomizationMenuItem
            // 
            this.CustomizationMenuItem.Index = 1;
            this.CustomizationMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.AutoCustMenuItem,
            this.menuItem3});
            this.CustomizationMenuItem.Text = "Customization";
            // 
            // AutoCustMenuItem
            // 
            this.AutoCustMenuItem.Index = 0;
            this.AutoCustMenuItem.Text = "Auto (For existing MS CRM 3.0)";
            this.AutoCustMenuItem.Click += new System.EventHandler(this.AutoCustMenuItem_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ImportNewEntitiesMenuItem,
            this.CorrectCustomizationFiles});
            this.menuItem3.Text = "Manual";
            // 
            // ImportNewEntitiesMenuItem
            // 
            this.ImportNewEntitiesMenuItem.Index = 0;
            this.ImportNewEntitiesMenuItem.Text = "Import new entities";
            this.ImportNewEntitiesMenuItem.Click += new System.EventHandler(this.ImportNewEntitiesMenuItem_Click);
            // 
            // CorrectCustomizationFiles
            // 
            this.CorrectCustomizationFiles.Index = 1;
            this.CorrectCustomizationFiles.Text = "Correct  customization files (to customize system entities  manually )";
            this.CorrectCustomizationFiles.Click += new System.EventHandler(this.CorrectCustomizationFiles_Click);
            // 
            // AutoTabPage
            // 
            this.AutoTabPage.Controls.Add(this.StopButton);
            this.AutoTabPage.Controls.Add(this.AutomaticApplyButton);
            this.AutoTabPage.Controls.Add(this.DiscountFullCusomizationGroupBox);
            this.AutoTabPage.Location = new System.Drawing.Point(4, 22);
            this.AutoTabPage.Name = "AutoTabPage";
            this.AutoTabPage.Size = new System.Drawing.Size(696, 222);
            this.AutoTabPage.TabIndex = 0;
            this.AutoTabPage.Text = "Auto";
            // 
            // StopButton
            // 
            this.StopButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StopButton.Enabled = false;
            this.StopButton.Location = new System.Drawing.Point(592, 56);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(56, 23);
            this.StopButton.TabIndex = 23;
            this.StopButton.Text = "Stop";
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // AutomaticApplyButton
            // 
            this.AutomaticApplyButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AutomaticApplyButton.Location = new System.Drawing.Point(592, 24);
            this.AutomaticApplyButton.Name = "AutomaticApplyButton";
            this.AutomaticApplyButton.Size = new System.Drawing.Size(56, 23);
            this.AutomaticApplyButton.TabIndex = 22;
            this.AutomaticApplyButton.Text = "Start";
            this.AutomaticApplyButton.Click += new System.EventHandler(this.AutomaticApplyButton_Click);
            // 
            // DiscountFullCusomizationGroupBox
            // 
            this.DiscountFullCusomizationGroupBox.Controls.Add(this.NewEntitiesGheckBox);
            this.DiscountFullCusomizationGroupBox.Controls.Add(this.SystemEntitiesCustomizationCheckBox);
            this.DiscountFullCusomizationGroupBox.Controls.Add(this.NewEntitiesGroupBox);
            this.DiscountFullCusomizationGroupBox.Location = new System.Drawing.Point(16, 16);
            this.DiscountFullCusomizationGroupBox.Name = "DiscountFullCusomizationGroupBox";
            this.DiscountFullCusomizationGroupBox.Size = new System.Drawing.Size(544, 200);
            this.DiscountFullCusomizationGroupBox.TabIndex = 21;
            this.DiscountFullCusomizationGroupBox.TabStop = false;
            this.DiscountFullCusomizationGroupBox.Text = "Discount Package for MS CRM Customization Setup";
            // 
            // NewEntitiesGheckBox
            // 
            this.NewEntitiesGheckBox.Checked = true;
            this.NewEntitiesGheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.NewEntitiesGheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NewEntitiesGheckBox.Location = new System.Drawing.Point(16, 24);
            this.NewEntitiesGheckBox.Name = "NewEntitiesGheckBox";
            this.NewEntitiesGheckBox.Size = new System.Drawing.Size(168, 24);
            this.NewEntitiesGheckBox.TabIndex = 27;
            this.NewEntitiesGheckBox.Text = "New Custom Entities";
            this.NewEntitiesGheckBox.CheckedChanged += new System.EventHandler(this.NewEntitiesGheckBox_CheckedChanged);
            // 
            // SystemEntitiesCustomizationCheckBox
            // 
            this.SystemEntitiesCustomizationCheckBox.Checked = true;
            this.SystemEntitiesCustomizationCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SystemEntitiesCustomizationCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SystemEntitiesCustomizationCheckBox.Location = new System.Drawing.Point(16, 168);
            this.SystemEntitiesCustomizationCheckBox.Name = "SystemEntitiesCustomizationCheckBox";
            this.SystemEntitiesCustomizationCheckBox.Size = new System.Drawing.Size(440, 24);
            this.SystemEntitiesCustomizationCheckBox.TabIndex = 7;
            this.SystemEntitiesCustomizationCheckBox.Text = "Customization of System Entities (Account, Product, Quote, Quote Product)";
            // 
            // NewEntitiesGroupBox
            // 
            this.NewEntitiesGroupBox.Controls.Add(this.ImportNewEntitiesCheckBox);
            this.NewEntitiesGroupBox.Controls.Add(this.CorrectISVCheckBox);
            this.NewEntitiesGroupBox.Controls.Add(this.CorrectSiteMapCheckBox);
            this.NewEntitiesGroupBox.Location = new System.Drawing.Point(32, 56);
            this.NewEntitiesGroupBox.Name = "NewEntitiesGroupBox";
            this.NewEntitiesGroupBox.Size = new System.Drawing.Size(488, 104);
            this.NewEntitiesGroupBox.TabIndex = 28;
            this.NewEntitiesGroupBox.TabStop = false;
            this.NewEntitiesGroupBox.Text = "Customization of new custom entities";
            // 
            // ImportNewEntitiesCheckBox
            // 
            this.ImportNewEntitiesCheckBox.Checked = true;
            this.ImportNewEntitiesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ImportNewEntitiesCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ImportNewEntitiesCheckBox.Location = new System.Drawing.Point(16, 24);
            this.ImportNewEntitiesCheckBox.Name = "ImportNewEntitiesCheckBox";
            this.ImportNewEntitiesCheckBox.Size = new System.Drawing.Size(240, 24);
            this.ImportNewEntitiesCheckBox.TabIndex = 27;
            this.ImportNewEntitiesCheckBox.Text = "Import (and publish) new  custom Entities";
            // 
            // CorrectISVCheckBox
            // 
            this.CorrectISVCheckBox.Checked = true;
            this.CorrectISVCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CorrectISVCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CorrectISVCheckBox.Location = new System.Drawing.Point(16, 48);
            this.CorrectISVCheckBox.Name = "CorrectISVCheckBox";
            this.CorrectISVCheckBox.Size = new System.Drawing.Size(408, 24);
            this.CorrectISVCheckBox.TabIndex = 28;
            this.CorrectISVCheckBox.Text = "Correct ISV.config (add discounts buttons on the Quote and Quote Product)";
            // 
            // CorrectSiteMapCheckBox
            // 
            this.CorrectSiteMapCheckBox.Checked = true;
            this.CorrectSiteMapCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CorrectSiteMapCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CorrectSiteMapCheckBox.Location = new System.Drawing.Point(16, 72);
            this.CorrectSiteMapCheckBox.Name = "CorrectSiteMapCheckBox";
            this.CorrectSiteMapCheckBox.Size = new System.Drawing.Size(384, 24);
            this.CorrectSiteMapCheckBox.TabIndex = 29;
            this.CorrectSiteMapCheckBox.Text = "SiteMap (set display areas for new entities: Settings, Sales, Workplace)";
            // 
            // ManualTabPage
            // 
            this.ManualTabPage.Controls.Add(this.AdditionalGroupBox);
            this.ManualTabPage.Location = new System.Drawing.Point(4, 22);
            this.ManualTabPage.Name = "ManualTabPage";
            this.ManualTabPage.Size = new System.Drawing.Size(696, 222);
            this.ManualTabPage.TabIndex = 1;
            this.ManualTabPage.Text = "Additional Tasks ";
            // 
            // AdditionalGroupBox
            // 
            this.AdditionalGroupBox.Controls.Add(this.EditRolesRadioButton);
            this.AdditionalGroupBox.Controls.Add(this.EditRoledButton);
            this.AdditionalGroupBox.Controls.Add(this.ViewCustomizationsButton);
            this.AdditionalGroupBox.Controls.Add(this.InitialPathCustLabel);
            this.AdditionalGroupBox.Controls.Add(this.ViewCustomizationFilesRadioButton);
            this.AdditionalGroupBox.Controls.Add(this.GetCodesPanel);
            this.AdditionalGroupBox.Controls.Add(this.GetCodesRadioButton);
            this.AdditionalGroupBox.Controls.Add(this.CorrectExistingCustFilesRadioButton);
            this.AdditionalGroupBox.Controls.Add(this.AdditionalButton);
            this.AdditionalGroupBox.Location = new System.Drawing.Point(8, 16);
            this.AdditionalGroupBox.Name = "AdditionalGroupBox";
            this.AdditionalGroupBox.Size = new System.Drawing.Size(664, 192);
            this.AdditionalGroupBox.TabIndex = 22;
            this.AdditionalGroupBox.TabStop = false;
            this.AdditionalGroupBox.Text = "Choose additional actions for manual customization of MS CRM 3.0 Discount Package" +
                "";
            // 
            // EditRolesRadioButton
            // 
            this.EditRolesRadioButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EditRolesRadioButton.Location = new System.Drawing.Point(16, 144);
            this.EditRolesRadioButton.Name = "EditRolesRadioButton";
            this.EditRolesRadioButton.Size = new System.Drawing.Size(264, 24);
            this.EditRolesRadioButton.TabIndex = 28;
            this.EditRolesRadioButton.Text = "Edit roles to be discounts availabled";
            this.EditRolesRadioButton.CheckedChanged += new System.EventHandler(this.EditRolesRadioButton_CheckedChanged);
            // 
            // EditRoledButton
            // 
            this.EditRoledButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EditRoledButton.Enabled = false;
            this.EditRoledButton.Location = new System.Drawing.Point(392, 144);
            this.EditRoledButton.Name = "EditRoledButton";
            this.EditRoledButton.Size = new System.Drawing.Size(56, 23);
            this.EditRoledButton.TabIndex = 29;
            this.EditRoledButton.Text = "Edit";
            this.EditRoledButton.Click += new System.EventHandler(this.EditRoledButton_Click);
            // 
            // ViewCustomizationsButton
            // 
            this.ViewCustomizationsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ViewCustomizationsButton.Location = new System.Drawing.Point(600, 24);
            this.ViewCustomizationsButton.Name = "ViewCustomizationsButton";
            this.ViewCustomizationsButton.Size = new System.Drawing.Size(48, 23);
            this.ViewCustomizationsButton.TabIndex = 27;
            this.ViewCustomizationsButton.Text = "View";
            this.ViewCustomizationsButton.Click += new System.EventHandler(this.ViewCustomizationsButton_Click);
            // 
            // InitialPathCustLabel
            // 
            this.InitialPathCustLabel.Location = new System.Drawing.Point(192, 16);
            this.InitialPathCustLabel.Name = "InitialPathCustLabel";
            this.InitialPathCustLabel.Size = new System.Drawing.Size(400, 40);
            this.InitialPathCustLabel.TabIndex = 26;
            this.InitialPathCustLabel.Text = "Initial path";
            // 
            // ViewCustomizationFilesRadioButton
            // 
            this.ViewCustomizationFilesRadioButton.Checked = true;
            this.ViewCustomizationFilesRadioButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ViewCustomizationFilesRadioButton.Location = new System.Drawing.Point(16, 24);
            this.ViewCustomizationFilesRadioButton.Name = "ViewCustomizationFilesRadioButton";
            this.ViewCustomizationFilesRadioButton.Size = new System.Drawing.Size(152, 24);
            this.ViewCustomizationFilesRadioButton.TabIndex = 24;
            this.ViewCustomizationFilesRadioButton.TabStop = true;
            this.ViewCustomizationFilesRadioButton.Text = "View Customization Files";
            this.ViewCustomizationFilesRadioButton.CheckedChanged += new System.EventHandler(this.ViewCustomizationFilesRadioButton_CheckedChanged);
            // 
            // GetCodesPanel
            // 
            this.GetCodesPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GetCodesPanel.Controls.Add(this.GetEntityCodeButton);
            this.GetCodesPanel.Controls.Add(this.label1);
            this.GetCodesPanel.Controls.Add(this.EntityCodeTextBox);
            this.GetCodesPanel.Controls.Add(this.EntitiesComboBox);
            this.GetCodesPanel.Enabled = false;
            this.GetCodesPanel.Location = new System.Drawing.Point(192, 56);
            this.GetCodesPanel.Name = "GetCodesPanel";
            this.GetCodesPanel.Size = new System.Drawing.Size(464, 40);
            this.GetCodesPanel.TabIndex = 14;
            // 
            // GetEntityCodeButton
            // 
            this.GetEntityCodeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GetEntityCodeButton.Location = new System.Drawing.Point(200, 8);
            this.GetEntityCodeButton.Name = "GetEntityCodeButton";
            this.GetEntityCodeButton.Size = new System.Drawing.Size(53, 23);
            this.GetEntityCodeButton.TabIndex = 24;
            this.GetEntityCodeButton.Text = "Get";
            this.GetEntityCodeButton.Click += new System.EventHandler(this.GetEntityCodeButton_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(280, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 23);
            this.label1.TabIndex = 15;
            this.label1.Text = "ObjectTypeCode:";
            // 
            // EntityCodeTextBox
            // 
            this.EntityCodeTextBox.Enabled = false;
            this.EntityCodeTextBox.Location = new System.Drawing.Point(376, 8);
            this.EntityCodeTextBox.Name = "EntityCodeTextBox";
            this.EntityCodeTextBox.Size = new System.Drawing.Size(80, 20);
            this.EntityCodeTextBox.TabIndex = 14;
            // 
            // EntitiesComboBox
            // 
            this.EntitiesComboBox.Items.AddRange(new object[] {
            "new_salesheaderdiscount",
            "new_saleslinesdiscount"});
            this.EntitiesComboBox.Location = new System.Drawing.Point(8, 8);
            this.EntitiesComboBox.Name = "EntitiesComboBox";
            this.EntitiesComboBox.Size = new System.Drawing.Size(176, 21);
            this.EntitiesComboBox.TabIndex = 12;
            // 
            // GetCodesRadioButton
            // 
            this.GetCodesRadioButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GetCodesRadioButton.Location = new System.Drawing.Point(16, 64);
            this.GetCodesRadioButton.Name = "GetCodesRadioButton";
            this.GetCodesRadioButton.Size = new System.Drawing.Size(184, 24);
            this.GetCodesRadioButton.TabIndex = 10;
            this.GetCodesRadioButton.Text = "Get ObjectTypeCode for Entity";
            this.GetCodesRadioButton.CheckedChanged += new System.EventHandler(this.GetCodesRadioButton_CheckedChanged);
            // 
            // CorrectExistingCustFilesRadioButton
            // 
            this.CorrectExistingCustFilesRadioButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CorrectExistingCustFilesRadioButton.Location = new System.Drawing.Point(16, 104);
            this.CorrectExistingCustFilesRadioButton.Name = "CorrectExistingCustFilesRadioButton";
            this.CorrectExistingCustFilesRadioButton.Size = new System.Drawing.Size(352, 24);
            this.CorrectExistingCustFilesRadioButton.TabIndex = 9;
            this.CorrectExistingCustFilesRadioButton.Text = "Correct Existing Customization Files (for manual customization)";
            this.CorrectExistingCustFilesRadioButton.CheckedChanged += new System.EventHandler(this.CorrectExistingCustFilesRadioButton_CheckedChanged);
            // 
            // AdditionalButton
            // 
            this.AdditionalButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AdditionalButton.Enabled = false;
            this.AdditionalButton.Location = new System.Drawing.Point(392, 104);
            this.AdditionalButton.Name = "AdditionalButton";
            this.AdditionalButton.Size = new System.Drawing.Size(56, 23);
            this.AdditionalButton.TabIndex = 23;
            this.AdditionalButton.Text = "Start";
            this.AdditionalButton.Click += new System.EventHandler(this.AdditionalButton_Click);
            // 
            // SetupTabControl
            // 
            this.SetupTabControl.Controls.Add(this.AutoTabPage);
            this.SetupTabControl.Controls.Add(this.ManualTabPage);
            this.SetupTabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.SetupTabControl.ItemSize = new System.Drawing.Size(42, 18);
            this.SetupTabControl.Location = new System.Drawing.Point(0, 0);
            this.SetupTabControl.Name = "SetupTabControl";
            this.SetupTabControl.SelectedIndex = 0;
            this.SetupTabControl.Size = new System.Drawing.Size(704, 248);
            this.SetupTabControl.TabIndex = 22;
            // 
            // customizatinProgressBar
            // 
            this.customizatinProgressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.customizatinProgressBar.Location = new System.Drawing.Point(0, 248);
            this.customizatinProgressBar.Name = "customizatinProgressBar";
            this.customizatinProgressBar.Size = new System.Drawing.Size(704, 32);
            this.customizatinProgressBar.TabIndex = 24;
            // 
            // StateListBox
            // 
            this.StateListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StateListBox.Location = new System.Drawing.Point(0, 280);
            this.StateListBox.Name = "StateListBox";
            this.StateListBox.Size = new System.Drawing.Size(704, 157);
            this.StateListBox.TabIndex = 26;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(0, 280);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(704, 3);
            this.splitter2.TabIndex = 27;
            this.splitter2.TabStop = false;
            // 
            // DiscountPackageSetupForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(704, 437);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.StateListBox);
            this.Controls.Add(this.customizatinProgressBar);
            this.Controls.Add(this.SetupTabControl);
            this.Menu = this.mainMenu1;
            this.MinimumSize = new System.Drawing.Size(712, 464);
            this.Name = "DiscountPackageSetupForm";
            this.Text = "DISCOUNT PACKAGE for MS CRM Installation";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.DiscountPackageSetupForm_Closing);
            this.Load += new System.EventHandler(this.SetupForm_Load);
            this.AutoTabPage.ResumeLayout(false);
            this.DiscountFullCusomizationGroupBox.ResumeLayout(false);
            this.NewEntitiesGroupBox.ResumeLayout(false);
            this.ManualTabPage.ResumeLayout(false);
            this.AdditionalGroupBox.ResumeLayout(false);
            this.GetCodesPanel.ResumeLayout(false);
            this.GetCodesPanel.PerformLayout();
            this.SetupTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion


        #endregion

        #region Variables

        private bool automode = false;
        private string dataPath = Path.GetFullPath("Customizations");

        private Thread currThread;

        private string parameterXml = @"<importexportxml>
                                        <entities>
                                            <entity>account</entity>
                                            <entity>product</entity>
                                            <entity>quote</entity>
                                            <entity>quotedetail</entity>
                                        </entities>
                                        <nodes/>
                                    </importexportxml>";

        private string parameterISVXml = @"<importexportxml>
											<entities>
												<entity>quote</entity>
												<entity>quotedetail</entity>
											</entities>
	                                        <nodes>
												<node>
													isvconfig
												</node>
											</nodes>
                                    </importexportxml>";

        private string parameterSiteMapXml = @"<importexportxml>
											<entities>
												<entity>new_saleslinesdiscount</entity>
												<entity>new_salesheaderdiscount</entity>
												<entity>new_productdiscountgroup</entity>
												<entity>new_customerdiscountgroup</entity>
											</entities>
	                                        <nodes>
												<node>
													sitemap
												</node>
											</nodes>
                                    </importexportxml>";

        #endregion

        #region Methods

        #region Discount Package Customization

        private void AddToLog(string mess)
        {
            Invoke(new Action(() =>
                                    {
                                        StateListBox.Items.Add(mess);
                                        StateListBox.SelectedIndex = StateListBox.Items.Count - 1;
                                    }));
        }

        private void ShowFileContent(string filepath)
        {
            try
            {
                var wp = new System.Diagnostics.Process();
                wp.StartInfo.FileName = "wordpad";
                wp.StartInfo.Arguments = filepath;
                wp.StartInfo.UseShellExecute = false;
                wp.Start();
                //System.Diagnostics.Process wp = System.Diagnostics.Process.Start("wordpad", filepath);					
            }
            catch (Exception ex)
            {
                try
                {
                    var np = new System.Diagnostics.Process();
                    np.StartInfo.FileName = "notepad";//notepad
                    np.StartInfo.Arguments = filepath;
                    np.StartInfo.UseShellExecute = false;
                    //np.StartInfo.RedirectStandardOutput = true;
                    //np.StartInfo.RedirectStandardInput = true;
                    //np.StartInfo.RedirectStandardError = true;
                    np.Start();

                }
                catch (Exception ex2)
                {
                    AddToLog("ERROR :" + ex2.Message);
                }
            }

        }

        //__1.1 
        private void ImportAllEntities()
        {
            //____Begin			
            AddToLog("*****");
            AddToLog("Import all customization.......");
            //currThread.Name = "Import all customization";
            //____ImportAllEntities
            string all_entities_file = dataPath + "\\all_customizations.xml";

            try
            {
                string result = ImportExportCustomizer.ApplyCustomizationFile(all_entities_file);
                AddToLog(result);
                //___End
                AddToLog("Import customization have been done !");
                InitForStopCustomization();
            }
            catch (Exception ex)
            {
                if (currThread.ThreadState != ThreadState.AbortRequested)
                    StopCustomization("ERROR " + ex.Message + ":" + " Import all customization ");
            }
        }

        private void ShowRolesWindow()
        {
            (new EditRolesForm()).Show();
        }

        private void ImportNewEntities()
        {
            //___Begin		
            AddToLog("*****");
            AddToLog("Import new entities.......");
            // currThread.Name = "Import new entities";
            try
            {

                //___ImportNewEntities
                string new_entities_file = dataPath + "\\new_entities_customizations.xml";
                AddToLog("File : " + new_entities_file);
                string result = ImportExportCustomizer.ApplyCustomizationFile(new_entities_file);
                AddToLog(result);
                //__End				
                AddToLog(" Import new entities is finished !");
                if ((!SystemEntitiesCustomizationCheckBox.Checked)
                    && (!CorrectISVCheckBox.Checked) && (!CorrectSiteMapCheckBox.Checked))
                    InitForStopCustomization();
            }
            catch (Exception ex)
            {
                if (currThread.ThreadState != ThreadState.AbortRequested)
                    StopCustomization("ERROR " + ex.Message + ":" + " Import new entities");
            }
        }

        //1.2
        private void CorrectSiteMap()
        {
            //SiteMap Customization			
            AddToLog("*****");
            AddToLog("SiteMap customization (set areas to display new entities)...");
            //currThread.Name = "SiteMap customization";
            try
            {
                string xmlCustomization = ImportExportCustomizer.ExportNeccessaryCustomization(parameterSiteMapXml);
                //LanaSoftCRM.ImportExportCustomizer.ExportCurrentCustomization();			

                var xmlCustDoc = new XmlDocument();
                xmlCustDoc.LoadXml(xmlCustomization);

                var sm = new LanaSoftCRM.SiteMapCustomizer(xmlCustDoc);

                var SHDArea = new SubArea();
                SHDArea.Entity = "new_salesheaderdiscount";
                SHDArea.Id = "new_salesheaderdiscount";
                SHDArea.Title = "Sales Header Discount";

                var SLDArea = new SubArea();
                SLDArea.Entity = "new_saleslinesdiscount";
                SLDArea.Id = "new_saleslinesdiscount";
                SLDArea.Title = "Sales Lines Discount";

                var CDGArea = new SubArea();
                CDGArea.Entity = "new_customerdiscountgroup";
                CDGArea.Id = "new_customerdiscountgroup";
                CDGArea.Title = "Customer Discount Group";

                var PDGArea = new SubArea();
                PDGArea.Entity = "new_productdiscountgroup";
                PDGArea.Id = "new_productdiscountgroup";
                PDGArea.Title = "Product Discount Group";

                //Sales_Area
                sm.AddSubArea("SFA", "Extensions", PDGArea);
                sm.AddSubArea("SFA", "Extensions", CDGArea);

                sm.AddSubArea("Workplace", "Extensions", SHDArea);
                sm.AddSubArea("Workplace", "Extensions", SLDArea);
                sm.AddSubArea("Workplace", "Extensions", PDGArea);
                sm.AddSubArea("Workplace", "Extensions", CDGArea);

                sm.AddSubArea("Settings", "Extensions", SHDArea);
                sm.AddSubArea("Settings", "Extensions", SLDArea);
                sm.AddSubArea("Settings", "Extensions", PDGArea);
                sm.AddSubArea("Settings", "Extensions", CDGArea);

                string result = LanaSoftCRM.ImportExportCustomizer.ApplyCustomizationString(sm.Xml.OuterXml);
                AddToLog(result);
                AddToLog(" SiteMap customization is finished!");

                if (!SystemEntitiesCustomizationCheckBox.Checked)
                    //&&(!CorrectExistingCustFilesCheckBox.Checked))
                    InitForStopCustomization();
            }
            catch (Exception ex)
            {
                if (currThread.ThreadState != ThreadState.AbortRequested)
                    StopCustomization("ERROR " + ex.Message + ":" + " SiteMap customization ");
            }
        }

        //1.3
        private void CorrectISVConfig()
        {
            //ISV config Customization		
            AddToLog("*****");
            AddToLog(" ISVConfig customization (add Discount buttons to 'Quote' and 'Quote Product').....");
            //currThread.Name = "ISVConfig customization";

            try
            {
                string xmlCustomization = LanaSoftCRM.ImportExportCustomizer.ExportNeccessaryCustomization(parameterISVXml);
                var xmlCustDoc = new XmlDocument();
                xmlCustDoc.LoadXml(xmlCustomization);

                var isv = new LanaSoftCRM.IsvConfigCustomizer(xmlCustDoc);

                var SHDButt = new LanaSoftCRM.ToolBarButton();
                //SHDButt.Id =  "btnDiscountHeader";
                SHDButt.AvailableOffline = LanaSoftCRM.ConfigurationBoolean.True;
                SHDButt.SupportedClients = LanaSoftCRM.ClientTypes.All;
                SHDButt.JavaScript = "document.body.ShowLookUp();";
                SHDButt.Icon = "/_imgs/ico_18_1055.gif";
                SHDButt.Title = "Header Discount...";
                SHDButt.AccessHotKey = 'H';
                SHDButt.ValideForCreate = ConfigurationBoolean.True;
                SHDButt.ValidForUpdate = ConfigurationBoolean.True;

                var SLDButt = new LanaSoftCRM.ToolBarButton();
                //SLDButt.Id =  "btnDiscountLines";
                SLDButt.AvailableOffline = ConfigurationBoolean.True;
                SLDButt.SupportedClients = ClientTypes.All;
                SLDButt.JavaScript = "document.body.ShowLookUp();";
                SLDButt.Icon = "/_imgs/ico_18_1055.gif";
                SLDButt.AccessHotKey = 'L';
                SLDButt.Title = "Lines Discount...";
                SLDButt.ValideForCreate = ConfigurationBoolean.True;
                SLDButt.ValidForUpdate = ConfigurationBoolean.True;

                isv.AddEntityToolBarButton("quote", SHDButt, false, true);
                isv.AddEntityToolBarButton("quotedetail", SLDButt, false, true);

                string result = ImportExportCustomizer.ApplyCustomizationString(isv.Xml.OuterXml);
                AddToLog(result);

                //2
                AddToLog(" ISVConfig customization is finished!");

                if (!SystemEntitiesCustomizationCheckBox.Checked)
                    //&&(!CorrectExistingCustFilesCheckBox.Checked))
                    InitForStopCustomization();
            }
            catch (Exception ex)
            {
                if (currThread.ThreadState != ThreadState.AbortRequested)
                    StopCustomization("ERROR " + ex.Message + ":" + " ISVConfig customization ");
            }
        }

        //__2
        private void SystemEntitiesCustomization()
        {
            int lang = 1033;

            //___Begin	
            AddToLog("*****");
            AddToLog(" Customization system entities........");
            //currThread.Name = " Customization system entities";
            try
            {
                //SystemEntitiesCustomization						
                string customizationXML = LanaSoftCRM.ImportExportCustomizer.ExportNeccessaryCustomization(parameterXml);

                var doc = new XmlDocument();
                doc.LoadXml(customizationXML);

                var formCustomizer = new FormCustomizer(doc, "");

                #region 1. Account

                XmlNode accountNode = doc.SelectSingleNode("ImportExportXml/Entities/Entity[Name=\"Account\"]");

                AddToLog("Account customization start.......");

                //1.1 New Form controls: Add Account Discount Group 

                XmlNode generalTabNode = accountNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab[sections/section/rows/row/cell/control[@id=\"name\"]]");
                XmlNode generalTabFirstSectionNode = generalTabNode.SelectSingleNode("sections/section[rows/row/cell/control[@id=\"name\"]]");
                XmlNode customerGroupCellTab = accountNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab[sections/section/rows/row/cell/control[@id=\"new_customerdiscountgroupid\"]]");

                Guid tabId = new Guid(generalTabNode.Attributes.GetNamedItem("id").Value);
                Guid sectionId = new Guid(generalTabFirstSectionNode.Attributes.GetNamedItem("id").Value);

                if (customerGroupCellTab == null)// There no new_customerdiscountgroupid on the Form
                {
                    formCustomizer.AddCellToNewRow(
                        "account",
                        tabId, sectionId,
                        "Customer Discount Group Name",
                        "new_customerdiscountgroupid",
                        LanaSoftCRM.FieldDataType.Lookup,
                        "new_customerdiscountgroupid",
                        false, true, false, 2, 1, lang);
                }
                else
                {
                    string customerGroupTabName = customerGroupCellTab.SelectSingleNode("labels/label").Attributes.GetNamedItem("description").Value;
                    AddToLog("Form consists CustomerDiscountGroup: See Tab \"" + customerGroupTabName + "\"");
                }

                //1.2 Add Form Events
                XmlNode accountEvents = accountNode.SelectSingleNode("FormXml/forms/entity/form/events");
                XmlNode accountOnLoadNode = accountNode.SelectSingleNode("FormXml/forms/entity/form/events/event[@name='onload']/script");

                int accountOnLoadCodeIndex = -1;
                if (accountOnLoadNode != null)
                    accountOnLoadCodeIndex = accountOnLoadNode.InnerText.IndexOf("___Discount Package 3.0___");

                if (accountOnLoadCodeIndex == -1)
                {
                    string account_onload_file = dataPath + "\\account\\onLoad.txt";
                    Stream account_onload_stream = new FileStream(account_onload_file, FileMode.Open);

                    StreamReader account_onload_SR = new StreamReader(account_onload_stream);
                    string account_onload_stript = account_onload_SR.ReadToEnd();
                    account_onload_SR.Close();

                    formCustomizer.AddFormEventJScript("account", account_onload_stript,
                        LanaSoftCRM.JScriptType.onload,
                        LanaSoftCRM.InsertionMode.InsertAfter,
                        new string[] { /*"new_customerdiscountgroupid"*/ });
                }

                //Account end
                AddToLog("Account customized!");

                #endregion

                #region 2. Product

                XmlNode productNode = doc.SelectSingleNode("ImportExportXml/Entities/Entity[Name=\"Product\"]");
                AddToLog("Product customization start.......");

                //2.1 New Form controls: Add Product Discount Group
                XmlNode productGenTabNode = productNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab[sections/section/rows/row/cell/control[@id=\"name\"]]");
                XmlNode productGenTabFirstSectionNode = productGenTabNode.SelectSingleNode("sections/section[rows/row/cell/control[@id=\"name\"]]");
                XmlNode productGroupCellTab = productNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab[sections/section/rows/row/cell/control[@id=\"new_productdiscountgroupid\"]]");

                Guid productTabId = new Guid(productGenTabNode.Attributes.GetNamedItem("id").Value);//CreateNavigator().GetAttribute("id", ""));
                Guid productSectionId = new Guid(productGenTabFirstSectionNode.Attributes.GetNamedItem("id").Value);

                if (productGroupCellTab == null)
                {
                    formCustomizer.AddCellToNewRow(
                        "product",
                        productTabId, productSectionId,
                        "Product Discount Group Name",
                        "new_productdiscountgroupid",
                        LanaSoftCRM.FieldDataType.Lookup,
                        "new_productdiscountgroupid",
                        false, true, false, 2, 1, lang);
                }
                else
                {
                    string productGroupTabName = productGroupCellTab.SelectSingleNode("labels/label").Attributes.GetNamedItem("description").Value;
                    AddToLog("Form consists ProductDiscountGroup: See Tab \"" + productGroupTabName + "\"");
                }

                //2.2 Add Form Events
                XmlNode productOnLoadNode = productNode.SelectSingleNode("FormXml/forms/entity/form/events/event[@name='onload']/script");

                int productOnLoadCodeIndex = -1;
                if (productOnLoadNode != null)
                    productOnLoadCodeIndex = productOnLoadNode.InnerText.IndexOf("___Discount Package 3.0___");

                if (productOnLoadCodeIndex == -1)
                {
                    string product_onload_file = dataPath + "\\product\\onLoad.txt";
                    Stream product_onload_stream = new FileStream(product_onload_file, FileMode.Open);
                    StreamReader product_onload_SR = new StreamReader(product_onload_stream);
                    string product_onload_stript = product_onload_SR.ReadToEnd();
                    product_onload_SR.Close();

                    formCustomizer.AddFormEventJScript("product", product_onload_stript,
                        LanaSoftCRM.JScriptType.onload,
                        LanaSoftCRM.InsertionMode.InsertAfter,
                        new string[] { "new_productdiscountgroupid" });
                }

                AddToLog("Product customized !");
                #endregion

                #region 3. Quote

                XmlNode quoteNode = doc.SelectSingleNode("ImportExportXml/Entities/Entity[Name=\"Quote\"]");

                AddToLog("Quote customization start.......");

                //3.1 Add Form Events

                XmlNode quoteOnLoadNode = quoteNode.SelectSingleNode("FormXml/forms/entity/form/events/event[@name='onload']/script");
                int quoteOnLoadCodeIndex = -1;
                if (quoteOnLoadNode != null)
                    quoteOnLoadCodeIndex = quoteOnLoadNode.InnerText.IndexOf("___Discount Package 3.0___");

                if (quoteOnLoadCodeIndex == -1)
                {
                    string quote_onload_file = dataPath + "\\quote\\onLoad.txt";
                    Stream quote_onload_stream = new FileStream(quote_onload_file, FileMode.Open);
                    StreamReader quote_onload_SR = new StreamReader(quote_onload_stream);
                    string quote_onload_stript = quote_onload_SR.ReadToEnd();
                    quote_onload_SR.Close();

                    formCustomizer.AddFormEventJScript("quote", quote_onload_stript,
                        LanaSoftCRM.JScriptType.onload,
                        LanaSoftCRM.InsertionMode.InsertAfter,
                        new string[] { });//???

                }
                AddToLog("Quote customized !");

                #endregion

                #region 4. Quote Product

                AddToLog("Quote Product customization start.......");

                XmlNode quotedetailNode = doc.SelectSingleNode("ImportExportXml/Entities/Entity[Name=\"QuoteDetail\"]");
                XmlNode quotedetailGenTabNode = quotedetailNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab[sections/section/rows/row/cell/control[@id=\"manualdiscountamount\"]]");
                productGenTabNode = quotedetailNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab[sections/section/rows/row/cell/control[@id=\"productid\"]]");

                if (quotedetailGenTabNode == null)
                    quotedetailGenTabNode = quotedetailNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab[sections/section/rows/row/cell/control[@id=\"baseamount\"]]");
                if (quotedetailGenTabNode == null)
                    quotedetailGenTabNode = quotedetailNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab");
                if (quotedetailGenTabNode == null)
                    quotedetailGenTabNode = productGenTabNode;


                //4.1 Add attributes for entity							
                LanaSoftCRM.ImportExportCustomizer impexpCust = new LanaSoftCRM.ImportExportCustomizer(doc);
                string discPersLabel = "Available Discount(%)";
                impexpCust.AddFloatElement("quotedetail", "new_manualdiscountpercents", discPersLabel, "Available Discount(%)", 0, 100);
                impexpCust.AddNTextElement("quotedetail", "new_customergroup", "Account Discount Group", "Account Discount Group");
                impexpCust.AddNTextElement("quotedetail", "new_parentaccount", "Parent Account", "Parent Account");
                impexpCust.AddNTextElement("quotedetail", "new_productgroup", "Product Discount Group", "Product Discount Group");
                doc = impexpCust.Xml;

                //customizationXML = doc.OuterXml;
                // result = LanaSoftCRM.ImportExportCustomizer.ApplyCustomizationString(customizationXML);
                AddToLog(" Attributes new_manualdiscountpercents,new_parentaccount,new_customergroup, new_productgroup added !");//+ result);


                //4.2 Form controls:	

                //4.2.1 OnChange Product field
                string product_onchange_file = dataPath + "\\quotedetail\\OnChange\\Product.txt";
                StreamReader product_onchange_stream_SR = new StreamReader(product_onchange_file);
                string product_onchange_stript = product_onchange_stream_SR.ReadToEnd();
                product_onchange_stream_SR.Close();

                Guid productGenTabId = new Guid(productGenTabNode.Attributes.GetNamedItem("id").Value);

                XmlNode productSectionNode = null;
                if (productGenTabNode != null)
                    productSectionNode = productGenTabNode.SelectSingleNode("sections/section[rows/row/cell/control[@id=\"productid\"]]");

                productSectionId = new Guid(productSectionNode.Attributes.GetNamedItem("id").Value);

                XmlNode quotedetailProductOnChangeNode = productGenTabNode.SelectSingleNode("sections/section/rows/row/cell[control[@id=\"productid\"]]/events/event[@name='onchange']/script");

                int quotedetailProductOnChangeCodeIndex = -1;
                if (quotedetailProductOnChangeNode != null)
                    quotedetailProductOnChangeCodeIndex = quotedetailProductOnChangeNode.InnerText.IndexOf("___Discount Package 3.0___");

                if (quotedetailProductOnChangeCodeIndex == -1)
                {
                    formCustomizer.AddOnChangeJScript("quotedetail",
                        productGenTabId, productSectionId,
                        "productid",
                        product_onchange_stript,
                        LanaSoftCRM.InsertionMode.InsertAfter,
                        new string[] { });
                }

                //4.2.2 New Discount Percent(%) field
                XmlNode discountRowNode = null;
                XmlNode discountSectionNode = null;

                if (quotedetailGenTabNode != null)
                {
                    discountRowNode = quotedetailGenTabNode.SelectSingleNode("sections/section/rows/row[cell/control[@id=\"manualdiscountamount\"]]");
                    discountSectionNode = quotedetailGenTabNode.SelectSingleNode("sections/section[rows/row/cell/control[@id=\"manualdiscountamount\"]]");
                }

                try
                {
                    Guid quotedetailGenTabId = new Guid(quotedetailGenTabNode.Attributes.GetNamedItem("id").Value);//CreateNavigator().GetAttribute("id", ""));																	
                    // Discount percent cell		
                    XmlNode discountperCellSection = quotedetailNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab/sections/section[rows/row/cell/control[@id=\"new_manualdiscountpercents\"]]");

                    if ((discountperCellSection == null))
                    {
                        if ((discountRowNode == null) && (quotedetailGenTabNode != null))
                        {
                            //TEST
                            StreamWriter sr = new StreamWriter("ERRORS.txt"); //???
                            sr.WriteLine(quotedetailGenTabNode.InnerXml);
                            sr.Close();
                            //TEST

                            discountSectionNode = formCustomizer.AddSection("quotedetail", quotedetailGenTabId, "Discounts", false, false).Element;

                            Guid discountSectId = new Guid(discountSectionNode.Attributes.GetNamedItem("id").Value);

                            discountRowNode = formCustomizer.AddCellToNewRow(
                                "quotedetail",
                                quotedetailGenTabId, discountSectId,
                                "Manual Discount",
                                "manualdiscountamount",
                                LanaSoftCRM.FieldDataType.Money,
                                "manualdiscountamount",
                                false, true, false, 1, 1, lang).Element;

                            formCustomizer.AddCellToNewRow(
                                "quotedetail",
                                quotedetailGenTabId, discountSectId,
                                discPersLabel,
                                "new_manualdiscountpercents",
                                LanaSoftCRM.FieldDataType.Float,
                                "new_manualdiscountpercents",
                                false, true, false, 1, 1, lang);
                        }
                        else
                            formCustomizer.AddCellToExistingRow(
                                "quotedetail", discPersLabel,
                                "new_manualdiscountpercents",
                                LanaSoftCRM.FieldDataType.Float,
                                "new_manualdiscountpercents",
                                false, true, false, 1, 1, discountRowNode, lang);
                    }
                    else
                    {
                        string discountperName = discountperCellSection.SelectSingleNode("labels/label").Attributes.GetNamedItem("description").Value;
                        AddToLog("Form consists new_manualdiscountpercents field: See Section \"" + discountperName + "\"");
                    }
                    //Attach OnChange event
                    string mandiscper_onchange_file = dataPath + "\\quotedetail\\OnChange\\manualdiscountpercents.txt";
                    StreamReader mandiscper_onchange_stream_SR = new StreamReader(mandiscper_onchange_file);
                    string mandiscper_onchange_stript = mandiscper_onchange_stream_SR.ReadToEnd();
                    mandiscper_onchange_stream_SR.Close();

                    Guid discountSectionId = new Guid(discountSectionNode.Attributes.GetNamedItem("id").Value);

                    XmlNode quotedetailPercOnChangeNode = productGenTabNode.SelectSingleNode("sections/section/rows/row/cell[control[@id=\"new_manualdiscountpercents\"]]/events/event[@name='onchange']/script");
                    int quotedetailPercOnChangeCodeIndex = -1;
                    if (quotedetailPercOnChangeNode != null)
                        quotedetailPercOnChangeCodeIndex = quotedetailPercOnChangeNode.InnerText.IndexOf("___Discount Package 3.0___");

                    if (quotedetailPercOnChangeCodeIndex == -1)
                    {
                        formCustomizer.AddOnChangeJScript("quotedetail",
                            quotedetailGenTabId, discountSectionId,
                            "new_manualdiscountpercents",
                            mandiscper_onchange_stript,
                            LanaSoftCRM.InsertionMode.Overwrite,
                            new string[] { "new_manualdiscountpercents" });
                    }

                }
                catch (Exception ex)
                {
                    AddToLog("ERROR " + ex.Message);
                }


                //4.2.2 Section with additional contols
                //new_productgroup,  new_customergroup
                //new_parentaccount     
                //XmlNode addFieldsTab = quotedetailNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab/sections/section[rows/row/cell/control[@id=\"new_customergroup\"]]");			

                //New tab and section
                Guid newTabId;
                XmlNode addFieldsTab = quotedetailNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab[labels/label[@description=\"Additional Fields\"]]");

                Guid newSectionId;
                XmlNode addFieldsSection = quotedetailNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab/sections/section[labels/label[@description=\"Additional Fields\"]]");
                if (addFieldsSection == null)
                {
                    //Create Tab
                    if (addFieldsTab == null)
                    {
                        LanaSoftCRM.FormNavigator newTabNav = formCustomizer.AddTab("quotedetail", "Additional Fields");
                        newTabId = new Guid(newTabNav.Id);
                    }
                    else
                    {
                        newTabId = new Guid(addFieldsTab.Attributes.GetNamedItem("id").Value);
                    }
                    //Create Section
                    LanaSoftCRM.FormNavigator newSectNav = formCustomizer.AddSection(
                        "quotedetail", newTabId,
                        "Additional Fields",
                        false, false);
                    newSectionId = new Guid(newSectNav.Id);
                }
                else
                {
                    newSectionId = new Guid(addFieldsSection.Attributes.GetNamedItem("id").Value);
                    //Detect Tab "Additional Fields"
                    addFieldsTab = quotedetailNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab[sections/section/labels/label[@description=\"Additional Fields\"]]");
                    newTabId = new Guid(addFieldsTab.Attributes.GetNamedItem("id").Value);
                }
                //Quote fields
                XmlNode accountCellSection = quotedetailNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab/sections/section[rows/row/cell/control[@id=\"new_parentaccount\"]]");
                if (accountCellSection == null)
                {
                    formCustomizer.AddCellToNewRow("quotedetail",
                        newTabId, newSectionId,
                        "Parent Account",
                        "new_parentaccount",
                        LanaSoftCRM.FieldDataType.Text,
                        "new_parentaccount",
                        false, false, false, 2, 1, lang);

                }
                else
                {
                    string accountSectionName = accountCellSection.SelectSingleNode("labels/label").Attributes.GetNamedItem("description").Value;
                    AddToLog("Form consists new_parentaccount field: See Section \"" + accountSectionName + "\"");
                }

                XmlNode custGroupCellSection = quotedetailNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab/sections/section[rows/row/cell/control[@id=\"new_customergroup\"]]");
                if (custGroupCellSection == null)
                {
                    formCustomizer.AddCellToNewRow("quotedetail",
                        newTabId, newSectionId,
                        "Customer Discount Group",
                        "new_customergroup",
                        LanaSoftCRM.FieldDataType.Text,
                        "new_customergroup",
                        false, false, false, 2, 1, lang);
                }
                else
                {
                    string custGroupSectionName = custGroupCellSection.SelectSingleNode("labels/label").Attributes.GetNamedItem("description").Value;
                    AddToLog("Form consists new_customergroup field: See Section \"" + custGroupSectionName + "\"");
                }

                XmlNode prodGroupCellSection = quotedetailNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab/sections/section[rows/row/cell/control[@id=\"new_productgroup\"]]");
                if (prodGroupCellSection == null)
                {
                    formCustomizer.AddCellToNewRow("quotedetail",
                        newTabId, newSectionId,
                        "Product Discount Group",
                        "new_productgroup",
                        LanaSoftCRM.FieldDataType.Text,
                        "new_productgroup",
                        false, false, false, 2, 1, lang);
                }
                else
                {
                    string prodGroupSectionName = prodGroupCellSection.SelectSingleNode("labels/label").Attributes.GetNamedItem("description").Value;
                    AddToLog("Form consists new_productgroup field: See Section \"" + prodGroupSectionName + "\"");
                }

                //IFRAME_Account, IFRAME_Quote, IFRAME_Product 			
                XmlNode accountIFRAMESection = quotedetailNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab/sections/section[rows/row/cell/control[@id=\"IFRAME_Account\"]]");
                if (accountIFRAMESection == null)
                {
                    LanaSoftCRM.FormNavigator navig_IFRAME_Account = formCustomizer.AddCellToNewRow("quotedetail",
                        newTabId, newSectionId,
                        "Account IFRAME",
                        "IFRAME_Account",
                        LanaSoftCRM.FieldDataType.IFrame,
                        null,
                        false, false, true, 2, 1, lang);

                    XmlNode accountIFrameNode = navig_IFRAME_Account.Element.SelectSingleNode("control[@id='" + navig_IFRAME_Account.Id + "']");
                    accountIFrameNode.InnerXml =
                        "<parameters>"
                        + "<Url>http://</Url>"
                        + "<PassParameters>false</PassParameters>"
                        + "<Security>false</Security>"
                        + "<Scrolling>auto</Scrolling>"
                        + "<Border>true</Border>"
                        + "</parameters>";
                }

                XmlNode quoteIFRAMESection = quotedetailNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab/sections/section[rows/row/cell/control[@id=\"IFRAME_Quote\"]]");
                if (quoteIFRAMESection == null)
                {
                    LanaSoftCRM.FormNavigator navig_IFRAME_Quote = formCustomizer.AddCellToNewRow("quotedetail",
                        newTabId, newSectionId,
                        "Quote IFRAME",
                        "IFRAME_Quote",
                        LanaSoftCRM.FieldDataType.IFrame,
                        null,
                        false, false, true, 2, 1, lang);
                    XmlNode quoteIFrameNode = navig_IFRAME_Quote.Element.SelectSingleNode("control[@id='" + navig_IFRAME_Quote.Id + "']");
                    quoteIFrameNode.InnerXml =
                        "<parameters>"
                        + "<Url>http://</Url>"
                        + "<PassParameters>false</PassParameters>"
                        + "<Security>false</Security>"
                        + "<Scrolling>auto</Scrolling>"
                        + "<Border>true</Border>"
                        + "</parameters>";

                }

                XmlNode productIFRAMESection = quotedetailNode.SelectSingleNode("FormXml/forms/entity/form/tabs/tab/sections/section[rows/row/cell/control[@id=\"IFRAME_Product\"]]");
                if (productIFRAMESection == null)
                {
                    LanaSoftCRM.FormNavigator navig_IFRAME_Product = formCustomizer.AddCellToNewRow("quotedetail",
                        newTabId, newSectionId,
                        "Product IFRAME",
                        "IFRAME_Product",
                        LanaSoftCRM.FieldDataType.IFrame,
                        null,
                        false, false, true, 2, 1, lang);
                    XmlNode productIFrameNode = navig_IFRAME_Product.Element.SelectSingleNode("control[@id='" + navig_IFRAME_Product.Id + "']");
                    productIFrameNode.InnerXml =
                        "<parameters>"
                        + "<Url>http://</Url>"
                        + "<PassParameters>false</PassParameters>"
                        + "<Security>false</Security>"
                        + "<Scrolling>auto</Scrolling>"
                        + "<Border>true</Border>"
                        + "</parameters>";
                }

                //4.3 Add Form Events
                XmlNode quotedetailOnLoadNode = quotedetailNode.SelectSingleNode("FormXml/forms/entity/form/events/event[@name='onload']/script");
                int quotedetailOnLoadCodeIndex = -1;
                if (quotedetailOnLoadNode != null)
                    quotedetailOnLoadCodeIndex = quotedetailOnLoadNode.InnerText.IndexOf("___Discount Package 3.0___");

                if (quotedetailOnLoadCodeIndex == -1)
                {
                    string quotedetail_onload_file = dataPath + "\\quotedetail\\onLoad.txt";
                    Stream quotedetail_onload_stream = new FileStream(quotedetail_onload_file, FileMode.Open);
                    StreamReader quotedetail_onload_SR = new StreamReader(quotedetail_onload_stream);
                    string quotedetail_onload_stript = quotedetail_onload_SR.ReadToEnd();
                    quotedetail_onload_SR.Close();

                    formCustomizer.AddFormEventJScript("quotedetail", quotedetail_onload_stript,
                        LanaSoftCRM.JScriptType.onload,
                        LanaSoftCRM.InsertionMode.InsertAfter,
                        new string[] { });
                }
                //__
                AddToLog("Quote Product customized !");

                #endregion

                //5. Save changes 							
                AddToLog("Import and publish changes at customization !");
                customizationXML = doc.OuterXml;
                string result = ImportExportCustomizer.ApplyCustomizationString(customizationXML);
                AddToLog(result);
                //___End		
                AddToLog(" System entities Customized !");
                //InitForStopCustomization();
            }
            catch (Exception ex)
            {
                if (currThread.ThreadState != ThreadState.AbortRequested)
                    StopCustomization("ERROR " + ex.Message + ":" + "  Customization system entities ");
            }
        }


        //__3
        private void CorrectEntitiesCodes()
        {
            AddToLog("*****");
            AddToLog(" Correcting entities codes(in CRM 3.0 system entities client-side(OnLoad) scripts)...");
            //currThread.Name = " Correcting entities codes";
            try
            {
                //1
                string custStr = ImportExportCustomizer.ExportNeccessaryCustomization(parameterXml);
                //LanaSoftCRM.ImportExportCustomizer.ExportCurrentCustomization();
                AddToLog("Export Current Customization finished !");

                //2
                int shdCode = CRMHelper.GetEntityCode("new_salesheaderdiscount");
                string pathern = "___SalesHeaderDiscountCode" + @"\s*[=]\s*(\d+)";
                custStr = Regex.Replace(custStr, pathern, "___SalesHeaderDiscountCode = " + shdCode);
                AddToLog("Replacing 'Sales Header Discount' entity code  finished !");
                //3
                int sldCode = CRMHelper.GetEntityCode("new_saleslinesdiscount");
                pathern = "___LinesDiscountCode" + @"\s*[=]\s*(\d+)";
                custStr = Regex.Replace(custStr, pathern, "___LinesDiscountCode = " + sldCode);
                AddToLog("Replacing 'Sales Lines Discount' entity code  finished !");
                //4
                ImportExportCustomizer.ApplyCustomizationString(custStr);

                //_____
                AddToLog(" Correcting is finished !");

                MessageBox.Show("Customization finished. But you must set access permissions to new_entities !");
                BeginInvoke(new Action(ShowRolesWindow));

                InitForStopCustomization();
            }
            catch (Exception ex)
            {
                if (currThread.ThreadState != ThreadState.AbortRequested)
                    StopCustomization("ERROR " + ex.Message + ":" + " Correcting entities codes ");
            }
        }

        private void CorrectEntitiesCodesInFiles()
        {
            //____Begin	
            AddToLog("*****");
            AddToLog(" Correcting new entities codes at customizations files...");
            //currThread.Name = "Correcting new entities codes at customizations files ";
            try
            {
                //1
                string quote_onload_file = dataPath + "\\quote\\onLoad.txt";

                var quoteSR = new StreamReader(quote_onload_file);
                string quote_onload_stript = quoteSR.ReadToEnd();
                quoteSR.Close();

                //2
                int shdCode = CRMHelper.GetEntityCode("new_salesheaderdiscount");
                string pathern = "___SalesHeaderDiscountCode" + @"\s*[=]\s*(\d+)";
                quote_onload_stript = Regex.Replace(quote_onload_stript, pathern, "___SalesHeaderDiscountCode = " + shdCode);

                var quoteSW = new StreamWriter(quote_onload_file);
                quoteSW.WriteLine(quote_onload_stript);
                quoteSW.Close();

                AddToLog("Replacing 'Sales Header Discount' entity code  finished !");

                //3
                string quotedetail_onload_file = dataPath + "\\quotedetail\\onLoad.txt";
                var quotedetailSR = new StreamReader(quotedetail_onload_file);
                string quotedetail_onload_stript = quotedetailSR.ReadToEnd();
                quotedetailSR.Close();

                int sldCode = CRMHelper.GetEntityCode("new_saleslinesdiscount");
                pathern = "___LinesDiscountCode" + @"\s*[=]\s*(\d+)";
                quotedetail_onload_stript = Regex.Replace(quotedetail_onload_stript, pathern, "___LinesDiscountCode = " + sldCode);

                var quotedetailSW = new StreamWriter(quotedetail_onload_file);
                quotedetailSW.WriteLine(quotedetail_onload_stript);
                quotedetailSW.Close();

                AddToLog("Replacing 'Sales Lines Discount' entity code  finished !");

                //_____End
                AddToLog(" Correcting is finished  !");

                if (!SystemEntitiesCustomizationCheckBox.Checked || !automode)
                    InitForStopCustomization();
            }
            catch (Exception ex)
            {
                if (currThread.ThreadState != ThreadState.AbortRequested)
                    StopCustomization("ERROR " + ex.Message + ":" + " Correcting new entities codes at customizations files");
            }
        }


        #endregion

        public void InitForStopCustomization()
        {
            ProgressStop();
            CustomizationMenuItem.Enabled = true;
            AutomaticApplyButton.Enabled = true;
            StopButton.Enabled = false;
        }

        private void InitForStartCustomization()
        {
            ProgressDraw();
            CustomizationMenuItem.Enabled = false;
            AutomaticApplyButton.Enabled = false;
            StopButton.Enabled = true;
        }

        private void StartFullCustomization()
        {
            StopCustomization();
            InitForStartCustomization();
            currThread = new Thread(ApplyCustomization_For_ExistingCRM);
            currThread.Name = "MS CRM 3.0 Discount Package customization ";
            currThread.Start();
        }

        private void ApplyCustomization_For_ExistingCRM()
        {
            //1__Import new entities
            if (NewEntitiesGheckBox.Checked)
            {
                //1.1
                if (ImportNewEntitiesCheckBox.Checked)
                    ImportNewEntities();
                //1.2
                if (CorrectISVCheckBox.Checked)
                    CorrectISVConfig();
                //1.3		
                if (CorrectSiteMapCheckBox.Checked)
                    CorrectSiteMap();
            }
            if (SystemEntitiesCustomizationCheckBox.Checked)
            {
                //__Correct codes of new entities for "Quote" and "Quote Product" onLoad events 
                //if(CorrectExistingCustFilesCheckBox.Checked)
                //CorrectEntitiesCodesInFiles();
                //3.1 Create relations between new custom entities and system entities, add OnLoad/OnChange events
                SystemEntitiesCustomization();
                //3.2 Replace customization codes for new entities, and apply customization again	
                CorrectEntitiesCodes();
            }
        }

        private void ProgressDraw()
        {
            processTimer.Start();
            //processTimer.Enabled = true;
        }

        private void ProgressStop()
        {
            //processTimer.Enabled = false;	
            processTimer.Stop();
            customizatinProgressBar.Value = customizatinProgressBar.Maximum;
        }

        private void StopCustomization()
        {
            if (currThread != null)
                StopCustomization(currThread.Name);
        }

        private void StopCustomization(string mess)
        {
            try
            {
                BeginInvoke(new Action(InitForStopCustomization));

                if (currThread != null)//&&(currThread.ThreadState == ThreadState.Running))
                {
                    AddToLog(mess + "is interrupted!!!");
                    currThread.Abort();
                    AddToLog("_____");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                AddToLog(ex.Message);
            }
        }

        #endregion

        #region Customization Form Events

        private void SetupForm_Load(object sender, System.EventArgs e)
        {
            ProgressStop();
            customizatinProgressBar.Value = 0;
            InitialPathCustLabel.Text = dataPath;
        }

        private void processTimer_Tick(object sender, System.EventArgs e)
        {
            //if(currThread.ThreadState == ThreadState.Running)
            {
                if (customizatinProgressBar.Value == customizatinProgressBar.Maximum)
                    customizatinProgressBar.Value = 0;
                else
                    customizatinProgressBar.Value++;
            }
            //else
            //InitForStopCustomization();
        }

        private void AutomaticApplyButton_Click(object sender, System.EventArgs e)
        {
            automode = true;
            //StateListBox.Items.Clear();									
            StartFullCustomization();
        }

        private void StopButton_Click(object sender, System.EventArgs e)
        {
            StopCustomization();
        }

        private void DiscountPackageSetupForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StopCustomization();
        }

        private void NewEntitiesOpen_Click(object sender, System.EventArgs e)
        {
            string file = dataPath + "\\new_entities_customizations.xml";
            ShowFileContent(file);
        }
        private void NewEntitiesGheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            NewEntitiesGroupBox.Enabled = ((CheckBox)sender).Checked;
        }

        private void AdditionalButton_Click(object sender, System.EventArgs e)
        {
            automode = false;
            if (CorrectExistingCustFilesRadioButton.Checked)
            {
                StopCustomization();
                InitForStartCustomization();
                currThread = new Thread(new ThreadStart(CorrectEntitiesCodesInFiles));
                currThread.Name = "Correct Entities Codes In Files ";
                currThread.Start();
            }
        }

        private void GetCodesRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            GetCodesPanel.Enabled = ((RadioButton)sender).Checked;
        }

        private void EditRoledButton_Click(object sender, System.EventArgs e)
        {
            BeginInvoke(new Action(ShowRolesWindow));
        }

        private void GetEntityCodeButton_Click(object sender, System.EventArgs e)
        {
            if (EntitiesComboBox.Text.Trim().Length == 0)
                MessageBox.Show("Enter entity name !");
            else
                EntityCodeTextBox.Text = CRMHelper.GetEntityCode(EntitiesComboBox.Text.Trim()).ToString();
        }

        private void CorrectExistingCustFilesRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            AdditionalButton.Enabled = ((RadioButton)sender).Checked;
        }

        private void EditRolesRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            EditRoledButton.Enabled = ((RadioButton)sender).Checked;
        }

        private void ViewCustomizationFilesRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            ViewCustomizationsButton.Enabled = ((RadioButton)sender).Checked;
            InitialPathCustLabel.Enabled = ((RadioButton)sender).Checked;
        }

        private void SaveCustLogFileMenuItem_Click(object sender, System.EventArgs e)
        {
            var sd = new SaveFileDialog();
            sd.Filter = "(*.txt)|*.txt";
            if (sd.ShowDialog() == DialogResult.OK)
            {
                using (var stream = new FileStream(sd.FileName, FileMode.OpenOrCreate))
                {
                    var sw = new StreamWriter(stream);
                    foreach (object str in StateListBox.Items)
                    {
                        sw.WriteLine(str.ToString());
                    }
                    sw.Close();
                }
            }
        }

        private void ViewCustomizationsButton_Click(object sender, System.EventArgs e)
        {
            var fd = new OpenFileDialog();

            fd.InitialDirectory = dataPath;
            //fd.Filter = "Text(*.txt)|(*.txt)|All(*.*)|(*.*)";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string filepath = fd.FileName;
                ShowFileContent(filepath);
            }
        }

        private void AutoCustMenuItem_Click(object sender, System.EventArgs e)
        {
            //Set all options for full customization
            NewEntitiesGheckBox.Checked = true;
            SystemEntitiesCustomizationCheckBox.Checked = true;
            ImportNewEntitiesCheckBox.Checked = true;
            CorrectISVCheckBox.Checked = true;
            CorrectSiteMapCheckBox.Checked = true;
            //
            automode = true;
            StartFullCustomization();
        }

        private void ExitMenuItem_Click(object sender, System.EventArgs e)
        {
            StopCustomization();
            this.Close();
        }

        private void ImportNewEntitiesMenuItem_Click(object sender, System.EventArgs e)
        {
            StopCustomization();
            InitForStartCustomization();
            //ThreadPool.QueueUserWorkItem(new WaitCallback(ImportNewEntities));
            currThread = new Thread(new ThreadStart(ImportNewEntities));
            currThread.Name = "Import new entities";
            currThread.Start();
        }

        private void CorrectCustomizationFiles_Click(object sender, System.EventArgs e)
        {
            if ((currThread != null) && (currThread.ThreadState == ThreadState.Running))
                StopCustomization();

            automode = true;
            ProgressDraw();
            currThread = new Thread(new ThreadStart(CorrectEntitiesCodesInFiles));
            currThread.Name = "Correct new Entities Codes at customizations files";
            currThread.Start();
        }

        private void AccountMenuItem_Click(object sender, System.EventArgs e)
        {
            string account_onload_file = dataPath + "\\account\\onLoad.txt";
            ShowFileContent(account_onload_file);
        }

        private void QuoteMenuItem_Click(object sender, System.EventArgs e)
        {
            string quote_onload_file = dataPath + "\\quote\\onLoad.txt";
            ShowFileContent(quote_onload_file);
        }

        private void ProductMenuItem_Click(object sender, System.EventArgs e)
        {
            string product_onload_file = dataPath + "\\product\\onLoad.txt";
            ShowFileContent(product_onload_file);
        }

        private void ISVMenuItem_Click(object sender, System.EventArgs e)
        {
            string quoteproduct_onload_file = dataPath + "\\ISVConfig.xml";
            ShowFileContent(quoteproduct_onload_file);
        }

        private void QuoteproductOnLoadMenuItem_Click(object sender, System.EventArgs e)
        {

            string quoteproduct_onLoad_file = dataPath + "\\quotedetail\\onload.txt";
            ShowFileContent(quoteproduct_onLoad_file);

        }

        private void onChangeProductMenuItem_Click(object sender, System.EventArgs e)
        {
            string quoteproduct_onChangeProduct_file = dataPath + "\\quotedetail\\onChange\\Product.txt";
            ShowFileContent(quoteproduct_onChangeProduct_file);
        }

        private void OnChangePercMenuItem_Click(object sender, System.EventArgs e)
        {
            string quoteproduct_onChangePerc_file = dataPath + "\\quotedetail\\onChange\\manualdiscountpercents.txt";
            ShowFileContent(quoteproduct_onChangePerc_file);
        }

        #endregion
    }
}
