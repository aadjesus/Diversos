using System;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Resources;

namespace LJ.Controls
{
	#region enum
	public enum ColumnScaleStyle
	{
		Slide,
		Spring
	}
	public enum GridState
	{
	   NONE,
	   ACTIVE,
	   EDIT
	}
	#endregion

	#region StringTools
	public class StringTools
	{
		private StringTools() { }
		
		static public System.Drawing.SizeF MeasureDisplayString(System.Drawing.Graphics graphics, string text, System.Drawing.Font font)
		{
			const int width = 32;
			
			System.Drawing.Bitmap   bitmap = new System.Drawing.Bitmap (width, 1, graphics);
			System.Drawing.SizeF    size   = graphics.MeasureString (text, font);
			System.Drawing.Graphics anagra = System.Drawing.Graphics.FromImage (bitmap);
			
			int measured_width = (int) size.Width;
			
			if (anagra != null) 
			{
				anagra.Clear (System.Drawing.Color.White);
				anagra.DrawString (text+"|", font, System.Drawing.Brushes.Black, width - measured_width, -font.Height / 2);
				
				for (int i = width-1; i >= 0; i--)
				{
					measured_width--;
					if (bitmap.GetPixel (i, 0).R == 0) 
					{
						break;
					}
				}
			}
			
			return new System.Drawing.SizeF (measured_width, size.Height);
		}
		
		static public int MeasureDisplayStringWidth(System.Drawing.Graphics graphics, string text, System.Drawing.Font font)
		{
			return (int) MeasureDisplayString (graphics, text, font).Width;
		}	
	}
	#endregion

	#region Type Converters
	public class ColumnConverter: TypeConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor) && value is Column)
			{
				Column lvi = (Column)value;

				ConstructorInfo ci = typeof(Column).GetConstructor(new Type[] {});
				if (ci != null)
				{
					return new InstanceDescriptor(ci, null, false);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}

	public class RowConverter: TypeConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor) && value is Row)
			{
				Row lvi = (Row)value;

				ConstructorInfo ci = typeof(Row).GetConstructor(new Type[] {});
				if (ci != null)
				{
					return new InstanceDescriptor(ci, null, false);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
	#endregion 
 
	#region Column classes
	[DesignTimeVisible(false), TypeConverter("LJ.Controls.ColumnConverter")]
	public class Column: ICloneable
	{
		#region Event
		public event EventHandler WidthResized;
		private void OnWidthResized()
		{
			if (WidthResized != null)
				WidthResized(this, new EventArgs());
		}
		#endregion

		#region Variable
		private int					left;
		private string				text;
		private HorizontalAlignment textAlign;
		private int					width;
		private bool				visible;
		private Bitmap				image;
		public  ArrayList           items;			//dates
		#endregion

		public Column()
		{
			left=0;
			textAlign = HorizontalAlignment.Left;
			width = 90;
			visible = true;

			items=new ArrayList();
		}


		#region Property
		[Category("Appearance"),Description("The image to display in this header.")]
		public Bitmap Image
		{
			get { return image; }
			set { image = value; }
		}

		[Category("Appearance"), Description("The title of this column header.")]
		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		[Category("Behavior"), Description("The alignment of the column headers title.")]
		public HorizontalAlignment TextAlign
		{
			get { return textAlign; }
			set { textAlign = value; }
		}

		[Category("Behavior"), Description("The left in pixels of this column header.")]
		public int Left
		{
			get{ return left;}
			set{ left=value;}
		}

		[Category("Behavior"), Description("The width in pixels of this column header."), DefaultValue(90)]
		public int Width
		{
			get { return width; }
			set 
			{ 
				if(value < 10)
					width=10;
				else
					width = value; 
				OnWidthResized();
			}
		}

		[Category("Behavior"), Description("Determines wether the control is visible or hidden.")]
		public bool Visible
		{
			get { return visible; }
			set { visible = value; }
		}
		#endregion

		public object Clone()
		{
			Column ch = new Column();
			ch.Text = text;
			ch.TextAlign = textAlign;
			ch.Width = width;

			return ch;
		}

		public override string ToString()
		{
			return text;
		}

		public void RemoveRange(int index,int count)
		{
			items.RemoveRange(index,count);
		}
	}
	#endregion

	#region ColumnCollection
	public class ColumnCollection: CollectionBase
	{
		#region Event
		public event EventHandler ColumnWidthChanged;
		private void OnColumnWidthChanged(object sender,EventArgs e)
		{
			for(int i=1;i<Count;i++) //do myself,rebuild left property
			{
				Column col=this[i];
				col.Left=this[i-1].Left+this[i-1].Width;
			}
			
			if( ColumnWidthChanged != null) // raise event 
				ColumnWidthChanged(sender,e);
		}
		public event EventHandler ColumnAdded; 
		private void OnColumnAdded(object sender, EventArgs e)
		{
			if (ColumnAdded != null)
				ColumnAdded(sender, e);
		}
		#endregion

		#region Interface Implementations		
		public Column this[int index]
		{
			get
			{ 
				Column tch = new Column();
				try
				{
					tch = List[index] as Column;
				}
				catch
				{
					Debug.WriteLine("Column at index " + index + " does not exist.");
				}
				return tch;
			}
			set 
			{ 
				List[index] = value; 
				((Column)List[index]).WidthResized += new EventHandler(ColumnWidthChanged);
			}
		}

		public virtual int Add(Column colhead)
		{
			//colhead.Index = List.Add(colhead);
			int index=List.Add(colhead);
			
			if(index > 0)
				colhead.Left = this[index-1].Left+this[index-1].Width;
			else
				colhead.Left=0;

			OnColumnAdded(this,new EventArgs());
			colhead.WidthResized+=new EventHandler(OnColumnWidthChanged); 
			return index;
		}

		/*public virtual Column Add(string str, int width, HorizontalAlignment textAlign)
		{
			Column tch = new Column();
			tch.Text = str;
			tch.Width = width;
			tch.TextAlign = textAlign;

			lock(List.SyncRoot)
			{
				tch.Index = List.Add(tch);
			}
			OnColumnChanged(this,new EventArgs());
			tch.WidthResized+=new EventHandler(ColumnWidthChanged); 
			return tch;
		}*/

		public virtual void AddRange(Column[] items)
		{
			lock(List.SyncRoot)
			{
				for (int i=0; i< items.Length; i++)
				{
					//List.Add(items[i]);
					//items[i].WidthResized+=new EventHandler(ColumnWidthChanged); 
					this.Add(items[i]);
				}
				//OnColumnChanged(this,new EventArgs());
			}
		}
		#endregion
	}
	#endregion

	#region RowCollection
	public class RowCollection: CollectionBase
	{
		#region Interface Implementations
		public Row this[int index]
		{
			get
			{
				if(index >=0 && index < this.Count)
					return List[index] as Row; 
				else
					return null;
			}
			set
			{
				List[index] = value;
			}
		}
		
		public int this[Row item]
		{
			get { return List.IndexOf(item); }
		}
		public virtual int Add(Row item)
		{
			int index= List.Add(item);
			return index;
		}

		public virtual void AddRange(Row[] items)
		{
			lock(List.SyncRoot)
			{
				for (int i=0; i<items.Length; i++)
				{
					List.Add(items[i]);
				}
			}
		}

		public virtual void Remove(Row item)
		{
			List.Remove(item);
		}

		public new void Clear()
		{			
			List.Clear();
		}
		public virtual int IndexOf(Row item)
		{
			return List.IndexOf(item);
		}
		public virtual void Insert(int index,Row item)
		{
			List.Insert(index,item);
		}
		#endregion
	}
	#endregion
 
	#region Row classes
	[DesignTimeVisible(false), TypeConverter("RowConverter")]
	public class Row: ICloneable
	{
		#region Variables
		private Color		backcolor;
		private Color		forecolor;
		private bool		selected;				
		private int         maxLines;
		private bool        open;
		#endregion

		#region Constructors
		public Row()
		{
			maxLines=1;
			open=false;
			selected=false;
			forecolor=Color.Black;
			backcolor=Color.LightGray; 
		}
		#endregion

		#region Properties
		public int MaxLines
		{
			get { return maxLines;}
			set
			{
				if(maxLines != value)
					maxLines=value;
			}
		}
		public Color BackColor
		{
			get { return backcolor;	}
			set { backcolor = value; }
		}

		public Color ForeColor
		{
			get { return forecolor; }
			set { forecolor = value; }
		}

		public bool Selected
		{
			get { return selected; }
			set { selected = value; }
		}
		#endregion

		#region Methods
		public object Clone()
		{
			Row lvi = new Row();
			lvi.BackColor = backcolor;
			lvi.ForeColor = forecolor;
			lvi.Selected = selected;
			return lvi;
		}
		#endregion
	}
	#endregion

	#region Cell class
	public class Cell
	{
		public int row;
		public int col;
         
		public Cell(int r,int c)
		{
			row=r;
			col=c;
		}
		public void Clear()
		{
			row=-1;
			col=-1;
		}
	}
	#endregion

	#region Row Select EventArgs
	public class RowSelectedEventArgs : System.EventArgs
	{
		public int index=-1;
		public RowSelectedEventArgs(int index)
		{
			this.index=index;
		}
	}
	#endregion

	#region Active Cell Changed EventArgs
	public class ActiveCellChangedEventArgs : System.EventArgs
	{
		public int col=-1;
		public int row=-1;
		public ActiveCellChangedEventArgs(int row,int col)
		{
			this.col=col;
			this.row=row;
		}
	}
	#endregion

	#region Grid
	public class Grid : System.Windows.Forms.Control
	{
		#region Event
		/*public event EventHandler ColumnSelected;
		private void FireColumnSelected()
		{
			if (ColumnSelected != null)
				ColumnSelected(this, new EventArgs());
		}*/
		public event EventHandler RowSelected;
		private void FireRowSelected(int index)
		{
			if (RowSelected != null)
				RowSelected(this, new RowSelectedEventArgs(index));
		}
		public event EventHandler ActiveCellChanged;
		private void FireActiveCellChanged(int row,int col)
		{
			if(ActiveCellChanged != null)
				ActiveCellChanged(this,new ActiveCellChangedEventArgs(row,col));
		}
		#endregion

		#region Variable
         
		public  static ResourceManager		rm;
		private Cell                        ActiveCell=new Cell(-1,-1);
		private int                         ActiveCell_ActiveRow=0;
		private int                         markRow=-1;
		private int                         markRowTop=0;
		private int							markRowHeight=0;
 
		private Point                       mousePoint=new Point(0,0);      
		private Point                       lastMousePoint=new Point(0,0);
		private Point                       globalMousePoint=new Point(0,0);
		private int                         resizeColumn=0;
		private bool						inVSpliteLineMouseDown=false;

		private int							rowHeight=0;

		public  HScrollBarEx				hscrollbar=null;
		public  VScrollBarEx				vscrollbar=null;
		private int                         firstvisibleRow=0;
		private int                         lastvisibleRow=0;
		private int							firstvisibleCol=0;
		private int							lastvisibleCol=0;
		private int							ysclWidth=0;
		private int							xsclHeight=0;
		
		private ColumnCollection			columns;
		private ArrayList					selectedCols;
		
		private RowCollection				rows;
		private ArrayList					selectedRows;   

		private ArrayList					topVSpliteLinesList;

		protected int						allColsWidth  =0;
		protected int						allRowsHeight =0;
 
		protected bool						gridLines = true;
		protected Color						gridLineColor = Color.WhiteSmoke;
       
		private Font						headerFont;
		private int							topHeaderHeight;
		private int							leftHeaderWidth;

		private SolidBrush dark_blueBrush=new SolidBrush(Color.DarkBlue); // for header font  
		private SolidBrush blackBrush=new SolidBrush(Color.Black);        // for content font;
		private Pen white_somkePen=new Pen(Color.WhiteSmoke,2);           // for grid line;  

		private TextBox                      editBox=null;           
  
		private bool                        ctrlkeyDown=false;
		private bool                        shiftkeyDown=false;

		private bool						readOnly=false;
		
		private GridState					gridState=GridState.NONE; 
		 
		private ContextMenu                 rightClickMenu=null;
		
		private ArrayList                   clip=null;
		#endregion

		#region Helper
		private void CalcVisibleRange()
		{
			if(hscrollbar == null || vscrollbar == null)
			{
				return;
			}
            
			if(vscrollbar.Visible)
				ysclWidth=vscrollbar.Width;//24;
			else
				ysclWidth=4;
            
			if(hscrollbar.Visible)
				xsclHeight=hscrollbar.Height;//24;
			else
				xsclHeight=4;
	
			int tmp;
			int i;
			if(rows.Count > 0)
			{
				for(i=firstvisibleRow,tmp=0;i<rows.Count && tmp <Height-xsclHeight-topHeaderHeight;i++)
				{
					tmp+=rows[i].MaxLines * (Font.Height + 4);
				}
				lastvisibleRow=i;
				/*if(lastvisibleRow < 0)
				{
					lastvisibleRow=0;
				}
				else*/ if(lastvisibleRow >= rows.Count)
				{
					lastvisibleRow = rows.Count-1;
				}
			}
			else
			{
				lastvisibleRow =0;
			}
			
			if(columns.Count > 0)
			{
				for(i=firstvisibleCol,tmp=0;i<columns.Count && tmp < Width-ysclWidth-leftHeaderWidth;i++)
				{
					tmp+=columns[i].Width;
				}
				lastvisibleCol=i-1;
				if(lastvisibleCol <0)
				{
					lastvisibleCol=0;
				}
				else if(lastvisibleCol >= columns.Count)
				{
					lastvisibleCol=columns.Count-1;
				}
			}
			else
			{
				lastvisibleCol=0;
			}

		}
		private void ReLocateScrollbars()
		{
			//this.SuspendLayout();

			if(hscrollbar == null || vscrollbar == null)
			{
				return;
			}

			allColsWidth=columns[columns.Count -1].Left+ columns[columns.Count -1].Width; 

			if((allColsWidth > 0) && (allColsWidth > ClientSize.Width-ysclWidth))
			{
				hscrollbar.Visible =true;
			}
			else
			{
				hscrollbar.Visible =false; 
			}

			if((allRowsHeight > 0) && (allRowsHeight > Height - topHeaderHeight -xsclHeight))
			{
				vscrollbar.Visible =true;
			}
			else
			{
				vscrollbar.Visible =false;
			}

			hscrollbar.Left=1;
			hscrollbar.Top=Height-xsclHeight-1;//22;
			hscrollbar.Maximum=columns.Count+3;//allColsWidth;
			if(vscrollbar.Visible)
			{
				hscrollbar.Width=Width-ysclWidth;//21;
			}
			else
			{
				hscrollbar.Width=Width-2;
			}

			vscrollbar.Top=1;//this.Top+2;
			vscrollbar.Left=this.Width-ysclWidth-1;//22;
			vscrollbar.Maximum=allRowsHeight;
			if(hscrollbar.Visible)
			{
				vscrollbar.Height=Height-xsclHeight;//21;
			}
			else
			{
				vscrollbar.Height=Height-2;
			}
			//this.ResumeLayout(true);
		}
		private string TruncatedString(string s, int width, int offset, Graphics g)
		{
			string sprint = "";
			int swid;
			int i;
			SizeF strSize;

			try
			{
				strSize = g.MeasureString(s, headerFont);
				swid = ((int)strSize.Width);
				i=s.Length;

				for (i=s.Length; i>0 && swid > width-offset; i--)
				{
					strSize = g.MeasureString(s.Substring(0, i), this.Font);
					swid = ((int)strSize.Width);				
				}
			
				if (i < s.Length)
				{
					if (i-2 <= 0)
					{
					    if(i==1)
						{
							sprint=".";
						}
						else if(i==2)
						{
							sprint=s.Substring(0,1);
						}
						else
						{
							sprint="";
						}
					}
						sprint = s.Substring(0, i-3) + "...";
				}
				else
					sprint = s.Substring(0, i);
			}
			catch
			{
			}

			return sprint;
		}

		private int GetRowFromY(int Y)
		{
			int tmp=topHeaderHeight;
			int i=0;
			for(i=firstvisibleRow;i<lastvisibleRow;i++)
			{
				int height=rows[i].MaxLines*(Font.Height+4);
				if(Y <height+tmp)
					break;
				tmp+=height;
			}
			if(i > rows.Count-1)
				i=-1;
			return i;
		}
		private int GetColFromX(int X)
		{
			int tmp=leftHeaderWidth;
			int i=0;
			for(i=firstvisibleCol;i<lastvisibleCol;i++)
			{
				int width=columns[i].Width;
				if(X <width+tmp)
					break;
				tmp+=width;
			}
			if(i > columns.Count-1)
				i=-1;
			return i;
		}
		protected Rectangle GetVisibleCellRect(int row,int col)
		{
			if(row < firstvisibleRow || row > lastvisibleRow)
				return new Rectangle(0,0,0,0);
			if(col < firstvisibleCol || col > lastvisibleCol)
				return new Rectangle(0,0,0,0);
			
			int top=topHeaderHeight;
			int height=0;
			for(int i=firstvisibleRow;i<=lastvisibleRow;i++)
			{
				height=rows[i].MaxLines*(Font.Height+4);
				if(row == i)
					break;
				top+=height;      
			}

			int left=leftHeaderWidth+2;
			int width=0;
			for(int i=firstvisibleCol;i<=lastvisibleCol;i++)
			{
				width=columns[i].Width;
				if(col == i)
					break;
				left+=width;
			}
			return new Rectangle(left-1,top,width-2,height-2);
		}

		protected void VisibleCol(int col)
		{
			if(col <0)
			{
				return;
			}
			if(col < firstvisibleCol && col >= 0)
			{				
				hscrollbar.Position=col;
				firstvisibleCol=col;
			}
			else if(col > lastvisibleCol && col < columns.Count)
			{
				int width=0;
				int i;
				for(i=col;i>=0;i--)
				{
					width+=columns[i].Width;
					if(width > Width-leftHeaderWidth-ysclWidth)
						break;
				}
				hscrollbar.Position=i;
				firstvisibleCol=i;
			}
			else if(col == lastvisibleCol)
			{
				hscrollbar.Position= firstvisibleCol++;
				firstvisibleCol++;
			}

			CalcVisibleRange();
			Invalidate(true); 
		}
		protected void VisibleRow(int row)
		{
			if(row <0)
			{
				return;
			}
			if(row < firstvisibleRow && row >=0)
			{
				int height=0;
				for(int i=0;i<row;i++)
				{
					height+=rows[i].MaxLines*rowHeight;
				}
				firstvisibleRow=row;
				vscrollbar.Position=height;
			}
			else if(row >= lastvisibleRow && row < rows.Count)
			{
				int oldfirstvisibleRow=firstvisibleRow;

				int height=0;
				int i;
				for(i=row;i>=0;i--)
				{
					height+=rows[i].MaxLines*rowHeight;
					if(height > Height-topHeaderHeight-xsclHeight)
					{
						i++;
						break;
					}
				}
				for(int j=oldfirstvisibleRow;j<i;j++)
					vscrollbar.Position+=rows[j].MaxLines*rowHeight;
				if(i<0)
				{
					i=0;
				}
				firstvisibleRow=i;
			}
			
			CalcVisibleRange();
			Invalidate(true);
		}
		private void BeginEdit()
		{
			if(readOnly) return;

			if(ActiveCell.col != -1 && ActiveCell.row != -1)
			{
				string tmp=columns[ActiveCell.col].items[ActiveCell.row].ToString();
				if(tmp.Length > 0)
				{
					if(tmp[tmp.Length -1] == '\t')
					{
						tmp=tmp.Substring(0,tmp.Length -1);
					}
				}
				string[] t=tmp.Split('\t');
				if(t.Length > 1)
				{
					Rectangle r=GetVisibleCellRect(ActiveCell.row,ActiveCell.col);
					
					if(ActiveCell_ActiveRow > t.Length-1)
					{
						return;
					}
					else
					{
						editBox.Text=t[ActiveCell_ActiveRow];
						
						editBox.Left=r.Left +5;
						editBox.Top = r.Top+1+ActiveCell_ActiveRow*(FontHeight+4);
						editBox.Height = editBox.Top +FontHeight;
						editBox.Width = r.Width -7;
						editBox.Visible=true;
						editBox.Focus(); 
					}
				}
				else
				{
					editBox.Text=tmp;
					Rectangle r=GetVisibleCellRect(ActiveCell.row,ActiveCell.col);
					editBox.Left=r.Left +5;
					editBox.Top = r.Top+1;
					editBox.Height = r.Height;
					editBox.Width = r.Width -7;
					editBox.Visible=true;
					editBox.Focus(); 
				}
			}
		}
		private void EndEdit()
		{
			if(readOnly) return;
			//if(gridState == GridState.EDIT)
			{
				if(ActiveCell.col != -1 && ActiveCell.row != -1 && editBox.Visible)
				{
					string tmp=columns[ActiveCell.col].items[ActiveCell.row].ToString();
					if(tmp.Length > 0)
					{
						if(tmp[tmp.Length -1] == '\t')
						{
							tmp=tmp.Substring(0,tmp.Length -1);
						}
					}
					string[] t=tmp.Split('\t');
					if(t.Length > 1 && ActiveCell_ActiveRow < t.Length )
					{
						t[ActiveCell_ActiveRow]=editBox.Text;
						tmp="";
						for(int i=0;i<t.Length;i++)
							tmp+=t[i]+'\t';
						columns[ActiveCell.col].items[ActiveCell.row]=tmp;
					}
					else
					{
						columns[ActiveCell.col].items[ActiveCell.row]=editBox.Text; 
					}
				}
			}
			editBox.Visible=false;
			editBox.Text="";
			gridState=GridState.ACTIVE;
		}
		private void RebuildeSelectRowsIndex(int index)
		{
			for(int j=index+1;j<selectedRows.Count;j++)
			{
				int reindex=(int)selectedRows[j]-1;
				selectedRows[j]=reindex;
			}
		}
		#endregion

		#region Constructure
		public Grid()
		{
			rm = new ResourceManager("Controls.Images", Assembly.GetAssembly(GetType())); 
			//rm = new ResourceManager("Controls",Assembly.GetCallingAssembly());

			mousePoint.X=mousePoint.Y=0;

			rowHeight = Font.Height+4;
			BackColor = Color.LightYellow;
			headerFont=new Font("Microsoft Sans Serif",10);
			topHeaderHeight=headerFont.Height+4; 
			leftHeaderWidth=22;

			SetStyle(ControlStyles.AllPaintingInWmPaint|ControlStyles.ResizeRedraw|
				ControlStyles.Opaque|ControlStyles.UserPaint|ControlStyles.DoubleBuffer|
				ControlStyles.Selectable|ControlStyles.UserMouse, true);

			BackColor = SystemColors.Window;

			columns = new ColumnCollection();
			selectedCols =new ArrayList(); 
	
			rows = new RowCollection();
			selectedRows = new ArrayList();
			clip=new ArrayList();

			topVSpliteLinesList=new ArrayList();

			hscrollbar = new HScrollBarEx(this);
			hscrollbar.UsingBothScrollBars=false;
			hscrollbar.DrawGripper=true;
			hscrollbar.BorderColor=Color.FromName("HotTrack");
			hscrollbar.GripperDarkColor=Color.FromName("HotTrack");
			hscrollbar.GripperLightColor=Color.FromName("HotTrack"); 
			hscrollbar.Parent = this;
			hscrollbar.Minimum = 0;
			hscrollbar.Maximum = 0;
			hscrollbar.SmallChange = 1;
			hscrollbar.LargeChange =4;
			hscrollbar.Position=0; 
			hscrollbar.Hide();

			hscrollbar.LineLeft += new EventHandler(OnHScrollPostionChange);
			hscrollbar.LineRight += new EventHandler(OnHScrollPostionChange);
			//hscrollbar.ThumbReleased +=new EventHandler(OnHScrollPostionChange); 
			hscrollbar.ThumbLeft +=new ThumbHandler(OnHScrollThumbChange);
			hscrollbar.ThumbRight +=new ThumbHandler(OnHScrollThumbChange);

			hscrollbar.PageLeft += new EventHandler(OnHScrollPostionChange);
			hscrollbar.PageRight += new EventHandler(OnHScrollPostionChange);

			vscrollbar = new VScrollBarEx(this);
			vscrollbar.UsingBothScrollBars=false;
			vscrollbar.DrawGripper=true;
			vscrollbar.BorderColor=Color.FromName("HotTrack");
			vscrollbar.GripperDarkColor=Color.FromName("HotTrack");
			vscrollbar.GripperLightColor=Color.FromName("HotTrack"); 
			vscrollbar.Parent = this;
			vscrollbar.Minimum = 0;
			vscrollbar.Maximum = 0;
			vscrollbar.SmallChange = rowHeight;
			vscrollbar.LargeChange = 5*rowHeight;
			vscrollbar.Position=0;
			vscrollbar.Hide();
			
			vscrollbar.LineUp += new EventHandler(OnScrollLineUp);
			vscrollbar.LineDown += new EventHandler(OnScrollLineDown);
			vscrollbar.ThumbReleased +=new EventHandler(OnVscrollThumbRelease); 
			vscrollbar.PageUp += new EventHandler(OnVScrollPageUp);
			vscrollbar.PageDown += new EventHandler(OnVScrollPageDown);

			//editBox=new EditEx();
			editBox=new TextBox();
			editBox.BorderStyle = BorderStyle.None;
			editBox.Font=Font;
			editBox.Visible=false;
			Controls.Add(editBox);

			//mouse right clikc menu
			rightClickMenu=new ContextMenu();
			rightClickMenu.MenuItems.Add("Insert befor this row");
			rightClickMenu.MenuItems.Add("Insert after this row");
			rightClickMenu.MenuItems.Add("Append row");
			rightClickMenu.MenuItems.Add("Delete row");
			rightClickMenu.MenuItems.Add("-");
			rightClickMenu.MenuItems.Add("Cut");
			rightClickMenu.MenuItems.Add("Copy");
			rightClickMenu.MenuItems.Add("Paste");

			rightClickMenu.MenuItems[0].Click +=new EventHandler(OnMenu_InsertBefor_Click);
			rightClickMenu.MenuItems[1].Click +=new EventHandler(OnMenu_InsertAfter_Click);
			rightClickMenu.MenuItems[2].Click +=new EventHandler(OnMenu_AppendRow_Click);
			rightClickMenu.MenuItems[3].Click +=new EventHandler(OnMenu_DeleteRow_Click);
			rightClickMenu.MenuItems[5].Click +=new EventHandler(OnMenu_Cut_Click);
			rightClickMenu.MenuItems[6].Click +=new EventHandler(OnMenu_Copy_Click);
			rightClickMenu.MenuItems[7].Click +=new EventHandler(OnMenu_Paste_Click);
			//rightClickMenu.MenuItems[0].OwnerDraw=true;
			//rightClickMenu.MenuItems[1].OwnerDraw=true;
			//rightClickMenu.MenuItems[2].OwnerDraw=true;
			//rightClickMenu.MenuItems[3].OwnerDraw=true;
			//rightClickMenu.MenuItems[0].DrawItem +=new DrawItemEventHandler(OnDrawMenuItem);
			//rightClickMenu.MenuItems[1].DrawItem +=new DrawItemEventHandler(OnDrawMenuItem);
			//rightClickMenu.MenuItems[2].DrawItem +=new DrawItemEventHandler(OnDrawMenuItem);
			//rightClickMenu.MenuItems[3].DrawItem +=new DrawItemEventHandler(OnDrawMenuItem);

			editBox.KeyDown +=new KeyEventHandler(OnEditBoxKeyDown);

			columns.ColumnWidthChanged += new EventHandler(OnColumnWidthChanged);
			columns.ColumnAdded += new EventHandler(OnColumnAdded);

			rowHeight=Font.Height+4;
		}
		#endregion

		#region Draw
		private void DrawSpliteLine(Point pos)
		{
			Graphics g=Graphics.FromHwnd(this.Handle);
			Rectangle rc=new Rectangle(lastMousePoint.X-1,0,3,ClientRectangle.Height);
			Invalidate(rc);
			g.DrawLine(new Pen(Color.Gray,2),pos.X,0,pos.X,ClientRectangle.Height);
			lastMousePoint=pos;
			g.Dispose();
		}
	
		private void DrawBackground(Graphics g,Rectangle r)
		{
			///Draw Background
			g.DrawRectangle(new Pen(Color.FromName("HotTrack"),1),0,0,r.Width-1,r.Height-1); 
			r.Inflate(-1,-1); 
			g.FillRectangle(new SolidBrush(Color.FromName("ControlLightLight")), r);
			
			///Draw Select columns Background
			for(int i=0;i<selectedCols.Count;i++)
			{
				int index=(int)selectedCols[i];
				if(index < firstvisibleCol || index > lastvisibleCol)
					continue;
				int left=leftHeaderWidth;

				for(int j=firstvisibleCol;j<lastvisibleCol && j<index;j++)
				{
					left+=columns[j].Width;
				}
				g.FillRectangle(new SolidBrush(Color.FromName("Control")),left,2,columns[index].Width,Height-4);
			}

			///Draw Select Rows Background
			for(int i=0;i<selectedRows.Count;i++)
			{
				int index=(int)selectedRows[i];
                if(index < firstvisibleRow || index > lastvisibleRow)
					continue;

				int top=topHeaderHeight;
				for(int j=firstvisibleRow;j<lastvisibleRow && j<index;j++)
				{
					top+=rows[j].MaxLines*/*rowHeight*/(Font.Height+4);
				}
				g.FillRectangle(new SolidBrush(Color.FromName("Control")),leftHeaderWidth+2,top+2,Width-ysclWidth-leftHeaderWidth-2,rowHeight-4);
			}
			
			///Draw Mark Row Background
			if(markRow >= firstvisibleRow && markRow <=lastvisibleRow && rows.Count > 0)
			{
				markRowTop=topHeaderHeight;
				for(int i=firstvisibleRow;i<lastvisibleRow && i<markRow;i++)
				{
					markRowTop+=rows[i].MaxLines*rowHeight;
				}
				markRowHeight=rows[markRow].MaxLines*rowHeight;

				g.FillRectangle(new SolidBrush(Color.FromName("inactiveBorder")),leftHeaderWidth+2,markRowTop+2,Width-ysclWidth-leftHeaderWidth-2,markRowHeight-4);
			}
		}
		private void DrawHeaders(Graphics g,Rectangle r)
		{
			///Draw TopHeader                
			Pen WhitePen=new Pen(Color.White,1);
				
			Color cx=Color.White;//Color.FromName("Control");
			Color cy=Color.FromName("Control");//Color.White; 
			Point px=new Point(r.Left+1,r.Top+1);
			Point pb=new Point(r.Left+1,topHeaderHeight-2);
			System.Drawing.Drawing2D.LinearGradientBrush brh=new LinearGradientBrush(px,pb,cx,cy); 
			g.FillRectangle(brh,2,2,Width-4,topHeaderHeight-2); 
	
			topVSpliteLinesList.Clear();
			int tmp=0;
			if(columns.Count > 0)
			{
				for(int i=firstvisibleCol;i<=lastvisibleCol;i++)
				{
					g.DrawLine(white_somkePen,tmp+leftHeaderWidth,2,tmp+leftHeaderWidth,Height-4);
					topVSpliteLinesList.Add(tmp+leftHeaderWidth);
					if (columns[i].Image != null)
					{
						//g.DrawImage(columns[i].Image, new Rectangle(tmp+4, 2, 16, 16));						
						//g.DrawString(TruncatedString(columns[i].Text, columns[i].Width, 25, g), headerFont, dark_blueBrush, tmp+22, 2);
					}
					else
					{
						//string sp = "";
						if (columns[i].TextAlign == HorizontalAlignment.Left)
							g.DrawString(TruncatedString(columns[i].Text, columns[i].Width, 0, g), headerFont, dark_blueBrush, (float)(tmp+leftHeaderWidth+4), (float)(2));
						else if (columns[i].TextAlign == HorizontalAlignment.Right)
						{
							//sp = TruncatedString(columns[i].Text, columns[i].Width, 0, g);
							//g.DrawString(sp, headerFont, dark_blueBrush, (float)(lp_scr+last+columns[i].Width-StringTools.MeasureDisplayStringWidth(g, sp, headerFont)-4), (float)(r.Top+2));
						}
						else
						{
							//sp = TruncatedString(columns[i].Text, columns[i].Width, 0, g);
							//g.DrawString(sp, headerFont, dark_blueBrush, (float)(lp_scr+last+(columns[i].Width/2)-(StringTools.MeasureDisplayStringWidth(g, sp, headerFont)/2)), (float)(r.Top+2));
						}
					}
					tmp += columns[i].Width;
				}
	                
				if(tmp < Width - ysclWidth)
				{
					g.FillRectangle(brh,tmp+leftHeaderWidth, 2, Width-tmp-leftHeaderWidth-2,topHeaderHeight-2);
					g.DrawLine(white_somkePen,tmp+leftHeaderWidth,2,tmp+leftHeaderWidth,Height-4);
					topVSpliteLinesList.Add(tmp+leftHeaderWidth);
				}
			}

			///Draw Left header
			px=new Point(r.Left,r.Top);
			pb=new Point(r.Left+leftHeaderWidth,r.Top);
			brh=new LinearGradientBrush(px,pb,cx,cy); 
			g.FillRectangle(brh,2,topHeaderHeight,leftHeaderWidth,Height-topHeaderHeight-xsclHeight);
		}
		private void DrawContent(Graphics g,Rectangle r)
		{
			if(rows.Count <=0 || columns.Count <=0)
			{
				return;
			}
			int height=topHeaderHeight;
			g.Clip=new Region(new Rectangle(2, topHeaderHeight,Width-ysclWidth,Height-xsclHeight-topHeaderHeight));
            
			try
			{
				for(int i=firstvisibleRow;i<=lastvisibleRow&&i<rows.Count;i++)
				{
					int width=leftHeaderWidth;
					for(int j=firstvisibleCol;j<=lastvisibleCol&&j<columns.Count;j++)
					{
						string tmp="";
						try
						{
							tmp=columns[j].items[i].ToString();
						}
						catch
						{
							tmp="";
						}

						//delete last '\t' char
						if(tmp.Length > 0)
						{
							if(tmp[tmp.Length -1] == '\t')
							{
								tmp=tmp.Substring(0,tmp.Length-1);
							}
							
							string[] t=tmp.Split('\t');
							if(t.Length > 1)
							{
								int hi=height;
								for(int k=0;k<t.Length;k++)
								{
									g.DrawString(t[k],Font,blackBrush,new Rectangle(width+4,hi+1,columns[j].Width-4,Font.Height)); 
									hi+=Font.Height+4;
									g.DrawLine(white_somkePen,width ,hi,columns[j].Width+width,hi);
								}
							}
							else
							{
								g.DrawString(tmp,Font,blackBrush,new Rectangle(width+4,height+1,columns[j].Width,Font.Height)); 
							}
						}
						width+=columns[j].Width;
					}
					height+=rows[i].MaxLines *(Font.Height+4);
					g.DrawLine(white_somkePen,0,height,ClientSize.Width,height); 
				}
			}
			catch(Exception exp)
			{
				MessageBox.Show(exp.Message);
			}
		}
		private void DrawActiveCell(Graphics g)
		{
			if(ActiveCell.row != -1 && ActiveCell.col != -1)
			{
				g.DrawRectangle(new Pen(Color.FromName("HotTrack"),1),GetVisibleCellRect(ActiveCell.row,ActiveCell.col));
			}
		}
		private void DrawMarkRowImage(Graphics g)
		{
			if(markRow >= firstvisibleRow && markRow <=lastvisibleRow && rows.Count>0)
			{
				Image ic=(Image)rm.GetObject("MarkRowAllow");
				g.DrawImage(ic,4,markRowTop+(markRowHeight-16)/2);
			}
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			Rectangle r = ClientRectangle;
			Graphics g = e.Graphics;
			DrawBackground(g,r);
			DrawHeaders(g,r);
			DrawContent(g,r);
			DrawActiveCell(g);
			DrawMarkRowImage(g);
		}
		#endregion
		
		#region Propertys
		public Cell ActivateCell
		{
			get{ return ActiveCell;}
			set
			{
				ActiveCell=value;
				Invalidate();
			}
		}
		[
		Category("Behavior"),
		Description("The lists column headers."),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Editor(typeof(CollectionEditor), typeof(UITypeEditor))
		]
		public ColumnCollection Columns
		{
			get{ return columns; }
			set
			{
				if(columns != value)
				{
					columns=value;
					Invalidate();
				}
			}
		}
		[
		Category("Font"),
		Description("The Content font")
		]
		public new Font Font
		{
			get{return base.Font;}
			set
			{
				if(Font != value)
				{
					Font=value;
					rowHeight=Font.Height+4;
					editBox.Font=value;
				}
			}
		}
		[
		Category("Font"),
		Description("The header font")
		]
		public Font HeaderFont 
		{
			get{ return headerFont;	}
			set
			{
				if(headerFont != value)
				{
					headerFont=value;
					topHeaderHeight=headerFont.Height+4;
					Invalidate();
				}
			}
		}
		[
		Browsable(false),
		Category("Behavior"),
		Description("The lists rows."),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Editor(typeof(CollectionEditor), typeof(UITypeEditor))
		]
		public RowCollection Rows
		{
			get{ return rows;}
		}
		public bool HasGridLine
		{
			get{ return gridLines;}
			set
			{
				if(gridLines != value)
				{
					gridLines=value;
					Invalidate();
				}
			}
		}
		public bool ReadOnly
		{
			get{ return readOnly;}
			set{ readOnly =value;}
		}
		#endregion

		#region Methods
		public void AppendRow()
		{
			Row r=new Row();
			rows.Add(r);
			for(int i=0;i<columns.Count;i++)
			{
				columns[i].items.Add("");
			}
			allRowsHeight+=Font.Height +4;
			OnRowsChanged();
		}
		public void AppendRow(string[] val)
		{
			Row r=new Row();
			rows.Add(r);
			char[] l={'\t'};
			for(int i=0;i<columns.Count;i++)
			{
				string[] p=val[i].Split(l);
				if(p != null)
					r.MaxLines=Math.Max(r.MaxLines,p.GetUpperBound(0));
				columns[i].items.Add(val[i]);
			}
			allRowsHeight+=r.MaxLines * (Font.Height+4);
			OnRowsChanged();
		}
		
		public void InsertRow(int index)
		{
			if(index < 0)
				return;
			LJ.Controls.Row row=new Row();
			row.MaxLines=1;
			rows.Insert(index,row);
			for(int i=0;i<columns.Count;i++)
			{
				columns[i].items.Insert(index,"");
			}
			//Invalidate();			
			allRowsHeight+=rows[index].MaxLines *(Font.Height +4);
			OnRowsChanged();
		}
		
		public void InsertRow(int index,string[] val)
		{
			if(index < 0)
				return;
			int maxline=1;
			for(int i=0;i<val.Length;i++)
			{
				string[] tmp=val[i].Split('\t');
				maxline=Math.Max(tmp.Length,maxline);
			}
			LJ.Controls.Row row=new Row();
			row.MaxLines=maxline;
			rows.Insert(index,row);
			for(int i=0;i<columns.Count;i++)
			{
				string tmp;
				if(i < val.Length)
				{
					tmp=val[i];
				}
				else
				{
					tmp="";
				}
				columns[i].items.Insert(index,tmp);
			}
			//Invalidate();
			allRowsHeight+=rows[index].MaxLines *(Font.Height +4);
			OnRowsChanged();
		}
		
		public void ClearAll()
		{
			for(int i=0;i<columns.Count;i++)
			{
				columns[i].items.Clear();
			}
			markRow=-1;
			rows.Clear();
			allRowsHeight=0;
			OnRowsChanged();
		}
		public void Refreash()
		{
			OnRowsChanged();
		}
		public void RemoveRow(int index,bool delay)
		{
			allRowsHeight-=rows[index].MaxLines *(Font.Height +4);
			rows.Remove(rows[index]);
			for(int i=0;i<columns.Count;i++)
			{
				columns[i].items.RemoveAt(index); 
			}
			if(!delay)
			{
				OnRowsChanged();
			}
		}
		public void RemoveRow(int index)
		{
			if(index <= rows.Count && index >=0)
			{
				allRowsHeight-=rows[index].MaxLines *(Font.Height +4);
				rows.Remove(rows[index]);
				for(int i=0;i<columns.Count;i++)
				{
					columns[i].items.RemoveAt(index); 
				}
				OnRowsChanged();
			}
		}
		public void SetCell(int row,int col,string val)
		{
			if(row > rows.Count-1 || col > columns.Count-1)
				throw new Exception("Bounds out side");
			columns[col].items[row]=val;
			char[] l={'\t'};
			string[] p=val.Split(l);
			int count=p.Length;
			if(rows[row].MaxLines < count)
				rows[row].MaxLines=count;
			Invalidate();
		}
		public void SetCell(int row,int col,string[] val)
		{
			if(row > rows.Count-1 || col > columns.Count-1)
				throw new Exception("Bounds out side");
			int count=val.Length;
			StringBuilder t=new StringBuilder();
			
			for(int i=0;i<count;i++)
			{
				t.Append(val[i]);
				if(i != count-1)
				{
					t.Append("\t");
				}
			}
			columns[col].items[row]=t.ToString();
			if(rows[row].MaxLines < count)
				rows[row].MaxLines=count;
			Invalidate();
		}
		public string GetCell(int row,int col)
		{
			if(row > rows.Count-1 || col > columns.Count-1)
				throw new Exception("Bounds out side");
			//string ret=new string();
			//ret=columns[col].items[row].ToString();
			return columns[col].items[row].ToString();
		}
		public void MarkRow(int index)
		{
			//if(index >= 0 && index < rows.Count )
			markRow=index;
			VisibleRow(index);
		}
		public int GetMarkRow()
		{
			return markRow;
		}
		#endregion

		#region EventHander
		protected override bool ProcessKeyMessage(ref Message m)
		{
			if(m.Msg == (int)Win32.Msg.WM_KEYDOWN)
			{
				switch((int)m.WParam)
				{
					case (int)Win32.VirtualKeys.VK_LEFT:
					{
						if(ActiveCell.col != -1 && ActiveCell.row != -1)
						{
							ActiveCell.col-=1;
							if(ActiveCell.col < 0) ActiveCell.col=0;
							VisibleCol(ActiveCell.col);
						}
						return true;
					}
					case (int)Win32.VirtualKeys.VK_RIGHT:
					{
						if(ActiveCell.col != -1 && ActiveCell.row != -1)
						{
							ActiveCell.col +=1;
							if(ActiveCell.col > columns.Count-1) ActiveCell.col=columns.Count-1;
							VisibleCol(ActiveCell.col);
						}
						return true;	
					}
					case (int)Win32.VirtualKeys.VK_UP:
					{
						if(ActiveCell.col != -1 && ActiveCell.row != -1)
						{
							ActiveCell.row -=1;
							if(ActiveCell.row < 0) ActiveCell.row=0;
							VisibleRow(ActiveCell.row);
						}
						return true;
					}
					case (int)Win32.VirtualKeys.VK_DOWN:
					{
						if(ActiveCell.col != -1 && ActiveCell.row != -1)
						{
							ActiveCell.row +=1;
							if(ActiveCell.row > rows.Count-1) ActiveCell.row=rows.Count-1;
							VisibleRow(ActiveCell.row);
						}
						return true;
					}
					case (int)Win32.VirtualKeys.VK_DELETE:    //key vk_insert
					{
						OnMenu_DeleteRow_Click(this,new EventArgs());
						return true;
					}
					case (int)Win32.VirtualKeys.VK_INSERT:  //key vk_delete
					{
						OnMenu_InsertBefor_Click(this,new EventArgs());
						return true;
					}
				}
			}
			return base.ProcessKeyMessage(ref m) ;
		}
		protected override bool IsInputKey(Keys keyData)  //all keyboard message will be process
		{
			if(keyData==Keys.Left || keyData==Keys.Right || keyData==Keys.Up || keyData==Keys.Down || keyData==Keys.Insert || keyData == Keys.Delete)
				return true;
			return base.IsInputKey(keyData);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			
			// If Mouse buttonleft down and Mouse point in columns's splite, draw splite line
			if(inVSpliteLineMouseDown)
			{
				if( Math.Abs(e.X - lastMousePoint.X) > 4)
					DrawSpliteLine(new Point(e.X,e.Y));
				return;
			}
			
			// Check region mouse is in headre
			Cursor=Cursors.Default;
			if (columns.Count > 0 && e.Y < topHeaderHeight)
			{
				for (int i=1; i<topVSpliteLinesList.Count; i++)
				{							
					int t=(int)topVSpliteLinesList[i];
					if (e.X > t-2 && e.X < t+2 )
					{
						Cursor=Cursors.VSplit;
						resizeColumn=i-1+firstvisibleCol;
						break;
					}
				}
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			//Recode mouse point for DoubleClick event
			globalMousePoint.X=e.X;
			globalMousePoint.Y=e.Y;

			if(e.Button == MouseButtons.Middle)          // middle button click; 
			{
				//MessageBox.Show("ok");
			}
			else if(e.Button == MouseButtons.Right)       // right button click
			{
				Point p=new Point(e.X,e.Y);
				
				if(selectedRows.Count <= 0)
				{
					rightClickMenu.MenuItems[0].Enabled=false;
					rightClickMenu.MenuItems[1].Enabled=false;
					rightClickMenu.MenuItems[3].Enabled=false;
					rightClickMenu.MenuItems[5].Enabled=false;
					rightClickMenu.MenuItems[6].Enabled=false;
					//rightClickMenu.MenuItems[7].Enabled=false;
				}
				else if(selectedRows.Count == 1)
				{
					rightClickMenu.MenuItems[0].Enabled=true;
					rightClickMenu.MenuItems[1].Enabled=true;
					rightClickMenu.MenuItems[2].Enabled=true;
					rightClickMenu.MenuItems[3].Enabled=true;
					rightClickMenu.MenuItems[5].Enabled=true;
					rightClickMenu.MenuItems[6].Enabled=true;
				}
				else if(selectedRows.Count >1)
				{
					rightClickMenu.MenuItems[2].Enabled=true;
					rightClickMenu.MenuItems[3].Enabled=true;
					rightClickMenu.MenuItems[5].Enabled=true;
					rightClickMenu.MenuItems[6].Enabled=true;
				}

				if(clip.Count > 0)
				{
					rightClickMenu.MenuItems[7].Enabled=true;
				}
				else
				{
					rightClickMenu.MenuItems[7].Enabled=false;
				}
				rightClickMenu.Show(this,p);
			}
			else if(e.Button == MouseButtons.Left)   // left button click
			{
				//in top header down
				if(e.Y < topHeaderHeight) 
				{
					selectedRows.Clear();
					if (columns.Count > 0)
					{
						if (Cursor==Cursors.VSplit)
						{      
							inVSpliteLineMouseDown=true;
						}
						else
						{
							inVSpliteLineMouseDown=false;
							
							//in Top Header region, add it to selected columns list.
							if(!ctrlkeyDown)
							{
								selectedCols.Clear();
							}
							int c=GetColFromX(e.X);
							selectedCols.Add(c);
							Invalidate();
						}
					}
					//Record mouse point for draw splite line
					mousePoint.X=e.X;
					mousePoint.Y=e.Y;
				}
				else 
				{
					selectedCols.Clear();

					// in left header down
					if(e.X < leftHeaderWidth) 
					{
						if(!ctrlkeyDown && !shiftkeyDown)
						{
							selectedRows.Clear();
						}
						int row=GetRowFromY(e.Y);
						if(shiftkeyDown && selectedRows.Count >0)
						{
							int index=(int)selectedRows[0];
							int begin=Math.Min(row,index);
							int end=Math.Max(row,index);
							selectedRows.Clear();
							for(int i=begin;i<=end;i++)
							{
								selectedRows.Add(i);
							}
						}
						else
						{
							selectedRows.Add(row);
						}
						Invalidate();
						FireRowSelected(row);
					}
					else // in content down
					{
						selectedRows.Clear();
						selectedCols.Clear();

						int row=GetRowFromY(e.Y);
						int col=GetColFromX(e.X);
						if(row == ActiveCell.row && col == ActiveCell.col)
						{
							Rectangle r=GetVisibleCellRect(ActiveCell.row,ActiveCell.col);
							int k=0;
							for(int i=r.Top;i<r.Bottom && k<rows[row].MaxLines-1;i=i+FontHeight+4,k++)
							{
								if(e.Y >= i && e.Y <= i+FontHeight+4)
								{
									break;
								}
							}
							if(ActiveCell_ActiveRow != k)
							{
								EndEdit();
							}
							ActiveCell_ActiveRow=k;
							BeginEdit();
						}
						else
						{
							EndEdit();
							ActiveCell.col=col;
							ActiveCell.row=row;
							FireActiveCellChanged(row,col);
						}
						Invalidate();
					}
				}
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if(inVSpliteLineMouseDown)
			{
				int width=e.X-mousePoint.X;
				columns[resizeColumn].Width+=width;
				Invalidate();

				mousePoint.X=mousePoint.Y=0;
			}
			inVSpliteLineMouseDown=false;
			Cursor=Cursors.Default;
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			ctrlkeyDown=e.Control;
			shiftkeyDown=e.Shift;
			base.OnKeyDown(e);
		}
		protected override void OnKeyUp(KeyEventArgs e)
		{
			ctrlkeyDown=e.Control;
			shiftkeyDown=e.Shift;
			base.OnKeyUp(e);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ActiveCell.Clear();
			ReLocateScrollbars();
			CalcVisibleRange();
		}
		protected override void OnLayout(LayoutEventArgs levent)
		{
			base.OnLayout(levent);
			this.SuspendLayout();
			ReLocateScrollbars();
			CalcVisibleRange();
			this.ResumeLayout(false);
		}
		protected void OnColumnWidthChanged(object sender, EventArgs e)
		{
			ReLocateScrollbars();
			CalcVisibleRange();
			ActiveCell.Clear();
		}
		protected void OnColumnAdded(object sender,EventArgs e)
		{
			ReLocateScrollbars();
			CalcVisibleRange();
			Invalidate(true);
		}
		protected void OnRowsChanged()
		{
			ReLocateScrollbars();
			CalcVisibleRange();
			Invalidate(true);
		}
		protected void OnScrollLineUp(object sender,EventArgs e)
		{
			EndEdit();
			firstvisibleRow--;
			if(firstvisibleRow < 0)
				firstvisibleRow=0;
			CalcVisibleRange();
			Invalidate();
		}
		protected void OnScrollLineDown(object sender,EventArgs e)
		{
			EndEdit();
			firstvisibleRow++;
			if(firstvisibleRow > rows.Count -1)
				firstvisibleRow=rows.Count -1;
			CalcVisibleRange();
			Invalidate();
		}
		protected void OnVScrollPageUp(object sender,EventArgs e)
		{
			EndEdit();
			firstvisibleRow-=5;
			if(firstvisibleRow < 0)
				firstvisibleRow=0;
			CalcVisibleRange();
			Invalidate();
		}
		protected void OnVScrollPageDown(object sender,EventArgs e)
		{
			EndEdit();
			firstvisibleRow+=5;
			if(firstvisibleRow > rows.Count -1)
				firstvisibleRow=rows.Count -1;
			CalcVisibleRange();
			Invalidate();
		}
		protected void OnVscrollThumbRelease(object sender,EventArgs e)
		{
			EndEdit();
			int i;
			int pos=vscrollbar.Position;
			int upbound=0;
			int lowbound=0;
			for(i=0;i<rows.Count;i++)
			{
				lowbound+=rows[i].MaxLines*(Font.Height+4);
				if(pos>=upbound && pos<=lowbound)
					break;
				upbound=lowbound;
			}
			if(upbound < 0)
				upbound=0;
			if(upbound > vscrollbar.Maximum)
				upbound=vscrollbar.Maximum;
			//vscrollbar.Position=upbound; 
			firstvisibleRow=i;
			CalcVisibleRange();
			Invalidate();
		}	

		protected void OnHScrollPostionChange(object sender,EventArgs e)
		{
			EndEdit();
			firstvisibleCol=hscrollbar.Position;
			CalcVisibleRange();
			Invalidate();
		}
		protected void OnHScrollThumbChange(object sender,int delta)
		{
			EndEdit();
			firstvisibleCol=hscrollbar.Position+delta;
			CalcVisibleRange();
			Invalidate();
		}

		// Menu item draw
		/*protected void OnDrawMenuItem(object sender,DrawItemEventArgs drawItem)
		{
			int idex=drawItem.Index;
			string caption=rightClickMenu.MenuItems[index].Text;
			switch(index)
			{
				case 0:
				case 1:
				case 2:
				case 3:
			}
		}*/
		protected void OnMenu_InsertBefor_Click(object sender,EventArgs e)
		{
			int index=markRow;
			InsertRow(index);
		}
		protected void OnMenu_InsertAfter_Click(object sender,EventArgs e)
		{
			if(selectedRows.Count > 0)
			{
				int index=(int)selectedRows[0];
				InsertRow(index+1);
			}
		}
		protected void OnMenu_AppendRow_Click(object sender,EventArgs e)
		{
			AppendRow();                 
		}
		protected void OnMenu_DeleteRow_Click(object sender,EventArgs e)
		{
			if(selectedRows.Count > 0)
			{
				for(int i=0;i<selectedRows.Count;i++)
				{
					int index=(int)selectedRows[i];
					RemoveRow(index);
					RebuildeSelectRowsIndex(i);
					/*for(int j=i+1;j<selectedRows.Count;j++)
					{
						int reindex=(int)selectedRows[j]-1;
						selectedRows[j]=reindex;
					}*/
				}
				selectedRows.Clear();
			}
		}
		protected void OnMenu_Cut_Click(object sender,EventArgs e)
		{
			clip.Clear();
			for(int i=0;i<selectedRows.Count;i++)
			{
				string[] tmp=new string[columns.Count];
				for(int j=0;j<columns.Count;j++)
				{
					tmp[j]=GetCell((int)selectedRows[i],j);
				}
				clip.Add(tmp);
			}
			for(int i=0;i<selectedRows.Count;i++)
			{
				try
				{
					int index=(int)selectedRows[i];
					RemoveRow(index);
					RebuildeSelectRowsIndex(i);
				}
				catch
				{
				}
			}
			selectedRows.Clear();
		}
		protected void OnMenu_Copy_Click(object sender,EventArgs e)
		{
			clip.Clear();
			for(int i=0;i<selectedRows.Count;i++)
			{
				string[] tmp=new string[columns.Count];
				for(int j=0;j<columns.Count;j++)
				{
					tmp[j]=GetCell((int)selectedRows[i],j);
				}
				clip.Add(tmp);
			}			
		}
		protected void OnMenu_Paste_Click(object sender,EventArgs e)
		{
			if(selectedRows.Count >0)
			{
				int index=(int)selectedRows[0];
				for(int i=0;i<clip.Count;i++)
				{
					InsertRow(index+i);
					string[] tmp=(string[])clip[i];
					for(int j=0;j<tmp.Length;j++)
					{
						string t=tmp[j];
						SetCell(index+i,j,t);
					}
				}
			}
		}
		protected void OnEditBoxKeyDown(object sender,KeyEventArgs e)
		{
			if(e.KeyData == Keys.Return)
			{
				EndEdit();
			}
		}
		#endregion
	}
	#endregion
}
