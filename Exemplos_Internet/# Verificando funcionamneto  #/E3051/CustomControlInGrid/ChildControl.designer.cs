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

namespace CustomControlInGrid
{
    partial class ChildControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.calcEdit1 = new DevExpress.XtraEditors.CalcEdit();
			((System.ComponentModel.ISupportInitialize)(this.calcEdit1.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// calcEdit1
			// 
			this.calcEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.calcEdit1.Location = new System.Drawing.Point(0, 0);
			this.calcEdit1.Name = "calcEdit1";
			this.calcEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.calcEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.calcEdit1.Properties.DisplayFormat.FormatString = "{0:n2} Kg";
			this.calcEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.calcEdit1.Size = new System.Drawing.Size(181, 18);
			this.calcEdit1.TabIndex = 0;
			// 
			// ChildControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.calcEdit1);
			this.Name = "ChildControl";
			this.Size = new System.Drawing.Size(181, 22);
			((System.ComponentModel.ISupportInitialize)(this.calcEdit1.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraEditors.CalcEdit calcEdit1;


	}
}
