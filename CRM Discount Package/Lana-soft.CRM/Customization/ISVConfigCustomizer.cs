using System.Xml;
using System.Globalization;

namespace LanaSoftCRM
{
    /// <summary>
    /// This class provides a series of helper methods that can be used to modify ISV.Config XML that is contained
    /// within a Microsoft CRM 3.0 "Customizations" XML file.
    /// 
    /// In general, the methods are smart enough to build the required XML structure required by the method.  For example
    /// if you attempt to add a global toolbar button and the global toolbar is not yet defined in the XML schema, it will
    /// be added and then the button will be added to it.
    /// </summary>
    public class IsvConfigCustomizer
    {
        private XmlDocument _importExportXml;
        private XmlDocument _isvConfigXml;

        /// <summary>
        /// Returns the Import Export XML document
        /// </summary>
        public XmlDocument Xml
        {
            get
            {
                XmlNode child = _importExportXml.SelectSingleNode("/ImportExportXml/IsvConfig");
                XmlNode parent = _importExportXml.SelectSingleNode("/ImportExportXml");

                XmlCDataSection data = _importExportXml.CreateCDataSection(_isvConfigXml.OuterXml);
                XmlNode newChild = _importExportXml.CreateElement("IsvConfig");
                newChild.AppendChild(data);

                if (parent != null) parent.ReplaceChild(newChild, child);	// assumes that IsvConfig node exists

                return _importExportXml;
            }
        }

        public IsvConfigCustomizer(XmlDocument importExportXml)
        {
            _importExportXml = importExportXml;

            // Find the "ISV Config" Node and load it.  Since the Isv Config
            // "XML" is stored in a CDATA we have to load it seperately to access it
            XmlNode tmp = _importExportXml.SelectSingleNode("/ImportExportXml/IsvConfig");

            _isvConfigXml = new XmlDocument();
            _isvConfigXml.LoadXml(tmp.InnerText);

            this.GetConfigurationNode();
        }


        #region Global Menu Bar / ToolBar Methods

        /// <summary>
        /// Add a new button to the global toolbar
        /// </summary>
        /// <param name="button">The button to add</param>
        public void AddGlobalToolBarButton(ToolBarButton button)
        {
            // Get the global toolbar and ensure it exists
            XmlNode toolBar = this.GetGlobalToolBar();

            // Add the button
            this.AddButtonToToolBar(toolBar, button);
        }

        /// <summary>
        /// Add a new "spacer" to the global tool bar
        /// </summary>
        public void AddGlobalToolBarSpacer()
        {
            // Get the global toolbar and ensure it exists
            XmlNode toolBar = this.GetGlobalToolBar();

            // Add the spacer
            this.AddSpacerToToolBar(toolBar);
        }

        /// <summary>
        /// Add a new Menu & Menu Item to the global menu bar.  If the specified menu already exists, the item will be
        /// added to it.  If the specified menu does not exist, the menu will be added and then the item will be added.
        /// </summary>
        /// <param name="menuTitle">The display title of the menu - This is used as both the user viewable title of the
        /// menu and the key by which menu items are added to menus.</param>
        /// <param name="menuItem">The MenuItem to add</param>
        public void AddGlobalMenuItem(string menuTitle, MenuItem menuItem)
        {
            // Get the global menu bar and ensure it exists
            XmlNode menuBar = this.GetGlobalMenuBar();

            // Add the menu item
            this.AddMenuItemToMenuBarMenu(menuBar, menuTitle, menuItem);
        }

        /// <summary>
        /// Add a new Menu & Spacer to the global menu bar.  If the specified menu already exists, the Spacer will be
        /// added to it.  If the specified menu does not exist, the menu will be added and then the Spacer will be added.
        /// </summary>
        /// <param name="menuTitle">The display title of the menu - This is used as both the user viewable title of the
        /// menu and the key by which spacers are added to menus.</param>
        public void AddGlobalMenuSpacer(string menuTitle)
        {
            // Get the global menu bar and ensure it exists
            XmlNode menuBar = this.GetGlobalMenuBar();

            // Add the spacer
            this.AddMenuSpacerToMenuBarMenu(menuBar, menuTitle);
        }

        #endregion

        #region Grid Menu / ToolBar Methods

        /// <summary>
        /// Add a new button to the grid toolbar for an entity
        /// </summary>
        /// <param name="entityName">The name of the entity whose grid toolbar you would like to modify</param>
        /// <param name="button">The button to add</param>
        public void AddEntityGridToolBarButton(string entityName, ToolBarButton button)
        {
            // Get the entity grid toolbar and ensure it exists
            XmlNode toolBar = this.GetEntityGridToolBar(entityName);

            // Add the button
            this.AddButtonToToolBar(toolBar, button);
        }

        /// <summary>
        /// Add a new "spacer" to the grid toolbar for an entity
        /// </summary>
        /// <param name="entityName">The name of the entity whose grid toolbar you would like to modify</param>
        public void AddEntityGridToolBarSpacer(string entityName)
        {
            // Get the entity grid toolbar and ensure it exists
            XmlNode toolBar = this.GetEntityGridToolBar(entityName);

            // Add the spacer
            this.AddSpacerToToolBar(toolBar);
        }

        /// <summary>
        /// Add a new Menu Item to the Actions Menu of an entity grid.
        /// </summary>
        /// <param name="entityName">The name of the entity whose grid Actions Menu you would like to modify</param>
        /// <param name="menuItem">The MenuItem to add</param>
        public void AddEntityGridActionMenuItem(string entityName, MenuItem menuItem)
        {
            // Get the entity grid actions menu and ensure it exists
            XmlNode menuBar = this.GetEntityActionsMenuBar(entityName);

            // Add the menu item
            this.AddMenuItemToMenuBarMenu(menuBar, null, menuItem);
        }

        /// <summary>
        /// Add a new Spacer to the Actions Menu of an entity grid.
        /// </summary>
        /// <param name="entityName">The name of the entity whose grid Actions Menu you would like to modify</param>
        public void AddEntityGridActionMenuSpacer(string entityName)
        {
            // Get the entity grid actions menu and ensure it exists
            XmlNode menuBar = this.GetEntityActionsMenuBar(entityName);

            // Add the spacer
            this.AddMenuSpacerToMenuBarMenu(menuBar, null);
        }

        #endregion

        #region Entity Menu / Tool Bar Methods

        /// <summary>
        /// Add a new button to the detail form toolbar of an entity
        /// </summary>
        /// <param name="entityName">The name of the entity whose toolbar you would like to modify</param>
        /// <param name="button">The button to add</param>
        public void AddEntityToolBarButton(string entityName, ToolBarButton button)
        {
            // Get the entity's toolbar and ensure it exists
            XmlNode toolBar = this.GetEntityToolBar(entityName);
            // Add the button
            this.AddButtonToToolBar(toolBar, button);
        }


        public void AddEntityToolBarButton(string entityName, ToolBarButton button, bool CheckByTitle, bool CheckByJavaScript)
        {
            // Get the entity's toolbar and ensure it exists
            XmlNode toolBar = this.GetEntityToolBar(entityName);

            bool add = true;
            if ((CheckByTitle) && (toolBar.SelectSingleNode("Button[@Title=\"" + button.Title + "\"]") != null)
               || (CheckByJavaScript) && (toolBar.SelectSingleNode("Button[@JavaScript=\"" + button.JavaScript + "\"]") != null))
                add = false;

            if (add)
            {
                // Add the button
                this.AddButtonToToolBar(toolBar, button);
            }
        }

        /// <summary>
        /// Add a new "spacer" to the detail form toolbar of an entity
        /// </summary>
        /// <param name="entityName">The name of the entity whose toolbar you would like to modify</param>
        public void AddEntityToolBarSpacer(string entityName)
        {
            // Get the entity's toolbar and ensure it exists
            XmlNode toolBar = this.GetEntityToolBar(entityName);

            // Add the spacer
            this.AddSpacerToToolBar(toolBar);
        }

        /// <summary>
        /// Add a new "Left Navigation Item" to the detail form of an entity
        /// </summary>
        /// <param name="entityName">The name of the entity whose left navbar you would like to modify</param>
        /// <param name="navItem">The LeftNavigationItem to add</param>
        public void AddEntityLeftNavigationItem(string entityName, IsvLeftNavigationItem navItem)
        {
            // Get the entity's left navbar and ensure that it exists
            XmlNode navBar = this.GetEntityNavBar(entityName);

            // Create the left navigation item xml
            XmlElement newElement = _isvConfigXml.CreateElement("NavBarItem");
            newElement.SetAttribute("Title", navItem.Title);
            newElement.SetAttribute("Icon", navItem.Icon);
            newElement.SetAttribute("Url", navItem.Url);
            newElement.SetAttribute("Id", navItem.Id);

            // Add the element to the navbar
            navBar.AppendChild(newElement);
        }

        /// <summary>
        /// Add a new Menu & Menu Item to the menu bar on the detail form of an entity.  If the specified menu already
        /// exists, the item will be added to it.  If the specified menu does not exist, the menu will be added and
        /// then the item will be added.
        /// </summary>
        /// <param name="entityName">The name of the entity whose menu bar you would like to modify</param>
        /// <param name="menuTitle">The display title of the menu - This is used as both the user viewable title of the
        /// menu and the key by which menu items are added to menus.</param>
        /// <param name="menuItem">The MenuItem to add</param>
        public void AddEntityMenuItem(string entityName, string menuTitle, MenuItem menuItem)
        {
            // Get the entity's menu bar and ensure that it exists
            XmlNode menuBar = this.GetEntityMenuBar(entityName);

            // Add the menu item
            this.AddMenuItemToMenuBarMenu(menuBar, menuTitle, menuItem);
        }

        /// <summary>
        /// Add a new Menu & Spacer to the menu bar on the detail form of an entity.  If the specified menu already
        /// exists, the spacer will be added to it.  If the specified menu does not exist, the menu will be added and
        /// then the spacer will be added.
        /// </summary>
        /// <param name="entityName">The name of the entity whose menu bar you would like to modify</param>
        /// <param name="menuTitle">The display title of the menu - This is used as both the user viewable title of the
        /// menu and the key by which menu items are added to menus.</param>
        public void AddEntityMenuSpacer(string entityName, string menuTitle)
        {
            // Get the entity's menu bar and ensure that it exists
            XmlNode menuBar = this.GetEntityMenuBar(entityName);

            // Add the spacer
            this.AddMenuSpacerToMenuBarMenu(menuBar, menuTitle);
        }

        #endregion

        #region Private Methods

        #region Add Helper Methods

        /// <summary>
        /// Adds a button to a toolbar
        /// </summary>
        /// <param name="toolBar">The toolbar node to add the button to</param>
        /// <param name="button">The button to add</param>
        private void AddButtonToToolBar(XmlNode toolBar, ToolBarButton button)
        {
            // Create the button
            XmlElement buttonElement = _isvConfigXml.CreateElement("Button");

            // "Hydrate" it...
            AddOptionalAttributeToElement(buttonElement, "Title", button.Title);
            AddOptionalAttributeToElement(buttonElement, "AccessKey", button.AccessHotKey.ToString(CultureInfo.InvariantCulture));
            AddOptionalAttributeToElement(buttonElement, "ToolTip", button.ToolTip);
            AddOptionalAttributeToElement(buttonElement, "Icon", button.Icon);

            if (button.Url != null && button.Url.Length > 0 &&
                button.JavaScript != null && button.JavaScript.Length > 0)
            {
                throw new System.ArgumentException("Url and JavaScript are mutually exclusive - Only one may be set for a button.");
            }

            //AddOptionalAttributeToElement(buttonElement, "Id", button.Id);
            AddOptionalAttributeToElement(buttonElement, "Url", button.Url);
            AddOptionalAttributeToElement(buttonElement, "JavaScript", button.JavaScript);
            AddOptionalAttributeToElement(buttonElement, "WinMode", button.WindowLaunchMode);
            AddOptionalAttributeToElement(buttonElement, "Client", button.SupportedClients);
            AddOptionalAttributeToElement(buttonElement, "PassParams", button.PassParameters);
            AddOptionalAttributeToElement(buttonElement, "AvailableOffline", button.PassParameters);
            AddOptionalAttributeToElement(buttonElement, "ValidForCreate", button.PassParameters);
            AddOptionalAttributeToElement(buttonElement, "ValidForUpdate", button.PassParameters);

            toolBar.AppendChild(buttonElement);
        }

        private void AddRequiredAttributeToElement(XmlElement element, string name, string value)
        {
            // Make sure the value is set, if not we will throw
            if (value != null && value.Length > 0)
            {
                throw new System.ArgumentNullException(name, "This property is required by the ISV.Config Schema");
            }
            else // It looks good enough, add it...
            {
                element.SetAttribute(name, value);
            }
        }

        private void AddOptionalAttributeToElement(XmlElement element, string name, string value)
        {
            // If the value hasn't been set, don't add an empty attribute
            if (value != null && value.Length > 0)
            {
                element.SetAttribute(name, value);
            }
        }

        private void AddOptionalAttributeToElement(XmlElement element, string name, ConfigurationBoolean value)
        {
            // If the value hasn't been set, don't add an empty attribute
            if (value != ConfigurationBoolean.Undefined)
            {
                // TODO: Get the correct ToString here
                element.SetAttribute(name, value.ToString());
            }
        }

        private void AddOptionalAttributeToElement(XmlElement element, string name, WindowMode value)
        {
            // If the value hasn't been set, don't add an empty attribute
            if (value != WindowMode.Undefined)
            {
                // TODO: Get the correct ToString here
                element.SetAttribute(name, value.ToString());
            }
        }

        private void AddOptionalAttributeToElement(XmlElement element, string name, ClientTypes value)
        {
            // If the value hasn't been set, don't add an empty attribute
            if (value != ClientTypes.Undefined)
            {
                // TODO: Implement the | (or'ing)
            }
        }


        /// <summary>
        /// Adds a spacer to a toolbar
        /// </summary>
        /// <param name="toolBar">The toolbar node to add the spacer to</param>
        private void AddSpacerToToolBar(XmlNode toolBar)
        {
            XmlElement newElement = _isvConfigXml.CreateElement("ToolBarSpacer");
            toolBar.AppendChild(newElement);
        }

        private void AddMenuItemToMenuBarMenu(XmlNode menuBar, string menuTitle, MenuItem menuItem)
        {
            XmlNode tmpNode;

            // This is an "Action Menu Item", add directly to it...
            if (menuTitle == null)
            {
                tmpNode = menuBar;
            }
            else
            {
                tmpNode = this.AddMenuToMenuBar(menuBar, menuTitle);
            }

            XmlElement newItem = _isvConfigXml.CreateElement("MenuItem");
            newItem.SetAttribute("Title", menuItem.Title);

            tmpNode.AppendChild(newItem);
        }

        private void AddMenuSpacerToMenuBarMenu(XmlNode menuBar, string menuTitle)
        {
            XmlNode tmpNode;

            // This is an "Action Menu Item", add directly to it...
            if (menuTitle == null)
            {
                tmpNode = menuBar;
            }
            else
            {
                tmpNode = this.AddMenuToMenuBar(menuBar, menuTitle);
            }

            XmlElement newItem = _isvConfigXml.CreateElement("MenuSpacer");

            tmpNode.AppendChild(newItem);
        }

        private XmlNode AddMenuToMenuBar(XmlNode menuBar, string menuTitle)
        {
            XmlNode tmpNode = menuBar.SelectSingleNode("Menu[@Title='" + menuTitle + "']");

            if (tmpNode == null)
            {
                XmlElement newElement = _isvConfigXml.CreateElement("Menu");

                newElement.SetAttribute("Title", menuTitle);

                menuBar.AppendChild(newElement);

                tmpNode = menuBar.SelectSingleNode("Menu[@Title='" + menuTitle + "']");
            }

            return tmpNode;
        }

        #endregion

        #region Node Helpers - Useful methods to find/create various nodes in the ISV.Config Schema

        private XmlNode GetGlobalToolBar()
        {
            XmlNode root = GetRootNode();

            XmlNode tmpNode = root.SelectSingleNode("ToolBar");

            if (tmpNode == null)
            {
                XmlElement newElement = _isvConfigXml.CreateElement("ToolBar");

                root.AppendChild(newElement);

                tmpNode = root.SelectSingleNode("ToolBar");
            }

            return tmpNode;
        }

        private XmlNode GetGlobalMenuBar()
        {
            XmlNode root = this.GetRootNode();
            return this.GetMenuBar(root);
        }

        private XmlNode GetEntityToolBar(string entityName)
        {
            XmlNode root = this.GetEntityNode(entityName);

            XmlNode tmpNode = root.SelectSingleNode("ToolBar");

            if (tmpNode == null)
            {
                XmlElement newElement = _isvConfigXml.CreateElement("ToolBar");
                root.AppendChild(newElement);
                tmpNode = root.SelectSingleNode("ToolBar");
            }

            return tmpNode;
        }

        private XmlNode GetEntityGridToolBar(string entityName)
        {
            XmlNode root = this.GetEntityGridNode(entityName);

            XmlNode tmpNode = root.SelectSingleNode("MenuBar");

            if (tmpNode == null)
            {
                XmlElement newElement = _isvConfigXml.CreateElement("MenuBar");
                root.AppendChild(newElement);

                tmpNode = root.SelectSingleNode("MenuBar");
            }

            XmlNode buttonsNode = tmpNode.SelectSingleNode("Buttons");

            if (buttonsNode == null)
            {
                XmlElement newElement = _isvConfigXml.CreateElement("Buttons");
                tmpNode.AppendChild(newElement);
                buttonsNode = tmpNode.SelectSingleNode("Buttons");
            }

            return buttonsNode;
        }

        private XmlNode GetEntityMenuBar(string entityName)
        {
            XmlNode root = this.GetEntityNode(entityName);
            return this.GetMenuBar(root);
        }

        private XmlNode GetEntityActionsMenuBar(string entityName)
        {
            XmlNode tmpNode = this.GetEntityGridNode(entityName);

            return this.GetActionMenuBar(tmpNode);
        }

        private XmlNode GetEntityGridNode(string entityName)
        {
            XmlNode root = this.GetEntityNode(entityName);
            XmlNode tmpNode = root.SelectSingleNode("Grid");

            if (tmpNode == null)
            {
                XmlElement newElement = _isvConfigXml.CreateElement("Grid");
                root.AppendChild(newElement);
                tmpNode = root.SelectSingleNode("Grid");
            }

            return tmpNode;
        }

        private XmlNode GetEntityNavBar(string entityName)
        {
            XmlNode root = this.GetEntityNode(entityName);
            XmlNode tmpNode = root.SelectSingleNode("NavBar");

            if (tmpNode == null)
            {
                XmlElement newElement = _isvConfigXml.CreateElement("NavBar");
                root.AppendChild(newElement);
                tmpNode = root.SelectSingleNode("NavBar");
            }

            return tmpNode;
        }

        private XmlNode GetMenuBar(XmlNode root)
        {
            return this.GetMenuContainerNode(root, "CustomMenus");
        }

        private XmlNode GetActionMenuBar(XmlNode root)
        {
            return this.GetMenuContainerNode(root, "ActionsMenu");
        }

        private XmlNode GetMenuContainerNode(XmlNode root, string containerName)
        {
            XmlNode tmpNode = root.SelectSingleNode("MenuBar/" + containerName);

            if (tmpNode == null)
            {
                tmpNode = root.SelectSingleNode("MenuBar");

                if (tmpNode == null)
                {
                    XmlElement newElement = _isvConfigXml.CreateElement("MenuBar");

                    root.AppendChild(newElement);
                }

                XmlElement newMenu = _isvConfigXml.CreateElement(containerName);

                tmpNode = root.SelectSingleNode("MenuBar");
                tmpNode.AppendChild(newMenu);

                tmpNode = root.SelectSingleNode("MenuBar/" + containerName);
            }

            return tmpNode;
        }

        private XmlNode GetConfigurationNode()
        {
            XmlNode tmpNode = _isvConfigXml.SelectSingleNode("/configuration");

            if (tmpNode == null)
            {
                XmlElement newElement = _isvConfigXml.CreateElement("configuration");
                newElement.SetAttribute("version", "3.0.0000.0");

                _isvConfigXml.AppendChild(newElement);

                tmpNode = _isvConfigXml.SelectSingleNode("/configuration");
            }

            return tmpNode;
        }

        private XmlNode GetRootNode()
        {
            XmlNode docRoot = GetConfigurationNode();

            XmlNode tmpNode = docRoot.SelectSingleNode("Root");

            if (tmpNode == null)
            {
                XmlElement newElement = _isvConfigXml.CreateElement("Root");

                docRoot.AppendChild(newElement);

                tmpNode = docRoot.SelectSingleNode("Root");
            }

            return tmpNode;
        }

        private XmlNode GetEntityNode(string entityName)
        {
            XmlNode docRoot = GetConfigurationNode().SelectSingleNode("Entities");

            XmlNode tmpNode = docRoot.SelectSingleNode("Entity[@name='" + entityName + "']");

            if (tmpNode == null)
            {
                XmlElement newElement = _isvConfigXml.CreateElement("Entity");
                newElement.SetAttribute("name", entityName);
                docRoot.AppendChild(newElement);

                tmpNode = docRoot.SelectSingleNode("Entity[@name='" + entityName + "']");
            }

            return tmpNode;
        }

        #endregion

        #endregion
    }
}