using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace CustomControls
{
	/// <summary>
	/// Author	: Moditha Kumara
	/// Date	: 27/5/2004
	/// 
	/// Custom CollectionEditor for StatusBarPanelEx.
	/// Finding out how this should be done was not that easy.
	/// Finaly managed to get it going. However, There is lot more
	/// to learn about design time support.
	/// 
	/// </summary>
	public class StatusBarPanelExCollectionEditor:CollectionEditor
	{
		private Type[] types; 
		public StatusBarPanelExCollectionEditor(Type type):base(type)
		{
			types = new Type[] {typeof(StatusBarPanelEx)}; 
		}

		protected override Type[] CreateNewItemTypes()
		{
			return types;
		}
	}
}
