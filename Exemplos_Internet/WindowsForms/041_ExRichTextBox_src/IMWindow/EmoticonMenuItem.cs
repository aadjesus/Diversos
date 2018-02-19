using System;
using System.Drawing;
using System.Windows.Forms;

namespace IMWindow {

	/// <summary>
	/// Summary description for EmoticonMenuItem.
	/// </summary>
	public class EmoticonMenuItem : MenuItem {

		private const int ICON_WIDTH = 19;
		private const int ICON_HEIGHT = 19;
		private const int ICON_MARGIN = 4;
		private Color backgroundColor, selectionColor, selectionBorderColor;
		private Image image;
		public Image Image {
			get {return image;}
			set {image = value;}
		}

		public EmoticonMenuItem() {
			this.OwnerDraw = true;
			backgroundColor = SystemColors.ControlLightLight;
			selectionColor = Color.FromArgb(50, 0, 0, 150);
			selectionBorderColor = SystemColors.Highlight;
		}

		public EmoticonMenuItem(Image _image) : this() {
			image = _image;
		}

		protected override void OnMeasureItem(MeasureItemEventArgs _args) {
			_args.ItemWidth = ICON_WIDTH + ICON_MARGIN;
			_args.ItemHeight = ICON_HEIGHT + 2 * ICON_MARGIN;
		}

		protected override void OnDrawItem(DrawItemEventArgs _args) {
			Graphics _graphics = _args.Graphics;
			Rectangle _bounds = _args.Bounds;

			DrawBackground(_graphics, _bounds, ((_args.State & DrawItemState.Selected) != 0));
			_graphics.DrawImage(image, _bounds.X + ((_bounds.Width - ICON_WIDTH) / 2), _bounds.Y + ((_bounds.Height - ICON_HEIGHT) / 2));
		}

		private void DrawBackground(Graphics _graphics, Rectangle _bounds, bool _selected) {
			if (_selected) {
				_graphics.FillRectangle(new SolidBrush(selectionColor), _bounds);
				_graphics.DrawRectangle(new Pen(selectionBorderColor), _bounds.X, _bounds.Y,
					_bounds.Width - 1, _bounds.Height - 1);
			}
			else {
				_graphics.FillRectangle(new SolidBrush(backgroundColor), _bounds);
			}
		}
	}
}
