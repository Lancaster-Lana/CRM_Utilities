namespace FetchXMLBuilder.UI
{
    partial class FetchXMLForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FetchXMLForm));
            this.commonSplitContainer = new System.Windows.Forms.SplitContainer();
            this.fetchXMLControl1 = new FetchXMLBuilder.UI.FetchXMLControl();
            this.runToolStrip = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.fetchXMLToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.generateFetchXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.attributeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkEntityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resulFetchXMLRichTextBox = new System.Windows.Forms.RichTextBox();
            this.commonSplitContainer.Panel1.SuspendLayout();
            this.commonSplitContainer.Panel2.SuspendLayout();
            this.commonSplitContainer.SuspendLayout();
            this.runToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // commonSplitContainer
            // 
            this.commonSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commonSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.commonSplitContainer.Name = "commonSplitContainer";
            this.commonSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // commonSplitContainer.Panel1
            // 
            this.commonSplitContainer.Panel1.Controls.Add(this.fetchXMLControl1);
            this.commonSplitContainer.Panel1.Controls.Add(this.runToolStrip);
            // 
            // commonSplitContainer.Panel2
            // 
            this.commonSplitContainer.Panel2.Controls.Add(this.resulFetchXMLRichTextBox);
            this.commonSplitContainer.Size = new System.Drawing.Size(797, 493);
            this.commonSplitContainer.SplitterDistance = 290;
            this.commonSplitContainer.TabIndex = 0;
            // 
            // fetchXMLControl1
            // 
            this.fetchXMLControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fetchXMLControl1.Location = new System.Drawing.Point(0, 25);
            this.fetchXMLControl1.Name = "fetchXMLControl1";
            this.fetchXMLControl1.Size = new System.Drawing.Size(797, 265);
            this.fetchXMLControl1.TabIndex = 1;
            // 
            // runToolStrip
            // 
            this.runToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.fetchXMLToolStripButton,
            this.addToolStripButton});
            this.runToolStrip.Location = new System.Drawing.Point(0, 0);
            this.runToolStrip.Name = "runToolStrip";
            this.runToolStrip.Size = new System.Drawing.Size(797, 25);
            this.runToolStrip.TabIndex = 0;
            this.runToolStrip.Text = "Run";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.newToolStripButton.Size = new System.Drawing.Size(81, 22);
            this.newToolStripButton.Text = "New Query";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(62, 22);
            this.openToolStripButton.Text = "Open";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(51, 22);
            this.saveToolStripButton.Text = "Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // fetchXMLToolStripButton
            // 
            this.fetchXMLToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateFetchXMLToolStripMenuItem,
            this.executeToolStripMenuItem});
            this.fetchXMLToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fetchXMLToolStripButton.Name = "fetchXMLToolStripButton";
            this.fetchXMLToolStripButton.Size = new System.Drawing.Size(69, 22);
            this.fetchXMLToolStripButton.Text = " FetchXML";
            // 
            // generateFetchXMLToolStripMenuItem
            // 
            this.generateFetchXMLToolStripMenuItem.Name = "generateFetchXMLToolStripMenuItem";
            this.generateFetchXMLToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.generateFetchXMLToolStripMenuItem.Text = "Generate";
            // 
            // executeToolStripMenuItem
            // 
            this.executeToolStripMenuItem.Name = "executeToolStripMenuItem";
            this.executeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.executeToolStripMenuItem.Text = "Execute ";
            // 
            // addToolStripButton
            // 
            this.addToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.attributeToolStripMenuItem1,
            this.filterToolStripMenuItem,
            this.linkEntityToolStripMenuItem});
            this.addToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("addToolStripButton.Image")));
            this.addToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addToolStripButton.Name = "addToolStripButton";
            this.addToolStripButton.Size = new System.Drawing.Size(55, 22);
            this.addToolStripButton.Text = "Add";
            // 
            // attributeToolStripMenuItem1
            // 
            this.attributeToolStripMenuItem1.Name = "attributeToolStripMenuItem1";
            this.attributeToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.attributeToolStripMenuItem1.Text = "Attribute";
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // linkEntityToolStripMenuItem
            // 
            this.linkEntityToolStripMenuItem.Name = "linkEntityToolStripMenuItem";
            this.linkEntityToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.linkEntityToolStripMenuItem.Text = "Link Entity";
            // 
            // resulFetchXMLRichTextBox
            // 
            this.resulFetchXMLRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resulFetchXMLRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.resulFetchXMLRichTextBox.Name = "resulFetchXMLRichTextBox";
            this.resulFetchXMLRichTextBox.Size = new System.Drawing.Size(797, 199);
            this.resulFetchXMLRichTextBox.TabIndex = 1;
            this.resulFetchXMLRichTextBox.Text = "";
            // 
            // FetchXMLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 493);
            this.Controls.Add(this.commonSplitContainer);
            this.Name = "FetchXMLForm";
            this.Text = "FetchXML builder";
            this.commonSplitContainer.Panel1.ResumeLayout(false);
            this.commonSplitContainer.Panel1.PerformLayout();
            this.commonSplitContainer.Panel2.ResumeLayout(false);
            this.commonSplitContainer.ResumeLayout(false);
            this.runToolStrip.ResumeLayout(false);
            this.runToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer commonSplitContainer;
        private System.Windows.Forms.ToolStrip runToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton addToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linkEntityToolStripMenuItem;
        private System.Windows.Forms.RichTextBox resulFetchXMLRichTextBox;
        private System.Windows.Forms.ToolStripMenuItem attributeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripDropDownButton fetchXMLToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem generateFetchXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem executeToolStripMenuItem;
        private FetchXMLControl fetchXMLControl1;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripDropDownButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;      
    }
}

