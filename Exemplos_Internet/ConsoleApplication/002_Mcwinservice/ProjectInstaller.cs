namespace mcWebService
{
    using System;
    using System.Collections;
    //using System.Core;
    using System.ComponentModel;
    using System.Configuration.Install;

    /// <summary>
    ///     Summary description for ProjectInstaller.
    /// </summary>
    [RunInstaller(true)]
    public class ProjectInstaller : System.Configuration.Install.Installer
    {
        /// <summary>
        ///    Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components;
		private System.ServiceProcess.ServiceInstaller serviceInstaller1;
		private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;

        public ProjectInstaller()
        {
            // This call is required by the Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitComponent call
        }

        /// <summary>
        ///    Required method for Designer support - do not modify
        ///    the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container ();
			this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller ();
			this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller ();
			//@this.TrayLargeIcon = false;
			//@this.TrayAutoArrange = true;

            serviceInstaller1.DisplayName = "My xisto";
            serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;            

			serviceInstaller1.ServiceName = "mcWinService";
			//@serviceProcessInstaller1.SetLocation (new System.Drawing.Point (7, 7));
			serviceProcessInstaller1.Password = null;
			serviceProcessInstaller1.Username = null;
			this.Installers.Add (this.serviceProcessInstaller1);
			this.Installers.Add (this.serviceInstaller1);
		}
    }
}
