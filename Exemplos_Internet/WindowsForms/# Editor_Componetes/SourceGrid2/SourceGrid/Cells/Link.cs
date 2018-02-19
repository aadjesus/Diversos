using System;
using System.Drawing;
using System.Windows.Forms;

namespace SourceGrid2.Cells.Virtual
{
	/// <summary>
	/// A cell that contains a HTML style link. Use the click event to execute the link
	/// </summary>
	public abstract class Link : CellVirtual, ICellCursor
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public Link()
		{
			VisualModel = VisualModels.Common.LinkStyle;
			Behaviors.Add(BehaviorModels.Cursor.Default);
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_ExecuteLink">Event to execute when the user Click on this cell</param>
		public Link(PositionEventHandler p_ExecuteLink):this()
		{
			if (p_ExecuteLink!=null)
				Click+=p_ExecuteLink;
		}

		public event PositionEventHandler Click;

		public override void OnClick(PositionEventArgs e)
		{
			base.OnClick (e);

			if (Click!=null)
				Click(this, e);
		}

		/// <summary>
		/// Get the cursor of the specified cell
		/// </summary>
		/// <param name="p_Position"></param>
		public System.Windows.Forms.Cursor GetCursor(Position p_Position)
		{
			return System.Windows.Forms.Cursors.Hand;
		}
	}
}

namespace SourceGrid2.Cells.Real
{
	/// <summary>
	/// A cell that contains a HTML style link. Use the click event to execute the link
	/// </summary>
	public class Link : Cell
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_Value"></param>
		public Link(object p_Value):base(p_Value)
		{
			VisualModel = VisualModels.Common.LinkStyle;
			Behaviors.Add(BehaviorModels.Cursor.Default);

			Cursor =  System.Windows.Forms.Cursors.Hand;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_Value"></param>
		/// <param name="p_ExecuteLink">Event to execute when the user Click on this cell</param>
		public Link(object p_Value, PositionEventHandler p_ExecuteLink):this(p_Value)
		{
			if (p_ExecuteLink!=null)
				Click+=p_ExecuteLink;
		}

		public event PositionEventHandler Click;

		public override void OnClick(PositionEventArgs e)
		{
			base.OnClick (e);

			if (Click!=null)
				Click(this, e);
		}
	}
}
