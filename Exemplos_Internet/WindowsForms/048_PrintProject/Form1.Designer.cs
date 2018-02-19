namespace PrintProject
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
            this.components = new System.ComponentModel.Container();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageTree = new DevExpress.XtraTab.XtraTabPage();
            this.printableTreeUserControl = new PrintProject.PrintableTreeUserControl();
            this.xtraTabPageScheduler = new DevExpress.XtraTab.XtraTabPage();
            this.printableSchedulerUserControl = new PrintProject.PrintableSchedulerUserControl();
            this.xtraTabPageGrid = new DevExpress.XtraTab.XtraTabPage();
            this.printableGridUserControl = new PrintProject.PrintableGridUserControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barButtonItemPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPageTree.SuspendLayout();
            this.xtraTabPageScheduler.SuspendLayout();
            this.xtraTabPageGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 24);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPageTree;
            this.xtraTabControl1.Size = new System.Drawing.Size(685, 394);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageTree,
            this.xtraTabPageScheduler,
            this.xtraTabPageGrid});
            // 
            // xtraTabPageTree
            // 
            this.xtraTabPageTree.Controls.Add(this.printableTreeUserControl);
            this.xtraTabPageTree.Name = "xtraTabPageTree";
            this.xtraTabPageTree.Size = new System.Drawing.Size(678, 365);
            this.xtraTabPageTree.Text = "Tree";
            // 
            // printableTreeUserControl
            // 
            this.printableTreeUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printableTreeUserControl.Location = new System.Drawing.Point(0, 0);
            this.printableTreeUserControl.Name = "printableTreeUserControl";
            this.printableTreeUserControl.Size = new System.Drawing.Size(678, 365);
            this.printableTreeUserControl.TabIndex = 0;
            // 
            // xtraTabPageScheduler
            // 
            this.xtraTabPageScheduler.Controls.Add(this.printableSchedulerUserControl);
            this.xtraTabPageScheduler.Name = "xtraTabPageScheduler";
            this.xtraTabPageScheduler.Size = new System.Drawing.Size(678, 365);
            this.xtraTabPageScheduler.Text = "Scheduler";
            // 
            // printableSchedulerUserControl
            // 
            this.printableSchedulerUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printableSchedulerUserControl.Location = new System.Drawing.Point(0, 0);
            this.printableSchedulerUserControl.Name = "printableSchedulerUserControl";
            this.printableSchedulerUserControl.Size = new System.Drawing.Size(678, 365);
            this.printableSchedulerUserControl.TabIndex = 0;
            // 
            // xtraTabPageGrid
            // 
            this.xtraTabPageGrid.Controls.Add(this.printableGridUserControl);
            this.xtraTabPageGrid.Name = "xtraTabPageGrid";
            this.xtraTabPageGrid.Size = new System.Drawing.Size(678, 365);
            this.xtraTabPageGrid.Text = "Grid";
            // 
            // printableGridUserControl
            // 
            this.printableGridUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printableGridUserControl.Location = new System.Drawing.Point(0, 0);
            this.printableGridUserControl.Name = "printableGridUserControl";
            this.printableGridUserControl.Size = new System.Drawing.Size(678, 365);
            this.printableGridUserControl.TabIndex = 0;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemPrint});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 1;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemPrint)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barButtonItemPrint
            // 
            this.barButtonItemPrint.Caption = "Print";
            this.barButtonItemPrint.Id = 0;
            this.barButtonItemPrint.Name = "barButtonItemPrint";
            this.barButtonItemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemPrint_ItemClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 418);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPageTree.ResumeLayout(false);
            this.xtraTabPageScheduler.ResumeLayout(false);
            this.xtraTabPageGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageTree;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageScheduler;
        private PrintableTreeUserControl printableTreeUserControl;
        private PrintableSchedulerUserControl printableSchedulerUserControl;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barButtonItemPrint;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageGrid;
        private PrintableGridUserControl printableGridUserControl;
    }
}

