//=============================================================================
// System  : EWSoftware Design Time Attributes and Editors
// File    : RefreshableItemListBox.cs
// Author  : Eric Woodruff
// Updated : 08/09/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a simple derived list box in which you can refresh a
// specified item to show updates to the text displayed in the list box for
// the item.
//
// This code may be used in compiled form in any way you desire.  This file
// may be redistributed unmodified by any means PROVIDING it is not sold for
// profit without the author's written consent, and providing that this notice
// and the author's name and all copyright notices remain intact.
//
// This code is provided "as is" with no warranty either express or implied.
// The author accepts no liability for any damage or loss of business that
// this product may cause.
//
// Version     Date     Who  Comments
// ============================================================================
// 1.0.0.0  08/09/2006  EFW  Created the code
//=============================================================================

using System;
using System.Windows.Forms;

namespace SandcastleBuilder.Utils.Design
{
    /// <summary>
    /// This is a a simple derived list box in which you can refresh a
    /// specified item to show updates to the text displayed in the list box
    /// for the item.
    /// </summary>
    public class RefreshableItemListBox : System.Windows.Forms.ListBox
    {
        /// <summary>
        /// Refresh the specified item in the list box
        /// </summary>
        /// <param name="index">The index of the item to refresh</param>
        public void Refresh(int index)
        {
 	        base.RefreshItem(index);
        }
    }
}
