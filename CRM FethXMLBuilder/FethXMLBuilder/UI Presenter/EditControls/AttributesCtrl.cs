using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FetchXMLBuilder.Services.MSMetadataService;

using FetchXMLBuilder.UI.Process;

namespace FetchXMLBuilder.UI
{
    public partial class AttributesCtrl : EditManager
    {

        #region Methods

        public AttributesCtrl(FetchXMLControl parentCtrl)
            : base(parentCtrl)
        {
            InitializeComponent();            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitEntityAttributes();
        }

        private void InitEntityAttributes()
        {  
            //1
            ArrayList visibleAttrs = ArrayList.Adapter(ParentCtrl.CurrentEntity.Attributes);
            EditManagerHelper.LoadAttributesFromSource(visibleAttributesListBox, visibleAttrs);

            //2
            //FetchXMLBuilder.Services.MSMetadataService.AttributeMetadata[] allAttrs = FetchHelper.GetEntityAttributes(ParentCtrl.CurrentEntity.Name);
            //List<Business.Attribute> unvisibleAttrs = MetadataAttributesToBusinessAttributes(allAttrs);//ArrayList.Adapter(allAttrs).Clone() as ArrayList;          

            List<Business.Attribute> unvisibleAttrs = FetchHelper.GetEntityAttributes(ParentCtrl.CurrentEntity.Name);

            foreach (Business.Attribute attr in visibleAttrs) unvisibleAttrs.Remove(attr);

            EditManagerHelper.LoadAttributesFromSource(availableAttributesListBox, unvisibleAttrs);
        }

               
        protected override void LoadDataToCtrl(Business.FetchXMLElement element)
        {  
            //base.LoadDataToCtrl(element);

            if (element is Business.Attribute)
            {
                Business.Attribute attr = (Business.Attribute)element;

                //Select in list
                visibleAttributesListBox.SelectedValue = attr.Name;

                availableAttributesListBox.SelectedIndex = -1;
            }
        }

    
        protected override void OnAdd()
        {
            //1
            ParentCtrl.CurrentEntity.Attributes.Clear();

            //2. Reload Attributes
            ArrayList visibleLst = (ArrayList)visibleAttributesListBox.DataSource;
            foreach (Business.Attribute attr in visibleLst)
            {
                Business.Attribute newAttr = new FetchXMLBuilder.Business.Attribute(attr.Name);
                //Data = newAttr;
                ParentCtrl.CurrentEntity.AddAttribute(newAttr);
                //newAttr.Name 
                
            }
        }

        protected override void OnUpdate()
        {            
                OnAdd();            
        }

        protected override void Apply()
        {
            OnUpdate();
            //Refresh Common view - TreeView 
            ParentCtrl.OnUpdateView(); //Full view
        }

        #endregion


        private void addButton_Click(object sender, EventArgs e)
        {           
            object attr = availableAttributesListBox.SelectedItem;
            
            //3.
            ArrayList visibleAttrs  = new ArrayList();
            visibleAttrs.AddRange((ArrayList)((ArrayList)visibleAttributesListBox.DataSource).Clone());
            visibleAttrs.Add(attr);

            EditManagerHelper.LoadAttributesFromSource(visibleAttributesListBox, visibleAttrs);

            //2.            
            ArrayList availableAttrs = new ArrayList();
            availableAttrs.AddRange((ArrayList)((ArrayList)availableAttributesListBox.DataSource).Clone());
            availableAttrs.Remove(attr);
            EditManagerHelper.LoadAttributesFromSource(availableAttributesListBox, availableAttrs);
            
            //
            //this.Invalidate();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {              
             object attr = visibleAttributesListBox.SelectedItem;
            
            //3.
            ArrayList availableAttrs = new ArrayList();   
            availableAttrs.AddRange((ArrayList)((ArrayList)availableAttributesListBox.DataSource).Clone());
            availableAttrs.Add(attr);
            EditManagerHelper.LoadAttributesFromSource(availableAttributesListBox, availableAttrs);

            //2.
            ArrayList visibleAttrs = new ArrayList();
            visibleAttrs.AddRange((ArrayList)((ArrayList)visibleAttributesListBox.DataSource).Clone());
            visibleAttrs.Remove(attr);
            EditManagerHelper.LoadAttributesFromSource(visibleAttributesListBox, visibleAttrs);
          
            //
            this.Invalidate();
        }


    }
}
