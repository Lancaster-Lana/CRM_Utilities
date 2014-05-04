using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FetchXMLBuilder.UI
{
    public partial class EntityCtrl : EditManager
    {
        Business.Entity _entity = new Business.Entity();


        #region Properties

        public List<Business.Attribute> Attributes
        {
            get
            {
                List<Business.Attribute> attrs = new List<FetchXMLBuilder.Business.Attribute>();
                foreach (object obj in attributesCheckedListBox.Items)
                {
                    Business.Attribute attr = (Business.Attribute)obj;
                    attrs.Add(attr);
                }
                return attrs;
            }
        }

        public List<Business.Order> Orders
        {
            get
            {
                List<Business.Order> orders = new List<Business.Order>();
                foreach (object obj in ordersListBox.Items)
                {
                    Business.Order order = (Business.Order)obj;
                    orders.Add(order);
                }
                return orders;
            }
        }

        public List<Business.Filter> Filters
        {
            get
            {
                List<Business.Filter> filters = new List<Business.Filter>();
                foreach (object obj in subFiltersListBox.Items)
                {
                    Business.Filter filter = (Business.Filter)obj;
                    filters.Add(filter);
                }
                return filters;
            }
        }

        public List<Business.LinkedEntity> LinkedEntities
        {
            get
            {
                List<Business.LinkedEntity> linkEntities = new List<Business.LinkedEntity>();
                foreach (object obj in linkedEntitiesListBox.Items)
                {
                    Business.LinkedEntity linkEntity = (Business.LinkedEntity)obj;
                    linkEntities.Add(linkEntity);
                }
                return linkEntities;
            }
        }

        #endregion


        #region Methods

        public EntityCtrl()
        {
            InitializeComponent();
        }

        public EntityCtrl(FetchXMLControl parentCtrl)
            : base(parentCtrl)
        {
            InitializeComponent();            
        }


        protected override void LoadDataToCtrl(Business.FetchXMLElement elementData)
        {
            //base.LoadDataToCtrl(elementData);

            if (elementData is Business.Entity)
            {
                _entity = (Business.Entity)elementData;
                entitiesComboBox.Text = _entity.Name;
                EditManagerHelper.LoadAttributesFromSource(attributesCheckedListBox, _entity.Attributes);
                EditManagerHelper.LoadOrdersFromSource(ordersListBox, _entity.Orders);
                EditManagerHelper.LoadFiltersFromSource(subFiltersListBox, _entity.Filters);
                //EditManagerHelper.LoadConditionsFromSource(conditionsListBox, _entity);
                EditManagerHelper.LoadLinkEntitiesFromSource(linkedEntitiesListBox, _entity.LinkedEntities);                
            }
        }

        protected override Business.FetchXMLElement GetDataFromCtrl()
        {
            base.InitDefaultData();

            _entity.Name = entitiesComboBox.Text;
            _entity.Orders = Orders;
            _entity.Attributes = Attributes;
            //_entity.AllAttributes
            //_entity.NoAttributes =
            _entity.Filters = Filters;
            _entity.LinkedEntities = LinkedEntities;
            return _entity;
        }


        protected override void InitDefaultData()
        {
            entitiesComboBox.Text = string.Empty;
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);  
         
            EditManagerHelper.LoadCRMEntitiesTo(entitiesComboBox);                      
        }  

       /*
        protected override void OnAdd()
        {            
           // TreeNode currentParent = ParentCtrl.CurrentNode; //Entity

            GetDataFromCtrl();
            //TreeNode entityNode = ParentCtrl.BuildFetchTreeFromEntity(currentParent, _entity);
            TreeNode entityNode = ParentCtrl.BuildFetchTreeFromEntity(_entity);
            ParentCtrl.NavigateNode(entityNode);            
        }*/
       
        public void OnLoadData(Business.Entity entity)
        {
            LoadDataToCtrl(entity);
        }

        #endregion

        private void editFilterButton_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}

