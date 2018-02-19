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
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Mask;

namespace CustomControlInGrid
{
    [UserRepositoryItem("Register")]
    class CustomRepositoryItem : RepositoryItem
    {
        UserControl _drawControl;
        internal UserControl DrawControl
        {
            get { return _drawControl; }
        }
        UserControl _editorControl;
        internal UserControl EditorControl
        {
            get { return _editorControl; }
        }
        internal Dictionary<string, RepositoryItem> ControlRepositories;

        Type _controlType;
        public Type ControlType
        {
            get { return _controlType; }
            set
            {
                if (_controlType == value)
                    return;
                _controlType = value;
                ConstructorInfo cConstructor = ControlType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance | BindingFlags.NonPublic, null, new Type[] { }, null);
                _drawControl = cConstructor.Invoke(null) as UserControl;
                _editorControl = cConstructor.Invoke(null) as UserControl;
                UpdateControlRepositoreies();
                this.OnPropertiesChanged();
            }
        }
        void UpdateControlRepositoreies()
        {
            ControlRepositories = new Dictionary<string, RepositoryItem>();
            foreach (Control control in _drawControl.Controls)
            {
                BaseEdit editor = control as BaseEdit;
                if (editor == null)
                    continue;
                ConstructorInfo cConstructor = editor.Properties.GetType().GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance | BindingFlags.NonPublic, null, new Type[] { }, null);
                RepositoryItem ri = cConstructor.Invoke(null) as RepositoryItem;
                if (!ControlRepositories.ContainsKey(ri.EditorTypeName))
                    ControlRepositories.Add(ri.EditorTypeName, ri);
            }
        }
        static CustomRepositoryItem()
        {
            Register();
        }
        public CustomRepositoryItem()
            : base()
        {
        }
        internal const string EditorName = "Custom Control";

        public static void Register()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName, typeof(CustomControl),
                typeof(CustomRepositoryItem), typeof(CustomControlViewInfo),
                new CustomControlPainter(), true, null));
        }
        public override string EditorTypeName
        {
            get { return EditorName; }
        }
        public override void Assign(RepositoryItem item)
        {
            base.Assign(item);
            ControlType = (item as CustomRepositoryItem).ControlType;
        }
    }

    public interface IEditValue
    {
        object EditValue { get; set; }
        event EventHandler EditValueChanged;
    }


}
