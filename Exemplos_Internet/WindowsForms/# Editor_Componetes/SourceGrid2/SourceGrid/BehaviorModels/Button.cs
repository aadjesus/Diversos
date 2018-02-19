using System;
using SourceGrid2.Cells;

namespace SourceGrid2.BehaviorModels
{
	/// <summary>
	/// Summary description for BehaviorModelButton. This behavior can be shared between multiple cells.
	/// </summary>
	public class Button : BehaviorModelGroup
	{
		public readonly static Button Default = new Button();

		public override void OnMouseDown(PositionMouseEventArgs e)
		{
			base.OnMouseDown (e);

			e.Grid.InvalidateCell(e.Position);
		}

		public override void OnMouseUp(PositionMouseEventArgs e)
		{
			base.OnMouseUp (e);

			e.Grid.InvalidateCell(e.Position);
		}
	}
}
