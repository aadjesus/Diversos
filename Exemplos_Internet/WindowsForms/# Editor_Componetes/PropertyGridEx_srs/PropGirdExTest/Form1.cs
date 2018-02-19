using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PropGirdExTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void AddListItem(object value, string description)
        {
            ListViewItem newItem = new ListViewItem(value.GetType().Name);
            newItem.SubItems.Add(description);
            newItem.Tag = value;
            this.listView1.Items.Add(newItem);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // use en-us for the demo form (german is no good for an english article)
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

            // add some samples
            this.AddListItem(this.propertyGridEx1, "The PropertyGridEx");
            this.AddListItem(DateTime.Now, "DateTime.Now");
            this.AddListItem(AppDomain.CurrentDomain, "The current ApDomain");
            this.AddListItem(System.Reflection.Assembly.GetEntryAssembly(), "The executing Assembly");
            this.AddListItem(System.Threading.Thread.CurrentThread, "The UI Thread");
            this.AddListItem(this.listView1,"This ListView" );
            this.AddListItem(this,"This Form");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // has a site?
            if (this.Site != null)
            {
                // site provides ISelectionService?
                System.ComponentModel.Design.ISelectionService selectionService = this.Site.GetService(typeof(System.ComponentModel.Design.ISelectionService)) as System.ComponentModel.Design.ISelectionService;
                if (selectionService != null)
                {
                    if (this.listView1.SelectedIndices.Count == 1)
                    {
                        // set the current selection the current items tag
                        selectionService.SetSelectedComponents(new object[] { this.listView1.Items[this.listView1.SelectedIndices[0]].Tag });
                    }
                    else
                    {
                        // multi selection is no supported
                        selectionService.SetSelectedComponents(new object[] { null });
                    }
                }
            }
            
            if (this.listView1.SelectedIndices.Count == 1)
                this.propertyGridEx1.SelectedObject = this.listView1.Items[this.listView1.SelectedIndices[0]].Tag;
            else
                this.propertyGridEx1.SelectedObject = 0;
        }
    }
}