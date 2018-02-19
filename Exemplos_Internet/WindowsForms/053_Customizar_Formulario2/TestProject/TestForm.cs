using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FormExNS;

namespace TestProject
{
    public partial class TestForm : FormEx
    {
        public TestForm()
        {
            InitializeComponent();
            cmbKeys.DataSource = Enum.GetValues(typeof(Keys));
            cmbKeys.SelectedIndex = 0;
        }

        private void Form2_PaintTitleBar(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Rectangle rec = new Rectangle(70, 5, 150, 20);
            Rectangle circleLeft = new Rectangle(60, 5, 20, 20);
            Rectangle circleRight = new Rectangle(140 + 70, 5, 20, 20);
            System.Drawing.Drawing2D.LinearGradientBrush lgb = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, 0), new Point(350, 0), Color.Red, Color.MediumSpringGreen);

            try
            {
                g.FillRectangle(lgb, rec);
                g.FillEllipse(lgb, circleLeft);
                g.FillEllipse(lgb, circleRight);
                g.DrawString("CCCCCCCCCCCCCCCCC", new Font("Arial", 12, FontStyle.Regular), Brushes.White, new PointF(67, 6));
            }
            catch { }
        }

        private void ToggleButton(Button btn)
        {
            if (btn.BackColor == Color.Green)
                btn.BackColor = Color.Red;
            else
                btn.BackColor = Color.Green;
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            this.DesktopAttached = !this.DesktopAttached;

            ToggleButton(btnAttach);
        }

        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            this.FullScreen = !this.FullScreen;

            ToggleButton(btnFullScreen);
        }

        private void btnMovable_Click(object sender, EventArgs e)
        {
            this.Movable = !this.Movable;

            ToggleButton(btnMovable);
        }

        private void btnSizable_Click(object sender, EventArgs e)
        {
            this.Sizable = !this.Sizable;

            ToggleButton(btnSizable);
        }

        private void btnCloseButton_Click(object sender, EventArgs e)
        {
            this.CloseButton = !this.CloseButton;

            ToggleButton(btnCloseButton);
        }

        private void btnGetKeyState_MouseDown(object sender, MouseEventArgs e)
        {
            if (cmbKeys.SelectedIndex == -1)
                return;

            btnGetKeyState.BackColor = Color.DeepSkyBlue;
            Keys key = (Keys)cmbKeys.SelectedItem;
            txtKeyState.Text = this.GetKeyState(key).ToString();
            txtKeyValue.Text = this.GetKeyValue(key).ToString();
        }

        private void btnGetKeyState_MouseUp(object sender, MouseEventArgs e)
        {
            btnGetKeyState.BackColor = Color.MidnightBlue;
            txtKeyState.Clear();
            txtKeyValue.Clear();
        }

        private void btnGetKeyState_KeyDown(object sender, KeyEventArgs e)
        {
            if (cmbKeys.SelectedIndex == -1)
                return;

            Keys key = (Keys)cmbKeys.SelectedItem;
            txtKeyState.Text = this.GetKeyState(key).ToString();
            txtKeyValue.Text = this.GetKeyValue(key).ToString();
        }

        private void btnGetKeyState_KeyUp(object sender, KeyEventArgs e)
        {
            if (cmbKeys.SelectedIndex == -1)
                return;

            Keys key = (Keys)cmbKeys.SelectedItem;
            txtKeyState.Text = this.GetKeyState(key).ToString();
            txtKeyValue.Text = this.GetKeyValue(key).ToString();
        }
    }
}


