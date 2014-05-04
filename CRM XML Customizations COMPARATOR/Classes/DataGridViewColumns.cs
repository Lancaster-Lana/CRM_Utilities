using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Diagnostics;
using System.Reflection;

namespace CustomizationsComparator
{

        /// <summary>
        /// Custom column type dedicated to the DataGridViewRadioButtonCell cell type.
        /// </summary>
        public class DataGridViewCustomColumn : DataGridViewColumn
        {

            public DataGridViewCustomColumn(): base(new DataGridViewCustomCell())
            {
            }
            /// <summary>
            /// Represents the implicit cell that gets cloned when adding rows to the grid.
            /// </summary>
            [ Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override DataGridViewCell CellTemplate
            {
                get
                {
                    return base.CellTemplate;
                }
                set
                {
                    DataGridViewCustomCell dataGridViewCustomCell = value as DataGridViewCustomCell;
                    if (value != null && dataGridViewCustomCell == null)
                    {
                        throw new InvalidCastException("Value provided for CellTemplate must be of type DataGridViewRadioButtonElements.DataGridViewRadioButtonCell or derive from it.");
                    }
                    base.CellTemplate = value;
                }
            }
            /// <summary>
            /// Replicates the DataSource property of the DataGridViewRadioButtonCell cell type.
            /// </summary>
            [AttributeProvider(typeof(IListSource)), Category("Data"), DefaultValue(null),Description("The data source that populates the radio buttons."),
            RefreshProperties(RefreshProperties.Repaint)]
            public object DataSource
            {
                get
                {
                    if (this.CustomCellTemplate == null)
                    {
                        throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                    }
                    return this.CustomCellTemplate.DataSource;
                }
                set
                {
                    if (this.CustomCellTemplate == null)
                    {
                        throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                    }
                    this.CustomCellTemplate.DataSource = value;
                    if (this.DataGridView != null)
                    {
                        DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
                        int rowCount = dataGridViewRows.Count;
                        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                        {
                            DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                            DataGridViewCustomCell dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewCustomCell;
                            if (dataGridViewCell != null)
                            {
                                dataGridViewCell.DataSource = value;
                            }
                        }
                        this.DataGridView.InvalidateColumn(this.Index);
                        // TODO: This column and/or grid rows may need to be autosized depending on their
                        //       autosize settings. Call the autosizing methods to autosize the column, rows, 
                        //       column headers / row headers as needed.
                    }
                }
            }
            /// <summary>
            /// Replicates the DisplayMember property of the DataGridViewRadioButtonCell cell type.
            /// </summary>
            [Category("Data"),DefaultValue(""), Description("A string that specifies the property or column from which to retrieve strings for display in the radio buttons."),
            Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)),
            TypeConverterAttribute("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design")]
            public string DisplayMember
            {
                get
                {
                    if (this.CustomCellTemplate == null)
                    {
                        throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                    }
                    return this.CustomCellTemplate.DisplayMember;
                }
                set
                {
                    if (this.CustomCellTemplate == null)
                    {
                        throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                    }
                    this.CustomCellTemplate.DisplayMember = value;
                    if (this.DataGridView != null)
                    {
                        DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
                        int rowCount = dataGridViewRows.Count;
                        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                        {
                            DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                            DataGridViewCustomCell dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewCustomCell;
                            if (dataGridViewCell != null)
                            {
                                dataGridViewCell.DisplayMember = value;
                            }
                        }
                        this.DataGridView.InvalidateColumn(this.Index);
                        // TODO: Add code to autosize the column and rows, the column headers,
                        // the row headers, depending on the autosize settings of the grid.
                    }
                }
            }
            /// <summary>
            /// Replicates the Items property of the DataGridViewRadioButtonCell cell type.
            /// </summary>
            [ Category("Data"),Description("The collection of objects used as entries for the radio buttons."),
             DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
             Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor))]
            public DataGridViewCustomCell.ObjectCollection Items
            {
                get
                {
                    if (this.CustomCellTemplate == null)
                    {
                        throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                    }
                    return this.CustomCellTemplate.Items;
                }
            }

            /// <summary>
            /// Small utility function that returns the template cell as a DataGridViewRadioButtonCell.
            /// </summary>
            private DataGridViewCustomCell CustomCellTemplate
            {
                get
                {
                    return (DataGridViewCustomCell)this.CellTemplate;
                }
            }
            /// <summary>
            /// Replicates the ValueMember property of the DataGridViewRadioButtonCell cell type.
            /// </summary>
            [Category("Data"), DefaultValue(""),Description("A string that specifies the property or column from which to get values that correspond to the radio buttons."),
            Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)),
            TypeConverterAttribute("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design")]
            public string ValueMember
            {
                get
                {
                    if (this.CustomCellTemplate == null)
                    {
                        throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                    }
                    return this.CustomCellTemplate.ValueMember;
                }
                set
                {
                    if (this.CustomCellTemplate == null)
                    {
                        throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                    }
                    this.CustomCellTemplate.ValueMember = value;
                    if (this.DataGridView != null)
                    {
                        DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
                        int rowCount = dataGridViewRows.Count;
                        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                        {
                            DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                            DataGridViewCustomCell dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewCustomCell;
                            if (dataGridViewCell != null)
                            {
                                dataGridViewCell.ValueMember = value;
                            }
                        }
                        this.DataGridView.InvalidateColumn(this.Index);
                        // TODO: Add code to autosize the column and rows, the column headers,
                        // the row headers, depending on the autosize settings of the grid.
                    }
                }
            }
            /// <summary>
            /// Call this public method when the Items collection of this column's CellTemplate was changed.
            /// Updates the items collection of each existing DataGridViewRadioButtonCell in the column.
            /// </summary>
            public void NotifyItemsCollectionChanged()
            {
                if (this.DataGridView != null)
                {
                    DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    DataGridViewCustomCell cellTemplate = this.CellTemplate as DataGridViewCustomCell;
                    object[] items = new object[cellTemplate.Items.Count];
                    cellTemplate.Items.CopyTo(items, 0);
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        DataGridViewCustomCell dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewCustomCell;
                        if (dataGridViewCell != null)
                        {
                            dataGridViewCell.Items.Clear();
                            dataGridViewCell.Items.AddRange(items);
                        }
                    }
                    this.DataGridView.InvalidateColumn(this.Index);
                    // This column and/or rows may need to be autosized.
                }
            }
            /// <summary>
            /// Returns a standard compact string representation of the column.
            /// </summary>
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder(64);
                sb.Append("DataGridViewCustomColumn { Name=");
                sb.Append(this.Name);
                sb.Append(", Index=");
                sb.Append(this.Index.ToString(CultureInfo.CurrentCulture));
                sb.Append(" }");
                return sb.ToString();
            }
        }//END CLASS
        public class DataGridViewCustomCell : DataGridViewComboBoxCell, IDataGridViewEditingCell
        {
            /// <summary>
            /// Convenient enumeration using privately for calculating preferred cell sizes.
            /// </summary>
            private enum DataGridViewRadioButtonFreeDimension
            {
                Both,
                Height,
                Width
            }
            private PropertyDescriptor displayMemberProperty;   // Property descriptor for the DisplayMember property
            private PropertyDescriptor valueMemberProperty;     // Property descriptor for the ValueMember property
            private CurrencyManager dataManager;                // Currency manager for the cell's DataSource
            private bool dataSourceInitializedHookedUp;         // Indicates whether the DataSource's Initialized event is listened to
            private int CELL_MARGIN = 4;
            private int DEFAULT_LINE_SPACING = 1;
            /// <summary>
            /// DataGridViewCustomCell class constructor.
            /// </summary>
            public DataGridViewCustomCell()
            {
            }

            // Implementation of the IDataGridViewEditingCell interface starts here.
            /// <summary>
            /// Represents the cell's formatted value
            /// </summary>
            public virtual object EditingCellFormattedValue
            {
                get
                {
                    return null;
                }
                set
                {
                }
            }
            /// <summary>
            /// Keeps track of whether the cell's value has changed or not.
            /// </summary>
            public virtual bool EditingCellValueChanged
            {
                get
                {
                    return false;
                }
                set
                {

                }
            }
            /// <summary>
            /// Returns the current formatted value of the cell
            /// </summary>
            public virtual object GetEditingCellFormattedValue(DataGridViewDataErrorContexts context)
            {
                return null;
            }
            /// <summary>
            /// Called by the grid when the cell enters editing mode. 
            /// </summary>
            public virtual void PrepareEditingCellForEdit(bool selectAll)
            {
                // This cell type has nothing to do here.
            }
            // Implementation of the IDataGridViewEditingCell interface stops here.
            /// <summary>
            /// Stores the CurrencyManager associated to the cell's DataSource
            /// </summary>
            private CurrencyManager DataManager
            {
                get
                {
                    CurrencyManager cm = this.dataManager;
                    if (cm == null && this.DataSource != null && this.DataGridView != null &&
                        this.DataGridView.BindingContext != null && !(this.DataSource == Convert.DBNull))
                    {
                        ISupportInitializeNotification dsInit = this.DataSource as ISupportInitializeNotification;
                        if (dsInit != null && !dsInit.IsInitialized)
                        {
                            // The datasource is not ready yet. Attaching to its Initialized event to be notified
                            // when it's finally ready
                            if (!this.dataSourceInitializedHookedUp)
                            {
                                dsInit.Initialized += new EventHandler(DataSource_Initialized);
                                this.dataSourceInitializedHookedUp = true;
                            }
                        }
                        else
                        {
                            cm = (CurrencyManager)this.DataGridView.BindingContext[this.DataSource];
                            this.DataManager = cm;
                        }
                    }
                    return cm;
                }
                set
                {
                    this.dataManager = value;
                }
            }
            /// <summary>
            /// Overrides the DataGridViewComboBox's implementation of the DataSource property to 
            /// initialize the displayMemberProperty and valueMemberProperty members.
            /// </summary>
            public override object DataSource
            {
                get
                {
                    return base.DataSource;
                }
                set
                {
                    if (this.DataSource != value)
                    {
                        // Invalidate the currency manager
                        this.DataManager = null;
                        ISupportInitializeNotification dsInit = this.DataSource as ISupportInitializeNotification;
                        if (dsInit != null && this.dataSourceInitializedHookedUp)
                        {
                            // If we previously hooked the datasource's ISupportInitializeNotification
                            // Initialized event, then unhook it now (we don't always hook this event,
                            // only if we needed to because the datasource was previously uninitialized)
                            dsInit.Initialized -= new EventHandler(DataSource_Initialized);
                            this.dataSourceInitializedHookedUp = false;
                        }
                        base.DataSource = value;
                        // Update the displayMemberProperty and valueMemberProperty members.
                        try
                        {
                            InitializeDisplayMemberPropertyDescriptor(this.DisplayMember);
                        }
                        catch
                        {
                            Debug.Assert(this.DisplayMember != null && this.DisplayMember.Length > 0);
                            InitializeDisplayMemberPropertyDescriptor(null);
                        }
                        try
                        {
                            InitializeValueMemberPropertyDescriptor(this.ValueMember);
                        }
                        catch
                        {
                            Debug.Assert(this.ValueMember != null && this.ValueMember.Length > 0);
                            InitializeValueMemberPropertyDescriptor(null);
                        }
                        if (value == null)
                        {
                            InitializeDisplayMemberPropertyDescriptor(null);
                            InitializeValueMemberPropertyDescriptor(null);
                        }
                    }
                }
            }
            /// <summary>
            /// Overrides the DataGridViewComboBox's implementation of the DisplayMember property to
            /// update the displayMemberProperty member.
            /// </summary>
            public override string DisplayMember
            {
                get
                {
                    return base.DisplayMember;
                }
                set
                {
                    base.DisplayMember = value;
                    InitializeDisplayMemberPropertyDescriptor(value);
                }
            }
            /// <summary>
            /// Overrides the base implementation to replace the 'complex editing experience'
            /// with a 'simple editing experience'.
            /// </summary>
            public override Type EditType
            {
                get
                {
                    // Return null since no editing control is used for the editing experience.
                    return null;
                }
            }

            /// <summary>
            /// Called internally by the DataGridViewRadioButtonColumn class to avoid the invalidation
            /// done by the MaxDisplayedItems setter above (for performance reasons).
            /// </summary>
            //internal int MaxDisplayedItemsInternal
            //{
            //    set
            //    {
            //        Debug.Assert(value >= 1 && value <= 100);
            //        this.maxDisplayedItems = value;
            //    }
            //}
            /// <summary>
            /// Utility function that returns the standard thickness (in pixels) of the four borders of the cell.
            /// </summary>
            private Rectangle StandardBorderWidths
            {
                get
                {
                    if (this.DataGridView != null)
                    {
                        DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder = new DataGridViewAdvancedBorderStyle(), dgvabsEffective;
                        dgvabsEffective = AdjustCellBorderStyle(this.DataGridView.AdvancedCellBorderStyle,
                            dataGridViewAdvancedBorderStylePlaceholder,
                            false /*singleVerticalBorderAdded*/,
                            false /*singleHorizontalBorderAdded*/,
                            false /*isFirstDisplayedColumn*/,
                            false /*isFirstDisplayedRow*/);
                        return BorderWidths(dgvabsEffective);
                    }
                    else
                    {
                        return Rectangle.Empty;
                    }
                }
            }
            /// <summary>
            /// Overrides the DataGridViewComboBox's implementation of the ValueMember property to
            /// update the valueMemberProperty member.
            /// </summary>
            public override string ValueMember
            {
                get
                {
                    return base.ValueMember;
                }
                set
                {
                    base.ValueMember = value;
                    InitializeValueMemberPropertyDescriptor(value);
                }
            }
            /// <summary>
            /// Utility function that returns the cell state inherited from the owning row and column.
            /// </summary>
            private DataGridViewElementStates CellStateFromColumnRowStates(DataGridViewElementStates rowState)
            {
                Debug.Assert(this.DataGridView != null);
                Debug.Assert(this.ColumnIndex >= 0);
                DataGridViewElementStates orFlags = DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected;
                DataGridViewElementStates andFlags = DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible;
                DataGridViewElementStates cellState = (this.OwningColumn.State & orFlags);
                cellState |= (rowState & orFlags);
                cellState |= ((this.OwningColumn.State & andFlags) & (rowState & andFlags));
                return cellState;
            }
            /// <summary>
            /// Custom implementation of the Clone method to copy over the special properties of the cell.
            /// </summary>
            public override object Clone()
            {
                DataGridViewCustomCell dataGridViewCell = base.Clone() as DataGridViewCustomCell;
                return dataGridViewCell;
            }
            /// <summary>
            /// Computes the layout of the cell and optionally paints it.
            /// </summary>
            private void ComputeLayout(Graphics graphics,
                                       Rectangle clipBounds,
                                       Rectangle cellBounds,
                                       int rowIndex,
                                       DataGridViewElementStates cellState,
                                       object formattedValue,
                                       string errorText,
                                       DataGridViewCellStyle cellStyle,
                                       DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                       DataGridViewPaintParts paintParts,
                                       bool paint)
            {
                if (paint && DataGridViewCustomCell.PartPainted(paintParts, DataGridViewPaintParts.Border))
                {
                    // Paint the borders first
                    PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
                }
                // Discard the space taken up by the borders.
                Rectangle borderWidths = BorderWidths(advancedBorderStyle);
                Rectangle valBounds = cellBounds;
                valBounds.Offset(borderWidths.X, borderWidths.Y);
                valBounds.Width -= borderWidths.Right;
                valBounds.Height -= borderWidths.Bottom;
                SolidBrush backgroundBrush = null;
                try
                {
                    Point ptCurrentCell = this.DataGridView.CurrentCellAddress;
                    bool cellCurrent = ptCurrentCell.X == this.ColumnIndex && ptCurrentCell.Y == rowIndex;
                    bool cellSelected = (cellState & DataGridViewElementStates.Selected) != 0;
                    bool mouseOverCell = cellBounds.Contains(this.DataGridView.PointToClient(Control.MousePosition));
                    if (DataGridViewCustomCell.PartPainted(paintParts, DataGridViewPaintParts.SelectionBackground) && cellSelected)
                    {
                        backgroundBrush = new SolidBrush(cellStyle.SelectionBackColor);
                    }
                    else
                    {
                        backgroundBrush = new SolidBrush(cellStyle.BackColor);
                    }
                    if (paint && DataGridViewCustomCell.PartPainted(paintParts, DataGridViewPaintParts.Background) && backgroundBrush.Color.A == 255)
                    {
                        Rectangle backgroundRect = valBounds;
                        backgroundRect.Intersect(clipBounds);
                        graphics.FillRectangle(backgroundBrush, backgroundRect);
                    }
                    Rectangle errorBounds = valBounds;
                    Rectangle scrollBounds = valBounds;
                    if (valBounds.Width > 0 && valBounds.Height > 0)
                    {
                        int textHeight = cellStyle.Font.Height;
                        int textBoxHeight = textHeight + (CELL_MARGIN * 2);
                        int itemIndex = 0;
                        Rectangle textBoxCellBounds = valBounds;
                        Rectangle textBoxBound = valBounds;
                        while (itemIndex < this.Items.Count && textBoxCellBounds.Height > 0)
                        {
                            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                            // Figure out the width of the longest entry
                            double preferredItemHeight = 0;
                            string text;
                            double wordCount;
                            Size charSize;
                            Size textSize;
                            Size spaceSize;
                            double charHeight;
                            double charWidth;
                            double cellWidth;
                            double no_of_char_per_line;
                            double no_of_line;
                            double line_spacing;
                            text = GetFormattedValue(GetItemValue(this.Items[itemIndex]), rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.PreferredSize) as string;
                            spaceSize = spaceSize = DataGridViewCell.MeasureTextSize(graphics, " ", cellStyle.Font, flags);
                            if (!String.IsNullOrEmpty(text))
                                textSize = DataGridViewCell.MeasureTextSize(graphics, text.ToString(), cellStyle.Font, flags);
                            else
                                textSize = spaceSize;
                            wordCount = text.ToCharArray().Length;
                            charHeight = textSize.Height;
                            cellWidth = valBounds.Width;
                            if ((textSize.Width) < cellWidth)
                                no_of_line = 1;
                            else
                            {
                                no_of_line = Math.Ceiling((textSize.Width + (spaceSize.Width / 2)) / cellWidth);
                                //Adjustment
                                double times = Math.Ceiling((textSize.Width + (spaceSize.Width / 2)) / cellWidth);
                                double final = times * cellWidth;
                                if ((final > textSize.Width) && ((final - textSize.Width) < 5))
                                    no_of_line++;
                            }
                            preferredItemHeight = Math.Ceiling(Convert.ToDouble(no_of_line * textSize.Height));
                            line_spacing = (cellStyle.Font.Height / 4) * (no_of_line - 1);
                            preferredItemHeight += CELL_MARGIN * 2;
                            if (cellStyle.WrapMode == DataGridViewTriState.False || cellStyle.WrapMode == DataGridViewTriState.NotSet)
                            {
                                textBoxBound.Height = textSize.Height + (2 * CELL_MARGIN);
                            }
                            else
                            {
                                textBoxBound.Height = (int)Math.Ceiling(preferredItemHeight);
                            }

                            if (paint && DataGridViewCustomCell.PartPainted(paintParts, DataGridViewPaintParts.ContentBackground))
                            {
                                Rectangle itemRect = textBoxCellBounds;
                                itemRect.Intersect(clipBounds);
                                if (!itemRect.IsEmpty)
                                {
                                    bool itemReadOnly = (cellState & DataGridViewElementStates.ReadOnly) != 0;
                                    bool itemSelected = false;
                                    if (formattedValue != null)
                                    {
                                        object displayValue = GetItemDisplayValue(this.Items[itemIndex]);
                                        if (formattedValue.Equals(displayValue))
                                        {
                                            itemSelected = true;
                                        }
                                    }
                                    PaintItem(graphics,
                                              textBoxCellBounds,
                                              textBoxBound,
                                              rowIndex,
                                              itemIndex,
                                              cellStyle,
                                              itemReadOnly,
                                              itemSelected,
                                              mouseOverCell,
                                              cellCurrent && DataGridViewCustomCell.PartPainted(paintParts, DataGridViewPaintParts.Focus),
                                              cellState);
                                    //PaintItem(graphics,
                                    //          textBoxCellBounds,
                                    //          rowIndex,
                                    //          itemIndex,
                                    //          cellStyle,
                                    //          itemReadOnly,
                                    //          itemSelected,
                                    //          mouseOverCell,
                                    //          cellCurrent && DataGridViewCustomCell.PartPainted(paintParts, DataGridViewPaintParts.Focus),
                                    //          cellState);
                                }
                            }

                            //-----------
                            if (cellStyle.WrapMode == DataGridViewTriState.False || cellStyle.WrapMode == DataGridViewTriState.NotSet)
                            {
                                textBoxBound.Y += textBoxHeight;
                            }
                            else
                            {
                                textBoxBound.Y += (int)Math.Ceiling(preferredItemHeight);
                            }
                            //-----------

                            textBoxCellBounds.Height -= textBoxBound.Height;
                            itemIndex++;
                        }
                    }
                    // Finally paint the potential error icon
                    if (paint &&
                        DataGridViewCustomCell.PartPainted(paintParts, DataGridViewPaintParts.ErrorIcon) &&
                        !(cellCurrent && this.DataGridView.IsCurrentCellInEditMode) &&
                        this.DataGridView.ShowCellErrors)
                    {
                        PaintErrorIcon(graphics, clipBounds, errorBounds, errorText);
                    }
                }
                finally
                {
                    if (backgroundBrush != null)
                    {
                        backgroundBrush.Dispose();
                    }
                }
            }
            /// <summary>
            /// Returns whether calling the OnContentClick method would force the owning row to be unshared.
            /// </summary>
            protected override bool ContentClickUnsharesRow(DataGridViewCellEventArgs e)
            {
                Point ptCurrentCell = this.DataGridView.CurrentCellAddress;
                return ptCurrentCell.X == this.ColumnIndex &&
                       ptCurrentCell.Y == e.RowIndex &&
                       this.DataGridView.IsCurrentCellInEditMode;
            }
            /// <summary>
            /// Raised when the cell's DataSource is initialized.
            /// </summary>
            private void DataSource_Initialized(object sender, EventArgs e)
            {
                Debug.Assert(sender == this.DataSource);
                Debug.Assert(this.DataSource is ISupportInitializeNotification);
                Debug.Assert(this.dataSourceInitializedHookedUp);
                ISupportInitializeNotification dsInit = this.DataSource as ISupportInitializeNotification;
                // Unhook the Initialized event.
                if (dsInit != null)
                {
                    dsInit.Initialized -= new EventHandler(DataSource_Initialized);
                }
                // The wait is over: the DataSource is initialized.
                this.dataSourceInitializedHookedUp = false;
                // Check the DisplayMember and ValueMember values - will throw if values don't match existing fields.
                InitializeDisplayMemberPropertyDescriptor(this.DisplayMember);
                InitializeValueMemberPropertyDescriptor(this.ValueMember);
            }
            /// <summary>
            /// Returns whether calling the OnEnter method would force the owning row to be unshared.
            /// </summary>
            protected override bool EnterUnsharesRow(int rowIndex, bool throughMouseClick)
            {
                return true;// this.focusedItemIndex == -1;
            }

            /// <summary>
            /// Utility function that converts a constraintSize provided to GetPreferredSize into a 
            /// DataGridViewRadioButtonFreeDimension enum value.
            /// </summary>
            private static DataGridViewRadioButtonFreeDimension GetFreeDimensionFromConstraint(Size constraintSize)
            {
                if (constraintSize.Width < 0 || constraintSize.Height < 0)
                {
                    throw new ArgumentException("InvalidArgument=Value of '" + constraintSize.ToString() + "' is not valid for 'constraintSize'.");
                }
                if (constraintSize.Width == 0)
                {
                    if (constraintSize.Height == 0)
                    {
                        return DataGridViewRadioButtonFreeDimension.Both;
                    }
                    else
                    {
                        return DataGridViewRadioButtonFreeDimension.Width;
                    }
                }
                else
                {
                    if (constraintSize.Height == 0)
                    {
                        return DataGridViewRadioButtonFreeDimension.Height;
                    }
                    else
                    {
                        throw new ArgumentException("InvalidArgument=Value of '" + constraintSize.ToString() + "' is not valid for 'constraintSize'.");
                    }
                }
            }
            /// <summary>
            /// Utility function that returns the display value of an item given the 
            /// display/value property descriptors and display/value property names.
            /// </summary>
            private object GetItemDisplayValue(object item)
            {
                Debug.Assert(item != null);
                bool displayValueSet = false;
                object displayValue = null;
                if (this.displayMemberProperty != null)
                {
                    displayValue = this.displayMemberProperty.GetValue(item);
                    displayValueSet = true;
                }
                else if (this.valueMemberProperty != null)
                {
                    displayValue = this.valueMemberProperty.GetValue(item);
                    displayValueSet = true;
                }
                else if (!string.IsNullOrEmpty(this.DisplayMember))
                {
                    PropertyDescriptor propDesc = TypeDescriptor.GetProperties(item).Find(this.DisplayMember, true /*caseInsensitive*/);
                    if (propDesc != null)
                    {
                        displayValue = propDesc.GetValue(item);
                        displayValueSet = true;
                    }
                }
                else if (!string.IsNullOrEmpty(this.ValueMember))
                {
                    PropertyDescriptor propDesc = TypeDescriptor.GetProperties(item).Find(this.ValueMember, true /*caseInsensitive*/);
                    if (propDesc != null)
                    {
                        displayValue = propDesc.GetValue(item);
                        displayValueSet = true;
                    }
                }
                if (!displayValueSet)
                {
                    displayValue = item;
                }
                return displayValue;
            }
            /// <summary>
            /// Utility function that returns the value of an item given the 
            /// display/value property descriptors and display/value property names.
            /// </summary>
            private object GetItemValue(object item)
            {
                bool valueSet = false;
                object value = null;
                if (this.valueMemberProperty != null)
                {
                    value = this.valueMemberProperty.GetValue(item);
                    valueSet = true;
                }
                else if (this.displayMemberProperty != null)
                {
                    value = this.displayMemberProperty.GetValue(item);
                    valueSet = true;
                }
                else if (!string.IsNullOrEmpty(this.ValueMember))
                {
                    PropertyDescriptor propDesc = TypeDescriptor.GetProperties(item).Find(this.ValueMember, true /*caseInsensitive*/);
                    if (propDesc != null)
                    {
                        value = propDesc.GetValue(item);
                        valueSet = true;
                    }
                }
                if (!valueSet && !string.IsNullOrEmpty(this.DisplayMember))
                {
                    PropertyDescriptor propDesc = TypeDescriptor.GetProperties(item).Find(this.DisplayMember, true /*caseInsensitive*/);
                    if (propDesc != null)
                    {
                        value = propDesc.GetValue(item);
                        valueSet = true;
                    }
                }
                if (!valueSet)
                {
                    value = item;
                }
                return value;
            }//END


            /// <summary>
            /// Custom implementation of the GetPreferredSize method.
            /// </summary>
            protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
            {
                if (this.DataGridView == null)
                {
                    return new Size(-1, -1);
                }
                DataGridViewRadioButtonFreeDimension freeDimension = DataGridViewCustomCell.GetFreeDimensionFromConstraint(constraintSize);
                Rectangle borderWidthsRect = this.StandardBorderWidths;
                int borderAndPaddingWidths = borderWidthsRect.Left + borderWidthsRect.Width + cellStyle.Padding.Horizontal;
                int borderAndPaddingHeights = borderWidthsRect.Top + borderWidthsRect.Height + cellStyle.Padding.Vertical;
                int preferredHeight = 0, preferredWidth = 0;
                // Assuming here that all radio button states use the same size.

                if (freeDimension != DataGridViewRadioButtonFreeDimension.Width)
                {
                    if (cellStyle.WrapMode == DataGridViewTriState.True)
                    {
                        preferredHeight = ComputeHeightForWrapMode(graphics, cellStyle, rowIndex, constraintSize);
                    }
                    else
                    {
                        preferredHeight = this.Items.Count * (cellStyle.Font.Height + (CELL_MARGIN * 2));
                    }
                }
                if (freeDimension != DataGridViewRadioButtonFreeDimension.Height)
                {
                    //TextFormatFlags flags = TextFormatFlags.Top | TextFormatFlags.Left | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.NoPrefix;
                    TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                    if (this.Items.Count > 0)
                    {
                        // Figure out the width of the longest entry
                        int maxPreferredItemWidth = -1, preferredItemWidth;
                        foreach (object item in this.Items)
                        {
                            string formattedValue = GetFormattedValue(GetItemValue(item), rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.PreferredSize) as string;
                            if (formattedValue != null)
                            {
                                preferredItemWidth = DataGridViewCell.MeasureTextSize(graphics, formattedValue, cellStyle.Font, flags).Width;
                            }
                            else
                            {
                                preferredItemWidth = DataGridViewCell.MeasureTextSize(graphics, " ", cellStyle.Font, flags).Width;
                            }
                            if (preferredItemWidth > maxPreferredItemWidth)
                            {
                                maxPreferredItemWidth = preferredItemWidth;
                            }
                        }
                        preferredWidth = maxPreferredItemWidth;
                    }
                }
                return new Size(preferredWidth, preferredHeight);
            }//END
            private int ComputeHeightForWrapMode(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
            {
                Rectangle borderWidths = BorderWidths(this.DataGridView.AdvancedCellBorderStyle);
                Rectangle valBounds = new Rectangle(0, 0, constraintSize.Width, constraintSize.Height);
                valBounds.Offset(borderWidths.X, borderWidths.Y);
                valBounds.Width -= borderWidths.Right;
                valBounds.Height -= borderWidths.Bottom;
                TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;
                // Figure out the width of the longest entry
                double maxPreferredItemHeight = 0, preferredItemHeight = 0;
                string formattedValue;
                double wordCount;
                Size charSize;
                Size textSize;
                Size spaceSize;
                double charHeight;
                double charWidth;
                double cellWidth;
                double no_of_char_per_line;
                double no_of_line;
                double line_spacing;
                foreach (object item in this.Items)
                {
                    formattedValue = GetFormattedValue(GetItemValue(item), rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.PreferredSize) as string;
                    wordCount = formattedValue.ToCharArray().Length;
                    spaceSize = DataGridViewCell.MeasureTextSize(graphics, " ", cellStyle.Font, flags);
                    if (!String.IsNullOrEmpty(formattedValue))
                        textSize = DataGridViewCell.MeasureTextSize(graphics, formattedValue.ToString(), cellStyle.Font, flags);
                    else
                        textSize = spaceSize;
                    charHeight = textSize.Height;
                    cellWidth = valBounds.Width;
                    if ((textSize.Width) < cellWidth)
                        no_of_line = 1;
                    else
                    {
                        no_of_line = Math.Ceiling((textSize.Width + (spaceSize.Width / 2)) / cellWidth);
                        //Adjustment
                        double times = Math.Ceiling((textSize.Width + (spaceSize.Width / 2)) / cellWidth);
                        double final = times * cellWidth;
                        if ((final > textSize.Width) && ((final - textSize.Width) < 5))
                            no_of_line++;
                    }
                    preferredItemHeight = Math.Ceiling(Convert.ToDouble(no_of_line * textSize.Height));
                    line_spacing = (cellStyle.Font.Height / 4) * (no_of_line - 1);

                    preferredItemHeight += CELL_MARGIN * 2;
                    maxPreferredItemHeight += preferredItemHeight;
                }
                maxPreferredItemHeight = Math.Ceiling(maxPreferredItemHeight);
                return (int)maxPreferredItemHeight;
            }//END
            private void GetWrapText()
            {
            }
            /// <summary>
            /// Helper function that sets the displayMemberProperty member based on the DataSource and the provided displayMember field name
            /// </summary>
            private void InitializeDisplayMemberPropertyDescriptor(string displayMember)
            {
                if (this.DataManager != null)
                {
                    if (String.IsNullOrEmpty(displayMember))
                    {
                        this.displayMemberProperty = null;
                    }
                    else
                    {
                        BindingMemberInfo displayBindingMember = new BindingMemberInfo(displayMember);
                        // make the DataManager point to the sublist inside this.DataSource
                        this.DataManager = this.DataGridView.BindingContext[this.DataSource, displayBindingMember.BindingPath] as CurrencyManager;
                        PropertyDescriptorCollection props = this.DataManager.GetItemProperties();
                        PropertyDescriptor displayMemberProperty = props.Find(displayBindingMember.BindingField, true);
                        if (displayMemberProperty == null)
                        {
                            throw new ArgumentException("Field called '" + displayMember + "' does not exist.");
                        }
                        else
                        {
                            this.displayMemberProperty = displayMemberProperty;
                        }
                    }
                }
            }//END
            /// <summary>
            /// Helper function that sets the valueMemberProperty member based on the DataSource and the provided valueMember field name
            /// </summary>
            private void InitializeValueMemberPropertyDescriptor(string valueMember)
            {
                if (this.DataManager != null)
                {
                    if (String.IsNullOrEmpty(valueMember))
                    {
                        this.valueMemberProperty = null;
                    }
                    else
                    {
                        BindingMemberInfo valueBindingMember = new BindingMemberInfo(valueMember);
                        // make the DataManager point to the sublist inside this.DataSource
                        this.DataManager = this.DataGridView.BindingContext[this.DataSource, valueBindingMember.BindingPath] as CurrencyManager;
                        PropertyDescriptorCollection props = this.DataManager.GetItemProperties();
                        PropertyDescriptor valueMemberProperty = props.Find(valueBindingMember.BindingField, true);
                        if (valueMemberProperty == null)
                        {
                            throw new ArgumentException("Field called '" + valueMember + "' does not exist.");
                        }
                        else
                        {
                            this.valueMemberProperty = valueMemberProperty;
                        }
                    }
                }
            }//END

            /// <summary>
            /// Updates the property descriptors when the cell gets attached to the grid.
            /// </summary>
            protected override void OnDataGridViewChanged()
            {
                if (this.DataGridView != null)
                {
                    // Will throw an error if DataGridView is set and a member is invalid
                    InitializeDisplayMemberPropertyDescriptor(this.DisplayMember);
                    InitializeValueMemberPropertyDescriptor(this.ValueMember);
                }
                base.OnDataGridViewChanged();
            }//END               
            /// <summary>
            /// Paints the entire cell.
            /// </summary>
            protected override void Paint(Graphics graphics,
                Rectangle clipBounds,
                Rectangle cellBounds,
                int rowIndex,
                DataGridViewElementStates cellState,
                object value,
                object formattedValue,
                string errorText,
                DataGridViewCellStyle cellStyle,
                DataGridViewAdvancedBorderStyle advancedBorderStyle,
                DataGridViewPaintParts paintParts)
            {
                ComputeLayout(graphics,
                              clipBounds,
                              cellBounds,
                              rowIndex,
                              cellState,
                              formattedValue,
                              errorText,
                              cellStyle,
                              advancedBorderStyle,
                              paintParts,
                              true  /*paint*/);
            }//END
            /// <summary>
            /// Paints a single item.
            /// </summary>
            private void PaintItem(Graphics graphics,
                                   Rectangle textBoxCellBound,
                                   Rectangle textBoxBound,
                                   int rowIndex,
                                   int itemIndex,
                                   DataGridViewCellStyle cellStyle,
                                   bool itemReadOnly,
                                   bool itemSelected,
                                   bool mouseOverCell,
                                   bool paintFocus,
                                   DataGridViewElementStates cellState)
            {

                object itemFormattedValue = GetFormattedValue(GetItemValue(this.Items[itemIndex]),
                                                        rowIndex,
                                                        ref cellStyle,
                                                        null /*valueTypeConverter*/,
                                                        null /*formattedValueTypeConverter*/,
                                                        DataGridViewDataErrorContexts.Display);
                string itemFormattedText = itemFormattedValue as string;
                if (string.IsNullOrEmpty(itemFormattedText))
                {
                    return;
                }
                else
                {
                    Rectangle textrect = textBoxBound;
                    int textHeight = cellStyle.Font.Height;
                    textrect = textBoxBound;
                    textrect.Y += CELL_MARGIN;
                    if (cellStyle.WrapMode == DataGridViewTriState.False || cellStyle.WrapMode == DataGridViewTriState.NotSet)
                    {
                        textrect.Height = textHeight + (CELL_MARGIN / 2);
                    }
                    else
                    {
                        textrect.Height = textBoxBound.Height - (CELL_MARGIN);
                    }
                    using (Region clipRegion = graphics.Clip)
                    {
                        graphics.SetClip(textBoxBound);
                        SolidBrush brush;
                        if ((cellState & DataGridViewElementStates.Selected) == 0)
                            brush = new SolidBrush(cellStyle.ForeColor);
                        else
                            brush = new SolidBrush(cellStyle.SelectionForeColor);
                        SolidBrush borderbrush = new SolidBrush(this.DataGridView.GridColor);
                        Pen pen = new Pen(borderbrush);
                        if (itemIndex > 0)
                            graphics.DrawLine(pen, textBoxBound.Left, textBoxBound.Top, textBoxBound.Right, textBoxBound.Top);
                        graphics.DrawString(itemFormattedText, cellStyle.Font, brush, textrect);
                        graphics.Clip = clipRegion;
                    }
                }
            }//END
            /// <summary>
            /// Helper function that indicates if a paintPart needs to be painted.
            /// </summary>
            private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart)
            {
                return (paintParts & paintPart) != 0;
            }
            /// <summary>
            /// Custom implementation that follows the standard representation of cell types.
            /// </summary>
            public override string ToString()
            {
                return "DataGridViewCustomCell { ColumnIndex=" + this.ColumnIndex.ToString(CultureInfo.CurrentCulture) + ", RowIndex=" + this.RowIndex.ToString(CultureInfo.CurrentCulture) + " }";
            }

        }//END CLASS   


}
