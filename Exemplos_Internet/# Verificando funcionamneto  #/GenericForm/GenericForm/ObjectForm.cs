/******************************************************************************* 
 *  Copyright 2009 http://www.SoapPanda.com.
 *  
 *  Author: Anantjot Anand
 *
 * 
 * */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

namespace GenericForm
{
    public partial class ObjectForm : Form
    {
        public ObjectForm(Assembly assembly, Object obj)
        {
            InitializeComponent();

            this.obj = obj;
            this.assembly = assembly;

            this.Text = string.Format("Instance Members of {0}", obj.GetType().FullName);

        }

        public ObjectForm(Assembly assembly, Object obj, Type arrType)
        {
            InitializeComponent();
            this.obj = obj;
            this.assembly = assembly;
            this.arrType = arrType;
            this.Text = string.Format("Set Array of values of type {0}", arrType.FullName);

        }
        private Type arrType;
        private Object obj;
        private Assembly assembly;
        private FlowLayoutPanel flowLayoutPanel1;
        private ObjectPanel objPanel;
        private Panel botPanel;
        private Button btnOK;
        private Button btnCancel;

        ComplexTypeArray cmplxArray;
        ComplexType cmplx;

        private void ObjectForm_Load(object sender, EventArgs e)
        {            
            objPanel = new ObjectPanel(obj);
            //objPanel.AutoScroll = true;
            objPanel.AutoSize = true;
            objPanel.Location = new System.Drawing.Point(0, 0);
            objPanel.Name = "topPanel";
            if (this.obj.GetType()==typeof(ArrayList))
            {
                cmplxArray = new ComplexTypeArray(assembly, (ArrayList)obj, this.arrType, objPanel,false);
                objPanel.Controls.Add(cmplxArray);
            }
            else
            {
                cmplx = new ComplexType(assembly, obj, objPanel, false);
                objPanel.Controls.Add(cmplx);
            }

            botPanel = new Panel();
            botPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            botPanel.Name = "botPanel";

            btnOK = new Button();
            btnOK.Name = "btnOK";
            btnOK.Text = "Ok";
            btnOK.Click += new EventHandler(btnOK_Click);
            btnCancel = new Button();
            btnCancel.Name = "btnCancel";
            btnCancel.Text = "Cancel";
            btnCancel.Click += new EventHandler(btnCancel_Click);
            botPanel.Controls.Add(btnOK);
            botPanel.Controls.Add(btnCancel);


            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Name = "fPanel1";
            flowLayoutPanel1.Controls.Add(objPanel);

            this.Controls.Add(flowLayoutPanel1);
            this.Controls.Add(botPanel);

            //resize
            ObjectForm_Resize(sender, e);
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            if (obj != null)
            {
                if (obj.GetType() == typeof(ArrayList))
                {
                    cmplxArray.SetAll();
                }
                else
                {
                    cmplx.SetAll(obj);
                }
            }
            Close();
        }

        private void ObjectForm_Resize(object sender, EventArgs e)
        {
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Size = new Size(this.Width-10, this.Height - 90);

            botPanel.Location = new Point(3, this.Height - 70);
            botPanel.Size = new Size(this.Width, 50);

            btnOK.Location = new System.Drawing.Point(this.Width / 2 - 200, 15);
            btnCancel.Location = new System.Drawing.Point(this.Width / 2 + 50, 15);

        }
    }
}
