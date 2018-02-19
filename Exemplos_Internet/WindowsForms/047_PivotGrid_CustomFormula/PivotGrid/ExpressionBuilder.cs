using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Design;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.Data;
using System.Collections.Generic;

namespace DXSample {
    /// <summary>
    /// See also: http://www.devexpress.com/Support/Center/CodeCentral/ViewExample.aspx?exampleId=E1705
    /// </summary>
    public abstract class ExpressionPanel :PanelControl {
        public ExpressionPanel() {
            BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        }

        object[] arguments;
        protected object[] Arguments { get { return arguments; } }

        ExpressionEditorForm form = null;
        protected ExpressionEditorForm Form { get { return form; } }

        public virtual void StartEdit(params object[] arguments) {
            DestroyForm();
            this.arguments = arguments;
            this.form = CreateForm(arguments);
            if (form == null) return;
            form.Dock = DockStyle.Fill;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Closing += new CancelEventHandler(form_Closing);
            form.buttonCancel.Click += new EventHandler(buttonCancel_Click);
            form.buttonOK.Text = "Apply";
            Controls.Add(form);
            form.Visible = true;
        }

        void buttonCancel_Click(object sender, EventArgs e) {
            if (form != null) form.Close();
        }

        void form_Closing(object sender, CancelEventArgs e) {
            e.Cancel = true;
            if (this.arguments == null || this.form == null) return;
            if (form.DialogResult == DialogResult.OK) {
                ApplyExpression(form.Expression);
            } else {
                form.ResetMemoText();
            }
        }

        public void DestroyForm() {
            if (form != null) form.Dispose();
            form = null;
        }

        protected abstract void ApplyExpression(string expression);

        protected abstract ExpressionEditorForm CreateForm(params object[] arguments);
    }
    public class UnboundExpressionPanel :ExpressionPanel {
        public UnboundExpressionPanel() : base() { StartEdit(new ExpressionBuilderColumnInfo()); }

        private string _Expression;
        public string Expression {
            get { return _Expression; }
            set {
                _Expression = value;
            }
        }

        protected override ExpressionEditorForm CreateForm(params object[] arguments) {
            return new UnboundColumnExpressionEditorForm(arguments[0], null);
        }

        protected override void ApplyExpression(string expression) {
            if (Arguments == null) return;
            Expression = expression;
        }

        public override void StartEdit(params object[] arguments) {
            if (arguments.Length < 1) return;
            IDataColumnInfo columnInfo = arguments[0] as IDataColumnInfo;
            if (columnInfo == null) return;
            base.StartEdit(columnInfo);
        }
    }
}