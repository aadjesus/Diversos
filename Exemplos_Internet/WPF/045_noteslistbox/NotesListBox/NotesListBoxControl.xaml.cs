using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;

namespace NotesListBox
{

    public delegate void NoteEventHandler(object sender, NoteEventArgs e);

    /// <summary>
    /// A simple NotesListBoxControl, that hosts a number of 
    /// <see cref="NotesListBox.Note">NotesListBox.Note</see>s
    /// </summary>
    public partial class NotesListBoxControl : UserControl
    {
        #region Data
        private readonly int itemOffset = 110;
        private ObservableCollection<Note> notes;
        #endregion

        #region Ctor
        public NotesListBoxControl()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(NotesListBoxControl_Loaded);
        }
        #endregion

        #region Routed Events

        #region Note Added Event
        public static readonly RoutedEvent NoteAddedEvent =
            EventManager.RegisterRoutedEvent(
            "NoteAdded", RoutingStrategy.Bubble,
            typeof(NoteEventHandler),
            typeof(NotesListBoxControl));

        //add remove handlers
        public event NoteEventHandler NoteAdded
        {
            add { AddHandler(NoteAddedEvent, value); }
            remove { RemoveHandler(NoteAddedEvent, value); }
        }
        #endregion

        #region Note Removed Event
        public static readonly RoutedEvent NoteRemovedEvent =
            EventManager.RegisterRoutedEvent(
            "NoteRemoved", RoutingStrategy.Bubble,
            typeof(NoteEventHandler),
            typeof(NotesListBoxControl));

        //add remove handlers
        public event NoteEventHandler NoteRemoved
        {
            add { AddHandler(NoteRemovedEvent, value); }
            remove { RemoveHandler(NoteRemovedEvent, value); }
        }
        #endregion

        #region Note Changed Event
        public static readonly RoutedEvent NoteChangedEvent =
            EventManager.RegisterRoutedEvent(
            "NoteChanged", RoutingStrategy.Bubble,
            typeof(NoteEventHandler),
            typeof(NotesListBoxControl));

        //add remove handlers
        public event NoteEventHandler NoteChanged
        {
            add { AddHandler(NoteChangedEvent, value); }
            remove { RemoveHandler(NoteChangedEvent, value); }
        }
        #endregion

        #region Close Notes Event
        public static readonly RoutedEvent CloseNotesEvent =
            EventManager.RegisterRoutedEvent(
            "CloseNotes", RoutingStrategy.Bubble,
            typeof(EventHandler),
            typeof(NotesListBoxControl));

        //add remove handlers
        public event EventHandler CloseNotes
        {
            add { AddHandler(CloseNotesEvent, value); }
            remove { RemoveHandler(CloseNotesEvent, value); }
        }
        #endregion

        #endregion

        #region Public Properties
        public ObservableCollection<Note> Notes
        {
            get { return notes; }
            set
            {
                if (value != null)
                {
                    notes = value;
                    lstNotes.ItemsSource = notes;
                    this.notes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Notes_CollectionChanged);
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Resize the ListBoxControl to fill the available space 
        /// (The hosting Window/UserControl may have changed Size)
        /// </summary>
        /// <param name="heightToUse">The available Height that can be used</param>
        public void ResizeToFillAvailableSpace(double heightToUse)
        {
            gridRowForList.Height = new GridLength((double)
            heightToUse - gridRowForHeader.Height.Value, GridUnitType.Pixel);

        }
        #endregion

        #region Private Methods
        private void NotesListBoxControl_Loaded(object sender, RoutedEventArgs e)
        {
            ResizeToFillAvailableSpace(this.ActualHeight);
            lstNotes.Style = CreateStyleForListBox();
        }


        /// <summary>
        /// When The Notes collection changes create a new Style which contains a 
        /// Canvas just big enough for all the ListBox items.
        /// </summary>
        private void Notes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            lstNotes.Style = CreateStyleForListBox();
        }


        /// <summary>
        /// Creates a ListBox style that caters for a maximum size for the ListBox ItemsPanel based
        /// on the number of actual ListBox items
        /// </summary>
        /// <returns></returns>
        private Style CreateStyleForListBox()
        {
            Style style = new Style(typeof(ListBox));
            style.Setters.Add(new Setter(Control.BackgroundProperty, Brushes.Transparent));
            style.Setters.Add(new Setter(Control.BorderBrushProperty, Brushes.Transparent));
            FrameworkElementFactory feCanvas = new FrameworkElementFactory(typeof(Canvas));
            feCanvas.SetValue(Control.BackgroundProperty, Brushes.Transparent);
            feCanvas.SetValue(Control.WidthProperty, 200.0);
            feCanvas.SetValue(Control.MarginProperty, new Thickness(5));
            feCanvas.SetValue(Control.HeightProperty, (double)(lstNotes.Items.Count * itemOffset));
            ItemsPanelTemplate itemsPanelTemplate = new ItemsPanelTemplate(feCanvas);
            style.Setters.Add(new Setter(ItemsControl.ItemsPanelProperty, itemsPanelTemplate));
            return style;
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Note newNote = new Note
            {
                DateCreated = DateTime.Now.Subtract(TimeSpan.FromDays(1.0)),
                Data = string.Empty
            };
            newNote.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Note_PropertyChanged);
            Notes.Add(newNote);
            lstNotes.SelectedIndex = lstNotes.Items.Count - 1;

            NoteEventArgs args = new NoteEventArgs(NoteAddedEvent, newNote);
            RaiseEvent(args);    
        }


        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lstNotes.SelectedIndex >= 0)
            {
                Note noteToRemove = lstNotes.SelectedItem as Note;
                Notes.Remove(noteToRemove);

                NoteEventArgs args = new NoteEventArgs(NoteRemovedEvent, noteToRemove);
                RaiseEvent(args);  
            }
        }


        private void Note_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Note noteToUpdate = sender as Note;
            NoteEventArgs args = new NoteEventArgs(NoteChangedEvent, noteToUpdate);
            RaiseEvent(args);    
        }


        private void txt_GotFocus(object sender, RoutedEventArgs e)
        {
            ListBoxItem lbi = GetAncestorByType(
                   e.OriginalSource as DependencyObject,
                   typeof(ListBoxItem)) as ListBoxItem;

            if (lbi != null)
            {
                lstNotes.SelectedIndex =
                    lstNotes.ItemContainerGenerator.IndexFromContainer(lbi);
            }
        }
        

        private static DependencyObject GetAncestorByType(DependencyObject element, Type type)
        {
            if (element == null) return null;
            if (element.GetType() == type) return element;
            return GetAncestorByType(VisualTreeHelper.GetParent(element), type);
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(CloseNotesEvent);
            RaiseEvent(args);  
        }
        #endregion
    }
}
