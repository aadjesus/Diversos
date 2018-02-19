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
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System.Drawing;

namespace CustomControlInGrid
{
    class CustomControlPainter : BaseEditPainter
    {
        public CustomControlPainter() : base() { }
        public override void Draw(ControlGraphicsInfoArgs info)
        {
            base.Draw(info);

            CustomControlViewInfo vi = info.ViewInfo as CustomControlViewInfo;
            CustomRepositoryItem cri = vi.Item as CustomRepositoryItem;
            if (cri.ControlType == null)
                return;
            (cri.DrawControl as IEditValue).EditValue = vi.EditValue;

            List<BaseEdit> editors = new List<BaseEdit>();
            List<Control> controls = new List<Control>();
            foreach (Control control in cri.DrawControl.Controls)
            {
                BaseEdit editor = control as BaseEdit;
                if (editor != null)
                    editors.Add(editor);
                else
                    controls.Add(control);
            }
            DrawEditors(editors, info, cri);
            DrawControls(controls, info);
        }
        void DrawEditors(List<BaseEdit> editors, ControlGraphicsInfoArgs info, CustomRepositoryItem cri)
        {
            foreach (BaseEdit editor in editors)
            {
                RepositoryItem ri = cri.ControlRepositories[editor.EditorTypeName];
                ri.Assign(editor.Properties);
                BaseEditViewInfo bevi = ri.CreateViewInfo();
                bevi.EditValue = editor.EditValue;
                Rectangle rec = editor.Bounds;

                rec.X += info.ViewInfo.Bounds.X;
                rec.Y += info.ViewInfo.Bounds.Y;

                bevi.CalcViewInfo(info.Graphics, MouseButtons.Left, new Point(0, 0), rec);
                ControlGraphicsInfoArgs cinfo = new ControlGraphicsInfoArgs(bevi, info.Cache, info.ViewInfo.Bounds);
                BaseEditPainter bp = ri.CreatePainter();
                try
                {
                    bp.Draw(cinfo);
                }
                catch { }
            }
        }
        void DrawControls(List<Control> controls, ControlGraphicsInfoArgs info)
        {
            foreach (Control control in controls)
            {
                if (control.Visible == false)
                    continue;
                Bitmap bm = new Bitmap(control.Width, control.Height);
                Control c = control;
                control.DrawToBitmap(bm, new Rectangle(0, 0, bm.Width, bm.Height));
                Point newLoc = control.Location;
                newLoc.Offset(info.ViewInfo.Bounds.Location);
                info.Graphics.DrawImage(bm, newLoc);
            }

        }
    }

}
