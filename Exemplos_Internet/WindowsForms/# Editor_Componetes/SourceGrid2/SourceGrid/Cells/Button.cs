using System;
using System.Windows.Forms;


namespace SourceGrid2.Cells.Virtual
{
	/// <summary>
	/// A cell that rappresent a button 
	/// </summary>
	public abstract class Button : CellVirtual
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public Button()
		{
			Behaviors.Add(BehaviorModels.Button.Default);
			VisualModel = VisualModels.Header.Default;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_Click"></param>
		public Button(PositionEventHandler p_Click):this()
		{
			if (p_Click!=null)
				Click += p_Click;
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

namespace SourceGrid2.Cells.Real
{
	/// <summary>
	/// A cell that rappresent a button 
	/// </summary>
	public class Button : Cell
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_Value"></param>
		public Button(object p_Value):this(p_Value, null)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="p_Value"></param>
		/// <param name="p_Click"></param>
		public Button(object p_Value, PositionEventHandler p_Click):base(p_Value)
		{
			Behaviors.Add(BehaviorModels.Button.Default);

			VisualModel = VisualModels.Header.Default;
			if (p_Click!=null)
				Click += p_Click;
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
