using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FetchXMLBuilder.UI
{
    public partial class OrderCtrl : EditManager
    {

        Business.Order _order = new FetchXMLBuilder.Business.Order();

        public OrderCtrl(FetchXMLControl parentCtrl)
            : base(parentCtrl)
        {
            InitializeComponent();
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EditManagerHelper.LoadEntityAttributes(ParentCtrl.CurrentEntity.Name, attributeComboBox);
        }

        protected override void LoadDataToCtrl(Business.FetchXMLElement elementData)
        {
            //base.LoadDataToCtrl(elementData);
           
            if (elementData is Business.Order)
            {
                //1. Save to buffer
                _order = ((Business.Order)elementData);

                //2. Load to controls
                attributeComboBox.Text = _order.AttributeName;
                if (_order.Descending)
                    dascRadioButton.Checked = true;
                else
                    ascRadioButton.Checked = true;               
            }            
        }

        protected override Business.FetchXMLElement GetDataFromCtrl()
        {            
            _order.Descending = dascRadioButton.Checked;
            _order.AttributeName = attributeComboBox.Text;                        
            return _order;
        }


        protected override void InitDefaultData()
        {
            base.InitDefaultData();            
            //Data = new Business.Order();

            attributeComboBox.Text = string.Empty;
        }

        /*
        protected override void OnAdd()
        {
            GetDataFromCtrl();

            ((Business.Entity)ParentCtrl.CurrentParent).AddOrder(_order);
        }*/

    }
}

