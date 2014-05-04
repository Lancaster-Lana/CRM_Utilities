using System;
using System.IO;
using System.Reflection;
using System.Configuration;

namespace LanaSoft.Licensing.Usage
{
    public enum ErrorLicenseCode { licenseisefficient = 0, licensenotapplied = 1, filenotfount = 2, timeexpired = 3 }
    /// <summary>
    /// Summary description for CRMDiscountPackageLicenseApplier.
    /// </summary>

    public class CRMDiscountPackageLicenseApplier : MarshalByRefObject
    {
        public ErrorLicenseCode LicenseStatus
        {
            get { return _licenseResult; }
        }

        private string LicenseFileName = "LecenseFileName";
        private CRMDiscountPackageLicense _license;

        #region License properties

        private bool _licenseEnded = true;
        private DateTime _firstLaunch = DateTime.MinValue;
        private ErrorLicenseCode _licenseResult = ErrorLicenseCode.licensenotapplied;

        #endregion

        public CRMDiscountPackageLicenseApplier()
        {
            string licenseFileName = String.Empty;
            if (ConfigurationSettings.AppSettings[LicenseFileName] != null)
            {
                licenseFileName = ConfigurationSettings.AppSettings[LicenseFileName].ToString();
            }
            if (licenseFileName.Length == 0)
            {
                _licenseResult = ErrorLicenseCode.filenotfount;
                //System.Windows.Forms.MessageBox("License file not specified !");
            }
            else
            {
                //Assembly of exec file
                string execPath;
                AssemblyName assName = Assembly.GetEntryAssembly().GetName();
                Uri uri = new Uri(assName.EscapedCodeBase);              
                execPath = uri.ToString();
                //Check license
                string licenseFilePath = Path.Combine(Path.GetDirectoryName(execPath), licenseFileName);

                _license = new CRMDiscountPackageLicense();
                _license.Validate(licenseFilePath);

                if (GetLicenseExpiration())
                    _licenseResult = ErrorLicenseCode.timeexpired;
                else if (IsLicenseApplied())
                    _licenseResult = ErrorLicenseCode.licenseisefficient;
            }
        }

        private bool IsLicenseApplied()
        {
            if (_license.IsValid)
                return true;
            return false;
        }

        private bool GetLicenseExpiration()
        {
            if ((_license.ExpirationDate != DateTime.MinValue)
                && (_license.ExpirationDate != DateTime.MaxValue))
            {
                DateTime today = DateTime.Today;

                if (today > _license.ExpirationDate)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
