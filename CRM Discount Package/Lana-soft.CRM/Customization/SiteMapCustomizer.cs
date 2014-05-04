using System;
using System.Globalization;
using System.Text;
using System.Xml;

namespace LanaSoftCRM
{
    public class SiteMapCustomizer
    {
        #region Properties

        /// <summary>
        /// The current Site Map XML
        /// </summary>
        private XmlDocument _siteMapXml;
        public XmlDocument Xml
        {
            get
            {
                return _siteMapXml;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// This class allows you to modify the Microsoft CRM 3.0 Site Map XML
        /// </summary>
        /// <param name="siteMapXml">An XML document containing the Site Map XML</param>
        public SiteMapCustomizer(XmlDocument siteMapXml)
        {
            // instantiates and initializes variables
            _siteMapXml = siteMapXml;
            _siteMapXml.PreserveWhitespace = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds an area element node to the XML document
        /// </summary>
        /// <param name="area">Represents an area element node</param>
        public void AddArea(Area area)
        {
            if (IsValid(area))
            {
                // retrieve the XPaths
                string siteMapXPath = GetSiteMapXPath();
                string areaXPath = GetAreaXPath(area.Id);

                // retrieve the element nodes
                var siteMapElement = _siteMapXml.SelectSingleNode(siteMapXPath) as XmlElement;
                var areaElement = _siteMapXml.SelectSingleNode(areaXPath) as XmlElement;

                // assume that the sitemap element node exists
                if (siteMapElement == null)
                {
                    throw new NotSupportedException("SiteMap node must exist.");
                }

                if (areaElement == null)
                {
                    // create an element node
                    areaElement = _siteMapXml.CreateElement("Area");

                    #region add attributes
                    if (!string.IsNullOrEmpty(area.Description))
                    {
                        areaElement.SetAttribute("Description", area.Description);
                    }
                    if (!string.IsNullOrEmpty(area.Icon))
                    {
                        areaElement.SetAttribute("Icon", area.Icon);
                    }
                    areaElement.SetAttribute("Id", area.Id);
                    if (area.License != LicenseTypes.None)
                    {
                        areaElement.SetAttribute("License", ConvertFlagToString(area.License));
                    }
                    if (area.ShowGroups != ShowGroupsType.None)
                    {
                        areaElement.SetAttribute("ShowGroups", Utility.GetDescriptionAttribute(area.ShowGroups));
                    }
                    areaElement.SetAttribute("Title", area.Title);
                    if (!string.IsNullOrEmpty(area.Url))
                    {
                        areaElement.SetAttribute("Url", area.Url);
                    }
                    #endregion

                    // append the area
                    siteMapElement.AppendChild(areaElement);
                }
            }
        }

        /// <summary>
        /// Adds a group element node to the XML document
        /// </summary>
        /// <param name="areaId">Unique Id attribute of the area element node</param>
        /// <param name="group">Represents a group element node</param>
        public void AddGroup(string areaId, Group group)
        {
            if (IsValid(group))
            {
                // retrieve the XPaths
                string areaXPath = GetAreaXPath(areaId);
                string groupXPath = GetGroupXPath(areaId, group.Id);

                // retrieve the element nodes
                var areaElement = _siteMapXml.SelectSingleNode(areaXPath) as XmlElement;
                var groupElement = _siteMapXml.SelectSingleNode(groupXPath) as XmlElement;

                // if the area element node does not exist, create it
                if (areaElement == null)
                {
                    // create an area struct with minimal values
                    var area = new Area();
                    area.Id = areaId;
                    area.Title = areaId;

                    // add the area
                    AddArea(area);
                    // re-retrieve the element node
                    areaElement = _siteMapXml.SelectSingleNode(areaXPath) as XmlElement;
                }

                if (groupElement == null)
                {
                    // create an element node
                    groupElement = _siteMapXml.CreateElement("Group");

                    #region add attributes
                    if (group.Description != null && group.Description.Length != 0)
                    {
                        groupElement.SetAttribute("Description", group.Description);
                    }
                    if (group.Icon != null && group.Icon.Length != 0)
                    {
                        groupElement.SetAttribute("Icon", group.Icon);
                    }
                    groupElement.SetAttribute("Id", group.Id);
                    if (group.IsProfile != IsProfileType.None)
                    {
                        groupElement.SetAttribute("IsProfile", Utility.GetDescriptionAttribute(group.IsProfile));
                    }
                    if (group.License != LicenseTypes.None)
                    {
                        groupElement.SetAttribute("License", ConvertFlagToString(group.License));
                    }
                    groupElement.SetAttribute("Title", group.Title);
                    if (group.Url != null && group.Url.Length != 0)
                    {
                        groupElement.SetAttribute("Url", group.Url);
                    }
                    #endregion

                    // append the group
                    if (areaElement != null) areaElement.AppendChild(groupElement);
                }
            }
            else
            {
                throw new ArgumentException("Group has invalid values.");
            }
        }

        /// <summary>
        /// Adds a sub-area element node to the XML document
        /// </summary>
        /// <param name="areaId">Unique Id attribute of the area element node</param>
        /// <param name="groupId">Unique Id attribute of the group element node</param>
        /// <param name="subArea">Represents a sub-area element node</param>
        public void AddSubArea(string areaId, string groupId, SubArea subArea)
        {
            if (IsValid(subArea))
            {
                // retrieve the XPaths
                string groupXPath = GetGroupXPath(areaId, groupId);
                string subAreaXPath = GetSubAreaXPath(areaId, groupId, subArea.Id);

                // retrieve the element nodes
                var groupElement = _siteMapXml.SelectSingleNode(groupXPath) as XmlElement;
                var subAreaElement = _siteMapXml.SelectSingleNode(subAreaXPath) as XmlElement;

                // if the group element node does not exist, create it
                if (groupElement == null)
                {
                    // create a group struct with minimal values
                    var group = new Group();
                    group.Id = groupId;
                    group.Title = groupId;

                    // add the group
                    AddGroup(areaId, group);

                    // re-retrieve the element node
                    groupElement = _siteMapXml.SelectSingleNode(groupXPath) as XmlElement;
                }

                // if the area element node does not exist, create it
                if (subAreaElement == null)
                {
                    // create an element node
                    subAreaElement = _siteMapXml.CreateElement("SubArea");

                    #region add attributes
                    if (subArea.AvailableOffline != AvailableOfflineType.None)
                    {
                        subAreaElement.SetAttribute("AvailableOffline", Utility.GetDescriptionAttribute(subArea.AvailableOffline));
                    }
                    if (subArea.Client != ClientTypes.None)
                    {
                        subAreaElement.SetAttribute("Client", subArea.Client.ToString(CultureInfo.InvariantCulture));
                    }
                    if (!string.IsNullOrEmpty(subArea.Description))
                    {
                        subAreaElement.SetAttribute("Description", subArea.Description);
                    }
                    if (!string.IsNullOrEmpty(subArea.Entity))
                    {
                        subAreaElement.SetAttribute("Entity", subArea.Entity);
                    }
                    if (!string.IsNullOrEmpty(subArea.Icon))
                    {
                        subAreaElement.SetAttribute("Icon", subArea.Icon);
                    }
                    subAreaElement.SetAttribute("Id", subArea.Id);	// IsValid() already checked
                    if (subArea.License != LicenseTypes.None)
                    {
                        subAreaElement.SetAttribute("License", ConvertFlagToString(subArea.License));
                    }
                    if (!string.IsNullOrEmpty(subArea.OutlookShortcutIcon))
                    {
                        subAreaElement.SetAttribute("OutlookShortcutIcon", subArea.OutlookShortcutIcon);
                    }
                    subAreaElement.SetAttribute("Title", subArea.Title);	// IsValid() already checked
                    if (!string.IsNullOrEmpty(subArea.Url))
                    {
                        subAreaElement.SetAttribute("Url", subArea.Url);
                    }
                    #endregion

                    // append the sub-area
                    if (groupElement != null) groupElement.AppendChild(subAreaElement);
                }
            }
            else
            {
                throw new ArgumentException("SubArea has invalid values.");
            }
        }

        // <summary>
        /// Adds a privilege element node to the XML document
        /// </summary>
        /// <param name="areaId">Unique Id attribute of the area element node</param>
        /// <param name="groupId">Unique Id attribute of the group element node</param>
        /// <param name="subArea">Represents a sub-area element node</param>
        /// <param name="privilege">Represents a new PrivilegeNode object</param>
        public void AddPrivilegeNode(string areaId, string groupId, SubArea subArea, PrivilegeNode privilege)
        {
            if (IsValid(privilege))
            {
                // retrieve the XPaths				
                string subAreaXPath = GetSubAreaXPath(areaId, groupId, subArea.Id);
                string privilegeXPath = GetPrivilegeXPath(areaId, groupId, subArea.Id, privilege.EntityName);

                // retrieve the subArea element			
                XmlElement subAreaElement = _siteMapXml.SelectSingleNode(subAreaXPath) as XmlElement;

                // if the area element node does not exist, do not add the privilege
                if (subAreaElement != null)
                {
                    var privilegeElement = _siteMapXml.SelectSingleNode(privilegeXPath) as XmlElement;

                    // if the privilege does not already exist, add it
                    if (privilegeElement == null)
                    {
                        privilegeElement = _siteMapXml.CreateElement("Privilege");
                        privilegeElement.SetAttribute("Entity", privilege.EntityName);
                        privilegeElement.SetAttribute("Privilege", ConvertFlagToString(privilege.Privileges));

                        // append the privilege to the subarea
                        subAreaElement.AppendChild(privilegeElement);
                    }
                }
                else
                {
                    throw new ArgumentException("SubArea does not exist.");
                }
            }
            else
            {
                throw new ArgumentException("Privilege has invalid values.");
            }
        }

        /// <summary>
        /// Removes an area element node to the XML document
        /// </summary>
        /// <param name="areaId">Unique Id attribute of the area element node</param>
        public void RemoveArea(string areaId)
        {
            // get XPaths
            string siteMapXPath = GetSiteMapXPath();
            string areaXPath = GetAreaXPath(areaId);

            // retrieve the XML element nodes
            var siteMapElement = _siteMapXml.SelectSingleNode(siteMapXPath) as XmlElement;
            var areaElement = _siteMapXml.SelectSingleNode(areaXPath) as XmlElement;

            if (siteMapElement != null && areaElement != null)
            {
                siteMapElement.RemoveChild(areaElement);
            }
        }

        /// <summary>
        /// Removes a group element node to the XML document
        /// </summary>
        /// <param name="areaId">Unique Id attribute of the area element node</param>
        /// <param name="groupId">Unique Id attribute of the group element node</param>
        public void RemoveGroup(string areaId, string groupId)
        {
            // get XPaths
            string areaXPath = GetAreaXPath(areaId);
            string groupXPath = GetGroupXPath(areaId, groupId);

            // retrieve the XML element nodes
            var areaElement = _siteMapXml.SelectSingleNode(areaXPath) as XmlElement;
            var groupElement = _siteMapXml.SelectSingleNode(groupXPath) as XmlElement;

            if (areaElement != null && groupElement != null)
            {
                areaElement.RemoveChild(groupElement);
            }
        }

        /// <summary>
        /// Removes a sub-area element node to the XML document
        /// </summary>
        /// <param name="areaId">Unique Id attribute of the area element node</param>
        /// <param name="groupId">Unique Id attribute of the group element node</param>
        /// <param name="subAreaId">Unique Id attribute of the sub-area element node</param>
        public void RemoveSubArea(string areaId, string groupId, string subAreaId)
        {
            // get XPaths
            string groupXPath = GetGroupXPath(areaId, groupId);
            string subAreaXPath = GetSubAreaXPath(areaId, groupId, subAreaId);

            // retrieve the XML element nodes
            var groupElement = _siteMapXml.SelectSingleNode(groupXPath) as XmlElement;
            var subAreaElement = _siteMapXml.SelectSingleNode(subAreaXPath) as XmlElement;

            if (groupElement != null && subAreaElement != null)
            {
                groupElement.RemoveChild(subAreaElement);
            }
        }

        #endregion

        #region Methods: Helper

        /// <summary>
        /// Gets the XPath to the sitemap element node
        /// </summary>
        /// <returns>Returns the XPath as a string</returns>
        private string GetSiteMapXPath()
        {
            return "//ImportExportXml/SiteMap/SiteMap";
        }

        /// <summary>
        /// Gets the XPath to the area element node
        /// </summary>
        /// <param name="areaId">Unique Id attribute of the area element node</param>
        /// <returns>Returns the XPath as a string</returns>
        private string GetAreaXPath(string areaId)
        {
            // check that the area Id is alpha-numeric
            if (Utility.IsAlphaNumeric(areaId))
            {
                string siteMapXPath = GetSiteMapXPath();
                return string.Format("{0}/Area[@Id=\"{1}\"]", siteMapXPath, areaId);
            }
            throw new ArgumentException("Invalid Id.");
        }

        /// <summary>
        /// Gets the XPath to the group element node
        /// </summary>
        /// <param name="areaId">Unique Id attribute of the area element node</param>
        /// <param name="groupId">Unique Id attribute of the group element node</param>
        /// <returns>Returns the XPath as a string</returns>
        private string GetGroupXPath(string areaId, string groupId)
        {
            // check that the group Id is alpha-numeric
            if (Utility.IsAlphaNumeric(groupId))
            {
                string areaXPath = GetAreaXPath(areaId);
                return string.Format("{0}/Group[@Id=\"{1}\"]", areaXPath, groupId);
            }
            throw new ArgumentException("Invalid Id.");
        }

        /// <summary>
        /// Gets the XPath to the subArea element node
        /// </summary>
        /// <param name="areaId">Unique Id attribute of the area element node</param>
        /// <param name="groupId">Unique Id attribute of the group element node</param>
        /// <param name="subAreaId">Unique Id attribute of the sub-area element node</param>
        /// <returns>Returns the XPath as a string</returns>
        private string GetSubAreaXPath(string areaId, string groupId, string subAreaId)
        {
            // check that the sub-area Id is alpha-numeric
            if (Utility.IsAlphaNumeric(subAreaId))
            {
                string groupXPath = GetGroupXPath(areaId, groupId);
                return string.Format("{0}/SubArea[@Id=\"{1}\"]", groupXPath, subAreaId);
            }
            throw new ArgumentException("Invalid Id.");
        }

        /// <summary>
        /// Gets the XPath to the privilege element node
        /// </summary>
        /// <param name="areaId">Unique Id attribute of the area element node</param>
        /// <param name="groupId">Unique Id attribute of the group element node</param>
        /// <param name="subAreaId">Unique Id attribute of the sub-area element node</param>
        /// <param name="entityName">Name of the entity that the privilege is checking for</param>
        /// <returns>Returns the XPath as a string</returns>
        private string GetPrivilegeXPath(string areaId, string groupId, string subAreaId, string entityName)
        {
            // check that the entityName is alpha-numeric
            if (Utility.IsAlphaNumeric(entityName))
            {
                string subAreaXPath = GetSubAreaXPath(areaId, groupId, subAreaId);
                return string.Format("{0}/Privilege[@Entity=\"{1}\"]", subAreaXPath, entityName);
            }
            throw new ArgumentException("Invalid EntityName.");
        }

        /// <summary>
        /// Checks to make sure the Area struct is valid
        /// </summary>
        /// <param name="area">Represents an area element node</param>
        /// <returns>Returns true if the struct is valid, otherwise false</returns>
        private bool IsValid(Area area)
        {
            // if there is no Id or no Title, the area is not valid
            if (string.IsNullOrEmpty(area.Id) ||
                string.IsNullOrEmpty(area.Title))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks to make sure the Group struct is valid
        /// </summary>
        /// <param name="group">Represents a group element node</param>
        /// <returns>Returns true if the struct is valid, otherwise false</returns>
        private bool IsValid(Group group)
        {
            // if there is no Id or no Title, the group is not valid
            if (string.IsNullOrEmpty(group.Id) ||
                string.IsNullOrEmpty(group.Title))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks to make sure the SubArea struct is valid
        /// </summary>
        /// <param name="subArea">Represents a sub-area element node</param>
        /// <returns>Returns true if the struct is valid, otherwise false</returns>
        private bool IsValid(SubArea subArea)
        {
            // if there is no Id or no Title, the sub-area is not valid
            if (string.IsNullOrEmpty(subArea.Id) ||
                string.IsNullOrEmpty(subArea.Title))
            {
                // if there is no client type or if it's only web, the setting cannot be available offline
                if (subArea.Client == ClientTypes.Web || subArea.Client == ClientTypes.None)
                {
                    if (subArea.AvailableOffline == AvailableOfflineType.True)
                    {
                        return false;
                    }
                    return true;
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks to make sure the privilege struct is valid
        /// </summary>
        /// <param name="privilegeNode">Represents a privilege element node</param>
        /// <returns>Returns true if the struct is valid, otherwise false</returns>
        private bool IsValid(PrivilegeNode privilegeNode)
        {
            // if there is no EntityName, the group is not valid
            return !string.IsNullOrEmpty(privilegeNode.EntityName);
        }

        /// <summary>
        /// Converts the client flag enum into a comma separated XML attribute value
        /// </summary>
        /// <param name="client"></param>
        /// <returns>Returns the XML attribute value as a string</returns>
        private string ConvertFlagToString(ClientTypes client)
        {
            if (client == ClientTypes.All)
            {
                return string.Format("{0},{1},{2},{3}",
                    ClientTypes.Outlook.ToString(CultureInfo.InvariantCulture),
                    ClientTypes.OutlookLaptopClient.ToString(CultureInfo.InvariantCulture),
                    ClientTypes.OutlookWorkstationClient.ToString(CultureInfo.InvariantCulture),
                    ClientTypes.Web.ToString(CultureInfo.InvariantCulture));
            }
            if (client == ClientTypes.None)
            {
                return string.Empty;
            }
            var output = new StringBuilder();

            if ((client & ClientTypes.Outlook) == ClientTypes.Outlook)
            {
                output.Append(ClientTypes.Outlook.ToString(CultureInfo.InvariantCulture));
            }
            if ((client & ClientTypes.OutlookLaptopClient) == ClientTypes.OutlookLaptopClient)
            {
                if (output.Length != 0)
                {
                    output.Append(',');
                }
                output.Append(ClientTypes.OutlookLaptopClient.ToString(CultureInfo.InvariantCulture));
            }
            if ((client & ClientTypes.OutlookWorkstationClient) == ClientTypes.OutlookWorkstationClient)
            {
                if (output.Length != 0)
                {
                    output.Append(',');
                }
                output.Append(ClientTypes.OutlookWorkstationClient.ToString(CultureInfo.InvariantCulture));
            }
            if ((client & ClientTypes.Web) == ClientTypes.Web)
            {
                if (output.Length != 0)
                {
                    output.Append(',');
                }
                output.Append(ClientTypes.Web.ToString(CultureInfo.InvariantCulture));
            }

            return output.ToString();
        }

        /// <summary>
        /// Converts the license flag enum into a comma separated XML attribute value 
        /// </summary>
        /// <param name="license"></param>
        /// <returns>Returns the XML attribute value as a string</returns>
        private string ConvertFlagToString(LicenseTypes license)
        {
            if (license == LicenseTypes.All)
            {
                return string.Format("{0},{1}",
                    LicenseTypes.Professional.ToString(CultureInfo.InvariantCulture),
                    LicenseTypes.SmallBusiness.ToString(CultureInfo.InvariantCulture));
            }
            else if (license == LicenseTypes.None)
            {
                return string.Empty;
            }
            else
            {
                var output = new StringBuilder();

                if ((license & LicenseTypes.Professional) == LicenseTypes.Professional)
                {
                    output.Append(LicenseTypes.Professional.ToString(CultureInfo.InvariantCulture));
                }
                if ((license & LicenseTypes.SmallBusiness) == LicenseTypes.SmallBusiness)
                {
                    if (output.Length != 0)
                    {
                        output.Append(',');
                    }
                    output.Append(LicenseTypes.SmallBusiness.ToString(CultureInfo.InvariantCulture));
                }

                return output.ToString();
            }
        }

        /// <summary>
        /// Converts the privilege flag enum into a comma separated XML attribute value
        /// </summary>
        /// <param name="privilege"></param>
        /// <returns>Returns the XML attribute value as a string</returns>
        private string ConvertFlagToString(PrivilegeTypes privilege)
        {
            if (privilege == PrivilegeTypes.All)
            {
                return string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                    PrivilegeTypes.Append.ToString(CultureInfo.InvariantCulture),
                    PrivilegeTypes.AppendTo.ToString(CultureInfo.InvariantCulture),
                    PrivilegeTypes.Assign.ToString(CultureInfo.InvariantCulture),
                    PrivilegeTypes.Create.ToString(CultureInfo.InvariantCulture),
                    PrivilegeTypes.Delete.ToString(CultureInfo.InvariantCulture),
                    PrivilegeTypes.Read.ToString(CultureInfo.InvariantCulture),
                    PrivilegeTypes.Share.ToString(CultureInfo.InvariantCulture),
                    PrivilegeTypes.Write.ToString(CultureInfo.InvariantCulture));
            }
            if (privilege == PrivilegeTypes.None)
            {
                return string.Empty;
            }
            var output = new StringBuilder();

            if ((privilege & PrivilegeTypes.Append) == PrivilegeTypes.Append)
            {
                output.Append(PrivilegeTypes.Append.ToString(CultureInfo.InvariantCulture));
            }
            if ((privilege & PrivilegeTypes.AppendTo) == PrivilegeTypes.AppendTo)
            {
                if (output.Length != 0)
                {
                    output.Append(',');
                }
                output.Append(PrivilegeTypes.AppendTo.ToString(CultureInfo.InvariantCulture));
            }
            if ((privilege & PrivilegeTypes.Assign) == PrivilegeTypes.Assign)
            {
                if (output.Length != 0)
                {
                    output.Append(',');
                }
                output.Append(PrivilegeTypes.Assign.ToString(CultureInfo.InvariantCulture));
            }
            if ((privilege & PrivilegeTypes.Create) == PrivilegeTypes.Create)
            {
                if (output.Length != 0)
                {
                    output.Append(',');
                }
                output.Append(PrivilegeTypes.Create.ToString(CultureInfo.InvariantCulture));
            }
            if ((privilege & PrivilegeTypes.Delete) == PrivilegeTypes.Delete)
            {
                if (output.Length != 0)
                {
                    output.Append(',');
                }
                output.Append(PrivilegeTypes.Delete.ToString(CultureInfo.InvariantCulture));
            }
            if ((privilege & PrivilegeTypes.Read) == PrivilegeTypes.Read)
            {
                if (output.Length != 0)
                {
                    output.Append(',');
                }
                output.Append(PrivilegeTypes.Read.ToString(CultureInfo.InvariantCulture));
            }
            if ((privilege & PrivilegeTypes.Share) == PrivilegeTypes.Share)
            {
                if (output.Length != 0)
                {
                    output.Append(',');
                }
                output.Append(PrivilegeTypes.Share.ToString(CultureInfo.InvariantCulture));
            }
            if ((privilege & PrivilegeTypes.Write) == PrivilegeTypes.Write)
            {
                if (output.Length != 0)
                {
                    output.Append(',');
                }
                output.Append(PrivilegeTypes.Write.ToString(CultureInfo.InvariantCulture));
            }

            return output.ToString();
        }

        #endregion
    }
}