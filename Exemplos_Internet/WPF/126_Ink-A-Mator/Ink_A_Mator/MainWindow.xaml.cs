/****************************************************************************
 *                                                                          *
 *      Created By: Ernie Booth                                             *
 *      Contact: ebooth@microsoft.com - http://blogs.msdn.com/ebooth        *
 *      Last Modified: 6/23/2006                                            *
 *                                                                          *
 * **************************************************************************/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Ink;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

namespace Ink_A_Mator
{
    /// <summary>
    /// Allows for the creation, saving and loading of animations.
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Window Creation / Initialization
        
        /// <summary>
        /// Initialize and static fields
        /// </summary>
        static MainWindow()
        {
            // Initialize the ChangeColor command.
            ChangeColor = new RoutedCommand("ChangeColor", typeof(MainWindow));
        }

        /// <summary>
        /// Creates the Main Window.
        /// </summary>
        public MainWindow()
        {
            // We want the application to close when this window is closed.  This is important because we have an owned window that hidden(ColorPalette).
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Application.Current.MainWindow = this;

            InitializeComponent();
            
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);

            // Setup our Render Loop and timing
            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
            watch = Stopwatch.StartNew();

            CurrentFrame = 0;
            
            this.DataContext = this;

            // Bind the commands from the ColorPalette Window
            SetupChangeColorCommandBinding();
        }

        /// <summary>
        /// Now that the controls we host our loaded do some initialization on them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetupCommands();

            // Turn on Bezier smoothing.  This should probably be an option.
            this.Paper.DefaultDrawingAttributes.FitToCurve = true;

            // We want to know when we have made any changes to our document.
            this.Paper.StrokeErased += new RoutedEventHandler(Paper_StrokeErased);
            AddHandler(InkCanvas.StrokeCollectedEvent, new InkCanvasStrokeCollectedEventHandler(Paper_StrokeCollected), true);

            // We want to update the current frame on 'Enter' press.
            this.CurrentFrameBox.KeyDown += new KeyEventHandler(CurrentFrameBox_KeyDown);

            // Play the default.flpbk animation if we have one.
            AutoPlay();
        }

        /// <summary>
        /// Setup the commands we deal with.
        /// </summary>
        void SetupCommands()
        {
            // Open Command
            CommandBinding cb = new CommandBinding(ApplicationCommands.Open);
            cb.CanExecute += new CanExecuteRoutedEventHandler(SetCanExecuteTrue);
            cb.Executed += new ExecutedRoutedEventHandler(OpenExecute);
            CommandBindings.Add(cb);

            // New Command
            cb = new CommandBinding(ApplicationCommands.New);
            cb.CanExecute += new CanExecuteRoutedEventHandler(SetCanExecuteTrue);
            cb.Executed += new ExecutedRoutedEventHandler(NewExecute);
            CommandBindings.Add(cb);

            // Save Command
            cb = new CommandBinding(ApplicationCommands.Save);
            cb.CanExecute += new CanExecuteRoutedEventHandler(SaveCanExecute);
            cb.Executed += new ExecutedRoutedEventHandler(OnSave);
            CommandBindings.Add(cb);
        }

        /// <summary>
        /// Some commands we want to work regardless of the state of the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SetCanExecuteTrue(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion

        #region File Load / Save

        /// <summary>
        /// Open a file given a fileName
        /// </summary>
        /// <param name="fileName">The file to open</param>
        private void OpenFile(string fileName)
        {
            // Clear our current document.
            OnClear();

            // Open our file using the Open Packaging APIs
            using (Package p = ZipPackage.Open(fileName, FileMode.Open))
            {
                // We pull frames by part so we need the package relationship.
                PackageRelationshipCollection rels = p.GetRelationshipsByType(ResourceRelationshipType);

                // Walk through our frames in part form.
                foreach (PackageRelationship rel in rels)
                {
                    // Get the address of this frame part.
                    Uri uri = PackUriHelper.ResolvePartUri(
                        new Uri("/", UriKind.Relative), rel.TargetUri);

                    // Get the frame part with the address.
                    PackagePart part = p.GetPart(uri);

                    // Put the frame to memory
                    using (Stream s = part.GetStream())
                    {
                        // A frame just consists of a stroke collection so load it.
                        this.Paper.Strokes = new StrokeCollection(s);

                        // Close our stream.
                        s.Close();

                        // Move to the next frame.
                        OnNextFrame(this, new RoutedEventArgs());
                    }
                }

                // Move back to the last frame.
                OnPreviousFrame(this, new RoutedEventArgs());
            }
        }

        // This is our resource type, it is used to load by type.
        private const string ResourceRelationshipType =
           @"http://schemas.microsoft.com/samples/Ink-A-Mator/Required-Resource";

        /// <summary>
        /// Save our our animation using the OpenPackaging APIs
        /// </summary>
        /// <returns>True == Saved sucessfully</returns>
        bool Save()
        {
            // See if we already have a file name we set this field to string.Empty on 'New'.
            // if we don't then present the save dialog to the user.
            if (fileName == string.Empty)
            {
                SaveFileDialog diag = new SaveFileDialog();

                diag.Filter = "Ink_A_Mator|*.flpbk";

                diag.AddExtension = true;

                if (diag.ShowDialog(this).Value == false)
                {
                    return false;
                }

                fileName = diag.FileName;
            }
            
            // Save the current frame.
            if (this.Paper.Strokes.Count != 0)
            {
                SaveFrame();
            }

            // Create a new ZipPackage and save the animation in it.
            using (Package p = ZipPackage.Open(fileName, FileMode.Create))
            {
                // Save out each of our frames.
                for (int i = 0; i < this.frames.Count; i++)
                {
                    // We need to create a part to save to.
                    PackagePart part = p.CreatePart(PackUriHelper.CreatePartUri(new Uri(string.Format("Frame{0}{1}", i, ".frm"), UriKind.Relative)), "Ink/WPF");

                    // Create a relationship for our part so that we can walk through relationships on open.
                    p.CreateRelationship(part.Uri, TargetMode.Internal, ResourceRelationshipType);

                    // Get a memory stream to save our part.
                    using (Stream s = part.GetStream())
                    {
                        // save each frame into the stream
                        this.frames[i].Save(s);

                        // don't forget to close the stream.
                        s.Close();
                    }
                }
            }

            // We have saved.
            saved = true;

            // return our status.
            return saved;
        }

        /// <summary>
        /// Routed event handler for Saving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSave(object sender, RoutedEventArgs e)
        {
            Save();
        }

        /// <summary>
        /// Routed event handler for loading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnLoad(object sender, RoutedEventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();

            diag.Filter = "Ink_A_Mator(*.flpbk)|*.flpbk";

            if (diag.ShowDialog(this).Value == false)
            {
                return;
            }

            OpenFile(diag.FileName);

        }

        // We want to save our file name so we don't have to prompt the user everytime.
        string fileName = string.Empty;

        // Has the document been saved?
        bool saved = false;

        /// <summary>
        /// Play default.flpbk if it exists
        /// </summary>
        private void AutoPlay()
        {
            if (File.Exists("default.flpbk"))
            {
                OpenFile("default.flpbk");

                OnPlay(null, null);
            }
        }

        /// <summary>
        /// Our Save event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSave(object sender, ExecutedRoutedEventArgs e)
        {
            OnSave(sender, new RoutedEventArgs());
        }

        /// <summary>
        /// Don't allow saving unless we have some content.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SaveCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.frames.Count != 0 || this.Paper.Strokes.Count != 0)
            {
                e.CanExecute = true;
            }
        }

        /// <summary>
        /// The Routed event handler for opening an animation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OpenExecute(object sender, ExecutedRoutedEventArgs e)
        {
            OnLoad(sender, new RoutedEventArgs());
        }

        #endregion

        #region Frame Functionality

        // Our Frames
        List<StrokeCollection> frames = new List<StrokeCollection>();

        // The current frame.
        int currentFrame = 0;

        /// <summary>
        /// Gets or Sets the Current Frame.
        /// </summary>
        public int CurrentFrame
        {
            get { return currentFrame; }
            set
            {
                currentFrame = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentFrame"));
            }
        }

        /// <summary>
        ///  Change the current frame
        /// </summary>
        /// <param name="currentFrame">The frame to change to</param>
        void ChangeFrame(int frame)
        {
            if (this.frames.Count > frame && frame >= 0)
            {
                // Save the current frame.
                if (!playing)
                    SaveFrame();

                // Upade our current frame.
                CurrentFrame = frame;

                // Display the frame.
                this.Paper.Strokes = this.frames[frame].Clone();

                // Redraw our Onion layer.
                RenderOnionLayer(frame);
            }
            else
            {
                this.CurrentFrameBox.Text = CurrentFrame.ToString();
            }
        }

        /// <summary>
        /// Our Render loop for playing our animation back.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            // Only render if we are playing.
            if (playing)
            {
                long l = watch.ElapsedTicks;

                // Play at 24 frames a second.
                if (l > Stopwatch.Frequency / 24)
                {
                    // We are done playing through the frames
                    if (playingFrame >= this.frames.Count)
                    {
                        DonePlaying();
                        return;
                    }

                    // Advance the frame.
                    this.Player.Strokes = this.frames[playingFrame++];

                    // restart the stopwatch
                    watch.Reset();
                    watch.Start();
                }
            }
        }

        /// <summary>
        /// Change our state from playing.
        /// </summary>
        private void DonePlaying()
        {
            CurrentFrame = this.frames.Count - 1;

            playing = false;

            playingFrame = 0;

            this.OnionLayer.Visibility = Visibility.Visible;

            this.Paper.Visibility = Visibility.Visible;

            this.Player.Visibility = Visibility.Hidden;
            
        }

        /// <summary>
        /// Update when the user hits the 'Enter' key.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CurrentFrameBox_KeyDown(object sender, KeyEventArgs e)
        {
            int i = 0;

            if (int.TryParse(this.CurrentFrameBox.Text, out i))
            {
                if (e.Key == Key.Enter)
                {
                    ChangeFrame(i);
                }
            }
        }

        // Are we playing?
        bool playing = false;

        // The current frame we are playing.
        int playingFrame = 0;

        // The playing timer.
        Stopwatch watch;

        /// <summary>
        ///  Clears the current document.
        /// </summary>
        void OnClear()
        {
            CurrentFrame = 0;

            this.frames.Clear();

            this.Paper.Strokes.Clear();
        }

        /// <summary>
        /// The Mouse wheel was used to change the frame.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnChangeFrame(object sender, MouseWheelEventArgs e)
        {
            // Only advance one frame or go back one frame.
            if (e.Delta > 0)
            {
                OnNextFrame(this, new RoutedEventArgs());
            }
            else
            {
                OnPreviousFrame(this, new RoutedEventArgs());
            }
        }

        /// <summary>
        /// Advance to the next frame.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnNextFrame(object sender, RoutedEventArgs e)
        {
            SaveFrame();

            this.Paper.Strokes = new StrokeCollection();

            CurrentFrame++;

            if (frames.Count > CurrentFrame)
            {
                this.Paper.Strokes = frames[CurrentFrame];
            }

            RenderOnionLayer();
        }

        /// <summary>
        /// Put a frame in between this frame and the next frame.
        /// </summary>
        /// <param name="CurrentFrame"></param>
        /// <param name="strokeCollection"></param>
        private void InsertFrame(int CurrentFrame, StrokeCollection strokeCollection)
        {
            this.frames.Insert(CurrentFrame, strokeCollection);

            this.Paper.Strokes = this.frames[CurrentFrame];

            RenderOnionLayer();
        }

        /// <summary>
        /// Go back to the previous frame.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnPreviousFrame(object sender, RoutedEventArgs e)
        {
            if (CurrentFrame == 0)
            {
                return;
            }

            SaveFrame();

            this.Paper.Strokes = new StrokeCollection();

            if (frames.Count > 0)
            {
                CurrentFrame--;

                this.Paper.Strokes = frames[CurrentFrame];
            }

            RenderOnionLayer();
        }

        /// <summary>
        /// Save the current frame.
        /// </summary>
        private void SaveFrame()
        {
            if (frames.Count <= CurrentFrame)
            {
                frames.Add(this.Paper.Strokes);
            }
            else
            {
                frames[CurrentFrame] = this.Paper.Strokes;
            }
        }

        int onionFrameCount = 5;

        /// <summary>
        /// The number of onion layers we have.
        /// </summary>
        public int OnionFrameCount
        {
            get { return onionFrameCount; }
            set
            {
                onionFrameCount = value;

                PropertyChanged(this, new PropertyChangedEventArgs("OnionFrameCount"));

                RenderOnionLayer();
            }
        }

        /// <summary>
        /// Render the Onion layer.
        /// </summary>
        void RenderOnionLayer()
        {
            RenderOnionLayer(CurrentFrame);
        }

        /// <summary>
        /// Displays frames before the current frame as shadows of themselves getting lighter in gray as they get older.
        /// </summary>
        /// <param name="currentFrame"></param>
        void RenderOnionLayer(int currentFrame)
        {
            // Get starting frame for the onioning 
            int i = CurrentFrame - onionFrameCount;

            int count = currentFrame;

            // Make sure the starting onion frame isn't less then zero.
            if (i < 0)
            {
                // update the count if it is less then zero.  We only want to show up to this frame.
                count = i + onionFrameCount;

                i = 0;
            }

            // Create a new stroke collection for the onion layer.
            this.OnionLayer.Strokes = new StrokeCollection();

            int c = 0;
            
            // iterate through the onion layers.
            for (int j = i; j < count; j++)
            {
                // Make a copy of the real frame.
                this.OnionLayer.Strokes.Add(this.frames[j].Clone());

                // Figure out how many stokes we have, since we are going to add all the onion strokes to the same frame.
                c = this.OnionLayer.Strokes.Count - this.frames[j].Count;
                
                // Make sure we have strokes to color
                if (this.OnionLayer.Strokes.Count != 0)
                {
                    // Go through the strokes in this onion frame and change their color.
                    for (int k = c; k < this.OnionLayer.Strokes.Count; k++)
                    {
                        Color color = Colors.Gray;

                        // Calculate the shade of gray dynamically so we can change based on the onion layer count.
                        color.A = (byte)(color.A / (count - j + 1));

                        // Apply the color.
                        this.OnionLayer.Strokes[k].DrawingAttributes.Color = color;
                    }
                }
            }
        }

#endregion

        #region Document
        /// <summary>
        /// Routed event handler playing the animation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnPlay(object sender, RoutedEventArgs e)
        {
            if (this.frames.Count > 0)
            {
                this.OnionLayer.Visibility = Visibility.Hidden;
                this.Paper.Visibility = Visibility.Hidden;
                this.Player.Visibility = Visibility.Visible;

                this.playing = true;

                Stopwatch.StartNew();
            }
        }

        /// <summary>
        /// Routed event handler for duplicating the current frame.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnDuplicate(object sender, RoutedEventArgs e)
        {
            OnDuplicate();
        }

        /// <summary>
        /// Duplicates the current frame and prompts the user where they would like it placed.
        /// </summary>
        private void OnDuplicate()
        {
            // Is there are frame after this?  If so does it have ink?
            if (this.frames.Count > CurrentFrame + 1 && this.frames[CurrentFrame + 1].Count != 0)
            {
                bool? result = new DuplicateDialog().ShowDialog();

                if (!result.HasValue)
                {
                    return;
                }
                else if (result.Value == false)
                {
                    InsertFrame(CurrentFrame, this.frames[CurrentFrame].Clone());

                    OnNextFrame(this, new RoutedEventArgs());

                    return;
                }
            }

            OnNextFrame(this, new RoutedEventArgs());

            this.Paper.Strokes = this.frames[CurrentFrame - 1].Clone();
        }

       /// <summary>
       /// Creates a new document.
       /// </summary>
        void OnNew()
        {
            if (!saved)
            {
                MessageBoxResult result = MessageBox.Show("Would you like to save your work?", "Unsaved work", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);

                if (result == MessageBoxResult.Yes)
                {
                    if (Save() == false)
                    {
                        return;
                    }
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    return;
                }
            }

            OnClear();

            this.OnionLayer.Strokes.Clear();
            this.Player.Strokes.Clear();

            fileName = string.Empty;
        }

        /// <summary>
        /// RoutedEvent handler for creating a new document.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void NewExecute(object sender, ExecutedRoutedEventArgs e)
        {
            OnNew();
        }

        #endregion

        #region Ink

        // We change mode on right click so we want to beable to change back to the other mode when we let go.
        InkCanvasEditingMode oldMode;

        // A gesture has started?
        bool gestureStarted = false;

        /// <summary>
        /// Make our saved state dirty.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Paper_StrokeErased(object sender, RoutedEventArgs e)
        {
            saved = false;
        }

        /// <summary>
        /// Make our saved state dirty.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Paper_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            saved = false;
        }

        /// <summary>
        /// Deal with Inking mode changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnEditingModeChanged(object sender, RoutedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if (sender != null && Paper != null)
            {
                ComboBoxItem item = cb.SelectedValue as ComboBoxItem;

                switch ((string)item.Content)
                {
                    case "Erase Point":
                        Paper.EditingMode = InkCanvasEditingMode.EraseByPoint;
                        break;
                    case "Ink":
                        Paper.EditingMode = InkCanvasEditingMode.Ink;
                        break;
                    case "Erase Stroke":
                        Paper.EditingMode = InkCanvasEditingMode.EraseByStroke;
                        break;
                }
            }

        }

        /// <summary>
        /// Change to gesture only mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnGestureStart(object sender, MouseButtonEventArgs e)
        {
            oldMode = Paper.EditingMode;

            gestureStarted = true;

            Paper.EditingMode = InkCanvasEditingMode.GestureOnly;
        }

        /// <summary>
        /// Change Frames.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnGesture(object sender, InkCanvasGestureEventArgs e)
        {
            ReadOnlyCollection<GestureRecognitionResult> results = e.GetGestureRecognitionResults();

            foreach (GestureRecognitionResult result in results)
            {
                if (result.ApplicationGesture == ApplicationGesture.Right)
                {
                    OnNextFrame(this, new RoutedEventArgs());
                }
                else if (result.ApplicationGesture == ApplicationGesture.Left)
                {
                    OnPreviousFrame(this, new RoutedEventArgs());
                }
            }
        }

        /// <summary>
        /// Change back to the old mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnGestureStop(object sender, MouseButtonEventArgs e)
        {
            gestureStarted = false;

            oldPosition = null;

            Paper.EditingMode = oldMode;
        }

        /// <summary>
        /// Change the size of the stylus point.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (gestureStarted)
            {
                Point p = e.GetPosition(this.Paper);

                if (oldPosition != null)
                {
                    double x = p.X - oldPosition.Value.X;
                    double y = p.Y - oldPosition.Value.Y;

                    if (this.Paper.DefaultDrawingAttributes.Width + x >= 1)
                        this.Paper.DefaultDrawingAttributes.Width += x;

                    if (this.Paper.DefaultDrawingAttributes.Height + y >= 1)
                        this.Paper.DefaultDrawingAttributes.Height += y;
                }

                oldPosition = p;
            }
        }

        /// <summary>
        /// We need a delta to do the stylus point change.
        /// </summary>
        Point? oldPosition = null;

        #endregion

        #region Palette Functionality
        
        /// <summary>
        /// Apply palette choices to the paper.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnChooseColor(object sender, RoutedEventArgs e)
        {
            RadioButton el = (RadioButton)sender;

            Paper.DefaultDrawingAttributes.Color = ((SolidColorBrush)el.Background).Color;

        }

        /// <summary>
        /// Apply the palette choice to the paper.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnColorChanged(object sender, ColorChangedEventArgs e)
        {
            Paper.DefaultDrawingAttributes.Color = e.Color;
        }

        /// <summary>
        /// An event handler for color changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ColorChangedEventHandler(object sender, ColorChangedEventArgs e);

        /// <summary>
        /// 
        /// </summary>
        ColorPalette colorPaletteWindow = new ColorPalette();

        /// <summary>
        /// Show or Hide the Color Palette Window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnDisplayColorPalette(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            if (b == null)
                return;

            if (!colorPaletteWindow.IsVisible)
            {
                colorPaletteWindow.Owner = this;

                colorPaletteWindow.Show();
            }
            else
            {
                colorPaletteWindow.Hide();
            }
        }

        /// <summary>
        /// Commands for color change.
        /// </summary>
        static RoutedCommand changeColor;
        public static RoutedCommand ChangeColor
        {
            get { return MainWindow.changeColor; }
            set { MainWindow.changeColor = value; }
        }

        /// <summary>
        /// Setup the handling of the ChangeColor command.
        /// </summary>
        private void SetupChangeColorCommandBinding()
        {
            CommandBinding changeColorBinding = new CommandBinding(ChangeColor);

            changeColorBinding.CanExecute += new CanExecuteRoutedEventHandler(SetCanExecuteTrue);
            changeColorBinding.Executed += new ExecutedRoutedEventHandler(changeColorBinding_Executed);

            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), changeColorBinding);

        }

        /// <summary>
        /// Event Handler for ChangeColor Command.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void changeColorBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ColorChangedEventArgs args = e.Parameter as ColorChangedEventArgs;

            if (args != null)
            {
                OnColorChanged(sender, args);
            }
        }
    
        #endregion        

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}