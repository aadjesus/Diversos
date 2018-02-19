namespace DateNavigator_Demo
{
   partial class Navigator
   {
      /// <summary> 
      /// Erforderliche Designervariable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Verwendete Ressourcen bereinigen.
      /// </summary>
      /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Vom Komponenten-Designer generierter Code

      /// <summary> 
      /// Erforderliche Methode für die Designerunterstützung. 
      /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
         this.bar1 = new DevExpress.XtraBars.Bar();
         this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
         this.m_dateNavigator = new DateNavigator_Demo.MyDateNavigator();
         this.barCheckItem_KalenderSync = new DevExpress.XtraBars.BarCheckItem();
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_dateNavigator)).BeginInit();
         this.SuspendLayout();
         // 
         // barManager1
         // 
         this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
         this.barManager1.DockControls.Add(this.barDockControlTop);
         this.barManager1.DockControls.Add(this.barDockControlBottom);
         this.barManager1.DockControls.Add(this.barDockControlLeft);
         this.barManager1.DockControls.Add(this.barDockControlRight);
         this.barManager1.Form = this;
         this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barCheckItem_KalenderSync});
         this.barManager1.MaxItemId = 1;
         // 
         // bar1
         // 
         this.bar1.BarName = "Tools";
         this.bar1.DockCol = 0;
         this.bar1.DockRow = 0;
         this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
         this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItem_KalenderSync)});
         this.bar1.OptionsBar.AllowQuickCustomization = false;
         this.bar1.OptionsBar.DrawDragBorder = false;
         this.bar1.OptionsBar.UseWholeRow = true;
         this.bar1.Text = "Tools";
         // 
         // barDockControlTop
         // 
         this.barDockControlTop.CausesValidation = false;
         this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
         this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
         this.barDockControlTop.Size = new System.Drawing.Size(240, 29);
         // 
         // barDockControlBottom
         // 
         this.barDockControlBottom.CausesValidation = false;
         this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.barDockControlBottom.Location = new System.Drawing.Point(0, 437);
         this.barDockControlBottom.Size = new System.Drawing.Size(240, 0);
         // 
         // barDockControlLeft
         // 
         this.barDockControlLeft.CausesValidation = false;
         this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
         this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
         this.barDockControlLeft.Size = new System.Drawing.Size(0, 408);
         // 
         // barDockControlRight
         // 
         this.barDockControlRight.CausesValidation = false;
         this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
         this.barDockControlRight.Location = new System.Drawing.Point(240, 29);
         this.barDockControlRight.Size = new System.Drawing.Size(0, 408);
         // 
         // m_dateNavigator
         // 
         this.m_dateNavigator.DateTime = new System.DateTime(2011, 2, 10, 0, 0, 0, 0);
         this.m_dateNavigator.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_dateNavigator.HotDate = null;
         this.m_dateNavigator.Location = new System.Drawing.Point(0, 29);
         this.m_dateNavigator.Name = "m_dateNavigator";
         this.m_dateNavigator.Size = new System.Drawing.Size(240, 408);
         this.m_dateNavigator.TabIndex = 4;
         // 
         // barCheckItem_KalenderSync
         // 
         this.barCheckItem_KalenderSync.Caption = "ReSync with Scheduler";
         this.barCheckItem_KalenderSync.Id = 0;
         this.barCheckItem_KalenderSync.Name = "barCheckItem_KalenderSync";
         // 
         // Navigator
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.m_dateNavigator);
         this.Controls.Add(this.barDockControlLeft);
         this.Controls.Add(this.barDockControlRight);
         this.Controls.Add(this.barDockControlBottom);
         this.Controls.Add(this.barDockControlTop);
         this.Name = "Navigator";
         this.Size = new System.Drawing.Size(240, 437);
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.m_dateNavigator)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private DevExpress.XtraBars.BarManager barManager1;
      private DevExpress.XtraBars.Bar bar1;
      private DevExpress.XtraBars.BarDockControl barDockControlTop;
      private DevExpress.XtraBars.BarDockControl barDockControlBottom;
      private DevExpress.XtraBars.BarDockControl barDockControlLeft;
      private DevExpress.XtraBars.BarDockControl barDockControlRight;
      private MyDateNavigator m_dateNavigator;
      private DevExpress.XtraBars.BarCheckItem barCheckItem_KalenderSync;

   }

   public class MyDateNavigator : DevExpress.XtraScheduler.DateNavigator
   {
      /// <summary>
      /// Ableitung des DevExpress DateNavigator
      /// </summary>
      public MyDateNavigator() : base() { }

      /// <summary>
      /// Übrschriebene Methode zum neu Zeichnen des Controls
      /// </summary>
      public void RefreshLayout()
      {
         base.LayoutChanged();
      }
   }
}
