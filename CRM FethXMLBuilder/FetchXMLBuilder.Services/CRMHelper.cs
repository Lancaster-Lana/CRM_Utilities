using System;
using System.Net;
using System.IO;
using System.Security.Permissions;
using System.Xml.Serialization;
using Microsoft.Win32;
using FetchXMLBuilder.Services.MSCrmService;
using FetchXMLBuilder.Services.MSMetadataService;

namespace FetchXMLBuilder.Services
{
    public static class CRMHelper
    {
        static CrmService _crmService;
        static MetadataService _metadataService;

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

        #region Properties

        public static CrmService CrmService
        {
            get
            {
                return _crmService;
            }
            set
            {
                _crmService = value;
            }
        }

        public static MetadataService MetadataService
        {
            get
            {
                return _metadataService;
            }
            set
            {
                _metadataService = value;
            }
        }

        #endregion

        #region Methods

        #region Services Helper

        #region Load Local CRM Services
        /*
        /// <summary>
        /// Init by default local CRM Services settings - from registry
        /// </summary>
        static CRMHelper()
        {
            CrmService = GetLocalCrmService();
            MetadataService = GetLocalMetadataService();
        }
        */

        public static void InitCrmServices()
        {
            CrmService = GetLocalCrmService();
            MetadataService = GetLocalMetadataService();
        }

        private static string GetServicesPath()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE");
            key = key.OpenSubKey("Microsoft");
            key = key.OpenSubKey("MSCRM");
            return key == null ? string.Empty
                : (string)key.GetValue("ServerUrl");
        }

        [RegistryPermission(SecurityAction.Assert, Read = ErpRegistryPath)]
        public static CrmService GetLocalCrmService()
        {            
            string path = GetServicesPath();
            if (string.IsNullOrEmpty(path))
                throw new Exception("Error. You don't have CRM installed !");

            string url =  path + CrmServiceName;
            var service = new CrmService();
            service.Url = url;
            service.Credentials = CredentialCache.DefaultCredentials;            
            var req = new WhoAmIRequest();
            var resp = (WhoAmIResponse)service.Execute(req);
            service.CallerIdValue = new CallerId();
            service.CallerIdValue.CallerGuid = resp.UserId;
            return service;
        }

        [RegistryPermission(SecurityAction.Assert, Read = ErpRegistryPath)]
        public static MetadataService GetLocalMetadataService()
        {
            string url = GetServicesPath() + MetadataServiceName;
            var service = new MetadataService();
            service.Url = url;
            service.Credentials = CredentialCache.DefaultCredentials;
            return service;
        }

        #endregion

        /// <summary>
        /// Instanciate CRM Services
        /// </summary>
        /// <param name="crmServicesPath"></param>
        /// <param name="credentials"></param>
        public static void InitCrmServices(string crmServicesPath, System.Net.ICredentials credentials)
        {

            //1 Init MetadataService
            string metaUrl = crmServicesPath + MetadataServiceName;
            var metaService = new MetadataService();
            metaService.Url = metaUrl;
            metaService.Credentials = credentials;
            MetadataService = metaService;

            //2 Init CrmService   
            string crmServiceUrl = crmServicesPath + CrmServiceName;
            var crmService = new CrmService();
            crmService.Url = crmServiceUrl;
            crmService.Credentials = credentials;
            CrmService = crmService;
        }

        #endregion

        #region Retrieve Methods

        public static EntityMetadata[] GetEntities()
        {
            var metadata = MetadataService.RetrieveMetadata(MetadataFlags.All);
            return metadata.Entities;
        }

        public static EntityMetadata GetEntityMetadata(string entityName)
        {            
            if (!string.IsNullOrEmpty(entityName))
            {
                var entityData = MetadataService.RetrieveEntityMetadata(entityName, EntityFlags.All);
                return entityData;
            }
            return null;
        }

        public static AttributeMetadata GetEntityAttribute(string entityName, string attributeName)
        {
            AttributeMetadata attributeData = MetadataService.RetrieveAttributeMetadata(entityName, attributeName);
            return attributeData;
        }

        public static int GetEntityCode(string entityName)
        {
            var _metadataService = MetadataService;
            try
            {
                var entMetadata = _metadataService.RetrieveEntityMetadata(entityName, EntityFlags.EntityOnly);

                if (entMetadata != null)
                {
                    return entMetadata.ObjectTypeCode;
                }
            }
            catch (Exception ex)
            {
                //EventLog.WriteEntry(ex.Message);
            }
            return -1;
        }

        public static DynamicEntity DeserializeXmlToDynamicEntity(String XmlString)
        {
            TextReader sr = new StringReader(XmlString);
            var root = new XmlRootAttribute("BusinessEntity");
            root.Namespace = "http://schemas.microsoft.com/crm/2006/WebServices";
            var xmlSerializer = new XmlSerializer(typeof(BusinessEntity), root);

            var entity = (BusinessEntity)xmlSerializer.Deserialize(sr);
            var resDE = entity as DynamicEntity;
            return resDE;
        }

        public static string RunFetchXML(string fetchXML)
        {
            // Standard CRM Service Setup
            String result = CrmService.Fetch(fetchXML);

            /*
              string fetch2 = @"<fetch mapping=""logical"">
									<entity name=""account"">
										<attribute name=""accountid""/>
										<attribute name=""name""/>
										<link-entity name=""systemuser"" to=""owninguser"">
											<filter type=""and"">
												<condition attribute=""lastname"" operator=""ne"" value=""Cannon""/>
											</filter>
										</link-entity>
									</entity>
								</fetch>";

            */
            return result;
        }

        #endregion

        #endregion

    }
}