using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MaskedInput
{
    public partial class frmMaskedInput : Form
    {
        public frmMaskedInput()
        {
            InitializeComponent();
        }

        //Mask 001: Set different Masks based on the selected radio buttons. 
        //              These masks control the output in a specific format.
        private void radMask1_CheckedChanged(object sender, EventArgs e)
        {
            maskedInput.Mask = radMask1.Text;
        }

        private void radMask2_CheckedChanged(object sender, EventArgs e)
        {
            maskedInput.Mask = radMask2.Text;
        }

        private void radMask3_CheckedChanged(object sender, EventArgs e)
        {
            maskedInput.Mask = radMask3.Text;
        }

        private void radMask4_CheckedChanged(object sender, EventArgs e)
        {
            maskedInput.Mask = radMask4.Text;
        }

        private void radMask5_CheckedChanged(object sender, EventArgs e)
        {
            maskedInput.Mask = radMask5.Text;
        }

        //Mask 002: The output is read from the text property. As usual.
        private void btnOutput_Click(object sender, EventArgs e)
        {
            txtOutput.Text = maskedInput.Text;
        }

        //Mask 003: Hides the prompt chars on the mask when we leave the control
        private void chkHideMsk_CheckedChanged(object sender, EventArgs e)
        {
            maskedInput.HidePromptOnLeave = chkHideMsk.Checked;
        }

        //Mask 004: When user enters the invalid char raises the beep (Or plays corrosponding .wav file
        //               when external sound device is connected)
        private void chkBeepErr_CheckedChanged(object sender, EventArgs e)
        {
            maskedInput.BeepOnError = chkBeepErr.Checked;
        }

        //Mask 005: When we take the output from mask control, we have the option of skipping the 
        //              Prompt and/or literal.
        private void chkIncludeLiteral_CheckedChanged(object sender, EventArgs e)
        {
            SetOutputFormat();
        }

        private void chkIncludePrompt_CheckedChanged(object sender, EventArgs e)
        {
            SetOutputFormat();
        }

        private void SetOutputFormat()
        {
            if (chkIncludeLiteral.Checked == true)
            {
                if (chkIncludePrompt.Checked == true)
                {
                    maskedInput.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                }
                else
                {
                    maskedInput.TextMaskFormat = MaskFormat.IncludeLiterals;
                }
            }
            else
            {
                if (chkIncludePrompt.Checked == true)
                {
                    maskedInput.TextMaskFormat = MaskFormat.IncludePrompt;
                }
                else
                {
                    maskedInput.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                }
            }            
        }

        //Mask 006: Setting the different mask prompt charactor
        private void btnSet_Click(object sender, EventArgs e)
        {
            maskedInput.PromptChar = System.Convert.ToChar(txtmskPromptChar.Text);
        }

        //Mask 007: Show the Mask Completed label when required input is completed.
        private void maskedInput_TextChanged(object sender, EventArgs e)
        {
            if (maskedInput.MaskCompleted == true)
                lblMaskCompleted.Visible = true;
            else
                lblMaskCompleted.Visible = false;
        }
    }
}