using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Data;
using System.Windows.Forms;

using System.ComponentModel.Design;
using System.Diagnostics;
using System.Windows.Forms.ComponentModel;
using System.Windows.Forms.Design;


namespace CProgressControls
{
    /// <summary>
    /// The progress grids column styles
    /// </summary>
    public enum EProgressDataGridStyle
    {
        eInset, eSolidBorder, eFlat, e3DSet
    };

    #region value editor handler
    /// <summary>
    /// The column styles editor handler implementation. Called by the VS.NET framework when user clicks the ... button.
    /// </summary>
    public class CProgressDataGridColumnsValueEditor : UITypeEditor
    {
        private IWindowsFormsEditorService edSvc = null;

        /// <summary>
        /// The main function. We create new instance of the Columns properties and set them in the form dedicated for the 
        /// editing only - the instance of the CProgressDataGridColumnsValueEditorForm classs.
        /// </summary>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            CProgressDataGrid oGrid = (CProgressDataGrid)context.Instance;

            string oValue = new string(' ', 1);

            CProgressDataGridColumnsValueEditorForm oEditorDialog = new CProgressDataGridColumnsValueEditorForm(oValue, oGrid);

            edSvc.ShowDialog(oEditorDialog);

            if (oEditorDialog.Changed == false)
                return value;

            value = oEditorDialog.sData;

            return value;
        }

        /// <summary>
        /// Function sets the edit style to dialog. Telling the VS.NET property window to show the delimiter ...
        /// </summary>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return UITypeEditorEditStyle.Modal;
            }

            return base.GetEditStyle(context);
        }
    }
    #endregion

    /// <summary>
    /// The owner draw data grid control class. Class provides functionality to add the progress bar into the control's cell.
    /// Simple control's api allows developer to consume the control easily and fully take advantage of the package..
    /// </summary>
    public class CProgressDataGrid : System.Windows.Forms.DataGrid
    {
        #region private members
        // The progress bar color. Default set to dark blue color
        private Color m_oProgressColor = SystemColors.ActiveCaption;

        // The progress show font
        private Color m_oProgressPercentageColor = Color.Black;

        // the progress use font
        private bool m_bProgressShowPercentage = false;

        // The progress bar style. Default set to eSolidBorder
        private EProgressDataGridStyle m_oProgressStyle = EProgressDataGridStyle.eSolidBorder;

        // The collection of columns that are of type progress bar
        private string m_oProgressColumn = new string(' ', 1);

        // The data table name to be provided with the progress controls
        private string m_sDataTableName = "";

        // the designer integration
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.ComponentModel.Container components = null;
        #endregion

        #region properties
        [
            Category("Progress control"),
            Description("The color"),
        ]
        public Color ProgressColor
        {
            get { return m_oProgressColor; }
            set { m_oProgressColor = value; Invalidate(); }
        }

        [
            Category("Progress control"),
            Description("The font color used to draw the percentage information"),
        ]
        public Color ProgressPercentageColor
        {
            get { return m_oProgressPercentageColor; }
            set { m_oProgressPercentageColor = value; Invalidate(); }
        }

        [
            Category("Progress control"),
            Description("The font color activation property"),
        ]
        public bool ProgressShowPercentage
        {
            get { return m_bProgressShowPercentage; }
            set { m_bProgressShowPercentage = value; Invalidate(); }
        }

        [
            Category("Progress control"),
            Description("The progress control style"),
        ]
        public EProgressDataGridStyle ProgressStyle
        {
            get { return m_oProgressStyle; }
            set { m_oProgressStyle = value; Invalidate(); }
        }

        [
            Category("Progress control"),
            Description("The progress control data table name"),
        ]
        public String ProgressDataTableName
        {
            get { return m_sDataTableName; }
            set { m_sDataTableName = value; Invalidate(); }
        }

        [
            Category("Progress control"),
            Description("The progress control columns"),
            Editor(typeof(CProgressDataGridColumnsValueEditor), typeof(UITypeEditor)),
        ]
        public string ProgressColumns
        {
            get
            {
                return m_oProgressColumn;
            }
            set
            {
                m_oProgressColumn = value; 
                Invalidate();
            }
        }
        #endregion

        #region contruction
        /// <summary>
        /// The defult constructor
        /// </summary>
        public CProgressDataGrid()
        {
            InitializeComponent();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;

            // disable sorting
            this.AllowSorting = false;
        }
        #endregion

        #region accessor functions
        /// <summary>
        /// Sets the column as a progress control.
        /// </summary>
        /// <param name="nCol">The column index to be set as progress.</param>
        public void SetProgressControlColumn(int nCol)
        {
            m_oProgressColumn += nCol + ",";

            Invalidate();
        }

        /// <summary>
        /// Sets the color of the progress control bar.
        /// </summary>
        /// <param name="oColor">The color to set.</param>
        public void SetProgressControlColor(Color oColor)
        {
            m_oProgressColor = oColor;

            Invalidate();
        }

        /// <summary>
        /// Sets the progress style. See EProgressDataGridStyle for more information.
        /// </summary>
        /// <param name="oStyle">The style to set.</param>
        public void SetProgressControlStyle(EProgressDataGridStyle oStyle)
        {
            m_oProgressStyle = oStyle;

            Invalidate();
        }

        /// <summary>
        /// Sets the name of the table from the data set to use to retrieve the data for the progess bar
        /// </summary>
        /// <param name="sDataTableName"></param>
        public void SetProgressControlDataTableName(string sDataTableName)
        {
            m_sDataTableName = sDataTableName;

            Invalidate();
        }

        /// <summary>
        /// Sets the flag to use the percentage information
        /// </summary>
        /// <param name="bUse"></param>
        public void SetProgressControlUsePercentage(bool bUse)
        {
            m_bProgressShowPercentage = bUse;

            Invalidate();
        }

        /// <summary>
        /// Sets the color of the percentage information
        /// </summary>
        /// <param name="oColor"></param>
        public void SetProgressControlPercentageColor(Color oColor)
        {
            m_oProgressPercentageColor = oColor;

            Invalidate();
        }
        #endregion

        #region painting functions
        /// <summary>
        /// The main drawing function. Checks which columns are the progress ones. Gets the value of the specified cell
        /// in the associated dataset and draws the progress there.
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
        {
            // perform the basic painting
            base.OnPaint(pe);

            try
            {
                // iterate every entry in the column list for progress controls
                foreach (int nCol in utils.GetColArrayList(m_oProgressColumn))
                {
                    DataSet oSet = (DataSet)this.DataSource;

                    if (oSet == null)
                        return;

                    DataTable oTable = oSet.Tables[m_sDataTableName];

                    if (oTable == null)
                        return;

                    int nRow = 0;

                    // iterate the rows of the data set table
                    foreach (DataRow oRow in oTable.Rows)
                    {
                        if (oRow == null)
                            continue;

                        PaintProgressCell(pe.Graphics, oRow, nRow, nCol);

                        nRow++;
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        /// <summary>
        /// Paints the cell.
        /// </summary>
        private void PaintProgressCell(Graphics oGraphics, DataRow oRow, int nRow, int nCol)
        {
            // get the value at the desired location
            string sValue = oRow.ItemArray.GetValue(nCol).ToString();

            // get the cells rect
            Rectangle rect = this.GetCellBounds(nRow, nCol);
            Rectangle rect2 = this.GetCellBounds(nRow, nCol);

            Brush oBrush = null;

            if ((nRow & 1) != 1)
                oBrush = new SolidBrush(this.BackColor);
            else
                oBrush = new SolidBrush(this.AlternatingBackColor);

            oGraphics.FillRectangle(oBrush, rect);

            oBrush.Dispose();

            if (m_oProgressStyle == EProgressDataGridStyle.eSolidBorder)
            {
                PaintProgressCellSolidBorderStyle(rect, sValue, oGraphics);
            }

            if (m_oProgressStyle == EProgressDataGridStyle.eFlat)
            {
                PaintProgressCellFlatStyle(rect, sValue, oGraphics);
            }

            if (m_oProgressStyle == EProgressDataGridStyle.eInset)
            {
                PaintProgressCellInsetStyle(rect, sValue, oGraphics);
            }

            if (m_oProgressStyle == EProgressDataGridStyle.e3DSet)
            {
                PaintProgressCell3DsetStyle(rect, sValue, oGraphics);
            }

            if (m_bProgressShowPercentage == true)
            {
                oBrush = new SolidBrush(m_oProgressPercentageColor);
                rect2.X += 2;
                rect2.Y += 1;
                rect2.Width -= 4;
                rect2.Height -= 2;
                int nValue = Convert.ToInt32(sValue);
                if (nValue > 100)
                    nValue = 100;
                oGraphics.DrawString(nValue.ToString() + "%", this.Font, oBrush, rect2);
                oBrush.Dispose();
            }
        }

        /// <summary>
        /// The drawing function for the solid border style progress bar
        /// </summary>
        /// <param name="rect"></param>
        private void PaintProgressCellSolidBorderStyle(Rectangle rect, string sValue, Graphics oGraphics)
        {
            double dValue = (double)Convert.ToDouble(sValue);
            if (dValue > 100.0)
                dValue = 100.0;

            // calculate the width of the progress
            double fWidth = (float)rect.Width;
            fWidth = fWidth / 100.0 * dValue;

            rect.Width = (int)fWidth;

            rect.Height -= 2;
            rect.Width -= 2;
            rect.Y += 1;
            rect.X += 1;

            // draw the border
            oGraphics.FillRectangle(Brushes.Black, rect);

            rect.Height -= 2;
            rect.Width -= 2;
            rect.Y += 1;
            rect.X += 1;

            // draw the progress
            Brush oBrush = new SolidBrush(m_oProgressColor);

            oGraphics.FillRectangle(oBrush, rect);

            oBrush.Dispose();
        }

        /// <summary>
        /// The drawing function for the flat style progress bar
        /// </summary>
        /// <param name="rect"></param>
        private void PaintProgressCellFlatStyle(Rectangle rect, string sValue, Graphics oGraphics)
        {
            double dValue = (double)Convert.ToDouble(sValue);
            if (dValue > 100.0)
                dValue = 100.0;

            // calculate the width of the progress
            double fWidth = (float)rect.Width;
            fWidth = fWidth / 100.0 * dValue;

            rect.Width = (int)fWidth;

            rect.Height -= 2;
            rect.Width -= 2;
            rect.Y += 1;
            rect.X += 1;

            // draw the progress
            Brush oBrush = new SolidBrush(m_oProgressColor);

            oGraphics.FillRectangle(oBrush, rect);

            oBrush.Dispose();
        }

        /// <summary>
        /// The drawing function for the inset style progress bar
        /// </summary>
        /// <param name="rect"></param>
        private void PaintProgressCellInsetStyle(Rectangle rect, string sValue, Graphics oGraphics)
        {
            double dValue = (double)Convert.ToDouble(sValue);
            if (dValue > 100.0)
                dValue = 100.0;

            // calculate the width of the progress
            double fWidth = (float)rect.Width;
            fWidth = fWidth / 100.0 * dValue;

            rect.Width = (int)fWidth;

            rect.Height -= 2;
            rect.Width -= 2;
            rect.Y += 1;
            rect.X += 1;

            oGraphics.FillRectangle(Brushes.Silver, rect);

            rect.Height -= 1;
            rect.Width -= 1;

            oGraphics.FillRectangle(Brushes.Black, rect);

            // draw the progress
            Brush oBrush = new SolidBrush(m_oProgressColor);

            rect.Height -= 1;
            rect.Width -= 1;
            rect.Y += 1;
            rect.X += 1;

            oGraphics.FillRectangle(oBrush, rect);

            oBrush.Dispose();
        }

        /// <summary>
        /// The drawing function for the inset style progress bar
        /// </summary>
        /// <param name="rect"></param>
        private void PaintProgressCell3DsetStyle(Rectangle rect, string sValue, Graphics oGraphics)
        {
            double dValue = (double)Convert.ToDouble(sValue);
            if (dValue > 100.0)
                dValue = 100.0;

            // calculate the width of the progress
            double fWidth = (float)rect.Width;
            fWidth = fWidth / 100.0 * dValue;

            rect.Width = (int)fWidth;

            rect.Height -= 2;
            rect.Width -= 2;
            rect.Y += 1;
            rect.X += 1;

            oGraphics.FillRectangle(Brushes.Black, rect);

            rect.Height -= 1;
            rect.Width -= 1;

            oGraphics.FillRectangle(Brushes.Silver, rect);

            // draw the progress
            Brush oBrush = new SolidBrush(m_oProgressColor);

            rect.Height -= 1;
            rect.Width -= 1;
            rect.Y += 1;
            rect.X += 1;

            oGraphics.FillRectangle(oBrush, rect);

            oBrush.Dispose();
        }
        #endregion
    }
}
