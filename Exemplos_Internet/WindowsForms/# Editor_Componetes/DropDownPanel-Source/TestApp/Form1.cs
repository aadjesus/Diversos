using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TestApp
{
	/// <summary>
	/// Descrizione di riepilogo per Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private ScrewTurn.DropDownPanel dropDownPanel1;
		private ScrewTurn.DropDownPanel dropDownPanel2;
        private ScrewTurn.DropDownPanel dropDownPanel3;
		/// <summary>
		/// Variabile di progettazione necessaria.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Necessario per il supporto di Progettazione Windows Form
			//
			InitializeComponent();

			//
			// TODO: aggiungere il codice del costruttore dopo la chiamata a InitializeComponent
			//
		}

		/// <summary>
		/// Pulire le risorse in uso.
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

		#region Codice generato da Progettazione Windows Form
		/// <summary>
		/// Metodo necessario per il supporto della finestra di progettazione. Non modificare
		/// il contenuto del metodo con l'editor di codice.
		/// </summary>
		private void InitializeComponent()
		{
            this.dropDownPanel1 = new ScrewTurn.DropDownPanel();
            this.dropDownPanel2 = new ScrewTurn.DropDownPanel();
            this.dropDownPanel3 = new ScrewTurn.DropDownPanel();
            this.SuspendLayout();
            // 
            // dropDownPanel1
            // 
            this.dropDownPanel1.AutoCollapseDelay = -1;
            this.dropDownPanel1.EnableHeaderMenu = true;
            this.dropDownPanel1.ExpandAnimationSpeed = ScrewTurn.AnimationSpeed.Medium;
            this.dropDownPanel1.Expanded = true;
            this.dropDownPanel1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dropDownPanel1.HeaderHeight = 20;
            this.dropDownPanel1.HeaderIconNormal = null;
            this.dropDownPanel1.HeaderIconOver = null;
            this.dropDownPanel1.HeaderText = "DropDownPanel";
            this.dropDownPanel1.HomeLocation = new System.Drawing.Point(8, 12);
            this.dropDownPanel1.HotTrackStyle = ScrewTurn.HotTrackStyle.Both;
            this.dropDownPanel1.Location = new System.Drawing.Point(8, 12);
            this.dropDownPanel1.ManageControls = true;
            this.dropDownPanel1.Moveable = true;
            this.dropDownPanel1.Name = "dropDownPanel1";
            this.dropDownPanel1.RoundedCorners = true;
            this.dropDownPanel1.Size = new System.Drawing.Size(232, 190);
            this.dropDownPanel1.TabIndex = 0;
            // 
            // dropDownPanel2
            // 
            this.dropDownPanel2.AutoCollapseDelay = -1;
            this.dropDownPanel2.EnableHeaderMenu = true;
            this.dropDownPanel2.ExpandAnimationSpeed = ScrewTurn.AnimationSpeed.Medium;
            this.dropDownPanel2.Expanded = true;
            this.dropDownPanel2.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dropDownPanel2.HeaderHeight = 32;
            this.dropDownPanel2.HeaderIconNormal = null;
            this.dropDownPanel2.HeaderIconOver = null;
            this.dropDownPanel2.HeaderText = "DropDownPanel";
            this.dropDownPanel2.HomeLocation = new System.Drawing.Point(8, 208);
            this.dropDownPanel2.HotTrackStyle = ScrewTurn.HotTrackStyle.Both;
            this.dropDownPanel2.Location = new System.Drawing.Point(8, 208);
            this.dropDownPanel2.ManageControls = true;
            this.dropDownPanel2.Moveable = false;
            this.dropDownPanel2.Name = "dropDownPanel2";
            this.dropDownPanel2.RoundedCorners = true;
            this.dropDownPanel2.Size = new System.Drawing.Size(232, 104);
            this.dropDownPanel2.TabIndex = 1;
            // 
            // dropDownPanel3
            // 
            this.dropDownPanel3.AutoCollapseDelay = -1;
            this.dropDownPanel3.EnableHeaderMenu = true;
            this.dropDownPanel3.ExpandAnimationSpeed = ScrewTurn.AnimationSpeed.Medium;
            this.dropDownPanel3.Expanded = true;
            this.dropDownPanel3.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dropDownPanel3.HeaderHeight = 32;
            this.dropDownPanel3.HeaderIconNormal = null;
            this.dropDownPanel3.HeaderIconOver = null;
            this.dropDownPanel3.HeaderText = "DropDownPanel";
            this.dropDownPanel3.HomeLocation = new System.Drawing.Point(8, 320);
            this.dropDownPanel3.HotTrackStyle = ScrewTurn.HotTrackStyle.Both;
            this.dropDownPanel3.Location = new System.Drawing.Point(8, 320);
            this.dropDownPanel3.ManageControls = false;
            this.dropDownPanel3.Moveable = false;
            this.dropDownPanel3.Name = "dropDownPanel3";
            this.dropDownPanel3.RoundedCorners = true;
            this.dropDownPanel3.Size = new System.Drawing.Size(232, 104);
            this.dropDownPanel3.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(248, 434);
            this.Controls.Add(this.dropDownPanel3);
            this.Controls.Add(this.dropDownPanel2);
            this.Controls.Add(this.dropDownPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DropDownPanel TestApp";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Il punto di ingresso principale dell'applicazione.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e) {
			dropDownPanel1.AddManagedControl(dropDownPanel2);
			dropDownPanel1.AddManagedControl(dropDownPanel3);
			dropDownPanel2.AddManagedControl(dropDownPanel3);
		}
	}
}
