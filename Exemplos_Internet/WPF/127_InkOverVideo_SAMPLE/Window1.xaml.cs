using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Ink;
using System.Windows.Threading;
using Microsoft.Win32;
namespace InkOverVideo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class MediaElementExample : Window
    {
        OpenFileDialog openFileDialog = null;
        string currentOpenedFile = null;

        StrokeCollection recordedStrokes;

        public MediaElementExample()
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = ".";
            openFileDialog.Filter = "Media Files (*.wmv)|*.wmv|Media Files (*.avi)|*.avi|All Files (*.*)|*.*";
            openFileDialog.RestoreDirectory = false;

            //Store the strokes we record in a strokecollection for playback
            recordedStrokes = new StrokeCollection();
        }
        void OpenMedia(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                currentOpenedFile = openFileDialog.FileName;
                //update the MediaElement's source
                myMediaElement.Source = new Uri(openFileDialog.FileName);
            }
        }

        public void Element_MediaEnded(object sender, EventArgs e)
        {
            recording = false;
        }

        //Two guids used to record playback times as custom data on each Stroke
        public Guid startTime = new Guid("a1ed8a88-ecda-47d7-9599-11e748e02a2f");
        public Guid endTime = new Guid("1c073bcb-d41c-4e91-98b9-efe452ae57bc");       
        bool recording = false;
        
        //Get the start times for each of stroke.
        long tsStrokeBegin = 0;
        void InkCanvasPreviewMouseDown(object sender, MouseButtonEventArgs args)
        {
            // Get the start time relative to the MediaElement's position
            if (recording)
            {
                tsStrokeBegin = myMediaElement.Position.Ticks;
            }
        }
 
        //Get the end time for each stroke.
        long tsStrokeEnd = 0;
        void InkCanvasStrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            // Add the start/end time relative to the MediaElement's position to the stroke property data
            if (recording)
            {
                tsStrokeEnd = myMediaElement.Position.Ticks;
                //Add the times for each stroke as a string to the stroke's PropertyData
                e.Stroke.AddPropertyData(startTime, tsStrokeBegin);
                e.Stroke.AddPropertyData(endTime, tsStrokeEnd);
                recordedStrokes.Add(e.Stroke.Clone());
            }
            tsStrokeBegin = 0;
            tsStrokeEnd = 0;
        }

        void RecordInk(object sender, RoutedEventArgs args)
        {
            if (currentOpenedFile != null)
            {
                recordedStrokes.Clear();
                recording = true;
                myInkCanvas.Strokes.Clear();
                myMediaElement.Position = TimeSpan.Zero;
                myMediaElement.Play();
            }
        }
        void PlayBackInk(object sender, RoutedEventArgs args)
        {
            myInkCanvas.Strokes.Clear();
            myInkCanvas.EditingMode = InkCanvasEditingMode.None;
            myMediaElement.Position = TimeSpan.Zero;
            myMediaElement.Play();
            for (int i = 0; i < recordedStrokes.Count; i++)
            {
                long strokeBegin = 0;
                long strokeEnd = 0;
                
                //Get the stroke
                CustomStroke theStroke = (CustomStroke)recordedStrokes[i].Clone();
                //Grab the start/end times for each stroke
                if (theStroke.ContainsPropertyData(startTime))
                    strokeBegin = (long)theStroke.GetPropertyData(startTime);
                if (theStroke.ContainsPropertyData(endTime))
                    strokeEnd = (long)theStroke.GetPropertyData(endTime);
                //Calculate the frequency/speed to playback the points in each stroke
                long frequency = (strokeEnd - strokeBegin)/ theStroke.StylusPoints.Count;

                StylusPointCollection spc = new StylusPointCollection();
                spc.Add(theStroke.StylusPoints[0]);
                //Create the new stroke from the styluspointcollection and add it to the InkCanvas
                CustomStroke newStroke = new CustomStroke(spc);
                newStroke.DrawingAttributes = theStroke.DrawingAttributes.Clone();

                //we will wait until it is time to playback a stroke
                while (myMediaElement.Position.Ticks < strokeBegin)
                {
                    Yield(strokeBegin - myMediaElement.Position.Ticks);
                }

                myInkCanvas.Strokes.Add(newStroke);
                for (int j = 1; j < theStroke.StylusPoints.Count; j++)
                {
                    newStroke.StylusPoints.Add(theStroke.StylusPoints[j]);
                    if (myMediaElement.Position.Ticks < strokeBegin + j * frequency)
                    {
                        // yield only if we are not running behind
                        Yield(frequency);
                    }
                }
            }
            myInkCanvas.EditingMode = InkCanvasEditingMode.Ink;        
        }

        private void Yield(long ticks)
        {
            long dtEnd = DateTime.Now.AddTicks(ticks).Ticks;
            while (DateTime.Now.Ticks < dtEnd)
            {
                this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, (DispatcherOperationCallback)delegate(object unused) { return null; }, null);
            }
        }
    }

}