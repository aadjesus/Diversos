using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;

namespace DateNavigator_Demo
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();

         my_navigator.OnEditDateModified += my_navigator_OnEditDateModified;
         my_scheduler.OnVisibleIntervalChanged += my_scheduler_OnVisibleIntervalChanged;
      }

      void my_scheduler_OnVisibleIntervalChanged(TimeIntervalCollection ti)
      {
         TimeIntervalCollection newTi = new TimeIntervalCollection();

         if (DateTime.Compare(ti.Start, ti.End) <= 0)
         {            
            foreach (TimeInterval tiv in ti)
            {
               // Allways a Day to long...
               newTi.Add(new TimeInterval(tiv.Start, tiv.End.AddMinutes(-1)));
            }
         }
         else
         {
            newTi = ti;
         }
         my_navigator.SetTimeInterval(newTi);
      }

      void my_navigator_OnEditDateModified(TimeIntervalCollection ti)
      {
         my_scheduler.SetVisibleInterval(ti);
      }

   }
}
