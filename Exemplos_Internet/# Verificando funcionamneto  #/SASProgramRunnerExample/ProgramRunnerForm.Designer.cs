namespace SAS.Tasks.Examples.SASProgramRunner
{
    partial class ProgramRunnerForm
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
            this.btnClose = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.ctlEditor = new SAS.Tasks.Toolkit.Controls.SASTextEditorCtl();
            this.ctlLogViewer = new SAS.Tasks.Toolkit.Controls.SASTextEditorCtl();
            this.button1 = new System.Windows.Forms.Button();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(497, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.ctlEditor);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.ctlLogViewer);
            this.splitContainer.Size = new System.Drawing.Size(580, 398);
            this.splitContainer.SplitterDistance = 270;
            this.splitContainer.TabIndex = 2;
            // 
            // sasTextEditorCtl1
            // 
            this.ctlEditor.BackColor = System.Drawing.SystemColors.Window;
            this.ctlEditor.ContentType = SAS.Tasks.Toolkit.Controls.SASTextEditorCtl.eContentType.SASProgram;
            this.ctlEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlEditor.EditorText = "";
            this.ctlEditor.Location = new System.Drawing.Point(0, 0);
            this.ctlEditor.Name = "sasTextEditorCtl1";
            this.ctlEditor.ReadOnly = false;
            this.ctlEditor.Size = new System.Drawing.Size(270, 398);
            this.ctlEditor.TabIndex = 0;
            // 
            // sasTextEditorCtl2
            // 
            this.ctlLogViewer.BackColor = System.Drawing.SystemColors.Window;
            this.ctlLogViewer.ContentType = SAS.Tasks.Toolkit.Controls.SASTextEditorCtl.eContentType.SASLog;
            this.ctlLogViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlLogViewer.EditorText = "";
            this.ctlLogViewer.Location = new System.Drawing.Point(0, 0);
            this.ctlLogViewer.Name = "sasTextEditorCtl2";
            this.ctlLogViewer.ReadOnly = false;
            this.ctlLogViewer.Size = new System.Drawing.Size(306, 398);
            this.ctlLogViewer.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(389, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Run program";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.button1);
            this.panelButtons.Controls.Add(this.btnClose);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 366);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(580, 32);
            this.panelButtons.TabIndex = 4;
            // 
            // ProgramRunnerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 398);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.splitContainer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgramRunnerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SAS Program Runner";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.SplitContainer splitContainer;
        private SAS.Tasks.Toolkit.Controls.SASTextEditorCtl ctlEditor;
        private SAS.Tasks.Toolkit.Controls.SASTextEditorCtl ctlLogViewer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelButtons;
    }
}