using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FetchXMLBuilder.UI
{
    public partial class LinkedEntityCtrl : EditManager
    {
        Business.LinkedEntity _linkEntity = new Business.LinkedEntity();


        #region Methods

        public LinkedEntityCtrl(FetchXMLControl parentCtrl)
            : base(parentCtrl)
        {
            InitializeComponent();
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            EditManagerHelper.LoadEntityAttributes(ParentCtrl.CurrentEntity.Name, fromAttributeComboBox);
            EditManagerHelper.LoadEntityAttributes(ParentCtrl.CurrentEntity.Name, toAttributeComboBox);
            EditManagerHelper.LoadCRMEntitiesTo(entityComboBox);
        }

        private void operatorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected override void LoadDataToCtrl(Business.FetchXMLElement elementData)
        {
            //base.LoadDataToCtrl(elementData);

            //
            if (elementData is Business.LinkedEntity)
            {
                //1.
                _linkEntity = (Business.LinkedEntity)elementData;

                //2.
                fromAttributeComboBox.Text = _linkEntity.LinkFrom;
                toAttributeComboBox.Text = _linkEntity.LinkTo;
                entityComboBox.Text = _linkEntity.Name;
                linkTypeComboBox.Text = _linkEntity.LinkType;
            }
        }

        protected override FetchXMLBuilder.Business.FetchXMLElement GetDataFromCtrl()
        {
            _linkEntity.Name = entityComboBox.Text;
            _linkEntity.LinkTo = toAttributeComboBox.Text;
            _linkEntity.LinkFrom = fromAttributeComboBox.Text;
            _linkEntity.LinkType = linkTypeComboBox.Text;
            //_linkEntity.Filters = ;
            //_linkEntity.Attributes = ;
            //_linkEntity.Filters = ;
            //_linkEntity.LinkedEntities =;

            return _linkEntity;
        }


        protected override void InitDefaultData()
        {
            base.InitDefaultData();
        }
        /*
        protected override void OnAdd()
        {
            GetDataFromCtrl();

            ((Business.Entity)ParentCtrl.CurrentParent).AddLinkEntity(_linkEntity);
        }
        */

        #endregion 

        private void editLinkEntityInfoButton_Click(object sender, EventArgs e)
        {
            EditLinkEntityFrm linkEntityForm = new EditLinkEntityFrm(_linkEntity as Business.LinkedEntity);
            if (linkEntityForm.ShowDialog() == DialogResult.OK)
            { 

            }

        }


    }
}
