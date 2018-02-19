using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        public class MyDateNavigator : DevExpress.XtraScheduler.DateNavigator
        {
             

            protected override DevExpress.XtraEditors.Drawing.DateEditPainter CreatePainter()
            {
                this.BackColor = Color.Red;
                
                //return new MyPainter(this);
                return base.CreatePainter();
            }

            

        }

        class MyPainter : DevExpress.XtraEditors.Drawing.DateEditPainter
        {
            public MyPainter(DevExpress.XtraScheduler.DateNavigator nav) : base(nav) { }

            protected override void DrawHeader(DevExpress.XtraEditors.Calendar.CalendarObjectInfoArgs info)
            {
               

                base.DrawHeader(info);
                //base.DrawHeader(info);
            }
        }
    }
}