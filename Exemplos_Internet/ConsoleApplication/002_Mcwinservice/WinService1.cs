namespace mcWebService
{
    using System;
    using System.Collections;
    //using System.Core;
    using System.ComponentModel;
    using System.Configuration;
    using System.Data;
    using System.Web.Services;
    using System.Diagnostics;
    using System.ServiceProcess;
	using System.IO; 


    public class WinService1 : System.ServiceProcess.ServiceBase
    {
        /// <summary> 
        ///    Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components;

        public WinService1()
        {
            // This call is required by the WinForms Component Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitComponent call
        }

        // The main entry point for the process
        static void Main()
        {
            System.ServiceProcess.ServiceBase[] ServicesToRun;
    
            // More than one user Service may run within the same process. To add
            // another service to this process, change the following line to
            // create a second service object. For example,
            //
            //   ServicesToRun = New System.ServiceProcess.ServiceBase[] {new WinService1(), new MySecondUserService()};
            //
            ServicesToRun = new System.ServiceProcess.ServiceBase[] { new WinService1() };

            System.ServiceProcess.ServiceBase.Run(ServicesToRun);
        }

        /// <summary> 
        ///    Required method for Designer support - do not modify 
        ///    the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container ();
			//@this.TrayAutoArrange = true;
			//@this.TrayLargeIcon = false;
			this.ServiceName = "mcWinService";
		}

        /// <summary>
        ///    Set things in motion so your service can do its work.
        /// </summary>
        protected override void OnStart(string[] args)
        {
            // Create a text file C:\temp\mcb.txt 
			FileStream fs = new FileStream(@"c:\temp\mcWindowsService.txt" , FileMode.OpenOrCreate, FileAccess.Write); 
			StreamWriter m_streamWriter = new StreamWriter(fs); 
			m_streamWriter.BaseStream.Seek(0, SeekOrigin.End); 
			m_streamWriter.WriteLine(" mcWindowsService: Service Started \n"); 
			m_streamWriter.Flush(); 


        }
 
        /// <summary>
        ///    Stop this service.
        /// </summary>
        protected override void OnStop()
        {
            // Create a text file C:\temp\mcb.txt 
			FileStream fs = new FileStream(@"c:\temp\mcWindowsService.txt" , FileMode.OpenOrCreate, FileAccess.Write); 
			StreamWriter m_streamWriter = new StreamWriter(fs); 
			m_streamWriter.BaseStream.Seek(0, SeekOrigin.End); 
			m_streamWriter.WriteLine(" mcWindowsService: Service Stopped \n"); 
			m_streamWriter.Flush(); 
        }
    }
}