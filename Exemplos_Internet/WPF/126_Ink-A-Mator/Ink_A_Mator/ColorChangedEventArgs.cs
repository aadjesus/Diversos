/****************************************************************************
 *                                                                          *
 *      Created By: Ernie Booth                                             *
 *      Contact: ebooth@microsoft.com - http://blogs.msdn.com/ebooth        *
 *      Last Modified: 6/23/2006                                            *
 *                                                                          *
 * **************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Ink_A_Mator
{
    public class ColorChangedEventArgs : EventArgs
    {
        private Color color;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
    }
}
