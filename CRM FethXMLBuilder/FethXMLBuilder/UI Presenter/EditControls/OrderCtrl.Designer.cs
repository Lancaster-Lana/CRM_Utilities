namespace FetchXMLBuilder.UI
{
    partial class OrderCtrl
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
            this.ascRadioButton = new System.Windows.Forms.RadioButton();
            this.typeGroupBox = new System.Windows.Forms.GroupBox();
            this.dascRadioButton = new System.Windows.Forms.RadioButton();
            this.attributeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.typeGroupBox.SuspendLayout();
            this.SuspendLayout();
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
            // typeGroupBox
            // 
            this.typeGroupBox.Controls.Add(this.dascRadioButton);
            this.typeGroupBox.Controls.Add(this.ascRadioButton);
            this.typeGroupBox.Location = new System.Drawing.Point(8, 90);
            this.typeGroupBox.Name = "typeGroupBox";
            this.typeGroupBox.Size = new System.Drawing.Size(199, 48);
            this.typeGroupBox.TabIndex = 3;
            this.typeGroupBox.TabStop = false;
            this.typeGroupBox.Text = "Type";
            // 
            // dascRadioButton
            // 
            this.dascRadioButton.AutoSize = true;
            this.dascRadioButton.Location = new System.Drawing.Point(75, 19);
            this.dascRadioButton.Name = "dascRadioButton";
            this.dascRadioButton.Size = new System.Drawing.Size(54, 17);
            this.dascRadioButton.TabIndex = 3;
            this.dascRadioButton.TabStop = true;
            this.dascRadioButton.Text = "DASC";
            this.dascRadioButton.UseVisualStyleBackColor = true;
            // 
            // attributeComboBox
            // 
            this.attributeComboBox.FormattingEnabled = true;
            this.attributeComboBox.Location = new System.Drawing.Point(57, 53);
            this.attributeComboBox.Name = "attributeComboBox";
            this.attributeComboBox.Size = new System.Drawing.Size(150, 21);
            this.attributeComboBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Attribute";
            // 
            // OrderCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.attributeComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.typeGroupBox);
            this.Name = "OrderCtrl";
            this.Size = new System.Drawing.Size(301, 196);
            this.Title = "Order";
            this.Controls.SetChildIndex(this.typeGroupBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.attributeComboBox, 0);
            this.typeGroupBox.ResumeLayout(false);
            this.typeGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton ascRadioButton;
        private System.Windows.Forms.GroupBox typeGroupBox;
        private System.Windows.Forms.RadioButton dascRadioButton;
        private System.Windows.Forms.ComboBox attributeComboBox;
        private System.Windows.Forms.Label label1;
    }
}
