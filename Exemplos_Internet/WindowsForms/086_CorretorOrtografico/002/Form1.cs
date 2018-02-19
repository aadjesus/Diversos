using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Reflection;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;

namespace SpellCheckDemo
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(40, 40);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(344, 136);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(392, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Check Spelling";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(40, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 16);
            this.label1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(496, 205);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "SpellCheckDemo";
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.Run(new Form1());
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            fSpellCheck(textBox1, label1);
        }

        public void fSpellCheck(TextBox tBox, Label lLbl)
        {
            Word.Application app = new Word.Application();

            int errors = 0;
            if (textBox1.Text.Length > 0)
            {
                app.Visible = false;

                object template = Missing.Value;
                object newTemplate = Missing.Value;
                object documentType = Missing.Value;
                object visible = true;

                Word._Document doc1 = app.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);
                
                doc1.Words.First.InsertBefore(textBox1.Text);
                Word.ProofreadingErrors spellErrorsColl = doc1.SpellingErrors;
                errors = spellErrorsColl.Count;

                object optional = Missing.Value;

                doc1.CheckSpelling(
                    ref optional, ref optional, ref optional, ref optional, ref optional, ref optional,
                    ref optional, ref optional, ref optional, ref optional, ref optional, ref optional);

                label1.Text = errors + " errors corrected ";
                object first = 0;
                object last = doc1.Characters.Count - 1;
                textBox1.Text = doc1.Range(ref first, ref last).Text;
            }

            object saveChanges = false;
            object originalFormat = Missing.Value;
            object routeDocument = Missing.Value;

            app.Quit(ref saveChanges, ref originalFormat, ref routeDocument);
        }
    }
}
