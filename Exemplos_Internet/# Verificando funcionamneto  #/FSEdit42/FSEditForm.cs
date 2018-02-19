using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SAS.Shared.AddIns;

namespace SAS.Tasks.Examples.FSEdit
{
    public partial class FSEditForm : SAS.Tasks.Toolkit.Controls.TaskForm
    {
        private System.Data.DataTable dt;
        private System.Data.DataSet ds;
        private String strMember;
        private String strLibrary;
        private String strUniqueIdentifier;
        private int nRow;
        private int nPreviousTabIndex;
        private CustomPropertyTable editTable;
        private CustomPropertyTable insertTable;
        private bool bEmptyDataSource;
        private bool bShowOkWarning;
        private bool bShowArrowWarning;
        private bool bShowDeleteWarning;
        private bool bIgnoreEvents;
        private OleDbDataAdapter adapter;
        private OleDbConnection dbConnection;
        private String strSelectClause;

        public FSEditForm(SAS.Shared.AddIns.ISASTaskConsumer3 consumer, String uniqueIdentifier)
        {
            strUniqueIdentifier = uniqueIdentifier;
            Consumer = consumer;
            InitializeComponent();
            nPreviousTabIndex = 0;
            bShowOkWarning = true;
            bShowArrowWarning = true;
            bShowDeleteWarning = true;
            bIgnoreEvents = false;
            strMember = Consumer.ActiveData.Member;
            strLibrary = consumer.ActiveData.Library;
            dt = new DataTable(strMember);
            ds = new System.Data.DataSet();
            ds.Tables.Add(dt);
            nRow = (int)udObs.Value;
            editTable = new CustomPropertyTable();
            insertTable = new CustomPropertyTable();
            strSelectClause = "select * from " + strLibrary + "." + strMember;
            bCommit.Enabled = false;

            SAS.Tasks.Toolkit.SasServer server = new SAS.Tasks.Toolkit.SasServer(Consumer.AssignedServer);
            using (dbConnection = server.GetOleDbConnection())
            {
                using (adapter = new OleDbDataAdapter(strSelectClause, dbConnection))
                {
                    try
                    {
                        dbConnection.Open();

                        //Count the number of rows in the source
                        Consumer.ActiveData.Accessor.Open();
                        int rowCount = Consumer.ActiveData.Accessor.RowCount;
                        Consumer.ActiveData.Accessor.Close();
                        if (rowCount <= 0)
                        {
                            udObs.Maximum = 1;
                            udObs.Value = 1;
                            totalObsLabel.Text = "1";
                            bEmptyDataSource = true;
                        }
                        else
                        {
                            udObs.Maximum = rowCount;
                            totalObsLabel.Text = udObs.Maximum.ToString();
                            bEmptyDataSource = false;
                            bDelete.Enabled = true;
                        }

                        //Fill in the dataset to display the current row number
                        //Note: making nRow 0 based
                        adapter.Fill(ds, nRow - 1, 1, strMember);
                        InitializeSelectedVariables();
                        PopulatePropertyGrid(editPropertyGrid, editTable, true);
                        PopulatePropertyGrid(insertPropertyGrid, insertTable, false);
                    }
                    catch
                    {
                    }
                    finally
                    {
                        dbConnection.Close();
                    }
                }
            }           
        }

        private void InitializeSelectedVariables()
        {
            selectedVariables.Items.Clear();
            DataColumn dataColumn;
            for (int i = 0; i < ds.Tables[strMember].Columns.Count; i++)
            {
                dataColumn = ds.Tables[strMember].Columns[i];
                selectedVariables.Items.Add(dataColumn.ColumnName, true);
            }
        }

        private void PopulatePropertyGrid(PropertyGrid grid, CustomPropertyTable table, bool bIncludeValues)
        {
            DataColumn dataColumn;
            for (int i = 0; i < ds.Tables[strMember].Columns.Count; i++)
            {
                //                                     |||||||||||||||||||||
                //                                     vvvvvvvvvvvvvvvvvvvvv in the case where we are updating the insertGrid, all of the columns must be present. for the update grid, only the selected variables
                if (selectedVariables.GetItemChecked(i) || !bIncludeValues)
                {
                    dataColumn = ds.Tables[strMember].Columns[i];
                    int varIdx = selectedVariables.Items.IndexOf(dataColumn.ColumnName);
                    //check to see if the variable is checked before adding it to the grid to be displayed.
                    if (selectedVariables.GetItemChecked(varIdx) || !bIncludeValues)
                    {
                        table.CustomProperties.Add(new CustomProperty(dataColumn.ColumnName, dataColumn.DataType.ToString(), null));
                        if (bIncludeValues && !bEmptyDataSource)
                            table[dataColumn.ColumnName] = ds.Tables[strMember].Rows[0][i];
                        else
                            table[dataColumn.ColumnName] = "";
                    }
                }
            }
            grid.SelectedObject = table;
        }

        private bool HasTableChanged(CustomPropertyTable table)
        {
            //The check for nRow is to prevent updates being possible when an insertion has happened
            if (!bEmptyDataSource && nRow == (int)udObs.Value)
            {
                DataColumn dataColumn;
                for (int i = 0; i < ds.Tables[strMember].Columns.Count; i++)
                {
                    dataColumn = ds.Tables[strMember].Columns[i];
                    if (!table[dataColumn.ColumnName].ToString().Equals(ds.Tables[strMember].Rows[0][i].ToString()))
                        return true;
                }
            }
            return false;
        }

        #region Data Functions

        private void UpdateData(String strCode)
        {
            Cursor c = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            if (strCode.Length > 0)
                Consumer.Submit.SubmitCode(strCode, Consumer.AssignedServer, false);

            Cursor.Current = c;
        }

        private String BuildUpdateSqlCode()
        {
            DataColumn dataColumn;
            String strCode, strQuote = "";
            DataTable table = ds.Tables[strMember];
            int nColumnCount = table.Columns.Count;

            strCode = "Proc SQL UNDO_POLICY=OPTIONAL;";

            for (int colIdx = 0; colIdx < nColumnCount; colIdx++)
            {
                if (selectedVariables.GetItemChecked(colIdx))
                {
                    //Create the set statement for the value that is to be changed
                    dataColumn = table.Columns[colIdx];

                    //Skip empty/missing values
                    if (editTable[dataColumn.ColumnName].ToString() == String.Empty)
                        continue;

                    strQuote = ((dataColumn.DataType.ToString().Equals("System.String")) ? "'" : "");
                    strCode += " Update ";
                    strCode += (strLibrary + "." + strMember);
                    strCode += " Set " + dataColumn.ColumnName + " = " + strQuote + editTable[dataColumn.ColumnName].ToString() + strQuote;
                    strCode += " where ";

                    //create the where clause to encompass all the values of this row to ensure uniqueness.  
                    //NOTE: If there are duplicate rows in the datasource that are identical, they will be updated as well because they match the same SQL where statement.  I am not sure if there is a way to prevent this.
                    for (int columnIdx = 0; columnIdx < nColumnCount; columnIdx++)
                    {
                        //Skip empty/missing values
                        if (table.Rows[0][columnIdx].ToString() == String.Empty)
                            continue;

                        DataColumn dc = table.Columns[columnIdx];
                        strQuote = ((dc.DataType.ToString().Equals("System.String")) ? "'" : "");
                        strCode += dc.ColumnName + " = " + strQuote + table.Rows[0][columnIdx].ToString() + strQuote;
                        if (columnIdx + 1 < nColumnCount)
                            strCode += " and ";
                    }

                    //change the value in the original array to keep it in sync with the change from the foregoing SQL update statement
                    //this way if more than one value in the same row has been changed, the update statement remains correct because each subsequent where clause has the newer values
                    if (editTable[dataColumn.ColumnName].ToString() != String.Empty)
                    {
                        table.Rows[0].BeginEdit();
                        table.Rows[0][colIdx] = editTable[dataColumn.ColumnName].ToString();
                        table.Rows[0].EndEdit();
                    }
                    strCode += ";";
                    strCode = strCode.Replace("and ;", ";"); //clean up any loose strings in which we would have bad SQL because of running out of values.
                }
            }

            strCode += " QUIT;";

            return strCode;
        }

        private String BuildInsertSqlCode()
        {
            DataColumn dataColumn;
            String strCode, strQuote = "";
            DataTable table = ds.Tables[strMember];
            int nColumnCount = table.Columns.Count;

            strCode = "Proc SQL UNDO_POLICY=OPTIONAL;";
            strCode += " Insert into ";
            strCode += (strLibrary + "." + strMember + " values(");

            for (int colIdx = 0; colIdx < nColumnCount; colIdx++)
            {
                //Create the set statement for the value that is to be changed
                dataColumn = table.Columns[colIdx];
                strQuote = ((dataColumn.DataType.ToString().Equals("System.String")) ? "'" : "");

                //handle inserting missing valuesease
                if (insertTable[dataColumn.ColumnName].ToString().Equals(String.Empty))
                {
                    if (strQuote.Equals("'"))
                        strCode += "null";
                    else
                        strCode += ".";
                }
                else
                    strCode += strQuote + insertTable[dataColumn.ColumnName].ToString() + strQuote;

                if ((colIdx + 1) < nColumnCount)
                    strCode += ", ";
            }

            strCode += ");";
            strCode += " QUIT;";

            return strCode;
        }

        private String BuildDeleteSqlCode()
        {
            DataColumn dataColumn;
            String strCode, strQuote = "";
            DataTable table = ds.Tables[strMember];
            int nColumnCount = table.Columns.Count;

            strCode = "Proc SQL UNDO_POLICY=OPTIONAL;";
            strCode += " delete from " + (strLibrary + "." + strMember) + " where ";

            for (int colIdx = 0; colIdx < nColumnCount; colIdx++)
            {
                dataColumn = table.Columns[colIdx];
                strQuote = ((dataColumn.DataType.ToString().Equals("System.String")) ? "'" : "");
                strCode += dataColumn.ColumnName + " = " + strQuote + table.Rows[0][colIdx].ToString() + strQuote;
                if (colIdx + 1 < nColumnCount)
                    strCode += " and ";
            }

            strCode += ";";
            strCode += " QUIT;";

            return strCode;
        }

        private void UpdateGrid()
        {
            dbConnection = new OleDbConnection("Provider=sas.iomprovider.1; SAS Workspace ID=" + strUniqueIdentifier);
            adapter = new OleDbDataAdapter(strSelectClause, dbConnection);
            if (!bEmptyDataSource)
            {
                nRow = (int)udObs.Value;
                ds.Tables[strMember].Clear();
                //Note: nRow-1 == making nRow 0 based
                adapter.Fill(ds, nRow - 1, 1, strMember);
                editTable.CustomProperties.Clear();
                PopulatePropertyGrid(editPropertyGrid, editTable, true);
            }
            else
                bCommit.Enabled = false;
            dbConnection.Close();
            adapter.Dispose();

        }
        #endregion

        #region UI EventHandlers

        private void bOK_Click(object sender, System.EventArgs e)
        {
            if (bCommit.Enabled == true && bShowOkWarning)
            {
                WarningDialog wd = new WarningDialog("You have not committed the changes to the current observation.  Is this ok?");
                DialogResult result = wd.ShowDialog();
                bShowOkWarning = wd.ShowAgain;
                if (result == DialogResult.Cancel)
                    return;
                else
                {
                    String strCode = BuildUpdateSqlCode();
                    UpdateData(strCode);
                }
            }
            else
            {
                String strCode = BuildUpdateSqlCode();
                UpdateData(strCode);
            }
            this.Close();
        }

        private void bRight_Click(object sender, System.EventArgs e)
        {
            if (bCommit.Enabled == true && bShowArrowWarning)
            {
                WarningDialog wd = new WarningDialog("You are about to discard changes to the current observation.  Is this ok?");
                DialogResult result = wd.ShowDialog();
                bShowArrowWarning = wd.ShowAgain;
                if (result == DialogResult.Cancel)
                    return;
            }

            bCommit.Enabled = false;
            if ((udObs.Value + 1) <= udObs.Maximum)
                udObs.Value++;
        }

        private void bLeft_Click(object sender, System.EventArgs e)
        {
            if (bCommit.Enabled == true && bShowArrowWarning)
            {
                WarningDialog wd = new WarningDialog("You are about to discard changes to the current observation.  Is this ok?");
                DialogResult result = wd.ShowDialog();
                bShowArrowWarning = wd.ShowAgain;
                if (result == DialogResult.Cancel)
                    return;
            }

            bCommit.Enabled = false;
            if ((udObs.Value - 1) >= udObs.Minimum)
                udObs.Value--;
        }

        private void bCommit_Click(object sender, System.EventArgs e)
        {
            //if any changes have been made to the current row, generate the SQL code and submit it
            if (HasTableChanged(editTable))
            {
                String strCode = BuildUpdateSqlCode();
                UpdateData(strCode);
            }
            bCommit.Enabled = false;
        }

        private void bDelete_Click(object sender, System.EventArgs e)
        {
            if (bShowDeleteWarning)
            {
                WarningDialog wd = new WarningDialog("You are about to permenantly delete an observation.  Are you sure you want to do this?");
                DialogResult result = wd.ShowDialog();
                bShowDeleteWarning = wd.ShowAgain;
                if (result == DialogResult.Cancel)
                    return;
            }

            String strCode = BuildDeleteSqlCode();
            UpdateData(strCode);
            if (udObs.Maximum - 1 >= udObs.Minimum)
            {
                udObs.Maximum--;
                totalObsLabel.Text = udObs.Maximum.ToString();
            }
            else
            {
                bEmptyDataSource = true;  //we just deleted that last obs of the data source
                editTable.CustomProperties.Clear();
                PopulatePropertyGrid(editPropertyGrid, editTable, false);
                bCommit.Enabled = false;
                bDelete.Enabled = false;
                return;
            }
            UpdateGrid();
            UpdateGrid();
        }

        private void udObs_ValueChanged(object sender, System.EventArgs e)
        {
            UpdateGrid();
        }

        private void bInsert_Click(object sender, System.EventArgs e)
        {
            String strCode = BuildInsertSqlCode();
            UpdateData(strCode);
            if (!bEmptyDataSource)  //only increment the maximum if we are adding into a non empty data source, otherwise, we need to prime the pump as it were
            {
                udObs.Maximum++;
                totalObsLabel.Text = udObs.Maximum.ToString();
            }
            else
            {
                bEmptyDataSource = false;
                bDelete.Enabled = true;
                UpdateGrid();
            }
            insertTable.CustomProperties.Clear();
            PopulatePropertyGrid(insertPropertyGrid, insertTable, false);
        }

        private void editPropertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
        {
            bCommit.Enabled = true;
        }

        //Handle updates to the editGrid to reflect possible changes due to variable selection.
        //Optimization: There may be a better way to do this, but for now, anytime we switch to 
        //the first tab, repopulate the grid the grid
        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (bIgnoreEvents)
                return;

            if (nPreviousTabIndex == 0 && bCommit.Enabled == true && bShowArrowWarning)
            {
                WarningDialog wd = new WarningDialog("You are about to discard changes to the current observation.  Is this ok?");
                DialogResult result = wd.ShowDialog();
                bShowArrowWarning = wd.ShowAgain;
                if (result == DialogResult.Cancel)
                {
                    bIgnoreEvents = true;
                    tabControl1.SelectedIndex = 0;
                    bIgnoreEvents = false;
                    return;
                }
            }

            if (tabControl1.SelectedIndex == 0)
            {
                editTable.CustomProperties.Clear();
                PopulatePropertyGrid(editPropertyGrid, editTable, true);
            }
            if (tabControl1.SelectedIndex == 1)
            {
                insertTable.CustomProperties.Clear();
                PopulatePropertyGrid(insertPropertyGrid, insertTable, false);
            }
            nPreviousTabIndex = tabControl1.SelectedIndex;
            bCommit.Enabled = false;
        }

        private void tabControl1_Selected(object sender, System.EventArgs e)
        {
            UpdateGrid();
        }
        #endregion
    }
}