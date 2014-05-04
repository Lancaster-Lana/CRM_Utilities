namespace FetchXMLBuilder.UI
{
    partial class AttributesCtrl
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
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.availableAttributesListBox = new System.Windows.Forms.ListBox();
            this.visibleAttributesListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(171, 65);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(51, 23);
            this.addButton.TabIndex = 0;
            this.addButton.Text = ">>";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(171, 94);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(51, 23);
            this.removeButton.TabIndex = 1;
            this.removeButton.Text = "<<";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // availableAttributesListBox
            // 
            this.availableAttributesListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.availableAttributesListBox.FormattingEnabled = true;
            this.availableAttributesListBox.Location = new System.Drawing.Point(0, 38);
            this.availableAttributesListBox.Name = "availableAttributesListBox";
            this.availableAttributesListBox.Size = new System.Drawing.Size(165, 212);
            this.availableAttributesListBox.TabIndex = 2;
            // 
            // visibleAttributesListBox
            // 
            this.visibleAttributesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.visibleAttributesListBox.FormattingEnabled = true;
            this.visibleAttributesListBox.Location = new System.Drawing.Point(232, 38);
            this.visibleAttributesListBox.Name = "visibleAttributesListBox";
            this.visibleAttributesListBox.Size = new System.Drawing.Size(169, 212);
            this.visibleAttributesListBox.TabIndex = 3;
            // 
            // AttributesCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.availableAttributesListBox);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.visibleAttributesListBox);
            this.Controls.Add(this.addButton);
            this.Name = "AttributesCtrl";
            this.Size = new System.Drawing.Size(404, 297);
            this.Title = "Attributes";
            this.Controls.SetChildIndex(this.addButton, 0);
            this.Controls.SetChildIndex(this.visibleAttributesListBox, 0);
            this.Controls.SetChildIndex(this.removeButton, 0);
            this.Controls.SetChildIndex(this.availableAttributesListBox, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.ListBox availableAttributesListBox;
        private System.Windows.Forms.ListBox visibleAttributesListBox;
    }
}
