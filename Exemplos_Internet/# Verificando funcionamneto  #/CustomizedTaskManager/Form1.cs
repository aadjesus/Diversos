using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Threading;

namespace CustomizedTaskManager
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmmain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.NotifyIcon taskmgrnotify;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem8;
       	private System.Windows.Forms.StatusBarPanel processcount;
		private System.Windows.Forms.StatusBarPanel threadcount;
		private System.Windows.Forms.ContextMenu lvcxtmnu;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.ListView lvprocesslist;
		private System.Windows.Forms.ColumnHeader procname;
		private System.Windows.Forms.ColumnHeader PID;
		private System.Windows.Forms.ColumnHeader procstarttime;
		private System.Windows.Forms.ColumnHeader proccputime;
		private System.Windows.Forms.ColumnHeader memusage;
		private System.Windows.Forms.ColumnHeader peakmemusage;
		private System.Windows.Forms.ColumnHeader noofhandles;
		private System.Windows.Forms.ColumnHeader nonofthreads;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Diagnostics.PerformanceCounter pcclrmemmngr;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.Label lblheapbyte;
		private System.Windows.Forms.Label gen0heapsize;
		private System.Windows.Forms.Label Label2;
		private System.Windows.Forms.Label gen1heapsize;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label gen2heapsize;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lblgctime;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lbltotalreservedbytes;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lbltotheapsize;
		#region User-defined variables
		public static string newprocpathandparm,mcname;
		public static frmmain objtaskmgr;
		public static frmnewprcdetails objnewprocess;
		public System.Threading.Timer t =null;
		public System.Threading.Timer tclr =null;
		public bool erroccured = false;
		private System.Windows.Forms.MenuItem menuItem17;
		public Hashtable presentprocdetails;
		public Process[] processes = null;
		#endregion
		#region User-Defined Methods
		private void LoadAllProcessesOnStartup()
		{
			Process[] processes = null;
			try
			{
				processes = Process.GetProcesses(mcname);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				Application.Exit();
				return;
			}
			int threadscount = 0;
			foreach(Process p in processes)
			{
				try
				{
					string[] prcdetails = new string[]{p.ProcessName,p.Id.ToString(),p.StartTime.ToShortTimeString(),p.TotalProcessorTime.Duration().Hours.ToString()+":"+p.TotalProcessorTime.Duration().Minutes.ToString()+":"+p.TotalProcessorTime.Duration().Seconds.ToString(),(p.WorkingSet/1024).ToString()+"k",(p.PeakWorkingSet/1024).ToString()+"k",p.HandleCount.ToString(),p.Threads.Count.ToString()};
					ListViewItem proc = new ListViewItem(prcdetails);
					lvprocesslist.Items.Add(proc);
					threadscount += p.Threads.Count;
				}
				catch{}
			}
			statusBar1.Panels[0].Text  = "Processes : "+processes.Length.ToString();
			statusBar1.Panels[1].Text  = "Threads : "+(threadscount+1).ToString();
		}
		private void LoadAllProcesses(object temp)
		{
			try
			{
				presentprocdetails.Clear();
				processes = Process.GetProcesses(mcname);
				bool runningproccountchanged= false;
				Hashtable lvprocesses = null;
				int threadscount = 0;
				foreach(Process p in processes)
				{
					try
					{
						string[] prcdetails = new string[]{p.ProcessName,p.Id.ToString(),p.StartTime.ToShortTimeString(),p.TotalProcessorTime.Duration().Hours.ToString()+":"+p.TotalProcessorTime.Duration().Minutes.ToString()+":"+p.TotalProcessorTime.Duration().Seconds.ToString(),(p.WorkingSet/1024).ToString()+"k",(p.PeakWorkingSet/1024).ToString()+"k",p.HandleCount.ToString(),p.Threads.Count.ToString()};
						presentprocdetails.Add(prcdetails[1],prcdetails[0].ToString()+"#"+prcdetails[2].ToString()+"#"+prcdetails[3].ToString()+"#"+prcdetails[4].ToString()+"#"+prcdetails[5].ToString()+"#"+prcdetails[6].ToString()+"#"+prcdetails[7].ToString());
						threadscount += p.Threads.Count;
					}
					catch{}
				}
				if((processes.Length != lvprocesslist.Items.Count) || erroccured)
				{
					runningproccountchanged = true;
					lvprocesses = new Hashtable();
					foreach(ListViewItem item in lvprocesslist.Items)
					{
						lvprocesses.Add(item.SubItems[1].Text,"");
					}
				}
				if(runningproccountchanged || erroccured)
				{
					erroccured = false;
					foreach(Process p in Process.GetProcesses(mcname))
					{
						try
						{
							if(!lvprocesses.Contains(p.Id.ToString()))
							{
								string[] newprcdetails = new string[]{p.ProcessName,p.Id.ToString(),p.StartTime.ToShortTimeString(),p.TotalProcessorTime.Duration().Hours.ToString()+":"+p.TotalProcessorTime.Duration().Minutes.ToString()+":"+p.TotalProcessorTime.Duration().Seconds.ToString(),(p.WorkingSet/1024).ToString()+"k",(p.PeakWorkingSet/1024).ToString()+"k",p.HandleCount.ToString(),p.Threads.Count.ToString()};
								ListViewItem newprocess = new ListViewItem(newprcdetails);
								lvprocesslist.Items.Add(newprocess);
							}
							IDictionaryEnumerator enlvprocesses = lvprocesses.GetEnumerator();
							while(enlvprocesses.MoveNext())
							{
								if(!presentprocdetails.Contains(enlvprocesses.Key))
								{
									foreach(ListViewItem item in lvprocesslist.Items)
									{
										if(item.SubItems[1].Text.ToString().ToUpper() == enlvprocesses.Key.ToString().ToUpper())
										{
											lvprocesslist.Items.Remove(item);
										}
									}
								}
							}
						}
						catch{}
					}
				}
				IDictionaryEnumerator enpresentprodetails = presentprocdetails.GetEnumerator();
				bool valchanged = false;
				while (enpresentprodetails.MoveNext())
				{
					foreach(ListViewItem item in lvprocesslist.Items)
					{
						if(item.SubItems[1].Text.ToString().ToUpper() == enpresentprodetails.Key.ToString().ToUpper())
						{
							string[] presentprocessdetails = enpresentprodetails.Value.ToString().Split('#');
							if(item.SubItems[3].Text.ToString() != presentprocessdetails[2].ToString())
							{
								valchanged = true;
								item.SubItems[3].Text = presentprocessdetails[2].ToString();
							}
							if(item.SubItems[4].Text.ToString() != presentprocessdetails[3].ToString())
							{
								valchanged = true;
								item.SubItems[4].Text = presentprocessdetails[3].ToString();
							}
							if(item.SubItems[5].Text.ToString() != presentprocessdetails[4].ToString())
							{
								valchanged = true;
								item.SubItems[5].Text = presentprocessdetails[4].ToString();
							}
							if(item.SubItems[6].Text.ToString() != presentprocessdetails[5].ToString())
							{
								valchanged = true;
								item.SubItems[6].Text = presentprocessdetails[5].ToString();
							}
							if(item.SubItems[7].Text.ToString() != presentprocessdetails[6].ToString())
							{
								valchanged = true;
								item.SubItems[7].Text = presentprocessdetails[6].ToString();
							}
							if(menuItem17.Checked)
							{
								valchanged = false;
							}
							if(valchanged)
							{
								item.ForeColor = Color.Red;
								valchanged = false;
							}
							else
							{
								item.ForeColor = Color.Black;
							}
							break;
						}
					}
				}
				statusBar1.Panels[0].Text  = "Processes : "+processes.Length.ToString();
				statusBar1.Panels[1].Text  = "Threads : "+(threadscount+1).ToString();
			}
			catch{}
		}
		private void SetProcessPriority(MenuItem item)
		{
			try
			{
				int selectedpid = Convert.ToInt32(lvprocesslist.SelectedItems[0].SubItems[1].Text.ToString());
				Process selectedprocess = Process.GetProcessById(selectedpid,mcname);
				if(item.Text.ToUpper() == "HIGH")
					selectedprocess.PriorityClass = ProcessPriorityClass.High;
				else if(item.Text.ToUpper() == "LOW")
					selectedprocess.PriorityClass = ProcessPriorityClass.Idle;
				else if(item.Text.ToUpper() == "REAL-TIME")
					selectedprocess.PriorityClass = ProcessPriorityClass.RealTime;
				else if(item.Text.ToUpper() == "ABOVE NORMAL")
					selectedprocess.PriorityClass = ProcessPriorityClass.AboveNormal;
				else if(item.Text.ToUpper() == "BELOW NORMAL")
					selectedprocess.PriorityClass = ProcessPriorityClass.BelowNormal;
				else if(item.Text.ToUpper() == "NORMAL")
					selectedprocess.PriorityClass = ProcessPriorityClass.Normal;
				foreach(MenuItem mnuitem in menuItem10.MenuItems)
				{
					mnuitem.Checked = false;
				}
				item.Checked = true;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private  void LoadAllMemoryDetails(object temp)
		{
			try
			{
				float totheapsize,fgen0heapsize,fgen1heapsize,fgen2heapsize;
				pcclrmemmngr.MachineName = mcname;
				pcclrmemmngr.InstanceName = "aspnet_wp";//System.Reflection.Assembly.GetExecutingAssembly().GetName().ToString().Substring(0, 14);
				pcclrmemmngr.CategoryName = ".NET CLR Memory";
				pcclrmemmngr.CounterName = "# Bytes in all Heaps";
				totheapsize = pcclrmemmngr.NextValue();
				lbltotheapsize.Text = totheapsize+" Bytes";
				pcclrmemmngr.CounterName = "Gen 0 heap size";
				fgen0heapsize = pcclrmemmngr.NextValue();
				gen0heapsize.Text   = fgen0heapsize.ToString()+" Bytes"+" ("+((fgen0heapsize/totheapsize)*100)+")"+"%";
				pcclrmemmngr.CounterName = "Gen 1 heap size";
				fgen1heapsize = pcclrmemmngr.NextValue();
				gen1heapsize.Text   = fgen1heapsize.ToString()+" Bytes"+" ("+((fgen1heapsize/totheapsize)*100)+")"+"%";
				pcclrmemmngr.CounterName = "Gen 2 heap size";
				fgen2heapsize = pcclrmemmngr.NextValue();
				gen2heapsize.Text   = fgen2heapsize.ToString()+" Bytes"+" ("+((fgen2heapsize/totheapsize)*100)+")"+"%";
				pcclrmemmngr.CounterName = "# Total committed Bytes";
				lblgctime.Text =pcclrmemmngr.NextValue().ToString()+" Bytes";
				pcclrmemmngr.CounterName = "# Total reserved Bytes";
				label1.Text =pcclrmemmngr.NextValue().ToString()+" Bytes";
				pcclrmemmngr.CounterName = "Large Object Heap size";
				label7.Text =pcclrmemmngr.NextValue().ToString()+" Bytes";
			}
			catch
			{
				
			}
		}
		#endregion
		public frmmain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmmain));
			this.taskmgrnotify = new System.Windows.Forms.NotifyIcon(this.components);
			this.lvcxtmnu = new System.Windows.Forms.ContextMenu();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItem15 = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.processcount = new System.Windows.Forms.StatusBarPanel();
			this.threadcount = new System.Windows.Forms.StatusBarPanel();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem17 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.pcclrmemmngr = new System.Diagnostics.PerformanceCounter();
			this.lvprocesslist = new System.Windows.Forms.ListView();
			this.procname = new System.Windows.Forms.ColumnHeader();
			this.PID = new System.Windows.Forms.ColumnHeader();
			this.procstarttime = new System.Windows.Forms.ColumnHeader();
			this.proccputime = new System.Windows.Forms.ColumnHeader();
			this.memusage = new System.Windows.Forms.ColumnHeader();
			this.peakmemusage = new System.Windows.Forms.ColumnHeader();
			this.noofhandles = new System.Windows.Forms.ColumnHeader();
			this.nonofthreads = new System.Windows.Forms.ColumnHeader();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.label4 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lbltotalreservedbytes = new System.Windows.Forms.Label();
			this.lblgctime = new System.Windows.Forms.Label();
			this.gen2heapsize = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.gen1heapsize = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.gen0heapsize = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.lbltotheapsize = new System.Windows.Forms.Label();
			this.lblheapbyte = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.processcount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.threadcount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pcclrmemmngr)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// taskmgrnotify
			// 
			this.taskmgrnotify.Icon = ((System.Drawing.Icon)(resources.GetObject("taskmgrnotify.Icon")));
			this.taskmgrnotify.Text = "Task Manager is in visible Mode";
			this.taskmgrnotify.Visible = true;
			this.taskmgrnotify.Click += new System.EventHandler(this.taskmgrnotify_Click);
			// 
			// lvcxtmnu
			// 
			this.lvcxtmnu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuItem9,
																					 this.menuItem10});
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 0;
			this.menuItem9.Text = "End Process";
			this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 1;
			this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuItem11,
																					   this.menuItem15,
																					   this.menuItem16,
																					   this.menuItem12,
																					   this.menuItem13,
																					   this.menuItem14});
			this.menuItem10.Text = "Set Priority";
			this.menuItem10.Popup += new System.EventHandler(this.menuItem10_Popup);
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 0;
			this.menuItem11.RadioCheck = true;
			this.menuItem11.Text = "High";
			this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
			// 
			// menuItem15
			// 
			this.menuItem15.Index = 1;
			this.menuItem15.RadioCheck = true;
			this.menuItem15.Text = "Above Normal";
			this.menuItem15.Click += new System.EventHandler(this.menuItem15_Click);
			// 
			// menuItem16
			// 
			this.menuItem16.Index = 2;
			this.menuItem16.RadioCheck = true;
			this.menuItem16.Text = "Below Normal";
			this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
			// 
			// menuItem12
			// 
			this.menuItem12.Index = 3;
			this.menuItem12.RadioCheck = true;
			this.menuItem12.Text = "Normal";
			this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
			// 
			// menuItem13
			// 
			this.menuItem13.Index = 4;
			this.menuItem13.RadioCheck = true;
			this.menuItem13.Text = "Low";
			this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
			// 
			// menuItem14
			// 
			this.menuItem14.Index = 5;
			this.menuItem14.RadioCheck = true;
			this.menuItem14.Text = "Real Time";
			this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 380);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						  this.processcount,
																						  this.threadcount});
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(432, 24);
			this.statusBar1.TabIndex = 1;
			// 
			// processcount
			// 
			this.processcount.Text = "statusBarPanel1";
			// 
			// threadcount
			// 
			this.threadcount.Text = "statusBarPanel1";
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem5});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem7,
																					  this.menuItem17,
																					  this.menuItem2,
																					  this.menuItem3,
																					  this.menuItem4});
			this.menuItem1.Text = "Main";
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 0;
			this.menuItem7.Shortcut = System.Windows.Forms.Shortcut.CtrlM;
			this.menuItem7.Text = "Connect To";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// menuItem17
			// 
			this.menuItem17.Checked = true;
			this.menuItem17.Index = 1;
			this.menuItem17.Text = "Stop Coloring current process";
			this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 2;
			this.menuItem2.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
			this.menuItem2.Text = "New Task ";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 3;
			this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
			this.menuItem3.Text = "End Process";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 4;
			this.menuItem4.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
			this.menuItem4.Text = "Exit Task Manager";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 1;
			this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem6,
																					  this.menuItem8});
			this.menuItem5.Text = "Options";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 0;
			this.menuItem6.Text = "Always On Top";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 1;
			this.menuItem8.Text = "Hide When Minimized";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// pcclrmemmngr
			// 
			this.pcclrmemmngr.CategoryName = ".NET CLR Memory";
			this.pcclrmemmngr.CounterName = "Large Object Heap size";
			this.pcclrmemmngr.InstanceName = "aspnet_wp";
			// 
			// lvprocesslist
			// 
			this.lvprocesslist.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
			this.lvprocesslist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.procname,
																							this.PID,
																							this.procstarttime,
																							this.proccputime,
																							this.memusage,
																							this.peakmemusage,
																							this.noofhandles,
																							this.nonofthreads});
			this.lvprocesslist.ContextMenu = this.lvcxtmnu;
			this.lvprocesslist.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvprocesslist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lvprocesslist.FullRowSelect = true;
			this.lvprocesslist.Location = new System.Drawing.Point(0, 0);
			this.lvprocesslist.Name = "lvprocesslist";
			this.lvprocesslist.Size = new System.Drawing.Size(424, 354);
			this.lvprocesslist.TabIndex = 0;
			this.lvprocesslist.View = System.Windows.Forms.View.Details;
			// 
			// procname
			// 
			this.procname.Text = "Process Name";
			this.procname.Width = 120;
			// 
			// PID
			// 
			this.PID.Text = "PID";
			// 
			// procstarttime
			// 
			this.procstarttime.Text = "Start Time";
			this.procstarttime.Width = 70;
			// 
			// proccputime
			// 
			this.proccputime.Text = "CPU Time";
			this.proccputime.Width = 80;
			// 
			// memusage
			// 
			this.memusage.Text = "Memory Usage";
			this.memusage.Width = 90;
			// 
			// peakmemusage
			// 
			this.peakmemusage.Text = "Peak  memory usage";
			this.peakmemusage.Width = 120;
			// 
			// noofhandles
			// 
			this.noofhandles.Text = "No.of Handles";
			this.noofhandles.Width = 90;
			// 
			// nonofthreads
			// 
			this.nonofthreads.Text = "No.of Threads";
			this.nonofthreads.Width = 90;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(432, 380);
			this.tabControl1.TabIndex = 2;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.lvprocesslist);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(424, 354);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Task Manager";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.label4);
			this.tabPage2.Controls.Add(this.label7);
			this.tabPage2.Controls.Add(this.label8);
			this.tabPage2.Controls.Add(this.label1);
			this.tabPage2.Controls.Add(this.lbltotalreservedbytes);
			this.tabPage2.Controls.Add(this.lblgctime);
			this.tabPage2.Controls.Add(this.gen2heapsize);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Controls.Add(this.gen1heapsize);
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Controls.Add(this.gen0heapsize);
			this.tabPage2.Controls.Add(this.Label2);
			this.tabPage2.Controls.Add(this.lbltotheapsize);
			this.tabPage2.Controls.Add(this.lblheapbyte);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(424, 354);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = ".NET CLR Memory Monitor";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(8, 144);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128, 23);
			this.label4.TabIndex = 14;
			this.label4.Text = "Total committed Bytes :";
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(224, 208);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(152, 23);
			this.label7.TabIndex = 13;
			this.label7.Tag = "";
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.Location = new System.Drawing.Point(8, 208);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(128, 23);
			this.label8.TabIndex = 12;
			this.label8.Text = "Large Object Heap size :";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(224, 176);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 23);
			this.label1.TabIndex = 11;
			this.label1.Tag = "";
			// 
			// lbltotalreservedbytes
			// 
			this.lbltotalreservedbytes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbltotalreservedbytes.Location = new System.Drawing.Point(8, 176);
			this.lbltotalreservedbytes.Name = "lbltotalreservedbytes";
			this.lbltotalreservedbytes.Size = new System.Drawing.Size(128, 23);
			this.lbltotalreservedbytes.TabIndex = 10;
			this.lbltotalreservedbytes.Text = "Total reserved Bytes :";
			// 
			// lblgctime
			// 
			this.lblgctime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblgctime.Location = new System.Drawing.Point(224, 144);
			this.lblgctime.Name = "lblgctime";
			this.lblgctime.Size = new System.Drawing.Size(152, 23);
			this.lblgctime.TabIndex = 9;
			this.lblgctime.Tag = "";
			// 
			// gen2heapsize
			// 
			this.gen2heapsize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.gen2heapsize.Location = new System.Drawing.Point(224, 120);
			this.gen2heapsize.Name = "gen2heapsize";
			this.gen2heapsize.Size = new System.Drawing.Size(168, 23);
			this.gen2heapsize.TabIndex = 7;
			this.gen2heapsize.Tag = "";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(8, 120);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 23);
			this.label5.TabIndex = 6;
			this.label5.Text = "Gen 2 Heap Size :";
			// 
			// gen1heapsize
			// 
			this.gen1heapsize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.gen1heapsize.Location = new System.Drawing.Point(224, 88);
			this.gen1heapsize.Name = "gen1heapsize";
			this.gen1heapsize.Size = new System.Drawing.Size(160, 23);
			this.gen1heapsize.TabIndex = 5;
			this.gen1heapsize.Tag = "";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(8, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "Gen 1 Heap Size :";
			// 
			// gen0heapsize
			// 
			this.gen0heapsize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.gen0heapsize.Location = new System.Drawing.Point(224, 56);
			this.gen0heapsize.Name = "gen0heapsize";
			this.gen0heapsize.Size = new System.Drawing.Size(168, 23);
			this.gen0heapsize.TabIndex = 3;
			this.gen0heapsize.Tag = "";
			// 
			// Label2
			// 
			this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label2.Location = new System.Drawing.Point(8, 56);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(112, 23);
			this.Label2.TabIndex = 2;
			this.Label2.Text = "Gen 0 Heap Size :";
			// 
			// lbltotheapsize
			// 
			this.lbltotheapsize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbltotheapsize.Location = new System.Drawing.Point(224, 24);
			this.lbltotheapsize.Name = "lbltotheapsize";
			this.lbltotheapsize.Size = new System.Drawing.Size(120, 23);
			this.lbltotheapsize.TabIndex = 1;
			this.lbltotheapsize.Tag = "";
			// 
			// lblheapbyte
			// 
			this.lblheapbyte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblheapbyte.Location = new System.Drawing.Point(8, 24);
			this.lblheapbyte.Name = "lblheapbyte";
			this.lblheapbyte.Size = new System.Drawing.Size(112, 23);
			this.lblheapbyte.TabIndex = 0;
			this.lblheapbyte.Text = "Total Bytes in Heap : ";
			// 
			// frmmain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(432, 404);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.statusBar1);
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.Name = "frmmain";
			this.Text = "Task Manager Connected to Local";
			this.Load += new System.EventHandler(this.frmmain_Load);
			((System.ComponentModel.ISupportInitialize)(this.processcount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.threadcount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pcclrmemmngr)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			objtaskmgr = new frmmain();
			Application.Run(objtaskmgr);
		}

		private void taskmgrnotify_Click(object sender, System.EventArgs e)
		{
			if(objtaskmgr.Visible)
			{
				objtaskmgr.Visible = false;
				taskmgrnotify.Text = "Task Manager is in Invisible Mode";
			}
			else
			{
				objtaskmgr.Visible = true;
				taskmgrnotify.Text = "Task Manager is in visible Mode";
			}
		}
		private void frmmain_Load(object sender, System.EventArgs e)
		{
			mcname = ".";
			presentprocdetails = new Hashtable();
			LoadAllProcessesOnStartup();
			System.Threading.TimerCallback timerDelegate = 
				new System.Threading.TimerCallback(this.LoadAllProcesses);
			t = new System.Threading.Timer(timerDelegate,null,1000,1000);
    	}
	
		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			//Here,We are going to kill selected Process by getting ID...
			if(lvprocesslist.SelectedItems.Count>=1)
			{
				try
				{
					int selectedpid = Convert.ToInt32(lvprocesslist.SelectedItems[0].SubItems[1].Text.ToString());
					Process.GetProcessById(selectedpid,mcname).Kill();
				}
				catch
				{
					erroccured = true;
				}
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			try
			{
			    objnewprocess = new frmnewprcdetails();
				objnewprocess.ShowDialog();
                if(newprocpathandparm.Length != 0)
				{
					if(newprocpathandparm.IndexOf("\\") == -1)
					{
						string[] newprocdetails = newprocpathandparm.Split(' ');
						if(newprocdetails.Length > 1)
						{
							Process newprocess = Process.Start(newprocdetails[0].ToString(),newprocdetails[1].ToString());
						}
						else
						{
							Process newprocess = Process.Start(newprocdetails[0].ToString());
						}
					}
					else
					{
						string procname = newprocpathandparm.Substring(newprocpathandparm.LastIndexOf("\\")+1);
						string[] newprocdetails = procname.Split(' ');
						if(newprocdetails.Length > 1)
						{
							Process newprocess = Process.Start(newprocpathandparm.Replace(newprocdetails[1].ToString(),""),newprocdetails[1].ToString());
						}
						else
						{
							Process newprocess = Process.Start(newprocpathandparm);
						}

					}
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			if(menuItem6.Checked)
			{
				menuItem6.Checked = false;
				objtaskmgr.TopMost = false;
			}
			else
			{
				menuItem6.Checked = true;
				objtaskmgr.TopMost = true;
			}
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			if(menuItem8.Checked)
			{
				menuItem8.Checked = false;
				objtaskmgr.ShowInTaskbar = true;
			}
			else
			{
				menuItem8.Checked = true;
				objtaskmgr.ShowInTaskbar = false;
			}
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			menuItem3_Click(sender,e);
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			SetProcessPriority(menuItem11);
		}

		private void menuItem15_Click(object sender, System.EventArgs e)
		{
			SetProcessPriority(menuItem15);
		}

		private void menuItem16_Click(object sender, System.EventArgs e)
		{
			SetProcessPriority(menuItem16);
		}

		private void menuItem12_Click(object sender, System.EventArgs e)
		{
			SetProcessPriority(menuItem12);
		}
		private void menuItem13_Click(object sender, System.EventArgs e)
		{
			SetProcessPriority(menuItem13);
		}

		private void menuItem14_Click(object sender, System.EventArgs e)
		{
			SetProcessPriority(menuItem14);
		}

		private void menuItem10_Popup(object sender, System.EventArgs e)
		{
			try
			{
				int selectedpid = Convert.ToInt32(lvprocesslist.SelectedItems[0].SubItems[1].Text.ToString());
				Process selectedprocess = Process.GetProcessById(selectedpid,mcname);
				string priority = selectedprocess.PriorityClass.ToString();
				foreach(MenuItem mnuitem in menuItem10.MenuItems)
				{
					string mnutext = mnuitem.Text.ToUpper().Replace(" ","");
					if(mnutext == "LOW")
                       mnutext = "IDLE";
					if(mnutext != priority.ToUpper())
						mnuitem.Checked = false;
					else
					{
						mnuitem.Checked = true;
					}
				}
				
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			try
			{
				string caption = "Enter Machine Name";
				objnewprocess = new frmnewprcdetails(caption);
				if(objnewprocess.ShowDialog()!= DialogResult.Cancel)
				{
					t.Dispose();
					presentprocdetails.Clear();
					lvprocesslist.Items.Clear();
					LoadAllProcessesOnStartup();
					if(frmmain.mcname == ".")
					{
						frmmain.objtaskmgr.Text = "Task Manager Connected to Local";
						menuItem3.Visible = true;
						menuItem9.Visible = true;
						menuItem2.Visible = true;
						menuItem10.Visible = true;
					}
					else
					{
						frmmain.objtaskmgr.Text = "Task Manager Connected to "+frmmain.mcname;
						menuItem3.Visible = false;
						menuItem9.Visible = false;
						menuItem2.Visible = false;
						menuItem10.Visible = false;
					}
					System.Threading.TimerCallback timerDelegate = 
						new System.Threading.TimerCallback(this.LoadAllProcesses);
					t = new System.Threading.Timer(timerDelegate,null,1000,1000);
				}
				}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		
		}

		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(tabControl1.SelectedIndex == 1)
			{
				System.Threading.TimerCallback timerDelegate = 
					new System.Threading.TimerCallback(this.LoadAllMemoryDetails);
				tclr = new System.Threading.Timer(timerDelegate,null,0,1000);
			}
			else
			{
				tclr.Dispose();
			}
		}

		private void menuItem17_Click(object sender, System.EventArgs e)
		{
			if(menuItem17.Checked)
				menuItem17.Checked = false;
			else
				menuItem17.Checked = true;
							
							
		}

	}
}
