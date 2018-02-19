using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace SourceGrid2.DataModels
{
	public class EditorDateTime : EditorControlBase
	{
		public EditorDateTime():base(typeof(DateTime))
		{
		}

		#region Edit Control
		public override Control CreateEditorControl()
		{
			System.Windows.Forms.DateTimePicker l_dtPicker = new DateTimePicker();
			l_dtPicker.Format = DateTimePickerFormat.Short;
			return l_dtPicker;
		}
		public virtual System.Windows.Forms.DateTimePicker GetEditorDateTimePicker(GridVirtual p_Grid)
		{
			return (System.Windows.Forms.DateTimePicker)GetEditorControl(p_Grid);
		}
		#endregion

		/// <summary>
		/// Start editing the cell passed
		/// </summary>
		/// <param name="p_Cell">Cell to start edit</param>
		/// <param name="p_Position">Editing position(Row/Col)</param>
		/// <param name="p_StartEditValue">Can be null(in this case use the p_cell.Value</param>
		public override void InternalStartEdit(Cells.ICellVirtual p_Cell, Position p_Position, object p_StartEditValue)
		{
			base.InternalStartEdit(p_Cell, p_Position, p_StartEditValue);

			if (EnableEdit==false)
				return;

			DateTimePicker l_DtPicker = GetEditorDateTimePicker(p_Cell.Grid);
			if (p_StartEditValue != null)
			{
				if (p_StartEditValue is DateTime)
					l_DtPicker.Value = (DateTime)p_StartEditValue;
				else if (p_StartEditValue == null)
					l_DtPicker.Value = DateTime.Now;
				else
					throw new SourceGridException("Invalid StartEditValue, expected DateTime");
			}
			else
			{
				object l_Val = p_Cell.GetValue(p_Position);
				if (l_Val is DateTime)
					l_DtPicker.Value = (DateTime)l_Val;
				else if (l_Val == null)
					l_DtPicker.Value = DateTime.Now;
				else
					throw new SourceGridException("Invalid cell value, expected DateTime");
			}
		}

		/// <summary>
		/// Returns the value inserted with the current editor control
		/// </summary>
		/// <returns></returns>
		public override object GetEditedValue()
		{
			return GetEditorDateTimePicker(EditCell.Grid).Value;
		}
	}
}

