
namespace LanaSoftCRM
{
	/// <summary>
	/// Used to add a group with the defined attributes	to the site map
	/// </summary>
	public struct Group
	{
		#region Fields
		private string _description;
		private string _icon;
		private string _id;
		private IsProfileType _isProfile;
		private LicenseTypes _license;
		private string _title;
		private string _url;
		#endregion

		#region Properties
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
			}
		}

		public string Icon
		{
			get
			{
				return _icon;
			}
			set
			{
				_icon = value;
			}
		}

		public string Id
		{
			get
			{
				return _id;
			}
			set
			{
				if (Utility.IsAlphaNumeric(value))
				{
					_id = value;
				}
			}
		}
			
		public IsProfileType IsProfile
		{
			get
			{
				return _isProfile;
			}
			set
			{
				_isProfile = value;
			}
		}

		public LicenseTypes License
		{
			get
			{
				return _license;
			}
			set
			{
				_license = value;
			}
		}

		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				_title = value;
			}
		}

		public string Url
		{
			get
			{
				return _url;
			}
			set
			{
				_url = value;
			}
		}
		#endregion
	}
}