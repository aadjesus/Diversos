using System;
using System.Drawing;

namespace SourceGrid2.Cells.Virtual
{
	/// <summary>
	/// Class that rapresent a row header with resize feature.
	/// </summary>
	public abstract class RowHeader : Header
	{
		public RowHeader():base(VisualModels.Header.RowHeader, BehaviorModels.RowHeader.Default)
		{
		}
	}

}


namespace SourceGrid2.Cells.Real
{
	/// <summary>
	/// Class that rapresent a row header with resize feature.
	/// </summary>
	public class RowHeader : Header
	{
		public RowHeader():base(null, VisualModels.Header.RowHeader, BehaviorModels.RowHeader.Default)
		{
		}

		public RowHeader(object p_Value):base(p_Value, VisualModels.Header.RowHeader, BehaviorModels.RowHeader.Default)
		{
		}
	}

}
