namespace SAS.Tasks.Examples.FSEdit
{
    partial class FSEditForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.bOK = new System.Windows.Forms.Button();
            this.bCommit = new System.Windows.Forms.Button();
            this.bDelete = new System.Windows.Forms.Button();
            this.editPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.totalObsLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bRight = new System.Windows.Forms.Button();
            this.bLeft = new System.Windows.Forms.Button();
            this.udObs = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.bInsert = new System.Windows.Forms.Button();
            this.insertPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.selectedVariables = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udObs)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(320, 248);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.bOK);
            this.tabPage1.Controls.Add(this.bCommit);
            this.tabPage1.Controls.Add(this.bDelete);
            this.tabPage1.Controls.Add(this.editPropertyGrid);
            this.tabPage1.Controls.Add(this.totalObsLabel);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.bRight);
            this.tabPage1.Controls.Add(this.bLeft);
            this.tabPage1.Controls.Add(this.udObs);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(312, 222);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Edit Existing Obs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.Location = new System.Drawing.Point(211, 193);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(95, 23);
            this.bOK.TabIndex = 9;
            this.bOK.Text = "&OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCommit
            // 
            this.bCommit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCommit.Location = new System.Drawing.Point(110, 193);
            this.bCommit.Name = "bCommit";
            this.bCommit.Size = new System.Drawing.Size(95, 23);
            this.bCommit.TabIndex = 8;
            this.bCommit.Text = "&Commit Changes";
            this.bCommit.UseVisualStyleBackColor = true;
            this.bCommit.Click += new System.EventHandler(this.bCommit_Click);
            // 
            // bDelete
            // 
            this.bDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bDelete.Location = new System.Drawing.Point(10, 193);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(95, 23);
            this.bDelete.TabIndex = 7;
            this.bDelete.Text = "&Delete Obs";
            this.bDelete.UseVisualStyleBackColor = true;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // editPropertyGrid
            // 
            this.editPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editPropertyGrid.HelpVisible = false;
            this.editPropertyGrid.Location = new System.Drawing.Point(10, 30);
            this.editPropertyGrid.Name = "editPropertyGrid";
            this.editPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.editPropertyGrid.Size = new System.Drawing.Size(296, 157);
            this.editPropertyGrid.TabIndex = 6;
            this.editPropertyGrid.ToolbarVisible = false;
            this.editPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.editPropertyGrid_PropertyValueChanged);
            // 
            // totalObsLabel
            // 
            this.totalObsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.totalObsLabel.AutoSize = true;
            this.totalObsLabel.Location = new System.Drawing.Point(184, 8);
            this.totalObsLabel.Name = "totalObsLabel";
            this.totalObsLabel.Size = new System.Drawing.Size(16, 13);
            this.totalObsLabel.TabIndex = 5;
            this.totalObsLabel.Text = "   ";
            this.totalObsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "of";
            // 
            // bRight
            // 
            this.bRight.Image = global::SAS.Tasks.Examples.FSEdit.Properties.Resources.ButtonRightArrow;
            this.bRight.Location = new System.Drawing.Point(136, 4);
            this.bRight.Name = "bRight";
            this.bRight.Size = new System.Drawing.Size(20, 20);
            this.bRight.TabIndex = 3;
            this.bRight.UseVisualStyleBackColor = true;
            this.bRight.Click += new System.EventHandler(this.bRight_Click);
            // 
            // bLeft
            // 
            this.bLeft.Image = global::SAS.Tasks.Examples.FSEdit.Properties.Resources.ButtonLeftArrow;
            this.bLeft.Location = new System.Drawing.Point(77, 4);
            this.bLeft.Name = "bLeft";
            this.bLeft.Size = new System.Drawing.Size(20, 20);
            this.bLeft.TabIndex = 1;
            this.bLeft.UseVisualStyleBackColor = true;
            this.bLeft.Click += new System.EventHandler(this.bLeft_Click);
            // 
            // udObs
            // 
            this.udObs.Location = new System.Drawing.Point(77, 4);
            this.udObs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udObs.Name = "udObs";
            this.udObs.Size = new System.Drawing.Size(62, 20);
            this.udObs.TabIndex = 2;
            this.udObs.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.udObs.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udObs.ValueChanged += new System.EventHandler(this.udObs_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current obs:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.bInsert);
            this.tabPage2.Controls.Add(this.insertPropertyGrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(312, 222);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Insert New Obs";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // bInsert
            // 
            this.bInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bInsert.Location = new System.Drawing.Point(220, 193);
            this.bInsert.Name = "bInsert";
            this.bInsert.Size = new System.Drawing.Size(89, 23);
            this.bInsert.TabIndex = 1;
            this.bInsert.Text = "&Insert New Obs";
            this.bInsert.UseVisualStyleBackColor = true;
            this.bInsert.Click += new System.EventHandler(this.bInsert_Click);
            // 
            // insertPropertyGrid
            // 
            this.insertPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.insertPropertyGrid.HelpVisible = false;
            this.insertPropertyGrid.Location = new System.Drawing.Point(7, 7);
            this.insertPropertyGrid.Name = "insertPropertyGrid";
            this.insertPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.insertPropertyGrid.Size = new System.Drawing.Size(302, 180);
            this.insertPropertyGrid.TabIndex = 0;
            this.insertPropertyGrid.ToolbarVisible = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.selectedVariables);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(312, 222);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Select Variables";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // selectedVariables
            // 
            this.selectedVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedVariables.CheckOnClick = true;
            this.selectedVariables.FormattingEnabled = true;
            this.selectedVariables.Location = new System.Drawing.Point(10, 24);
            this.selectedVariables.Name = "selectedVariables";
            this.selectedVariables.Size = new System.Drawing.Size(296, 184);
            this.selectedVariables.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(259, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Select or deselect variables to display in the Edit Grid.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FSEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 272);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(351, 299);
            this.Name = "FSEditForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SAS Custom Task";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udObs)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button bLeft;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bRight;
        private System.Windows.Forms.NumericUpDown udObs;
        private System.Windows.Forms.Label totalObsLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PropertyGrid editPropertyGrid;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCommit;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.Button bInsert;
        private System.Windows.Forms.PropertyGrid insertPropertyGrid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox selectedVariables;
    }
}