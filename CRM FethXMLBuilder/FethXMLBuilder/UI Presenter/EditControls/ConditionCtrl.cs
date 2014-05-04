using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FetchXMLBuilder.UI
{
    public partial class ConditionCtrl : EditManager
    {
        Business.Condition _condition = new Business.Condition();

        #region Methods

        public ConditionCtrl()
        {
            InitializeComponent();    
        }

        public ConditionCtrl(FetchXMLControl parentCtrl)
            : base(parentCtrl)
        {
            InitializeComponent();
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (ParentCtrl != null)
                EditManagerHelper.LoadEntityAttributes(ParentCtrl.CurrentEntity.Name, attributeComboBox);        
        }        

        protected override void LoadDataToCtrl(Business.FetchXMLElement element)
        {
            //base.LoadDataToCtrl(element);            

            //Load into internal variable
            if (element != null)
            {
                _condition = (Business.Condition)element;

                attributeComboBox.Text = _condition.AttributeName;
                operatorComboBox.Text = _condition.Operator;

                //Load Values
                StringBuilder sb = new StringBuilder();
                foreach (string val in _condition.ValuesList)
                    sb.AppendLine(val);

                valuesTextBox.Text = sb.ToString();
            }                               
        }

        protected override Business.FetchXMLElement GetDataFromCtrl()
        {
            _condition.AttributeName = attributeComboBox.Text;
            _condition.Operator = operatorComboBox.Text;
            _condition.ValuesList = new List<string>();
            foreach (string strVal in valuesTextBox.Lines)
                _condition.ValuesList.Add(strVal);

            return _condition;
        }

        protected override void InitDefaultData()
        {
            base.InitDefaultData();

            attributeComboBox.Text = string.Empty;
            operatorComboBox.Text = string.Empty;
            valuesTextBox.Text = string.Empty;
        }

        /*
        protected override void OnAdd()
        {
            if (ParentCtrl.CurrentParent is Business.Filter)
            {
                GetDataFromCtrl();

                ((Business.Filter)ParentCtrl.CurrentParent).AddCondition(_condition);
            }
        }
        */

        /*
        protected override void OnUpdate()
        {            
            ParentCtrl.CurrentNode.Tag = ConditionData;
            ParentCtrl.CurrentNode.Text = ConditionData.ToString();
        }*/

        public void OnLoadData(Business.Condition condition)
        {
            LoadDataToCtrl(condition);
        }

        public void OnLoadData(FetchXMLControl parentCtrl, Business.Condition condition)
        {
            ParentCtrl = parentCtrl;
            OnLoadData(condition);
        }

        public void OnLoadData(FetchXMLControl parentCtrl, Business.Filter parentFilter)
        {
            OnLoadData(parentCtrl, new FetchXMLBuilder.Business.Condition());
        }
     

        #endregion 

        private void applyButton_Click(object sender, EventArgs e)
        {

        }
    }
}

