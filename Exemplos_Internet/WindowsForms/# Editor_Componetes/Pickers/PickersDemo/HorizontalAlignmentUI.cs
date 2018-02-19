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
using Pickers;

namespace PickersDemo
{
    [ToolboxItem(false)]
    public partial class HorizontalAlignmentUI : UserControl, IDropDownUI
    {
        HorizontalAlignment selectedValue = HorizontalAlignment.Left;

        public HorizontalAlignmentUI()
        {
            InitializeComponent();
        }

        private void RaiseCloseDropDown()
        {
            if (CloseDropDownHolder != null)
                CloseDropDownHolder(this, EventArgs.Empty);
        }

        object IDropDownUI.GetSelectedValue()
        {
            return selectedValue;
        }

        void IDropDownUI.SetSelectedValue(object value)
        {
            switch (value.ToString())
            {
                case "Left":
                    SetSelectedValueCore(HorizontalAlignment.Left);
                    break;
                case "Center":
                    SetSelectedValueCore(HorizontalAlignment.Center);
                    break;
                case "Right":
                    SetSelectedValueCore(HorizontalAlignment.Right);
                    break;
                default:
                    SetSelectedValueCore(HorizontalAlignment.Left);
                    break;
            }
        }

        private void SetSelectedValueCore(HorizontalAlignment value)
        {
            selectedValue = value;
            SetButtons((value == HorizontalAlignment.Left), (value == HorizontalAlignment.Center), (value == HorizontalAlignment.Right));
        }

        private void SetButtons(bool left, bool center, bool right)
        {
            toolStripButtonLeft.Checked = left;
            toolStripButtonCenter.Checked = center;
            toolStripButtonRight.Checked = right;
        }

        public event EventHandler CloseDropDownHolder;

        private void toolStripButtonLeft_Click(object sender, EventArgs e)
        {
            SetSelectedValueCore(HorizontalAlignment.Left);
            RaiseCloseDropDown();
        }

        private void toolStripButtonCenter_Click(object sender, EventArgs e)
        {
            SetSelectedValueCore(HorizontalAlignment.Center);
            RaiseCloseDropDown();
        }

        private void toolStripButtonRight_Click(object sender, EventArgs e)
        {
            SetSelectedValueCore(HorizontalAlignment.Right);
            RaiseCloseDropDown();
        }

    }
}
