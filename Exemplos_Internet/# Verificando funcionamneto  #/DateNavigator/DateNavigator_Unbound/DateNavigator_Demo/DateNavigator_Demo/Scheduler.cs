using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Native;
using DevExpress.XtraBars;

namespace DateNavigator_Demo
{

   public partial class Scheduler : UserControl
   {

      /// <summary>
      /// Legt fest ob die Kalenderansicht automatisch an die Auswahl des Zeitfilters angepasst wird.
      /// </summary>
      public bool WechsleKalenderAnsichtAutomatisch
      {
         get { return barCheckItem_AnsichtenWechseln.Checked; }
         set
         {
            barCheckItem_AnsichtenWechseln.Checked = value;
         }
      }

      public Scheduler()
      {
         InitializeComponent();
         m_schedulerControl.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Day;

         m_schedulerControl.VisibleIntervalChanged += schedulerControl1_VisibleIntervalChanged;
         barCheckItem_AnsichtenWechseln.CheckedChanged += barCheckItem_AnsichtenWechseln_CheckedChanged;

         WechsleKalenderAnsichtAutomatisch = true;
      }

      void schedulerControl1_VisibleIntervalChanged(object sender, EventArgs e)
      {
         if (OnVisibleIntervalChanged != null)
            OnVisibleIntervalChanged(m_schedulerControl.ActiveView.GetVisibleIntervals());
      }
      public delegate void VisibleIntervalChanged(TimeIntervalCollection ti);
      public event VisibleIntervalChanged OnVisibleIntervalChanged;


      public void SetVisibleInterval(TimeIntervalCollection ti)
      {
         // m_schedulerControl.ActiveView.SetVisibleIntervals(ti);

         //TimeIntervalCollection curTimeIntervalCollection = m_schedulerControl.ActiveView.GetVisibleIntervals();
         //if (DateTime.Compare(curTimeIntervalCollection.Start, timeIntervalCollection.Start) == 0
         //   && DateTime.Compare(curTimeIntervalCollection.End.AddDays(-1), timeIntervalCollection.End) == 0)
         //   return;

         if (WechsleKalenderAnsichtAutomatisch == true)
         {
            DayIntervalCollection dic = new DayIntervalCollection();
            dic.AddRange(ti);
            m_schedulerControl.ActiveViewType = BerechneAnsichtNachTagesauswahl(dic);
         }

         m_schedulerControl.BeginUpdate();
         try
         {
            m_schedulerControl.ActiveView.SetVisibleIntervals(ti);
         }
         catch (Exception ex)
         {
            // throw EsAF.ErstelleAusnahme("EC:GSZCG2", ex, "Fehler beim setzen des Zeitraums im Steuerelement EsKalender.");
         }
         finally
         {
            m_schedulerControl.EndUpdate();
         }
      }

      /// <summary>
      /// Berechne eine neue Ansicht nach der Tagesauswahl des Zeitfilters.
      /// </summary>
      /// <param name="days">DayIntervalCollection</param>
      /// <returns>SchedulerViewType</returns>
      private SchedulerViewType BerechneAnsichtNachTagesauswahl(DayIntervalCollection days)
      {
         if (m_schedulerControl.ActiveViewType == SchedulerViewType.Timeline)
            return SchedulerViewType.Timeline;

         if (GesamteWocheAusgewählt(days))
         {
            if (days.Count >= 5 && days.Count < 7)
            {
               return SchedulerViewType.WorkWeek;
            }
            if (days.Count == 7)
            {
               return SchedulerViewType.Week;
            }
            else
               return SchedulerViewType.Month;
         }
         else
         {
            if (m_schedulerControl.ActiveViewType == SchedulerViewType.WorkWeek && days.Count == 1)
               return SchedulerViewType.WorkWeek;
            else
            return SchedulerViewType.Day;
         }
      }

      /// <summary>
      /// Gibt den ersten Tag einer Woche zurück.
      /// </summary>
      private DayOfWeek FirstDayOfWeek { get { return m_schedulerControl != null ? m_schedulerControl.FirstDayOfWeek : DateTimeHelper.FirstDayOfWeek; } }

      /// <summary>
      /// Prüft ob eine komplette Woche im Zeitfilter ausgewählt wurde.
      /// </summary>
      /// <param name="days">DayIntervalCollection</param>
      /// <returns></returns>
      private bool GesamteWocheAusgewählt(DayIntervalCollection days)
      {
         if (days.Count == 0)
            return false;
         if ((days.Count % 7) != 0)
            return false;
         int weekCount = days.Count / 7;
         DayOfWeek firstDayOfWeek = FirstDayOfWeek;
         TimeSpan weekSpan = TimeSpan.FromDays(6);
         for (int i = 0, dayIndex = 0; i < weekCount; i++, dayIndex += 7)
         {
            DateTime date = days[dayIndex].Start;
            if (date.DayOfWeek != firstDayOfWeek)
               return false;
            if (days[dayIndex + 6].Start - date != weekSpan)
               return false;
         }
         return true;
      }

      /// <summary>
      /// Der Toolbar-Button barCheckItem_AnsichtenWechseln wurde geklickt.
      /// </summary>
      /// <param name="sender">Event-Parameter</param>
      /// <param name="e">Event-Parameter</param>
      void barCheckItem_AnsichtenWechseln_CheckedChanged(object sender, ItemClickEventArgs e)
      {         
         WechsleKalenderAnsichtAutomatisch = ((BarCheckItem)sender).Checked;
      }

   }
}
