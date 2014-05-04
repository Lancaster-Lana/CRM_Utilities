using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using FetchXMLBuilder.UI.Process;
using FetchXMLBuilder.Services.MSMetadataService;

namespace FetchXMLBuilder.UI
{
    public class EditManagerHelper
    {
        public static void LoadCRMEntitiesTo(ListControl box)
        {
            /*FetchXMLBuilder.Services.MSMetadataService.EntityMetadata[]*/
            ArrayList entities = FetchHelper.GetAllEntities();
            box.DataSource = entities;
            box.DisplayMember = "Name";
            box.ValueMember = "ObjectTypeCode";
            box.Invalidate();
        }

        public static void LoadEntityAttributes(string entityName, ListControl box)
        {
            List<Business.Attribute> source = FetchHelper.GetEntityAttributes(entityName);

            box.DataSource = source;
            if (source.Count > 0)
            {
                box.DisplayMember = "Name";
                box.ValueMember = "Name";
            }
            box.Invalidate();
        }

        public static void LoadAttributesFromSource(ListControl lstBox, ArrayList source)
        {
            lstBox.DataSource = source;
            if (source.Count > 0)
            {
                lstBox.DisplayMember = "Name";
                lstBox.ValueMember = "Name";
            }
            lstBox.Invalidate();
        }

        public static void LoadOrdersFromSource(ListControl lstBox, ArrayList source)
        {
            lstBox.DataSource = source;
            if (source.Count > 0)
            {
                lstBox.DisplayMember = "Description";
                //lstBox.ValueMember = "Name";
            }
            lstBox.Invalidate();
        }
        
        public static void LoadFiltersFromSource(ListControl lstBox, ArrayList source)
        {
            lstBox.DataSource = source;
            if (source.Count > 0)
            {
                lstBox.DisplayMember = "Description";
                //lstBox.ValueMember = "object";
            }
            lstBox.Invalidate();
        }

        public static void LoadConditionsFromSource(ListControl lstBox, ArrayList source)
        {
            lstBox.DataSource = source;
            if (source.Count > 0)
            {
                lstBox.DisplayMember = "Description";
                //lstBox.ValueMember = "object";
            }
            lstBox.Invalidate();
        }

        public static void LoadLinkEntitiesFromSource(ListControl lstBox, ArrayList source)
        {
            lstBox.DataSource = source;
            if (source.Count > 0)
            {
                lstBox.DisplayMember = "Description";
                //lstBox.ValueMember = "value";//source
            }
            lstBox.Invalidate();
        }

        public static void LoadAttributesFromSource(ListControl lstBox, List<Business.Attribute> source)
        {
            LoadAttributesFromSource(lstBox, ArrayList.Adapter(source));
        }

        public static void LoadOrdersFromSource(ListControl lstBox, List<Business.Order> source)
        {
            LoadOrdersFromSource(lstBox, ArrayList.Adapter(source));
        }

        public static void LoadFiltersFromSource(ListControl lstBox, List<Business.Filter> source)
        {
            LoadFiltersFromSource(lstBox, ArrayList.Adapter(source));
        }

        public static void LoadConditionsFromSource(ListControl lstBox, List<Business.Condition> source)
        {
            LoadConditionsFromSource(lstBox, ArrayList.Adapter(source));
        }

        public static void LoadLinkEntitiesFromSource(ListControl lstBox, List<Business.LinkedEntity> source)
        {
            LoadLinkEntitiesFromSource(lstBox, ArrayList.Adapter(source));
        }

    }
}
