using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using FetchXMLBuilder.Services;
using FetchXMLBuilder.Business;

namespace FetchXMLBuilder.UI
{
    public partial class FetchXMLControl : UserControl
    {
        #region Variables & Events

        private EditManager _editManager = new EditManager();

        // Call methods from Entity Class after select TreeNodes
        event Action<ElementType> NewEvent;
        event Action<FetchXMLElement> DeleteEvent;
        event Action<FetchXMLElement> EditEvent;

        #endregion

        #region Properties
        
        private Entity _entity = new Entity(string.Empty);
        public Entity CurrentEntity
        {
            get
            {
                return _entity;
            }
            set
            {
                _entity = value;
            }
        }

        public Order CurrentOrder
        {
            get
            {
                if (CurrentNode.Tag is Business.Order)
                    return (Business.Order)(CurrentNode.Tag);
                return null;
            }
        }

        public Filter ParentFilter
        {
            get
            {
                if ((CurrentNode != null) && (CurrentNode.Parent.Tag is Business.Filter))
                    return (Filter)(CurrentNode.Parent.Tag);
                return null;
            }
        }

        public Filter CurrentFilter
        {
            get
            {
                if (CurrentNode.Tag is Filter)
                    return (Filter)(CurrentNode.Tag);
                return null;
            }
        }

        public Condition CurrentCondition
        {
            get
            {
                if (CurrentNode.Tag is Condition)
                    return (Condition)(CurrentNode.Tag);
                return null;
            }
        }

        public TreeNode CurrentNode
        {
            get
            {
                return this.fetchXMLTreeView.SelectedNode;
            }
        }

        public FetchXMLElement CurrentParent
        {
            get
            {
                if (EditManager.Action == ActionType.New)
                    return CurrentNode.Tag as FetchXMLElement;
                return CurrentNode.Parent.Tag as FetchXMLElement;
            }
        }

        public EditManager EditManager
        {
            get { return _editManager; }
            set
            {
                _editManager = value;

                //Load Control
                mainSplitContainer.Panel2.Controls.Clear();

                if (EditManager != null)
                {
                    mainSplitContainer.Panel2.Controls.Add(_editManager);
                    EditManager.Dock = DockStyle.Top;
                }
            }
        }

        #endregion

        #region Constructors

        internal FetchXMLControl(string crmServicesPath, ICredentials credentials)
        {
            try
            {
                InitializeComponent();

                //Event Add, Delete, Update fetchXML elements
                InitEvents();

                //Init CRM Services
                if (!string.IsNullOrEmpty(crmServicesPath))
                    CRMHelper.InitCrmServices(crmServicesPath, credentials);
                else
                    CRMHelper.InitCrmServices();

                ////Load CRM Entities to Control
                //LoadCRMEntities();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public FetchXMLControl()
            : this(string.Empty)
        {

        }

        public FetchXMLControl(string crmServicesPath)
            : this(crmServicesPath, CredentialCache.DefaultNetworkCredentials)
        {
        }


        public FetchXMLControl(string crmServicesPath, string userName, string password, string domain)
            : this(crmServicesPath, new NetworkCredential(userName, password, domain))
        {

        }

        #endregion

        #region Methods

        private void InitEvents()
        {
            NewEvent += OnCreateElement;
            DeleteEvent += OnDeleteElement;
            EditEvent += OnEditElement;
        }

        #region Event Handlers

        public void OnCreateElement(ElementType elementType)
        {
            switch (elementType)
            {
                case ElementType.entity:
                    EditManager = new EntityCtrl(this);
                    break;
                case ElementType.attribute:
                    EditManager = new AttributesCtrl(this);
                    break;
                case ElementType.order:
                    EditManager = new OrderCtrl(this);
                    break;
                case ElementType.filter:
                    EditManager = new FilterCtrl(this);
                    break;
                case ElementType.condition:
                    EditManager = new ConditionCtrl(this);
                    break;
                case ElementType.linkedEntity:
                    EditManager = new LinkedEntityCtrl(this);
                    break;
            }
            //EditManager.Action = ActionType.New;
            EditManager.BeginAdding(elementType);
        }

        public void OnDeleteElement(FetchXMLElement item)
        {
            //1. Update entity subelements data
            if (item is Business.Attribute)
                CurrentEntity.RemoveAttribute((Business.Attribute)item);
            else if (item is Order)
                CurrentEntity.RemoveOrder((Order)item);

            else if (item is Filter)
                CurrentEntity.RemoveFilter((Filter)item);
            else if (item is Condition)
                ParentFilter.RemoveCondition((Condition)item);

            else if (item is LinkedEntity)
                CurrentEntity.RemoveLinkedEntity((LinkedEntity)item);

            //2. Remove node from tree view //fetchXMLTreeView.Nodes.Remove(CurrentNode);                        
            var prevNode = CurrentNode.PrevNode ?? CurrentNode.Parent;
            UpdateFetchXMLTree();
            NavigateNode(prevNode);
        }

        public void OnEditElement(FetchXMLElement item)
        {
            //1. Load suitable ctrl
            if (item is LinkedEntity)
            {
                EditManager = new LinkedEntityCtrl(this);
            }
            else if (item is Entity)
            {
                EditManager = new EntityCtrl(this);
            }
            else if (item is Business.Attribute)
            {
                EditManager = new AttributesCtrl(this);
            }
            else if (item is Filter)
            {
                EditManager = new FilterCtrl(this);
            }
            else if (item is Condition)
            {
                EditManager = new ConditionCtrl(this);
            }
            else if (item is Business.Order)
            {
                EditManager = new OrderCtrl(this);
            }
            //2. Load data
            EditManager.BeginEdit(item);
        }

        public void CancelAddingNewElement()
        {
            //Select root element to load
            fetchXMLTreeView.SelectedNode = fetchXMLTreeView.Nodes[0];
        }

        public void OnUpdateView()
        {
            UpdateFetchXMLTree();
        }

        public void OnUpdateCurrentNode(Business.FetchXMLElement Data)
        {
            //1. Update Entity Data
            if (CurrentNode != null)
            {
                if (Data is LinkedEntity)
                {
                    //CurrentEntity.Orders.rep
                }
                else if (Data is Entity)
                {
                }
                else if (Data is Business.Attribute)
                {
                }
                else if (Data is Order)
                {
                    var entity = CurrentParent as Entity;
                    if (entity != null)
                    {
                        var currEntity = entity;
                        int currOrderIndex = currEntity.Orders.IndexOf(CurrentOrder);
                        if (currOrderIndex > -1)
                        {
                            currEntity.Orders[currOrderIndex] = Data as Business.Order;
                        }
                    }
                }
                else if (Data is Filter)
                {
                    if (CurrentParent is Entity)
                    {
                        var parentEntity = (Entity)CurrentParent;
                        int currFilterIndex = parentEntity.Filters.IndexOf(CurrentFilter);
                        parentEntity.Filters[currFilterIndex] = Data as Filter;
                    }
                    else if (CurrentParent is Filter)
                    {
                        var parentFilter = (Filter)CurrentParent;
                        int currFilterIndex = parentFilter.Filters.IndexOf(CurrentFilter);
                        parentFilter.Filters[currFilterIndex] = Data as Filter;
                    }                    
                    //int currFilterIndex = CurrentEntity.Filters.IndexOf(CurrentFilter);
                    //if (currFilterIndex > -1)
                    //{
                    //    CurrentEntity.Filters[currFilterIndex] = Data as Business.Filter;
                    //}
                }
                else if (Data is Condition)
                {
                    if (CurrentParent is Filter)
                    {
                        var parentFilter = (Filter)CurrentParent;

                        //int currFilterIndex = parentFilter.Filters.IndexOf(CurrentFilter);
                        int currCondIndex = parentFilter.Conditions.IndexOf(CurrentCondition);

                        if (currCondIndex > -1)//)((currFilterIndex > -1) && 
                        {
                            parentFilter.Conditions[currCondIndex] = Data as Business.Condition;
                            //CurrentEntity.Filters[currFilterIndex] = ParentFilter;
                        }
                    }
                }
                //2.Update Tree View
                CurrentNode.Tag = Data;
                CurrentNode.Text = Data.ToString();
            }
        }

        #endregion

        #region Tree Methods

        public TreeNode BuildFetchTreeFromEntity(Business.Entity entity)
        {
            fetchXMLTreeView.Nodes.Clear();

            //1. Entity Node
            TreeNode rootNode = new TreeNode(entity.ToString());
            rootNode.Tag = entity;
            fetchXMLTreeView.Nodes.Add(rootNode);

            //2.
            BuildEntitySubNodes(rootNode, entity);

            //3. Navigate root entity node
            //NavigateNode(rootNode);

            //4.Expand
            rootNode.Expand();

            return rootNode;
        }

        private void BuildEntitySubNodes(TreeNode rootNode, Business.Entity entity)
        {
            //2. Add Attributes
            foreach (Business.Attribute attr in entity.Attributes)
                AddElementToTreeViewNode(rootNode, attr);


            //3. Add Orders
            foreach (Business.Order order in entity.Orders)
                AddElementToTreeViewNode(rootNode, order);

            //4. Add Filters
            foreach (Business.Filter filter in entity.Filters)
            {
                TreeNode rootFilterNode = AddElementToTreeViewNode(rootNode, filter);
                BuildFilterSubNodes(rootFilterNode, filter);
            }

            //5. Add Linked Entities
            foreach (Business.LinkedEntity linkedEntity in entity.LinkedEntities)
            {
                TreeNode rootLinkEntityNode = AddElementToTreeViewNode(rootNode, linkedEntity);
                BuildLinkedEntitySubNodes(rootLinkEntityNode, linkedEntity);
            }

        }

        private void BuildFilterSubNodes(TreeNode rootFilterNode, Business.Filter filter)
        {
            //1. Load Conditions
            foreach (Business.Condition condition in filter.Conditions)
            {
                //TreeNode conditionNode = rootFilterNode.Nodes.Add(condition.ToString());
                //conditionNode.Tag = condition;              
                AddElementToTreeViewNode(rootFilterNode, condition);
            }

            //2. Load Filters
            foreach (Business.Filter subFilter in filter.Filters)
            {
                //TreeNode subFilterNode = rootFilterNode.Nodes.Add("filter type='" + subFilter.Type.ToString() + "'");
                //subFilterNode.Tag = subFilter;              
                TreeNode subFilterNode = AddElementToTreeViewNode(rootFilterNode, subFilter);
                BuildFilterSubNodes(subFilterNode, subFilter);//Loop if filter has conditions and/or filters
            }

        }

        private void BuildLinkedEntitySubNodes(TreeNode rootLinkEntityNode, /*Linked*/Entity linkedEntity)
        {
            BuildEntitySubNodes(rootLinkEntityNode, linkedEntity);
        }


        public TreeNode AddElementToTreeViewNode(TreeNode parentNode, Business.FetchXMLElement childElement)
        {
            TreeNode childNode = parentNode.Nodes.Add(childElement.ToString());
            childNode.Tag = childElement;
            return childNode;
        }

        public void NavigateNode(TreeNode node)
        {
            fetchXMLTreeView.SelectedNode = node;
            fetchXMLTreeView.Invalidate();
        }

        public TreeNode NavigateSubNode(Business.FetchXMLElement childElem)
        {
            TreeNode node = null;
            foreach (TreeNode childNode in CurrentNode.Nodes)
            {
                if (childNode.Tag.Equals(childElem))
                {
                    node = childNode;
                    break;
                }
            }
            NavigateNode(node);
            return node;
        }


        private void UpdateFetchXMLTree()
        {
            //Load Entity Data to Tree
            BuildFetchTreeFromEntity(CurrentEntity);
            //fetchXMLTreeView.Invalidate();
        }


        private Business.FetchXMLElement GetSelectedNodeData()
        {
            if (fetchXMLTreeView.SelectedNode != null)
            {
                Business.FetchXMLElement item = fetchXMLTreeView.SelectedNode.Tag as Business.FetchXMLElement;
                return item;
            }
            return null;
        }

        #region ContextMenu events

        private void InitContextMenu(Business.FetchXMLElement selectedItem)
        {

            if (selectedItem is Business.Entity)
            {
                newToolStripMenuItem.Visible = true;
                attributeToolStripMenuItem.Visible = true;
                orderToolStripMenuItem.Visible = true;
                filterToolStripMenuItem.Visible = true;
                conditionToolStripMenuItem.Visible = false;
                linkEntityToolStripMenuItem.Visible = true;

                if (selectedItem is Business.LinkedEntity)
                    deleteToolStripMenuItem.Visible = true;
                else
                    deleteToolStripMenuItem.Visible = false;
            }
            else if (selectedItem is Business.Filter)
            {
                newToolStripMenuItem.Visible = true;
                attributeToolStripMenuItem.Visible = false;
                orderToolStripMenuItem.Visible = false;
                filterToolStripMenuItem.Visible = true;
                conditionToolStripMenuItem.Visible = true;
                linkEntityToolStripMenuItem.Visible = false;

                deleteToolStripMenuItem.Visible = true;
            }
            else if ((selectedItem is Business.Order) || (selectedItem is Business.Attribute) || (selectedItem is Business.Condition))
            {
                newToolStripMenuItem.Visible = false;
                deleteToolStripMenuItem.Visible = true;
            }
        }

        private void fetchXMLTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //1.
            CurrentNode.BackColor = Color.LightBlue;
            // mainSplitContainer.Panel2.Controls.Clear();

            //2.
            if (EditEvent != null)
            {
                Business.FetchXMLElement nodeData = GetSelectedNodeData();

                //2.1 Init TreeView Context Menu
                InitContextMenu(nodeData);

                //2.2  Load edited data
                EditEvent(nodeData);
            }
        }

        private void fetchXMLTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (CurrentNode != null)
            {
                CurrentNode.BackColor = Color.White;
            }
        }


        public void NewTemplate()
        {
            fetchXMLTreeView.Nodes.Clear();
            //fetchXMLTreeView.Nodes.Add("New Query");

            NewEvent(ElementType.entity);

        }

        private void attributeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewEvent(ElementType.attribute);
        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewEvent(ElementType.order);
        }

        private void conditionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewEvent(ElementType.condition);
        }

        private void filterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewEvent(ElementType.filter);
        }

        private void linkEntityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewEvent(ElementType.linkedEntity);
            //Business.LinkedEntity newLinkedEntity = new Business.LinkedEntity("Default LinkedEntity");      
        }


        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditEvent != null)
            {
                Business.FetchXMLElement nodeData = GetSelectedNodeData();
                EditEvent(nodeData);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DeleteEvent != null)
            {
                Business.FetchXMLElement nodeData = GetSelectedNodeData();
                DeleteEvent(nodeData);
            }

        }

        #endregion

        #endregion

        #region Entity Methods

        public bool LoadFetchXML(string LoadFetchXML)
        {
            try
            {
                CurrentEntity = Entity.Deserialize(LoadFetchXML);
                BuildFetchTreeFromEntity(CurrentEntity);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot parse input string" + ex.Message);
                return false;
            }
            return true;
        }


        public void SaveToXMLFile(string fileName)
        {
            Entity.Serialize(CurrentEntity, fileName); //CurrentEntity.ToXML();
        }

        #endregion

        #endregion
    }
}
