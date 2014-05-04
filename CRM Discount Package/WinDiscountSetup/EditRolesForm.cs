using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinDiscountSetup
{
	/// <summary>
	/// Summary description for EditRolesForm.
	/// </summary>
	public class EditRolesForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label RolesEditLabel;
		private AxSHDocVw.AxWebBrowser axRolesWebBrowser;
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public EditRolesForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(EditRolesForm));
			this.RolesEditLabel = new System.Windows.Forms.Label();
			this.axRolesWebBrowser = new AxSHDocVw.AxWebBrowser();
			((System.ComponentModel.ISupportInitialize)(this.axRolesWebBrowser)).BeginInit();
			this.SuspendLayout();
			// 
			// RolesEditLabel
			// 
			this.RolesEditLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.RolesEditLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.RolesEditLabel.ForeColor = System.Drawing.Color.Brown;
			this.RolesEditLabel.Location = new System.Drawing.Point(0, 0);
			this.RolesEditLabel.Name = "RolesEditLabel";
			this.RolesEditLabel.Size = new System.Drawing.Size(488, 80);
			this.RolesEditLabel.TabIndex = 37;
			this.RolesEditLabel.Text = @"    To be discounts availabled: for each role related with Sales Process (for instance, SalesManager (as suplier) and  Salesperson (as customer)) :    1) Click role name 2) select  the page ""Custom Entities""   3) set access permissions  for every new entity";
			// 
			// axRolesWebBrowser
			// 
			this.axRolesWebBrowser.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.axRolesWebBrowser.Enabled = true;
			this.axRolesWebBrowser.Location = new System.Drawing.Point(0, 93);
			this.axRolesWebBrowser.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axRolesWebBrowser.OcxState")));
			this.axRolesWebBrowser.Size = new System.Drawing.Size(488, 304);
			this.axRolesWebBrowser.TabIndex = 38;
			// 
			// EditRolesForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(488, 397);
			this.Controls.Add(this.axRolesWebBrowser);
			this.Controls.Add(this.RolesEditLabel);
			this.Name = "EditRolesForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Role permissions";
			this.Load += new System.EventHandler(this.EditRolesForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.axRolesWebBrowser)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void EditEntityPermissionInRole()
		{
			axRolesWebBrowser.Navigate("http://localhost:5555/Tools/business/home_role.aspx");
			//http://localhost:5555/Biz/Roles/edit.aspx
			//http://localhost:5555/Tools/business/home_role.aspx
		}

		private void EditRolesForm_Load(object sender, System.EventArgs e)
		{
			EditEntityPermissionInRole();
		}
	}
}
