using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FetchXMLBuilder.UI
{
    public partial class FilterCtrl : EditManager
    {
        Business.Filter _filter = new Business.Filter();

        public List<Business.Filter> Filters
        {
            get
            {
                List<Business.Filter> filters = new List<Business.Filter>();
                foreach (object obj in subFiltersListBox.Items)
                {
                    Business.Filter fld = (Business.Filter)obj;
                    filters.Add(fld);
                }
                return filters;
            }
        }

        public List<Business.Condition> Conditions
        {
            get 
            {
                List<Business.Condition> cnds = new List<FetchXMLBuilder.Business.Condition>();
                foreach (object obj in conditionsListBox.Items)
                {
                    Business.Condition cnd = (Business.Condition)obj;
                    cnds.Add(cnd);
                }
                return cnds; 
            }
        }


        #region Methods

        public FilterCtrl()
        {
            InitializeComponent();
        }

        public FilterCtrl(FetchXMLControl parentCtrl)
            : base(parentCtrl)
        {
            InitializeComponent();
        }



        protected override void LoadDataToCtrl(Business.FetchXMLElement elementData)
        {
            // base.LoadDataToCtrl(elementData);
            if (elementData != null)
            {
                _filter = (Business.Filter)elementData;

                EditManagerHelper.LoadConditionsFromSource(conditionsListBox, _filter.Conditions);
                EditManagerHelper.LoadFiltersFromSource(subFiltersListBox, _filter.Filters);                
                //Oparator
                if (_filter.Type == FetchXMLBuilder.Business.FilterType.and)
                    andTypeRadioButton.Checked = true;
                else
                    orTypeRadioButton.Checked = true;
            }            
        }

        protected override Business.FetchXMLElement GetDataFromCtrl()
        {            
            _filter.Type = andTypeRadioButton.Checked ? FetchXMLBuilder.Business.FilterType.and : FetchXMLBuilder.Business.FilterType.or;

            _filter.Conditions = Conditions;
            _filter.Filters = Filters;                            

            return _filter;
        }


        /// <summary>
        /// To Create New Control
        /// </summary>
        protected override void InitDefaultData()
        {
            base.InitDefaultData();
            //base.InitDefaultData();            
            //Data = new Business.Filter();

            subFiltersListBox.DataSource = null;
            conditionsListBox.DataSource = null;
        }        

        #endregion
      

        private void addConditionButton_Click(object sender, EventArgs e)
        {
            /*ConditionFrm conditionForm = new ConditionFrm(this.ParentCtrl, _filter as Business.Filter);
            if (conditionForm.ShowDialog() == DialogResult.OK)
            { 

            }*/
            ParentCtrl.OnCreateElement(Business.ElementType.condition);
        }

        private void addFilterButton_Click(object sender, EventArgs e)
        {
            /*
            FilterFrm filterForm = new FilterFrm();
            if (filterForm.ShowDialog() == DialogResult.OK)
            {

            }*/
            ParentCtrl.OnCreateElement(Business.ElementType.filter);
        }


        private void editConditionButton_Click(object sender, EventArgs e)
        {
            /*
            ConditionFrm conditionForm = new ConditionFrm(
                                        ParentCtrl,
                                        conditionsListBox.SelectedItem as Business.Condition);
            if (conditionForm.ShowDialog() == DialogResult.OK)
            {

            }*/
            ParentCtrl.NavigateSubNode(conditionsListBox.SelectedItem as Business.FetchXMLElement);
        }

        private void editFilterButton_Click(object sender, EventArgs e)
        {
            /*FilterFrm filterForm = new FilterFrm(subFiltersListBox.SelectedItem as Business.Filter);
            if (filterForm.ShowDialog() == DialogResult.OK)
            {

            }*/
            ParentCtrl.NavigateSubNode(subFiltersListBox.SelectedItem as Business.FetchXMLElement);
        }


        private void deleteConditionButton_Click(object sender, EventArgs e)
        {
            //conditionsListBox.Items.Remove(conditionsListBox.SelectedItem);
            ParentCtrl.OnDeleteElement(conditionsListBox.SelectedItem as Business.FetchXMLElement);
        }

        private void deleteFilterButton_Click(object sender, EventArgs e)
        {
            ParentCtrl.OnDeleteElement(subFiltersListBox.SelectedItem as Business.FetchXMLElement);
            //subFiltersListBox.Items.Remove(subFiltersListBox.SelectedItem);
        }


        public void OnLoadData(Business.Filter filter)
        {
            LoadDataToCtrl(filter);
        }
    }
}

/*
protected override void OnAdd()
{
    //1.
     GetDataFromCtrl();
     if (ParentCtrl.CurrentParent is Business.Entity)
        ((Business.Entity)ParentCtrl.CurrentParent).AddFilter(_filter);
     else if (ParentCtrl.CurrentParent is Business.Filter)
        ((Business.Filter)ParentCtrl.CurrentParent).AddFilter(_filter);

    //2.
    TreeNode createdElementNode = ParentCtrl.AddElementToTreeViewNode(ParentCtrl.CurrentNode, _filter as Business.FetchXMLElement);
    ParentCtrl.NavigateNode(createdElementNode);
}*/

