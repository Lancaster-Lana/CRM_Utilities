
namespace LanaSoftCRM
{
	/// <summary>
	/// Used to add an area with the defined attributes to the site map
	/// </summary>
	public struct Area
	{
		#region Fields
		private string _description;
		private string _icon;
		private string _id;
		private LicenseTypes _license;
		private ShowGroupsType _showGroups;
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

		public ShowGroupsType ShowGroups
		{
			get
			{
				return _showGroups;
			}
			set
			{
				_showGroups = value;
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