using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace SourceGrid2.DataModels
{
	public class EditorTextBoxButton : EditorControlBase
	{
		#region Constructor
		/// <summary>
		/// Construct a Model. Based on the Type specified the constructor populate AllowNull, DefaultValue, TypeConverter, StandardValues, StandardValueExclusive
		/// </summary>
		/// <param name="p_Type">The type of this model</param>
		public EditorTextBoxButton(Type p_Type):base(p_Type)
		{
		}
		#endregion

		#region Edit Control
		public override Control CreateEditorControl()
		{
			SourceLibrary.Windows.Forms.TextBoxTypedButton l_ComboBox = new SourceLibrary.Windows.Forms.TextBoxTypedButton();
			l_ComboBox.TextBox.BorderStyle = BorderStyle.None;
			return l_ComboBox;
		}
		public virtual SourceLibrary.Windows.Forms.TextBoxTypedButton GetEditorTextBoxTypedButton(GridVirtual p_Grid)
		{
			return (SourceLibrary.Windows.Forms.TextBoxTypedButton)GetEditorControl(p_Grid);
		}
		#endregion

		/// <summary>
		/// Start editing the cell passed. Do not call this method for start editing a cell, you must use Cell.StartEdit.
		/// </summary>
		/// <param name="p_Cell">Cell to start edit</param>
		/// <param name="p_Position">Editing position(Row/Col)</param>
		/// <param name="p_StartEditValue">Can be null(in this case use the p_cell.Value</param>
		public override void InternalStartEdit(Cells.ICellVirtual p_Cell, Position p_Position, object p_StartEditValue)
		{
			base.InternalStartEdit(p_Cell, p_Position, p_StartEditValue);

			if (EnableEdit==false)
				return;

			SourceLibrary.Windows.Forms.TextBoxTypedButton l_Combo = GetEditorTextBoxTypedButton(p_Cell.Grid);
			l_Combo.Validator = this;
			l_Combo.EnableEscapeKeyUndo = false;
			l_Combo.EnableEnterKeyValidate = false;
			l_Combo.EnableLastValidValue = false;
			l_Combo.EnableAutoValidation = false;

			if (p_StartEditValue is string && IsStringConversionSupported())
			{
				l_Combo.TextBox.Text = SourceLibrary.Windows.Forms.TextBoxTyped.ValidateCharactersString((string)p_StartEditValue, l_Combo.TextBox.ValidCharacters, l_Combo.TextBox.InvalidCharacters);
				if (l_Combo.TextBox.Text!=null)
					l_Combo.TextBox.SelectionStart = l_Combo.TextBox.Text.Length;
				else
					l_Combo.TextBox.SelectionStart = 0;
			}
			else
			{
				l_Combo.Value = p_Cell.GetValue(p_Position);
				l_Combo.SelectAllTextBox();
			}
		}

		/// <summary>
		/// Returns the value inserted with the current editor control
		/// </summary>
		/// <returns></returns>
		public override object GetEditedValue()
		{
			return GetEditorTextBoxTypedButton(EditCell.Grid).Value;
		}
	}
}

