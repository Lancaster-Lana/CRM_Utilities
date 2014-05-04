
namespace LanaSoftCRM
{
	/// <summary>
	/// Used to add a sub-area with the defined attributes to the site map
	/// </summary>
	public struct SubArea
	{
		#region Fields
		private AvailableOfflineType _availableOffline;
		private ClientTypes _client;
		private string _description;
		private string _entity;
		private string _icon;
		private string _id;
		private LicenseTypes _license;
		private string _outlookShortcutIcon;
		private string _title;
		private string _url;
		#endregion

		#region Properties
		public AvailableOfflineType AvailableOffline
		{
			get
			{
				return _availableOffline;
			}
			set
			{
				_availableOffline = value;
			}
		}

		public ClientTypes Client
		{
			get
			{
				return _client;
			}
			set
			{
				_client = value;
			}
		}

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
		public string Entity
		{
			get
			{
				return _entity;
			}
			set
			{
				_entity = value;
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

		public string OutlookShortcutIcon
		{
			get
			{
				return _outlookShortcutIcon;
			}
			set
			{
				_outlookShortcutIcon = value;
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