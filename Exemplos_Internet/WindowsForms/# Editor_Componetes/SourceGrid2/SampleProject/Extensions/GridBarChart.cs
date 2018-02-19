using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Cells = SourceGrid2.Cells.Real;

namespace SampleProject.Extensions
{
	/// <summary>
	/// Summary description for GridBarChart.
	/// </summary>
	public class GridBarChart : System.Windows.Forms.UserControl
	{
		private SourceGrid2.Grid grid;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public GridBarChart()
		{
			// This call is required by the Windows.Forms Form Designer.
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

		#region Component Designer generated code
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
			this.grid.AutoSizeMinHeight = 10;
			this.grid.AutoSizeMinWidth = 10;
			this.grid.AutoStretchColumnsToFitWidth = false;
			this.grid.AutoStretchRowsToFitHeight = false;
			this.grid.ContextMenuStyle = SourceGrid2.ContextMenuStyle.None;
			this.grid.CustomSort = false;
			this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grid.GridToolTipActive = true;
			this.grid.Location = new System.Drawing.Point(0, 0);
			this.grid.Name = "grid";
			this.grid.Size = new System.Drawing.Size(556, 444);
			this.grid.SpecialKeys = SourceGrid2.GridSpecialKeys.Default;
			this.grid.TabIndex = 0;
			// 
			// GridBarChart
			// 
			this.Controls.Add(this.grid);
			this.Name = "GridBarChart";
			this.Size = new System.Drawing.Size(556, 444);
			this.ResumeLayout(false);

		}
		#endregion

		private ChartBarCollection m_Bars = new ChartBarCollection();

		public ChartBarCollection Bars
		{
			get{return m_Bars;}
		}

		private int m_StepNumber = 20;

		public int StepNumber
		{
			get{return m_StepNumber;}
			set{m_StepNumber = value;}
		}

		public void LoadChart()
		{
			grid.Redim(0,0);
			if (Bars.Count > 0)
			{
				double l_Max = double.MinValue;
				double l_Min = double.MaxValue;
				for (int i = 0; i < m_Bars.Count; i++)
				{
					if (m_Bars[i].Value > l_Max)
						l_Max = m_Bars[i].Value;

					if (m_Bars[i].Value < l_Min)
						l_Min = m_Bars[i].Value;
				}

				//l_Min -= ((l_Max-l_Min)/((double)m_StepNumber))*2;//per fare in modo che ance il più piccolo sia visibile
				double l_ScalingFactor = ((double)m_StepNumber)/(l_Max-l_Min);

				//Draw Background
				SourceGrid2.VisualModels.Common l_TopBorderModel = new SourceGrid2.VisualModels.Common(false);
				l_TopBorderModel.Border = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black, 1), new SourceGrid2.Border(Color.Black, 0), new SourceGrid2.Border(Color.Black, 0), new SourceGrid2.Border(Color.Black, 0) );
				l_TopBorderModel.BackColor = grid.BackColor;
				l_TopBorderModel.TextAlignment = ContentAlignment.MiddleCenter;
				SourceGrid2.VisualModels.Common l_RightBorderModel = new SourceGrid2.VisualModels.Common(false);
				l_RightBorderModel.Border = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black, 0), new SourceGrid2.Border(Color.Black, 0), new SourceGrid2.Border(Color.Black, 0), new SourceGrid2.Border(Color.Black, 1) );
				l_RightBorderModel.BackColor = grid.BackColor;
				l_RightBorderModel.TextAlignment = ContentAlignment.BottomRight;
				SourceGrid2.VisualModels.Common l_RightTopBorderModel = new SourceGrid2.VisualModels.Common(false);
				l_RightTopBorderModel.Border = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black, 1), new SourceGrid2.Border(Color.Black, 0), new SourceGrid2.Border(Color.Black, 0), new SourceGrid2.Border(Color.Black, 1) );
				l_RightTopBorderModel.BackColor = grid.BackColor;

				grid.Redim(m_StepNumber+2,m_Bars.Count*2 + 2); //+ 1 per gli header e per uno spazio in alto e in basso

				//barra X
				for (int c = 1; c < grid.ColumnsCount; c++)
					grid[grid.RowsCount-1, c] = new Cells.Cell((object)null, null, l_TopBorderModel);

				//barra Y
				double l_YValue = l_Max;
				for (int r = 0; r < grid.RowsCount-1; r++)
				{
					grid[r, 0] = new Cells.Cell(l_YValue.ToString() + " _", null, l_RightBorderModel);
					l_YValue -= 1/l_ScalingFactor;
				}

				//left bottom cell
				grid[grid.RowsCount-1, 0] = new Cells.Cell((object)null, null, l_RightTopBorderModel);

				//bars header
				int l_Col = 2;
				foreach (ChartBar bar in Bars)
				{
					grid[grid.RowsCount-1, l_Col] = new Cells.Cell(bar.Caption + "\n" + bar.Value.ToString(), null, l_TopBorderModel);

					l_Col += 2;
				}

				grid.AutoStretchColumnsToFitWidth = true;
				grid.AutoStretchRowsToFitHeight = true;
				grid.AutoSizeAll(5,5);

				//bars
				l_Col = 2;
				foreach (ChartBar bar in Bars)
				{
					int l_ScaledValue = (int)((bar.Value-l_Min) * l_ScalingFactor);

					if (l_ScaledValue>0)
					{
						grid[(StepNumber - l_ScaledValue) + 1, l_Col] = bar.CreateCell();
						grid[(StepNumber - l_ScaledValue) + 1, l_Col].RowSpan = l_ScaledValue;
					}

					grid[grid.RowsCount-1, l_Col] = new Cells.Cell(bar.Caption + "\n" + bar.Value.ToString(), null, l_TopBorderModel);

					l_Col += 2;
				}
			}
		}
	}


	public class ChartBar
	{
		private string m_Caption = "New Bar";
		private double m_Value = 0;
		private Color m_Color = Color.SteelBlue;
		private Color m_ForeColor = Color.White;

		public ChartBar()
		{
		}

		public string Caption
		{
			get{return m_Caption;}
			set{m_Caption = value;}
		}

		public double Value
		{
			get{return m_Value;}
			set{m_Value = value;}
		}

		public Color Color
		{
			get{return m_Color;}
			set{m_Color = value;}
		}

		public Color ForeColor
		{
			get{return m_ForeColor;}
			set{m_ForeColor = value;}
		}

		public ChartBar(string p_Caption, double p_Value, Color p_Color, Color p_ForeColor)
		{
			Caption = p_Caption;
			Value = p_Value;
			Color = p_Color;
			ForeColor = p_ForeColor;
		}

		public Cells.Cell CreateCell()
		{
			SourceGrid2.VisualModels.Header l_VisualModel = new SourceGrid2.VisualModels.Header(false);
			l_VisualModel.StringFormat.FormatFlags |= StringFormatFlags.DirectionVertical;
			//l_VisualModel.Border = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black, 1));
			l_VisualModel.HeaderLightColor = Color.White;
			l_VisualModel.HeaderLightBorderWidth = 6;
			l_VisualModel.HeaderShadowBorderWidth = 4;
			l_VisualModel.HeaderShadowColor = Color.Gray;
			l_VisualModel.BackColor = Color;
			l_VisualModel.TextAlignment = ContentAlignment.MiddleCenter;
			l_VisualModel.ForeColor = ForeColor;

			Cells.Cell l_Cell = new SourceGrid2.Cells.Real.Cell(Caption, null, l_VisualModel);
			l_Cell.ToolTipText = Caption + " - " + Value.ToString();
			return l_Cell;
		}
	}

	/// <summary>
	/// A collection of elements of type ChartBar
	/// </summary>
	public class ChartBarCollection: System.Collections.CollectionBase
	{
		/// <summary>
		/// Initializes a new empty instance of the ChartBarCollection class.
		/// </summary>
		public ChartBarCollection()
		{
			// empty
		}

		/// <summary>
		/// Initializes a new instance of the ChartBarCollection class, containing elements
		/// copied from an array.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the new ChartBarCollection.
		/// </param>
		public ChartBarCollection(ChartBar[] items)
		{
			this.AddRange(items);
		}

		/// <summary>
		/// Initializes a new instance of the ChartBarCollection class, containing elements
		/// copied from another instance of ChartBarCollection
		/// </summary>
		/// <param name="items">
		/// The ChartBarCollection whose elements are to be added to the new ChartBarCollection.
		/// </param>
		public ChartBarCollection(ChartBarCollection items)
		{
			this.AddRange(items);
		}

		/// <summary>
		/// Adds the elements of an array to the end of this ChartBarCollection.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the end of this ChartBarCollection.
		/// </param>
		public virtual void AddRange(ChartBar[] items)
		{
			foreach (ChartBar item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
		/// Adds the elements of another ChartBarCollection to the end of this ChartBarCollection.
		/// </summary>
		/// <param name="items">
		/// The ChartBarCollection whose elements are to be added to the end of this ChartBarCollection.
		/// </param>
		public virtual void AddRange(ChartBarCollection items)
		{
			foreach (ChartBar item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
		/// Adds an instance of type ChartBar to the end of this ChartBarCollection.
		/// </summary>
		/// <param name="value">
		/// The ChartBar to be added to the end of this ChartBarCollection.
		/// </param>
		public virtual void Add(ChartBar value)
		{
			this.List.Add(value);
		}

		/// <summary>
		/// Determines whether a specfic ChartBar value is in this ChartBarCollection.
		/// </summary>
		/// <param name="value">
		/// The ChartBar value to locate in this ChartBarCollection.
		/// </param>
		/// <returns>
		/// true if value is found in this ChartBarCollection;
		/// false otherwise.
		/// </returns>
		public virtual bool Contains(ChartBar value)
		{
			return this.List.Contains(value);
		}

		/// <summary>
		/// Return the zero-based index of the first occurrence of a specific value
		/// in this ChartBarCollection
		/// </summary>
		/// <param name="value">
		/// The ChartBar value to locate in the ChartBarCollection.
		/// </param>
		/// <returns>
		/// The zero-based index of the first occurrence of the _ELEMENT value if found;
		/// -1 otherwise.
		/// </returns>
		public virtual int IndexOf(ChartBar value)
		{
			return this.List.IndexOf(value);
		}

		/// <summary>
		/// Inserts an element into the ChartBarCollection at the specified index
		/// </summary>
		/// <param name="index">
		/// The index at which the ChartBar is to be inserted.
		/// </param>
		/// <param name="value">
		/// The ChartBar to insert.
		/// </param>
		public virtual void Insert(int index, ChartBar value)
		{
			this.List.Insert(index, value);
		}

		/// <summary>
		/// Gets or sets the ChartBar at the given index in this ChartBarCollection.
		/// </summary>
		public virtual ChartBar this[int index]
		{
			get
			{
				return (ChartBar) this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		/// <summary>
		/// Removes the first occurrence of a specific ChartBar from this ChartBarCollection.
		/// </summary>
		/// <param name="value">
		/// The ChartBar value to remove from this ChartBarCollection.
		/// </param>
		public virtual void Remove(ChartBar value)
		{
			this.List.Remove(value);
		}

		/// <summary>
		/// Type-specific enumeration class, used by ChartBarCollection.GetEnumerator.
		/// </summary>
		public class Enumerator: System.Collections.IEnumerator
		{
			private System.Collections.IEnumerator wrapped;

			public Enumerator(ChartBarCollection collection)
			{
				this.wrapped = ((System.Collections.CollectionBase)collection).GetEnumerator();
			}

			public ChartBar Current
			{
				get
				{
					return (ChartBar) (this.wrapped.Current);
				}
			}

			object System.Collections.IEnumerator.Current
			{
				get
				{
					return (ChartBar) (this.wrapped.Current);
				}
			}

			public bool MoveNext()
			{
				return this.wrapped.MoveNext();
			}

			public void Reset()
			{
				this.wrapped.Reset();
			}
		}

		/// <summary>
		/// Returns an enumerator that can iterate through the elements of this ChartBarCollection.
		/// </summary>
		/// <returns>
		/// An object that implements System.Collections.IEnumerator.
		/// </returns>        
		public new virtual ChartBarCollection.Enumerator GetEnumerator()
		{
			return new ChartBarCollection.Enumerator(this);
		}
	}
}
