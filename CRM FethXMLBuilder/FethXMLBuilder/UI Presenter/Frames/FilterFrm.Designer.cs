namespace FetchXMLBuilder.UI
{
    partial class FilterFrm
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
            this.filterCtrl = new FetchXMLBuilder.UI.FilterCtrl();
            this.SuspendLayout();
            // 
            // filterCtrl
            // 
            this.filterCtrl.Data = null;
            this.filterCtrl.Dock = System.Windows.Forms.DockStyle.Fill;

            this.filterCtrl.Location = new System.Drawing.Point(0, 0);
            this.filterCtrl.Name = "filterCtrl";
            this.filterCtrl.ParentCtrl = null;
            this.filterCtrl.Size = new System.Drawing.Size(434, 305);
            this.filterCtrl.TabIndex = 0;
            this.filterCtrl.Title = "Filter";
            this.filterCtrl.Action = FetchXMLBuilder.UI.ActionType.Update;
            // 
            // FilterFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 305);
            this.ControlBox = false;
            this.Controls.Add(this.filterCtrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilterFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FilterFrm";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private FilterCtrl filterCtrl;
    }
}