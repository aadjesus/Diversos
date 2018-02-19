/* ===============================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
 * ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
 * THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 * PARTICULAR PURPOSE.
 * 
 * (C) MSI 2007. All Rights Reserved.
 *
 * For questions/comments, please contact me at msafderiqbal@hotmail.com.
 * ===============================================================================
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PickersDemo
{
    public partial class AppearanceChanger : UserControl
    {
        private Pickers.IPickerControl buddyPicker;

        public Pickers.IPickerControl BuddyPicker
        {
            get { return buddyPicker; }
            set { buddyPicker = value; }
        }

        public AppearanceChanger()
        {
            InitializeComponent();
        }

        private void radioButtonCB_CheckedChanged(object sender, EventArgs e)
        {
            ChangeAppearance(Pickers.PickerAppearance.ComboBox);
        }

        private void ChangeAppearance(Pickers.PickerAppearance value)
        {
            if (buddyPicker != null)
                buddyPicker.Appearance = value;
        }

        private void radioButtonECB_CheckedChanged(object sender, EventArgs e)
        {
            ChangeAppearance(Pickers.PickerAppearance.EditableComboBox);
        }

        private void radioButtonChkB_CheckedChanged(object sender, EventArgs e)
        {
            ChangeAppearance(Pickers.PickerAppearance.CheckButton);
        }
    }
}
