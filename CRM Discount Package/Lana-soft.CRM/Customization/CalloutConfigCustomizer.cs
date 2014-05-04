using System.Xml;

namespace LanaSoftCRM
{
	/// <summary>
	/// This class allows you to modify the Microsoft CRM 3.0 Callout Configuration File
	/// </summary>
	public class CalloutConfigCustomizer
	{
		// Constants
		private const string DEFAULT_PREFIX	= "crmDefault";	// sets the default prefix used by the NamespaceManager for the XmlDocument

		#region Fields

		private XmlDocument			_calloutConfigXml;
		private XmlNamespaceManager _manager;
		private string				_defaultNamespace;
		private string				_subscriptionAssembly;
		private string				_subscriptionClass;

		#endregion

		#region Properties

		/// <summary>
		/// The Callout.Config XML
		/// </summary>
		public XmlDocument Xml
		{
			get
			{
				return _calloutConfigXml;
			}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// This class allows you to modify the Microsoft CRM 3.0 Callout Configuration File
		/// </summary>
		/// <param name="calloutConfigXml">An XML Document containing the Callout.config.xml</param>
		/// <param name="subscriptionAssembly">The name of the assembly to register or unregister</param>
		/// <param name="subscriptionClass">The name of the class to register or unregister</param>
		public CalloutConfigCustomizer(XmlDocument calloutConfigXml, string subscriptionAssembly, string subscriptionClass)
		{
			_subscriptionAssembly	= subscriptionAssembly;
			_subscriptionClass		= subscriptionClass;
			_calloutConfigXml		= calloutConfigXml;

			// retrieves the default namespace dynamically (should be "http://microsoft.com/mscrm/callout/")
			_defaultNamespace = _calloutConfigXml.DocumentElement.NamespaceURI;
                
			_manager = new XmlNamespaceManager(_calloutConfigXml.NameTable);
			_manager.AddNamespace(DEFAULT_PREFIX, _defaultNamespace);
		}

		#endregion

		#region Methods: Adding Callouts

		/// <summary>
		/// Used to add callouts for specified entities to callout.config.xml
		/// </summary>
		/// <param name="entityNames">Names of the entities that will use pre-callouts</param>
		/// <param name="calloutType">Specifies the type of the pre-callout/param>
		/// <remarks>This method automatically creates a backup file of callout.config.xml before saving the changes</remarks>
		public void AddCallouts(string[] entityNames, PreCalloutType calloutType)
		{
			for(int i = 0; i < entityNames.Length; i++)
			{
				AddCallout(entityNames[i], OnErrorType.Default, 0, calloutType);
			}
		}

		/// <summary>
		/// Used to add callouts for specified entities to callout.config.xml
		/// </summary>
		/// <param name="entityNames">Names of the entities that will use pre-callouts</param>
		/// <param name="onErrors">Specifies what action to take when an error occurs. Each item 
		/// in the onErrors array corresponds with an item in the entityNames array at the same index.</param>
		/// <param name="timeouts">Specifies the amount of time (in seconds) that the callout is
		/// allowed to execute before the system terminates its execution by throwing an exception.
		/// The timeout value can be between 5 and 600. If the timeout is set any other value outside this
		/// allowable range, the default timeout of 60 seconds is used. Each item in the timeouts array
		/// corresponds with an item in the entityNames array at the same index.</param>
      /// <param name="calloutType">Specifies the type of the pre-callout</param>
		/// <remarks>This method automatically creates a backup file of callout.config.xml before saving the changes</remarks>
		public void AddCallouts(string[] entityNames, OnErrorType[] onErrors, int[] timeouts, PreCalloutType calloutType)
		{
			for(int i = 0; i < entityNames.Length; i++)
			{
				AddCallout(entityNames[i], onErrors[i], timeouts[i], calloutType);
			}
		}

		/// <summary>
		/// Adds a callout of the specified enity & callout type to the XML document
		/// </summary>
		/// <param name="entityName">Name of the entity that will use pre-callouts</param>
		/// <param name="calloutType">Specifies whether the callout is a pre-create or pre-update</param>
		/// <param name="preValues">Specifies preImageEntityXml values. Set to 'null' for no values</param>
		/// <param name="postValues">Specifies postImageEntityXml values. Set to 'null' for no values</param>
		/// <remarks>Assumes that callout.config node exists in the XML file</remarks>
		public void AddCallout(string entityName, PreCalloutType calloutType)
		{
			AddCallout(entityName, OnErrorType.Default, 0, calloutType);
		}

		/// <summary>
		/// Adds a callout of the specified enity & callout type to the XML document
		/// </summary>
		/// <param name="entityName">Name of the entity that will use pre-callouts</param>
		/// <param name="onError">Specifies the onerror Attribute of the generated subscription XML element</param>
		/// <param name="timeout">Specifies the amount of time (in seconds) that the callout is
		/// allowed to execute before the system terminates its execution by throwing an exception.
		/// The timeout value can be between 5 and 600. If the timeout is set any other value outside this
		/// allowable range, the default timeout of 60 seconds is used.</param>
		/// <param name="calloutType">Specifies whether the callout is a pre-create or pre-update</param>
		/// <remarks>Assumes that callout.config node exists in the XML file</remarks>
		public void AddCallout(string entityName, OnErrorType onError, int timeout, PreCalloutType calloutType)
		{
			// <subscription>
			string subscriptionXPath = GetSubscriptionXPath(entityName, calloutType.ToString());
			XmlNode subscriptionNode = _calloutConfigXml.SelectSingleNode(subscriptionXPath, _manager);
			
			// create <subscription> node if it does not exist
			if (subscriptionNode == null)
			{
				// <callout>
				string calloutXPath = string.Format("/{0}:callout.config/{0}:callout[@entity=\"{1}\" and @event=\"{2}\"]", 
					DEFAULT_PREFIX, entityName, calloutType.ToString());
				XmlNode calloutNode = _calloutConfigXml.SelectSingleNode(calloutXPath, _manager);

				// create <callout> node if it does not exist
				if (calloutNode == null)
				{
					// <callout.config>
					string configXPath = string.Format("/{0}:callout.config", DEFAULT_PREFIX);
					XmlNode configNode = _calloutConfigXml.SelectSingleNode(configXPath, _manager);
						
					// create <callout> element
					XmlElement calloutElement = _calloutConfigXml.CreateElement("callout", _defaultNamespace);
					calloutElement.SetAttribute("entity", entityName);
					calloutElement.SetAttribute("event", calloutType.ToString());
						
					// append <callout> element
					configNode.AppendChild(calloutElement);
						
					// set calloutNode (since <callout> now exists in the document)
					calloutNode = _calloutConfigXml.SelectSingleNode(calloutXPath, _manager);
				}

				// create <subscription> element
				XmlElement subscriptionElement = _calloutConfigXml.CreateElement("subscription", _defaultNamespace);
				subscriptionElement.SetAttribute("assembly", _subscriptionAssembly);
				subscriptionElement.SetAttribute("class", _subscriptionClass);

				// Set the onError attribute if not using the defualt value.
				if(onError != OnErrorType.Default)
					subscriptionElement.SetAttribute("onerror", onError.ToString("g").ToLower());

				// Set the timeout attribute.
				if( timeout > 4 && timeout < 601 )
					subscriptionElement.SetAttribute("timeout", timeout.ToString());

				// append <subscription> element
				calloutNode.AppendChild(subscriptionElement);
			}
		}

		/// <summary>
		/// Adds a callout of the specified enity & callout type to the XML document
		/// </summary>
		/// <param name="entityName">Name of the entity that will use pre-callouts</param>
		/// <param name="calloutType">Specifies whether the callout is a pre-create or pre-update</param>
		/// <param name="preValues">Specifies preImageEntityXml values. Set to 'null' for no values</param>
		/// <param name="postValues">Specifies postImageEntityXml values. Set to 'null' for no values</param>
		/// <remarks>Assumes that callout.config node exists in the XML file</remarks>
		public void AddCallout(string entityName, PostCalloutType calloutType, string[] preValues, string[] postValues)
		{
			AddCallout(entityName, OnErrorType.Default, 0, calloutType, preValues, postValues);
		}

		/// <summary>
		/// Adds a callout of the specified enity & callout type to the XML document
		/// </summary>
		/// <param name="entityName">Name of the entity that will use pre-callouts</param>
		/// <param name="onError">Specifies the onerror Attribute of the generated subscription XML element</param>
		/// <param name="timeout">Specifies the amount of time (in seconds) that the callout is
		/// allowed to execute before the system terminates its execution by throwing an exception.
		/// The timeout value can be between 5 and 600. If the timeout is set any other value outside this
		/// allowable range, the default timeout of 60 seconds is used.</param>
		/// <param name="calloutType">Specifies whether the callout is a pre-create or pre-update</param>
		/// <param name="preValues">Specifies preImageEntityXml values. Set to 'null' for no values</param>
		/// <param name="postValues">Specifies postImageEntityXml values. Set to 'null' for no values</param>
		/// <remarks>Assumes that callout.config node exists in the XML file</remarks>
		public void AddCallout(string entityName, OnErrorType onError, int timeout, PostCalloutType calloutType, string[] preValues, string[] postValues)
		{
			// <subscription>
			string subscriptionXPath = GetSubscriptionXPath(entityName, calloutType.ToString());
			XmlNode subscriptionNode = _calloutConfigXml.SelectSingleNode(subscriptionXPath, _manager);
			
			// create <subscription> node if it does not exist
         if (subscriptionNode == null)
         {
            // <callout>
            string calloutXPath = string.Format("/{0}:callout.config/{0}:callout[@entity=\"{1}\" and @event=\"{2}\"]", 
               DEFAULT_PREFIX, entityName, calloutType.ToString());
            XmlNode calloutNode = _calloutConfigXml.SelectSingleNode(calloutXPath, _manager);

            // create <callout> node if it does not exist
            if (calloutNode == null)
            {
               // <callout.config>
               string configXPath = string.Format("/{0}:callout.config", DEFAULT_PREFIX);
               XmlNode configNode = _calloutConfigXml.SelectSingleNode(configXPath, _manager);
						
               // create <callout> element
               XmlElement calloutElement = _calloutConfigXml.CreateElement("callout", _defaultNamespace);
               calloutElement.SetAttribute("entity", entityName);
               calloutElement.SetAttribute("event", calloutType.ToString());
						
               // append <callout> element
               configNode.AppendChild(calloutElement);
						
               // set calloutNode (since <callout> now exists in the document)
               calloutNode = _calloutConfigXml.SelectSingleNode(calloutXPath, _manager);
            }

            // create <subscription> element
            XmlElement subscriptionElement = _calloutConfigXml.CreateElement("subscription", _defaultNamespace);
            subscriptionElement.SetAttribute("assembly", _subscriptionAssembly);
            subscriptionElement.SetAttribute("class", _subscriptionClass);

            // Set the onError attribute if not set to the default value.
            if(onError != OnErrorType.Default)
               subscriptionElement.SetAttribute("onerror", onError.ToString("g").ToLower());

            // Set the timeout attribute.
            if( timeout > 4 && timeout < 601 )
               subscriptionElement.SetAttribute("timeout", timeout.ToString());

				// create and append prevalue elements
				if (preValues != null)
				{
					foreach (string preValue in preValues)
					{
						// create <prevalue> element
						XmlElement prevalueElement = _calloutConfigXml.CreateElement("prevalue", _defaultNamespace);
						prevalueElement.InnerText = preValue;

						// append <prevalue> element
						subscriptionElement.AppendChild(prevalueElement);
					}
				}

				// create and append postvalue elements
				if (postValues != null)
				{
					foreach (string postValue in postValues)
					{
						// create <postvalue> element
						XmlElement postvalueElement = _calloutConfigXml.CreateElement("postvalue", _defaultNamespace);
						postvalueElement.InnerText = postValue;

						// append <postvalue> element
						subscriptionElement.AppendChild(postvalueElement);
					}
				}

				// append <subscription> element
				calloutNode.AppendChild(subscriptionElement);
			}
		}

		#endregion

		#region Methods: Removing Callouts

		/// <summary>
		/// Used to remove callouts for specific entities from callout.config.xml
		/// </summary>
		/// <param name="entityName">Name of the entity that will no longer use pre-callouts</param>
		/// <param name="calloutType">Specifies the type of the pre-callout/param>
		/// <remarks>This method automatically creates a backup file of callout.config.xml before saving the changes</remarks>
		public void RemoveCallouts(string[] entityNames, PreCalloutType calloutType)
		{
			foreach (string entityName in entityNames)
			{
				RemoveCallout(entityName, calloutType);
			}
		}

		/// <summary>
		/// Used to remove callouts for specific entities from callout.config.xml
		/// </summary>
		/// <param name="entityName">Name of the entity that will no longer use post-callouts</param>
		/// <param name="calloutType">Specifies the type of the post-callout/param>
		/// <remarks>This method automatically creates a backup file of callout.config.xml before saving the changes</remarks>
		public void RemoveCallouts(string[] entityNames, PostCalloutType calloutType)
		{
			foreach (string entityName in entityNames)
			{
				RemoveCallout(entityName, calloutType);
			}
		}

		/// <summary>
		/// Removes a callout of the specified entity & callout type from the XML document
		/// </summary>
		/// <param name="entityName">Name of the entity that will no longer use pre-callouts</param>
		/// <param name="calloutType">Specifies the type of the pre-callout/param>
		/// <remarks>Assumes that callout.config node exists in the XML file</remarks>
		public void RemoveCallout(string entityName, PreCalloutType calloutType)
		{
			RemoveCalloutXPath(GetSubscriptionXPath(entityName, calloutType.ToString()));
		}

		/// <summary>
		/// Removes a callout of the specified entity & callout type from the XML document
		/// </summary>
		/// <param name="entityName">Name of the entity that will no longer use post-callouts</param>
		/// <param name="calloutType">Specifies the type of the post-callout/param>
		/// <remarks>Assumes that callout.config node exists in the XML file</remarks>
		public void RemoveCallout(string entityName, PostCalloutType calloutType)
		{
			RemoveCalloutXPath(GetSubscriptionXPath(entityName, calloutType.ToString()));
		}

		/// <summary>
		/// Helper function for RemoveCallout.
		/// Removes a callout of the specified entity & callout type from the XML document
		/// </summary>
		/// <param name="xpath">xml path of the entity to be removed</param>
		private void RemoveCalloutXPath(string xpath)
		{
			// retrieve the node to delete
			XmlNode node = _calloutConfigXml.SelectSingleNode(xpath, _manager);

			// null checks on the node to delete and its parent node (used to delete it's child)
			if (node != null && node.ParentNode != null)
			{
				XmlNode parent = node.ParentNode;
				parent.RemoveChild(node);

				// deletes the <callout> if there is no child node
				if (!parent.HasChildNodes)
				{
					parent.ParentNode.RemoveChild(parent);
				}
			}
		}

		#endregion

		#region Methods: Miscellaneous

		/// <summary>
		/// Gets the XPath for the subscription node of with the specified parameters in callout.config.xml
		/// </summary>
		/// <param name="entityName">Name of the entity that will either use or no longer use pre-callouts</param>
		/// <param name="calloutType">Specifies name of the calloutType</param>
		/// <returns>Returns the XPath as a string for the specified XML node</returns>
		private string GetSubscriptionXPath(string entityName, string calloutTypeName)
		{
			return string.Format("//{0}:callout.config/{0}:callout[@entity=\"{1}\" and @event=\"{2}\"]/{0}:subscription[@assembly=\"{3}\" and @class=\"{4}\"]",
				DEFAULT_PREFIX, entityName, calloutTypeName, _subscriptionAssembly, _subscriptionClass);
		}

		#endregion
	}
}