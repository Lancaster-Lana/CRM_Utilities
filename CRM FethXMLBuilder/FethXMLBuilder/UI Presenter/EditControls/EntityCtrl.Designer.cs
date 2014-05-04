namespace FetchXMLBuilder.UI
{
    partial class EntityCtrl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.linkedEntitiesListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ordersListBox = new System.Windows.Forms.ListBox();
            this.typeGroupBox = new System.Windows.Forms.GroupBox();
            this.dascRadioButton = new System.Windows.Forms.RadioButton();
            this.ascRadioButton = new System.Windows.Forms.RadioButton();
            this.attributesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.subFiltersListBox = new System.Windows.Forms.ListBox();
            this.editFilterButton = new System.Windows.Forms.Button();
            this.addFilterButton = new System.Windows.Forms.Button();
            this.deleteFilterButton = new System.Windows.Forms.Button();
            this.entitiesComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.typeGroupBox.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.linkedEntitiesListBox);
            this.groupBox3.Location = new System.Drawing.Point(188, 256);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(473, 98);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Linked Entities";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(385, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Edit";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(385, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Add";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(385, 39);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 14;
            this.button3.Text = "Delete";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // linkedEntitiesListBox
            // 
            this.linkedEntitiesListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.linkedEntitiesListBox.FormattingEnabled = true;
            this.linkedEntitiesListBox.Location = new System.Drawing.Point(3, 16);
            this.linkedEntitiesListBox.Name = "linkedEntitiesListBox";
            this.linkedEntitiesListBox.Size = new System.Drawing.Size(373, 79);
            this.linkedEntitiesListBox.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ordersListBox);
            this.groupBox1.Controls.Add(this.typeGroupBox);
            this.groupBox1.Controls.Add(this.attributesCheckedListBox);
            this.groupBox1.Location = new System.Drawing.Point(185, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(473, 92);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Visible Attributes and Order";
            // 
            // ordersListBox
            // 
            this.ordersListBox.FormattingEnabled = true;
            this.ordersListBox.Location = new System.Drawing.Point(300, 17);
            this.ordersListBox.Name = "ordersListBox";
            this.ordersListBox.Size = new System.Drawing.Size(160, 69);
            this.ordersListBox.TabIndex = 14;
            // 
            // typeGroupBox
            // 
            this.typeGroupBox.Controls.Add(this.dascRadioButton);
            this.typeGroupBox.Controls.Add(this.ascRadioButton);
            this.typeGroupBox.Location = new System.Drawing.Point(196, 19);
            this.typeGroupBox.Name = "typeGroupBox";
            this.typeGroupBox.Size = new System.Drawing.Size(65, 61);
            this.typeGroupBox.TabIndex = 4;
            this.typeGroupBox.TabStop = false;
            this.typeGroupBox.Text = "Order";
            // 
            // dascRadioButton
            // 
            this.dascRadioButton.AutoSize = true;
            this.dascRadioButton.Location = new System.Drawing.Point(6, 38);
            this.dascRadioButton.Name = "dascRadioButton";
            this.dascRadioButton.Size = new System.Drawing.Size(54, 17);
            this.dascRadioButton.TabIndex = 3;
            this.dascRadioButton.TabStop = true;
            this.dascRadioButton.Text = "DASC";
            this.dascRadioButton.UseVisualStyleBackColor = true;
            // 
            // ascRadioButton
            // 
            this.ascRadioButton.AutoSize = true;
            this.ascRadioButton.Location = new System.Drawing.Point(6, 19);
            this.ascRadioButton.Name = "ascRadioButton";
            this.ascRadioButton.Size = new System.Drawing.Size(46, 17);
            this.ascRadioButton.TabIndex = 2;
            this.ascRadioButton.TabStop = true;
            this.ascRadioButton.Text = "ASC";
            this.ascRadioButton.UseVisualStyleBackColor = true;
            // 
            // attributesCheckedListBox
            // 
            this.attributesCheckedListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.attributesCheckedListBox.FormattingEnabled = true;
            this.attributesCheckedListBox.Location = new System.Drawing.Point(3, 16);
            this.attributesCheckedListBox.Name = "attributesCheckedListBox";
            this.attributesCheckedListBox.Size = new System.Drawing.Size(177, 64);
            this.attributesCheckedListBox.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.subFiltersListBox);
            this.groupBox4.Controls.Add(this.editFilterButton);
            this.groupBox4.Controls.Add(this.addFilterButton);
            this.groupBox4.Controls.Add(this.deleteFilterButton);
            this.groupBox4.Location = new System.Drawing.Point(185, 142);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(473, 108);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Filter(s)";
            // 
            // subFiltersListBox
            // 
            this.subFiltersListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.subFiltersListBox.FormattingEnabled = true;
            this.subFiltersListBox.Location = new System.Drawing.Point(3, 16);
            this.subFiltersListBox.Name = "subFiltersListBox";
            this.subFiltersListBox.Size = new System.Drawing.Size(376, 82);
            this.subFiltersListBox.TabIndex = 13;
            // 
            // editFilterButton
            // 
            this.editFilterButton.Location = new System.Drawing.Point(385, 75);
            this.editFilterButton.Name = "editFilterButton";
            this.editFilterButton.Size = new System.Drawing.Size(75, 23);
            this.editFilterButton.TabIndex = 12;
            this.editFilterButton.Text = "Edit";
            this.editFilterButton.UseVisualStyleBackColor = true;
            this.editFilterButton.Click += new System.EventHandler(this.editFilterButton_Click);
            // 
            // addFilterButton
            // 
            this.addFilterButton.Location = new System.Drawing.Point(385, 16);
            this.addFilterButton.Name = "addFilterButton";
            this.addFilterButton.Size = new System.Drawing.Size(75, 23);
            this.addFilterButton.TabIndex = 9;
            this.addFilterButton.Text = "Add";
            this.addFilterButton.UseVisualStyleBackColor = true;
            // 
            // deleteFilterButton
            // 
            this.deleteFilterButton.Location = new System.Drawing.Point(385, 46);
            this.deleteFilterButton.Name = "deleteFilterButton";
            this.deleteFilterButton.Size = new System.Drawing.Size(75, 23);
            this.deleteFilterButton.TabIndex = 10;
            this.deleteFilterButton.Text = "Delete";
            this.deleteFilterButton.UseVisualStyleBackColor = true;
            // 
            // entitiesComboBox
            // 
            this.entitiesComboBox.FormattingEnabled = true;
            this.entitiesComboBox.Location = new System.Drawing.Point(8, 60);
            this.entitiesComboBox.Name = "entitiesComboBox";
            this.entitiesComboBox.Size = new System.Drawing.Size(157, 21);
            this.entitiesComboBox.TabIndex = 23;
            // 
            // EntityCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.entitiesComboBox);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "EntityCtrl";
            this.Size = new System.Drawing.Size(661, 416);
            this.Title = "Entity";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.Controls.SetChildIndex(this.entitiesComboBox, 0);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.typeGroupBox.ResumeLayout(false);
            this.typeGroupBox.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox attributesCheckedListBox;
        private System.Windows.Forms.CheckedListBox linkedEntitiesListBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox subFiltersListBox;
        private System.Windows.Forms.Button editFilterButton;
        private System.Windows.Forms.Button addFilterButton;
        private System.Windows.Forms.Button deleteFilterButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox entitiesComboBox;
        private System.Windows.Forms.GroupBox typeGroupBox;
        private System.Windows.Forms.RadioButton dascRadioButton;
        private System.Windows.Forms.RadioButton ascRadioButton;
        private System.Windows.Forms.ListBox ordersListBox;
    }
}
