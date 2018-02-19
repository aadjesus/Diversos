/*
* Copyright (c) 2007, KO Software (official@koapproach.com)
*
* All rights reserved.
*
* Redistribution and use in source and binary forms, with or without
* modification, are permitted provided that the following conditions are met:
*
*     * All original and modified versions of this source code must include the
*       above copyright notice, this list of conditions and the following
*       disclaimer.
*
*     * This code may not be used with or within any modules or code that is 
*       licensed in any way that that compels or requires users or modifiers
*       to release their source code or changes as a requirement for
*       the use, modification or distribution of binary, object or source code
*       based on the licensed source code. (ex: Cannot be used with GPL code.)
*
*     * The name of KO Software may be used to endorse or promote products
*       derived from this software without specific prior written permission.
*
* THIS SOFTWARE IS PROVIDED BY KO SOFTWARE ``AS IS'' AND ANY EXPRESS OR
* IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
* OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO
* EVENT WILL KO SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
* SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
* PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; 
* OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
* WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR
* OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
* ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Step1
{
	public class TestForm : Form
	{
		private System.ComponentModel.Container components = null;

		private System.Windows.Forms.Button myCtlBtn1;
		private System.Windows.Forms.Button myCtlBtn2;
		private System.Windows.Forms.Button myCtlBtn3;
		private System.Windows.Forms.Label myCtlLblPage1;
		private System.Windows.Forms.Label myCtlLblPage2;
		private System.Windows.Forms.Label myCtlLblPage3;
		private System.Windows.Forms.Label myCtlLblPage4;
		private System.Windows.Forms.Label myCtlLblPage5;
		private System.Windows.Forms.ComboBox myCtlComboBox;

		private Kerido.Controls.MultiPaneControl myCtlMultiPane1;
		private Kerido.Controls.MultiPaneControl myCtlMultiPane2;

		private Kerido.Controls.MultiPanePage myCtlMultiPanePage1;
		private Kerido.Controls.MultiPanePage myCtlMultiPanePage2;
		private Kerido.Controls.MultiPanePage myCtlMultiPanePage3;
		private Kerido.Controls.MultiPanePage myCtlMultiPanePage4;
		private Kerido.Controls.MultiPanePage myCtlMultiPanePage5;

		public TestForm()
		{
			InitializeComponent();

			myCtlComboBox.SelectedIndex = 0;

		}

		protected override void Dispose( bool theDisposing )
		{
			if( theDisposing )
				if (components != null) 
					components.Dispose();

			base.Dispose( theDisposing );
		}

		#region Event handlers

		private void Handler_Btn1_Click(object sender, System.EventArgs e)
		{
			myCtlMultiPane1.SelectedPage = myCtlMultiPanePage1;
		}

		private void Handler_Btn2_Click(object sender, System.EventArgs e)
		{
		
			myCtlMultiPane1.SelectedPage = myCtlMultiPanePage2;
		}

		private void Handler_Btn3_Click(object sender, System.EventArgs e)
		{
		
			myCtlMultiPane1.SelectedPage = myCtlMultiPanePage3;
		}

		private void Handler_DropDown_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( myCtlComboBox.SelectedIndex == 0 )
				myCtlMultiPane2.SelectedPage = myCtlMultiPanePage4;
			else
				myCtlMultiPane2.SelectedPage = myCtlMultiPanePage5;
		}

		#endregion

		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
            this.myCtlLblPage1 = new System.Windows.Forms.Label();
            this.myCtlLblPage2 = new System.Windows.Forms.Label();
            this.myCtlLblPage3 = new System.Windows.Forms.Label();
            this.myCtlBtn1 = new System.Windows.Forms.Button();
            this.myCtlBtn2 = new System.Windows.Forms.Button();
            this.myCtlBtn3 = new System.Windows.Forms.Button();
            this.myCtlComboBox = new System.Windows.Forms.ComboBox();
            this.myCtlMultiPane1 = new Kerido.Controls.MultiPaneControl();
            this.myCtlMultiPanePage2 = new Kerido.Controls.MultiPanePage();
            this.myCtlMultiPanePage1 = new Kerido.Controls.MultiPanePage();
            this.myCtlMultiPanePage3 = new Kerido.Controls.MultiPanePage();
            this.myCtlMultiPane2 = new Kerido.Controls.MultiPaneControl();
            this.myCtlMultiPanePage4 = new Kerido.Controls.MultiPanePage();
            this.myCtlLblPage5 = new System.Windows.Forms.Label();
            this.myCtlMultiPanePage5 = new Kerido.Controls.MultiPanePage();
            this.myCtlLblPage4 = new System.Windows.Forms.Label();
            this.myCtlMultiPane1.SuspendLayout();
            this.myCtlMultiPanePage2.SuspendLayout();
            this.myCtlMultiPanePage1.SuspendLayout();
            this.myCtlMultiPanePage3.SuspendLayout();
            this.myCtlMultiPane2.SuspendLayout();
            this.myCtlMultiPanePage4.SuspendLayout();
            this.myCtlMultiPanePage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // myCtlLblPage1
            // 
            this.myCtlLblPage1.Font = new System.Drawing.Font("Tahoma", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.myCtlLblPage1.Location = new System.Drawing.Point(40, 24);
            this.myCtlLblPage1.Name = "myCtlLblPage1";
            this.myCtlLblPage1.Size = new System.Drawing.Size(154, 47);
            this.myCtlLblPage1.TabIndex = 1;
            this.myCtlLblPage1.Text = "Control A Page 1";
            this.myCtlLblPage1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // myCtlLblPage2
            // 
            this.myCtlLblPage2.Font = new System.Drawing.Font("Tahoma", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.myCtlLblPage2.Location = new System.Drawing.Point(40, 24);
            this.myCtlLblPage2.Name = "myCtlLblPage2";
            this.myCtlLblPage2.Size = new System.Drawing.Size(240, 64);
            this.myCtlLblPage2.TabIndex = 2;
            this.myCtlLblPage2.Text = "Control A Page 2";
            this.myCtlLblPage2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // myCtlLblPage3
            // 
            this.myCtlLblPage3.Font = new System.Drawing.Font("Tahoma", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.myCtlLblPage3.Location = new System.Drawing.Point(40, 24);
            this.myCtlLblPage3.Name = "myCtlLblPage3";
            this.myCtlLblPage3.Size = new System.Drawing.Size(240, 64);
            this.myCtlLblPage3.TabIndex = 0;
            this.myCtlLblPage3.Text = "Control A Page 3";
            this.myCtlLblPage3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // myCtlBtn1
            // 
            this.myCtlBtn1.Location = new System.Drawing.Point(8, 8);
            this.myCtlBtn1.Name = "myCtlBtn1";
            this.myCtlBtn1.Size = new System.Drawing.Size(104, 23);
            this.myCtlBtn1.TabIndex = 1;
            this.myCtlBtn1.Text = "Control A Page 1";
            this.myCtlBtn1.Click += new System.EventHandler(this.Handler_Btn1_Click);
            // 
            // myCtlBtn2
            // 
            this.myCtlBtn2.Location = new System.Drawing.Point(124, 8);
            this.myCtlBtn2.Name = "myCtlBtn2";
            this.myCtlBtn2.Size = new System.Drawing.Size(104, 23);
            this.myCtlBtn2.TabIndex = 1;
            this.myCtlBtn2.Text = "Control A Page 2";
            this.myCtlBtn2.Click += new System.EventHandler(this.Handler_Btn2_Click);
            // 
            // myCtlBtn3
            // 
            this.myCtlBtn3.Location = new System.Drawing.Point(240, 8);
            this.myCtlBtn3.Name = "myCtlBtn3";
            this.myCtlBtn3.Size = new System.Drawing.Size(104, 23);
            this.myCtlBtn3.TabIndex = 1;
            this.myCtlBtn3.Text = "Control A Page3";
            this.myCtlBtn3.Click += new System.EventHandler(this.Handler_Btn3_Click);
            // 
            // myCtlComboBox
            // 
            this.myCtlComboBox.Items.AddRange(new object[] {
            "Control B Page 1",
            "Control A Page 2"});
            this.myCtlComboBox.Location = new System.Drawing.Point(8, 160);
            this.myCtlComboBox.Name = "myCtlComboBox";
            this.myCtlComboBox.Size = new System.Drawing.Size(136, 21);
            this.myCtlComboBox.TabIndex = 2;
            this.myCtlComboBox.SelectedIndexChanged += new System.EventHandler(this.Handler_DropDown_SelectedIndexChanged);
            // 
            // myCtlMultiPane1
            // 
            this.myCtlMultiPane1.BackColor = System.Drawing.Color.Transparent;
            this.myCtlMultiPane1.Controls.Add(this.myCtlMultiPanePage2);
            this.myCtlMultiPane1.Controls.Add(this.myCtlMultiPanePage1);
            this.myCtlMultiPane1.Controls.Add(this.myCtlMultiPanePage3);
            this.myCtlMultiPane1.Location = new System.Drawing.Point(8, 37);
            this.myCtlMultiPane1.Name = "myCtlMultiPane1";
            this.myCtlMultiPane1.SelectedPage = this.myCtlMultiPanePage1;
            this.myCtlMultiPane1.Size = new System.Drawing.Size(336, 115);
            this.myCtlMultiPane1.TabIndex = 0;
            // 
            // myCtlMultiPanePage2
            // 
            this.myCtlMultiPanePage2.Controls.Add(this.myCtlLblPage2);
            this.myCtlMultiPanePage2.Name = "myCtlMultiPanePage2";
            this.myCtlMultiPanePage2.Size = new System.Drawing.Size(336, 115);
            this.myCtlMultiPanePage2.TabIndex = 0;
            // 
            // myCtlMultiPanePage1
            // 
            this.myCtlMultiPanePage1.Controls.Add(this.myCtlLblPage1);
            this.myCtlMultiPanePage1.Name = "myCtlMultiPanePage1";
            this.myCtlMultiPanePage1.Size = new System.Drawing.Size(336, 115);
            this.myCtlMultiPanePage1.TabIndex = 0;
            // 
            // myCtlMultiPanePage3
            // 
            this.myCtlMultiPanePage3.Controls.Add(this.myCtlLblPage3);
            this.myCtlMultiPanePage3.Name = "myCtlMultiPanePage3";
            this.myCtlMultiPanePage3.Size = new System.Drawing.Size(336, 115);
            this.myCtlMultiPanePage3.TabIndex = 2;
            // 
            // myCtlMultiPane2
            // 
            this.myCtlMultiPane2.BackColor = System.Drawing.Color.Transparent;
            this.myCtlMultiPane2.Controls.Add(this.myCtlMultiPanePage4);
            this.myCtlMultiPane2.Controls.Add(this.myCtlMultiPanePage5);
            this.myCtlMultiPane2.Location = new System.Drawing.Point(8, 192);
            this.myCtlMultiPane2.Name = "myCtlMultiPane2";
            this.myCtlMultiPane2.SelectedPage = this.myCtlMultiPanePage4;
            this.myCtlMultiPane2.Size = new System.Drawing.Size(336, 104);
            this.myCtlMultiPane2.TabIndex = 1;
            // 
            // myCtlMultiPanePage4
            // 
            this.myCtlMultiPanePage4.Controls.Add(this.myCtlLblPage5);
            this.myCtlMultiPanePage4.Name = "myCtlMultiPanePage4";
            this.myCtlMultiPanePage4.Size = new System.Drawing.Size(336, 104);
            this.myCtlMultiPanePage4.TabIndex = 3;
            // 
            // myCtlLblPage5
            // 
            this.myCtlLblPage5.Font = new System.Drawing.Font("Tahoma", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.myCtlLblPage5.Location = new System.Drawing.Point(40, 24);
            this.myCtlLblPage5.Name = "myCtlLblPage5";
            this.myCtlLblPage5.Size = new System.Drawing.Size(240, 64);
            this.myCtlLblPage5.TabIndex = 2;
            this.myCtlLblPage5.Text = "Control B Page 1";
            this.myCtlLblPage5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // myCtlMultiPanePage5
            // 
            this.myCtlMultiPanePage5.Controls.Add(this.myCtlLblPage4);
            this.myCtlMultiPanePage5.Name = "myCtlMultiPanePage5";
            this.myCtlMultiPanePage5.Size = new System.Drawing.Size(336, 104);
            this.myCtlMultiPanePage5.TabIndex = 0;
            // 
            // myCtlLblPage4
            // 
            this.myCtlLblPage4.Font = new System.Drawing.Font("Tahoma", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.myCtlLblPage4.Location = new System.Drawing.Point(40, 24);
            this.myCtlLblPage4.Name = "myCtlLblPage4";
            this.myCtlLblPage4.Size = new System.Drawing.Size(240, 64);
            this.myCtlLblPage4.TabIndex = 2;
            this.myCtlLblPage4.Text = "Control B Page 2";
            this.myCtlLblPage4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TestForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(354, 303);
            this.Controls.Add(this.myCtlMultiPane1);
            this.Controls.Add(this.myCtlMultiPane2);
            this.Controls.Add(this.myCtlComboBox);
            this.Controls.Add(this.myCtlBtn1);
            this.Controls.Add(this.myCtlBtn2);
            this.Controls.Add(this.myCtlBtn3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TestForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "MultiPaneControl Test";
            this.myCtlMultiPane1.ResumeLayout(false);
            this.myCtlMultiPanePage2.ResumeLayout(false);
            this.myCtlMultiPanePage1.ResumeLayout(false);
            this.myCtlMultiPanePage3.ResumeLayout(false);
            this.myCtlMultiPane2.ResumeLayout(false);
            this.myCtlMultiPanePage4.ResumeLayout(false);
            this.myCtlMultiPanePage5.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion


		[STAThread]
		static void Main() 
		{
			Application.Run(new TestForm());
		}

	}
}
