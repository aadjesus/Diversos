using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TB.nsListViewEx;

namespace WindowsApplication1{
	public class Form1 : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private TB.nsListViewEx.DataListView dataListView1;
		private ArrayList arrList = new ArrayList();
		private const string SampleTable = "Sample_TBL";
		private System.Windows.Forms.Button btnFill;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnToXML;
		private System.Windows.Forms.Button btnFromXML;
        private DataColumnHeader dataColumnHeader1;
        private DataColumnHeader dataColumnHeader2;
		private DataSet ds = null;

		public Form1(){
			InitializeComponent();
		}

		protected override void Dispose( bool disposing ){
			if( disposing ){
				if (components != null){
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("sadas");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("asdasd");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("sadas");
            this.btnFill = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnToXML = new System.Windows.Forms.Button();
            this.btnFromXML = new System.Windows.Forms.Button();
            this.dataListView1 = new TB.nsListViewEx.DataListView();
            this.dataColumnHeader1 = ((TB.nsListViewEx.DataColumnHeader)(new TB.nsListViewEx.DataColumnHeader()));
            this.dataColumnHeader2 = ((TB.nsListViewEx.DataColumnHeader)(new TB.nsListViewEx.DataColumnHeader()));
            this.SuspendLayout();
            // 
            // btnFill
            // 
            this.btnFill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFill.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnFill.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFill.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFill.ForeColor = System.Drawing.Color.Navy;
            this.btnFill.Location = new System.Drawing.Point(10, 128);
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size(75, 23);
            this.btnFill.TabIndex = 4;
            this.btnFill.Text = "Fill";
            this.btnFill.UseVisualStyleBackColor = false;
            this.btnFill.Click += new System.EventHandler(this.btnFill_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.Color.CadetBlue;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.ForeColor = System.Drawing.Color.PowderBlue;
            this.btnClear.Location = new System.Drawing.Point(90, 128);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnToXML
            // 
            this.btnToXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToXML.BackColor = System.Drawing.Color.Gainsboro;
            this.btnToXML.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToXML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToXML.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnToXML.Location = new System.Drawing.Point(170, 128);
            this.btnToXML.Name = "btnToXML";
            this.btnToXML.Size = new System.Drawing.Size(75, 23);
            this.btnToXML.TabIndex = 7;
            this.btnToXML.Text = "> Disk";
            this.btnToXML.UseVisualStyleBackColor = false;
            this.btnToXML.Click += new System.EventHandler(this.btnToXML_Click);
            // 
            // btnFromXML
            // 
            this.btnFromXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFromXML.BackColor = System.Drawing.Color.Gainsboro;
            this.btnFromXML.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFromXML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFromXML.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnFromXML.Location = new System.Drawing.Point(250, 128);
            this.btnFromXML.Name = "btnFromXML";
            this.btnFromXML.Size = new System.Drawing.Size(75, 23);
            this.btnFromXML.TabIndex = 8;
            this.btnFromXML.Text = "< Disk";
            this.btnFromXML.UseVisualStyleBackColor = false;
            this.btnFromXML.Click += new System.EventHandler(this.btnFromXML_Click);
            // 
            // dataListView1
            // 
            this.dataListView1.AutoDiscovery = true;
            this.dataListView1.BackColor = System.Drawing.Color.PaleTurquoise;
            this.dataListView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataListView1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataListView1.DataBindThreading = true;
            this.dataListView1.DataMember = "";
            this.dataListView1.DataSource = null;
            this.dataListView1.DeSerializationThreading = true;
            this.dataListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataListView1.ForeColor = System.Drawing.Color.Blue;
            this.dataListView1.FullRowSelect = true;
            listViewGroup1.Header = "ListViewGroup";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "ListViewGroup";
            listViewGroup2.Name = "listViewGroup2";
            listViewGroup3.Header = "ListViewGroup";
            listViewGroup3.Name = "listViewGroup3";
            this.dataListView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this.dataListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.dataListView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.dataListView1.Location = new System.Drawing.Point(0, 0);
            this.dataListView1.MultiSelect = false;
            this.dataListView1.Name = "dataListView1";
            this.dataListView1.SerializationThreading = true;
            this.dataListView1.Size = new System.Drawing.Size(336, 173);
            this.dataListView1.TabIndex = 5;
            this.dataListView1.UnavailableDataMessage = "No data available.";
            this.dataListView1.UseCompatibleStateImageBehavior = false;
            this.dataListView1.ItemActivate += new System.EventHandler(this.dataListView1_ItemActivate);
            // 
            // dataColumnHeader1
            // 
            this.dataColumnHeader1.Field = null;
            // 
            // dataColumnHeader2
            // 
            this.dataColumnHeader2.Field = null;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(336, 173);
            this.Controls.Add(this.btnFromXML);
            this.Controls.Add(this.btnToXML);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnFill);
            this.Controls.Add(this.dataListView1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(344, 200);
            this.Name = "Form1";
            this.Text = "DataListView Demo";
            this.ResumeLayout(false);

		}
		#endregion

		#region BootStrapper
		[STAThread]
		static void Main(){
			Application.Run(new Form1());
		}
		#endregion

		#region btnFill_Click Event Handler
		private void btnFill_Click(object sender, System.EventArgs e) {
			// Uncomment this to use the ArrayList collection
			//this.CreateAndFillArrList();
			//
			this.CreateAndFillDS();
			//
			this.dataListView1.DataSource = this.ds;
			//this.dataListView1.DataMember = SampleTable;
			//
			if (this.ds.Tables[SampleTable].Rows.Count >= 1 || arrList.Count >= 1){
				this.dataListView1.Columns[0].Text = "First";
				this.dataListView1.Columns[1].Text = "Second";
				this.dataListView1.Columns[2].Text = "Third";
				this.dataListView1.Columns[3].Text = "Fourth";
				this.dataListView1.Columns[3].TextAlign = HorizontalAlignment.Right;
				//this.dataListView1.Columns[2].TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
				this.dataListView1.ResizeColumns();
			}
		}
		#endregion

		#region btnClear_Click Event Handler
		private void btnClear_Click(object sender, System.EventArgs e) {
			this.dataListView1.Clear();
		}
		#endregion

		#region CreateAndFillDS Method
		private void CreateAndFillDS(){
			if (this.ds != null){
				this.ds.Tables.Clear();
				this.ds.Dispose();
				this.ds = null;
			}
			if (this.ds == null){
				this.ds = new DataSet("SampleDS");
				this.ds.Tables.Add(SampleTable);
				this.ds.Tables[SampleTable].Columns.Add("GetFirstTestSample");
				this.ds.Tables[SampleTable].Columns.Add("GetSecondTestSample");
				this.ds.Tables[SampleTable].Columns.Add("GetThirdTestSample");
				this.ds.Tables[SampleTable].Columns.Add("GetFourthTestSample");
			}
			this.FillSampleDataSet();
		}

		private void FillSampleDataSet(){
			this.ds.Tables[SampleTable].Rows.Add(new object[]{"Hello", "World", "Fubar", 1});
			this.ds.Tables[SampleTable].Rows.Add(new object[]{"Tom", "Brennan", "D'Guru", 2});
			this.ds.Tables[SampleTable].Rows.Add(new object[]{"Leonardo", "DaVinci", "D'Genius", 3});
			this.ds.Tables[SampleTable].Rows.Add(new object[]{"Dan", "Brown", "DaVinci Code", 4});
		}
		#endregion

		#region CreateAndFillArrList Method
		private void CreateAndFillArrList(){
			arrList.Clear();
			arrList.Add(new Sample("Hello", "World", "Fubar", 1));
			arrList.Add(new Sample("Tom", "Brennan", "D'Guru", 2));
			arrList.Add(new Sample("Leonardo", "DaVinci", "D'Genius", 3));
			arrList.Add(new Sample("Dan", "Brown", "DaVinci Code", 4));
		}
		#endregion
		
		#region btnToXML_Click Event Handler
		private void btnToXML_Click(object sender, System.EventArgs e) {
			int nCount = 0;
			foreach (ListViewItem lvi in this.dataListView1.Items){
				lvi.Tag = string.Format("TestTag #{0}", nCount++);
			}
			this.dataListView1.SerializeToDisk("test.xml", false);
		}
		#endregion

		#region btnFromXML_Click Event Handler
		private void btnFromXML_Click(object sender, System.EventArgs e) {
			this.dataListView1.DeSerializeFromDisk("test.xml");
		}
		#endregion

		#region dataListView1_ItemActivate Event Handler
		private void dataListView1_ItemActivate(object sender, System.EventArgs e) {
			if (this.dataListView1.SelectedItems.Count >= 1){
				ListViewItem lvi = (ListViewItem)this.dataListView1.SelectedItems[0];
				System.Diagnostics.Debug.WriteLine("Text: " + lvi.Text + "; Tag: " + lvi.Tag.ToString());
			}
		}
		#endregion
		
	}

	#region Sample Class
	public class Sample{
		private readonly string TestSample;
		private readonly string TestSample2;
		private readonly string TestSample3;
		private readonly int TestSample4;
		public Sample(string _TestSample, string _TestSample2, string _TestSample3, int _TestSample4){
			this.TestSample = _TestSample;
			this.TestSample2 = _TestSample2;
			this.TestSample3 = _TestSample3;
			this.TestSample4 = _TestSample4;
		}
		public string GetFirstTestSample{
			get{ return this.TestSample; }
		}
		public string GetSecondTestSample{
			get{ return this.TestSample2; }
		}
		public string GetThirdTestSample{
			get{ return this.TestSample3; }
		}
		public int GetFourthTestSample{
			get{ return this.TestSample4; }
		}
	}
	#endregion
}
