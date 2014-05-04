using System;
using System.Xml;

namespace LanaSoftCRM
{
	public class FormNavigator
	{
		// Fields
		private string _id;
		private XmlNode _element;
		private bool _isIdGuid;

		#region Properties
		public string Id
		{
			get
			{
				return _id;
			}
			set
			{
				if (IsIdGuid)
				{
					try
					{
						Guid temp = new Guid(value);
					}
					catch (FormatException)
					{
						throw;
					}

					_id = value;
				}
				else
				{
					_id = value;
				}
			}
		}

		public XmlNode Element
		{
			get
			{
				return _element;
			}
			set
			{
				_element = value;
			}
		}

		public bool IsIdGuid
		{
			get
			{
				return _isIdGuid;
			}
			set
			{
				_isIdGuid = value;
			}
		}
		#endregion

		#region Constructors
		public FormNavigator()
		{
			_isIdGuid = false;
		}

		public FormNavigator(XmlNode element) : this()
		{
			_element = element;
		}

		public FormNavigator(XmlNode element, string id) : this(element)
		{
			Id = id;
		}

		public FormNavigator(XmlNode element, string id, bool isIdGuid)
		{
			_element = element;
			_isIdGuid = IsIdGuid;

			Id = id;
		}
		#endregion
	}
}