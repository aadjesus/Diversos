using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SplashScreen
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private bool m_bLayoutCalled = false;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.IContainer components;
        private DateTime m_dt;

        public Form1()
        {
            InitializeComponent();
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            // 
            // timer1
            // 
            this.timer1.Interval = 100;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(440, 380);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Form1_Layout);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SplashScreen.ShowSplashScreen();
            Application.DoEvents();
            SplashScreen.SetStatus("Loading module 1");
            System.Threading.Thread.Sleep(500);
            SplashScreen.SetStatus("Loading module 2");
            System.Threading.Thread.Sleep(300);
            SplashScreen.SetStatus("Loading module 3");
            System.Threading.Thread.Sleep(900);
            SplashScreen.SetStatus("Loading module 4");
            System.Threading.Thread.Sleep(100);
            SplashScreen.SetStatus("Loading module 5");
            System.Threading.Thread.Sleep(400);
            SplashScreen.SetStatus("Loading module 6");
            System.Threading.Thread.Sleep(50);
            SplashScreen.SetStatus("Loading module 7");
            System.Threading.Thread.Sleep(240);
            SplashScreen.SetStatus("Loading module 8");
            System.Threading.Thread.Sleep(900);
            SplashScreen.SetStatus("Loading module 9");
            System.Threading.Thread.Sleep(240);
            SplashScreen.SetStatus("Loading module 10");
            System.Threading.Thread.Sleep(90);
            SplashScreen.SetStatus("Loading module 11");
            System.Threading.Thread.Sleep(1000);
            SplashScreen.SetStatus("Loading module 12");
            System.Threading.Thread.Sleep(100);
            SplashScreen.SetStatus("Loading module 13");
            System.Threading.Thread.Sleep(500);
            SplashScreen.SetStatus("Loading module 14", false);
            System.Threading.Thread.Sleep(1000);
            SplashScreen.SetStatus("Loading module 14a", false);
            System.Threading.Thread.Sleep(1000);
            SplashScreen.SetStatus("Loading module 14b", false);
            System.Threading.Thread.Sleep(1000);
            SplashScreen.SetStatus("Loading module 14c", false);
            System.Threading.Thread.Sleep(1000);
            SplashScreen.SetStatus("Loading module 15");
            System.Threading.Thread.Sleep(20);
            SplashScreen.SetStatus("Loading module 16");
            System.Threading.Thread.Sleep(450);
            SplashScreen.SetStatus("Loading module 17");
            System.Threading.Thread.Sleep(240);
            SplashScreen.SetStatus("Loading module 18");
            System.Threading.Thread.Sleep(90);
            Application.Run(new Form1());
        }

        private void Form1_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
        {
            try
            {
                if (m_bLayoutCalled == false)
                {
                    m_bLayoutCalled = true;
                    m_dt = DateTime.Now;
                    if (SplashScreen.SplashForm != null)
                        SplashScreen.SplashForm.Owner = this;
                    this.Activate();
                    SplashScreen.CloseForm();
                    timer1.Start();
                }
            }
            catch (Exception)
            {
            }

        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            TimeSpan ts = DateTime.Now.Subtract(m_dt);
            if (ts.TotalSeconds > 2)
                this.Close();
        }
    }
}
