namespace PivotGrid
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
            this.pgvObjects = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.pivotGridField1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.cboSummaryType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cboField = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.customFormulaList = new DevExpress.XtraEditors.ImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pgvObjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSummaryType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customFormulaList.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pgvObjects
            // 
            this.pgvObjects.Cursor = System.Windows.Forms.Cursors.Default;
            this.pgvObjects.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pivotGridField1});
            this.pgvObjects.Location = new System.Drawing.Point(12, 65);
            this.pgvObjects.Name = "pgvObjects";
            this.pgvObjects.Size = new System.Drawing.Size(496, 257);
            this.pgvObjects.TabIndex = 0;
            this.pgvObjects.CustomSummary += new DevExpress.XtraPivotGrid.PivotGridCustomSummaryEventHandler(this.pgvObjects_CustomSummary);
            // 
            // pivotGridField1
            // 
            this.pivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField1.AreaIndex = 0;
            this.pivotGridField1.Name = "pivotGridField1";
            // 
            // cboSummaryType
            // 
            this.cboSummaryType.Location = new System.Drawing.Point(99, 39);
            this.cboSummaryType.Name = "cboSummaryType";
            this.cboSummaryType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.cboSummaryType.Properties.Appearance.Options.UseBackColor = true;
            this.cboSummaryType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSummaryType.Size = new System.Drawing.Size(100, 20);
            this.cboSummaryType.TabIndex = 7;
            this.cboSummaryType.SelectedIndexChanged += new System.EventHandler(this.cboSummaryType_SelectedIndexChanged);
            // 
            // cboField
            // 
            this.cboField.Location = new System.Drawing.Point(62, 12);
            this.cboField.Name = "cboField";
            this.cboField.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.cboField.Properties.Appearance.Options.UseBackColor = true;
            this.cboField.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboField.Size = new System.Drawing.Size(137, 20);
            this.cboField.TabIndex = 6;
            this.cboField.SelectedIndexChanged += new System.EventHandler(this.cboField_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Summary Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Field";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(205, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(81, 13);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "Custom Formula:";
            // 
            // customFormulaList
            // 
            this.customFormulaList.Location = new System.Drawing.Point(292, 40);
            this.customFormulaList.Name = "customFormulaList";
            this.customFormulaList.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.customFormulaList.Properties.Appearance.Options.UseBackColor = true;
            this.customFormulaList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)});
            this.customFormulaList.Size = new System.Drawing.Size(216, 20);
            this.customFormulaList.TabIndex = 10;
            this.customFormulaList.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.OnCustomFormulaListAddButtonClick);
            this.customFormulaList.SelectedIndexChanged += new System.EventHandler(this.OnCustomFormulaListSelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 334);
            this.Controls.Add(this.customFormulaList);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cboSummaryType);
            this.Controls.Add(this.cboField);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pgvObjects);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pgvObjects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSummaryType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customFormulaList.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraPivotGrid.PivotGridControl pgvObjects;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField1;
        private DevExpress.XtraEditors.ImageComboBoxEdit cboSummaryType;
        private DevExpress.XtraEditors.ImageComboBoxEdit cboField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ImageComboBoxEdit customFormulaList;
    }
}

