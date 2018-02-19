using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WpfApplication2
{
    public partial class Form1 : Form
    {
        //UserControl1 aaa = new UserControl1();
        public Form1()
        {
            InitializeComponent();

            // elementHost1.Child = aaa;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.MdiParent = this;

            //elementHost1.Container.Add(form);




            form.Show();
            //elementHost1.Container.Add(form);

            //myMdiChildForm.MdiParent = mdiClientPanel1.MdiForm; 


            //this.LayoutMdi(MdiLayout.ArrangeIcons);            
        }

        public void xisto()
        {
            MyClass xx = new MyClass();
            xx.Xisto = "xxxxx";
            foreach (var item in xx.GetType().GetCustomAttributesData())
            {
                
            }

        }

    }

  

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class Ale : Attribute
    {
        readonly string positionalString;

        public Ale()
        {
            this.positionalString = "";
        }

        public Ale(string descricao)
        {
            this.positionalString = descricao;
        }

        public string PositionalString
        {
            get { return positionalString; }
        }

        // This is a named argument
        public int NamedInt { get; set; }
    }

    public class MyClass : Ale
    {
        private string _xisto;

        [Ale("aaaa")]
        public string Xisto
        {
            get { return _xisto; }
            set { _xisto = value; }
        }
    }


}
