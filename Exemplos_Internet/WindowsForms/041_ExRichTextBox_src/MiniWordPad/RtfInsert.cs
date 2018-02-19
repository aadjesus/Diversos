using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Khendys.Controls;

namespace RtfInsert
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class RtfInsert : System.Windows.Forms.Form
    {
        private Khendys.Controls.ExRichTextBox rtBox_Main;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.Panel panel1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public RtfInsert()
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
            this.rtBox_Main = new Khendys.Controls.ExRichTextBox();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtBox_Main
            // 
            this.rtBox_Main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtBox_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtBox_Main.HiglightColor = Khendys.Controls.RtfColor.White;
            this.rtBox_Main.Location = new System.Drawing.Point(12, 12);
            this.rtBox_Main.Name = "rtBox_Main";
            this.rtBox_Main.Size = new System.Drawing.Size(704, 369);
            this.rtBox_Main.TabIndex = 0;
            this.rtBox_Main.Text = "";
            this.rtBox_Main.TextColor = Khendys.Controls.RtfColor.Black;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3});
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem8,
																					  this.menuItem9});
            this.menuItem2.Text = "Insert";
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 0;
            this.menuItem8.Text = "Image";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 1;
            this.menuItem9.Text = "Plain Text";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem10});
            this.menuItem3.Text = "About";
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 0;
            this.menuItem10.Text = "ExRichTextBox";
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.rtBox_Main});
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.DockPadding.All = 12;
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(728, 393);
            this.panel1.TabIndex = 1;
            // 
            // RtfInsert
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(728, 393);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel1});
            this.Menu = this.mainMenu1;
            this.Name = "RtfInsert";
            this.Text = "RTF Images";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new RtfInsert());
        }

        /// <summary>
        /// InsertImage_Click
        /// 
        /// Allow the user to insert image files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem8_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog _dialog = new OpenFileDialog();
            _dialog.Filter = "All Image Files|*.bmp;*.ico;*.gif;*.jpeg;*.jpg;*.png;*.tif;*.tiff|" +
                             "Windows Bitmap(*.bmp)|*.bmp|" +
                             "Windows Icon(*.ico)|*.ico|" +
                             "Graphics Interchange Format (*.gif)|(*.gif)|" +
                             "JPEG File Interchange Format (*.jpg)|*.jpg;*.jpeg|" +
                             "Portable Network Graphics (*.png)|*.png|" +
                             "Tag Image File Format (*.tif)|*.tif;*.tiff";
            if (DialogResult.OK == _dialog.ShowDialog(this))
            {
                try
                {
                    // If file is an icon
                    if (_dialog.FileName.ToUpper().EndsWith(".ICO"))
                    {
                        // Create a new icon, get it's handle, and create a bitmap from
                        // its handle
                        rtBox_Main.InsertImage(Bitmap.FromHicon((new Icon(_dialog.FileName)).Handle));
                    }
                    else
                    {
                        // Create a bitmap from the filename
                        rtBox_Main.InsertImage(Image.FromFile(_dialog.FileName));
                    }
                }
                catch (Exception _e)
                {
                    MessageBox.Show("The file could not be opened:\n\n" + _e.Message, "File Open Error");
                }
            }
        }

        /// <summary>
        /// InsertPlainText_Click 
        /// 
        /// Inserts the text the user enters in the dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem9_Click(object sender, System.EventArgs e)
        {

            PlainTextDialog _dialog = new PlainTextDialog();

            if (DialogResult.OK == _dialog.ShowDialog())
            {

                // Insert text with a little formatting
                rtBox_Main.InsertTextAsRtf(_dialog.PlainText, new Font(this.Font, FontStyle.Bold | FontStyle.Underline),
                    RtfColor.Blue, RtfColor.Yellow);
            }
        }

        /// <summary>
        /// About_Click
        /// 
        /// Print some text and images
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem10_Click(object sender, System.EventArgs e)
        {
            string[] _about = new string[10];
            Font[] _fonts = new Font[3];

            rtBox_Main.Rtf = "";

            _fonts[0] = new Font(FontFamily.GenericSerif, 16f);
            _fonts[1] = new Font(FontFamily.GenericSansSerif, 12f);
            _fonts[2] = new Font(FontFamily.GenericMonospace, 8f);

            _about[0] = "This text and images are ";
            _about[1] = "being inserted on the\n";
            _about[2] = "Click ";
            _about[3] = "event of this menu item.\n\n";
            _about[4] = "Sorry I cleared the document, ";
            _about[5] = " but just click ";
            _about[6] = "undo ";
            _about[7] = "and you'll be ok. ";
            _about[8] = "\n\nHa Ha!  No undo.\n\n";
            _about[9] = "Khendys was here ...";

            rtBox_Main.InsertTextAsRtf(_about[0], _fonts[0]);
            rtBox_Main.InsertTextAsRtf(_about[1], new Font(_fonts[1], FontStyle.Strikeout));
            rtBox_Main.InsertTextAsRtf(_about[2], new Font(_fonts[0], FontStyle.Bold | FontStyle.Italic), RtfColor.Olive, RtfColor.Aqua);
            rtBox_Main.InsertTextAsRtf(_about[3], new Font(_fonts[1], FontStyle.Italic), RtfColor.Gray);
            rtBox_Main.InsertTextAsRtf(_about[4], new Font(_fonts[2], FontStyle.Bold), RtfColor.Red, RtfColor.Yellow);
            rtBox_Main.InsertImage(new Bitmap(typeof(RtfInsert), "Smilies.AngrySmile.png"));
            rtBox_Main.InsertTextAsRtf(_about[5], _fonts[0]);
            rtBox_Main.InsertTextAsRtf(_about[6], _fonts[0], RtfColor.Blue, RtfColor.Aqua);
            rtBox_Main.InsertTextAsRtf(_about[7], _fonts[1]);
            rtBox_Main.InsertImage(new Bitmap(typeof(RtfInsert), "Smilies.CrySmile.png"));
            rtBox_Main.InsertTextAsRtf(_about[8], new Font(_fonts[2], FontStyle.Bold | FontStyle.Italic), RtfColor.Red, RtfColor.Aqua);
            rtBox_Main.InsertTextAsRtf(_about[9], new Font(_fonts[1], FontStyle.Italic | FontStyle.Bold | FontStyle.Underline), RtfColor.Black, RtfColor.Red);
            rtBox_Main.InsertImage(new Bitmap(typeof(RtfInsert), "Smilies.Beer.png"));
        }
    }
}
