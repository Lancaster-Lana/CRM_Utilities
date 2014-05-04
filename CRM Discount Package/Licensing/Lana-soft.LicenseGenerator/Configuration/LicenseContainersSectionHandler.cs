using System;
using System.Collections;
using System.Xml;

namespace LanaSoft.LicenseGenerator
{
	/// <summary>
	/// The license containers section handler.
	/// </summary>
	public class LicenseContainersSectionHandler : System.Configuration.IConfigurationSectionHandler
	{
		#region Private Constants
		private const string				ContainerElementName			= "Container";
		private const string				NameAttribute					= "name";
		private const string				AssemblyAttribute				= "assembly";
		private const string				ClassAttribute					= "class";
		private const string				TemplateAttribute				= "template";
		#endregion

		#region Private Stuff
		private static string GetNodeAttributeAsString(XmlNode xmlNode, string name)
		{
			if (null == xmlNode)
				throw new ArgumentNullException("xmlNode");

			if (null == xmlNode.Attributes[name])
				return string.Empty;

			if (null == xmlNode.Attributes[name].Value)
				return string.Empty;

			return Convert.ToString(xmlNode.Attributes[name].Value,
						System.Globalization.CultureInfo.CurrentCulture);
		}
		#endregion

		#region IConfigurationSectionHandler Members
		public object Create(object parent, object configContext, System.Xml.XmlNode section)
		{
			ArrayList licenseContainers = new ArrayList();
			if (null == section)
				return licenseContainers;

			XmlNodeList containerNodes = section.SelectNodes(ContainerElementName);
			for (int index = 0; index < containerNodes.Count; index++)
			{
				string name = GetNodeAttributeAsString(containerNodes[index], NameAttribute);
				string assembly = GetNodeAttributeAsString(containerNodes[index], AssemblyAttribute);
				string className = GetNodeAttributeAsString(containerNodes[index], ClassAttribute);
				string templateLicense = GetNodeAttributeAsString(containerNodes[index], TemplateAttribute);

				LicenseContainer licenseContainer = new LicenseContainer(name, assembly, className, templateLicense);
				licenseContainers.Add(licenseContainer);
			}

			return licenseContainers;
		}
		#endregion
	}
}
