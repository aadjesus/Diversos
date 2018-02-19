using DevExpress.XtraGrid.Columns;
using System.ComponentModel;
using DevExpress.Utils.Serializing;

namespace PropertyGridControl
{
    public class CustomOptionsColumn : OptionsColumn
    {
        bool allowQuickHide;
        public CustomOptionsColumn()
            : base()
        {
            allowQuickHide = true;
        }
        [Description("Gets or sets whether the column allow quick hide."), DefaultValue(true), XtraSerializableProperty()]
        public bool AllowQuickHide
        {
            set
            {
                if (allowQuickHide == value) return;
                allowQuickHide = value;
            }
            get { return allowQuickHide; }
        }
    }
}
