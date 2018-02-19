using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Cells = SourceGrid2.Cells.Real;

namespace SampleProject
{
	/// <summary>
	/// Summary description for frmSample3.
	/// </summary>
	public class frmSample3 : System.Windows.Forms.Form
	{
		private SourceGrid2.Grid grid;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSample3()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.grid = new SourceGrid2.Grid();
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grid.AutoSizeMinHeight = 10;
			this.grid.AutoSizeMinWidth = 10;
			this.grid.AutoStretchColumnsToFitWidth = false;
			this.grid.AutoStretchRowsToFitHeight = false;
			this.grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.grid.ContextMenuStyle = ((SourceGrid2.ContextMenuStyle)(((SourceGrid2.ContextMenuStyle.ClearSelection) 
				| SourceGrid2.ContextMenuStyle.CopyPasteSelection)));
			this.grid.CustomSort = false;
			this.grid.GridToolTipActive = true;
			this.grid.Location = new System.Drawing.Point(12, 12);
			this.grid.Name = "grid";
			this.grid.Size = new System.Drawing.Size(516, 368);
			this.grid.SpecialKeys = SourceGrid2.GridSpecialKeys.Default;
			this.grid.TabIndex = 0;
			// 
			// frmSample3
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(540, 391);
			this.Controls.Add(this.grid);
			this.Name = "frmSample3";
			this.Text = "Cell Editors, Specials Cells, Formatting and Image";
			this.Load += new System.EventHandler(this.frmSample3_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmSample3_Load(object sender, System.EventArgs e)
		{
			grid.Redim(46,3);

			SourceGrid2.VisualModels.Common l_TitleModel = new SourceGrid2.VisualModels.Common(false);
			l_TitleModel.BackColor = Color.SteelBlue;
			l_TitleModel.ForeColor = Color.White;
			l_TitleModel.TextAlignment = ContentAlignment.MiddleCenter;
			SourceGrid2.VisualModels.Common l_CaptionModel = new SourceGrid2.VisualModels.Common(false);
			l_CaptionModel.BackColor = grid.BackColor;

			int l_CurrentRow = 0;

			#region Base Types
			grid[l_CurrentRow, 0] = new Cells.Cell("Base Types", null, l_TitleModel);
			grid[l_CurrentRow, 0].ColumnSpan = 3;
			l_CurrentRow++;

			//string
			grid[l_CurrentRow,0] = new Cells.Cell("String", null, l_CaptionModel);

			grid[l_CurrentRow,1] = new Cells.Cell("String Value", typeof(string));

			l_CurrentRow++;

			//double
			grid[l_CurrentRow, 0] = new Cells.Cell("Double", null, l_CaptionModel);
			grid[l_CurrentRow, 1] = new Cells.Cell(1.5, typeof(double));

			l_CurrentRow++;

			//int
			grid[l_CurrentRow, 0] = new Cells.Cell("Int", null, l_CaptionModel);
			grid[l_CurrentRow, 1] = new Cells.Cell(5, typeof(int));

			l_CurrentRow++;

			//DateTime
			grid[l_CurrentRow, 0] = new Cells.Cell("DateTime", null, l_CaptionModel);
			grid[l_CurrentRow, 1] = new Cells.Cell(DateTime.Now, typeof(DateTime));

			l_CurrentRow++;

			//Boolean
			grid[l_CurrentRow, 0] = new Cells.Cell("Boolean", null, l_CaptionModel);
			grid[l_CurrentRow, 1] = new Cells.Cell(true, typeof(Boolean));

			l_CurrentRow++;
			#endregion

			#region Complex Types
			grid[l_CurrentRow, 0] = new Cells.Cell("Complex Types", null, l_TitleModel);
			grid[l_CurrentRow, 0].ColumnSpan = 3;
			l_CurrentRow++;

			//Font
			grid[l_CurrentRow, 0] = new Cells.Cell("Font", null, l_CaptionModel);

			grid[l_CurrentRow, 1] = new Cells.Cell(this.Font, typeof(Font));

			l_CurrentRow++;

			//Cursor
			grid[l_CurrentRow, 0] = new Cells.Cell("Cursor", null, l_CaptionModel);

			grid[l_CurrentRow, 1] = new Cells.Cell(Cursors.Arrow, typeof(Cursor));

			l_CurrentRow++;

			//Point
			grid[l_CurrentRow, 0] = new Cells.Cell("Point", null, l_CaptionModel);

			grid[l_CurrentRow, 1] = new Cells.Cell(new Point(2,3), typeof(Point));

			l_CurrentRow++;

			//Rectangle
			grid[l_CurrentRow, 0] = new Cells.Cell("Rectangle", null, l_CaptionModel);

			grid[l_CurrentRow, 1] = new Cells.Cell(new Rectangle(100,100,200,200), typeof(Rectangle));

			l_CurrentRow++;

			//Image
			grid[l_CurrentRow, 0] = new Cells.Cell("Image", null, l_CaptionModel);

			grid[l_CurrentRow, 1] = new Cells.Cell(null, typeof(Image));

			l_CurrentRow++;

			//Enum AnchorStyle
			grid[l_CurrentRow, 0] = new Cells.Cell("AnchorStyle", null, l_CaptionModel);

			grid[l_CurrentRow, 1] = new Cells.Cell(AnchorStyles.Bottom, typeof(AnchorStyles));

			l_CurrentRow++;

			//Enum
			grid[l_CurrentRow, 0] = new Cells.Cell("Enum", null, l_CaptionModel);
			grid[l_CurrentRow, 1] = new Cells.Cell(System.Windows.Forms.BorderStyle.Fixed3D, typeof(System.Windows.Forms.BorderStyle));

			l_CurrentRow++;
			#endregion

			#region Special Editors and Cells
			grid[l_CurrentRow, 0] = new Cells.Cell("Special Editors and Cells", null, l_TitleModel);
			grid[l_CurrentRow, 0].ColumnSpan = 3;
			l_CurrentRow++;

			//Double Chars Validation
			grid[l_CurrentRow, 0] = new Cells.Cell("Double Chars Validation", null, l_CaptionModel);
			SourceGrid2.DataModels.EditorTextBoxNumeric l_NumericEditor = new SourceGrid2.DataModels.EditorTextBoxNumeric(typeof(double));
			l_NumericEditor.NumericCharStyle = SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol;
			grid[l_CurrentRow, 1] = new Cells.Cell(0.5, l_NumericEditor);

			l_CurrentRow++;

			//String Chars (ABC)
			grid[l_CurrentRow, 0] = new Cells.Cell("String Chars Validation(only ABC)", null, l_CaptionModel);
			SourceGrid2.DataModels.EditorTextBox l_StringEditor = new SourceGrid2.DataModels.EditorTextBox(typeof(string));
			l_StringEditor.ValidCharacters = new char[]{'A','B','C'};
			grid[l_CurrentRow, 1] = new Cells.Cell("AABB", l_StringEditor);

			l_CurrentRow++;

			//Int 0-100 or null
			grid[l_CurrentRow, 0] = new Cells.Cell("Int 0-100 or null", null, l_CaptionModel);
			SourceGrid2.DataModels.EditorTextBoxNumeric l_NumericEditor0_100 = new SourceGrid2.DataModels.EditorTextBoxNumeric(typeof(int));
			l_NumericEditor0_100.NumericCharStyle = SourceLibrary.Windows.Forms.NumericCharStyle.None;
			l_NumericEditor0_100.MinimumValue = 0;
			l_NumericEditor0_100.MaximumValue = 100;
			l_NumericEditor0_100.AllowNull = true;
			grid[l_CurrentRow, 1] = new Cells.Cell(7, l_NumericEditor0_100);

			l_CurrentRow++;

			//Enum Custom Display
			grid[l_CurrentRow, 0] = new Cells.Cell("Enum Custom Display", null, l_CaptionModel);
			SourceGrid2.DataModels.EditorComboBox l_KeysCombo = new SourceGrid2.DataModels.EditorComboBox(typeof(Keys));
			l_KeysCombo.ConvertingValueToDisplayString += new SourceLibrary.ComponentModel.ConvertingObjectEventHandler(l_KeysCombo_ConvertingValueToDisplayString);
			grid[l_CurrentRow, 1] = new Cells.Cell(Keys.Enter);
			grid[l_CurrentRow, 1].DataModel = l_KeysCombo;

			l_CurrentRow++;

			string[] l_CmbArr = new string[]{"Value 1","Value 2","Value 3"};
			//ComboBox 1
			grid[l_CurrentRow, 0] = new Cells.Cell("ComboBox String", null, l_CaptionModel);
			grid[l_CurrentRow, 1] = new Cells.ComboBox(l_CmbArr[0], typeof(string), l_CmbArr, false);

			l_CurrentRow++;

			//ComboBox 2
			grid[l_CurrentRow, 0] = new Cells.Cell("ComboBox String Exclusive", null, l_CaptionModel);
			grid[l_CurrentRow, 1] = new Cells.ComboBox(l_CmbArr[0], typeof(string), l_CmbArr, true);

			l_CurrentRow++;

			//ComboBox 3
			grid[l_CurrentRow, 0] = new Cells.Cell("ComboBox String No TextBox", null, l_CaptionModel);
			grid[l_CurrentRow, 1] = new Cells.ComboBox(l_CmbArr[0], typeof(string), l_CmbArr, true);
			grid[l_CurrentRow, 1].DataModel.AllowStringConversion = false;

			l_CurrentRow++;

			//ComboBox DateTime Editable
			grid[l_CurrentRow, 0] = new Cells.Cell("ComboBox DateTime", null, l_CaptionModel);
			DateTime[] l_CmbArrDt = new DateTime[]{new DateTime(1981, 10, 6),new DateTime(1991, 10, 6),new DateTime(2001, 10, 6)};
			grid[l_CurrentRow, 1] = new Cells.ComboBox(l_CmbArrDt[0], typeof(DateTime), l_CmbArrDt, false);

			l_CurrentRow++;

			//ComboBox Custom Display (create a datamodel that has a custom display string)
			grid[l_CurrentRow, 0] = new Cells.Cell("ComboBox Custom Display", null, l_CaptionModel);
			int[] l_CmbArrInt = new int[]{0,1,2,3,4};
			SourceGrid2.DataModels.EditorComboBox l_ComboBoxDescription = new SourceGrid2.DataModels.EditorComboBox(typeof(int), l_CmbArrInt, true);
			SourceLibrary.ComponentModel.Validator.ValueMapping l_ComboMapping = new SourceLibrary.ComponentModel.Validator.ValueMapping();
			l_ComboMapping.DisplayStringList = new string[]{"0 - Zero", "1 - One", "2 - Two", "3 - Three", "4- Four"};
			l_ComboMapping.ValueList = l_CmbArrInt;
			l_ComboMapping.BindValidator(l_ComboBoxDescription);
			grid[l_CurrentRow, 1] = new Cells.Cell(0);
			grid[l_CurrentRow, 1].DataModel = l_ComboBoxDescription;

			Cells.Cell l_CellComboRealValue = new Cells.Cell(grid[l_CurrentRow, 1].Value, null, l_CaptionModel);
			SourceGrid2.BehaviorModels.BindProperty l_ComboBindProperty = new SourceGrid2.BehaviorModels.BindProperty(typeof(Cells.Cell).GetProperty("Value"), l_CellComboRealValue);
			grid[l_CurrentRow, 1].Behaviors.Add(l_ComboBindProperty);
			grid[l_CurrentRow, 2] = l_CellComboRealValue;

			l_CurrentRow++;

			//Numeric Up Down Editor
			grid[l_CurrentRow, 0] = new Cells.Cell("NumericUpDown", null, l_CaptionModel);

			grid[l_CurrentRow, 1] = new Cells.Cell(0);
			SourceGrid2.DataModels.EditorNumericUpDown l_NumericUpDownEditor = new SourceGrid2.DataModels.EditorNumericUpDown(typeof(int), 100, 0, 1);
			grid[l_CurrentRow, 1].DataModel = l_NumericUpDownEditor;

			l_CurrentRow++;

			//Multiline Textbox
			grid[l_CurrentRow, 0] = new Cells.Cell("Multiline Textbox", null, l_CaptionModel);
			grid[l_CurrentRow, 0].ColumnSpan = 1;
			grid[l_CurrentRow, 0].RowSpan = 2;

			grid[l_CurrentRow, 1] = new Cells.Cell("Hello\r\nWorld");
			SourceGrid2.DataModels.EditorTextBox l_MultilineEditor = new SourceGrid2.DataModels.EditorTextBox(typeof(string));
			l_MultilineEditor.Multiline = true;
			grid[l_CurrentRow, 1].DataModel = l_MultilineEditor;
			grid[l_CurrentRow, 1].RowSpan = 2;

			l_CurrentRow++;
			l_CurrentRow++;

			//Boolean (CheckBox)
			grid[l_CurrentRow, 0] = new Cells.Cell("Boolean (CheckBox)", null, l_CaptionModel);
			grid[l_CurrentRow, 1] = new Cells.CheckBox(true);

			Cells.CheckBox l_DisabledCheckBox = new Cells.CheckBox("Disabled Checkbox", true);
			l_DisabledCheckBox.EnableEdit = false;
			grid[l_CurrentRow, 2] = l_DisabledCheckBox;

			l_CurrentRow++;

			//Cell Button
			grid[l_CurrentRow,1] = new Cells.Button("CellButton");

			l_CurrentRow++;

			//Cell Link
			grid[l_CurrentRow,1] = new Cells.Link("CellLink");

			l_CurrentRow++;

			#endregion

			#region Custom Formatting
			grid[l_CurrentRow, 0] = new Cells.Cell("Custom Formatting", null, l_TitleModel);
			grid[l_CurrentRow, 0].ColumnSpan = 3;
			l_CurrentRow++;

			//Percent Editor
			grid[l_CurrentRow, 0] = new Cells.Cell("Percent Format", null, l_CaptionModel);

			grid[l_CurrentRow, 1] = new Cells.Cell(0.5);
			SourceGrid2.DataModels.EditorTextBox l_PercentEditor = new SourceGrid2.DataModels.EditorTextBox(typeof(double));
			l_PercentEditor.TypeConverter = new SourceLibrary.ComponentModel.Converter.PercentTypeConverter(typeof(double));
			grid[l_CurrentRow, 1].DataModel = l_PercentEditor;

			l_CurrentRow++;

			//Currency Editor
			grid[l_CurrentRow, 0] = new Cells.Cell("Currency Format", null, l_CaptionModel);

			grid[l_CurrentRow, 1] = new Cells.Cell(50.0M);
			SourceGrid2.DataModels.EditorTextBox l_CurrencyEditor = new SourceGrid2.DataModels.EditorTextBox(typeof(decimal));
			l_CurrencyEditor.TypeConverter = new SourceLibrary.ComponentModel.Converter.CurrencyTypeConverter(typeof(decimal));
			grid[l_CurrentRow, 1].DataModel = l_CurrencyEditor;

			l_CurrentRow++;

			//Custom Format Number Editor
			grid[l_CurrentRow, 0] = new Cells.Cell("Custom Format Number", null, l_CaptionModel);

			Cells.Cell l_CellNumberEditor = new Cells.Cell(84.23);
			SourceGrid2.DataModels.EditorTextBox l_CustomFormatEditor = new SourceGrid2.DataModels.EditorTextBox(typeof(double));
			l_CustomFormatEditor.EnableEdit = false;

			SourceLibrary.ComponentModel.Converter.NumberTypeConverter l_NumberConverter = new SourceLibrary.ComponentModel.Converter.NumberTypeConverter(typeof(double));
			l_CustomFormatEditor.TypeConverter = l_NumberConverter;
			l_CellNumberEditor.DataModel = l_CustomFormatEditor;
			l_CellNumberEditor.VisualModel = l_CaptionModel;
			grid[l_CurrentRow, 1] = l_CellNumberEditor;

			grid[l_CurrentRow,2] = new Cells.ComboBox(l_NumberConverter.Format, typeof(string),new string[]{"G","C","P","000.00","#.000"},false);
			SourceGrid2.BehaviorModels.BindProperty l_BindProperty = new SourceGrid2.BehaviorModels.BindProperty(typeof(SourceLibrary.ComponentModel.Converter.NumberTypeConverter).GetProperty("Format"), l_NumberConverter);
			grid[l_CurrentRow,2].Behaviors.Add(l_BindProperty);
			SourceGrid2.BehaviorModels.CustomEvents l_Event = new SourceGrid2.BehaviorModels.CustomEvents();
			l_Event.ValueChanged+=new SourceGrid2.PositionEventHandler(CellCustomFormat_ValueChanged);
			grid[l_CurrentRow,2].Behaviors.Add(l_Event);

			l_CurrentRow++;

			//DateTime 2 (using custom formatting)
			string l_FormatDt2 = "yyyy MM dd";
			grid[l_CurrentRow, 0] = new Cells.Cell("Date(" + l_FormatDt2 + ")", null, l_CaptionModel);

			string[] l_ParseFormat = new string[]{l_FormatDt2};
			System.Globalization.DateTimeStyles l_dtStyles = System.Globalization.DateTimeStyles.AllowInnerWhite|System.Globalization.DateTimeStyles.AllowLeadingWhite|System.Globalization.DateTimeStyles.AllowTrailingWhite|System.Globalization.DateTimeStyles.AllowWhiteSpaces;
			TypeConverter l_dtConverter = new SourceLibrary.ComponentModel.Converter.DateTimeTypeConverter(l_FormatDt2,l_ParseFormat, l_dtStyles);
			string tmp = l_dtConverter.ConvertToString(DateTime.Today);
			DateTime l_dtValue = (DateTime)l_dtConverter.ConvertFromString(tmp);

			SourceGrid2.DataModels.EditorUITypeEditor l_editorDt2 = new SourceGrid2.DataModels.EditorUITypeEditor(typeof(DateTime));
			l_editorDt2.TypeConverter = l_dtConverter;
			grid[l_CurrentRow, 1] = new Cells.Cell(DateTime.Today,l_editorDt2);

			l_CurrentRow++;

			#endregion
			
			#region Image And Text Properties
			grid[l_CurrentRow, 0] = new Cells.Cell("Image And Text Properties", null, l_TitleModel);
			grid[l_CurrentRow, 0].ColumnSpan = 3;
			l_CurrentRow++;

			//Cell Image
			Cells.Cell l_CellImage1 = new Cells.Cell("Single Image", null, l_CaptionModel);
			grid[l_CurrentRow,2] = l_CellImage1;
			l_CellImage1.RowSpan = 5;
			SourceGrid2.VisualModels.Common l_ModelImage = new SourceGrid2.VisualModels.Common(false);
			l_ModelImage.Image = SampleImages.FACE02;
			l_CellImage1.VisualModel = l_ModelImage;

			grid[l_CurrentRow,0] = new Cells.Cell("Image Alignment", null, l_CaptionModel);
			grid[l_CurrentRow,1] = new Cells.Cell(l_ModelImage.ImageAlignment,typeof(ContentAlignment));
			grid[l_CurrentRow,1].Behaviors.Add(new SourceGrid2.BehaviorModels.BindProperty(typeof(SourceGrid2.VisualModels.Common).GetProperty("ImageAlignment"), l_ModelImage));

			l_CurrentRow++;

			grid[l_CurrentRow,0] = new Cells.Cell("Stretch Image", null, l_CaptionModel);
			grid[l_CurrentRow,1] = new Cells.Cell(l_ModelImage.ImageStretch,typeof(bool));
			grid[l_CurrentRow,1].Behaviors.Add(new SourceGrid2.BehaviorModels.BindProperty(typeof(SourceGrid2.VisualModels.Common).GetProperty("ImageStretch"), l_ModelImage));

			l_CurrentRow++;

			grid[l_CurrentRow,0] = new Cells.Cell("Text Alignment", null, l_CaptionModel);
			grid[l_CurrentRow,1] = new Cells.Cell(l_ModelImage.TextAlignment,typeof(ContentAlignment));
			grid[l_CurrentRow,1].Behaviors.Add(new SourceGrid2.BehaviorModels.BindProperty(typeof(SourceGrid2.VisualModels.Common).GetProperty("TextAlignment"), l_ModelImage));

			l_CurrentRow++;

			grid[l_CurrentRow,0] = new Cells.Cell("AlignTextToImage", null, l_CaptionModel);
			grid[l_CurrentRow,1] = new Cells.Cell(l_ModelImage.AlignTextToImage,typeof(bool));
			grid[l_CurrentRow,1].Behaviors.Add(new SourceGrid2.BehaviorModels.BindProperty(typeof(SourceGrid2.VisualModels.Common).GetProperty("AlignTextToImage"), l_ModelImage));

			l_CurrentRow++;

			grid[l_CurrentRow,0] = new Cells.Cell("StringFormat.FormatFlags", null, l_CaptionModel);
			grid[l_CurrentRow,1] = new Cells.Cell(l_ModelImage.StringFormat.FormatFlags,typeof(StringFormatFlags));
			grid[l_CurrentRow,1].Behaviors.Add(new SourceGrid2.BehaviorModels.BindProperty(typeof(StringFormat).GetProperty("FormatFlags"), l_ModelImage.StringFormat));

			l_CurrentRow++;

			// Cell VisualModelMultiImages
			grid[l_CurrentRow,2] = new Cells.Cell("Multi Images");
			grid[l_CurrentRow,2].RowSpan = 5;
			SourceGrid2.VisualModels.MultiImages l_ModelMultiImages = new SourceGrid2.VisualModels.MultiImages(false);
			l_ModelMultiImages.SubImages.Add(new SourceGrid2.PositionedImage(SampleImages.FACE00,ContentAlignment.TopLeft));
			l_ModelMultiImages.SubImages.Add(new SourceGrid2.PositionedImage(SampleImages.FACE01,ContentAlignment.TopRight));
			l_ModelMultiImages.SubImages.Add(new SourceGrid2.PositionedImage(SampleImages.FACE02,ContentAlignment.BottomLeft));
			l_ModelMultiImages.SubImages.Add(new SourceGrid2.PositionedImage(SampleImages.FACE04,ContentAlignment.BottomRight));
			l_ModelMultiImages.StringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
			grid[l_CurrentRow,2].VisualModel = l_ModelMultiImages;

			l_CurrentRow++;
			l_CurrentRow++;
			l_CurrentRow++;
			l_CurrentRow++;
			#endregion


			grid.AutoSizeAll();
			grid.AutoStretchColumnsToFitWidth = true;
			grid.StretchColumnsToFitWidth();
		}

		private void CellCustomFormat_ValueChanged(object sender, SourceGrid2.PositionEventArgs e)
		{
			e.Cell.Invalidate(e.Position);
		}

		private void l_KeysCombo_ConvertingValueToDisplayString(object sender, SourceLibrary.ComponentModel.ConvertingObjectEventArgs e)
		{
			if (e.Value is Keys)
			{
				e.Value = (int)((Keys)e.Value) + " - " + e.Value.ToString();
			}
		}
	}
}
