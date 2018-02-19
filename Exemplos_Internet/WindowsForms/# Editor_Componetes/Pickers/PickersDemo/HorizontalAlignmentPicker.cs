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
    public class HorizontalAlignmentPicker : PickerBase<HorizontalAlignment, HorizontalAlignmentUI>
    {
        protected override HorizontalAlignment GetDefaultValue()
        {
            return HorizontalAlignment.Left;
        }

    }
}
