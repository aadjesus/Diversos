using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Collections.ObjectModel;
using System.Windows;

namespace NotesListBox
{
    public class NotesAdornerDecorator : AdornerDecorator
    {
        #region Data
        /// <summary>
        /// You MUST have an AdornerLayer to show the NotesListBoxControl
        /// as it was designed with the AdornerLayer in mind
        /// </summary>
        private AdornerLayer layer = null;
        /// <summary>
        /// A adorner to show the NotesListBoxControl in the AdornerLayer
        /// </summary>
        private NoteAdorner adorner = null;
        private ObservableCollection<Note> notes = new ObservableCollection<Note>();
        private FrameworkElement adornedElement;
        #endregion

        #region Ctor
        public NotesAdornerDecorator()
        {
            this.Loaded += new RoutedEventHandler(NotesAdornerDecorator_Loaded);
        }
        #endregion

        #region DPs

        /// <summary>
        /// DisplayNotes Dependency Property
        /// </summary>
        public static readonly DependencyProperty DisplayNotesProperty =
            DependencyProperty.Register("DisplayNotes", typeof(ObservableCollection<Note>), 
            typeof(NotesAdornerDecorator),
                new FrameworkPropertyMetadata(
                    new PropertyChangedCallback(OnDisplayNotesChanged)));

        /// <summary>
        /// Gets or sets the DisplayNotes property.  
        /// </summary>
        public ObservableCollection<Note> DisplayNotes
        {
            get { return (ObservableCollection<Note>)GetValue(DisplayNotesProperty); }
            set { SetValue(DisplayNotesProperty, value); }
        }

        /// <summary>
        /// Handles changes to the DisplayNotes property.
        /// </summary>
        private static void OnDisplayNotesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            NotesAdornerDecorator notesAdornerDecorator = (NotesAdornerDecorator)d;

            ((NotesAdornerDecorator)d).notes = (ObservableCollection<Note>)e.NewValue;
            
            if (notesAdornerDecorator.adorner != null & notesAdornerDecorator.layer != null)
                notesAdornerDecorator.layer.Remove(notesAdornerDecorator.adorner);

            notesAdornerDecorator.adorner = 
                new NoteAdorner(notesAdornerDecorator.adornedElement, notesAdornerDecorator.notes);
            notesAdornerDecorator.layer.Add(notesAdornerDecorator.adorner);

        }


        #endregion

        #region Routed Events

        #region Note Added Event
        public static readonly RoutedEvent NoteAddedEvent =
            EventManager.RegisterRoutedEvent(
            "NoteNoteAdded", RoutingStrategy.Bubble,
            typeof(NoteEventHandler),
            typeof(NotesAdornerDecorator));

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
            "NoteRemovedRemoved", RoutingStrategy.Bubble,
            typeof(NoteEventHandler),
            typeof(NotesAdornerDecorator));

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
            typeof(NotesAdornerDecorator));

        //add remove handlers
        public event NoteEventHandler NoteChanged
        {
            add { AddHandler(NoteChangedEvent, value); }
            remove { RemoveHandler(NoteChangedEvent, value); }
        }
        #endregion



        #endregion

        #region Private Methods

        private void NotesAdornerDecorator_Loaded(object sender, RoutedEventArgs e)
        {
            layer = this.AdornerLayer;

            //I am assuming that I need to create an actual Child element
            adornedElement = new FrameworkElement { Height = this.Height, Width = this.Width };
            this.Child = adornedElement;

            #region Wire up the actual NotesListBoxControl, and raise our own events for main UI
            //Wire up the Note Added Event, which will come from the 
            //NotesListBoxControl on the AdornerLayer
            EventManager.RegisterClassHandler(
                typeof(NotesListBoxControl),
                NotesListBoxControl.NoteAddedEvent,
                new NoteEventHandler(
                    (s, ea) =>
                    {
                        NoteEventArgs args = new NoteEventArgs(NoteRemovedEvent, ea.Note);
                        RaiseEvent(ea);
                    }));

            //Wire up the Note Removed Event, which will come from the 
            //NotesListBoxControl on the AdornerLayer
            EventManager.RegisterClassHandler(
                typeof(NotesListBoxControl),
                NotesListBoxControl.NoteRemovedEvent,
                new NoteEventHandler(
                    (s, ea) =>
                    {
                        NoteEventArgs args = new NoteEventArgs(NoteRemovedEvent, ea.Note);
                        RaiseEvent(ea); 
                    }));

            //Wire up the Note Changed Event, which will come from the 
            //NotesListBoxControl on the AdornerLayer
            EventManager.RegisterClassHandler(
                typeof(NotesListBoxControl),
                NotesListBoxControl.NoteChangedEvent,
                new NoteEventHandler(
                    (s, ea) =>
                    {
                        NoteEventArgs args = new NoteEventArgs(NoteChangedEvent, ea.Note);
                        RaiseEvent(ea); 
                    }));



            //Wire up the Close Notes Event, which will come from the 
            //NotesListBoxControl on the AdornerLayer
            EventManager.RegisterClassHandler(
                typeof(NotesListBoxControl),
                NotesListBoxControl.CloseNotesEvent,
                new EventHandler(
                    (s, ea) =>
                    {
                        if (adorner != null && layer != null)
                        {
                            layer.Remove(adorner);
                            adorner = null;
                        }
                    }));
            #endregion
        }
        #endregion

        #region Overrides
        protected override void OnRenderSizeChanged(System.Windows.SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            if (adornedElement != null)
            {
                adornedElement.Height = sizeInfo.NewSize.Height;
                adornedElement.Width = sizeInfo.NewSize.Width;
            }
 
            if (adorner!=null)
                adorner.ResizeToFillAvailableSpace(sizeInfo.NewSize);
        }
        #endregion
    }
}
