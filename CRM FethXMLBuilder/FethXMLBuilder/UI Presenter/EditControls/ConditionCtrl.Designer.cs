namespace FetchXMLBuilder.UI
{
    partial class ConditionCtrl
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
            this.valuesTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.attributeComboBox = new System.Windows.Forms.ComboBox();
            this.operatorComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // valuesTextBox
            // 
            this.valuesTextBox.Location = new System.Drawing.Point(96, 118);
            this.valuesTextBox.Multiline = true;
            this.valuesTextBox.Name = "valuesTextBox";
            this.valuesTextBox.Size = new System.Drawing.Size(351, 86);
            this.valuesTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Attribute";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Operator";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Value(s)";
            // 
            // attributeComboBox
            // 
            this.attributeComboBox.FormattingEnabled = true;
            this.attributeComboBox.Location = new System.Drawing.Point(96, 48);
            this.attributeComboBox.Name = "attributeComboBox";
            this.attributeComboBox.Size = new System.Drawing.Size(121, 21);
            this.attributeComboBox.TabIndex = 6;
            // 
            // operatorComboBox
            // 
            this.operatorComboBox.FormattingEnabled = true;
            this.operatorComboBox.Items.AddRange(new object[] {
            "eq",
            "ne",
            "lt",
            "gt",
            "le",
            "ge",
            "like",
            "not-like",
            "on",
            "on-or-before",
            "on-or-after",
            "last-x-hours",
            "next-x-hours",
            "last-x-days",
            "next-x-days",
            "last-x-weeks",
            "next-x-weeks",
            "last-x-months",
            "next-x-months",
            "last-x-years",
            "next-x-years"});
            this.operatorComboBox.Location = new System.Drawing.Point(96, 80);
            this.operatorComboBox.Name = "operatorComboBox";
            this.operatorComboBox.Size = new System.Drawing.Size(121, 21);
            this.operatorComboBox.TabIndex = 7;
            // 
            // ConditionCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.operatorComboBox);
            this.Controls.Add(this.attributeComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.valuesTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "ConditionCtrl";
            this.Size = new System.Drawing.Size(454, 258);
            this.Title = "Condition";
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.valuesTextBox, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.attributeComboBox, 0);
            this.Controls.SetChildIndex(this.operatorComboBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox valuesTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox attributeComboBox;
        private System.Windows.Forms.ComboBox operatorComboBox;
    }
}
