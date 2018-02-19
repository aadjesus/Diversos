using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace MissinScrollBar {
	public partial class Form1 : DevExpress.XtraEditors.XtraForm {

		private DevExpress.XtraEditors.GroupControl groupControlList;
		private System.Windows.Forms.NotifyIcon m_notifyIcon;
		private System.Windows.Forms.ContextMenu m_contextMenu;
		private System.Windows.Forms.MenuItem m_menuItemExit;
		private System.Windows.Forms.MenuItem m_menuItemShow;
		private System.ComponentModel.IContainer m_components;
		bool m_bReallyClose = false;
		private bool m_bHide = true;
		private bool m_bRestoreToMMaximized = false;



		public Form1() {
			InitializeComponent();
			CreateGroup();

			this.m_components = new System.ComponentModel.Container();

		


			m_menuItemExit = new System.Windows.Forms.MenuItem();
			m_menuItemExit.Index = 0;
			m_menuItemExit.Text = "E&xit";
			m_menuItemExit.Click += new System.EventHandler(this.OnMenuExit);

			m_menuItemShow = new System.Windows.Forms.MenuItem();
			m_menuItemShow.Index = 0;
			m_menuItemShow.Text = "&Show";
			m_menuItemShow.Click += new System.EventHandler(this.OnMenuShow);

		
			this.m_contextMenu = new System.Windows.Forms.ContextMenu();
			this.m_contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { 
				this.m_menuItemShow,				
				this.m_menuItemExit,
			});

			m_notifyIcon = new System.Windows.Forms.NotifyIcon(this.m_components);
			m_notifyIcon.Icon = this.Icon;
			m_notifyIcon.ContextMenu = this.m_contextMenu;
			m_notifyIcon.Text = "Missing Scrollbar";
			m_notifyIcon.Visible = true;
			m_notifyIcon.DoubleClick += new System.EventHandler(this.OnMenuShow);
			//m_notifyIcon.Click += new EventHandler(m_notifyIcon_Click);
		}

		

		public void CreateGroup() {

			if (groupControlList == null) {
				groupControlList = new DevExpress.XtraEditors.GroupControl();
				groupControlList.Size = new System.Drawing.Size(160, 140);
				groupControlList.Location = new System.Drawing.Point(10, 10);
				groupControlList.ShowCaption = true;
				groupControlList.Text = "Other Devices";
				groupControlList.CaptionLocation = DevExpress.Utils.Locations.Bottom;
				groupControlList.TabIndex = 4;
				groupControlList.Name = "groupControlList";
				//groupControlList.Hide();

			}

			ScrollableBottom.Controls.Add(groupControlList);

			Size peSize = this.pictureEdit1.Size;
			int StartX = 10;
			int StartY = 5;

			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));

			for (int i = 0; i < 15; i++) {

				DevExpress.XtraEditors.PictureEdit pe = new DevExpress.XtraEditors.PictureEdit();
				pe.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
				pe.Location = new System.Drawing.Point(StartX + (peSize.Width*i),StartY);
				pe.Name = "pe" + i.ToString();
				pe.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
				pe.Size = peSize;
				pe.TabIndex = i+3;

				groupControlList.Controls.Add(pe);
				Size groupSize = new Size();
				groupSize.Height = pe.Size.Height + StartY * 2;
				groupSize.Width = (peSize.Width * (i+1)) + StartX+2;
				groupControlList.Size = groupSize;
			}
		}

		public void OnMenuShow(Object sender, EventArgs e) {
			if (m_bHide) {
				OnHide();
			}
			else {
				this.ShowInTaskbar = true;
				if (m_bRestoreToMMaximized) {
					this.WindowState = FormWindowState.Maximized;
					m_bRestoreToMMaximized = false;
				}
				else if (this.WindowState == FormWindowState.Minimized) {
					this.WindowState = FormWindowState.Normal;
				}

				this.Activate();
				this.Show();
			}
		}
		
		public void OnMenuExit(Object sender, EventArgs e) {
			m_bReallyClose = true;
			this.Close();
		}

		private void setShowMenuText() {
			if (m_menuItemShow != null) {
				if (!this.Visible || (this.WindowState == FormWindowState.Minimized)) {
					m_menuItemShow.Text = "Show";
					m_bHide = false;
				}
				else {
					m_menuItemShow.Text = "Hide";
					m_bHide = true;
				}
			}
		}

		private void OnFormClosing(object sender, FormClosingEventArgs e) {
			if (!m_bReallyClose) {
				OnHide();
				e.Cancel = true;
				return;
			}


			m_notifyIcon.Visible = false;
			m_notifyIcon.Dispose();
		}

		private void OnVisableChanged(object sender, EventArgs e) {
			setShowMenuText();
		}

		private void OnSizeChanged(object sender, EventArgs e) {
			setShowMenuText();
		}

		private void OnHide() {
			this.Hide();
			if (this.WindowState == FormWindowState.Maximized) {
				this.WindowState = FormWindowState.Normal;
				m_bRestoreToMMaximized = true;
			}
			this.ShowInTaskbar = false;
		}
	}
}