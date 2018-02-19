namespace DateNavigator_Demo
{
   partial class Scheduler
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
         DevExpress.XtraScheduler.TimeRuler timeRuler1 = new DevExpress.XtraScheduler.TimeRuler();
         DevExpress.XtraScheduler.TimeRuler timeRuler2 = new DevExpress.XtraScheduler.TimeRuler();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scheduler));
         this.m_schedulerControl = new DevExpress.XtraScheduler.SchedulerControl();
         this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
         this.viewNavigatorBar1 = new DevExpress.XtraScheduler.UI.ViewNavigatorBar();
         this.viewNavigatorBackwardItem1 = new DevExpress.XtraScheduler.UI.ViewNavigatorBackwardItem();
         this.viewNavigatorForwardItem1 = new DevExpress.XtraScheduler.UI.ViewNavigatorForwardItem();
         this.viewNavigatorTodayItem1 = new DevExpress.XtraScheduler.UI.ViewNavigatorTodayItem();
         this.viewSelectorBar1 = new DevExpress.XtraScheduler.UI.ViewSelectorBar();
         this.viewSelectorItem1 = new DevExpress.XtraScheduler.UI.ViewSelectorItem();
         this.viewSelectorItem2 = new DevExpress.XtraScheduler.UI.ViewSelectorItem();
         this.viewSelectorItem3 = new DevExpress.XtraScheduler.UI.ViewSelectorItem();
         this.viewSelectorItem4 = new DevExpress.XtraScheduler.UI.ViewSelectorItem();
         this.viewSelectorItem5 = new DevExpress.XtraScheduler.UI.ViewSelectorItem();
         this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
         this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
         this.schedulerStorage1 = new DevExpress.XtraScheduler.SchedulerStorage(this.components);
         this.viewNavigator1 = new DevExpress.XtraScheduler.UI.ViewNavigator(this.components);
         this.viewSelector1 = new DevExpress.XtraScheduler.UI.ViewSelector(this.components);
         this.barCheckItem_AnsichtenWechseln = new DevExpress.XtraBars.BarCheckItem();
         ((System.ComponentModel.ISupportInitialize)(this.m_schedulerControl)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.schedulerStorage1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.viewNavigator1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.viewSelector1)).BeginInit();
         this.SuspendLayout();
         // 
         // m_schedulerControl
         // 
         this.m_schedulerControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.m_schedulerControl.Location = new System.Drawing.Point(0, 62);
         this.m_schedulerControl.MenuManager = this.barManager1;
         this.m_schedulerControl.Name = "m_schedulerControl";
         this.m_schedulerControl.OptionsView.NavigationButtons.Visibility = DevExpress.XtraScheduler.NavigationButtonVisibility.Never;
         this.m_schedulerControl.OptionsView.ResourceHeaders.RotateCaption = false;
         this.m_schedulerControl.Size = new System.Drawing.Size(520, 461);
         this.m_schedulerControl.Start = new System.DateTime(2011, 2, 10, 0, 0, 0, 0);
         this.m_schedulerControl.Storage = this.schedulerStorage1;
         this.m_schedulerControl.TabIndex = 0;
         this.m_schedulerControl.Text = "schedulerControl1";
         this.m_schedulerControl.Views.DayView.TimeRulers.Add(timeRuler1);
         this.m_schedulerControl.Views.WorkWeekView.TimeRulers.Add(timeRuler2);
         // 
         // barManager1
         // 
         this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.viewNavigatorBar1,
            this.viewSelectorBar1});
         this.barManager1.DockControls.Add(this.barDockControlTop);
         this.barManager1.DockControls.Add(this.barDockControlBottom);
         this.barManager1.DockControls.Add(this.barDockControlLeft);
         this.barManager1.DockControls.Add(this.barDockControlRight);
         this.barManager1.Form = this;
         this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.viewNavigatorBackwardItem1,
            this.viewNavigatorForwardItem1,
            this.viewNavigatorTodayItem1,
            this.viewSelectorItem1,
            this.viewSelectorItem2,
            this.viewSelectorItem3,
            this.viewSelectorItem4,
            this.viewSelectorItem5,
            this.barCheckItem_AnsichtenWechseln});
         this.barManager1.MaxItemId = 16;
         // 
         // viewNavigatorBar1
         // 
         this.viewNavigatorBar1.DockCol = 0;
         this.viewNavigatorBar1.DockRow = 0;
         this.viewNavigatorBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
         this.viewNavigatorBar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.viewNavigatorBackwardItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.viewNavigatorForwardItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.viewNavigatorTodayItem1)});
         this.viewNavigatorBar1.OptionsBar.AllowQuickCustomization = false;
         this.viewNavigatorBar1.OptionsBar.DrawDragBorder = false;
         this.viewNavigatorBar1.OptionsBar.UseWholeRow = true;
         // 
         // viewNavigatorBackwardItem1
         // 
         this.viewNavigatorBackwardItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("viewNavigatorBackwardItem1.Glyph")));
         this.viewNavigatorBackwardItem1.GroupIndex = 1;
         this.viewNavigatorBackwardItem1.Id = 0;
         this.viewNavigatorBackwardItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("viewNavigatorBackwardItem1.LargeGlyph")));
         this.viewNavigatorBackwardItem1.Name = "viewNavigatorBackwardItem1";
         // 
         // viewNavigatorForwardItem1
         // 
         this.viewNavigatorForwardItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("viewNavigatorForwardItem1.Glyph")));
         this.viewNavigatorForwardItem1.GroupIndex = 1;
         this.viewNavigatorForwardItem1.Id = 1;
         this.viewNavigatorForwardItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("viewNavigatorForwardItem1.LargeGlyph")));
         this.viewNavigatorForwardItem1.Name = "viewNavigatorForwardItem1";
         // 
         // viewNavigatorTodayItem1
         // 
         this.viewNavigatorTodayItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("viewNavigatorTodayItem1.Glyph")));
         this.viewNavigatorTodayItem1.GroupIndex = 1;
         this.viewNavigatorTodayItem1.Id = 2;
         this.viewNavigatorTodayItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("viewNavigatorTodayItem1.LargeGlyph")));
         this.viewNavigatorTodayItem1.Name = "viewNavigatorTodayItem1";
         // 
         // viewSelectorBar1
         // 
         this.viewSelectorBar1.DockCol = 0;
         this.viewSelectorBar1.DockRow = 1;
         this.viewSelectorBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
         this.viewSelectorBar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.viewSelectorItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.viewSelectorItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.viewSelectorItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.viewSelectorItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.viewSelectorItem5),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItem_AnsichtenWechseln)});
         this.viewSelectorBar1.OptionsBar.AllowQuickCustomization = false;
         this.viewSelectorBar1.OptionsBar.DrawDragBorder = false;
         this.viewSelectorBar1.OptionsBar.UseWholeRow = true;
         // 
         // viewSelectorItem1
         // 
         this.viewSelectorItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("viewSelectorItem1.Glyph")));
         this.viewSelectorItem1.GroupIndex = 1;
         this.viewSelectorItem1.Id = 10;
         this.viewSelectorItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("viewSelectorItem1.LargeGlyph")));
         this.viewSelectorItem1.Name = "viewSelectorItem1";
         this.viewSelectorItem1.SchedulerViewType = DevExpress.XtraScheduler.SchedulerViewType.Day;
         // 
         // viewSelectorItem2
         // 
         this.viewSelectorItem2.Glyph = ((System.Drawing.Image)(resources.GetObject("viewSelectorItem2.Glyph")));
         this.viewSelectorItem2.GroupIndex = 1;
         this.viewSelectorItem2.Id = 11;
         this.viewSelectorItem2.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("viewSelectorItem2.LargeGlyph")));
         this.viewSelectorItem2.Name = "viewSelectorItem2";
         this.viewSelectorItem2.SchedulerViewType = DevExpress.XtraScheduler.SchedulerViewType.WorkWeek;
         // 
         // viewSelectorItem3
         // 
         this.viewSelectorItem3.Glyph = ((System.Drawing.Image)(resources.GetObject("viewSelectorItem3.Glyph")));
         this.viewSelectorItem3.GroupIndex = 1;
         this.viewSelectorItem3.Id = 12;
         this.viewSelectorItem3.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("viewSelectorItem3.LargeGlyph")));
         this.viewSelectorItem3.Name = "viewSelectorItem3";
         this.viewSelectorItem3.SchedulerViewType = DevExpress.XtraScheduler.SchedulerViewType.Week;
         // 
         // viewSelectorItem4
         // 
         this.viewSelectorItem4.Glyph = ((System.Drawing.Image)(resources.GetObject("viewSelectorItem4.Glyph")));
         this.viewSelectorItem4.GroupIndex = 1;
         this.viewSelectorItem4.Id = 13;
         this.viewSelectorItem4.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("viewSelectorItem4.LargeGlyph")));
         this.viewSelectorItem4.Name = "viewSelectorItem4";
         this.viewSelectorItem4.SchedulerViewType = DevExpress.XtraScheduler.SchedulerViewType.Month;
         // 
         // viewSelectorItem5
         // 
         this.viewSelectorItem5.Glyph = ((System.Drawing.Image)(resources.GetObject("viewSelectorItem5.Glyph")));
         this.viewSelectorItem5.GroupIndex = 1;
         this.viewSelectorItem5.Id = 14;
         this.viewSelectorItem5.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("viewSelectorItem5.LargeGlyph")));
         this.viewSelectorItem5.Name = "viewSelectorItem5";
         this.viewSelectorItem5.SchedulerViewType = DevExpress.XtraScheduler.SchedulerViewType.Timeline;
         // 
         // barDockControlTop
         // 
         this.barDockControlTop.CausesValidation = false;
         this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
         this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
         this.barDockControlTop.Size = new System.Drawing.Size(520, 62);
         // 
         // barDockControlBottom
         // 
         this.barDockControlBottom.CausesValidation = false;
         this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.barDockControlBottom.Location = new System.Drawing.Point(0, 523);
         this.barDockControlBottom.Size = new System.Drawing.Size(520, 0);
         // 
         // barDockControlLeft
         // 
         this.barDockControlLeft.CausesValidation = false;
         this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
         this.barDockControlLeft.Location = new System.Drawing.Point(0, 62);
         this.barDockControlLeft.Size = new System.Drawing.Size(0, 461);
         // 
         // barDockControlRight
         // 
         this.barDockControlRight.CausesValidation = false;
         this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
         this.barDockControlRight.Location = new System.Drawing.Point(520, 62);
         this.barDockControlRight.Size = new System.Drawing.Size(0, 461);
         // 
         // viewNavigator1
         // 
         this.viewNavigator1.BarManager = this.barManager1;
         this.viewNavigator1.SchedulerControl = this.m_schedulerControl;
         // 
         // viewSelector1
         // 
         this.viewSelector1.BarManager = this.barManager1;
         this.viewSelector1.SchedulerControl = this.m_schedulerControl;
         // 
         // barCheckItem_AnsichtenWechseln
         // 
         this.barCheckItem_AnsichtenWechseln.Caption = "Autoswitch Views";
         this.barCheckItem_AnsichtenWechseln.Id = 15;
         this.barCheckItem_AnsichtenWechseln.Name = "barCheckItem_AnsichtenWechseln";
         // 
         // Scheduler
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.m_schedulerControl);
         this.Controls.Add(this.barDockControlLeft);
         this.Controls.Add(this.barDockControlRight);
         this.Controls.Add(this.barDockControlBottom);
         this.Controls.Add(this.barDockControlTop);
         this.Name = "Scheduler";
         this.Size = new System.Drawing.Size(520, 523);
         ((System.ComponentModel.ISupportInitialize)(this.m_schedulerControl)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.schedulerStorage1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.viewNavigator1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.viewSelector1)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private DevExpress.XtraScheduler.SchedulerControl m_schedulerControl;
      private DevExpress.XtraScheduler.SchedulerStorage schedulerStorage1;
      private DevExpress.XtraBars.BarManager barManager1;
      private DevExpress.XtraScheduler.UI.ViewNavigatorBar viewNavigatorBar1;
      private DevExpress.XtraScheduler.UI.ViewNavigatorBackwardItem viewNavigatorBackwardItem1;
      private DevExpress.XtraScheduler.UI.ViewNavigatorForwardItem viewNavigatorForwardItem1;
      private DevExpress.XtraScheduler.UI.ViewNavigatorTodayItem viewNavigatorTodayItem1;
      private DevExpress.XtraBars.BarDockControl barDockControlTop;
      private DevExpress.XtraBars.BarDockControl barDockControlBottom;
      private DevExpress.XtraBars.BarDockControl barDockControlLeft;
      private DevExpress.XtraBars.BarDockControl barDockControlRight;
      private DevExpress.XtraScheduler.UI.ViewNavigator viewNavigator1;
      private DevExpress.XtraScheduler.UI.ViewSelectorBar viewSelectorBar1;
      private DevExpress.XtraScheduler.UI.ViewSelectorItem viewSelectorItem1;
      private DevExpress.XtraScheduler.UI.ViewSelectorItem viewSelectorItem2;
      private DevExpress.XtraScheduler.UI.ViewSelectorItem viewSelectorItem3;
      private DevExpress.XtraScheduler.UI.ViewSelectorItem viewSelectorItem4;
      private DevExpress.XtraScheduler.UI.ViewSelectorItem viewSelectorItem5;
      private DevExpress.XtraScheduler.UI.ViewSelector viewSelector1;
      private DevExpress.XtraBars.BarCheckItem barCheckItem_AnsichtenWechseln;
   }
}
