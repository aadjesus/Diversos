// Developer Express Code Central Example:
// How to put a custom UserControl in a GridView cell
// 
// This example demonstrates how a custom UserControl can be used as an in-place
// editor in GridView. As described in the http://www.devexpress.com/scid=A128
// Knowledge Base, it is not possible to just place a control within a cell,
// because cells are not controls. When a cell's editor is not activated, its
// content is drawn via a painter. So, in our example, we have created a painter to
// draw the entire UserControl's content. All cells in GridView will be drawn using
// this painter until an end-user clicks a cell. In this case, an actual instance
// of the UserControl class will be created. Controls inherited from the BaseEdit
// class are drawn via their painters, other controls are drawn via the
// DrawToBitmap function. In case of 3rd-party controls, you need to draw them
// manually. If you want to use your custom control in GridView or other controls,
// you need to implement the IEditValue interface in it.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3051

using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.ComponentModel;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.ViewInfo;

namespace CustomControlInGrid
{
    class CustomControl : BaseEdit, IAutoHeightControl
    {
        #region IAutoHeightControl implement
        bool IAutoHeightControl.SupportsAutoHeight { get { return true; } }

        public event EventHandler heightChanged;
        event EventHandler IAutoHeightControl.HeightChanged
        {
            add { heightChanged += value; }
            remove { heightChanged -= value; }
        }
        protected void RaiseHeightChanged()
        {
            if (heightChanged != null)
                heightChanged(this, EventArgs.Empty);
        }

        int IAutoHeightControl.CalcHeight(GraphicsCache cache)
        {
            if (ViewInfo.IsReady)
            {
                IHeightAdaptable ih = ViewInfo as IHeightAdaptable;
                if (ih != null) return ih.CalcHeight(cache, Width);
            }
            return Height;

        }
        #endregion
        static CustomControl()
        {
            CustomRepositoryItem.Register();
        }
        public CustomControl()
            : base()
        {
            UpdateControls();
        }
        void UpdateControls()
        {
            Controls.Clear();
            if (Properties.ControlType == null)
                return;
            Controls.Add(Properties.EditorControl);
            (Properties.EditorControl as IEditValue).EditValueChanged += new EventHandler(this.editor_EditValueChanged);
            (Properties.EditorControl as IEditValue).EditValue = EditValue;
            Height = Properties.EditorControl.Height;
            Width = Properties.EditorControl.Width;
        }

        void editor_EditValueChanged(object sender, EventArgs e)
        {

            EditValue = (Properties.EditorControl as IEditValue).EditValue;
            IsModified = true;
        }
        public override string EditorTypeName
        {
            get { return CustomRepositoryItem.EditorName; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new CustomRepositoryItem Properties
        {
            get { return base.Properties as CustomRepositoryItem; }
        }

        protected override void OnPropertiesChanged()
        {
            base.OnPropertiesChanged();
            UpdateControls();
            this.RaiseHeightChanged();
        }
        public override object EditValue
        {
            get
            {
                return base.EditValue;
            }
            set
            {
                base.EditValue = value;
                (Properties.EditorControl as IEditValue).EditValue = EditValue;
            }
        }
    }

}
