using System;
using DXSample;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors.Design;
using DevExpress.XtraEditors.Controls;

namespace PivotGrid {
    public partial class ExpressionBuilderDialog :UnboundColumnExpressionEditorForm {
        private ComboBoxEdit aggregatesList;

        public ExpressionBuilderDialog(ExpressionBuilderColumnInfo columns, Aggregate aggregate) : base(columns, null) {
            aggregatesList = new ComboBoxEdit();
            aggregatesList.Parent = this;
            aggregatesList.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            aggregatesList.Location = new Point(110, 385);
            aggregatesList.Properties.Items.AddRange(Enum.GetNames(typeof(Aggregate)));
            aggregatesList.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            aggregatesList.SelectedIndex = (int)aggregate;
            LabelControl aggregatesLabel = new LabelControl();
            aggregatesLabel.Text = "Aggregate Function:";
            aggregatesLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            aggregatesLabel.Location = new Point(10, 388);
            aggregatesLabel.Parent = this;
        }

        public CustomSummaryExpressionInfo ExpressionInfo { 
            get { 
                return new CustomSummaryExpressionInfo(
                    Expression,
                    (Aggregate)Enum.Parse(typeof(Aggregate), aggregatesList.Text)
                    ); 
            } 
        }
    }
}