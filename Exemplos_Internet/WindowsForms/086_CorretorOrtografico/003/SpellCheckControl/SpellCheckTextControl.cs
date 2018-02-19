using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;



namespace SpellCheckControl
{
    public partial class SpellCheckTextControl : System.Windows.Forms.RichTextBox
    {

        #region Member Variables

        private object emptyItem = System.Reflection.Missing.Value;
        private object oNothing = null;
        private object oTrue = true;
        private object oFalse = false;
        private object oAlwaysSuggest = true;
        private object oIgnoreUpperCase = false;
        private bool mAlwaysSuggest = true;
        private bool mIgnoreUpperCase = false;

        #endregion


        #region Properties

        public bool AlwaysSuggest
        {
            get
            {
                return mAlwaysSuggest;
            }
            set
            {
                mAlwaysSuggest = value;

                if (mAlwaysSuggest == true)
                    oAlwaysSuggest = true;
                else
                    oAlwaysSuggest = false;
            }
        }


        public bool IgnoreUpperCase
        {
            get
            {
                return mIgnoreUpperCase;
            }
            set
            {
                mIgnoreUpperCase = value;

                if (mIgnoreUpperCase == true)
                    oIgnoreUpperCase = true;
                else
                    oIgnoreUpperCase = false;
            }
        }

        #endregion


        #region Constructor and Defaults


        public SpellCheckTextControl()
        {
            InitializeComponent();
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        #endregion


        #region Perform Spell Check


        /// <summary>
        /// Perform spell check operation on control content.
        /// This operation displays the results of the spell
        /// check.  
        /// 
        /// Alternatively, the function could be set to return
        /// a string and the results string could be returned
        /// to the caller
        /// </summary>
        public void CheckSpelling()
        {

            // declare local variables to track error count 
            // and information
            int SpellingErrors = 0;
            string ErrorCountMessage = string.Empty;

            // create an instance of a word application
            Microsoft.Office.Interop.Word.Application WordApp =
                new Microsoft.Office.Interop.Word.Application();

            // hide the MS Word document during the spellcheck
            WordApp.Visible = false;
            WordApp.ShowWindowsInTaskbar = false;

            // check for zero length content in text area
            if (this.Text.Length > 0)
            {

                // create an instance of a word document
                _Document WordDoc = WordApp.Documents.Add(ref emptyItem,
                                                  ref emptyItem,
                                                  ref emptyItem,
                                                  ref oFalse);

                // load the content written into the word doc
                WordDoc.Words.First.InsertBefore(this.Text);

                // collect errors form new temporary document set to contain
                // the content of this control
                Microsoft.Office.Interop.Word.ProofreadingErrors docErrors = WordDoc.SpellingErrors;
                SpellingErrors = docErrors.Count;

                // execute spell check; assumes no custom dictionaries
                WordDoc.CheckSpelling(ref oNothing, ref oIgnoreUpperCase, ref oAlwaysSuggest,
                    ref oNothing, ref oNothing, ref oNothing, ref oNothing, ref oNothing,
                    ref oNothing, ref oNothing, ref oNothing, ref oNothing);

                // format a string to contain a report of the errors detected
                ErrorCountMessage = "Spell check complete; errors detected: " + SpellingErrors;

                // return corrected text to control's text area
                object first = 0;
                object last = WordDoc.Characters.Count - 1;
                this.Text = WordDoc.Range(ref first, ref last).Text;
            }
            else
            {
                // if nothing was typed into the control, abort and inform user
                ErrorCountMessage = "Unable to spell check an empty text box.";
            }

            WordApp.Quit(ref oFalse, ref emptyItem, ref emptyItem);

            // return report on errors corrected
            // - could either display from the control or change this to 
            // - return a string which the caller could use as desired.
            MessageBox.Show(ErrorCountMessage, "Finished Spelling Check");
        }

        #endregion


    }
}
