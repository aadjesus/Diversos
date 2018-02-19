using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace SpellCheck
{
    /// <summary>
    /// Demo of the use of the spell check custom control
    /// </summary>
    public partial class Form1 : Form
    {

        // String pointing the file path of the current file
        private string currentFile = string.Empty;


        public Form1()
        {
            InitializeComponent();
        }



        // evoke the spell check function
        private void tspSpellCheck_Click(object sender, EventArgs e)
        {
            this.spellCheckTextControl1.CheckSpelling();
            this.Refresh();
        }


        // save the current file
        private void tspSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog1.Title = "Save File";
                SaveFileDialog1.DefaultExt = "rtf";
                SaveFileDialog1.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*";
                SaveFileDialog1.FilterIndex = 1;

                if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    if (SaveFileDialog1.FileName == "")
                    {
                        return;
                    }

                    string strExt;
                    strExt = System.IO.Path.GetExtension(SaveFileDialog1.FileName);
                    strExt = strExt.ToUpper();

                    if (strExt == ".RTF")
                    {
                        spellCheckTextControl1.SaveFile(SaveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        System.IO.StreamWriter txtWriter;
                        txtWriter = new System.IO.StreamWriter(SaveFileDialog1.FileName);
                        txtWriter.Write(spellCheckTextControl1.Text);
                        txtWriter.Close();
                        txtWriter = null;
                        spellCheckTextControl1.SelectionStart = 0;
                        spellCheckTextControl1.SelectionLength = 0;
                    }

                    currentFile = SaveFileDialog1.FileName;
                    spellCheckTextControl1.Modified = false;
                    MessageBox.Show(currentFile.ToString() + " saved.", "File Save");
                }
                else
                {
                    MessageBox.Show("Save File request cancelled by user.", "Cancelled");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }


        // open a file into the control
        private void tspOpen_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog1.Title = "Open File";
                OpenFileDialog1.DefaultExt = "rtf";
                OpenFileDialog1.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*";
                OpenFileDialog1.FilterIndex = 1;
                OpenFileDialog1.FileName = string.Empty;

                if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    if (OpenFileDialog1.FileName == "")
                    {
                        return;
                    }

                    string strExt;
                    strExt = System.IO.Path.GetExtension(OpenFileDialog1.FileName);
                    strExt = strExt.ToUpper();

                    if (strExt == ".RTF")
                    {
                        spellCheckTextControl1.LoadFile(OpenFileDialog1.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        System.IO.StreamReader txtReader;
                        txtReader = new System.IO.StreamReader(OpenFileDialog1.FileName);
                        spellCheckTextControl1.Text = txtReader.ReadToEnd();
                        txtReader.Close();
                        txtReader = null;
                        spellCheckTextControl1.SelectionStart = 0;
                        spellCheckTextControl1.SelectionLength = 0;
                    }

                    currentFile = OpenFileDialog1.FileName;
                    spellCheckTextControl1.Modified = false;
                }
                else
                {
                    MessageBox.Show("Open File request cancelled by user.", "Cancelled");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }


        // exit
        private void tspExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}