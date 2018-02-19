using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using NotesListBox;

namespace NotesListBoxTest
{
    #region Test Classes



    #region Person class
    /// <summary>
    /// Test data, but with a collection of Note objects that
    /// are able to be shown/edited using the NotesListBoxControl
    /// </summary>
    public class Person
    {
        public String PersonId { get; set; }
        public ObservableCollection<Note> Notes { get; set; }

        public Person()
        {
            Notes = new ObservableCollection<Note>();
        }
    }
    #endregion

    #endregion
}
