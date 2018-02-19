using DevExpress.XtraGrid.Views.Base;
using System.ComponentModel;
using DevExpress.Utils.Serializing;
using System.Drawing;


namespace PropertyGridControl
{
    public class QuickCustomizationIcon : ViewBaseOptions
    {
        Image image;
        Color transperentColor;
        public QuickCustomizationIcon()
        {
            transperentColor = Color.Empty;
        }
        [Description("Allow to chose image to show on QuickCustomizationButton"), XtraSerializableProperty()]
        public Image Image { set { if (image != value) image = value; } get { return image; } }
        [Description("Allow to chose transperent color for QuickCustumisationImage"), XtraSerializableProperty()]
        public Color TransperentColor { get { return transperentColor; } set { if (transperentColor != value) transperentColor = value; } }
    }
    public enum QuickCustomizationIconStatus { Hidden, Enabled, Hot };
}
