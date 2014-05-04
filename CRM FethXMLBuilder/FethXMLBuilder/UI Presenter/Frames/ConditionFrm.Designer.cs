namespace FetchXMLBuilder.UI
{
    partial class ConditionFrm
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
            this.conditionCtrl = new FetchXMLBuilder.UI.ConditionCtrl();
            this.SuspendLayout();
            // 
            // conditionCtrl
            // 
            this.conditionCtrl.Data = null;
            this.conditionCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conditionCtrl.Location = new System.Drawing.Point(0, 0);
            this.conditionCtrl.Name = "conditionCtrl";
            this.conditionCtrl.ParentCtrl = null;
            this.conditionCtrl.Size = new System.Drawing.Size(376, 255);
            this.conditionCtrl.TabIndex = 0;
            this.conditionCtrl.Title = "Condition";
            this.conditionCtrl.Action = FetchXMLBuilder.UI.ActionType.Update;
            // 
            // ConditionFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 255);
            this.ControlBox = false;
            this.Controls.Add(this.conditionCtrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ConditionFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Condition";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private ConditionCtrl conditionCtrl;
    }
}