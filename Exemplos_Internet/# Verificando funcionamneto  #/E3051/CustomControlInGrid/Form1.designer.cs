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
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.TestCol = new DevExpress.XtraGrid.Columns.GridColumn();
			this.customRepositoryItem1 = new CustomControlInGrid.CustomRepositoryItem();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.customRepositoryItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControl1
			// 
			this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			gridLevelNode1.RelationName = "Level1";
			this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
			this.gridControl1.Location = new System.Drawing.Point(0, 0);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.customRepositoryItem1,
            this.repositoryItemCalcEdit1});
			this.gridControl1.Size = new System.Drawing.Size(1105, 590);
			this.gridControl1.TabIndex = 6;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
			// 
			// gridView1
			// 
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.TestCol,
            this.gridColumn1});
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
			this.gridView1.OptionsView.RowAutoHeight = true;
			// 
			// TestCol
			// 
			this.TestCol.Caption = "EmptyColumn";
			this.TestCol.ColumnEdit = this.customRepositoryItem1;
			this.TestCol.FieldName = "TestCol";
			this.TestCol.Name = "TestCol";
			this.TestCol.Visible = true;
			this.TestCol.VisibleIndex = 0;
			// 
			// customRepositoryItem1
			// 
			this.customRepositoryItem1.AutoHeight = false;
			this.customRepositoryItem1.Name = "customRepositoryItem1";
			// 
			// gridColumn1
			// 
			this.gridColumn1.Caption = "gridColumn1";
			this.gridColumn1.ColumnEdit = this.repositoryItemCalcEdit1;
			this.gridColumn1.FieldName = "TestCol2";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 1;
			// 
			// repositoryItemCalcEdit1
			// 
			this.repositoryItemCalcEdit1.AutoHeight = false;
			this.repositoryItemCalcEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemCalcEdit1.DisplayFormat.FormatString = "{0:n2} Kg";
			this.repositoryItemCalcEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1105, 590);
			this.Controls.Add(this.gridControl1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.customRepositoryItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraGrid.GridControl gridControl1;
		private CustomRepositoryItem customRepositoryItem1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraGrid.Columns.GridColumn TestCol;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit1;
       
    }
}

