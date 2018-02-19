using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataSynchronizer;
using GenericEntities;
using GoogleAdapter;
using System.Collections;
namespace TestApplication
{
    public partial class Form1 : Form
    {
        SyncService service = new SyncService();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            IList  list = service.SynchronizeEvents();
            dataGridView1.DataSource = list;
            //dataGridView1.DataBindings;
           
        }
    }

    
}
