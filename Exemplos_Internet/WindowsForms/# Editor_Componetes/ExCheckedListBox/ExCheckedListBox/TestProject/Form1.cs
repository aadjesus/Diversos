using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace TestProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'testDataSet.Cities' table. You can move, or remove it, as needed.
            this.citiesTableAdapter.Fill(this.testDataSet.Cities);
            // TODO: This line of code loads data into the 'testDataSet.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.testDataSet.Products);

           
        }

       

        private void productsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.productsBindingSource.EndEdit();
            this.productsTableAdapter.Update(this.testDataSet.Products);

        }


        
    }
}