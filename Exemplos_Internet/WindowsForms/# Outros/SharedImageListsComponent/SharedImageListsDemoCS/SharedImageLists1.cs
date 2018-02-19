using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SharedImageListsDemo
{
  public partial class SharedImageLists1
  {
    public SharedImageLists1()
    {
      InitializeComponent();
    }

    public SharedImageLists1(IContainer container)
    {
      container.Add(this);

      InitializeComponent();
    }
  }
}
