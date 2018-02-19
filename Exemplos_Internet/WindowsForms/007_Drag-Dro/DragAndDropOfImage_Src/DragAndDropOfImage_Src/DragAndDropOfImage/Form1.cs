using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DragAndDropOfImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            //we will pass the data that user wants to drag DoDragDrop method is used for holding data
            //DoDragDrop accepts two paramete first paramter is data(image,file,text etc) and second paramter 
            //specify either user wants to copy the data or move data
            
            Panel source = (Panel)sender;
            DoDragDrop(source.BackgroundImage, DragDropEffects.Copy);

            
        }

        private void panel_DragEnter(object sender, DragEventArgs e)
        {
            //As we are interested in Image data only we will check this as follows
            if (e.Data.GetDataPresent(typeof(Bitmap)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        private void panel_DragDrop(object sender, DragEventArgs e)
        {
            //target control will accept data here 
            Panel destination = (Panel)sender;
            destination.BackgroundImage = (Bitmap)e.Data.GetData(typeof(Bitmap));

        }

        


    }
}