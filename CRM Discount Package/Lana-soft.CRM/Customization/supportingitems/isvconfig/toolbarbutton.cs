
namespace LanaSoftCRM
{
	/// <summary>
	/// Describes a Menu Item that may appear in a menu on the global menu bar, entity menu bar or grid actions menu
	/// </summary>
	public struct ToolBarButton
	{
		private string _id;
		private string _title;
		private string _toolTip;
		private string _iconUrl;
		private string _url;
		private string _winParams;
		//private string _jScript;
		private string _javaScript;
		private char _accessKey;
		private ClientTypes _clients;
		private WindowMode _winMode;
		private ConfigurationBoolean _passParams;
		private ConfigurationBoolean _availableOffline;
		private ConfigurationBoolean _validForCreate;
		private ConfigurationBoolean _validForUpdate;


		/// <summary>
		/// The title of the menu item
		/// </summary>
		public string Id
		{
			get
			{
				return _id;
			}

			set
			{
				_id = value;
			}
		}

		/// <summary>
		/// The title of the menu item
		/// </summary>
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

		/// <summary>
		/// The tool tip text that will display when the user moves the mouse to the button
		/// </summary>
		public string ToolTip
		{
			get
			{
				return _toolTip;
			}

			set
			{
				_toolTip = value;
			}
		}
		
		/// <summary>
		/// The Windows "Hot key" that will activate this item.  Note this character must exist as a character
		/// within the title.
		/// </summary>
		public char AccessHotKey
		{
			get
			{
				return _accessKey;
			}

			set
			{
				_accessKey = value;
			}
		}

		/// <summary>
		/// The URL to the Icon that should be rendered next to the title for this item
		/// </summary>
		public string Icon
		{
			get
			{
				return _iconUrl;
			}

			set
			{
				_iconUrl = value;
			}
		}

		/// <summary>
		/// The URL to load when the user navigates to this item
		/// </summary>
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

		/// <summary>
		/// Determines what type of new window should be loaded when the user selects this item
		/// </summary>
		public string JavaScript
		{
			get
			{
				return _javaScript;
			}

			set
			{
				_javaScript = value;
			}
		}

		/// <summary>
		/// Determines what type of new window should be loaded when the user selects this item
		/// </summary>
		public WindowMode WindowLaunchMode
		{
			get
			{
				return _winMode;
			}

			set
			{
				_winMode = value;
			}
		}
		
		/// <summary>
		/// Determines what type of new window should be loaded when the user selects this item
		/// </summary>
		public string WindowParameters
		{
			get
			{
				return _winParams;
			}

			set
			{
				_winParams = value;
			}
		}

		
		/// <summary>
		/// Determines what Microsoft CRM clients this menu item should be available in
		/// </summary>
		public ClientTypes SupportedClients
		{
			get
			{
				return _clients;
			}

			set
			{
				_clients = value;
			}
		}

		/// <summary>
		/// Indicates if the object type and object ID parameters are to be passed to the URL. 
		/// </summary>
		public ConfigurationBoolean PassParameters
		{
			get
			{
				return _passParams;
			}

			set
			{
				_passParams = value;
			}
		}

		/// <summary>
		/// Set this to true if the menu item does not depend on the user being connected to the organization's network.
		/// </summary>
		public ConfigurationBoolean AvailableOffline
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

		/// <summary>
		/// Indicates if this menu item should render on the "Create" (New) form of an entity
		/// </summary>
		public ConfigurationBoolean ValideForCreate
		{
			get
			{
				return _validForCreate;
			}

			set
			{
				_validForCreate = value;
			}
		}

		/// <summary>
		/// Indicates if this menu item should render on the "Update" (Existing) form of an entity
		/// </summary>
		public ConfigurationBoolean ValidForUpdate
		{
			get
			{
				return _validForUpdate;
			}

			set
			{
				_validForUpdate = value;
			}
		}
	}
}
