using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Controls;

namespace testpaintcontrol
{
    [UserRepositoryItem("RegisterListEditor")] 
    public class RepositoryItemListEditor : RepositoryItemPopupContainerEdit
    {
        public const string ControlName = "ListEditor";

        /// <summary>Unique name of the editor</summary>
        public override string EditorTypeName 
        {
            get
            {
                return ControlName;
            }
        }

        public RepositoryItemListEditor() : base() 
        {
            IconWidth = 16;
        }

        //The static constructor which calls the registration method
        static RepositoryItemListEditor()
        {
            RegisterListEditor();
        }

        /// <summary>
        /// Registers the editor with the DevExpress Editor Repository.
        /// </summary>
        public static void RegisterListEditor()
        {
            EditorRegistrationInfo.Default.Editors.Add(
                new EditorClassInfo(ControlName, typeof(ListEditor),
                    typeof(RepositoryItemListEditor),
                    typeof(ListEditViewInfo),
                    new ListEditPainter(),
                    true));
        }

        private int _iconWidth;
        [DefaultValue(16)]
        public int IconWidth { get { return _iconWidth; } set { _iconWidth = value; } }
    }

    public class ListEditViewInfo : ButtonEditViewInfo
    {
        public ListEditViewInfo(RepositoryItem ri)
            : base(ri)
        {
        }

        protected override Size CalcClientSize(Graphics g)
        {
            Size s = base.CalcClientSize(g);
            s.Width += 4 + (this.Item as RepositoryItemListEditor).IconWidth;
            return s;
        }

        protected override void CalcContentRect(Rectangle bounds)
        {
            base.CalcContentRect(bounds);
            Rectangle rect = this.fMaskBoxRect;
            rect.Width -= 4 + (this.Item as RepositoryItemListEditor).IconWidth;
            rect.X += 4 + (this.Item as RepositoryItemListEditor).IconWidth;
            this.fMaskBoxRect = rect;
        }
    }

    public class ListEditPainter : ButtonEditPainter
    {

        protected override void DrawButtons(ControlGraphicsInfoArgs info)
        {
            ListEditViewInfo vi = info.ViewInfo as ListEditViewInfo;
            Rectangle rect = info.Bounds;
            rect.Width = 4 + (vi.Item as RepositoryItemListEditor).IconWidth;
            info.Cache.FillRectangle(info.ViewInfo.PaintAppearance.BackColor, rect);
            ImageValue v = vi.EditValue as ImageValue;
            if (v != null){
            Image img = v.Image;
            info.Cache.Paint.DrawImage(info.Graphics, img, new Point(
                rect.X + (rect.Width - img.Width) / 2,
                rect.Y + (rect.Height - img.Height) / 2));
            }
            base.DrawButtons(info);
        }
    }

}
