using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Khendys.Controls;

namespace IMWindow
{

    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class IMWindow : System.Windows.Forms.Form
    {

        private Khendys.Controls.ExRichTextBox rtbox_MessageHistory;
        private Khendys.Controls.ExRichTextBox rtbox_SendMessage;
        private System.Windows.Forms.Label lbl_MessageHistory;
        private System.Windows.Forms.Panel pnl_SendMessage;
        private System.Windows.Forms.ToolBar tbar_SendMessage;
        private System.Windows.Forms.Button btn_SendMessage;
        private System.Windows.Forms.Panel pnl_MessageHistory;
        private System.Windows.Forms.ToolBarButton tbBtn_Font;
        private System.Windows.Forms.ToolBarButton tbBtn_Emoticons;
        private System.Windows.Forms.FontDialog fdlg_SendMessage;
        private System.Windows.Forms.ContextMenu cmenu_Emoticons;
        private System.Windows.Forms.CheckBox chBox_ShowRtf;
        private Image[] emoticons;
        private RtfCodes frm_RtfCodes;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public IMWindow()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            // Position the window
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point((SystemInformation.PrimaryMonitorMaximizedWindowSize.Width - this.DesktopBounds.Width) / 2,
                (SystemInformation.PrimaryMonitorMaximizedWindowSize.Height - this.DesktopBounds.Height) / 2);

            // Remove DropDownArrow from ToolBar
            tbar_SendMessage.DropDownArrows = false;

            // Load Emoticon Images
            emoticons = new Image[9];
            emoticons[0] = new Bitmap(typeof(IMWindow), "Emoticons.AngelSmile.png");
            emoticons[1] = new Bitmap(typeof(IMWindow), "Emoticons.AngrySmile.png");
            emoticons[2] = new Bitmap(typeof(IMWindow), "Emoticons.Beer.png");
            emoticons[3] = new Bitmap(typeof(IMWindow), "Emoticons.BrokenHeart.png");
            emoticons[4] = new Bitmap(typeof(IMWindow), "Emoticons.ConfusedSmile.png");
            emoticons[5] = new Bitmap(typeof(IMWindow), "Emoticons.CrySmile.png");
            emoticons[6] = new Bitmap(typeof(IMWindow), "Emoticons.DevilSmile.png");
            emoticons[7] = new Bitmap(typeof(IMWindow), "Emoticons.EmbarassedSmile.png");
            emoticons[8] = new Bitmap(typeof(IMWindow), "Emoticons.ThumbsUp.png");

            // Create Emoticon DropDownMenu
            EmoticonMenuItem _menuItem;
            int _count = 0;
            foreach (Image _emoticon in emoticons)
            {
                _menuItem = new EmoticonMenuItem(_emoticon);
                _menuItem.Click += new EventHandler(cmenu_Emoticons_Click);
                if (_count % 3 == 0)
                    _menuItem.BarBreak = true;

                cmenu_Emoticons.MenuItems.Add(_menuItem);
                ++_count;
            }
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
            this.rtbox_MessageHistory = new Khendys.Controls.ExRichTextBox();
            this.rtbox_SendMessage = new Khendys.Controls.ExRichTextBox();
            this.lbl_MessageHistory = new System.Windows.Forms.Label();
            this.pnl_SendMessage = new System.Windows.Forms.Panel();
            this.chBox_ShowRtf = new System.Windows.Forms.CheckBox();
            this.btn_SendMessage = new System.Windows.Forms.Button();
            this.tbar_SendMessage = new System.Windows.Forms.ToolBar();
            this.tbBtn_Font = new System.Windows.Forms.ToolBarButton();
            this.tbBtn_Emoticons = new System.Windows.Forms.ToolBarButton();
            this.cmenu_Emoticons = new System.Windows.Forms.ContextMenu();
            this.pnl_MessageHistory = new System.Windows.Forms.Panel();
            this.fdlg_SendMessage = new System.Windows.Forms.FontDialog();
            this.pnl_SendMessage.SuspendLayout();
            this.pnl_MessageHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbox_MessageHistory
            // 
            this.rtbox_MessageHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbox_MessageHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbox_MessageHistory.HiglightColor = Khendys.Controls.RtfColor.White;
            this.rtbox_MessageHistory.Location = new System.Drawing.Point(0, 23);
            this.rtbox_MessageHistory.Name = "rtbox_MessageHistory";
            this.rtbox_MessageHistory.ReadOnly = true;
            this.rtbox_MessageHistory.Size = new System.Drawing.Size(512, 158);
            this.rtbox_MessageHistory.TabIndex = 0;
            this.rtbox_MessageHistory.Text = "";
            this.rtbox_MessageHistory.TextColor = Khendys.Controls.RtfColor.Black;
            // 
            // rtbox_SendMessage
            // 
            this.rtbox_SendMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbox_SendMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbox_SendMessage.HiglightColor = Khendys.Controls.RtfColor.White;
            this.rtbox_SendMessage.Location = new System.Drawing.Point(0, 50);
            this.rtbox_SendMessage.Name = "rtbox_SendMessage";
            this.rtbox_SendMessage.Size = new System.Drawing.Size(424, 78);
            this.rtbox_SendMessage.TabIndex = 1;
            this.rtbox_SendMessage.Text = "";
            this.rtbox_SendMessage.TextColor = Khendys.Controls.RtfColor.Black;
            // 
            // lbl_MessageHistory
            // 
            this.lbl_MessageHistory.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_MessageHistory.Location = new System.Drawing.Point(0, 0);
            this.lbl_MessageHistory.Name = "lbl_MessageHistory";
            this.lbl_MessageHistory.Size = new System.Drawing.Size(512, 23);
            this.lbl_MessageHistory.TabIndex = 2;
            this.lbl_MessageHistory.Text = "Chat History";
            // 
            // pnl_SendMessage
            // 
            this.pnl_SendMessage.Controls.Add(this.chBox_ShowRtf);
            this.pnl_SendMessage.Controls.Add(this.rtbox_SendMessage);
            this.pnl_SendMessage.Controls.Add(this.btn_SendMessage);
            this.pnl_SendMessage.Controls.Add(this.tbar_SendMessage);
            this.pnl_SendMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_SendMessage.Location = new System.Drawing.Point(0, 181);
            this.pnl_SendMessage.Name = "pnl_SendMessage";
            this.pnl_SendMessage.Size = new System.Drawing.Size(512, 128);
            this.pnl_SendMessage.TabIndex = 3;
            // 
            // chBox_ShowRtf
            // 
            this.chBox_ShowRtf.Checked = true;
            this.chBox_ShowRtf.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chBox_ShowRtf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chBox_ShowRtf.Location = new System.Drawing.Point(192, 8);
            this.chBox_ShowRtf.Name = "chBox_ShowRtf";
            this.chBox_ShowRtf.Size = new System.Drawing.Size(136, 24);
            this.chBox_ShowRtf.TabIndex = 4;
            this.chBox_ShowRtf.Text = "Show Rtf Codes";
            this.chBox_ShowRtf.CheckedChanged += new System.EventHandler(this.chBox_ShowRtf_CheckedChanged);
            // 
            // btn_SendMessage
            // 
            this.btn_SendMessage.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_SendMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SendMessage.Location = new System.Drawing.Point(424, 50);
            this.btn_SendMessage.Name = "btn_SendMessage";
            this.btn_SendMessage.Size = new System.Drawing.Size(88, 78);
            this.btn_SendMessage.TabIndex = 3;
            this.btn_SendMessage.Text = "Send";
            this.btn_SendMessage.Click += new System.EventHandler(this.btn_SendMessage_Click);
            // 
            // tbar_SendMessage
            // 
            this.tbar_SendMessage.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbBtn_Font,
            this.tbBtn_Emoticons});
            this.tbar_SendMessage.DropDownArrows = true;
            this.tbar_SendMessage.Location = new System.Drawing.Point(0, 0);
            this.tbar_SendMessage.Name = "tbar_SendMessage";
            this.tbar_SendMessage.ShowToolTips = true;
            this.tbar_SendMessage.Size = new System.Drawing.Size(512, 50);
            this.tbar_SendMessage.TabIndex = 2;
            this.tbar_SendMessage.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbar_SendMessage_ButtonClick);
            // 
            // tbBtn_Font
            // 
            this.tbBtn_Font.Name = "tbBtn_Font";
            this.tbBtn_Font.Text = "Font";
            this.tbBtn_Font.ToolTipText = "Change the font for messages you send";
            // 
            // tbBtn_Emoticons
            // 
            this.tbBtn_Emoticons.DropDownMenu = this.cmenu_Emoticons;
            this.tbBtn_Emoticons.Name = "tbBtn_Emoticons";
            this.tbBtn_Emoticons.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.tbBtn_Emoticons.Text = "Emoticons";
            this.tbBtn_Emoticons.ToolTipText = "Insert an emoticon";
            // 
            // pnl_MessageHistory
            // 
            this.pnl_MessageHistory.Controls.Add(this.rtbox_MessageHistory);
            this.pnl_MessageHistory.Controls.Add(this.lbl_MessageHistory);
            this.pnl_MessageHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_MessageHistory.Location = new System.Drawing.Point(0, 0);
            this.pnl_MessageHistory.Name = "pnl_MessageHistory";
            this.pnl_MessageHistory.Size = new System.Drawing.Size(512, 181);
            this.pnl_MessageHistory.TabIndex = 4;
            // 
            // fdlg_SendMessage
            // 
            this.fdlg_SendMessage.ShowColor = true;
            // 
            // IMWindow
            // 
            this.AcceptButton = this.btn_SendMessage;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(512, 309);
            this.Controls.Add(this.pnl_MessageHistory);
            this.Controls.Add(this.pnl_SendMessage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IMWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "IM Window";
            this.Load += new System.EventHandler(this.IMWindow_Load);
            this.pnl_SendMessage.ResumeLayout(false);
            this.pnl_SendMessage.PerformLayout();
            this.pnl_MessageHistory.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new IMWindow());
        }

        // When an emoticon is clicked, insert its image into to RTF
        private void cmenu_Emoticons_Click(object _sender, EventArgs _args)
        {
            EmoticonMenuItem _item = (EmoticonMenuItem)_sender;
            try
            {
                rtbox_SendMessage.InsertImage(_item.Image);
            }
            catch (Exception _e)
            {
                MessageBox.Show("Rtf Image Insert Error\n\n" + _e.ToString());
            }
        }

        // Show RTF code window if option is checked
        private void chBox_ShowRtf_CheckedChanged(object sender, System.EventArgs e)
        {
            if (chBox_ShowRtf.Checked)
                frm_RtfCodes.Show();
            else
                frm_RtfCodes.Hide();
        }

        private void IMWindow_Load(object sender, System.EventArgs e)
        {
            // Load the window to show the Rtf Codes
            frm_RtfCodes = new RtfCodes(this);
            frm_RtfCodes.Show();
        }

        private void btn_SendMessage_Click(object sender, System.EventArgs e)
        {

            try
            {
                // Add fake message owner using insert
                rtbox_MessageHistory.AppendTextAsRtf("Local User Said\n\n",
                    new Font(this.Font, FontStyle.Underline | FontStyle.Bold), RtfColor.Blue, RtfColor.Yellow);

                // Just to show it's possible, if the text contains a smiley face [:)]
                // insert the smiley image instead. This is not a practical way to do this.
                int _index;
                if ((_index = rtbox_SendMessage.Find(":)")) > -1)
                {
                    rtbox_SendMessage.Select(_index, ":)".Length);
                    rtbox_SendMessage.InsertImage(new Bitmap(typeof(IMWindow), "Emoticons.Beer.png"));
                }

                // Add the message to the history
                rtbox_MessageHistory.AppendRtf(rtbox_SendMessage.Rtf);

                // Add a newline below the added line, just to add spacing
                rtbox_MessageHistory.AppendTextAsRtf("\n");

                // History gets the focus
                rtbox_MessageHistory.Focus();

                // Scroll to bottom so newly added text is seen.
                rtbox_MessageHistory.Select(rtbox_MessageHistory.TextLength, 0);
                rtbox_MessageHistory.ScrollToCaret();

                // Return focus to message text box
                rtbox_SendMessage.Focus();

                // Add the Rtf Codes to the RtfCode Window
                frm_RtfCodes.AppendText(rtbox_SendMessage.Rtf);

                // Clear the SendMessage box.
                rtbox_SendMessage.Text = String.Empty;
            }
            catch (Exception _e)
            {
                MessageBox.Show("An error occured when \"sending\" :\n\n" +
                    _e.Message, "Send Error");
            }
        }

        private void tbar_SendMessage_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            if (e.Button == this.tbBtn_Font)
            {
                if (DialogResult.OK == fdlg_SendMessage.ShowDialog())
                {
                    rtbox_SendMessage.Font = fdlg_SendMessage.Font;
                    rtbox_SendMessage.ForeColor = fdlg_SendMessage.Color;
                }
            }
        }
    }
}