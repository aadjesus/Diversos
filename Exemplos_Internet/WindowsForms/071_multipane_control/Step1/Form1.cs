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

		MultiPaneControl myCtlMultiPane;
		MultiPanePage myCtlPage1;
		MultiPanePage myCtlPage2;
		MultiPanePage myCtlPage3;
		private System.Windows.Forms.Button myCtlBtn1;
		private System.Windows.Forms.Button myCtlBtn2;
		private System.Windows.Forms.Button myCtlBtn3;
		private System.Windows.Forms.Label myCtlLblPage1;
        private System.Windows.Forms.Label myCtlLblPage2;
		private System.Windows.Forms.Label myCtlLblPage3;

		public TestForm()
		{
			InitializeComponent();
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
			myCtlMultiPane.SelectedPage = myCtlPage1;
		}

		private void Handler_Btn2_Click(object sender, System.EventArgs e)
		{
		
			myCtlMultiPane.SelectedPage = myCtlPage2;
		}

		private void Handler_Btn3_Click(object sender, System.EventArgs e)
		{
		
			myCtlMultiPane.SelectedPage = myCtlPage3;
		}

		#endregion

		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
            this.myCtlBtn1 = new System.Windows.Forms.Button();
            this.myCtlBtn2 = new System.Windows.Forms.Button();
            this.myCtlBtn3 = new System.Windows.Forms.Button();
            this.myCtlMultiPane = new Step1.MultiPaneControl();
            this.myCtlPage1 = new Step1.MultiPanePage();
            this.myCtlLblPage1 = new System.Windows.Forms.Label();
            this.myCtlPage2 = new Step1.MultiPanePage();
            this.myCtlLblPage2 = new System.Windows.Forms.Label();
            this.myCtlPage3 = new Step1.MultiPanePage();
            this.myCtlLblPage3 = new System.Windows.Forms.Label();
            this.myCtlMultiPane.SuspendLayout();
            this.myCtlPage1.SuspendLayout();
            this.myCtlPage2.SuspendLayout();
            this.myCtlPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // myCtlBtn1
            // 
            this.myCtlBtn1.Location = new System.Drawing.Point(8, 8);
            this.myCtlBtn1.Name = "myCtlBtn1";
            this.myCtlBtn1.Size = new System.Drawing.Size(96, 23);
            this.myCtlBtn1.TabIndex = 1;
            this.myCtlBtn1.Text = "Page 1";
            this.myCtlBtn1.Click += new System.EventHandler(this.Handler_Btn1_Click);
            // 
            // myCtlBtn2
            // 
            this.myCtlBtn2.Location = new System.Drawing.Point(120, 8);
            this.myCtlBtn2.Name = "myCtlBtn2";
            this.myCtlBtn2.Size = new System.Drawing.Size(96, 23);
            this.myCtlBtn2.TabIndex = 1;
            this.myCtlBtn2.Text = "Page 2";
            this.myCtlBtn2.Click += new System.EventHandler(this.Handler_Btn2_Click);
            // 
            // myCtlBtn3
            // 
            this.myCtlBtn3.Location = new System.Drawing.Point(232, 8);
            this.myCtlBtn3.Name = "myCtlBtn3";
            this.myCtlBtn3.Size = new System.Drawing.Size(96, 23);
            this.myCtlBtn3.TabIndex = 1;
            this.myCtlBtn3.Text = "Page3";
            this.myCtlBtn3.Click += new System.EventHandler(this.Handler_Btn3_Click);
            // 
            // myCtlMultiPane
            // 
            this.myCtlMultiPane.BackColor = System.Drawing.Color.Transparent;
            this.myCtlMultiPane.Controls.Add(this.myCtlPage2);
            this.myCtlMultiPane.Controls.Add(this.myCtlPage3);
            this.myCtlMultiPane.Location = new System.Drawing.Point(8, 37);
            this.myCtlMultiPane.Name = "myCtlMultiPane";
            this.myCtlMultiPane.SelectedPage = this.myCtlPage1;
            this.myCtlMultiPane.Size = new System.Drawing.Size(320, 158);
            this.myCtlMultiPane.TabIndex = 0;
            // 
            // myCtlPage1
            // 
            this.myCtlPage1.BackColor = System.Drawing.Color.Red;
            this.myCtlPage1.Controls.Add(this.myCtlLblPage1);
            this.myCtlPage1.Location = new System.Drawing.Point(59, 62);
            this.myCtlPage1.Name = "myCtlPage1";
            this.myCtlPage1.Size = new System.Drawing.Size(320, 155);
            this.myCtlPage1.TabIndex = 0;
            // 
            // myCtlLblPage1
            // 
            this.myCtlLblPage1.Font = new System.Drawing.Font("Tahoma", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.myCtlLblPage1.Location = new System.Drawing.Point(40, 48);
            this.myCtlLblPage1.Name = "myCtlLblPage1";
            this.myCtlLblPage1.Size = new System.Drawing.Size(240, 64);
            this.myCtlLblPage1.TabIndex = 1;
            this.myCtlLblPage1.Text = "This is page 1";
            this.myCtlLblPage1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // myCtlPage2
            // 
            this.myCtlPage2.Controls.Add(this.myCtlLblPage2);
            this.myCtlPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myCtlPage2.Location = new System.Drawing.Point(0, 0);
            this.myCtlPage2.Name = "myCtlPage2";
            this.myCtlPage2.Size = new System.Drawing.Size(320, 184);
            this.myCtlPage2.TabIndex = 0;
            // 
            // myCtlLblPage2
            // 
            this.myCtlLblPage2.Font = new System.Drawing.Font("Tahoma", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.myCtlLblPage2.Location = new System.Drawing.Point(40, 48);
            this.myCtlLblPage2.Name = "myCtlLblPage2";
            this.myCtlLblPage2.Size = new System.Drawing.Size(240, 64);
            this.myCtlLblPage2.TabIndex = 2;
            this.myCtlLblPage2.Text = "This is page 2";
            this.myCtlLblPage2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // myCtlPage3
            // 
            this.myCtlPage3.Controls.Add(this.myCtlLblPage3);
            this.myCtlPage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myCtlPage3.Location = new System.Drawing.Point(0, 0);
            this.myCtlPage3.Name = "myCtlPage3";
            this.myCtlPage3.Size = new System.Drawing.Size(320, 184);
            this.myCtlPage3.TabIndex = 0;
            // 
            // myCtlLblPage3
            // 
            this.myCtlLblPage3.Font = new System.Drawing.Font("Tahoma", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.myCtlLblPage3.Location = new System.Drawing.Point(40, 48);
            this.myCtlLblPage3.Name = "myCtlLblPage3";
            this.myCtlLblPage3.Size = new System.Drawing.Size(240, 64);
            this.myCtlLblPage3.TabIndex = 0;
            this.myCtlLblPage3.Text = "This is page 3";
            this.myCtlLblPage3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TestForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(336, 229);
            this.Controls.Add(this.myCtlPage1);
            this.Controls.Add(this.myCtlMultiPane);
            this.Controls.Add(this.myCtlBtn1);
            this.Controls.Add(this.myCtlBtn2);
            this.Controls.Add(this.myCtlBtn3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TestForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "MultiPaneControl Test 1";
            this.myCtlMultiPane.ResumeLayout(false);
            this.myCtlPage1.ResumeLayout(false);
            this.myCtlPage2.ResumeLayout(false);
            this.myCtlPage3.ResumeLayout(false);
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
