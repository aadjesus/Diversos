using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Cells = SourceGrid2.Cells.Real;

namespace SampleProject
{
	/// <summary>
	/// Summary description for frmSample8.
	/// </summary>
	public class frmSample8 : System.Windows.Forms.Form
	{
		private SourceGrid2.Grid grid;
		public System.Windows.Forms.CheckBox chkMouseEnter;
		public System.Windows.Forms.CheckBox chkMouseLeave;
		private System.Windows.Forms.TextBox txtOutput;
		public System.Windows.Forms.CheckBox chkMouseMove;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.CheckBox chkGridMouseMove;
		public System.Windows.Forms.CheckBox chkGridOthers;
		public System.Windows.Forms.CheckBox chkCellEvents;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSample8()
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
			this.chkMouseMove = new System.Windows.Forms.CheckBox();
			this.chkMouseEnter = new System.Windows.Forms.CheckBox();
			this.chkMouseLeave = new System.Windows.Forms.CheckBox();
			this.txtOutput = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.chkGridMouseMove = new System.Windows.Forms.CheckBox();
			this.chkGridOthers = new System.Windows.Forms.CheckBox();
			this.chkCellEvents = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grid.AutoSizeMinHeight = 10;
			this.grid.AutoSizeMinWidth = 10;
			this.grid.AutoStretchColumnsToFitWidth = false;
			this.grid.AutoStretchRowsToFitHeight = false;
			this.grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.grid.CustomSort = false;
			this.grid.GridToolTipActive = true;
			this.grid.Location = new System.Drawing.Point(144, 8);
			this.grid.Name = "grid";
			this.grid.Size = new System.Drawing.Size(328, 188);
			this.grid.SpecialKeys = SourceGrid2.GridSpecialKeys.Default;
			this.grid.TabIndex = 0;
			this.grid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grid_MouseDown);
			this.grid.Click += new System.EventHandler(this.grid_Click);
			this.grid.DoubleClick += new System.EventHandler(this.grid_DoubleClick);
			this.grid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.grid_MouseUp);
			this.grid.MouseHover += new System.EventHandler(this.grid_MouseHover);
			this.grid.Leave += new System.EventHandler(this.grid_Leave);
			this.grid.CellGotFocus += new SourceGrid2.PositionCancelEventHandler(this.grid_CellGotFocus);
			this.grid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.grid_MouseMove);
			this.grid.MouseEnter += new System.EventHandler(this.grid_MouseEnter);
			this.grid.MouseLeave += new System.EventHandler(this.grid_MouseLeave);
			this.grid.CellLostFocus += new SourceGrid2.PositionCancelEventHandler(this.grid_CellLostFocus);
			this.grid.Enter += new System.EventHandler(this.grid_Enter);
			// 
			// chkMouseMove
			// 
			this.chkMouseMove.Location = new System.Drawing.Point(24, 28);
			this.chkMouseMove.Name = "chkMouseMove";
			this.chkMouseMove.Size = new System.Drawing.Size(112, 24);
			this.chkMouseMove.TabIndex = 5;
			this.chkMouseMove.Text = "Cell MouseMove";
			// 
			// chkMouseEnter
			// 
			this.chkMouseEnter.Checked = true;
			this.chkMouseEnter.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkMouseEnter.Location = new System.Drawing.Point(24, 52);
			this.chkMouseEnter.Name = "chkMouseEnter";
			this.chkMouseEnter.Size = new System.Drawing.Size(112, 24);
			this.chkMouseEnter.TabIndex = 2;
			this.chkMouseEnter.Text = "Cell MouseEnter";
			// 
			// chkMouseLeave
			// 
			this.chkMouseLeave.Checked = true;
			this.chkMouseLeave.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkMouseLeave.Location = new System.Drawing.Point(24, 76);
			this.chkMouseLeave.Name = "chkMouseLeave";
			this.chkMouseLeave.Size = new System.Drawing.Size(112, 24);
			this.chkMouseLeave.TabIndex = 3;
			this.chkMouseLeave.Text = "Cell MouseLeave";
			// 
			// txtOutput
			// 
			this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.txtOutput.AutoSize = false;
			this.txtOutput.HideSelection = false;
			this.txtOutput.Location = new System.Drawing.Point(4, 216);
			this.txtOutput.Multiline = true;
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtOutput.Size = new System.Drawing.Size(472, 160);
			this.txtOutput.TabIndex = 4;
			this.txtOutput.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(4, 200);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(428, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Output Events";
			// 
			// chkGridMouseMove
			// 
			this.chkGridMouseMove.Location = new System.Drawing.Point(24, 152);
			this.chkGridMouseMove.Name = "chkGridMouseMove";
			this.chkGridMouseMove.Size = new System.Drawing.Size(112, 24);
			this.chkGridMouseMove.TabIndex = 9;
			this.chkGridMouseMove.Text = "Grid MouseMove";
			// 
			// chkGridOthers
			// 
			this.chkGridOthers.Checked = true;
			this.chkGridOthers.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkGridOthers.Location = new System.Drawing.Point(12, 132);
			this.chkGridOthers.Name = "chkGridOthers";
			this.chkGridOthers.Size = new System.Drawing.Size(124, 24);
			this.chkGridOthers.TabIndex = 10;
			this.chkGridOthers.Text = "Grid Events";
			// 
			// chkCellEvents
			// 
			this.chkCellEvents.Checked = true;
			this.chkCellEvents.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCellEvents.Location = new System.Drawing.Point(12, 8);
			this.chkCellEvents.Name = "chkCellEvents";
			this.chkCellEvents.Size = new System.Drawing.Size(124, 24);
			this.chkCellEvents.TabIndex = 11;
			this.chkCellEvents.Text = "Cell Events";
			// 
			// frmSample8
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(480, 383);
			this.Controls.Add(this.chkCellEvents);
			this.Controls.Add(this.chkGridOthers);
			this.Controls.Add(this.chkGridMouseMove);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtOutput);
			this.Controls.Add(this.chkMouseLeave);
			this.Controls.Add(this.chkMouseEnter);
			this.Controls.Add(this.chkMouseMove);
			this.Controls.Add(this.grid);
			this.Name = "frmSample8";
			this.Text = "frmSample8";
			this.Load += new System.EventHandler(this.frmSample8_Load);
			this.ResumeLayout(false);

		}
		#endregion

		public void WriteLine(string p_Line)
		{
			txtOutput.Text += p_Line + "\r\n";
			txtOutput.SelectionLength = 0;
			txtOutput.SelectionStart = txtOutput.Text.Length;
			txtOutput.ScrollToCaret();
		}

		private void frmSample8_Load(object sender, System.EventArgs e)
		{
			grid.Redim(10, 5);

			DebugBehavior l_DebugBehavior = new DebugBehavior(this);
			for (int r = 0; r < grid.RowsCount; r++)
				for (int c = 0; c < grid.ColumnsCount; c++)
				{
					grid[r,c] = new Cells.Cell();
					grid[r,c].DataModel = new SourceGrid2.DataModels.EditorTextBox(typeof(string));
					grid[r,c].Value = grid[r,c].Range.Start.ToString();
					grid[r,c].Behaviors.Add(l_DebugBehavior);
				}

			grid.AutoStretchColumnsToFitWidth = true;
			grid.AutoSizeAll();
		}

		public void PrintGridEvents(string p_Event)
		{
			if (chkGridOthers.Checked)
				WriteLine(p_Event);
		}
		public void PrintCellEvents(string p_Event)
		{
			if (chkCellEvents.Checked)
				WriteLine(p_Event);
		}

		private void grid_CellGotFocus(object sender, SourceGrid2.PositionCancelEventArgs e)
		{
			PrintGridEvents("Grid::CellGotFocus " + e.Position.ToString());
		}

		private void grid_CellLostFocus(object sender, SourceGrid2.PositionCancelEventArgs e)
		{
			PrintGridEvents("Grid::CellLostFocus " + e.Position.ToString());
		}

		private void grid_Click(object sender, System.EventArgs e)
		{
			PrintGridEvents("Grid::Click");
		}

		private void grid_DoubleClick(object sender, System.EventArgs e)
		{
			PrintGridEvents("Grid::DoubleClick");
		}

		private void grid_Enter(object sender, System.EventArgs e)
		{
			PrintGridEvents("Grid::Enter");
		}

		private void grid_Leave(object sender, System.EventArgs e)
		{
			PrintGridEvents("Grid::Leave");
		}

		private void grid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			PrintGridEvents("Grid::MouseDown");
		}

		private void grid_MouseEnter(object sender, System.EventArgs e)
		{
			PrintGridEvents("Grid::MouseEnter");
		}

		private void grid_MouseLeave(object sender, System.EventArgs e)
		{
			PrintGridEvents("Grid::MouseLeave");
		}

		private void grid_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (chkGridMouseMove.Checked)
				PrintGridEvents("Grid::MouseMove");
		}

		private void grid_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			PrintGridEvents("Grid::MouseUp");
		}

		private void grid_MouseHover(object sender, System.EventArgs e)
		{
			PrintGridEvents("Grid::MouseHover");
		}
	}

	public class DebugBehavior : SourceGrid2.BehaviorModels.IBehaviorModel
	{
		public DebugBehavior(frmSample8 p_Debug)
		{
			m_Debug = p_Debug;
		}

		private frmSample8 m_Debug;
		#region IBehaviorModel Members

		public void OnClick(SourceGrid2.PositionEventArgs e)
		{
			m_Debug.PrintCellEvents(e.Position.ToString() + " OnClick");
		}

		public void OnMouseDown(SourceGrid2.PositionMouseEventArgs e)
		{
			m_Debug.PrintCellEvents(e.Position.ToString() + " OnMouseDown");
		}

		public void OnMouseUp(SourceGrid2.PositionMouseEventArgs e)
		{
			m_Debug.PrintCellEvents(e.Position.ToString() + " OnMouseUp");
		}

		public void OnKeyDown(SourceGrid2.PositionKeyEventArgs e)
		{
			m_Debug.PrintCellEvents(e.Position.ToString() + " OnKeyDown");
		}

		public void OnKeyUp(SourceGrid2.PositionKeyEventArgs e)
		{
			m_Debug.PrintCellEvents(e.Position.ToString() + " OnKeyUp");
		}

		public void OnMouseEnter(SourceGrid2.PositionEventArgs e)
		{
			if (m_Debug.chkMouseEnter.Checked == true)
				m_Debug.PrintCellEvents(e.Position.ToString() + " OnMouseEnter");
		}

		public void OnMouseMove(SourceGrid2.PositionMouseEventArgs e)
		{
			if (m_Debug.chkMouseMove.Checked == true)
				m_Debug.PrintCellEvents(e.Position.ToString() + " OnMouseMove");
		}

		public void OnContextMenuPopUp(SourceGrid2.PositionContextMenuEventArgs e)
		{
			m_Debug.PrintCellEvents(e.Position.ToString() + " OnContextMenuPopUp");
		}

		public void OnValueChanged(SourceGrid2.PositionEventArgs e)
		{
			m_Debug.PrintCellEvents(e.Position.ToString() + " OnValueChanged");
		}

		public void OnMouseLeave(SourceGrid2.PositionEventArgs e)
		{
			if (m_Debug.chkMouseLeave.Checked == true)
				m_Debug.PrintCellEvents(e.Position.ToString() + " OnMouseLeave");
		}

		public void OnDoubleClick(SourceGrid2.PositionEventArgs e)
		{
			m_Debug.PrintCellEvents(e.Position.ToString() + " OnDoubleClick");
		}

		public void OnKeyPress(SourceGrid2.PositionKeyPressEventArgs e)
		{
			m_Debug.PrintCellEvents(e.Position.ToString() + " OnKeyPress");
		}

		public void OnFocusEntering(SourceGrid2.PositionCancelEventArgs e)
		{
			m_Debug.PrintCellEvents(e.Position.ToString() + " OnFocusEntering");
		}
		public void OnFocusEntered(SourceGrid2.PositionEventArgs e)
		{
			m_Debug.PrintCellEvents(e.Position.ToString() + " OnFocusEntered");
		}
		public void OnFocusLeaving(SourceGrid2.PositionCancelEventArgs e)
		{
			m_Debug.PrintCellEvents(e.Position.ToString() + " OnFocusLeaving");
		}
		public void OnFocusLeft(SourceGrid2.PositionEventArgs e)
		{
			m_Debug.PrintCellEvents(e.Position.ToString() + " OnFocusLeft");
		}
		public void OnEditStarting(SourceGrid2.PositionCancelEventArgs e)
		{
			m_Debug.PrintCellEvents(e.Position.ToString() + " OnEditStarting");
		}
		public void OnEditEnded(SourceGrid2.PositionCancelEventArgs e)
		{
			m_Debug.PrintCellEvents(e.Position.ToString() + " OnEditEnded");
		}
		public bool CanReceiveFocus
		{
			get{return true;}
		}
		#endregion
	}
}
