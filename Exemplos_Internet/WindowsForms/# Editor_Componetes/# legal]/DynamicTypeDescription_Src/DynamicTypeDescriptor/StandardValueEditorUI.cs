using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Dyn = DynamicTypeDescriptor;

namespace DynamicTypeDescriptor
{

  internal partial class StandardValueEditorUI : UserControl
  {
    private Type m_ValueType = null;
    private object m_Value = null;
    IWindowsFormsEditorService m_editorService = null;

    public StandardValueEditorUI()
    {
      InitializeComponent( );
    }

    public void SetData( ITypeDescriptorContext context, IWindowsFormsEditorService editorService, object value )
    {
      Debug.Assert(value != null);
      Debug.Assert(editorService != null);
      Debug.Assert(context != null);
      Debug.Assert(context.PropertyDescriptor != null);
      Debug.Assert(context.PropertyDescriptor is Dyn.PropertyDescriptor);

      m_editorService = editorService;
      m_ValueType = value.GetType( );


      m_Value = value;

      listViewSvc.Items.Clear( );

      PropertyDescriptor cpd = context.PropertyDescriptor as Dyn.PropertyDescriptor;
      // create list view items for the visible Enum items
      foreach (StandardValue sv in cpd.StandardValues)
      {
        if (sv.Visible)
        {
          ListViewItem lvi = new ListViewItem( );
          lvi.Text = sv.DisplayName;
          lvi.ForeColor = (sv.Enabled == true ? lvi.ForeColor : Color.FromKnownColor(KnownColor.GrayText));
          lvi.Tag = sv;
          listViewSvc.Items.Add(lvi);
        }
      }

      foreach (ListViewItem lvi in listViewSvc.Items)
      {
        StandardValue sv = lvi.Tag as StandardValue;
        if (sv.Value.Equals(m_Value))
        {
          lvi.Selected = true;
          lvi.EnsureVisible( );
          break;
        }
      }
    }

    public object GetValue()
    {
      return m_Value;
    }

    private void listViewSvc_SelectedIndexChanged( object sender, EventArgs e )
    {
      if (listViewSvc.SelectedItems.Count > 0)
      {
        ListView listView = (ListView)sender;
        StandardValue sv = listView.SelectedItems[0].Tag as StandardValue;

        lblDispName.Text = sv.DisplayName;
        lblDesc.Text = sv.Description;
        if (sv.Enabled)
        {
          m_Value = sv.Value;
        }
      }
    }

    private void listViewSvc_MouseDoubleClick( object sender, MouseEventArgs e )
    {
      m_editorService.CloseDropDown( );
    }

    private void listViewSvc_SizeChanged( object sender, EventArgs e )
    {
      listViewSvc.Columns[0].Width = listViewSvc.Width - 20;
      listViewSvc.Invalidate( );
      lblDesc.Invalidate( );
      this.Invalidate( );
    }
  }

}
