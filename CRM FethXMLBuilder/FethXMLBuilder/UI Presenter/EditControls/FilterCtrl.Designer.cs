namespace FetchXMLBuilder.UI
{
    partial class FilterCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.filterTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.andTypeRadioButton = new System.Windows.Forms.RadioButton();
            this.orTypeRadioButton = new System.Windows.Forms.RadioButton();
            this.addConditionButton = new System.Windows.Forms.Button();
            this.deleteConditionButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.conditionsListBox = new System.Windows.Forms.ListBox();
            this.editConditionButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.subFiltersListBox = new System.Windows.Forms.ListBox();
            this.editFilterButton = new System.Windows.Forms.Button();
            this.addFilterButton = new System.Windows.Forms.Button();
            this.deleteFilterButton = new System.Windows.Forms.Button();
            this.filterTypeGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // filterTypeGroupBox
            // 
            this.filterTypeGroupBox.Controls.Add(this.andTypeRadioButton);
            this.filterTypeGroupBox.Controls.Add(this.orTypeRadioButton);
            this.filterTypeGroupBox.Location = new System.Drawing.Point(8, 54);
            this.filterTypeGroupBox.Name = "filterTypeGroupBox";
            this.filterTypeGroupBox.Size = new System.Drawing.Size(85, 74);
            this.filterTypeGroupBox.TabIndex = 6;
            this.filterTypeGroupBox.TabStop = false;
            this.filterTypeGroupBox.Text = "Type";
            // 
            // andTypeRadioButton
            // 
            this.andTypeRadioButton.AutoSize = true;
            this.andTypeRadioButton.Location = new System.Drawing.Point(19, 46);
            this.andTypeRadioButton.Name = "andTypeRadioButton";
            this.andTypeRadioButton.Size = new System.Drawing.Size(48, 17);
            this.andTypeRadioButton.TabIndex = 1;
            this.andTypeRadioButton.TabStop = true;
            this.andTypeRadioButton.Text = "AND";
            this.andTypeRadioButton.UseVisualStyleBackColor = true;
            // 
            // orTypeRadioButton
            // 
            this.orTypeRadioButton.AutoSize = true;
            this.orTypeRadioButton.Checked = true;
            this.orTypeRadioButton.Location = new System.Drawing.Point(19, 23);
            this.orTypeRadioButton.Name = "orTypeRadioButton";
            this.orTypeRadioButton.Size = new System.Drawing.Size(41, 17);
            this.orTypeRadioButton.TabIndex = 0;
            this.orTypeRadioButton.TabStop = true;
            this.orTypeRadioButton.Text = "OR";
            this.orTypeRadioButton.UseVisualStyleBackColor = true;
            // 
            // addConditionButton
            // 
            this.addConditionButton.Location = new System.Drawing.Point(244, 16);
            this.addConditionButton.Name = "addConditionButton";
            this.addConditionButton.Size = new System.Drawing.Size(75, 23);
            this.addConditionButton.TabIndex = 9;
            this.addConditionButton.Text = "Add";
            this.addConditionButton.UseVisualStyleBackColor = true;
            this.addConditionButton.Click += new System.EventHandler(this.addConditionButton_Click);
            // 
            // deleteConditionButton
            // 
            this.deleteConditionButton.Location = new System.Drawing.Point(244, 68);
            this.deleteConditionButton.Name = "deleteConditionButton";
            this.deleteConditionButton.Size = new System.Drawing.Size(75, 23);
            this.deleteConditionButton.TabIndex = 10;
            this.deleteConditionButton.Text = "Delete";
            this.deleteConditionButton.UseVisualStyleBackColor = true;
            this.deleteConditionButton.Click += new System.EventHandler(this.deleteConditionButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.conditionsListBox);
            this.groupBox1.Controls.Add(this.editConditionButton);
            this.groupBox1.Controls.Add(this.addConditionButton);
            this.groupBox1.Controls.Add(this.deleteConditionButton);
            this.groupBox1.Location = new System.Drawing.Point(105, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 97);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Condition(s)";
            // 
            // conditionsListBox
            // 
            this.conditionsListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.conditionsListBox.FormattingEnabled = true;
            this.conditionsListBox.Location = new System.Drawing.Point(3, 16);
            this.conditionsListBox.Name = "conditionsListBox";
            this.conditionsListBox.Size = new System.Drawing.Size(232, 69);
            this.conditionsListBox.TabIndex = 12;
            // 
            // editConditionButton
            // 
            this.editConditionButton.Location = new System.Drawing.Point(244, 43);
            this.editConditionButton.Name = "editConditionButton";
            this.editConditionButton.Size = new System.Drawing.Size(75, 23);
            this.editConditionButton.TabIndex = 11;
            this.editConditionButton.Text = "Edit";
            this.editConditionButton.UseVisualStyleBackColor = true;
            this.editConditionButton.Click += new System.EventHandler(this.editConditionButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.subFiltersListBox);
            this.groupBox2.Controls.Add(this.editFilterButton);
            this.groupBox2.Controls.Add(this.addFilterButton);
            this.groupBox2.Controls.Add(this.deleteFilterButton);
            this.groupBox2.Location = new System.Drawing.Point(105, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(333, 102);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter(s)";
            // 
            // subFiltersListBox
            // 
            this.subFiltersListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.subFiltersListBox.FormattingEnabled = true;
            this.subFiltersListBox.Location = new System.Drawing.Point(3, 16);
            this.subFiltersListBox.Name = "subFiltersListBox";
            this.subFiltersListBox.Size = new System.Drawing.Size(232, 82);
            this.subFiltersListBox.TabIndex = 13;
            // 
            // editFilterButton
            // 
            this.editFilterButton.Location = new System.Drawing.Point(244, 45);
            this.editFilterButton.Name = "editFilterButton";
            this.editFilterButton.Size = new System.Drawing.Size(75, 23);
            this.editFilterButton.TabIndex = 12;
            this.editFilterButton.Text = "Edit";
            this.editFilterButton.UseVisualStyleBackColor = true;
            this.editFilterButton.Click += new System.EventHandler(this.editFilterButton_Click);
            // 
            // addFilterButton
            // 
            this.addFilterButton.Location = new System.Drawing.Point(244, 16);
            this.addFilterButton.Name = "addFilterButton";
            this.addFilterButton.Size = new System.Drawing.Size(75, 23);
            this.addFilterButton.TabIndex = 9;
            this.addFilterButton.Text = "Add";
            this.addFilterButton.UseVisualStyleBackColor = true;
            this.addFilterButton.Click += new System.EventHandler(this.addFilterButton_Click);
            // 
            // deleteFilterButton
            // 
            this.deleteFilterButton.Location = new System.Drawing.Point(244, 74);
            this.deleteFilterButton.Name = "deleteFilterButton";
            this.deleteFilterButton.Size = new System.Drawing.Size(75, 23);
            this.deleteFilterButton.TabIndex = 10;
            this.deleteFilterButton.Text = "Delete";
            this.deleteFilterButton.UseVisualStyleBackColor = true;
            this.deleteFilterButton.Click += new System.EventHandler(this.deleteFilterButton_Click);
            // 
            // FilterCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.filterTypeGroupBox);
            this.Name = "FilterCtrl";
            this.Size = new System.Drawing.Size(450, 326);
            this.Title = "Filter";
            this.Controls.SetChildIndex(this.filterTypeGroupBox, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.filterTypeGroupBox.ResumeLayout(false);
            this.filterTypeGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox filterTypeGroupBox;
        private System.Windows.Forms.RadioButton andTypeRadioButton;
        private System.Windows.Forms.RadioButton orTypeRadioButton;
        private System.Windows.Forms.Button addConditionButton;
        private System.Windows.Forms.Button deleteConditionButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button editConditionButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button editFilterButton;
        private System.Windows.Forms.Button addFilterButton;
        private System.Windows.Forms.Button deleteFilterButton;
        private System.Windows.Forms.ListBox conditionsListBox;
        private System.Windows.Forms.ListBox subFiltersListBox;
    }
}
