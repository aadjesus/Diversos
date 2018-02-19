namespace DateNavigator_Demo
{
   partial class Form1
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

      #region Vom Windows Form-Designer generierter Code

      /// <summary>
      /// Erforderliche Methode für die Designerunterstützung.
      /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
         this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
         this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
         this.dockPanel2 = new DevExpress.XtraBars.Docking.DockPanel();
         this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
         this.my_scheduler = new DateNavigator_Demo.Scheduler();
         this.my_navigator = new DateNavigator_Demo.Navigator();
         ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
         this.dockPanel1.SuspendLayout();
         this.dockPanel1_Container.SuspendLayout();
         this.dockPanel2.SuspendLayout();
         this.dockPanel2_Container.SuspendLayout();
         this.SuspendLayout();
         // 
         // dockManager1
         // 
         this.dockManager1.Form = this;
         this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1,
            this.dockPanel2});
         this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
         // 
         // dockPanel1
         // 
         this.dockPanel1.Controls.Add(this.dockPanel1_Container);
         this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
         this.dockPanel1.ID = new System.Guid("581428f8-6132-4f12-8c27-d7df44a34587");
         this.dockPanel1.Location = new System.Drawing.Point(0, 0);
         this.dockPanel1.Name = "dockPanel1";
         this.dockPanel1.OriginalSize = new System.Drawing.Size(257, 200);
         this.dockPanel1.Size = new System.Drawing.Size(257, 619);
         this.dockPanel1.Text = "dockPanel1";
         // 
         // dockPanel1_Container
         // 
         this.dockPanel1_Container.Controls.Add(this.my_navigator);
         this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
         this.dockPanel1_Container.Name = "dockPanel1_Container";
         this.dockPanel1_Container.Size = new System.Drawing.Size(249, 592);
         this.dockPanel1_Container.TabIndex = 0;
         // 
         // dockPanel2
         // 
         this.dockPanel2.Controls.Add(this.dockPanel2_Container);
         this.dockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
         this.dockPanel2.ID = new System.Guid("46784646-0038-40f4-ab32-99f60d44402d");
         this.dockPanel2.Location = new System.Drawing.Point(257, 0);
         this.dockPanel2.Name = "dockPanel2";
         this.dockPanel2.OriginalSize = new System.Drawing.Size(703, 200);
         this.dockPanel2.Size = new System.Drawing.Size(703, 619);
         this.dockPanel2.Text = "dockPanel2";
         // 
         // dockPanel2_Container
         // 
         this.dockPanel2_Container.Controls.Add(this.my_scheduler);
         this.dockPanel2_Container.Location = new System.Drawing.Point(4, 23);
         this.dockPanel2_Container.Name = "dockPanel2_Container";
         this.dockPanel2_Container.Size = new System.Drawing.Size(695, 592);
         this.dockPanel2_Container.TabIndex = 0;
         // 
         // scheduler1
         // 
         this.my_scheduler.Dock = System.Windows.Forms.DockStyle.Fill;
         this.my_scheduler.Location = new System.Drawing.Point(0, 0);
         this.my_scheduler.Name = "scheduler1";
         this.my_scheduler.Size = new System.Drawing.Size(695, 592);
         this.my_scheduler.TabIndex = 0;
         // 
         // navigator1
         // 
         this.my_navigator.Dock = System.Windows.Forms.DockStyle.Fill;
         this.my_navigator.Location = new System.Drawing.Point(0, 0);
         this.my_navigator.Name = "navigator1";
         this.my_navigator.Size = new System.Drawing.Size(249, 592);
         this.my_navigator.TabIndex = 0;
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(960, 619);
         this.Controls.Add(this.dockPanel2);
         this.Controls.Add(this.dockPanel1);
         this.Name = "Form1";
         this.Text = "Form1";
         ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
         this.dockPanel1.ResumeLayout(false);
         this.dockPanel1_Container.ResumeLayout(false);
         this.dockPanel2.ResumeLayout(false);
         this.dockPanel2_Container.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private DevExpress.XtraBars.Docking.DockManager dockManager1;
      private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
      private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
      private DevExpress.XtraBars.Docking.DockPanel dockPanel2;
      private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
      private Scheduler my_scheduler;
      private Navigator my_navigator;
   }
}

