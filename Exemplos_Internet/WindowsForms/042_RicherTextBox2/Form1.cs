using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace RicherTextBox2
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private System.Windows.Forms.StatusBar statusBar1;
        private Nik.UserControls.RicherTextBox2 rtb;
        private System.Windows.Forms.StatusBarPanel statusBar;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            UpdateStatusBarPosition();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtb = new Nik.UserControls.RicherTextBox2();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.statusBar = new System.Windows.Forms.StatusBarPanel();
            ((System.ComponentModel.ISupportInitialize)(this.statusBar)).BeginInit();
            this.SuspendLayout();
            // 
            // rtb
            // 
            this.rtb.Location = new System.Drawing.Point(8, 8);
            this.rtb.Name = "rtb";
            this.rtb.Size = new System.Drawing.Size(275, 232);
            this.rtb.TabIndex = 0;
            this.rtb.Text = "Some example text";
            this.rtb.SelectionChanged += new System.EventHandler(this.rtb_SelectionChanged);
            this.rtb.CursorPositionChanged += new System.EventHandler(this.rtb_CursorPositionChanged);
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 244);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																												  this.statusBar});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(290, 22);
            this.statusBar1.TabIndex = 1;
            this.statusBar1.Text = "statusBar1";
            // 
            // statusBar
            // 
            this.statusBar.Width = 300;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(290, 266);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.rtb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "RicherTextBox2 Demo";
            ((System.ComponentModel.ISupportInitialize)(this.statusBar)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }

        private void rtb_CursorPositionChanged(object sender, System.EventArgs e)
        {
            UpdateStatusBarPosition();
        }

        private void UpdateStatusBarPosition()
        {
            int line = rtb.CurrentLine;
            int col = rtb.CurrentColumn;
            int pos = rtb.CurrentPosition;

            statusBar.Text = "Line " + line + ", Col " + col + ", Position " + pos;
        }

        private void rtb_SelectionChanged(object sender, System.EventArgs e)
        {
            int start = rtb.SelectionStart;
            int end = rtb.SelectionEnd;
            int length = rtb.SelectionLength;

            statusBar.Text = "Start " + start + ", End " + end + ", Length " + length;
        }
    }
}
