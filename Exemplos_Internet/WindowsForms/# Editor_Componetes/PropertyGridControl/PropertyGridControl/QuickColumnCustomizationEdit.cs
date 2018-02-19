using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace PropertyGridControl
{
    public class RepositoryItemQuickHideEdit : RepositoryItemPopupContainerEdit
    {
        ColumnPropertiesCollection columns;
        const int minFormWidth = 105, minFormHeight = 70;
        static RepositoryItemQuickHideEdit() { RegisterQuickHideEdit(); }
        public RepositoryItemQuickHideEdit()
        {
            PopupFormMinSize = new System.Drawing.Size(minFormWidth, minFormHeight);
            columns = new ColumnPropertiesCollection();
        }
        public ColumnPropertiesCollection Columns { get { return columns; } set { columns = value; } }

        public const string QuickHideEditName = "QuickHideEdit";
        public override string EditorTypeName { get { return QuickHideEditName; } }
        public static void RegisterQuickHideEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(QuickHideEditName,
              typeof(QuickHideEdit), typeof(RepositoryItemQuickHideEdit),
              typeof(PopupContainerEditViewInfo), new QuickHideEditPainter(), true));
        }
        public override BaseEditViewInfo CreateViewInfo() { return new QuickHideEditViewInfo(this); }
    }
    public class QuickHideEdit : PopupContainerEdit
    {
        const int editorWidth = 10, editorHeight = 11;
        CustomGridView gridView;
        PopupContainerControl integratedPopupControl;
        static QuickHideEdit() { RepositoryItemQuickHideEdit.RegisterQuickHideEdit(); }
        public QuickHideEdit() : base() { }
        public QuickHideEdit(CustomGridView view)
            : base()
        {
            gridView = view;
            Properties.LookAndFeel.Assign(gridView.GetLookAndFeel());
            MakeUnEnable();
            Size = new Size(editorWidth, editorHeight);
            BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            CreatePopupControl();
        }

        private void CreatePopupControl()
        {
            integratedPopupControl = new PopupContainerControl();
            Controls.Add(integratedPopupControl);
            Properties.PopupControl = integratedPopupControl;
        }


        protected internal virtual void MakeEnable()
        {
            Visible = true;
            Enabled = true;
        }
        protected internal virtual void MakeUnEnable()
        {
            Visible = false;
            Enabled = false;
        }
        public int EditorHeight { get { return editorHeight; } }

        public override string EditorTypeName { get { return RepositoryItemQuickHideEdit.QuickHideEditName; } }
        public new RepositoryItemQuickHideEdit Properties { get { return base.Properties as RepositoryItemQuickHideEdit; } }
        protected override PopupBaseForm CreatePopupForm()
        {
            QuickHidePopupForm form = new QuickHidePopupForm(this);
            return form;
        }
        protected override void DoShowPopup()
        {
            base.DoShowPopup();
            ((QuickHidePopupForm)PopupForm).PopulateListBox();
        }
        protected override void DoClosePopup(PopupCloseMode closeMode)
        {
            base.DoClosePopup(closeMode);
            MakeUnEnable();
        }
        public override void ClosePopup()
        {
            base.ClosePopup();
            Properties.Columns = ((QuickHidePopupForm)PopupForm).GetColumns(Properties.Columns);
            if (gridView != null)
                gridView.AcceptQuickHide();
        }
        public CustomGridView GridView { get { return gridView; } }
    }
    public class QuickHideEditViewInfo : PopupContainerEditViewInfo
    {
        public QuickHideEditViewInfo(RepositoryItem item) : base(item) { }
        protected override int CalcMinHeightCore(Graphics g) { return ((QuickHideEdit)OwnerEdit).EditorHeight; }
        protected internal virtual QuickCustomizationIcon GetIcon
        {
            get
            {
                if (((QuickHideEdit)OwnerEdit).GridView != null)
                    return ((QuickHideEdit)OwnerEdit).GridView.OptionsCustomization.QuickCustomizationIcons;
                return new QuickCustomizationIcon();
            }
        }
    }
    public class QuickHideEditPainter : ButtonEditPainter
    {
        protected override void DrawContent(ControlGraphicsInfoArgs info)
        {
            info.Graphics.FillRectangle(info.Cache.GetSolidBrush(Color.White), info.ViewInfo.ClientRect);
            QuickCustomizationIcon icon = ((QuickHideEditViewInfo)info.ViewInfo).GetIcon;
            if (icon.Image != null)
            {
                ImageAttributes attr = new ImageAttributes();
                attr.SetColorKey(icon.TransperentColor, icon.TransperentColor);
                info.Graphics.DrawImage(icon.Image, info.ViewInfo.ClientRect, 0, 0, icon.Image.Width, icon.Image.Height, GraphicsUnit.Pixel, attr);
            }
        }
    }

    public class QuickHidePopupForm : PopupContainerForm
    {
        CustomCheckedListBox listBox;
        public QuickHidePopupForm(PopupContainerEdit ownerEdit)
            : base(ownerEdit)
        {
            CreateChekedListBox();
        }

        private void CreateChekedListBox()
        {
            listBox = new CustomCheckedListBox();
            OwnerEdit.Properties.PopupControl.Controls.Add(listBox);
        }
        protected override void SetupButtons()
        {
            base.SetupButtons();
            fShowOkButton = true;
        }

        public virtual new RepositoryItemQuickHideEdit Properties { get { return (RepositoryItemQuickHideEdit)base.Properties; } }
        public virtual void PopulateListBox()
        {
            listBox.Items.Clear();
            foreach (ColumnProperties column in Properties.Columns)
                listBox.Items.Add(column.Caption, column.CheckState, column.AllowQuickHide);
            listBox.CreateAllowMovingArray();
            for (int i = 0; i < listBox.ItemCount; i++)
                listBox.SetAllowMoving(i, Properties.Columns[i].AllowMove);
        }
        public virtual ColumnPropertiesCollection GetColumns(ColumnPropertiesCollection oldCollection)
        {
            ColumnPropertiesCollection newCollection = new ColumnPropertiesCollection();
            foreach (CheckedListBoxItem item in listBox.Items)
            {
                newCollection.Add(item.Value.ToString(), item.CheckState == CheckState.Checked, oldCollection[listBox.Items.IndexOf(item)].VisibleIndex, item.Enabled);
            }
            return newCollection;

        }
        protected override void UpdateControlPositionsCore()
        {
            base.UpdateControlPositionsCore();
            listBox.Bounds = ViewInfo.ContentRect;
            listBox.Width = listBox.Width - listBox.Left * 2;
        }
    }
}
