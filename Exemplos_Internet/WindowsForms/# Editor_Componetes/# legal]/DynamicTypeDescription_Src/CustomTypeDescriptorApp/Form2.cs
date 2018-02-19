using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using DynamicTypeDescriptor;

using Dyn = DynamicTypeDescriptor;

using Scm = System.ComponentModel;

namespace CustomTypeDescriptorApp
{
  public partial class Form2 : Form
  {
    public Form2()
    {
      InitializeComponent( );
    }

    private void Form2_Load( object sender, EventArgs e )
    {
      propertyGrid1.PropertySort = PropertySort.CategorizedAlphabetical;
      MyClassA mcA = new MyClassA( );

      //Dyn.TypeDescriptor.IntallTypeDescriptor(mcA);
      this.propertyGrid1.SelectedObject = mcA;
    }

    private void button1_Click( object sender, EventArgs e )
    {
      Dyn.ResourceAttribute ra = new Dyn.ResourceAttribute( );
      ra.AssemblyFullName = this.GetType( ).Assembly.FullName;
      ra.KeyPrefix = "BuiltinBool_";
      ra.BaseName = "CustomTypeDescriptorApp.Properties.Resources";
      Scm.TypeConverterAttribute tca = new Scm.TypeConverterAttribute(typeof(Dyn.BooleanConverter));
      Scm.TypeDescriptor.AddAttributes(typeof(Boolean), ra, tca);
    }
  }

  public class MyClassA
  {
    public MyClassA()
    {
    }

    [Scm.DisplayName("1.Starsky && Hutch")]
    public EnumA PropA
    {
      get;
      set;
    }

    [Scm.DisplayName("2.Starsky & Hutch")]
    public EnumB PropB
    {
      get;
      set;
    }

    //private DateTime? m_PropD = null;

    //public DateTime PropD
    //{
    //  get
    //  {
    //    return (DateTime)m_PropD;
    //  }
    //  set
    //  {
    //    m_PropD = value;
    //  }
    //}
  }

  //[Dyn.Resource(BaseName = "", AssemblyFullName = "", KeyPrefix = "D_")]
  //[Scm.TypeConverter(typeof(Dyn.EnumConverter))]
  public enum EnumC
  {
    One,
    Two,
  }

  public enum EnumA
  {
    EnumItemA,
    EnumItemB,
    EnumItemC
  }

  [Flags]
  public enum EnumB
  {
    EnumItemD = 0,
    EnumItemE = 1,
    EnumItemF = 2,
    EnumItemG = 4
  }
}