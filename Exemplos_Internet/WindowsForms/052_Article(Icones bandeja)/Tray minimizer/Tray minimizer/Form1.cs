using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Security.Permissions;
using System.Security;

namespace Tray_minimizer
{
    public partial class Form1 : Form
    {
        List<window> windows = new List<window>();
        Properties.Settings set = new Properties.Settings();
        bool isinstartup = false;
        About.AboutBox box = new About.AboutBox("Tray minimizer");

        public Form1()
        {
            InitializeComponent();
            winapi.statusbar = winapi.FindWindow("Shell_TrayWnd", "");

            box.programname = "Tray minimizer";
            box.StartPosition = FormStartPosition.CenterScreen;
            box.BackColor = Color.BlanchedAlmond;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case winapi.WM_HOTKEY:
                    ProcessHotkey(m.WParam);
                    break;
            }
            base.WndProc(ref m);
        }

        private void ProcessHotkey(IntPtr wparam)
        {
            if (wparam.ToInt32() == 1729)
            {
                alltray_Click(null, null);
            }

            if (wparam.ToInt32() == 1730)
            {
                showall();
            }
        }

        private void savenewconfig(uint hidemod, uint hidekey, uint showmod, uint showkey,bool ignoretitle)
        {
            set.HideMod = hidemod;
            set.Hidekey = hidekey;

            set.ShowMod = showmod;
            set.Showkey = showkey;

            set.IgnoreTitle = ignoretitle;

            set.Save();
            set.Reload();
        }

        private void Tray_MouseDoubleClick(object sender, EventArgs e)
        {
            Options opt = new Options();
            opt.Icon = Properties.Resources.icon;

            opt.Hidemod = set.HideMod;
            opt.Hidekey = set.Hidekey;
            opt.Showmod = set.ShowMod;
            opt.Showkey = set.Showkey;

            opt.Startup = isinstartup;
            opt.Ignoretitle = set.IgnoreTitle;

            this.Tray.MouseDoubleClick -= new MouseEventHandler(Tray_MouseDoubleClick);
            if (opt.ShowDialog() == DialogResult.OK)
            {
                uint mod = set.HideMod;
                uint key = set.Hidekey;

                if (mod != opt.Hidemod || key != opt.Hidekey)
                {
                    if (mod > 0 && key > 0)
                    {
                        winapi.UnregisterHotKey(this.Handle, 1729);
                    }

                    if (opt.Hidemod > 0 && opt.Hidekey > 0)
                    {
                        winapi.RegisterHotKey(this.Handle, 1729, opt.Hidemod, 64 + opt.Hidekey);
                    }
                }

                mod = set.ShowMod;
                key = set.Showkey;

                if (mod != opt.Showmod || key != opt.Showkey)
                {
                    if (mod > 0 && key > 0)
                    {
                        winapi.UnregisterHotKey(this.Handle, 1730);
                    }

                    if (opt.Showmod > 0 && opt.Showkey > 0)
                    {
                        winapi.RegisterHotKey(this.Handle, 1730, opt.Showmod, 64 + opt.Showkey);
                    }
                }

                savenewconfig(opt.Hidemod, opt.Hidekey, opt.Showmod, opt.Showkey,opt.Ignoretitle);

                if (opt.Startup != isinstartup)
                {
                    startup(opt.Startup);
                }
            }

            this.Tray.MouseDoubleClick += new MouseEventHandler(Tray_MouseDoubleClick);

            opt.Dispose();
        }

        private void programclick(object sender, EventArgs e)
        {
            processwindow(((ToolStripMenuItem)sender).Tag as window);

            ClearItems();
        }

        private void AppContextMenu_Opening(object sender, CancelEventArgs e)
        {
            getwindows();
            Separator.Visible = windows.Count - 1 > 0;
            for (int i = 0; i < windows.Count - 1; i++)
            {
                ToolStripMenuItem temp = new ToolStripMenuItem(windows[i].title, null, programclick);
                temp.Tag = windows[i];
                AppContextMenu.Items.Insert(0, temp);
            }
        }

        private void AppContextMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (e.CloseReason != ToolStripDropDownCloseReason.ItemClicked)
            {
                ClearItems();
            }
        }

        private void ClearItems()
        {
            int count = AppContextMenu.Items.Count;
            for (int i = 0; i < count - 4; i++)
            {
                AppContextMenu.Items[0].Click -= new EventHandler(programclick);
                AppContextMenu.Items.RemoveAt(0);
            }
            windows.Clear();
        }

        private void getwindows()
        {
            winapi.EnumWindowsProc callback = new winapi.EnumWindowsProc(enumwindows);
            winapi.EnumWindows(callback, 0);
        }

        private bool enumwindows(IntPtr hWnd, int lParam)
        {
            if (!winapi.IsWindowVisible(hWnd))
                return true;

            StringBuilder title = new StringBuilder(256);
            winapi.GetWindowText(hWnd, title, 256);

            if (string.IsNullOrEmpty(title.ToString())&& set.IgnoreTitle)
            {
                return true;
            }

            if (title.Length != 0 || (title.Length == 0 & hWnd != winapi.statusbar))
            {
                windows.Add(new window(hWnd, title.ToString(), winapi.IsIconic(hWnd), winapi.IsZoomed(hWnd)));
            }

            return true;
        }

        private string pathfromhwnd(IntPtr hwnd)
        {
            uint dwProcessId;
            winapi.GetWindowThreadProcessId(hwnd, out dwProcessId);
            IntPtr hProcess = winapi.OpenProcess(winapi.ProcessAccessFlags.VMRead | winapi.ProcessAccessFlags.QueryInformation, false, dwProcessId);
            StringBuilder path = new StringBuilder(1024);
            winapi.GetModuleFileNameEx(hProcess, IntPtr.Zero, path, 1024);
            winapi.CloseHandle(hProcess);
            return path.ToString();
        }

        private Icon Iconfrompath(string path)
        {
            System.Drawing.Icon icon = null;

            if (System.IO.File.Exists(path))
            {
                winapi.SHFILEINFO info = new winapi.SHFILEINFO();
                winapi.SHGetFileInfo(path, 0, ref info, (uint)Marshal.SizeOf(info), winapi.SHGFI_ICON | winapi.SHGFI_SMALLICON);

                System.Drawing.Icon temp = System.Drawing.Icon.FromHandle(info.hIcon);
                icon = (System.Drawing.Icon)temp.Clone();
                winapi.DestroyIcon(temp.Handle);
            }

            return icon;
        }

        private void showwindow(window wnd, bool hide)
        {
            winapi.ShowWindow(wnd.handle, state(wnd, hide));
        }

        private int state(window wd, bool hide)
        {
            if (hide)
            {
                return winapi.SW_HIDE;
            }

            if (wd.isminimzed)
            {
                return winapi.SW_MINIMIZE;
            }

            if (wd.ismaximized)
            {
                return winapi.SW_MAXIMIZE;
            }
            return winapi.SW_SHOW;
        }

        private void processwindow(window wnd)
        {
            string path = pathfromhwnd(wnd.handle);
            System.Drawing.Icon icon = Iconfrompath(path);

            NotifyIcon tray = new NotifyIcon(this.components);
            tray.Icon = icon == null ? Properties.Resources.exeicon : icon;
            tray.Visible = true;
            tray.Tag = wnd;
            tray.Text = wnd.title.Length > 64 ? wnd.title.Substring(0, 63) : wnd.title;
            tray.Click += new EventHandler(tray_Click);

            showwindow(wnd, true);
        }

        private void showall()
        {
            int count = this.components.Components.Count;
            for (int i = 2; i < count; i++)
            {
                int index = this.components.Components.Count;
                if (this.components.Components[index - 1] is NotifyIcon)
                {
                    NotifyIcon temp = this.components.Components[index - 1] as NotifyIcon;
                    if (temp.Tag != null)
                    {
                        tray_Click(temp, null);
                    }
                }
            }
        }

        [RegistryPermissionAttribute(SecurityAction.LinkDemand, Write = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run")]
        private void startup(bool add)
        {
            isinstartup = add;
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            if (add)
            {
                key.SetValue("Tray minimizer", "\"" + Application.ExecutablePath + "\"");
            }
            else
                key.DeleteValue("Tray minimizer");

            key.Close();
        }

        private bool isstartup()
        {
            bool result = false;
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            result = key.GetValue("Tray minimizer") != null;
            key.Close();
            return result;
        }

        void tray_Click(object sender, EventArgs e)
        {
            NotifyIcon tray = sender as NotifyIcon;
            window wnd = tray.Tag as window;
            if (winapi.IsWindow(wnd.handle))
            {
                showwindow(wnd, false);
            }
            else
                MessageBox.Show("Window does not exist");
            tray.Click -= new EventHandler(tray_Click);
            tray.Dispose();
        }

        private void Exititem_Click(object sender, EventArgs e)
        {
            showall();
            winapi.UnregisterHotKey(this.Handle, 1729);
            winapi.UnregisterHotKey(this.Handle, 1730);
            Application.Exit();
        }

        private void all_Click(object sender, EventArgs e)
        {
            showall();
        }

        private void alltray_Click(object sender, EventArgs e)
        {
            getwindows();

            for (int i = 0; i < windows.Count - 1; i++)
            {
                processwindow(windows[i]);
            }

            windows.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            isinstartup = isstartup();

            this.Visible = false;
            this.Hide();

            if (!System.IO.File.Exists(Application.StartupPath+"\\Tray minimizer.exe.config"))
            {
                MessageBox.Show("Configuration file not found.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return;
            }
            System.IO.Directory.SetCurrentDirectory(Application.StartupPath);
            uint mod = set.HideMod;
            uint key = set.Hidekey;

            if (mod > 0 && key > 0)
            {
                winapi.RegisterHotKey(this.Handle, 1729, mod, 64 + key);
            }

            mod = set.ShowMod;
            key = set.Showkey;

            if (mod > 0 && key > 0)
            {
                winapi.RegisterHotKey(this.Handle, 1730, mod, 64 + key);
            }

            Tray.ShowBalloonTip(5);
        }

        private void Abouttoolstrip_Click(object sender, EventArgs e)
        {
            ClearItems();
            if (!box.Visible)
            {
                box.ShowDialog();
            }
        }
    }
}