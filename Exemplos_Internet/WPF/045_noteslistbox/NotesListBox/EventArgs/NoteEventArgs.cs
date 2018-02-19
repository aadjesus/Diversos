using System;
using System.Windows;

namespace NotesListBox
{
    public class NoteEventArgs : RoutedEventArgs
    {
        #region Instance fields
        public Note Note { get; private set; }
        #endregion

        #region Ctor
        public NoteEventArgs(RoutedEvent routedEvent, Note note) : base(routedEvent)
        {
            this.Note = note;
        }
        #endregion
    }
}
