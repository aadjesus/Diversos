using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GenericList
{
    public partial class Form1 : Form
    {
        public Tshrove.GenericList.GenericList<int> List = new Tshrove.GenericList.GenericList<int>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List.ItemAdded += new EventHandler<Tshrove.GenericList.GenericItemEventArgs<int>>(List_ItemAdded);
            List.ItemRemoved += new EventHandler<EventArgs>(List_ItemRemoved);
            List.ItemsCleared += new EventHandler<EventArgs>(List_ItemsCleared);
            List.BeforeItemAdded += new EventHandler<Tshrove.GenericList.GenericItemEventArgs<int>>(List_BeforeItemAdded);
            List.BeforeItemRemoved += new EventHandler<Tshrove.GenericList.GenericItemEventArgs<int>>(List_BeforeItemRemoved);

            List.Add(5);
            List.Remove(5);
            List.Add(10);
            List.Add(11);
            List.Clear();
        }

        void List_BeforeItemRemoved(object sender, Tshrove.GenericList.GenericItemEventArgs<int> e)
        {
            int i = 0;
        }

        void List_BeforeItemAdded(object sender, Tshrove.GenericList.GenericItemEventArgs<int> e)
        {
            int i = 0;
        }

        void List_ItemAdded(object sender, Tshrove.GenericList.GenericItemEventArgs<int> e)
        {
            int i = 0;
        }

        void List_ItemsCleared(object sender, EventArgs e)
        {
            int i = 0;
        }

        void List_ItemRemoved(object sender, EventArgs e)
        {
            int s = 0;
        }
    }
}
