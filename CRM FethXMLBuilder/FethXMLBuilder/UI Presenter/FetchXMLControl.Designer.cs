namespace FetchXMLBuilder.UI
{
    partial class FetchXMLControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FetchXMLControl));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Attributes");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Condition");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Filter type", new System.Windows.Forms.TreeNode[] {
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Link-entity name=");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Entity name =", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3,
            treeNode4});
            this.fetchXMLTreeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attributeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conditionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.linkEntityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attributesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.fetchXMLTreeView = new System.Windows.Forms.TreeView();
            this.fetchXMLTreeContextMenuStrip.SuspendLayout();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // fetchXMLTreeContextMenuStrip
            // 
            this.fetchXMLTreeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparator1,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.fetchXMLTreeContextMenuStrip.Name = "fetchXMLTreeContextMenuStrip";
            this.fetchXMLTreeContextMenuStrip.Size = new System.Drawing.Size(117, 76);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.orderToolStripMenuItem,
            this.attributeToolStripMenuItem,
            this.toolStripSeparator2,
            this.filterToolStripMenuItem,
            this.conditionToolStripMenuItem,
            this.toolStripSeparator3,
            this.linkEntityToolStripMenuItem});
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.newToolStripMenuItem.Text = "Add";
            // 
            // orderToolStripMenuItem
            // 
            this.orderToolStripMenuItem.Name = "orderToolStripMenuItem";
            this.orderToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.orderToolStripMenuItem.Text = "Order";
            this.orderToolStripMenuItem.Click += new System.EventHandler(this.orderToolStripMenuItem_Click);
            // 
            // attributeToolStripMenuItem
            // 
            this.attributeToolStripMenuItem.Name = "attributeToolStripMenuItem";
            this.attributeToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.attributeToolStripMenuItem.Text = "Attribute";
            this.attributeToolStripMenuItem.Click += new System.EventHandler(this.attributeToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(128, 6);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.filterToolStripMenuItem.Text = "Filter";
            this.filterToolStripMenuItem.Click += new System.EventHandler(this.filterToolStripMenuItem_Click);
            // 
            // conditionToolStripMenuItem
            // 
            this.conditionToolStripMenuItem.Name = "conditionToolStripMenuItem";
            this.conditionToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.conditionToolStripMenuItem.Text = "Condition";
            this.conditionToolStripMenuItem.Click += new System.EventHandler(this.conditionToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(128, 6);
            // 
            // linkEntityToolStripMenuItem
            // 
            this.linkEntityToolStripMenuItem.Name = "linkEntityToolStripMenuItem";
            this.linkEntityToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.linkEntityToolStripMenuItem.Text = "LinkEntity";
            this.linkEntityToolStripMenuItem.Click += new System.EventHandler(this.linkEntityToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.attributesToolStripMenuItem});
            this.editToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editToolStripMenuItem.Image")));
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Visible = false;
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // attributesToolStripMenuItem
            // 
            this.attributesToolStripMenuItem.Name = "attributesToolStripMenuItem";
            this.attributesToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.attributesToolStripMenuItem.Text = "Attributes";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.fetchXMLTreeView);
            this.mainSplitContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mainSplitContainer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mainSplitContainer.Size = new System.Drawing.Size(956, 518);
            this.mainSplitContainer.SplitterDistance = 520;
            this.mainSplitContainer.SplitterWidth = 5;
            this.mainSplitContainer.TabIndex = 4;
            // 
            // fetchXMLTreeView
            // 
            this.fetchXMLTreeView.ContextMenuStrip = this.fetchXMLTreeContextMenuStrip;
            this.fetchXMLTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fetchXMLTreeView.Location = new System.Drawing.Point(0, 0);
            this.fetchXMLTreeView.Name = "fetchXMLTreeView";
            treeNode1.Name = "AttributesNode";
            treeNode1.Tag = "attribute";
            treeNode1.Text = "Attributes";
            treeNode2.Name = "ConditionNode";
            treeNode2.Tag = "condition";
            treeNode2.Text = "Condition";
            treeNode3.Name = "FilterNode";
            treeNode3.Tag = "filter";
            treeNode3.Text = "Filter type";
            treeNode4.Name = "LinkEntityNode";
            treeNode4.Tag = "linkedEntity";
            treeNode4.Text = "Link-entity name=";
            treeNode5.Name = "EntityNode";
            treeNode5.Tag = "entity";
            treeNode5.Text = "Entity name =";
            this.fetchXMLTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5});
            this.fetchXMLTreeView.Size = new System.Drawing.Size(520, 518);
            this.fetchXMLTreeView.TabIndex = 1;
            this.fetchXMLTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.fetchXMLTreeView_AfterSelect);
            this.fetchXMLTreeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.fetchXMLTreeView_BeforeSelect);
            // 
            // FetchXMLControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainSplitContainer);
            this.Name = "FetchXMLControl";
            this.Size = new System.Drawing.Size(956, 518);
            this.fetchXMLTreeContextMenuStrip.ResumeLayout(false);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip fetchXMLTreeContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem attributeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linkEntityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conditionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.TreeView fetchXMLTreeView;
        private System.Windows.Forms.ToolStripMenuItem attributesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orderToolStripMenuItem;
    }
}
