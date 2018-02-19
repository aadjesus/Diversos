using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using SourceGrid2.Cells;

namespace SourceGrid2.VisualModels
{
	/// <summary>
	/// Class to manage the visual aspect of a cell. This class can be shared beetween multiple cells.
	/// </summary>
	[Serializable]
	public class Common : VisualModelBase
	{
		/// <summary>
		/// Represents a default Model
		/// </summary>
		public readonly static Common Default = new Common(true);
		/// <summary>
		/// Represents a model with a link style font and forecolor.
		/// </summary>
		public readonly static Common LinkStyle;

		static Common()
		{
			LinkStyle = new Common(false);
			LinkStyle.Font = new Font(FontFamily.GenericSerif,10,FontStyle.Underline);
			LinkStyle.ForeColor = Color.Blue;
			//LinkStyle.Cursor = Cursors.Hand;
			LinkStyle.m_bIsReadOnly = true;
		}

		#region Constructors

		/// <summary>
		/// Use default setting and construct a read and write VisualProperties
		/// </summary>
		public Common():this(false)
		{
		}

		/// <summary>
		/// Use default setting
		/// </summary>
		/// <param name="p_bReadOnly"></param>
		public Common(bool p_bReadOnly):base(p_bReadOnly)
		{
			m_SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight);
			m_SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText);
			m_FocusBackColor = ControlPaint.LightLight(m_SelectionBackColor);
			m_FocusForeColor = ForeColor;

			m_Image = null;
			m_ImageAlignment = ContentAlignment.MiddleLeft;
			m_imgStretch = false;
			m_AlignTextToImage = true;

			//Border
			m_FocusBorder = Border;
			m_SelectionBorder = Border;
		}

		/// <summary>
		/// Copy constructor.  This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
		/// </summary>
		/// <param name="p_Source"></param>
		/// <param name="p_bReadOnly"></param>
		public Common(Common p_Source, bool p_bReadOnly):base(p_Source, p_bReadOnly)
		{
			//Duplicate the reference fields
			Image l_tmpImage = null;
			if (p_Source.m_Image!=null)
				l_tmpImage = Utility.ImageClone(p_Source.m_Image);

			m_SelectionBackColor = p_Source.m_SelectionBackColor;
			m_SelectionForeColor = p_Source.m_SelectionForeColor;
			m_FocusBackColor = p_Source.m_FocusBackColor;
			m_FocusForeColor = p_Source.m_FocusForeColor;
			m_Image = l_tmpImage;
			m_ImageAlignment = p_Source.m_ImageAlignment;
			m_imgStretch = p_Source.ImageStretch;
			m_AlignTextToImage = p_Source.m_AlignTextToImage;
			m_FocusBorder = p_Source.m_FocusBorder;
			m_SelectionBorder = p_Source.m_SelectionBorder;
		}
		#endregion

		#region Format
		#region BackColor/ForeColor
		private Color m_SelectionForeColor; 

		/// <summary>
		/// Selection fore color (when Select is true)
		/// </summary>
		public Color SelectionForeColor
		{
			get{return m_SelectionForeColor;}
			set
			{
				if (m_bIsReadOnly)
					throw new ObjectIsReadOnlyException("VisualProperties is readonly.");
				m_SelectionForeColor = value;
				OnChange();
			}
		}

		private Color m_SelectionBackColor; 

		/// <summary>
		/// Selection back color (when Select is true)
		/// </summary>
		public Color SelectionBackColor
		{
			get{return m_SelectionBackColor;}
			set
			{
				if (m_bIsReadOnly)
					throw new ObjectIsReadOnlyException("VisualProperties is readonly.");
				m_SelectionBackColor = value;
				OnChange();
			}
		}


		private Color m_FocusForeColor; 

		/// <summary>
		/// Focus ForeColor (when Focus is true)
		/// </summary>
		public Color FocusForeColor
		{
			get{return m_FocusForeColor;}
			set
			{
				if (m_bIsReadOnly)
					throw new ObjectIsReadOnlyException("VisualProperties is readonly.");
				m_FocusForeColor = value;
				OnChange();
			}
		}

		private Color m_FocusBackColor; 

		/// <summary>
		/// Focus BackColor (when Focus is true)
		/// </summary>
		public Color FocusBackColor
		{
			get{return m_FocusBackColor;}
			set
			{
				if (m_bIsReadOnly)
					throw new ObjectIsReadOnlyException("VisualProperties is readonly.");
				m_FocusBackColor = value;
				OnChange();
			}
		}
		#endregion

		private Image m_Image = null;

		/// <summary>
		/// Image of the cell
		/// </summary>
		public Image Image
		{
			get{return m_Image;}
			set
			{
				if (m_bIsReadOnly)
					throw new ObjectIsReadOnlyException("VisualProperties is readonly.");
				m_Image = value;
				OnChange();
			}
		}
		private bool m_imgStretch = false;
		/// <summary>
		/// True to stretch the image otherwise false
		/// </summary>
		public bool ImageStretch
		{
			get{return m_imgStretch;}
			set
			{
				if (m_bIsReadOnly)
					throw new ObjectIsReadOnlyException("VisualProperties is readonly.");
				m_imgStretch = value;
				OnChange();
			}
		}

		private bool m_AlignTextToImage = true;
		/// <summary>
		/// True to align the text with the image.
		/// </summary>
		public bool AlignTextToImage
		{
			get{return m_AlignTextToImage;}
			set
			{
				if (m_bIsReadOnly)
					throw new ObjectIsReadOnlyException("VisualProperties is readonly.");
				m_AlignTextToImage = value;
				OnChange();
			}
		}

		private ContentAlignment m_ImageAlignment = ContentAlignment.MiddleLeft;
		/// <summary>
		/// Image Alignment
		/// </summary>
		public ContentAlignment ImageAlignment
		{
			get{return m_ImageAlignment;}
			set
			{
				if (m_bIsReadOnly)
					throw new ObjectIsReadOnlyException("VisualProperties is readonly.");
				m_ImageAlignment = value;
				OnChange();
			}
		}

		#region Border

		private RectangleBorder m_FocusBorder;
		/// <summary>
		/// The border of a cell when have the focus
		/// </summary>
		public RectangleBorder FocusBorder
		{
			get{return m_FocusBorder;}
			set
			{
				if (m_bIsReadOnly)
					throw new ObjectIsReadOnlyException("VisualProperties is readonly.");
				m_FocusBorder = value;
				OnChange();
			}
		}

		private RectangleBorder m_SelectionBorder;
		/// <summary>
		/// The border of the cell when is selected
		/// </summary>
		public RectangleBorder SelectionBorder
		{
			get{return m_SelectionBorder;}
			set
			{
				if (m_bIsReadOnly)
					throw new ObjectIsReadOnlyException("VisualProperties is readonly.");
				m_SelectionBorder = value;
				OnChange();
			}
		}
		#endregion
		#endregion

		#region Clone
		/// <summary>
		/// Clone this object. This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
		/// </summary>
		/// <param name="p_bReadOnly">True if the new object must be read only, otherwise false.</param>
		/// <returns></returns>
		public override object Clone(bool p_bReadOnly)
		{
			return new Common(this, p_bReadOnly);
		}
		#endregion

		#region GetRequiredSize
		/// <summary>
		/// Returns the minimum required size of the current cell, calculating using the current DisplayString, Image and Borders informations.
		/// </summary>
		/// <param name="p_Graphics"></param>
		/// <param name="p_Cell"></param>
		/// <param name="p_CellPosition"></param>
		/// <returns></returns>
		public override SizeF GetRequiredSize(Graphics p_Graphics,
			Cells.ICellVirtual p_Cell,
			Position p_CellPosition)
		{
			return Utility.CalculateRequiredSize(p_Graphics, p_Cell.GetDisplayText(p_CellPosition), StringFormat, GetCellFont(), m_Image, m_ImageAlignment, m_AlignTextToImage, m_imgStretch, Border);
		}

		#endregion

		#region DrawCell
		/// <summary>
		/// Draw the background of the specified cell. Background
		/// </summary>
		/// <param name="p_Cell"></param>
		/// <param name="p_CellPosition"></param>
		/// <param name="e">Paint arguments</param>
		/// <param name="p_ClientRectangle">Rectangle position where draw the current cell, relative to the current view,</param>
		/// <param name="p_Status"></param>
		protected override void DrawCell_Background(Cells.ICellVirtual p_Cell,
			Position p_CellPosition,
			PaintEventArgs e, 
			Rectangle p_ClientRectangle,
			DrawCellStatus p_Status)
		{
			Color l_BackColor = BackColor;
			if (p_Status == DrawCellStatus.Focus)
				l_BackColor = FocusBackColor;
			else if (p_Status == DrawCellStatus.Selected)
				l_BackColor = SelectionBackColor;

			using (SolidBrush br = new SolidBrush(l_BackColor))
			{
				e.Graphics.FillRectangle(br,p_ClientRectangle);
			}
		}

		/// <summary>
		/// Draw the borders of the specified cell.
		/// </summary>
		/// <param name="p_Cell"></param>
		/// <param name="p_CellPosition"></param>
		/// <param name="e">Paint arguments</param>
		/// <param name="p_ClientRectangle">Rectangle position where draw the current cell, relative to the current view,</param>
		/// <param name="p_Status"></param>
		protected override void DrawCell_Border(Cells.ICellVirtual p_Cell,
			Position p_CellPosition,
			PaintEventArgs e, 
			Rectangle p_ClientRectangle,
			DrawCellStatus p_Status)
		{
			RectangleBorder l_Border = Border;
			if (p_Status == DrawCellStatus.Focus)
				l_Border = FocusBorder;
			else if (p_Status == DrawCellStatus.Selected)
				l_Border = SelectionBorder;

			ControlPaint.DrawBorder(e.Graphics, p_ClientRectangle,
				l_Border.Left.Color,
				l_Border.Left.Width,
				ButtonBorderStyle.Solid,
				l_Border.Top.Color,
				l_Border.Top.Width,
				ButtonBorderStyle.Solid,
				l_Border.Right.Color,
				l_Border.Right.Width,
				ButtonBorderStyle.Solid,
				l_Border.Bottom.Color,
				l_Border.Bottom.Width,
				ButtonBorderStyle.Solid);
		}


		/// <summary>
		/// Draw the image and the displaystring of the specified cell.
		/// </summary>
		/// <param name="p_Cell"></param>
		/// <param name="p_CellPosition"></param>
		/// <param name="e">Paint arguments</param>
		/// <param name="p_ClientRectangle">Rectangle position where draw the current cell, relative to the current view,</param>
		/// <param name="p_Status"></param>
		protected override void DrawCell_ImageAndText(Cells.ICellVirtual p_Cell,
			Position p_CellPosition,
			PaintEventArgs e, 
			Rectangle p_ClientRectangle,
			DrawCellStatus p_Status)
		{
			RectangleBorder l_Border = Border;
			Color l_ForeColor = ForeColor;
			if (p_Status == DrawCellStatus.Focus)
			{
				l_Border = FocusBorder;
				l_ForeColor = FocusForeColor;
			}
			else if (p_Status == DrawCellStatus.Selected)
			{
				l_Border = SelectionBorder;
				l_ForeColor = SelectionForeColor;
			}

			Font l_CurrentFont = GetCellFont();

			//Image and Text
			Utility.PaintImageAndText(e.Graphics,
				p_ClientRectangle,
				Image,
				ImageAlignment, 
				ImageStretch, 
				p_Cell.GetDisplayText(p_CellPosition),
				StringFormat,
				AlignTextToImage,
				l_Border,
				l_ForeColor, 
				l_CurrentFont);
		}
		#endregion

		#region HTML Export
		/// <summary>
		/// Write the attributes of the tag specified
		/// </summary>
		/// <param name="p_Cell"></param>
		/// <param name="p_Position"></param>
		/// <param name="p_Export"></param>
		/// <param name="p_Writer"></param>
		/// <param name="p_ElementTagName"></param>
		protected override void ExportHTML_Attributes(Cells.ICellVirtual p_Cell,Position p_Position, IHTMLExport p_Export, System.Xml.XmlTextWriter p_Writer, string p_ElementTagName)
		{
			base.ExportHTML_Attributes(p_Cell, p_Position, p_Export, p_Writer, p_ElementTagName);
			if (p_ElementTagName == "img")
			{
				p_Writer.WriteAttributeString("align", Utility.ContentToHorizontalAlignment(ImageAlignment).ToString().ToLower());
				p_Writer.WriteAttributeString("src", p_Export.ExportImage(Image));
			}		
		}

		/// <summary>
		/// Write the content of the tag specified
		/// </summary>
		/// <param name="p_Cell"></param>
		/// <param name="p_Position"></param>
		/// <param name="p_Export"></param>
		/// <param name="p_Writer"></param>
		/// <param name="p_ElementTagName"></param>
		protected override void ExportHTML_Element(Cells.ICellVirtual p_Cell,Position p_Position, IHTMLExport p_Export, System.Xml.XmlTextWriter p_Writer, string p_ElementTagName)
		{
			base.ExportHTML_Element(p_Cell, p_Position, p_Export, p_Writer, p_ElementTagName);
			if (p_ElementTagName == "td")
			{
				#region Image
				//non esporto le immagini di ordinamento
				if (Image != null && CanExportHTMLImage(Image) )
				{
					p_Writer.WriteStartElement("img");

					ExportHTML_Attributes(p_Cell, p_Position, p_Export, p_Writer, "img");
					ExportHTML_Element(p_Cell, p_Position, p_Export, p_Writer, "img");

					//img
					p_Writer.WriteEndElement();
				}
				#endregion
			}
			else if (p_ElementTagName == "img")
			{
			}
		}
		/// <summary>
		/// Returns true if the specified image can be exported for HTML, otherwise false. Override this method to prevent exporting certains images.
		/// </summary>
		/// <param name="p_Image"></param>
		/// <returns></returns>
		protected virtual bool CanExportHTMLImage(Image p_Image)
		{
			return true;
		}

		#endregion
	}


}
