namespace CustomizationsComparator
{
    partial class SettingsDialog
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
            this.treeColorDialog = new System.Windows.Forms.ColorDialog();
            this.colorPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.colorGroupBox = new System.Windows.Forms.GroupBox();
            this.commonColorBox = new System.Windows.Forms.PictureBox();
            this.colorCommonButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.secondColorBox = new System.Windows.Forms.PictureBox();
            this.colorSecondButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.firstColorBox = new System.Windows.Forms.PictureBox();
            this.colorFirstButton = new System.Windows.Forms.Button();
            this.showCurrentCellContentCheckBox = new System.Windows.Forms.CheckBox();
            this.autoNavigateCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.differColorButton = new System.Windows.Forms.Button();
            this.differColorBox = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.absentButton = new System.Windows.Forms.Button();
            this.absentColorBox = new System.Windows.Forms.PictureBox();
            this.gridGroupBox = new System.Windows.Forms.GroupBox();
            this.gridsGroupBox = new System.Windows.Forms.GroupBox();
            this.showCellGroupBox = new System.Windows.Forms.GroupBox();
            this.wordPadRadioButton = new System.Windows.Forms.RadioButton();
            this.showCellContentOnFormRadioButton = new System.Windows.Forms.RadioButton();
            this.colorPanel.SuspendLayout();
            this.colorGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commonColorBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondColorBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstColorBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.differColorBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.absentColorBox)).BeginInit();
            this.gridGroupBox.SuspendLayout();
            this.gridsGroupBox.SuspendLayout();
            this.showCellGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // colorPanel
            // 
            this.colorPanel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.colorPanel.Controls.Add(this.cancelButton);
            this.colorPanel.Controls.Add(this.OKButton);
            this.colorPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.colorPanel.Location = new System.Drawing.Point(0, 227);
            this.colorPanel.Name = "colorPanel";
            this.colorPanel.Size = new System.Drawing.Size(373, 60);
            this.colorPanel.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cancelButton.Location = new System.Drawing.Point(184, 25);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(70, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(98, 25);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(70, 23);
            this.OKButton.TabIndex = 8;
            this.OKButton.Text = "Apply";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "First ";
            // 
            // colorGroupBox
            // 
            this.colorGroupBox.Controls.Add(this.commonColorBox);
            this.colorGroupBox.Controls.Add(this.colorCommonButton);
            this.colorGroupBox.Controls.Add(this.label3);
            this.colorGroupBox.Controls.Add(this.secondColorBox);
            this.colorGroupBox.Controls.Add(this.colorSecondButton);
            this.colorGroupBox.Controls.Add(this.label2);
            this.colorGroupBox.Controls.Add(this.firstColorBox);
            this.colorGroupBox.Controls.Add(this.colorFirstButton);
            this.colorGroupBox.Controls.Add(this.label1);
            this.colorGroupBox.Location = new System.Drawing.Point(14, 12);
            this.colorGroupBox.Name = "colorGroupBox";
            this.colorGroupBox.Size = new System.Drawing.Size(164, 94);
            this.colorGroupBox.TabIndex = 1;
            this.colorGroupBox.TabStop = false;
            this.colorGroupBox.Text = "Select customization color";
            // 
            // commonColorBox
            // 
            this.commonColorBox.BackColor = System.Drawing.Color.Transparent;
            this.commonColorBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.commonColorBox.Location = new System.Drawing.Point(76, 67);
            this.commonColorBox.Name = "commonColorBox";
            this.commonColorBox.Size = new System.Drawing.Size(34, 20);
            this.commonColorBox.TabIndex = 8;
            this.commonColorBox.TabStop = false;
            // 
            // colorCommonButton
            // 
            this.colorCommonButton.Location = new System.Drawing.Point(116, 67);
            this.colorCommonButton.Name = "colorCommonButton";
            this.colorCommonButton.Size = new System.Drawing.Size(34, 20);
            this.colorCommonButton.TabIndex = 7;
            this.colorCommonButton.Text = "...";
            this.colorCommonButton.UseVisualStyleBackColor = true;
            this.colorCommonButton.Click += new System.EventHandler(this.colorCommonButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Common";
            // 
            // secondColorBox
            // 
            this.secondColorBox.BackColor = System.Drawing.Color.LightCyan;
            this.secondColorBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.secondColorBox.Location = new System.Drawing.Point(76, 43);
            this.secondColorBox.Name = "secondColorBox";
            this.secondColorBox.Size = new System.Drawing.Size(34, 20);
            this.secondColorBox.TabIndex = 5;
            this.secondColorBox.TabStop = false;
            // 
            // colorSecondButton
            // 
            this.colorSecondButton.Location = new System.Drawing.Point(116, 43);
            this.colorSecondButton.Name = "colorSecondButton";
            this.colorSecondButton.Size = new System.Drawing.Size(34, 20);
            this.colorSecondButton.TabIndex = 4;
            this.colorSecondButton.Text = "...";
            this.colorSecondButton.UseVisualStyleBackColor = true;
            this.colorSecondButton.Click += new System.EventHandler(this.colorSecondButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Second";
            // 
            // firstColorBox
            // 
            this.firstColorBox.BackColor = System.Drawing.SystemColors.Info;
            this.firstColorBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.firstColorBox.Location = new System.Drawing.Point(76, 19);
            this.firstColorBox.Name = "firstColorBox";
            this.firstColorBox.Size = new System.Drawing.Size(34, 20);
            this.firstColorBox.TabIndex = 2;
            this.firstColorBox.TabStop = false;
            // 
            // colorFirstButton
            // 
            this.colorFirstButton.Location = new System.Drawing.Point(116, 19);
            this.colorFirstButton.Name = "colorFirstButton";
            this.colorFirstButton.Size = new System.Drawing.Size(34, 20);
            this.colorFirstButton.TabIndex = 1;
            this.colorFirstButton.Text = "...";
            this.colorFirstButton.UseVisualStyleBackColor = true;
            this.colorFirstButton.Click += new System.EventHandler(this.colorFirstButton_Click);
            // 
            // showCurrentCellContentCheckBox
            // 
            this.showCurrentCellContentCheckBox.AutoSize = true;
            this.showCurrentCellContentCheckBox.Location = new System.Drawing.Point(23, 43);
            this.showCurrentCellContentCheckBox.Name = "showCurrentCellContentCheckBox";
            this.showCurrentCellContentCheckBox.Size = new System.Drawing.Size(150, 17);
            this.showCurrentCellContentCheckBox.TabIndex = 9;
            this.showCurrentCellContentCheckBox.Text = "Show Current Cell Content";
            this.showCurrentCellContentCheckBox.UseVisualStyleBackColor = true;
            this.showCurrentCellContentCheckBox.CheckedChanged += new System.EventHandler(this.showCurrentCellContentCheckBox_CheckedChanged);
            // 
            // autoNavigateCheckBox
            // 
            this.autoNavigateCheckBox.AutoSize = true;
            this.autoNavigateCheckBox.Location = new System.Drawing.Point(23, 19);
            this.autoNavigateCheckBox.Name = "autoNavigateCheckBox";
            this.autoNavigateCheckBox.Size = new System.Drawing.Size(142, 17);
            this.autoNavigateCheckBox.TabIndex = 8;
            this.autoNavigateCheckBox.Text = "Auto Navigate Selected ";
            this.autoNavigateCheckBox.UseVisualStyleBackColor = true;
            this.autoNavigateCheckBox.CheckedChanged += new System.EventHandler(this.autoNavigateCheckBox_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Differ properties";
            // 
            // differColorButton
            // 
            this.differColorButton.Location = new System.Drawing.Point(130, 19);
            this.differColorButton.Name = "differColorButton";
            this.differColorButton.Size = new System.Drawing.Size(34, 20);
            this.differColorButton.TabIndex = 1;
            this.differColorButton.Text = "...";
            this.differColorButton.UseVisualStyleBackColor = true;
            this.differColorButton.Click += new System.EventHandler(this.differColorButton_Click);
            // 
            // differColorBox
            // 
            this.differColorBox.BackColor = System.Drawing.Color.Red;
            this.differColorBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.differColorBox.Location = new System.Drawing.Point(90, 19);
            this.differColorBox.Name = "differColorBox";
            this.differColorBox.Size = new System.Drawing.Size(34, 20);
            this.differColorBox.TabIndex = 2;
            this.differColorBox.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Absent";
            // 
            // absentButton
            // 
            this.absentButton.Location = new System.Drawing.Point(130, 43);
            this.absentButton.Name = "absentButton";
            this.absentButton.Size = new System.Drawing.Size(34, 20);
            this.absentButton.TabIndex = 4;
            this.absentButton.Text = "...";
            this.absentButton.UseVisualStyleBackColor = true;
            this.absentButton.Click += new System.EventHandler(this.absentButton_Click);
            // 
            // absentColorBox
            // 
            this.absentColorBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.absentColorBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.absentColorBox.Location = new System.Drawing.Point(90, 45);
            this.absentColorBox.Name = "absentColorBox";
            this.absentColorBox.Size = new System.Drawing.Size(34, 20);
            this.absentColorBox.TabIndex = 5;
            this.absentColorBox.TabStop = false;
            // 
            // gridGroupBox
            // 
            this.gridGroupBox.Controls.Add(this.absentColorBox);
            this.gridGroupBox.Controls.Add(this.absentButton);
            this.gridGroupBox.Controls.Add(this.label5);
            this.gridGroupBox.Controls.Add(this.differColorBox);
            this.gridGroupBox.Controls.Add(this.differColorButton);
            this.gridGroupBox.Controls.Add(this.label6);
            this.gridGroupBox.Location = new System.Drawing.Point(184, 31);
            this.gridGroupBox.Name = "gridGroupBox";
            this.gridGroupBox.Size = new System.Drawing.Size(173, 75);
            this.gridGroupBox.TabIndex = 10;
            this.gridGroupBox.TabStop = false;
            this.gridGroupBox.Text = "Select customization color";
            // 
            // gridsGroupBox
            // 
            this.gridsGroupBox.Controls.Add(this.showCellGroupBox);
            this.gridsGroupBox.Controls.Add(this.autoNavigateCheckBox);
            this.gridsGroupBox.Controls.Add(this.showCurrentCellContentCheckBox);
            this.gridsGroupBox.Location = new System.Drawing.Point(12, 112);
            this.gridsGroupBox.Name = "gridsGroupBox";
            this.gridsGroupBox.Size = new System.Drawing.Size(352, 109);
            this.gridsGroupBox.TabIndex = 12;
            this.gridsGroupBox.TabStop = false;
            this.gridsGroupBox.Text = "Grids Results";
            // 
            // showCellGroupBox
            // 
            this.showCellGroupBox.Controls.Add(this.wordPadRadioButton);
            this.showCellGroupBox.Controls.Add(this.showCellContentOnFormRadioButton);
            this.showCellGroupBox.Enabled = false;
            this.showCellGroupBox.Location = new System.Drawing.Point(179, 36);
            this.showCellGroupBox.Name = "showCellGroupBox";
            this.showCellGroupBox.Size = new System.Drawing.Size(167, 67);
            this.showCellGroupBox.TabIndex = 12;
            this.showCellGroupBox.TabStop = false;
            this.showCellGroupBox.Text = "as";
            // 
            // wordPadRadioButton
            // 
            this.wordPadRadioButton.AutoSize = true;
            this.wordPadRadioButton.Location = new System.Drawing.Point(21, 42);
            this.wordPadRadioButton.Name = "wordPadRadioButton";
            this.wordPadRadioButton.Size = new System.Drawing.Size(145, 17);
            this.wordPadRadioButton.TabIndex = 1;
            this.wordPadRadioButton.Text = "WordPad on double click";
            this.wordPadRadioButton.UseVisualStyleBackColor = true;
            this.wordPadRadioButton.CheckedChanged += new System.EventHandler(this.wordPadRadioButton_CheckedChanged);
            // 
            // showCellContentOnFormRadioButton
            // 
            this.showCellContentOnFormRadioButton.AutoSize = true;
            this.showCellContentOnFormRadioButton.Checked = true;
            this.showCellContentOnFormRadioButton.Location = new System.Drawing.Point(21, 19);
            this.showCellContentOnFormRadioButton.Name = "showCellContentOnFormRadioButton";
            this.showCellContentOnFormRadioButton.Size = new System.Drawing.Size(125, 17);
            this.showCellContentOnFormRadioButton.TabIndex = 0;
            this.showCellContentOnFormRadioButton.TabStop = true;
            this.showCellContentOnFormRadioButton.Text = "Form field after select";
            this.showCellContentOnFormRadioButton.UseVisualStyleBackColor = true;
            this.showCellContentOnFormRadioButton.CheckedChanged += new System.EventHandler(this.showCellContentOnFormRadioButton_CheckedChanged);
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 287);
            this.Controls.Add(this.gridsGroupBox);
            this.Controls.Add(this.gridGroupBox);
            this.Controls.Add(this.colorGroupBox);
            this.Controls.Add(this.colorPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(382, 367);
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SettingsDialog_Load);
            this.colorPanel.ResumeLayout(false);
            this.colorGroupBox.ResumeLayout(false);
            this.colorGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commonColorBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondColorBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstColorBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.differColorBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.absentColorBox)).EndInit();
            this.gridGroupBox.ResumeLayout(false);
            this.gridGroupBox.PerformLayout();
            this.gridsGroupBox.ResumeLayout(false);
            this.gridsGroupBox.PerformLayout();
            this.showCellGroupBox.ResumeLayout(false);
            this.showCellGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColorDialog treeColorDialog;
        private System.Windows.Forms.Panel colorPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox colorGroupBox;
        private System.Windows.Forms.PictureBox commonColorBox;
        private System.Windows.Forms.Button colorCommonButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox secondColorBox;
        private System.Windows.Forms.Button colorSecondButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox firstColorBox;
        private System.Windows.Forms.Button colorFirstButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox showCurrentCellContentCheckBox;
        private System.Windows.Forms.CheckBox autoNavigateCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button differColorButton;
        private System.Windows.Forms.PictureBox differColorBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button absentButton;
        private System.Windows.Forms.PictureBox absentColorBox;
        private System.Windows.Forms.GroupBox gridGroupBox;
        private System.Windows.Forms.GroupBox gridsGroupBox;
        private System.Windows.Forms.GroupBox showCellGroupBox;
        private System.Windows.Forms.RadioButton wordPadRadioButton;
        private System.Windows.Forms.RadioButton showCellContentOnFormRadioButton;
    }
}