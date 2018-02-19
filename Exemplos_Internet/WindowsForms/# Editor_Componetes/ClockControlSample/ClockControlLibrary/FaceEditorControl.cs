using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ClockControlLibrary
{
	internal partial class FaceEditorControl : UserControl
	{
		private ClockFace _face = ClockFace.Both;
		private IWindowsFormsEditorService _editorService = null;

		public FaceEditorControl()
		{
			InitializeComponent();
		}
		public FaceEditorControl(IWindowsFormsEditorService editorService)
		{
			InitializeComponent();
			_editorService = editorService;
		}

		public ClockFace Face
		{
			get { return _face; }
			set { _face = value; }
		}

		protected override void OnPaint(PaintEventArgs e)
		{

			// Get currently selected control
			PictureBox selected;
			switch (_face)
			{
				case ClockFace.Analog:
					selected = this.analogPictureBox;
					break;
				case ClockFace.Digital:
					selected = this.digitalPictureBox;
					break;
				default:
					selected = this.bothPictureBox;
					break;
			}

			// Paint the border
			Graphics g = e.Graphics;
			using (Pen pen = new Pen(Color.DarkGray, 1))
			{
				pen.DashStyle = DashStyle.Dash;
				g.DrawLine(pen, new Point(selected.Left - 2, selected.Top - 2), new Point(selected.Left + selected.Width + 1, selected.Top - 2));
				g.DrawLine(pen, new Point(selected.Left + selected.Width + 1, selected.Top - 2), new Point(selected.Left + selected.Width + 1, selected.Top + selected.Height + 1));
				g.DrawLine(pen, new Point(selected.Left + selected.Width + 1, selected.Top + selected.Height + 1), new Point(selected.Left - 2, selected.Top + selected.Height + 1));
				g.DrawLine(pen, new Point(selected.Left - 2, selected.Top + selected.Height + 1), new Point(selected.Left - 2, selected.Top - 2));
			}

			base.OnPaint(e);
		}

		private void analogPictureBox_Click(object sender, EventArgs e)
		{
			_face = ClockFace.Analog;

			// Close the UI editor upon value selection
			_editorService.CloseDropDown();
		}
		private void digitalPictureBox_Click(object sender, EventArgs e)
		{
			_face = ClockFace.Digital;

			// Close the UI editor upon value selection
			_editorService.CloseDropDown();
		}
		private void bothPictureBox_Click(object sender, EventArgs e)
		{
			_face = ClockFace.Both;

			// Close the UI editor upon value selection
			_editorService.CloseDropDown();
		}
	}
}
