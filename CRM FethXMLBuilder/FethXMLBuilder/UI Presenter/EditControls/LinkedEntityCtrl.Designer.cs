namespace FetchXMLBuilder.UI
{
    partial class LinkedEntityCtrl
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
            this.entityComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fromAttributeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toAttributeComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.linkTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.attributesGroupBox = new System.Windows.Forms.GroupBox();
            this.editLinkEntityInfoButton = new System.Windows.Forms.Button();
            this.attributesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // entityComboBox
            // 
            this.entityComboBox.FormattingEnabled = true;
            this.entityComboBox.Items.AddRange(new object[] {
            "eq ",
            "ne",
            "null",
            "not-null",
            "in",
            "between",
            "not-between",
            "like",
            "not-like",
            "yesterday",
            "today",
            "tomorrow",
            "next-seven-days",
            "last-seven-days",
            "next-week",
            "last-week",
            "last-month",
            "this-month",
            "next-month",
            "on",
            "on-or-before",
            "on-or-after",
            "last-year",
            "this-year",
            "next-year",
            "eq-userid",
            "ne-userid",
            "eq-businessid",
            "ne-businessid",
            "last-x-hours",
            "next-x-hours",
            "last-x-days",
            "next-x-days",
            "last-x-weeks",
            "next-x-weeks",
            "last-x-monthes",
            "next-x-monthes",
            "last-x-years",
            "next-x-years"});
            this.entityComboBox.Location = new System.Drawing.Point(90, 55);
            this.entityComboBox.Name = "entityComboBox";
            this.entityComboBox.Size = new System.Drawing.Size(166, 21);
            this.entityComboBox.TabIndex = 7;
            this.entityComboBox.SelectedIndexChanged += new System.EventHandler(this.operatorComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Linked entity";
            // 
            // fromAttributeComboBox
            // 
            this.fromAttributeComboBox.FormattingEnabled = true;
            this.fromAttributeComboBox.Items.AddRange(new object[] {
            "eq ",
            "ne",
            "null",
            "not-null",
            "in",
            "between",
            "not-between",
            "like",
            "not-like",
            "yesterday",
            "today",
            "tomorrow",
            "next-seven-days",
            "last-seven-days",
            "next-week",
            "last-week",
            "last-month",
            "this-month",
            "next-month",
            "on",
            "on-or-before",
            "on-or-after",
            "last-year",
            "this-year",
            "next-year",
            "eq-userid",
            "ne-userid",
            "eq-businessid",
            "ne-businessid",
            "last-x-hours",
            "next-x-hours",
            "last-x-days",
            "next-x-days",
            "last-x-weeks",
            "next-x-weeks",
            "last-x-monthes",
            "next-x-monthes",
            "last-x-years",
            "next-x-years"});
            this.fromAttributeComboBox.Location = new System.Drawing.Point(81, 23);
            this.fromAttributeComboBox.Name = "fromAttributeComboBox";
            this.fromAttributeComboBox.Size = new System.Drawing.Size(166, 21);
            this.fromAttributeComboBox.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "From";
            // 
            // toAttributeComboBox
            // 
            this.toAttributeComboBox.FormattingEnabled = true;
            this.toAttributeComboBox.Items.AddRange(new object[] {
            "eq ",
            "ne",
            "null",
            "not-null",
            "in",
            "between",
            "not-between",
            "like",
            "not-like",
            "yesterday",
            "today",
            "tomorrow",
            "next-seven-days",
            "last-seven-days",
            "next-week",
            "last-week",
            "last-month",
            "this-month",
            "next-month",
            "on",
            "on-or-before",
            "on-or-after",
            "last-year",
            "this-year",
            "next-year",
            "eq-userid",
            "ne-userid",
            "eq-businessid",
            "ne-businessid",
            "last-x-hours",
            "next-x-hours",
            "last-x-days",
            "next-x-days",
            "last-x-weeks",
            "next-x-weeks",
            "last-x-monthes",
            "next-x-monthes",
            "last-x-years",
            "next-x-years"});
            this.toAttributeComboBox.Location = new System.Drawing.Point(81, 57);
            this.toAttributeComboBox.Name = "toAttributeComboBox";
            this.toAttributeComboBox.Size = new System.Drawing.Size(166, 21);
            this.toAttributeComboBox.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "To";
            // 
            // linkTypeComboBox
            // 
            this.linkTypeComboBox.FormattingEnabled = true;
            this.linkTypeComboBox.Items.AddRange(new object[] {
            "natural",
            "inner",
            "outer"});
            this.linkTypeComboBox.Location = new System.Drawing.Point(90, 91);
            this.linkTypeComboBox.Name = "linkTypeComboBox";
            this.linkTypeComboBox.Size = new System.Drawing.Size(105, 21);
            this.linkTypeComboBox.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Link Type";
            // 
            // attributesGroupBox
            // 
            this.attributesGroupBox.Controls.Add(this.label1);
            this.attributesGroupBox.Controls.Add(this.fromAttributeComboBox);
            this.attributesGroupBox.Controls.Add(this.label3);
            this.attributesGroupBox.Controls.Add(this.toAttributeComboBox);
            this.attributesGroupBox.Location = new System.Drawing.Point(9, 131);
            this.attributesGroupBox.Name = "attributesGroupBox";
            this.attributesGroupBox.Size = new System.Drawing.Size(295, 94);
            this.attributesGroupBox.TabIndex = 14;
            this.attributesGroupBox.TabStop = false;
            this.attributesGroupBox.Text = "Linked  Attributes";
            // 
            // editLinkEntityInfoButton
            // 
            this.editLinkEntityInfoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editLinkEntityInfoButton.Location = new System.Drawing.Point(262, 53);
            this.editLinkEntityInfoButton.Name = "editLinkEntityInfoButton";
            this.editLinkEntityInfoButton.Size = new System.Drawing.Size(42, 23);
            this.editLinkEntityInfoButton.TabIndex = 15;
            this.editLinkEntityInfoButton.Text = "...";
            this.editLinkEntityInfoButton.UseVisualStyleBackColor = true;
            this.editLinkEntityInfoButton.Click += new System.EventHandler(this.editLinkEntityInfoButton_Click);
            // 
            // LinkedEntityCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editLinkEntityInfoButton);
            this.Controls.Add(this.entityComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.linkTypeComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.attributesGroupBox);
            this.Name = "LinkedEntityCtrl";
            this.Size = new System.Drawing.Size(316, 280);
            this.Title = "Linked Entity";
            this.Controls.SetChildIndex(this.attributesGroupBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.linkTypeComboBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.entityComboBox, 0);
            this.Controls.SetChildIndex(this.editLinkEntityInfoButton, 0);
            this.attributesGroupBox.ResumeLayout(false);
            this.attributesGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox entityComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox fromAttributeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox toAttributeComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox linkTypeComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox attributesGroupBox;
        protected internal System.Windows.Forms.Button editLinkEntityInfoButton;
    }
}
