using System;
using System.ComponentModel;

namespace LanaSoft.Licensing.Usage
{
	public class CRMDiscountPackageLicense: LicenseValidator
	{
		private const string _companyName = "GFK Ukraine";
		private const string _productName = "Discount Package Installation Tool";

		[Category("Customer Information"), Description("Public customer identifiniter"), Unreadable]
		public string CustomerId
		{
			get {return Properties.GetString("CustomerId");}
			set
			{
				Properties["CustomerId"] = value;
			}
		}

		[Category("Customer Information"), Description("The customer company name")]
		public string CompanyName
		{
			get {return Properties.GetString("CompanyName");}
			set
			{
				Properties["CompanyName"] = value;
			}
		}


		[Category("Customer Information"), Description("The customer address")]
		public string Address
		{
			get {return Properties.GetString("Address");}
			set
			{
				Properties["Address"] = value;
			}
		}


		/// <summary>
		/// Product INFORMATION		 		
		/// </summary>
		
		[Category("Customer Information"), Description("Discount Package Setup Tool Name")]
		public string ProductName
		{
			get {return Properties.GetString("ProductName");}
			set
			{
				Properties["ProductName"] = value;
			}
		}


				
		[Category("License Information"), Description("License number")]
		public string LicenseNumber
		{
			get {return Properties.GetString("LicenseNumber");}
			set
			{
				Properties["LicenseNumber"] = value;
			}
		}		


		[Category("License Information"), Description("Date when license is expired")]
		public DateTime ExpirationDate
		{
			get {return Properties.GetDateTime("ExpirationDate");}
			set
			{
				Properties["ExpirationDate"] = value;
			}
		}		

		public CRMDiscountPackageLicense()
		{
		}

		public override void Initialize()
		{	
			ExpirationDate = DateTime.Now;
			CompanyName = _companyName;
			ProductName = _productName;		
		}

		
	}
}
