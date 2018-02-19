using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Diagnostics;
using System.Reflection;
using System.Drawing;
using Dyn = DynamicTypeDescriptor;

namespace DynamicTypeDescriptor
{
  internal partial class EnumValueEditorUI : UserControl
  {
    private class TagItem
    {
      public bool SetByCode = false;
      public StandardValue Item = null;
      public TagItem( StandardValue item )
      {
        Item = item;
      }
    }
    private Type m_ValueType = null;
    private object m_Value = null;
    IWindowsFormsEditorService m_editorService = null;


    private bool m_bFlag = false;
    public EnumValueEditorUI()
    {
      InitializeComponent( );
    }

    public void SetData( ITypeDescriptorContext context, IWindowsFormsEditorService editorService, object value )
    {
      Debug.Assert(value != null);
      Debug.Assert(editorService != null);
      Debug.Assert(value.GetType( ).IsEnum);
      Debug.Assert(editorService != null);

      m_editorService = editorService;
      m_ValueType = value.GetType( );

      m_bFlag = (m_ValueType.GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0);

      m_Value = value;

      listViewSvc.Items.Clear( );
      listViewSvc.CheckBoxes = m_bFlag;


      List<Dyn.StandardValue> svaList = new List<StandardValue>( );
      if (context != null && context.PropertyDescriptor != null &&
        context.PropertyDescriptor is Dyn.PropertyDescriptor)
      {

        Dyn.PropertyDescriptor pd = context.PropertyDescriptor as Dyn.PropertyDescriptor;
        svaList.AddRange(pd.StandardValues);
      }
      else
      {
        svaList.AddRange(Dyn.EnumUtil.GetStandardValues(value.GetType( )));
        // make sure we get the localized version if possible
        TypeConverter tc = null;
        if (context != null && context.PropertyDescriptor != null)
        {
          tc = context.PropertyDescriptor.Converter;
        }
        else
        {
          tc = System.ComponentModel.TypeDescriptor.GetConverter(value);
        }
        if (tc != null && tc is DynamicTypeDescriptor.EnumConverter)
        {
          for (int i = 0; i < svaList.Count; i++)
          {
            Dyn.StandardValue svaLoc = tc.ConvertTo(svaList[i].Value, typeof(StandardValue)) as StandardValue;
            Debug.Assert(svaLoc != null);
            svaList[i] = svaLoc;
          }
        }
      }

      foreach (StandardValue sv in svaList)
      {
        Debug.Assert(sv != null);
        ListViewItem lvi = new ListViewItem( );
        lvi.Text = sv.DisplayName;
        lvi.ForeColor = (sv.Enabled == true ? lvi.ForeColor : Color.FromKnownColor(KnownColor.GrayText));
        lvi.Tag = new TagItem(sv);
        listViewSvc.Items.Add(lvi);
      }



      UpdateCheckState( );

      // make initial selection
      if (m_bFlag)
      {
        // select the first checked one
        foreach (ListViewItem lvi in listViewSvc.CheckedItems)
        {
          lvi.Selected = true;
          lvi.EnsureVisible( );
          break;
        }
      }
      else
      {
        foreach (ListViewItem lvi in listViewSvc.Items)
        {
          TagItem ti = lvi.Tag as TagItem;
          if (ti.Item.Value.Equals(m_Value))
          {
            lvi.Selected = true;
            lvi.EnsureVisible( );
            break;
          }
        }
      }

    }

    public object GetValue()
    {
      return Enum.ToObject(m_ValueType, m_Value);
    }

    private void listViewSvc_ItemCheck( object sender, ItemCheckEventArgs e )
    {
      TagItem ti = listViewSvc.Items[e.Index].Tag as TagItem;

      if (ti.SetByCode)
      {
        ti.SetByCode = false;
        return;
      }
      if (!ti.Item.Enabled)
      {
        e.NewValue = e.CurrentValue;
        return;
      }

      if (e.NewValue == CheckState.Checked)
      {
        bool bOk = EnumUtil.TurnOnBits(ref m_Value, ti.Item.Value);
      }
      else
      {
        bool bOk = EnumUtil.TurnOffBits(ref m_Value, ti.Item.Value);
      }

      e.NewValue = e.CurrentValue;
      UpdateCheckState( ); // this will change the check box on the list view item
    }

    private void listViewSvc_SelectedIndexChanged( object sender, EventArgs e )
    {
      if (listViewSvc.SelectedItems.Count > 0)
      {
        ListView listView = (ListView)sender;
        TagItem ti = listView.SelectedItems[0].Tag as TagItem;

        lblDispName.Text = ti.Item.DisplayName;
        lblDesc.Text = ti.Item.Description;

        if (!m_bFlag)
        {
          if (!ti.Item.Enabled)
          {
            return;
          }
          m_Value = ti.Item.Value;
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

    void UpdateCheckState()
    {
      if (!m_bFlag)
      {
        return;
      }

      foreach (ListViewItem lvi in listViewSvc.Items)
      {
        TagItem ti = lvi.Tag as TagItem;
        bool bitExist = EnumUtil.IsBitsOn(m_Value, ti.Item.Value);
        if (lvi.Checked != bitExist)
        {
          ti.SetByCode = true;
          lvi.Checked = bitExist;
        }
      }

    }
  }

}
