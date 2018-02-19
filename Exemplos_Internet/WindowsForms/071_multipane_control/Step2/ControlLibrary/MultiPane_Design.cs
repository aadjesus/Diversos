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
using System.Security;
using System.Security.Permissions;
using System.Runtime;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace Kerido.Controls.Design
{

	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	[Serializable]
	public class MultiPaneControlToolboxItem : ToolboxItem
	{
		public MultiPaneControlToolboxItem()
			: base(typeof(MultiPaneControl))
		{
		}

		// Serialization constructor, required for deserialization
		public MultiPaneControlToolboxItem(SerializationInfo theInfo, StreamingContext theContext)
		{
			Deserialize(theInfo, theContext);
		}

		protected override IComponent[] CreateComponentsCore(IDesignerHost theHost)
		{
			// Control
			MultiPaneControl aCtl = (MultiPaneControl)theHost.CreateComponent(typeof(MultiPaneControl));

			// Control's page
			MultiPanePage aNewPage1 = (MultiPanePage)theHost.CreateComponent(typeof(MultiPanePage));
		
			aCtl.Controls.Add(aNewPage1);

			return new IComponent[] { aCtl };
		}
	}


	internal class MultiPaneControlSelectedPageEditor : ObjectSelectorEditor
	{
		protected override void FillTreeWithData(Selector theSel,
			ITypeDescriptorContext theCtx, IServiceProvider theProvider)
		{
			base.FillTreeWithData(theSel, theCtx, theProvider);	//clear the selection

			MultiPaneControl aCtl = (MultiPaneControl) theCtx.Instance;

			foreach (MultiPanePage aIt in aCtl.Controls)
			{
				SelectorNode aNd = new SelectorNode(aIt.Name, aIt);

				theSel.Nodes.Add(aNd);

				if (aIt == aCtl.SelectedPage)
					theSel.SelectedNode = aNd;
			}
		}
	}
}
