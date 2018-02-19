using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace VEControl
{
  /// <summary>
  /// Interaction logic for VEMap.xaml
  /// </summary>
  public partial class VEMap : UserControl
  {
    public event EventHandler ViewChanged;
    public event EventHandler MapLoaded;
    public event EventHandler ModeInitialized;

    TopWindow topWindow;
    bool loaded;
    bool modeChanging = false;

    List<TempPushpin> pushPins = new List<TempPushpin>();
    List<UserControl> controlsAdded = new List<UserControl>();

    public VEMap()
    {
      InitializeComponent();

      this.Width = double.NaN;
      this.Height = double.NaN;

      if (!DesignerProperties.GetIsInDesignMode(this))
      {
        var ofs = new WindowExternalHelper(this);
        // Make the WindowExternalHelper available to
        // the webBrowser control
        this.webBrowser.ObjectForScripting = ofs;

        // Load the HTML for the web browser
        var a = Assembly.GetExecutingAssembly();
        var veMapSource =
          a.GetManifestResourceStream("VEControl.VEMap.htm");
        webBrowser.NavigateToStream(veMapSource);

        // Attach the event handlers
        this.Loaded += new RoutedEventHandler(VEMap_Loaded);
        this.SizeChanged += new SizeChangedEventHandler(VEMap_SizeChanged);
        this.Unloaded += new RoutedEventHandler(VEMap_Unloaded);

        this.topWindow = new TopWindow();
      }
    }

    public void LoadMap()
    {
      this.Show();
    }

    void VEMap_Loaded(object sender, RoutedEventArgs e)
    {
      this.SetOverlayPosition();
      this.SetOverlaySize();

      Window.GetWindow(this).LocationChanged += new EventHandler(VEMap_LocationChanged);
      Window.GetWindow(this).Activated += new EventHandler(VEMap_Activated);
      Window.GetWindow(this).Deactivated += new EventHandler(VEMap_Deactivated);
    }

    void VEMap_Deactivated(object sender, EventArgs e)
    {
      this.topWindow.Topmost = false;
    }

    void VEMap_Activated(object sender, EventArgs e)
    {
      this.topWindow.Topmost = true;
    }

    internal void ThrowException(Exception ex)
    {
      throw ex;
    }

    public UserControl InfoBox
    {
      get
      {
        return this.topWindow.InfoBox;
      }

      set
      {
        this.topWindow.InfoBox = value;
      }
    }

    internal void RaiseModeInitialized()
    {
      this.ModeInitialized(this, new EventArgs());
    }

    public void Show()
    {
      this.Visibility = Visibility.Visible;
      this.topWindow.Show();
    }

    public void Hide()
    {
      this.topWindow.Hide();
      this.Visibility = Visibility.Collapsed;
    }

    internal void RaiseViewChanged()
    {
      this.ViewChanged(this, new EventArgs());
      this.RepositionTopWindowElements();
    }

    private void RepositionTopWindowElements()
    {
      if (!modeChanging)
      {
        foreach (var pushPin in this.pushPins)
        {
          var latLong = new VELatLong(pushPin.LatLong.Latitude, pushPin.LatLong.Longitude);
          var pixel = this.LatLongToPixel(latLong);
          if (pixel != null)
          {
            pushPin.Button.Visibility = Visibility.Visible;
            var y = pixel.Y - (pushPin.Button.Height / 2);
            var x = pixel.X - (pushPin.Button.Width / 2);
            Canvas.SetTop(pushPin.Button, y);
            Canvas.SetLeft(pushPin.Button, x);
          }
          else
          {
            pushPin.Button.Visibility = Visibility.Collapsed;
          }
        }

        if (this.InfoBox != null)
        {
          if (this.InfoBox.Visibility == Visibility.Visible)
          {
            var sb = this.InfoBox.Tag as Button;
            var infoBoxContentControl = this.InfoBox.Parent as ContentControl;

            Canvas.SetLeft(infoBoxContentControl,
                        Canvas.GetLeft(sb));
            Canvas.SetTop(infoBoxContentControl,
                            Canvas.GetTop(sb));

            var buttonZIndex = Canvas.GetZIndex(sb);

            Canvas.SetZIndex(infoBoxContentControl, buttonZIndex + 1);
          }
        }

        //if (this.controlsAdded.Count > 0)
        //{
        //    foreach (var control in this.controlsAdded)
        //    {
        //        if (control.Visibility == Visibility.Visible)
        //        {
        //            //var pixel = new VEPixel(Canvas.GetLeft(control), Canvas.GetTop(control));

        //            //var latLong = this.PixelToLatLong()
        //            //Canvas.SetLeft(infoBoxContentControl,
        //            //            Canvas.GetLeft(sb));
        //            //Canvas.SetTop(infoBoxContentControl,
        //            //                Canvas.GetTop(sb));

        //            //var buttonZIndex = Canvas.GetZIndex(sb);

        //            //Canvas.SetZIndex(infoBoxContentControl, buttonZIndex + 1);
        //        }
        //    }
        //}
      }
    }

    internal void RaiseMapLoaded()
    {
      this.loaded = true;

      //BUG: This is causing an exception.  Need to investigate
      //this.MapLoaded(this, new EventArgs());
    }

    //void Resize(double width, double height)
    //{
    //    if (!DesignerProperties.GetIsInDesignMode(this))
    //    {
    //        if (this.loaded)
    //        {
    //            this.webBrowser.InvokeScript("Resize", width, height);
    //        }
    //    }
    //}

    void VEMap_LocationChanged(object sender, EventArgs e)
    {
      if (this.loaded)
      {
        this.SetOverlayPosition();
        //this.ForceProperZOrder();
      }
    }

    void VEMap_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      if (this.loaded)
      {
        this.SetOverlaySize();
        //this.ForceProperZOrder();
      }
    }

    void VEMap_Unloaded(object sender, RoutedEventArgs e)
    {
      this.topWindow.Close();
    }

    void SetOverlaySize()
    {
      this.topWindow.Width = this.ActualWidth;
      this.topWindow.Height = this.ActualHeight;
    }

    void SetOverlayPosition()
    {
      var controlVector = VisualTreeHelper.GetOffset(this);

      var controlOffsetPoint = this.PointToScreen(new Point(controlVector.X, controlVector.Y));

      var left = controlOffsetPoint.X - controlVector.X;
      var top = controlOffsetPoint.Y - controlVector.Y;

      this.topWindow.Left = left;
      this.topWindow.Top = top;
    }

    public void ZoomIn()
    {
      this.webBrowser.InvokeScript("ZoomIn");
    }

    public int GetZoomLevel()
    {
      return (int)this.webBrowser.InvokeScript("GetZoomLevel");
    }

    public void SetZoomLevel(int level)
    {
      this.webBrowser.InvokeScript("SetZoomLevel", level);
    }

    public void ZoomOut()
    {
      this.webBrowser.InvokeScript("ZoomOut");
    }

    public void Pan(int x, int y)
    {
      this.webBrowser.InvokeScript("Pan", x, y);
    }

    public void StartContinuousPan(double x, double y)
    {
      this.webBrowser.InvokeScript("StartContinuousPan", x, y);
    }

    public void EndContinuousPan()
    {
      this.webBrowser.InvokeScript("EndContinuousPan");
    }

    public VEMapMode GetMapMode()
    {
      var result = this.webBrowser.InvokeScript("GetMapMode");

      if (result.ToString() == "2")
      {
        return VEMapMode.Mode3D;
      }
      else
      {
        return VEMapMode.Mode2D;
      }
    }

    public void SetMapMode(VEMapMode mode)
    {
      modeChanging = true;

      switch (mode)
      {
        case VEMapMode.Mode2D:
          this.webBrowser.InvokeScript("SetMapMode", 1);
          break;
        case VEMapMode.Mode3D:
          this.webBrowser.InvokeScript("SetMapMode", 2);
          break;
      }

      modeChanging = false;
    }

    public VEMapStyle? GetMapStyle()
    {
      var result = this.webBrowser.InvokeScript("GetMapStyle");
      VEMapStyle? style = null;

      switch (result.ToString())
      {
        case "r":
          style = VEMapStyle.Road;
          break;
        case "a":
          style = VEMapStyle.Aerial;
          break;
        case "h":
          style = VEMapStyle.Hybrid;
          break;
        default:
          break;
      }

      return style;
    }

    public void SetMapStyle(VEMapStyle style)
    {
      switch (style)
      {
        case VEMapStyle.Road:
          this.webBrowser.InvokeScript("SetMapStyle", "r");
          break;
        case VEMapStyle.Aerial:
          this.webBrowser.InvokeScript("SetMapStyle", "a");
          break;
        case VEMapStyle.Hybrid:
          this.webBrowser.InvokeScript("SetMapStyle", "h");
          break;
        default:
          break;
      }
    }

    public VELatLong PixelToLatLong(VEPixel pixel)
    {
      var result = this.webBrowser.InvokeScript("PixelToLatLong", pixel.X, pixel.Y);

      var resultArray = result.ToString().Split('|');

      return new VELatLong(double.Parse(resultArray[0], System.Globalization.CultureInfo.InvariantCulture),
                           double.Parse(resultArray[1], System.Globalization.CultureInfo.InvariantCulture
                           ));
    }

    public VEPixel LatLongToPixel(VELatLong latLong)
    {
      var result = this.webBrowser.InvokeScript("LatLongToPixel",
                      latLong.Latitude, latLong.Longitude).ToString();

      if (result != string.Empty)
      {
        var resultArray = result.Split('|');
        return new VEPixel(double.Parse(resultArray[0], System.Globalization.CultureInfo.InvariantCulture),
                           double.Parse(resultArray[1], System.Globalization.CultureInfo.InvariantCulture));
      }
      else
      {
        return null;
      }
    }

    public void AddShape(VEShape shape)
    {
      switch (shape.Type)
      {
        case VEShapeType.Pushpin:
          //For pushpins, we render "invisible" buttons using WPF
          Button button = new Button();
          //NEED to evaluate a better way...
          button.Tag = shape.Tag;

          // Since we don't want to see the WPF/Surface button, we set it's opacity to .01
          // We will still use it for the MouseEnter event though.

          button.Opacity = .01;

          button.MouseEnter += new MouseEventHandler(button_MouseEnter);

          button.Width = 25;
          button.Height = 25;
          VEPixel pixel = this.LatLongToPixel(shape.Points[0]);
          double y = pixel.Y - (button.Height / 2);
          double x = pixel.X - (button.Width / 2);
          Canvas.SetTop(button, y);
          Canvas.SetLeft(button, x);

          this.topWindow.mainCanvas.Children.Add(button);
          this.pushPins.Add(new TempPushpin { Button = button, LatLong = shape.Points[0] });
          this.webBrowser.InvokeScript("AddShape", "Pushpin", ListOfLatLongToString(shape.Points));
          break;
        case VEShapeType.Polyline:
          //For polylines, we just let the map render
          this.webBrowser.InvokeScript("AddShape", "Polyline", ListOfLatLongToString(shape.Points));
          break;
        case VEShapeType.Polygon:
          //For polygons, we just let the map render
          this.webBrowser.InvokeScript("AddShape", "Polygon", ListOfLatLongToString(shape.Points));
          break;
      }
    }

    void button_MouseEnter(object sender, MouseEventArgs e)
    {
      if (this.InfoBox == null)
        return;

      var button = (Button)sender;
      var infoBoxContentControl = this.InfoBox.Parent as ContentControl;
      Canvas.SetLeft(infoBoxContentControl,
                      Canvas.GetLeft(button));
      Canvas.SetTop(infoBoxContentControl,
                      Canvas.GetTop(button));

      var buttonZIndex = Canvas.GetZIndex(button);

      Canvas.SetZIndex(infoBoxContentControl, buttonZIndex + 1);

      this.InfoBox.Tag = button;
      this.InfoBox.DataContext = button.Tag;

      this.InfoBox.Visibility = Visibility.Visible;
    }

    public void Clear()
    {
      foreach (var tempPushpin in this.pushPins)
      {
        this.topWindow.mainCanvas.Children.Remove(tempPushpin.Button);
      }

      this.pushPins.Clear();

      this.webBrowser.InvokeScript("Clear");
    }

    public void AddControl(UserControl control, int? zIndex)
    {
      control.Tag = zIndex;
      this.controlsAdded.Add(control);
      this.topWindow.mainCanvas.Children.Add(control);
    }

    public void SetMapView(List<VELatLong> arrayOfLatLong)
    {
      var stringOfLatLong = ListOfLatLongToString(arrayOfLatLong);
      this.webBrowser.InvokeScript("SetMapView", stringOfLatLong);
    }

    private string ListOfLatLongToString(List<VELatLong> arrayOfLatLong)
    {
      StringBuilder sb = new StringBuilder();

      foreach (VELatLong latLong in arrayOfLatLong)
      {
        sb.Append(latLong.ToString());
        sb.Append("#");
      }

      sb.Remove(sb.Length - 1, 1);

      return sb.ToString();
    }

    public VELatLong GetCenter()
    {
      var result = this.webBrowser.InvokeScript("GetCenter");

      var resultArray = result.ToString().Split('|');

      return new VELatLong(double.Parse(resultArray[0], System.Globalization.CultureInfo.InvariantCulture),
                           double.Parse(resultArray[1], System.Globalization.CultureInfo.InvariantCulture));
    }

    public void SetCenter(VELatLong latLong)
    {
      this.webBrowser.InvokeScript("SetCenter", latLong.Latitude, latLong.Longitude);
    }

    public void SetPitch(double pitch)
    {
      this.webBrowser.InvokeScript("SetPitch", pitch);
    }

    public void SetHeading(double heading)
    {
      this.webBrowser.InvokeScript("SetHeading", heading);
    }

    public void ShowDashboard()
    {
      this.webBrowser.InvokeScript("ShowDashboard");
    }

    public void HideDashboard()
    {
      this.webBrowser.InvokeScript("HideDashboard");
    }
  }
}
