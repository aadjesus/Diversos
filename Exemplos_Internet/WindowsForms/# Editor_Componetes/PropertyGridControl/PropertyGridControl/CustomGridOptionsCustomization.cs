using DevExpress.XtraGrid.Views.Grid;
using System.ComponentModel;
using DevExpress.Utils.Serializing;
using DevExpress.Utils.Controls;

namespace PropertyGridControl
{
    public class CustomGridOptionsCustomization : GridOptionsCustomization
    {
        bool allowQuickCustomization;
        QuickCustomizationIcon quickCustomizationIcon;
        public CustomGridOptionsCustomization()
            : base()
        {
            this.allowQuickCustomization = true;
            quickCustomizationIcon = new QuickCustomizationIcon();
        }
        [Description("Gets or sets a value which specifies whether end-users can use quick Customization drop dawn."), DefaultValue(true), XtraSerializableProperty()]
        public virtual bool AllowQuickCustomization
        {
            get { return allowQuickCustomization; }
            set
            {
                if (allowQuickCustomization == value) return;
                bool prevValue = allowQuickCustomization;
                allowQuickCustomization = value;
                OnChanged(new BaseOptionChangedEventArgs("AllowQuickCustomization", prevValue, allowQuickCustomization));
            }
        }
        [Description("Allow chose different icon for QuickCustomizationButton."), Category("Options"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        XtraSerializableProperty()]
        public virtual QuickCustomizationIcon QuickCustomizationIcons { get { return quickCustomizationIcon; } }
    }
}
