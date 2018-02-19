using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using System.IO;
using DevExpress.XtraEditors;

namespace PropertyGridControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //new DevExpress.XtraGrid.Helpers.GridViewQuickColumnCustomization(this.gridView1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataTable1.Rows.Add(new object[] { "A1", "IBM", 4, 10, "12/31/2009", "12/31/2009", 1 });
            dataTable1.Rows.Add(new object[] { "A1", "MSFT", 3, 15, "12/31/2009", "12/31/2009", 0 });
            dataTable1.Rows.Add(new object[] { "A1", "AAPL", 5, 20, "12/31/2009", "12/31/2009", 0 });
            dataTable1.Rows.Add(new object[] { "A1", "CAT", 7, 30, "12/31/2009", "12/31/2009", 1 });
            dataTable1.Rows.Add(new object[] { "A3", "IBM", 2, 10, "12/31/2009", "12/31/2009", 0 });
            dataTable1.Rows.Add(new object[] { "A2", "AAPL", 1, 4, "12/31/2009", "12/31/2009", 1 });
            dataTable1.Rows.Add(new object[] { "A2", "CAT", 2, 3, "12/31/2009", "12/31/2009", 0 });
            gridView1.ExpandAllGroups();
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog();

            if (!String.IsNullOrEmpty(fileName))
            {
                XlsExportOptions xlsOptions = new XlsExportOptions(TextExportMode.Value, true);
                if (!String.IsNullOrEmpty(Path.GetFileNameWithoutExtension(fileName)))
                    xlsOptions.SheetName = Path.GetFileNameWithoutExtension(fileName);

                gridView1.ShouldDrawFooter = false;
                this.gridControl1.ExportToXls(fileName, xlsOptions);

                // Ask user if they want to open the file
                OpenFile(fileName);
            }

        }

        /// <summary>
        /// Shows the save file dialog.
        /// </summary>
        /// <param name="exportFormat">The export format.</param>
        /// <returns></returns>
        /// <remarks>Added By: bmeeks - 12/2/2008</remarks>
        private string ShowSaveFileDialog()
        {
            string title, filter, extension;
            title = "Microsoft Excel Document";
            filter = "Microsoft Excel|*.xls";
            extension = ".xls";

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "Export To " + title;
                dlg.FileName = "Test Formatting";
                dlg.Filter = filter;
                dlg.DefaultExt = extension;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        /* -jricci- kind of a hacky way to be absolutely sure they can write to the location
                         * there's some weird citrix environments out there we have to be careful with that
                         * have caused our system to freeze in the past
                         */
                        using (FileStream fs = new FileStream(dlg.FileName, FileMode.OpenOrCreate))
                        {
                            if (!fs.CanWrite)
                                throw new IOException("Cannot write to file: " + dlg.FileName);
                        }

                        return dlg.FileName;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                return null;
            }
        }

        private void OpenFile(string fileName)
        {
            if (XtraMessageBox.Show("Do you want to open this file?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch (Exception ex)
                {
                    string msg = String.Format("Error opening file \"{0}\". {1}", fileName, ex.Message);
                    XtraMessageBox.Show(msg);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            gridView1.ShouldDrawFooter = true;
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = this.gridControl1;

            link.PaperKind = System.Drawing.Printing.PaperKind.Legal;
            link.Landscape = true;
            link.Margins.Left = 25;
            link.Margins.Right = 25;
            link.Margins.Top = 75;
            link.Margins.Bottom = 50;
            link.CreateDocument();

            if (link.PrintingSystem.Pages.Count > 1)
            {
                link.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            }
            link.ShowPreview();
        }

        private void btnColumnCustomization_Click(object sender, EventArgs e)
        {
        }
    }
}