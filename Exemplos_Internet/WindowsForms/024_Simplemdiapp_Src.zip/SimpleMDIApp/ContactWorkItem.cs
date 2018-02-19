
using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.CompositeUI.EventBroker;
using System.Drawing;

namespace SimpleMDIApp
{
	public class ContactWorkItem : WorkItem
	{
        private static Point pos = new Point(0, 0);
        private ContactView m_contactView;
        private WindowSmartPartInfo m_smartPart;
        private static ToolStripLabel m_status;

		public void Show(IWorkspace parentWorkspace)
		{
            m_smartPart = new WindowSmartPartInfo();
            m_smartPart.ControlBox = true;
            m_smartPart.Location = pos;
            m_smartPart.Title = "Untitled - Contact";
            pos = Point.Add(pos, new Size(10, 10));
            m_contactView = Items.AddNew<ContactView>();
            parentWorkspace.Show(m_contactView, m_smartPart);
 
            this.Activate();
		}

        private void DisplayStatus(string strStatus)
        {
            if (m_status == null)
            {
                m_status =  new ToolStripLabel();
                RootWorkItem.UIExtensionSites["StatusStrip"].Add(m_status);
            }
            m_status.Text = strStatus;
        }


		protected override void OnActivated()
		{
			base.OnActivated();
            DisplayStatus(m_contactView.ContactName);
		}

		protected override void OnDeactivated()
		{
			base.OnDeactivated();
		}
        
        protected override void OnTerminated()
        {
            base.OnTerminated();
        }

	}
}
