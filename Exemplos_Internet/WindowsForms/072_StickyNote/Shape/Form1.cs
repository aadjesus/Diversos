using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Shape
{
    public partial class Form_note : Form
    {
        public Form_note()
        {
            InitializeComponent();
        }

        /*
         * When you run this application a directory named note and a text file named note.txt will generate in the C:\Program Files
         */

        //this is the exit menu of the contexmenustrip
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        //this is the Font menu of the contexmenustrip
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            FontDialog font = new FontDialog();
            if (font.ShowDialog() == DialogResult.OK)
            {
                txt_note.Font = font.Font;                
            }
       }

       //this is the FontColor menu of the contexmenustrip    
        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                txt_note.ForeColor = color.Color;
            }
        }

        StreamWriter sw;
        StreamReader sr;
        FileStream note;
        private void Form_note_Load(object sender, EventArgs e)
        {     
            lbl_date.Text = System.DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy  hh:mm:ss");

           //Time is enable in the form load even
            timer1.Start();
            
            createFile();

            readNote();
        }

        /*
         * creaFile() function is called in the Form load event
         * It generate the directory ann file as mention above.
         */ 
        private void createFile()
        {
            bool existDir = Directory.Exists(@"C:\Program Files\Note");
            bool exitFolder = File.Exists(@"C:\Program Files\Note\note.txt");

           //If directory and text file is generated then it will not generate again when you run application next time.
            if (!(existDir && exitFolder))
            {
                Directory.CreateDirectory(@"C:\Program Files\Note");
                note = File.Create(@"C:\Program Files\Note\note.txt");
                note.Close();

                sw = new StreamWriter(@"C:\Program Files\Note\note.txt");
                sw.Write("Have a nice day\nWel-come from Priyank");
                sw.Close();
            }
        }

       /*
        * This is the richTextBox which is set into the pictureBox2.
        * Whatever you write in this text box it will saved into the note.txt file.
        */
        private void txt_writeNote_TextChanged(object sender, EventArgs e)
        {
            sr.Close();
            sw = new StreamWriter(@"C:\Program Files\Note\note.txt");
            sw.Write(txt_writeNote.Text);
            sw.Close();
        }
        
        /*
         * After writing your note in the text box when you close second Panel readNote() function come to the action.
         * It reads all the lines from the note.txt and it will display in the txt_note.Text which is set in the pictureBox1
         */ 
        private void readNote()
        {
            double size;
            float size1;
            string text;//,style;
            FontStyle style;

            sr = new StreamReader(@"C:\Program Files\Note\note.txt");
            text = sr.ReadToEnd();
            txt_note.Text = text;
            txt_writeNote.Text = text;
            sr.Close();            
        }

        /*
         * when you click here panel2 will visible so you are able to write your note.
         */ 
        private void btn_add_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        /*
         * After click on close button you are able to see your note.
         */ 
        private void btn_close_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            readNote();
        }        

        //Set the interval of the timer 1000.
        private void timer1_Tick(object sender, EventArgs e)
        {
            //display the current time and date
            lbl_date.Text =System.DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy  hh:mm:ss");
        }
    }
}
