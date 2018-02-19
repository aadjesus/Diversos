#region Using

using System;
using System.ComponentModel;
using System.Data;
using System.Xml;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.IsolatedStorage;
using System.Web;

#endregion

namespace ResourceEditor
{
  public partial class Editor : Form
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Editor"/> class.
    /// </summary>
    public Editor()
    {
      InitializeComponent();
      this.openFileDialog1.Filter = "Resource files (*.resx)|*.resx|All files (*.*)|*.*";
      btnSave.Click += new EventHandler(btnSave_Click);
      btnBrowse.Click += new EventHandler(btnBrowse_Click);
      openFileDialog1.FileOk += new CancelEventHandler(openFileDialog1_FileOk);
      gvMain.CellValueChanged += new DataGridViewCellEventHandler(gvMain_CellEndEdit);
      gvMain.CellEnter += new DataGridViewCellEventHandler(gvMain_CellEnter);
      gvMain.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(gvMain_RowHeaderMouseClick);
      gvMain.RowsRemoved += new DataGridViewRowsRemovedEventHandler(gvMain_RowsRemoved);

      LoadDefaultSettings();
      if (txtFilename.Text.Length > 0 && File.Exists(txtFilename.Text))
        this.BindGrid();

      if (gvMain.Columns.Count > 0)
        gvMain.Columns[0].ReadOnly = cbEnable.Checked;
    }

    #region Overrides

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Form.Load"></see> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.Init();
      this._IsDirty = false;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Form.Closed"></see> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs"></see> that contains the event data.</param>
    protected override void OnClosed(EventArgs e)
    {
      base.OnClosed(e);
      if (this._IsDirty)
        if (MessageBox.Show(Properties.Resources.ConfirmSave, this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
          SaveAllRows();

      SaveDefaultValues();
    }

    private void Init()
    {
      this.btnSave.Text = Properties.Resources.Save;
      this.btnBrowse.Text = Properties.Resources.Browse;
      this.cbEnable.Text = Properties.Resources.AllowRowDeletion;
      this.Text = Properties.Resources.ApplicationName;
    }

    #endregion

    #region Private fields

    private bool _IsDirty;
    private string _BeforeName;

    #endregion

    #region Events

    /// <summary>
    /// Handles the CheckedChanged event of the checkBox1 control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
      gvMain.AllowUserToAddRows = !cbEnable.Checked;
      gvMain.AllowUserToDeleteRows = !cbEnable.Checked;
      if (gvMain.Columns.Count > 0)
        gvMain.Columns[0].ReadOnly = cbEnable.Checked;
    }

    /// <summary>
    /// Handles the CellEnter event of the gvMain control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
    void gvMain_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
      this._BeforeName = gvMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
    }

    /// <summary>
    /// Handles the Click event of the btnBrowse control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
    private void btnBrowse_Click(object sender, EventArgs e)
    {
      openFileDialog1.ShowDialog();
    }

    /// <summary>
    /// Handles the FileOk event of the openFileDialog1 control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
    {
      if (gvMain.DataSource != null && this._IsDirty)
      {
        if (MessageBox.Show(Properties.Resources.ConfirmSave, this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
          SaveAllRows();
      }

      txtFilename.Text = openFileDialog1.FileName;
      this.BindGrid();
      //this.SetWidth();
    }

    /// <summary>
    /// Handles the CellEndEdit event of the gvMain control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
    private void gvMain_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex == 0)
        HandleItemChange(e);

      this._IsDirty = true;
    }

    /// <summary>
    /// Handles the RowsRemoved event of the gvMain control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewRowsRemovedEventArgs"/> instance containing the event data.</param>
    void gvMain_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
    {
      this._IsDirty = true;
    }

    /// <summary>
    /// Handles the RowHeaderMouseClick event of the gvMain control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
    void gvMain_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      gvMain.Rows[e.RowIndex].ReadOnly = true;
    }

    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
    private void btnSave_Click(object sender, EventArgs e)
    {
      SaveAllRows();
      this._IsDirty = false;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Calls the DeleteAllRows method and inserts
    /// all the changed data from the grid to
    /// the .resx file.
    /// </summary>
    private void SaveAllRows()
    {
      DataTable dt = gvMain.DataSource as DataTable;
      XmlDocument doc = GetXmlDocument();
      XmlDocumentFragment element = null;
      StringBuilder sb = new StringBuilder();

      try
      {
        DeleteAllRows(doc);
        foreach (DataRow row in dt.Rows)
        {
          sb.Append("<data name=\"" + HttpUtility.HtmlEncode(row["name"].ToString()) + "\"  xml:space=\"preserve\">");
          if (row["value"].ToString().Length > 0)
            sb.Append("<value>" + HttpUtility.HtmlEncode(row["value"].ToString()).Replace("\n", string.Empty) + "</value>");
          else
            sb.Append("<value/>");

          if (row["comment"].ToString().Length > 0)
            sb.Append("<comment>" + HttpUtility.HtmlEncode(row["comment"].ToString()).Replace("\n", string.Empty) + "</comment>");

          sb.Append("</data>");

          element = doc.CreateDocumentFragment(); ;
          element.InnerXml = sb.ToString();
          sb.Remove(0, sb.Length);
          doc.SelectSingleNode("root").AppendChild(element);
        }

        doc.Save(txtFilename.Text);
      }
      catch (XmlException)
      {
        MessageBox.Show(Properties.Resources.XmlErrorOnSave, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
      }
      catch (UnauthorizedAccessException)
      {
        MessageBox.Show(Properties.Resources.PermissionErrorOnSave, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
      }
    }

    /// <summary>
    /// Removes all the datarows from the .resx file
    /// </summary>
    /// <param name="doc">The doc.</param>
    private static void DeleteAllRows(XmlDocument doc)
    {
      XmlNodeList nodes = doc.SelectNodes("//data");

      for (int i = 0; i < nodes.Count; i++)
      {
        doc.SelectSingleNode("root").RemoveChild(nodes[i]);
      }
    }

    /// <summary>
    /// Binds the .resx file to the grid and sets
    /// the _IsDirty property to false because the
    /// data is fresh from the file and has not been
    /// changed at this time.
    /// </summary>
    private void BindGrid()
    {
      XmlDocument doc = GetXmlDocument();
      if (doc.FirstChild == null)
      {
        gvMain.DataSource = null;
        return;
      }

      DataTable dt = CreateDataTable();
      DataRow row = null;
      try
      {
        foreach (XmlNode node in doc.SelectNodes("//data"))
        {
          row = dt.NewRow();
          row["name"] = node.Attributes["name"].InnerText;
          if (node.SelectSingleNode("value") != null)
            row["value"] = node.SelectSingleNode("value").InnerText;
          if (node.SelectSingleNode("comment") != null)
            row["comment"] = node.SelectSingleNode("comment").InnerText;
          dt.Rows.Add(row);
        }
      }
      catch(NullReferenceException)
      {
        if (MessageBox.Show(Properties.Resources.FileCorruptLoadAnyway, this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
        {
          txtFilename.Text = string.Empty;
          return;
        }
      }

      gvMain.DataSource = dt;
      gvMain.Sort(gvMain.Columns[0], ListSortDirection.Ascending);
      base.Text = txtFilename.Text.Substring(txtFilename.Text.LastIndexOf('\\') + 1);
      cbEnable.Enabled = true;
      this._IsDirty = false;
      btnSave.Enabled = true;
    }

    /// <summary>
    /// Creates the data table.
    /// </summary>
    private static DataTable CreateDataTable()
    {
      DataTable dt = new DataTable();
      dt.Locale = System.Threading.Thread.CurrentThread.CurrentUICulture;
      dt.Columns.Add("Name", typeof(string));
      dt.Columns.Add("Value", typeof(string));
      dt.Columns.Add("Comment", typeof(string));
      dt.Columns[0].AllowDBNull = false;
      dt.Columns[0].Unique = true;
      return dt;
    }

    /// <summary>
    /// Loads the .resx file from disk.
    /// </summary>
    private XmlDocument GetXmlDocument()
    {
      XmlDocument doc = new XmlDocument();

      try
      {
        doc.Load(txtFilename.Text);
      }
      catch (XmlException)
      {
        MessageBox.Show(Properties.Resources.XmlFormatError, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        txtFilename.Text = string.Empty;
      }

      return doc;
    }

    /// <summary>
    /// This method is called every time data has been
    /// changed in the first column, the name column.
    /// It makes sure that two names cannot be the same. It
    /// will also violate the uniqueness of that column that
    /// was set in the CreateDataTable method.
    /// </summary>
    /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
    private void HandleItemChange(DataGridViewCellEventArgs e)
    {
      string newValue = gvMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
      if (string.IsNullOrEmpty(newValue))
      {
        MessageBox.Show(Properties.Resources.WarningOnEmptyString, Properties.Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        gvMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this._BeforeName;
        return;
      }

      for (int i = 0; i < (gvMain.DataSource as DataTable).Rows.Count; i++)
      {
        if (newValue.Equals((gvMain.DataSource as DataTable).Rows[i][0].ToString()) && e.RowIndex != i)
        {
          MessageBox.Show(Properties.Resources.WarningOnUniqueName, Properties.Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
          gvMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this._BeforeName;
          return;
        }
      }
    }

    /// <summary>
    /// Saves the default values.
    /// </summary>
    private void SaveDefaultValues()
    {
      using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
      {
        using (StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream("default.ini", FileMode.Create, isoStore)))
        {
          writer.WriteLine(txtFilename.Text);
          writer.WriteLine(cbEnable.Checked.ToString());
        }
      }
    }

    /// <summary>
    /// Loads the default settings.
    /// </summary>
    private void LoadDefaultSettings()
    {
      try
      {
        using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
        {
          if (isoStore.GetFileNames("default.ini").Length > 0)
          {
            using (StreamReader reader = new StreamReader(new IsolatedStorageFileStream("default.ini", FileMode.OpenOrCreate, isoStore)))
            {
              txtFilename.Text = reader.ReadLine();
              cbEnable.Checked = bool.Parse(reader.ReadLine());
            }
          }
        }
      }
      catch (IsolatedStorageException)
      { }
    }

    #endregion

  }
}