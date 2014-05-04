using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace LanaSoft.LicenseGenerator
{
    /// <summary>
    /// The license loader class.
    /// </summary>
    internal class LicenseLoader
    {
        private const string LicenseContainersSectionName = "LicenseContainers";

        private object _licenseValidator;
        private LicenseContainer _licenseContainer; // Contains current license Container
        public LicenseContainer LicenseContainer
        {
            get
            {
                return _licenseContainer;
            }
        }

        /// <summary>
        /// Initializes a new instance of the LicenseLoader class.
        /// </summary>
        public LicenseLoader()
        {
            // Getting the license 
            //_licenseValidator		= null;
            ArrayList licenseContainers = null;
            try
            {
                licenseContainers = (ArrayList)ConfigurationSettings.GetConfig(LicenseContainersSectionName);
            }
            catch (ConfigurationException ex)
            {
                throw new ApplicationException("The configuration error has occurred: " + ex.Message);
            }

            if ((null == licenseContainers) || (0 == licenseContainers.Count))
                throw new ApplicationException("No license containers were specified.");

            var licenseContainerPickerForm = new LicenseContainerPickerForm();
            licenseContainerPickerForm.LicenseContainers.Clear();

            licenseContainerPickerForm.LicenseContainers.AddRange(licenseContainers);
            DialogResult result = licenseContainerPickerForm.ShowDialog();
            if (DialogResult.Cancel == result)
                return;

            // Getting the license container file name
            string licenseContainer = licenseContainerPickerForm.SelectedLicenseContainer.Assembly;
            // Save selected license container
            _licenseContainer = licenseContainerPickerForm.SelectedLicenseContainer;

            if ((null == licenseContainer) && (0 == licenseContainer.Length))
                throw new ApplicationException("The license container is not specified in the configuration file.");

            // Getting the license class file name
            string licenseClass = licenseContainerPickerForm.SelectedLicenseContainer.Class;
            if ((null == licenseClass) && (0 == licenseClass.Length))
                throw new ApplicationException("The license class is not specified in the configuration file.");

            // Trying to load the license container
            Assembly container = null;
            try
            {
                container = Assembly.LoadFrom(licenseContainer);
            }
            catch (FileNotFoundException)
            {
                throw new ApplicationException(string.Format(
                            System.Globalization.CultureInfo.CurrentCulture,
                            "The specified license container '{0}' is not found.", licenseContainer));
            }
            catch (BadImageFormatException)
            {
                throw new ApplicationException(string.Format(
                            System.Globalization.CultureInfo.CurrentCulture,
                            "The specified license container '{0}' is not valid .NET assembly.", licenseContainer));
            }
            catch (PathTooLongException)
            {
                throw new ApplicationException(string.Format(
                            System.Globalization.CultureInfo.CurrentCulture,
                            "The path to the license container '{0}' is too long.", licenseContainer));
            }

            // Looking for the specified class
            Type licenseClassType = container.GetType(licenseClass, false, true);
            if (null == licenseClassType)
                throw new ApplicationException(string.Format(
                            System.Globalization.CultureInfo.CurrentCulture,
                            "The specified license class type '{0}' has not been found in the license container.", licenseClass));

            try
            {
                _licenseValidator = Activator.CreateInstance(licenseClassType);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format(
                    System.Globalization.CultureInfo.CurrentCulture,
                    "Cannot create a new instance of the license class '{0}' ({1}).", licenseClass, ex.Message));
            }
        }

        /// <summary>
        /// Gets the license validator.
        /// </summary>
        public object LicenseValidator
        {
            get
            {
                return _licenseValidator;
            }
        }
    }
}
