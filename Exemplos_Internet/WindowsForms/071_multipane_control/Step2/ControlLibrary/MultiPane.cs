/*
* Copyright (c) 2007, KO Software (official@koapproach.com)
*
* All rights reserved.
*
* Redistribution and use in source and binary forms, with or without
* modification, are permitted provided that the following conditions are met:
*
*     * All original and modified versions of this source code must include the
*       above copyright notice, this list of conditions and the following
*       disclaimer.
*
*     * This code may not be used with or within any modules or code that is 
*       licensed in any way that that compels or requires users or modifiers
*       to release their source code or changes as a requirement for
*       the use, modification or distribution of binary, object or source code
*       based on the licensed source code. (ex: Cannot be used with GPL code.)
*
*     * The name of KO Software may be used to endorse or promote products
*       derived from this software without specific prior written permission.
*
* THIS SOFTWARE IS PROVIDED BY KO SOFTWARE ``AS IS'' AND ANY EXPRESS OR
* IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
* OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO
* EVENT WILL KO SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
* SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
* PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; 
* OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
* WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR
* OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
* ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Windows;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace Kerido.Controls
{
	[ToolboxItem(typeof(Kerido.Controls.Design.MultiPaneControlToolboxItem))]
	public class MultiPaneControl : Control
	{
		protected static readonly Size ourDefaultSize = new Size(200, 100);

		protected MultiPanePage mySelectedPage;

		public MultiPaneControl()
		{
			ControlAdded += new ControlEventHandler(Handler_ControlAdded);

			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			BackColor = Color.Transparent;
		}

		protected override Size DefaultSize { get { return ourDefaultSize; } }

		#region Properties - own
		[
		Editor(
				typeof(Kerido.Controls.Design.MultiPaneControlSelectedPageEditor),
				typeof(System.Drawing.Design.UITypeEditor) )
		]
		public MultiPanePage SelectedPage
		{
			get { return mySelectedPage; }

			set
			{
				if (mySelectedPage != null)
					mySelectedPage.Visible = false;

				mySelectedPage = value;

				if (mySelectedPage != null)
					mySelectedPage.Visible = true;
			}
		}

		#endregion

		private void Handler_ControlAdded(object theSender, ControlEventArgs theArgs)
		{
			if (theArgs.Control is MultiPanePage)
			{
				MultiPanePage aPg = (MultiPanePage) theArgs.Control;

				// prevent the page from being moved and/or sized independently
				aPg.Location = new Point(0, 0);
				aPg.Size = ClientSize;
				aPg.Dock = DockStyle.Fill;

				if (Controls.Count == 1)
					mySelectedPage = aPg;
				else
					aPg.Visible = false;
			}
			else
			{
				//silently remove
				Controls.Remove(theArgs.Control);
			}
		}
	}

	

	[ToolboxItem(false)]
	public class MultiPanePage : Panel
	{
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override DockStyle Dock
		{
			get { return base.Dock; }
			set { base.Dock = value; }
		}

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Point Location
		{
			get { return base.Location; }
			set { base.Location = value; }
		}
	}
}
