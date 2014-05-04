using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FetchXMLBuilder.Services;
using FetchXMLBuilder.Services.MSMetadataService;

namespace FetchXMLBuilder.UI.Process
{
    public class FetchHelper
    {
        public static List<Business.Attribute> MetadataAttributesToBusinessAttributes(AttributeMetadata[] source)
        {
            List<Business.Attribute> attrsLst = new List<Business.Attribute>();
            foreach (AttributeMetadata mAttr in source)
            {
                Business.Attribute attr = new Business.Attribute(mAttr.Name);
                attrsLst.Add(attr);
            }

            return attrsLst;
        } 


        internal static EntityMetadata GetEntity(string entityName)
        {
            return CRMHelper.GetEntityMetadata(entityName);
        }

        internal static /*FetchXMLBuilder.Services.MSMetadataService.AttributeMetadata[]*/ List<Business.Attribute> GetEntityAttributes(string entityName)
        {
            EntityMetadata entityMD = GetEntity(entityName);
            if(entityMD != null)
            {
                AttributeMetadata[] metaAttrs = entityMD.Attributes;
                 return MetadataAttributesToBusinessAttributes(metaAttrs);
            }
            return null;
            //return ArrayList.Adapter(GetEntity(entityName).Attributes);
        }

        public static /*FetchXMLBuilder.Services.MSMetadataService.EntityMetadata[]*/ArrayList GetAllEntities()
        {
            return ArrayList.Adapter(CRMHelper.GetEntities());
        }

        public static string RunFetchXML(string xmlString)
        {            
            string xmlResult = CRMHelper.RunFetchXML(xmlString);
            return xmlResult;
        }


    }
}
