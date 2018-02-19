using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace WindowsFormsApplication7
{
    public partial class Form1 : Form
    {
        Microsoft.Office.Interop.Word.Application _wordApp = new Microsoft.Office.Interop.Word.Application();


        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            Microsoft.Office.Interop.Word.Range DRange;
            this.Text = "Creating Word...";

            //adding new document to word application
            _wordApp.Documents.Add();

            //set title
            this.Text = "Genarating error list...";

            //get the range of activer document
            DRange = _wordApp.ActiveDocument.Range();

            //insert textbox data after the content of range of active document
            DRange.InsertAfter(textBox1.Text);

            //createing object for error collection and store the all errors
            Microsoft.Office.Interop.Word.ProofreadingErrors spellCollection = DRange.SpellingErrors;

            if (spellCollection.Count > 0)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                int iword = 0;
                string newWord = null;

                //string sugestoes = spellCollection
                //    .OfType<Microsoft.Office.Interop.Word.Range>()
                //    .Select(s => _wordApp.GetSpellingSuggestions(s.Text)
                //                    .OfType<Microsoft.Office.Interop.Word.SpellingSuggestion>()
                //                    .Aggregate(String.Empty, (a, b) => a + (String.IsNullOrEmpty(a) ? "" : ", ") + b.Name))
                //    .Aggregate(String.Empty, (a, b) => a + (String.IsNullOrEmpty(a) ? "" : " | ") + b);

                //MessageBox.Show(sugestoes);


                for (iword = 1; iword <= spellCollection.Count; iword++)
                {
                    //-		SpellCollection[iword]	{System.__ComObject}	Microsoft.Office.Interop.Word.Range {System.__ComObject}

                    newWord = spellCollection[iword].Text;

                    listBox1.Items.Add(newWord);

                }
            }
            this.Text = "spelling checker Demo";

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Word.SpellingSuggestions correctionsCollection;

            correctionsCollection = _wordApp.GetSpellingSuggestions(listBox1.Text);
            listBox2.Items.Clear();
            if (correctionsCollection.Count > 0)
            {
                int iWord = 0;
                for (iWord = 1; iWord <= correctionsCollection.Count; iWord++)
                {
                    listBox2.Items.Add(correctionsCollection[iWord].Name);
                }
            }
            else
            {
                listBox2.Items.Add("No suggestions!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // it will replace word 
            try
            {
                if (listBox1.SelectedIndex >= 0 & listBox2.SelectedIndex >= 0)
                {

                    textBox1.Select(textBox1.Text.IndexOf(listBox1.SelectedItem.ToString()), listBox1.SelectedItem.ToString().Length);
                    textBox1.SelectedText = listBox2.SelectedItem.ToString();
                    listBox1.Items.Remove(listBox1.SelectedItem);
                    listBox2.Items.Clear();

                }
            }
            catch (Exception ex)
            {
                //handle exception 
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _wordApp.Quit(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(_wordApp);
            }
            catch { }

        }
    }
}
