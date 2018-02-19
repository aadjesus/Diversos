using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Design;

namespace SourceGrid2.DataModels
{
	/// <summary>
	///  A model that use a UITypeEditor to edit the cell.
	/// </summary>
	public class EditorUITypeEditor : EditorTextBoxButton
	{
		#region Constructor
		/// <summary>
		/// Construct a Model. Based on the Type specified the constructor populate AllowNull, DefaultValue, TypeConverter, StandardValues, StandardValueExclusive
		/// </summary>
		/// <param name="p_Type">The type of this model</param>
		public EditorUITypeEditor(Type p_Type):base(p_Type)
		{
			object l_Editor = System.ComponentModel.TypeDescriptor.GetEditor(p_Type,typeof(System.Drawing.Design.UITypeEditor));
			if (l_Editor == null)
				throw new SourceGridException("Type not valid, no editor associated to this type");
			m_UITypeEditor = (System.Drawing.Design.UITypeEditor)l_Editor;
		}
		/// <summary>
		/// Construct a Model. Based on the Type specified the constructor populate AllowNull, DefaultValue, TypeConverter, StandardValues, StandardValueExclusive
		/// </summary>
		/// <param name="p_Type">The type of this model</param>
		/// <param name="p_UITypeEditor"></param>
		public EditorUITypeEditor(Type p_Type, UITypeEditor p_UITypeEditor):base(p_Type)
		{
			m_UITypeEditor = p_UITypeEditor;
		}
		#endregion

		#region Edit Control
		public override Control CreateEditorControl()
		{
			SourceLibrary.Windows.Forms.TextBoxButtonUITypeEditor l_ComboBox = new SourceLibrary.Windows.Forms.TextBoxButtonUITypeEditor();
			l_ComboBox.TextBox.BorderStyle = BorderStyle.None;
			return l_ComboBox;
		}
		public virtual SourceLibrary.Windows.Forms.TextBoxButtonUITypeEditor GetEditorTextBoxButtonUITypeEditor(GridVirtual p_Grid)
		{
			return (SourceLibrary.Windows.Forms.TextBoxButtonUITypeEditor)GetEditorControl(p_Grid);
		}
		#endregion

		private UITypeEditor m_UITypeEditor;
		public UITypeEditor UITypeEditor
		{
			get{return m_UITypeEditor;}
			set{m_UITypeEditor = value;}
		}

		public override void InternalStartEdit(SourceGrid2.Cells.ICellVirtual p_Cell, Position p_Position, object p_StartEditValue)
		{
			base.InternalStartEdit (p_Cell, p_Position, p_StartEditValue);

			GetEditorTextBoxButtonUITypeEditor(p_Cell.Grid).UITypeEditor = m_UITypeEditor;
		}


//		/// <summary>
//		/// Start editing the cell passed. Do not call this method for start editing a cell, you must use Cell.StartEdit.
//		/// </summary>
//		/// <param name="p_Cell">Cell to start edit</param>
//		/// <param name="p_Position">Editing position(Row/Col)</param>
//		/// <param name="p_StartEditValue">Can be null(in this case use the p_cell.Value</param>
//		public override void InternalStartEdit(Cells.ICellVirtual p_Cell, Position p_Position, object p_StartEditValue)
//		{
//			base.InternalStartEdit(p_Cell, p_Position, p_StartEditValue);
//
//			if (EnableEdit==false)
//				return;
//
//			SourceLibrary.Windows.Forms.ComboBoxTyped l_Combo = GetEditorComboBox(p_Cell.Grid);
//			l_ComboBox.Validator = this;
//			l_ComboBox.EnableEscapeKeyUndo = false;
//			l_ComboBox.EnableEnterKeyValidate = false;
//			l_ComboBox.EnableLastValidValue = false;
//			l_ComboBox.EnableAutoValidation = false;
//
//			if (p_StartEditValue is string && IsStringConversionSupported())
//			{
//				l_Combo.TextBox.Text = SourceLibrary.Windows.Forms.TextBoxTyped.ValidateCharactersString((string)p_StartEditValue, l_Combo.TextBox.ValidCharacters, l_Combo.TextBox.InvalidCharacters);
//				if (l_Combo.TextBox.Text!=null)
//					l_Combo.TextBox.SelectionStart = l_Combo.TextBox.Text.Length;
//				else
//					l_Combo.TextBox.SelectionStart = 0;
//			}
//			else
//			{
//				l_Combo.Value = p_Cell.GetValue(p_Position);
//				l_Combo.SelectAllDisplayText();
//			}
//		}

//		/// <summary>
//		/// Returns the value inserted with the current editor control
//		/// </summary>
//		/// <returns></returns>
//		public override object GetEditedValue()
//		{
//			return GetEditorComboBox(EditCell.Grid).Value;
//		}
	}
}
