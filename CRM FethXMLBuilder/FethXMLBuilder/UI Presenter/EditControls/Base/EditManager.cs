using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FetchXMLBuilder.UI
{
    public enum ActionType {New = 0, Update = 1}
    internal delegate void FetchXMLControlHandler();

    public partial class EditManager : UserControl
    {
        event FetchXMLControlHandler CancelChanges;

        event FetchXMLControlHandler AddNewElement;
        event FetchXMLControlHandler UpdateElement;
        

        #region Variables

        ActionType _actionType = ActionType.Update;
        FetchXMLControl _parentCtrl = null;

        #endregion

        #region Properties

        /// <summary>
        /// Current TreeNode Data to manipulate with
        /// </summary>
        public Business.FetchXMLElement Data
        {
            get
            {
         
                Business.FetchXMLElement editedElementData = ParentCtrl.CurrentNode.Tag as Business.FetchXMLElement;
                return editedElementData;
            }
            set
            {
                if ((ParentCtrl != null) && (Action == ActionType.Update))
                {
                    Business.FetchXMLElement editedElementData = ParentCtrl.CurrentNode.Tag as Business.FetchXMLElement;
                    LoadDataToCtrl(editedElementData);
                }
            }
        }

        public FetchXMLControl ParentCtrl
        {
            get { return _parentCtrl; }
            set { _parentCtrl = value; }
        }

        [Browsable(true)]
        [Category("Custom Properties")]
        public string Title
        {
            get { return titleLabel.Text; }
            set { titleLabel.Text = value;}
        }

        public ActionType Action
        {
            get { return _actionType; }
            set { _actionType = value; }
        }

        #endregion

        #region Methods

        protected virtual void InitDefaultData()
        {
            Title = "New " + Title;
        }

        protected virtual void LoadDataToCtrl(Business.FetchXMLElement elementData)
        {
          
        }

        protected virtual Business.FetchXMLElement GetDataFromCtrl()
        {
            return Data;
        }


        public EditManager()
        {
            InitializeComponent();
        }

        public EditManager(FetchXMLControl parentCtrl)
            : this()
        {           
            ParentCtrl = parentCtrl;

            AddNewElement += OnAdd;
            UpdateElement += OnUpdate;
            CancelChanges += OnCancelChanges;
        }


        protected virtual void Apply()
        {
            //1. Call event 
            switch (Action)
            {
                case ActionType.New:
                    if (AddNewElement != null)
                        AddNewElement();
                    //2. Refresh Common view - TreeView 
                    //   UpdateView();
                    break;

                case ActionType.Update:
                    if (UpdateElement != null)
                        UpdateElement();
                    break;
            }
        }

        //After changing Entity Content -> reload tree
        private void UpdateView()
        {
            if (ParentCtrl != null)
                ParentCtrl.OnUpdateView();
        }

        public void BeginAdding(Business.ElementType elementType)
        {            
            Action = ActionType.New;
            applyButton.Text = "Create";
                          
            InitDefaultData();
        }

        public void BeginEdit(Business.FetchXMLElement elementData)
        {
            Action = ActionType.Update;
            applyButton.Text = "Update";

            Data = elementData; //AutoLoad data to control

            //LoadDataToCtrl(elementData);
        }


        /// <summary>
        /// Generated after adding element to fetchXML Entity
        /// </summary>
        protected virtual void OnAdd()
        {
            Business.FetchXMLElement newElementData = GetDataFromCtrl();

            //1. In case if new query is creating
            if ((newElementData is Business.Entity) && !(newElementData is Business.LinkedEntity))
            {
                ParentCtrl.CurrentEntity = newElementData as Business.Entity;
                TreeNode rootNode = ParentCtrl.BuildFetchTreeFromEntity(ParentCtrl.CurrentEntity);               
                ParentCtrl.NavigateNode(rootNode);
            }
            else //2. sub elements is creating
            {
                TreeNode currentParent = ParentCtrl.CurrentNode;

                if (currentParent != null)
                {
                    //Current parent node for adding new element to
                    Business.FetchXMLElement toElement = currentParent.Tag as Business.FetchXMLElement;

                    //1. Add new element to current entity
                    if (toElement is Business.Entity)
                    {
                        if (newElementData is Business.Order)
                            ((Business.Entity)toElement).AddOrder(newElementData as Business.Order);
                        else if (newElementData is Business.Filter)
                            ((Business.Entity)toElement).AddFilter(newElementData as Business.Filter);
                        else if (newElementData is Business.LinkedEntity)
                            ((Business.Entity)toElement).AddLinkEntity(newElementData as Business.LinkedEntity);
                    }
                    else if (toElement is Business.Filter)
                    {
                        if (newElementData is Business.Condition)
                            ((Business.Filter)toElement).AddCondition(newElementData as Business.Condition);
                        else if (newElementData is Business.Filter)
                            ((Business.Filter)toElement).AddFilter(newElementData as Business.Filter);
                    }

                    //2. Add new element to parent TreeNode
                    TreeNode createdElementNode = ParentCtrl.AddElementToTreeViewNode(currentParent, newElementData);
                    ParentCtrl.NavigateNode(createdElementNode);
                }
            }
                
        }
        
        /*
        protected virtual void OnUpdate()
        {
            ParentCtrl.OnUpdateCurrentNode(Data);
        }
         */

        protected virtual void OnUpdate()
        {
                Business.FetchXMLElement updatedData = GetDataFromCtrl();
                ParentCtrl.OnUpdateCurrentNode(updatedData);
        }

        private void OnCancelChanges()
        {
            switch (Action)
            {
                case ActionType.New:
                    //Go ot entity root element 
                    //Delete Control
                    ParentCtrl.CancelAddingNewElement();                   
                    break;

                case ActionType.Update:
                    LoadDataToCtrl(Data); // load original data
                    break;
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            Apply();
        }

        #endregion
    }
}

