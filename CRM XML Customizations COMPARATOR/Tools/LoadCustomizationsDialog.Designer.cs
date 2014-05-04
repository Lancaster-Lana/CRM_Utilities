namespace CustomizationsComparator
{
    partial class LoadCustomizationsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
       //private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.firstCustLoadButton = new System.Windows.Forms.Button();
            this.secondCustLoadButton = new System.Windows.Forms.Button();
            this.firstCustTextBox = new System.Windows.Forms.TextBox();
            this.secondCustTextBox = new System.Windows.Forms.TextBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.lblFirstCustomization = new System.Windows.Forms.Label();
            this.lblSecondCustomization = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // firstCustLoadButton
            // 
            this.firstCustLoadButton.Location = new System.Drawing.Point(448, 12);
            this.firstCustLoadButton.Name = "firstCustLoadButton";
            this.firstCustLoadButton.Size = new System.Drawing.Size(39, 23);
            this.firstCustLoadButton.TabIndex = 0;
            this.firstCustLoadButton.Text = "...";
            this.firstCustLoadButton.UseVisualStyleBackColor = true;
            this.firstCustLoadButton.Click += new System.EventHandler(this.firstCustLoadButton_Click);
            // 
            // secondCustLoadButton
            // 
            this.secondCustLoadButton.Location = new System.Drawing.Point(448, 41);
            this.secondCustLoadButton.Name = "secondCustLoadButton";
            this.secondCustLoadButton.Size = new System.Drawing.Size(39, 23);
            this.secondCustLoadButton.TabIndex = 1;
            this.secondCustLoadButton.Text = "...";
            this.secondCustLoadButton.UseVisualStyleBackColor = true;
            this.secondCustLoadButton.Click += new System.EventHandler(this.secondCustLoadButton_Click);
            // 
            // firstCustTextBox
            // 
            this.firstCustTextBox.Location = new System.Drawing.Point(149, 15);
            this.firstCustTextBox.Name = "firstCustTextBox";
            this.firstCustTextBox.Size = new System.Drawing.Size(293, 20);
            this.firstCustTextBox.TabIndex = 2;
            // 
            // secondCustTextBox
            // 
            this.secondCustTextBox.Location = new System.Drawing.Point(149, 41);
            this.secondCustTextBox.Name = "secondCustTextBox";
            this.secondCustTextBox.Size = new System.Drawing.Size(293, 20);
            this.secondCustTextBox.TabIndex = 3;
            // 
            // loadButton
            // 
            this.loadButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.loadButton.Location = new System.Drawing.Point(286, 67);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 4;
            this.loadButton.Text = "OK";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(367, 67);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // lblFirstCustomization
            // 
            this.lblFirstCustomization.AutoSize = true;
            this.lblFirstCustomization.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFirstCustomization.Location = new System.Drawing.Point(1, 18);
            this.lblFirstCustomization.Name = "lblFirstCustomization";
            this.lblFirstCustomization.Size = new System.Drawing.Size(113, 13);
            this.lblFirstCustomization.TabIndex = 6;
            this.lblFirstCustomization.Text = "First Customization";
            // 
            // lblSecondCustomization
            // 
            this.lblSecondCustomization.AutoSize = true;
            this.lblSecondCustomization.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSecondCustomization.Location = new System.Drawing.Point(3, 48);
            this.lblSecondCustomization.Name = "lblSecondCustomization";
            this.lblSecondCustomization.Size = new System.Drawing.Size(132, 13);
            this.lblSecondCustomization.TabIndex = 7;
            this.lblSecondCustomization.Text = "Second Customization";
            // 
            // LoadCustomizationsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 94);
            this.Controls.Add(this.lblSecondCustomization);
            this.Controls.Add(this.lblFirstCustomization);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.secondCustTextBox);
            this.Controls.Add(this.firstCustTextBox);
            this.Controls.Add(this.secondCustLoadButton);
            this.Controls.Add(this.firstCustLoadButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(520, 133);
            this.MinimizeBox = false;
            this.Name = "LoadCustomizationsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Load Customizations";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button firstCustLoadButton;
        private System.Windows.Forms.Button secondCustLoadButton;
        private System.Windows.Forms.TextBox firstCustTextBox;
        private System.Windows.Forms.TextBox secondCustTextBox;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label lblFirstCustomization;
        private System.Windows.Forms.Label lblSecondCustomization;
    }
}