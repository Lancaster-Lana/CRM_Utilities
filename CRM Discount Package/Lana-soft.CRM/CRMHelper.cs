using System;
using System.Security.Permissions;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Win32;
using LanaSoftCRM.MicrosoftMetadataService;
using LanaSoftCRM.MicrosoftCrmService;

namespace LanaSoftCRM
{
    /// <summary>
    /// Summary description for CRMHelper
    /// </summary>
    public class CRMHelper
    {
        #region Constants

        private static string _crmServiceName = "/2006/CrmService.asmx";
        private static string _metadataServiceName = "/2006/MetadataService.asmx";
        private const string ErpRegistryPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSCRM";

        public static string CrmServiceName
        {
            get
            {
                return _crmServiceName;
            }
        }


        public static string MetadataServiceName
        {
            get
            {
                return _metadataServiceName;
            }
        }


        #endregion

        #region Services Helper

        public static string errorStr = "";

        private static string GetServicesPath()
        {
            var key = Registry.LocalMachine.OpenSubKey("SOFTWARE");
            if (key != null)
            {
                key = key.OpenSubKey("Microsoft");
                key = key.OpenSubKey("MSCRM");
            }

            return key != null
                ? (string)key.GetValue("ServerUrl")
                : null;
        }

        [RegistryPermission(SecurityAction.Assert, Read = ErpRegistryPath)]
        public static CrmService GetCrmService()
        {
            string path = GetServicesPath();
            if (string.IsNullOrEmpty(path))
            {
                throw new Exception("Error. You don't have CRM installed !");
            }
            var service = new CrmService();
            service.Url = path + CrmServiceName;
            service.Credentials = CredentialCache.DefaultCredentials;

            var req = new WhoAmIRequest();
            var resp = (WhoAmIResponse)service.Execute(req);
            service.CallerIdValue = new CallerId();
            service.CallerIdValue.CallerGuid = resp.UserId;
            return service;
        }

        [RegistryPermission(SecurityAction.Assert, Read = ErpRegistryPath)]
        public static MetadataService GetMetadataService()
        {
            string path = GetServicesPath();

            if (string.IsNullOrEmpty(path))
            {
                throw new Exception("Error. You don't have CRM installed !");
            }
            string url = path + MetadataServiceName;

            var service = new MetadataService();
            service.Url = url;

            service.Credentials = CredentialCache.DefaultCredentials;

            return service;
        }

        public static int GetEntityCode(string entityName)
        {
            try
            {
                var _metadataService = GetMetadataService();

                var entMetadata = _metadataService.RetrieveEntityMetadata(entityName, EntityFlags.EntityOnly);

                if (entMetadata != null)
                {
                    return entMetadata.ObjectTypeCode;
                }
            }
            catch (Exception ex)
            {
                errorStr = ex.Message;
            }
            return -1;
        }

        public static DynamicEntity DeserializeXmltoDynamicEntity(String xmlString)
        {
            var sr = new StringReader(xmlString);
            var root = new XmlRootAttribute("BusinessEntity");
            root.Namespace = "http://schemas.microsoft.com/crm/2006/WebServices";
            var xmlSerializer = new XmlSerializer(typeof(BusinessEntity), root);

            var entity = (BusinessEntity)xmlSerializer.Deserialize(sr);
            var resDE = entity as DynamicEntity;
            return resDE;
        }

        #endregion
    }
}