using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace WinComponents
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Home : System.Windows.Forms.Form
	{
		#region Designer & Ctor stuff
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtCity;
		private System.Windows.Forms.TextBox txtState;
		private System.Windows.Forms.TextBox txtCountry;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Label label6;
		private PubsMvc.PublisherController publisherController;
		private System.Windows.Forms.TextBox txtID;
		private System.ComponentModel.IContainer components;

		public Home()
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
            System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtState = new System.Windows.Forms.TextBox();
            this.txtCountry = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.publisherController = new PubsMvc.PublisherController(this.components);
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Name:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "City:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "State:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Country:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(68, 8);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(144, 20);
            this.txtID.TabIndex = 7;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(68, 36);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(144, 20);
            this.txtName.TabIndex = 8;
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(68, 64);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(144, 20);
            this.txtCity.TabIndex = 9;
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(68, 92);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(144, 20);
            this.txtState.TabIndex = 10;
            // 
            // txtCountry
            // 
            this.txtCountry.Location = new System.Drawing.Point(68, 120);
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.Size = new System.Drawing.Size(144, 20);
            this.txtCountry.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtID);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtCountry);
            this.panel2.Controls.Add(this.txtState);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtName);
            this.panel2.Controls.Add(this.txtCity);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnLoad);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Location = new System.Drawing.Point(46, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(220, 188);
            this.panel2.TabIndex = 12;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.Location = new System.Drawing.Point(124, 156);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(56, 23);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.SystemColors.Control;
            this.btnLoad.Location = new System.Drawing.Point(12, 156);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(48, 23);
            this.btnLoad.TabIndex = 13;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.Location = new System.Drawing.Point(68, 156);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(48, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "Publisher information:";
            // 
            // publisherController
            // 
            this.publisherController.ConnectionString = ((string)(configurationAppSettings.GetValue("Pubs", typeof(string))));
            // 
            // ------------- ClariuS Custom Code -------------
            // Controller code
            // 
            this.publisherController.ConfiguredViews.Add("txtCity", new Mvc.Components.Controller.ViewInfo("txtCity", "Text", "Publisher", "City"));
            this.publisherController.ConfiguredViews.Add("txtID", new Mvc.Components.Controller.ViewInfo("txtID", "Text", "Publisher", "ID"));
            this.publisherController.ConfiguredViews.Add("txtName", new Mvc.Components.Controller.ViewInfo("txtName", "Text", "Publisher", "Name"));
            this.publisherController.ConfiguredViews.Add("txtCountry", new Mvc.Components.Controller.ViewInfo("txtCountry", "Text", "Publisher", "Country"));
            this.publisherController.ConfiguredViews.Add("txtState", new Mvc.Components.Controller.ViewInfo("txtState", "Text", "Publisher", "State"));
            // Connect the controller with the hosting environment.
            new Mvc.Components.Adapter.WindowsFormsAdapter().Connect(this.publisherController, this, this.components);
            // 
            // Home
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(310, 276);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel2);
            this.Name = "Home";
            this.Text = "Publisher";
            this.Load += new System.EventHandler(this.Home_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Home());
		}

		private void btnLoad_Click(object sender, System.EventArgs e)
		{
			publisherController.LoadPublisher();
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			publisherController.SavePublisher();
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			publisherController.DeletePublisher();
		}

		private void Home_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
