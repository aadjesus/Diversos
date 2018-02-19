using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Diagnostics;
using System.Xml;
using System.IO;

namespace Magi.OutlookConnector
{
	/// <summary>
	/// Summary description for DataExportForm.
	/// </summary>
	public class DataExportForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtExportFile;
		private System.Windows.Forms.ListView lstExportObjects;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGrid dgOutlookData;
		private System.Data.DataSet dsOutlookData;
		private System.Windows.Forms.Button btnPreview;
		private System.Windows.Forms.ProgressBar pgFolderProgress;
		private System.Windows.Forms.ProgressBar pgItemProgress;
		private System.Windows.Forms.LinkLabel lnkAbout;
		private System.ComponentModel.IContainer components;

		public DataExportForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Appointments");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Contacts");
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Inbox E-Mails");
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Notes");
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Tasks");
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DataExportForm));
			this.btnClose = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtExportFile = new System.Windows.Forms.TextBox();
			this.lstExportObjects = new System.Windows.Forms.ListView();
			this.btnExport = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.dgOutlookData = new System.Windows.Forms.DataGrid();
			this.dsOutlookData = new System.Data.DataSet();
			this.btnPreview = new System.Windows.Forms.Button();
			this.pgFolderProgress = new System.Windows.Forms.ProgressBar();
			this.pgItemProgress = new System.Windows.Forms.ProgressBar();
			this.lnkAbout = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)(this.dgOutlookData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsOutlookData)).BeginInit();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(552, 419);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.Location = new System.Drawing.Point(8, 395);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "Export File:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtExportFile
			// 
			this.txtExportFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtExportFile.Location = new System.Drawing.Point(80, 392);
			this.txtExportFile.Name = "txtExportFile";
			this.txtExportFile.Size = new System.Drawing.Size(544, 20);
			this.txtExportFile.TabIndex = 2;
			this.txtExportFile.Text = "OutlookExport.xml";
			// 
			// lstExportObjects
			// 
			this.lstExportObjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.lstExportObjects.CheckBoxes = true;
			listViewItem1.StateImageIndex = 0;
			listViewItem2.StateImageIndex = 0;
			listViewItem3.StateImageIndex = 0;
			listViewItem4.StateImageIndex = 0;
			listViewItem5.StateImageIndex = 0;
			this.lstExportObjects.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
																							 listViewItem1,
																							 listViewItem2,
																							 listViewItem3,
																							 listViewItem4,
																							 listViewItem5});
			this.lstExportObjects.Location = new System.Drawing.Point(8, 40);
			this.lstExportObjects.Name = "lstExportObjects";
			this.lstExportObjects.Size = new System.Drawing.Size(128, 344);
			this.lstExportObjects.TabIndex = 7;
			this.lstExportObjects.View = System.Windows.Forms.View.List;
			// 
			// btnExport
			// 
			this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExport.Location = new System.Drawing.Point(472, 419);
			this.btnExport.Name = "btnExport";
			this.btnExport.TabIndex = 9;
			this.btnExport.Text = "Export";
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(214, 25);
			this.label2.TabIndex = 10;
			this.label2.Text = "Outlook Data Export";
			// 
			// dgOutlookData
			// 
			this.dgOutlookData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgOutlookData.DataMember = "";
			this.dgOutlookData.DataSource = this.dsOutlookData;
			this.dgOutlookData.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgOutlookData.Location = new System.Drawing.Point(144, 40);
			this.dgOutlookData.Name = "dgOutlookData";
			this.dgOutlookData.ReadOnly = true;
			this.dgOutlookData.Size = new System.Drawing.Size(480, 344);
			this.dgOutlookData.TabIndex = 11;
			// 
			// dsOutlookData
			// 
			this.dsOutlookData.DataSetName = "OutlookData";
			this.dsOutlookData.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// btnPreview
			// 
			this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPreview.Location = new System.Drawing.Point(392, 419);
			this.btnPreview.Name = "btnPreview";
			this.btnPreview.TabIndex = 12;
			this.btnPreview.Text = "Preview";
			this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
			// 
			// pgFolderProgress
			// 
			this.pgFolderProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pgFolderProgress.Location = new System.Drawing.Point(8, 420);
			this.pgFolderProgress.Name = "pgFolderProgress";
			this.pgFolderProgress.Size = new System.Drawing.Size(368, 12);
			this.pgFolderProgress.Step = 1;
			this.pgFolderProgress.TabIndex = 13;
			// 
			// pgItemProgress
			// 
			this.pgItemProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pgItemProgress.Location = new System.Drawing.Point(8, 432);
			this.pgItemProgress.Name = "pgItemProgress";
			this.pgItemProgress.Size = new System.Drawing.Size(368, 12);
			this.pgItemProgress.Step = 1;
			this.pgItemProgress.TabIndex = 14;
			// 
			// lnkAbout
			// 
			this.lnkAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lnkAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lnkAbout.Location = new System.Drawing.Point(520, 8);
			this.lnkAbout.Name = "lnkAbout";
			this.lnkAbout.Size = new System.Drawing.Size(100, 24);
			this.lnkAbout.TabIndex = 15;
			this.lnkAbout.TabStop = true;
			this.lnkAbout.Text = "About";
			this.lnkAbout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lnkAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAbout_LinkClicked);
			// 
			// DataExportForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(632, 446);
			this.Controls.Add(this.lnkAbout);
			this.Controls.Add(this.pgItemProgress);
			this.Controls.Add(this.pgFolderProgress);
			this.Controls.Add(this.btnPreview);
			this.Controls.Add(this.dgOutlookData);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtExportFile);
			this.Controls.Add(this.btnExport);
			this.Controls.Add(this.lstExportObjects);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DataExportForm";
			this.Text = "Microsoft Outlook Data Exporter";
			this.Load += new System.EventHandler(this.DataExportForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgOutlookData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsOutlookData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new DataExportForm());
		}

		private void DataExportForm_Load(object sender, System.EventArgs e)
		{
			txtExportFile.Text = Directory.GetCurrentDirectory() + "\\OutlookExport.xml";
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void btnExport_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (txtExportFile.Text.Length == 0)
					throw new Exception("Export file path must be specified!");
				if (File.Exists(txtExportFile.Text))
					throw new Exception("File already exists!");
				
				//Create the FileStream to write with.
				FileStream fs = new FileStream(txtExportFile.Text, FileMode.Create);

				//Create an XmlTextWriter for the FileStream.
				XmlTextWriter xtw = new XmlTextWriter(fs, System.Text.Encoding.Unicode);

				//Add processing instructions to the beginning of the file
				xtw.WriteProcessingInstruction("xml", "version='1.0'");
				//xtw.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='outlook.xsl'");

				dsOutlookData = this.getCheckedItemSet();
				dsOutlookData.WriteXml(xtw);
				xtw.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnPreview_Click(object sender, System.EventArgs e)
		{
			dsOutlookData = this.getCheckedItemSet();
			dgOutlookData.DataSource = dsOutlookData;
		}

		private DataSet getCheckedItemSet()
		{
			DataSet ds = new DataSet();
			OutlookConnector outlook = new OutlookConnector();

			// setup progress bars and process selected folders
			pgFolderProgress.Value = 0;
			outlook.ItemProcessed += new OutlookItemProcessed(outlook_ItemProcessed);
			pgFolderProgress.Maximum = lstExportObjects.CheckedItems.Count;
			foreach (ListViewItem obj in lstExportObjects.CheckedItems)
			{
				pgFolderProgress.Value++;
				pgItemProgress.Value = 0;

				switch (obj.Index) 
				{
					case 0:
						pgItemProgress.Maximum = outlook.getFolderCount(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderCalendar);
						ds.Merge(outlook.getCalendarDataSet());
						break;
					case 1:
						pgItemProgress.Maximum = outlook.getFolderCount(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderContacts);
						ds.Merge(outlook.getContactDataSet());
						break;
					case 2:
						pgItemProgress.Maximum = outlook.getFolderCount(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);
						ds.Merge(outlook.getInboxDataSet());
						break;
					case 3:
						pgItemProgress.Maximum = outlook.getFolderCount(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderNotes);
						ds.Merge(outlook.getNoteDataSet());
						break;
					case 4:
						pgItemProgress.Maximum = outlook.getFolderCount(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderTasks);
						ds.Merge(outlook.getTaskDataSet());
						break;
					default:
						Debug.WriteLine("Unsupported Export: " + obj.Index);
						break;
				}
			}
			outlook.Dispose();
			return ds;
		}

		private void outlook_ItemProcessed()
		{
			pgItemProgress.Value++;
		}

		private void lnkAbout_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			string uri = Directory.GetCurrentDirectory() + "\\readme.html";
			if (!File.Exists(uri)) uri = Directory.GetCurrentDirectory() + "\\..\\..\\readme.html";
			if (!File.Exists(uri)) 
			{
				MessageBox.Show("Distribution notes not found.");
			} 
			else 
			{
				Process myProcess = new Process();
				ProcessStartInfo  myProcessStartInfo = new ProcessStartInfo("iexplore.exe", uri);
				myProcess.StartInfo = myProcessStartInfo;
				myProcess.Start();
			}
		}
	}
}
