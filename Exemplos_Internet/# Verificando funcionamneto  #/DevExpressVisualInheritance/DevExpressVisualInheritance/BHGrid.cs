using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Design;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Localization;
using Bloodhound.StrongTypesNS;

namespace Bloodhound
{
    /// <summary>
    /// Grid control. 
    /// </summary>
    [Designer(typeof(BHGridDesigner))]
    public class BHGrid : GridControl, IButtonControl
    {
        //** Variable Declarations

        private bool m_bDelayRowChangedEvent = false;
        private bool m_bActive = false;
        private int m_iPrevTopIndex; // Used for TopRowChanged handler
        private int m_iOldRowID;
        private DataRow m_rowOldRow = null; // Used for SelectedDataRowChanged handler
        private int m_iOldRowNumber = -1;
        private int m_iOldCount = 0;
        private System.Windows.Forms.Timer m_tmrDelayRowChangedEventTimer;
        private bool m_bCanSort = true;
        private bool m_bCheckColumn = false;
        private BHGridMultipleRowSelection m_grdCheck = null;
        private DataRow m_rowSelected = null;
        private ArrayList m_arowSelectedRows;
        private bool m_bInUseByDataObject = false;
        //prevents the grid from remembering the previously selected row when datasource is changed
        private bool m_bPreventRememberRowSelected = false;
        private bool m_bPreventSelectedRowChange = false;
        private bool m_bAllowHighlightedNotSelected = false;

        //** Event Definitons

        /// <summary>
        /// Occurs when the focused row changes due to grid navigation or sorting
        /// </summary>
        [Category("Bloodhound"), Browsable(true),
        Description("Occurs when the focused row changes due to grid navigation " +
            "or sorting.")]
        public event FocusedRowChangedEventHandler FocusedRowChanged;

        /// <summary>
        /// Occurs when the selected data row changes (or no row becomes selected) due to
        /// grid navigation, sorting, or change in datasource
        /// </summary>
        [Category("Bloodhound"), Browsable(true),
        Description("Occurs when the selected data row changes (or no row becomes " +
            "selected) due to grid navigation, sorting, or change in datasource")]
        public event DataRowChangedEventHandler SelectedDataRowChanged;

        /// <summary>
        /// Occurs when the selected row or the number of rows changes (useful for
        /// displaying Record X of Y)
        /// </summary>
        [Category("Bloodhound"), Browsable(true),
        Description("Occurs when the selected row or the number of rows changes " +
            "(useful for displaying Record X of Y)")]
        public event RowOrCountChangedEventHandler SelectedRowOrCountChanged;


        /// <summary>
        /// Occurs when the number of selected rows in a multi-select grid changes
        /// </summary>
        [Category("Bloodhound"), Browsable(true),
        Description("Occurs when the number of selected rows in a multi-select grid changes")]
        public event NumSelRowsChgEventHandler MultiSelectCountChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        public BHGrid()
        {
            this.TabStop = false;
            m_tmrDelayRowChangedEventTimer = new System.Windows.Forms.Timer();
            m_tmrDelayRowChangedEventTimer.Interval = 250;
            m_tmrDelayRowChangedEventTimer.Tick +=
                new System.EventHandler(this.tmrDelayRowChangedEventTimer_Tick);
            m_arowSelectedRows = new ArrayList();
        }

        //** Property Definitons

        /// <summary>
        /// Override
        /// </summary>
        new public object DataSource
        {
            get { return base.DataSource; }
            set
            {
                if (m_bInUseByDataObject)
                {
                    return;
                }

                if (m_grdCheck != null)
                    m_grdCheck.ClearSelection();
                m_bPreventRememberRowSelected = true;
                base.DataSource = value;
                m_bPreventRememberRowSelected = false;
                this.TopRowIndex = 0;
            }
        }

        /// <summary>
        /// Gets or sets the boolean value that determines if a 250ms pause will occur
        /// before firing the SelectedDataRowChanged event
        /// </summary>
        [Category("Bloodhound"), Browsable(true), DefaultValue(false),
        Description("Gets or sets the boolean value that determines if a 250ms pause will " +
            "occur before firing the SelectedDataRowChanged event")]
        public bool DelayRowChangedEvent
        {
            get { return m_bDelayRowChangedEvent; }
            set { m_bDelayRowChangedEvent = value; }
        }

        /// <summary>
        /// Gets or sets the boolean value that determines if the grid can be sorted manually
        /// </summary>
        [Category("Bloodhound"), Browsable(true), DefaultValue(true),
        Description("Gets or sets the boolean value that determines if the grid can be " +
            "sorted manually")]
        public bool CanSort
        {
            get { return m_bCanSort; }
            set
            {
                m_bCanSort = value;
                GridView gv = this.MainView as GridView;
                if (gv != null)
                    gv.OptionsCustomization.AllowSort = value;
            }
        }

        /// <summary>
        /// Gets or sets the boolean value that determines if the grid needs checkbox column
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool AllowMultiSelectGrid
        {
            get { return m_bCheckColumn; }
            set
            {
                m_bCheckColumn = value;
                GridView gv = this.MainView as GridView;
                if (gv != null && value && m_grdCheck == null)
                {
                    m_grdCheck = new BHGridMultipleRowSelection(gv, AllowHighlightedNotSelected);
                    m_grdCheck.NumberOfSelectedRowsChanged +=
                        new NumSelRowsChgEventHandler(m_grdCheck_NumberOfSelectedRowsChanged);
                    m_grdCheck.CheckMarkColumn.VisibleIndex = 0;
                }
                else if (m_grdCheck != null && !value)
                {
                    m_grdCheck.Detach();
                    m_grdCheck.NumberOfSelectedRowsChanged -=
                        new NumSelRowsChgEventHandler(m_grdCheck_NumberOfSelectedRowsChanged);
                    m_grdCheck = null;
                    AllowHighlightedNotSelected = false;
                }
            }
        }

        [Browsable(false),
         DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        internal bool IsActiveGrid
        {
            get { return m_bActive; }
            set
            {
                m_bActive = value;
                SetSelectedRowColor();
            }
        } // end IsActiveGrid

        /// <summary>
        /// Gets the parent (BHForm) for this control. If the parent form is not a BHForm,
        /// returns null;
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public BHForm ParentBHForm
        {
            get
            {
                Control ctlParent = this.Parent;
                while (ctlParent != null)
                {
                    if (ctlParent is BHForm)
                        return (BHForm)ctlParent;
                    else
                        ctlParent = ctlParent.Parent;
                }
                return null;
            }
        }

        // Implement IButtonControl interface, so that the
        // Grid's double-click can be used as a "default button" action
        /// <summary>
        /// Always returns DialogResult.None. Setting it has no effect.
        /// This is needed to support setting grid as default button.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public DialogResult DialogResult
        {
            get { return DialogResult.None; }
            set { }
        }

        /// <summary>
        /// Gets the DataRow for the focused row of the grid or null if there is no focused row 
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public DataRow SelectedDataRow
        {
            get
            {
                GridView gv = this.MainView as GridView;
                if (gv != null && gv.FocusedRowHandle >= 0)
                    return gv.GetDataRow(gv.FocusedRowHandle);
                else
                    return null;
            }
        }

        /// <summary>
        /// Gets or Sets the row handles that are selected in multiselect mode
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public ArrayList SelectedRowHandles
        {
            get
            {
                if (m_grdCheck == null)
                    return null;
                else
                    return m_grdCheck.SelectedRowHandles;
            }
            set
            {
                if (m_grdCheck != null)
                    m_grdCheck.SelectedRowHandles = value;
            }
        }

        /// <summary>
        /// Gets the DataRow for the focused row of the grid or null if there is no focused row 
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public DataRow GetPreviousDataRow
        {
            get
            {
                GridView gv = this.MainView as GridView;
                if (gv != null && gv.FocusedRowHandle - 1 >= 0)
                    return gv.GetDataRow(gv.FocusedRowHandle - 1);
                else
                    return null;
            }
        }

        /// <summary>
        /// Gets the DataRow for the focused row of the grid or null if there is no focused row 
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public DataRow GetNextDataRow
        {
            get
            {
                GridView gv = this.MainView as GridView;
                if (gv != null && gv.FocusedRowHandle + 1 < gv.DataRowCount)
                    return gv.GetDataRow(gv.FocusedRowHandle + 1);
                else
                    return null;
            }
        }

        /// <summary>
        /// Gets or Sets the focused Row by line number (0 is topmost row)
        /// Gets -1 if there is no focused row.
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get
            {
                GridView gv = this.MainView as GridView;
                if (gv != null && gv.DataRowCount > 0 && gv.FocusedRowHandle >= 0 &&
                    gv.RowHandle2VisibleIndex(gv.FocusedRowHandle) >= 0)
                    return gv.RowHandle2VisibleIndex(gv.FocusedRowHandle);
                else
                    return -1;
            }

            set
            {
                GridView gv = this.MainView as GridView;
                if (gv != null)
                    gv.FocusedRowHandle = gv.GetVisibleRowHandle(value);
            }
        }

        /// <summary>
        /// Gets or Sets the boolean value indicating if there is a selected row in the grid
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool IsRowSelected
        {
            get
            {
                return (SelectedIndex != -1);
            }
        }

        /// <summary>
        /// Gets the number of rows in the grid that contain data
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public int RowCount
        {
            get
            {
                GridView gv = this.MainView as GridView;
                if (gv != null)
                    return gv.DataRowCount;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Gets the number of selected rows in the grid
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public int NumberOfSelectedRows
        {
            get
            {
                if (this.AllowMultiSelectGrid && m_grdCheck != null)
                    return m_grdCheck.SelectedCount;
                else if (IsRowSelected)
                    return 1;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Gets or Sets the row by line number (0 is the topmost row) that appears at the
        /// top of the visible section of the grid
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public int TopRowIndex
        {
            get
            {
                GridView gv = this.MainView as GridView;
                if (gv != null)
                    return gv.TopRowIndex;
                else
                    return -1;
            }

            set
            {
                GridView gv = this.MainView as GridView;
                if (gv != null)
                    gv.TopRowIndex = value;
            }
        }

        /// <summary>
        /// Gets all the fields in the grid
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public FieldListDataTable AllFields
        {
            get
            {
                GridView gv = this.MainView as GridView;
                if (gv != null)
                {
                    FieldListDataTable tblFields = new FieldListDataTable();
                    tblFields.AddFieldListRow("rec_id", "character");
                    string sTypeString = "";
                    foreach (GridColumn gc in gv.Columns)
                    {
                        if (gc.FieldName == null || gc.FieldName == "")
                            continue;
                        if (gc.ColumnEdit is RepositoryItemCheckEdit)
                            sTypeString = "logical";
                        else
                        {
                            switch (gc.DisplayFormat.FormatType.ToString())
                            {
                                case "Numeric":
                                    if (gc.DisplayFormat.FormatString == "")
                                        sTypeString = "integer";
                                    else if (gc.DisplayFormat.FormatString == "c" ||
                                        gc.DisplayFormat.FormatString.StartsWith("p"))
                                        sTypeString = "decimal";
                                    break;
                                case "DateTime":
                                    sTypeString = "date";
                                    break;
                                default:
                                    sTypeString = "character";
                                    break;
                            }
                        }

                        if (sTypeString != "")
                            tblFields.AddFieldListRow(gc.FieldName, sTypeString);
                        else
                        {
                            // should probably throw an exception here if there is a type
                            // that's not getting handled
                        }
                    }
                    return tblFields;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Gets or sets a boolean value that determines if a Data Object is using this Grid
        /// </summary>
        /// <remarks>If true, The DataSource should not be changed manually</remarks>
        [Browsable(false),
        DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool InUseByDataObject
        {
            get { return m_bInUseByDataObject; }
            set { value = m_bInUseByDataObject; }
        }

        /// <summary>
        /// Gets or sets whether the SelectedDataRowChanged event should be fired
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool PreventSelectedRowChange
        {
            get { return m_bPreventSelectedRowChange; }
            set { m_bPreventSelectedRowChange = value; }
        }

        /// <summary>
        /// Gets or sets whether this multi-select grid allows a row to be highlighted
        /// without being selected
        /// </summary>
        [Browsable(false),
        DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool AllowHighlightedNotSelected
        {
            get 
            {
                return m_bAllowHighlightedNotSelected;
            } 
            set
            {
                m_bAllowHighlightedNotSelected = value;
                if (AllowMultiSelectGrid && m_grdCheck != null)
                    m_grdCheck.AllowHighlightedNotSelected = value;
            }
        }

        //** Method Definitons

        /// <summary>
        /// Gets the DataRows for all selected rows of the grid or null if there is no focused row 
        /// </summary>
        /// <param name="bIncludeFocusedRowEvenIfNotSelected"></param>
        public ArrayList GetSelectedDataRows(bool bIncludeFocusedRowEvenIfNotSelected)
        {
            ArrayList arowColln = new ArrayList();
            if (AllowMultiSelectGrid)
            {
                ArrayList aiSelectRowHandles = m_grdCheck.SelectedRowHandles;
                GridView gv = MainView as GridView;
                if (aiSelectRowHandles.Count == 0 && bIncludeFocusedRowEvenIfNotSelected &&
                    SelectedDataRow != null)
                    arowColln.Add(SelectedDataRow);
                else
                {
                    for (int i = 0; i < aiSelectRowHandles.Count; i++)
                        arowColln.Add(gv.GetDataRow((int)aiSelectRowHandles[i]));
                }
            }
            else
            {
                if (bIncludeFocusedRowEvenIfNotSelected && SelectedDataRow != null)
                    arowColln.Add(SelectedDataRow);
            }
            return arowColln;
        }

        /// <summary>
        /// Fires the FocusedRowChanged event for the grid
        /// </summary>
        /// <param name="e"></param>
        /// <remarks>
        /// This can happen either when the focus is set to a different row (with the
        /// arrow keys, for example), or when the contents of a row change due to
        /// sorting.
        /// </remarks>
        protected virtual void OnFocusedRowChanged(FocusedRowChangedEventArgs e)
        {
            if (FocusedRowChanged != null)
                FocusedRowChanged(this, e);
        }

        /// <summary>
        /// Triggers the SelectedDataRowChanged event
        /// </summary>
        protected virtual void OnSelectedDataRowChanged()
        {
            if (SelectedDataRowChanged != null && !m_bPreventSelectedRowChange)
                SelectedDataRowChanged(this, new DataRowChangedEventArgs(SelectedDataRow));
        }

        /// <summary>
        /// Triggers the SelectedRowOrCountChanged event
        /// </summary>
        protected virtual void OnSelectedRowOrCountChanged()
        {
            if (SelectedRowOrCountChanged != null)
                SelectedRowOrCountChanged(this,
                    new RowOrCountChangedEventArgs(SelectedIndex + 1, RowCount));
        }

        // This method tests if there is a new datarow selected and fires an event if there is
        private void TestForSelectedDataRowChange()
        {
            // m_rowOldRow keeps track of the last selected datarow from the last event
            // SelectedDataRow returns the current datarow
            if (SelectedDataRow != m_rowOldRow)
            {
                // If DelayRowChangedEvent is true, then we start the timer, otherwise, just
                // trigger the event
                if (m_bDelayRowChangedEvent)
                    m_tmrDelayRowChangedEventTimer.Start();
                else
                    OnSelectedDataRowChanged();

                m_rowOldRow = SelectedDataRow;
            }

            // m_iOldRowNumber and m_iOldCount keep track of the last values from
            // the last event
            if (SelectedIndex != m_iOldRowNumber || RowCount != m_iOldCount)
            {
                OnSelectedRowOrCountChanged();

                m_iOldRowNumber = SelectedIndex;
                m_iOldCount = RowCount;
            }
        }

        private void SetSelectedRowColor()
        {
            GridView gv = this.MainView as GridView;
            if (gv != null)
            {
                // If the grid is the form's active grid and it is enabled
                if (m_bActive && this.Enabled)
                    gv.ViewStyles.AddReplace("HideSelectionRow", "FocusedRow");
                else
                    gv.ViewStyles.AddReplace("HideSelectionRow", "HideSelectionRow");
            }
        }

        /// <summary>
        /// Sets the Enabled property and changes the Focused Row Color
        /// </summary>
        /// <remarks>Needed because the Enabled propety cannot be overriden
        /// and the EnableChanged event does not work</remarks>
        /// <param name="IsEnabled"></param>
        public void Enable(bool IsEnabled)
        {
            this.Enabled = IsEnabled;
            SetSelectedRowColor();
        }

        /// <summary>
        /// Clears the current sort order
        /// </summary>
        public void ClearCurrentSort()
        {
            GridView gv = this.MainView as GridView;
            gv.ClearSorting();
        }

        /// <summary>
        /// Does Nothing. This is needed to support setting grid as default button.
        /// </summary>
        /// <param name="value"></param>
        public void NotifyDefault(bool value) { }

        /// <summary>
        /// Raises the grid's DoubleClick event. This will be used on "return" if
        /// BHGrid is set as the Form's AcceptButton.
        /// </summary>
        public void PerformClick()
        {
            if (this.CanSelect)
            {
                this.OnDoubleClick(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Do Keystroke handling for the grid
        /// </summary>
        /// <param name="keyData">The keystroke to possibly handle</param>
        /// <returns>whether the keystroke was handled</returns>
        /// <remarks>
        /// The calling form can call this function from ProcessCmdKey. If
        /// the result is false, then calling form should call base.ProcessCmdKey
        /// <c>
        /// protected override bool ProcessCmdKey(ref Message msg,Keys keyData)
        /// {
        ///     bool bHandled
        ///     
        ///     if ( this.CurrentGrid != null )
        ///     {
        ///        bHandled = CurrentGrid.HandleKeyStroke;
        ///     } 
        ///     
        ///     if ( bHandled )
        ///         return true;
        ///     else
        ///        return base.ProcessCmdKey(ref msg, keyData);
        ///  }
        /// </c>
        /// </remarks>
        internal bool HandleKeyStroke(Keys keyData)
        {
            GridView gv = MainView as GridView;
            if (gv == null)
                return false;
            // For spacebar to take effect ona multi-select grid, the grid should have focus
            if (keyData == Keys.Space && !this.Focused)
                return false;
            if (this.AllowMultiSelectGrid)
                return m_grdCheck.HandleKey(keyData);
            else if (keyData == BHKey.Up)
            {
                gv.MovePrev();
                return true;
            }
            else if (keyData == BHKey.Down)
            {
                gv.MoveNext();
                return true;
            }
            else if (keyData == BHKey.PageUp)
            {
                gv.MovePrevPage();
                return true;
            }
            else if (keyData == BHKey.PageDown)
            {
                gv.MoveNextPage();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Suppress Grid Keys
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="onlyEvent"></param>
        /// <returns></returns>
        protected override bool ProcessGridKeys(KeyEventArgs keys, bool onlyEvent)
        {
            return true;
        }

        // Use reflection to get the GridViewInfo for the current GridView 
        // Reflection allows us to get around the proteced access level
        private GridViewInfo GetViewInfo(GridView view)
        {
            Type type = view.GetType();
            PropertyInfo pi = type.GetProperty("ViewInfo", BindingFlags.Instance |
                BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            return pi.GetValue(view, null) as GridViewInfo;
        }

        private int GetRowID(DataRow rowDataRow)
        {
            Type type = rowDataRow.GetType();
            FieldInfo objFieldInfo = type.GetField("_rowID", BindingFlags.Instance | BindingFlags.NonPublic);
            return Convert.ToInt32(objFieldInfo.GetValue(rowDataRow));
        }

        /// <summary>
        /// Returns whether a column with a particular filedname exists in the grid
        /// </summary>
        /// <param name="sFieldName"></param>       
        /// <returns></returns>
        public int ColumnIndex(string sFieldName)
        {
            GridView gv = this.MainView as GridView;
            int iIndex = -1;
            foreach (GridColumn grdFind in gv.Columns)
            {
                if (grdFind.FieldName == sFieldName)
                {
                    iIndex = grdFind.AbsoluteIndex;
                    break;
                }
            }
            return iIndex;
        }

        /// <summary>
        /// Adds a column to an existing grid
        /// </summary>
        /// <param name="rowColumnParam"></param>
        public void AddColumn(ColumnParamRow rowColumnParam)
        {
            if (rowColumnParam == null)
                return;
            GridView gv = this.MainView as GridView;            
            string sFormat = rowColumnParam.DataType.ToLower();
            if (gv != null)
            {
                GridColumn newColumn = new GridColumn();
                newColumn.Caption = rowColumnParam.ColumnHeader;
                newColumn.FieldName = rowColumnParam.FieldName;
                newColumn.VisibleIndex = gv.Columns.Count;
                newColumn.Width = rowColumnParam.ColumnWidth;
                if (sFormat == "money")
                {
                    newColumn.DisplayFormat.FormatType = FormatType.Numeric;
                    newColumn.DisplayFormat.FormatString = "c";
                }
                else if (sFormat == "date")
                {
                    newColumn.DisplayFormat.FormatType = FormatType.DateTime;
                    newColumn.DisplayFormat.FormatString = "MM/dd/yyyy";
                }
                else if (sFormat == "logical")
                {
                    RepositoryItemCheckEdit newCheckEdit = new RepositoryItemCheckEdit();
                    this.RepositoryItems.Add(newCheckEdit);
                    newColumn.ColumnEdit = newCheckEdit;
                    newCheckEdit.AutoHeight = false;
                }
                gv.Columns.Add(newColumn);
            }
        }

        /// <summary>
        /// Remove a particular column from the grid
        /// </summary>
        /// <param name="sFieldName"></param>
        /// <returns></returns>
        public bool RemoveColumn(string sFieldName)
        {
            int iColumnIndex = ColumnIndex(sFieldName);
            if (iColumnIndex >= 0)
            {
                GridView gv = this.MainView as GridView;
                if (gv != null)
                {
                    gv.Columns.RemoveAt(iColumnIndex);                    
                    return true;
                }
            }            
            return false;
        }

        /// <summary>
        /// Remove all columns from a Grid
        /// </summary>
        public void RemoveColumns()
        {
            GridView gv = this.MainView as GridView;

            while (gv != null && gv.Columns.Count > 0)
            {
                if (AllowMultiSelectGrid && gv.Columns[0] == m_grdCheck.CheckMarkColumn)
                    break;
                gv.Columns[0].Dispose();
            }
            while (gv != null && gv.Columns.Count > 1)
            {
                gv.Columns[1].Dispose();
            }
        }

        /// <summary>
        /// Gets the DataRow associated with a particular grid row number, where zero is the
        /// topmost visible row or null if there is no data at that row
        /// </summary>
        /// <param name="iGridRow"></param>
        /// <returns></returns>
        public DataRow GetDataRow(int iGridRow)
        {
            GridView gv = this.MainView as GridView;
            if (gv != null && iGridRow >= 0)
            {
                int iRowHandle = gv.GetVisibleRowHandle(iGridRow);
                if (iRowHandle != GridControl.InvalidRowHandle)
                    return gv.GetDataRow(iRowHandle);
            }
            return null;
        }

        /// <summary>
        /// Removes the Selected Row from the grid and the data source table
        /// </summary>
        public bool RemoveCurrentRow()
        {
            return RemoveRow(this.SelectedDataRow);
        }

        /// <summary>
        /// Removes a particular row from the grid and the data source table
        /// </summary>
        public bool RemoveRow(DataRow rowDelete)
        {
            DataTable tblSource = DataSource as DataTable;

            if (tblSource != null && rowDelete != null)
            {
                tblSource.Rows.Remove(rowDelete);
                if (this.AllowMultiSelectGrid && m_grdCheck.SelectedCount > 0)
                    m_grdCheck.HandleDelete();
                TestForSelectedDataRowChange();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// If there are 1 or more selected rows, then make sure the focused row is one of them.  If
        /// not, put focus on the first selected row
        /// </summary>
        public void FixFocusedRow()
        {
            if (this.SelectedRowHandles.Count > 0 &&
                !this.SelectedRowHandles.Contains(this.SelectedIndex))
            {
                // Highlight the first selected row
                this.SelectedIndex = (int)this.SelectedRowHandles[0];
            }
        }

        /// <summary>
        /// Changes the height of the grid, so that an exact number of rows is visible
        /// </summary>
        /// <param name="iRowCount"></param>
        public void SetNumberOfVisibleRows(int iRowCount)
        {
            Height = GetNumberOfVisibleRows(iRowCount);
        }

        /// <summary>
        /// Gets the height of the grid, so that an exact number of rows is visible
        /// </summary>
        /// <param name="iRowCount"></param>
        public int GetNumberOfVisibleRows(int iRowCount)
        {            
            Type type = MainView.GetType();
            FieldInfo fi = type.GetField("fViewInfo", BindingFlags.NonPublic | BindingFlags.Instance);
            GridViewInfo info = fi.GetValue(MainView) as GridViewInfo;

            int iNewHeight = info.ViewRects.GroupPanel.Height // Probably 0
                + info.ViewRects.ColumnPanel.Height // Column Labels Height
                + info.ViewRects.FilterPanel.Height // Probably 0
                + info.ViewRects.Footer.Height // Probably 0
                + (Height - info.ViewRects.Client.Height) // Border Height
                + (iRowCount * (info.MinRowHeight + 1)); // Height of Rows

            return iNewHeight;
        }

        /// <summary>
        /// Unselects all rows in a grid
        /// </summary>
        public void UnSelectAllRows()
        {
            if (m_grdCheck != null)
                m_grdCheck.ClearSelection();
        }

        /// <summary>
        /// Selects all rows in a grid
        /// </summary>
        public void SelectAllRows()
        {
            if (m_grdCheck != null)
                m_grdCheck.SelectAll();
        }

        /// <summary>
        /// Select multiple rows in a multi select grid
        /// </summary>
        /// <param name="tblRowIDs"></param>
        public void SelectMultipleRows(RowIDStringDataTable tblRowIDs)
        {
            if (tblRowIDs == null)
                return;
            else if (tblRowIDs.Rows.Count == 0 || RowCount == 0)
                return;
            ArrayList aiRowHandles = new ArrayList();
            foreach (RowIDStringRow drRowID in tblRowIDs.Rows)
            {
                string sRowID = drRowID.IDString;
                for (int i = 0; i < RowCount; i++)
                {
                    DataRow drCurrent = GetDataRow(i);
                    if (drCurrent != null)
                    {
                        if ((string)drCurrent["rec_id"] == sRowID)
                        {
                            aiRowHandles.Add(i);
                            break;
                        }
                    }
                }
            }
            SelectedRowHandles = aiRowHandles;
        }

        /// <summary>
        /// Gets the list of all rowids that are selected in a multi-select grid
        /// </summary>
        /// <param name="bIncludeFocusedRowEvenIfNotSelected"></param>
        /// <returns></returns>
        public RowIDStringDataTable GetSelectedRowIDs(bool bIncludeFocusedRowEvenIfNotSelected)
        {
            RowIDStringDataTable tblRowIDS = new RowIDStringDataTable();
            if (RowCount > 0)
            {
                try
                {
                    ArrayList aDataRowColln =
                        GetSelectedDataRows(bIncludeFocusedRowEvenIfNotSelected);
                    foreach (DataRow rowSelect in aDataRowColln)
                    {
                        if (rowSelect != null)
                            tblRowIDS.AddRowIDStringRow((string)rowSelect["rec_id"]);
                    }
                }
                catch
                {
                    return (new RowIDStringDataTable());
                }
            }
            return tblRowIDS;
        }

        /// <summary>
        /// Sets focus on a specific rowid
        /// </summary>
        /// <param name="sRowID"></param>
        /// <returns></returns>
        public bool SetFocusOnRowID(string sRowID)
        {
            if (sRowID == "")
                return false;
            for (int i = 0; i < RowCount; i++)
            {
                DataRow drCurrent = GetDataRow(i);
                if (drCurrent != null)
                {
                    if ((string)drCurrent["rec_id"] == sRowID)
                    {
                        SelectedIndex = i;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the focussed rowid
        /// </summary>
        /// <returns></returns>
        public string GetSelectedRowID()
        {
            if (RowCount == 0)
                return "";
            DataRow rowSelect = SelectedDataRow;
            if (rowSelect != null)
            {
                try
                {
                    return (string)rowSelect["rec_id"];
                }
                catch
                {
                }
            }
            return "";
        }

        //** Event Handlers

        /// <summary>
        /// After calling SizeChanged event handlers, ensure the focused row is visible.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            int iBottomRow = 0;
            base.OnSizeChanged(e);
            if (this.CanSelect)
            {
                GridView gridView1 = this.MainView as GridView;
                // If the focussed row is not visible, find the last row that is visible when
                // the grid is reset and scroll by the difference.
                if (gridView1 != null && gridView1.RowCount > 0)
                {
                    // Bottom Row must be greater than or equal to Top Row
                    iBottomRow = gridView1.TopRowIndex;
                    if (gridView1.IsRowVisible(gridView1.FocusedRowHandle) !=
                        RowVisibleState.Visible)
                    {
                        while (gridView1.IsRowVisible(iBottomRow) == RowVisibleState.Visible)
                        {
                            iBottomRow++;
                        }
                        int iRowsShowing = iBottomRow - gridView1.TopRowIndex;
                        gridView1.TopRowIndex = gridView1.FocusedRowHandle - iRowsShowing + 1;
                    }
                }
            }
        }

        /// <summary>
        /// Attach TopRowChanged event to GridView when loading.
        /// </summary>
        protected override void OnLoaded()
        {
            GridView gv = this.MainView as GridView;
            if (gv != null)
            {
                // We'll handle TopRowChanged to keep the focus always in view
                gv.TopRowChanged += new EventHandler(gv_TopRowChanged);
                // We will handle both FocusedRowChanged and EndSorting, to fire a new
                // BHGrid.FocusedRowChanged event.
                gv.FocusedRowChanged += new FocusedRowChangedEventHandler(gv_FocusedRowChanged);
                gv.EndSorting += new EventHandler(gv_EndSorting);
                gv.StartSorting += new EventHandler(gv_StartSorting);
                gv.OptionsCustomization.AllowGroup = false;
                gv.OptionsCustomization.AllowSort = this.m_bCanSort;
                gv.ShowGridMenu += new GridMenuEventHandler(gv_ShowGridMenu);
                gv.ViewStyles.AddReplace("FocusedCell", "FocusedRow");
            }
            base.OnLoaded();
        }

        // Switch focused row to something visible if necessary
        private void gv_TopRowChanged(object sender, EventArgs e)
        {
            if (m_grdCheck == null || m_grdCheck.SelectedCount == 0)
            {
                GridView view = sender as GridView;
                if (view.IsRowVisible(view.FocusedRowHandle) != RowVisibleState.Visible)
                {

                    if (view.TopRowIndex > m_iPrevTopIndex)
                        view.FocusedRowHandle = view.GetVisibleRowHandle(view.TopRowIndex);
                    else
                    {
                        GridViewInfo vi = GetViewInfo(view);
                        if (vi.RowsInfo.Count > 0)
                        {
                            int lastRow = vi.RowsInfo[vi.RowsInfo.Count - 1].RowHandle;
                            if (vi.RowsInfo.Count > 1 &&
                                view.IsRowVisible(lastRow) == RowVisibleState.Partially)
                            {
                                lastRow = vi.RowsInfo[vi.RowsInfo.Count - 2].RowHandle;
                            }
                            view.FocusedRowHandle = lastRow;
                        }
                    }
                }
                m_iPrevTopIndex = view.TopRowIndex;
            }
        }

        // Raise BHGrid.FocusedRowChanged event if there is really a new row
        private void gv_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView gv = this.MainView as GridView;
            DataRowView rv = gv.GetRow(gv.FocusedRowHandle) as DataRowView;
            if (rv != null)
            {
                int iFocusedRowID = GetRowID(rv.Row);
                if (iFocusedRowID != m_iOldRowID)
                {
                    m_iOldRowID = iFocusedRowID;
                    OnFocusedRowChanged(e);
                }
            }
            else // There is no focused row now... should we raise event?
            {
                m_iOldRowID = -1;
            }
            TestForSelectedDataRowChange();

        }

        /// <summary>
        /// Override
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            // Due to a bug in DevExpress and .Net 2.0
            //  When right clicking on any column header, Bloodhound would crash
            //  Error is in the DevExpress painting/redrawing issues
            if (e.Button == MouseButtons.Right)
                return;

            if (AllowMultiSelectGrid)
                if (m_grdCheck.PerformMouseOperations(e)) return;
            base.OnMouseDown(e);
        }



        /// <summary>
        /// Override
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDoubleClick(EventArgs e)
        {
            GridView gv = MainView as GridView;
            if (gv != null)
            {
                Point pntClickPoint = gv.GridControl.PointToClient(Control.MousePosition);
                GridHitInfo objHitInfo = gv.CalcHitInfo(pntClickPoint);
                if (objHitInfo.InColumn)
                    return;
            }
            base.OnDoubleClick(e);
        }



        // Raise BHGrid.FocusedRowChanged event if sorting causes the highlighted
        // row to change
        private void gv_EndSorting(object sender, EventArgs e)
        {
            GridView gv = this.MainView as GridView;
            if (gv != null)
            {
                int iSelectedIndex = 0;
                ArrayList tblRowHandles = new ArrayList();
                for (int i = 0; i < gv.DataRowCount; i++)
                {
                    DataRow drCurrent = gv.GetDataRow(i);
                    if (m_arowSelectedRows.IndexOf(drCurrent) != -1)
                        tblRowHandles.Add(i);
                    if (drCurrent == m_rowSelected && !m_bPreventRememberRowSelected)
                        iSelectedIndex = i;
                }
                if (AllowMultiSelectGrid)
                    this.SelectedRowHandles = tblRowHandles;
                if (gv.IsRowVisible(iSelectedIndex) != RowVisibleState.Visible)
                    gv.TopRowIndex = iSelectedIndex;
                gv.FocusedRowHandle = iSelectedIndex;
                this.OnSelectedDataRowChanged();
            }
        }

        private void tmrDelayRowChangedEventTimer_Tick(object sender, System.EventArgs e)
        {
            m_tmrDelayRowChangedEventTimer.Stop();
            OnSelectedDataRowChanged();
        }

        /// <summary>
        /// Override
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            // Make this Grid be the selected grid (takes the keystrokes) when it is
            // clicked
            if (this.ParentBHForm != null && this.ParentBHForm.CurrentGrid != null &&
                this.ParentBHForm.CurrentGrid != this && this.Visible && this.Enabled)
            {
                this.ParentBHForm.CurrentGrid = this;
            }
        }

        private DXMenuItem GetItemByStringId(DXPopupMenu menu, GridStringId id)
        {
            foreach (DXMenuItem item in menu.Items)
                if (item.Caption == GridLocalizer.Active.GetLocalizedString(id))
                    return item;
            return null;
        }

        private void gv_ShowGridMenu(object sender, GridMenuEventArgs e)
        {
            if (e.MenuType == GridMenuType.Column)
            {
                // Customize
                DXMenuItem mnuCustomize = GetItemByStringId(e.Menu, GridStringId.MenuColumnColumnCustomization);
                if (mnuCustomize != null)
                    mnuCustomize.Visible = false;

                // Group By This Column
                DXMenuItem mnuGroup = GetItemByStringId(e.Menu, GridStringId.MenuColumnGroup);
                if (mnuGroup != null)
                    mnuGroup.Visible = false;

                DXMenuItem mnuClearFilter = GetItemByStringId(e.Menu, GridStringId.MenuColumnClearFilter);
                if (mnuClearFilter != null)
                    mnuClearFilter.Visible = false;
            }
        }

        /// <summary>
        /// Occurs when MultiSelectCountChanged event is fired
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMultiSelectCountChanged(NumSelRowsChgEventArgs e)
        {
            if (MultiSelectCountChanged != null)
                MultiSelectCountChanged(this, e);
        }

        private void m_grdCheck_NumberOfSelectedRowsChanged(object sender, NumSelRowsChgEventArgs e)
        {
            OnMultiSelectCountChanged(e);
        }

        private void gv_StartSorting(object sender, EventArgs e)
        {
            m_arowSelectedRows = GetSelectedDataRows(false);
            m_rowSelected = SelectedDataRow;
        }
    }

    /// <summary>
    /// Designer class for extending the design mode behaviour of a BHGrid
    /// </summary>
    public class BHGridDesigner : GridControlDesigner
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public BHGridDesigner()
        {
            Verbs.Add(new DesignerVerb("Set GridView Defaults", new EventHandler(OnSetGridViewDefaults)));
        }

        //** Methods Definitons

        /// <summary>
        /// This method sets all default settings on the grid view control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSetGridViewDefaults(object sender, EventArgs e)
        {
            GridView gv = Grid.MainView as GridView;

            gv.ViewStylesInfo.FocusedCell = gv.ViewStylesInfo.FocusedRow;
            gv.OptionsBehavior.Editable = false;
            gv.OptionsCustomization.AllowFilter = false;
            gv.OptionsCustomization.AllowGroup = false;
            gv.OptionsNavigation.AutoFocusNewRow = true;
            gv.OptionsNavigation.UseTabKey = false;
            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsView.ShowIndicator = false;
            gv.OptionsView.ShowGroupedColumns = false;
            gv.OptionsView.ShowFilterPanel = false;

            FireChanged();
        }

        /// <summary>
        /// This method filters out all the unwanted properties of the control.
        /// </summary>
        /// <param name="properties"></param>
        protected override void PostFilterProperties(IDictionary properties)
        {
            BHControlDesigner.RemoveNeverUsedProperties(properties);

            properties.Remove("DataMember");
            properties.Remove("DataSource");
            properties.Remove("ExternalRepository");
            properties.Remove("LookAndFeel");
            properties.Remove("MenuManager");
            properties.Remove("TabIndex");
            properties.Remove("TabStop");
            properties.Remove("Text");
            properties.Remove("ToolTipController");
            properties.Remove("UseEmbeddedNavigator");
        }

        /// <summary>
        /// This method filters out all the unwanted events that this conttrol can raise.
        /// </summary>
        /// <param name="events"></param>
        protected override void PostFilterEvents(IDictionary events)
        {
            BHControlDesigner.RemoveNeverUsedEvents(events);

            events.Remove("DragDrop");
            events.Remove("DragOver");
            events.Remove("DragLeave");
            events.Remove("DragEnter");
            events.Remove("EditorKeyUp");
            events.Remove("EditorKeyPress");
            events.Remove("EditorKeyDown");
        }

        private void FireChanged()
        {
            IComponentChangeService srv =
                Selector.EditingGrid.InternalGetService(typeof(IComponentChangeService)) as IComponentChangeService;
            if (srv != null)
                srv.OnComponentChanged(Selector.EditingGrid, null, null, null);
        }
    }
}
