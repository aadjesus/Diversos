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
    public partial class DemoForm : Form
    {
        private string assemblyPath;
        private Assembly assembly;

        private Object obj;
        private ArrayList objArray;
        private Type arrType;

        public DemoForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1;
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.InitialDirectory = Environment.SpecialFolder.Recent.ToString();
            openFileDialog1.FileName = null;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.assemblyPath = openFileDialog1.FileName;
                this.textBox1.Text = openFileDialog1.FileName;
                try
                {
                    this.assembly = Assembly.LoadFrom(this.assemblyPath);

                    Module[] modules = assembly.GetLoadedModules();

                    foreach (Module module in modules)
                    {
                       this.listBox1.Items.Clear();
                       this.listBox1.BeginUpdate();
                       Type[] moduleTypes = module.GetTypes();
                       foreach (Type moduleType in moduleTypes)
                       {
                           this.listBox1.Items.Add(moduleType.FullName);
                       }
                       this.listBox1.EndUpdate();
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
           
        }

        private void crtObject_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem == null) return;

            try
            {
                this.obj = assembly.CreateInstance(this.listBox1.SelectedItem.ToString());
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            ObjectForm objForm = new ObjectForm(this.assembly, this.obj);

            objForm.ShowDialog();
        }

        private void edtObject_Click(object sender, EventArgs e)
        {
            if (this.obj == null) return;

            ObjectForm objForm = new ObjectForm(this.assembly, this.obj);

            objForm.ShowDialog();
        }

        private void crtArrayObject_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem == null) return;

            this.objArray = new ArrayList();
            this.arrType = assembly.GetType(this.listBox1.SelectedItem.ToString());

            if (this.arrType == null) return;

            ObjectForm objForm = new ObjectForm(this.assembly, this.objArray,this.arrType);

            objForm.ShowDialog();
        }

        private void edtArrayObject_Click(object sender, EventArgs e)
        {
            if (this.arrType == null || this.objArray==null) return;

            ObjectForm objForm = new ObjectForm(this.assembly, this.objArray, this.arrType);

            objForm.ShowDialog();

        }
    }
}
