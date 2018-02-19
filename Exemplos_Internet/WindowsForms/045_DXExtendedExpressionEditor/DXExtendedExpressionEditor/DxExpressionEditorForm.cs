using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;

namespace DXExtendedExpressionEditor
{
    public partial class DxExpressionEditorForm : DevExpress.XtraEditors.Design.UnboundColumnExpressionEditorForm
    {
        public DxExpressionEditorForm(string expr)
            : base(new GridColumn { UnboundExpression = expr, UnboundType = UnboundColumnType.String }, null)
        {
        }

        protected override bool ConvertFieldNamesToCaptions { get { return false; } }

        protected override object[] GetListOfInputTypesObjects(ComponentResourceManager resources)
        {
            // needed as Fields are not shown by default
            return new object[]
                       {
                           resources.GetString("Functions.Text"),
                           resources.GetString("Operators.Text"),
                           resources.GetString("Fields.Text"),
                           //resources.GetString("Parameters.Text"),
                           resources.GetString("Constants.Text"),
                       };
        }

        protected override void FillFieldsTable(Dictionary<string, string> itemsTable)
        {
            // add custom fields
            itemsTable.Add("Key1", "Value1");
            itemsTable.Add("Key2", "Value2");
        }

        protected override void FillParametersTable(Dictionary<string, string> itemsTable)
        {
            // not neede at the moment
        }

        protected override bool ValidateExpression(DevExpress.XtraEditors.MemoEdit expressionMemoEdit)
        {
            // stripped the validation logic
            return true;
        }
    }
}
