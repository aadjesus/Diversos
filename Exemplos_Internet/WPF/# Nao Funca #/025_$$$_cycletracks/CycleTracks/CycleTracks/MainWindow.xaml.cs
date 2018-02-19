﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CycleTracks.Converters;
using GarminData;
using Microsoft.Windows.Controls.Ribbon;
using VEControl;
using Run = GarminData.Run;

namespace CycleTracks
{
  /// <summary>
  /// Interaction logic for Window1.xaml
  /// </summary>
  public partial class MainWindow : RibbonWindow
  {
    public MainWindow()
    {
      InitializeComponent();

      this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
      this.Resources.MergedDictionaries
          .Add(Microsoft.Windows.Controls.Ribbon.PopularApplicationSkins.Office2007Black);

      // this.pictures.Load(@"D:\Clipart\BelgianBeers");

      this.map.ViewChanged += new EventHandler(map_ViewChanged);
    }

    void map_ViewChanged(object sender, EventArgs e)
    {
    }

    void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
      this.map.LoadMap();
      // LoadRoute("../../Garmin/valentijn.xml");
      //LoadRoute("../../Garmin/history.gar");
    }

    private void Clear()
    {
      this.map.Clear();
      this.isRouteDisplayed = false;
    }

    private void LoadRoute(string fileName)
    {
      this.Clear();

      Run run = Run.Load(fileName);

      // Little hack
      HartRatePositionConverter conv = 
        (HartRatePositionConverter) FindResource("posCalculator");
      conv.TotalDistance = run.TotalDistance;

      // First display the route on the map
      DisplayRoute(run.TrackPoints);

      // Get only track points per distance, otherwise too much data...
      List<TrackPoint> filtered = 
        FilterTrackPointsPerDistance(run.TrackPoints, 500).ToList();
      this.DataContext = filtered;
    }

    /// <summary>
    /// Filter trackpoints
    /// </summary>
    /// <param name="trackPoints">List of trackpoints</param>
    /// <param name="minDistance">minimum distance between two trackpoints</param>
    /// <returns>Filtered list of trackpoints</returns>
    private IEnumerable<TrackPoint> FilterTrackPointsPerDistance(IEnumerable<TrackPoint> trackPoints, double minDistance)
    {
      var files = (from picture in Directory.GetFiles(@"../../Pictures") select picture).GetEnumerator();

      double lastDistance = -minDistance - 1;
      // IEnumerator<int> heartRate = new Heart().HeartRate();
      var query = trackPoints.Where(tp =>
                         {
                           bool fOk = tp.Distance > lastDistance + minDistance;
                           if (fOk) lastDistance = tp.Distance;
                           return fOk;
                         })
                         .Select(tp =>
                         {
                           files.MoveNext();
                           tp.Image = File.Exists(files.Current) ? files.Current.LoadImage() : null;
                           return tp;
                         });
      return query;
    }

    private void OnCloseApplication(object sender, ExecutedRoutedEventArgs e)
    {
      Close();
    }

    private string GarminDirectory()
    {
      return @"D:\Temp\TechDays\CycleTracks\CycleTracks\Garmin";
    }

    private void OnAddNewEntry(object sender, ExecutedRoutedEventArgs e)
    {
      var ofd = new System.Windows.Forms.OpenFileDialog();
      ofd.InitialDirectory = GarminDirectory();
      ofd.Filter = "Garmin Files|*.gar|XML file|*.xml|All Files|*.*";
      ofd.Title = "Select Garmin Cycle file";

      if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        LoadRoute(ofd.FileName);
      }
    }

    private void OnClearEntry(object sender, ExecutedRoutedEventArgs e)
    {
    }

    private void OnDeleteEntry(object sender, ExecutedRoutedEventArgs e)
    {
    }

    private void OnHasSelectedEntries(object sender, CanExecuteRoutedEventArgs e)
    {
      //e.CanExecute = (dg != null && dg.SelectedItem as RegisterTransaction != null);
    }

    private void OnIgnore(object sender, ExecutedRoutedEventArgs e)
    {
    }

    private void OnShowGraph(object sender, RoutedEventArgs e)
    {
    }

    private bool isRouteDisplayed = false;

    private void DisplayRoute(List<TrackPoint> route)
    {
      if (isRouteDisplayed)
        return;

      var trackPointsToVE = from tp in route
                            select tp.ToVELatLong();

      var shape = new VEShape(VEShapeType.Polyline, trackPointsToVE.ToList());
      this.map.AddShape(shape);
      this.map.SetMapView(trackPointsToVE.ToList());

      isRouteDisplayed = true;
    }

    VEShape pin = null;

    private void TrackPointEnter(object sender, MouseEventArgs e)
    {
      List<TrackPoint> trackPoints = this.DataContext as List<TrackPoint>;
      if (trackPoints == null)
        return;
      TrackPoint first = trackPoints.First();
      TrackPoint last = trackPoints.Last();
      Ellipse ellipse = sender as Ellipse;
      if (ellipse != null)
      {
        TrackPoint tp = ellipse.Tag as TrackPoint;
        if (tp != null)
        {
          DisplayRoute(trackPoints);

          //pictures.SelectedItem = tp;
          pictures.BringItemIntoView(tp);

          pin = new VEShape(VEShapeType.Pushpin, tp.Position.ToVELatLong());
          // Add pin to map
          this.map.AddShape(pin);
          this.map.SetMapView(new List<VELatLong>() { first.ToVELatLong(), tp.ToVELatLong(), last.ToVELatLong() });
        }
      }
    }

    private void TrackPointLeave(object sender, MouseEventArgs e)
    {
      if (pin != null)
      {
        // remove pin from map
      }
    }

    private void OnAddMeshEntry(object sender, ExecutedRoutedEventArgs e)
    {

    }

    private void GoToU2U(object sender, ExecutedRoutedEventArgs e)
    {
      Process.Start("http://www.u2u.be");
    }
  }
}