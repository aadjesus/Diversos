using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraScheduler.Native;
using DevExpress.XtraBars;

namespace DateNavigator_Demo
{
   public partial class Navigator : UserControl
   {

      private bool blockiereNachrichtenSender;

      /// <summary>
      /// Sollen die Nachrichten über Zeitänderungen in Kalendern verarbeitet werden?
      /// </summary>
      private bool SynchronisiereKalender { get; set; }

      public Navigator()
      {
         blockiereNachrichtenSender = false;

         InitializeComponent();

         m_dateNavigator.EditDateModified += dateNavigator1_EditDateModified;
         m_dateNavigator.MouseMove += m_dateNavigator_MouseMove;
         m_dateNavigator.MouseUp += m_dateNavigator_MouseUp;
         barCheckItem_KalenderSync.CheckedChanged += barCheckItem_KalenderSync_CheckedChanged;

         SynchronisiereKalender = false;
         barCheckItem_KalenderSync.Checked = false;
      }

      void dateNavigator1_EditDateModified(object sender, EventArgs e)
      {
         if (OnEditDateModified != null)
         {
            if (!blockiereNachrichtenSender)
            {
               DateNavigator navigator = sender as DateNavigator;
               TimeIntervalCollection ti = new TimeIntervalCollection();

               foreach (DateTime time in navigator.Selection)
               {
                  ti.Add(new TimeInterval(time, time));
               }


               if (OnEditDateModified != null)
                  OnEditDateModified(ti);
            }
         }
      }
      public delegate void EditDateModified(TimeIntervalCollection ti);
      public event EditDateModified OnEditDateModified;


      public void SetTimeInterval(TimeIntervalCollection ti)
      {
         if (SynchronisiereKalender)
         {
            DatesCollection dc = CreateDatesCollection(ti);
            m_dateNavigator.DateTime = dc[0];
            m_dateNavigator.Selection.AddRange(dc);
            m_dateNavigator.RefreshLayout();
         }         
      }

      private DatesCollection CreateDatesCollection(TimeIntervalCollection newTi)
      {
         TimeIntervalCollection myTi = new TimeIntervalCollection();
         if (DateTime.Compare(newTi.Start, newTi.End) <= 0)
         {
            foreach (TimeInterval tiv in newTi)
            {
               myTi.Add(new TimeInterval(tiv.Start, tiv.End.AddMinutes(-1)));
            }
         }
         else
         {
            myTi = newTi;
         }

         DatesCollection datesCollection = new DatesCollection();
         foreach (TimeInterval tiInt in myTi)
         {
            if (tiInt.Duration.Days >= 1)
            {
               for (int i = 0; i <= tiInt.Duration.Days; i++)
               {
                  if (i > 0)
                     datesCollection.Add(tiInt.Start.AddDays((double)i));
                  else
                     datesCollection.Add(tiInt.Start);
               }
            }
            else
            {
               datesCollection.Add(tiInt.Start);
            }
         }
         return datesCollection;
      }

      /// <summary>
      /// MouseMove Event des m_dateNavigator
      /// </summary>
      /// <param name="sender">Event-Parameter</param>
      /// <param name="e">Event-Parameter</param>
      void m_dateNavigator_MouseMove(object sender, MouseEventArgs e)
      {
         if (e.Button == MouseButtons.Left)
            blockiereNachrichtenSender = true;
         else
            blockiereNachrichtenSender = false;
      }

      /// <summary>
      /// MouseUp Event des m_dateNavigator
      /// </summary>
      /// <param name="sender">Event-Parameter</param>
      /// <param name="e">Event-Parameter</param>
      void m_dateNavigator_MouseUp(object sender, MouseEventArgs e)
      {
         blockiereNachrichtenSender = false;
      }

      /// <summary>
      /// Eventhandler: Der Wert ider SelecBox barEditItem_KalenderSync wurde geändert.
      /// </summary>
      /// <param name="sender">Event-Parameter</param>
      /// <param name="e">Event-Parameter</param>
      void barCheckItem_KalenderSync_CheckedChanged(object sender, EventArgs e)
      {
         try
         {
            SynchronisiereKalender = ((BarCheckItem)sender).Checked;        
         }
         catch (Exception ex)
         {
            // throw EsAF.ErstelleAusnahme("EC:GS88BC", ex, "Der Event OnKalenderSyncModeChanged() konnte nicht gefeuert werden.");
         }

      }
   }
}
