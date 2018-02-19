using DevExpress.XtraGrid.Columns;
using System.ComponentModel;
using DevExpress.Utils.Serializing;

namespace PropertyGridControl
{
    public class CustomGridColumn : GridColumn
    {
        public CustomGridColumn() : base() { }
        protected override OptionsColumn CreateOptionsColumn() { return new CustomOptionsColumn(); }
        [Description("Provides access to the column's options."), Category("Options"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue)]
        public new CustomOptionsColumn OptionsColumn
        {
            get { return (CustomOptionsColumn)base.OptionsColumn; }
        }
    }
}
