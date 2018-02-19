// ******************************************************************
//
//	If this code works it was written by:
//		Henry Minute
//		MamSoft / Manniff Computers
//		© 2008 - 2009...
//
//	if not, I have no idea who wrote it.
//
// ******************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImprovedNestedControlDesignerLibrary
{
	[Designer(typeof(ImprovedNestedControlDesignerLibrary.Designers.TestControlDesigner))]
	public partial class ImprovedTestControl : UserControl
	{
		public ImprovedTestControl()
		{
			InitializeComponent();
		}

		#region TestControl PROPERTIES ..........................
		
        public string CaptionX
		{
			get
			{
				return this.lblCaption.Text;
			}

			set
			{
				this.lblCaption.Text = value;
			}
		}

        public int XXX
        {
            get
            {
                return this.pnlHeader.Height;
            }

            set
            {
                this.pnlHeader.Height = value;
            }
        }


        //[
        //    //Designer(typeof(NestedControlDesignerLibrary.Designers.WorkingAreaDesigner)),
        //DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Panel WorkingArea
		{
			get
			{
				return this.pnlWorkingArea;
			}
        }


        [
            //Designer(typeof(NestedControlDesignerLibrary.Designers.WorkingAreaDesigner)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel WorkingArea1
        {
            get
            {
                return this.pnlHeader;
            }
        }




        #endregion

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
