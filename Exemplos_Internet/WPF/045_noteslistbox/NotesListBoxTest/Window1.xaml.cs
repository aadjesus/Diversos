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
using NotesListBox;
using System.Collections.ObjectModel;

namespace NotesListBoxTest
{
    /// <summary>
    /// Shows an instance of the NotesListBoxControl in
    /// the AdornerLayer associated with this Window.
    /// The NotesListBoxControl was designed to work with an AdornerLayer.
    /// 
    /// Though it is a stand alone control in its own right, so can be used
    /// as such. But when used as a standalone control, it will have to be part
    /// of the Window/Control layout. Using it in the AdornerLayer conserves the
    /// screen real estate of the Window/Control using the NotesListBoxControl, as
    /// it shown in the layer above these control (the AdornerLayer basically).
    /// 
    /// I have included a small test setup where a Person object has
    /// a collection of Note objects that can be shown and edited using the
    /// NotesListBoxControl
    /// </summary>
    public partial class Window1 : Window
    {

        #region Ctor
        public Window1()
        {
            InitializeComponent();
            this.Loaded +=new RoutedEventHandler(Window1_Loaded);
        }
        #endregion

        #region Private Methods
        private void  Window1_Loaded(object sender, RoutedEventArgs e)
        {

            #region Create Dummy Data

            ObservableCollection<Person> people = new ObservableCollection<Person>();

            Person p = new Person { PersonId = "sacha" };
            p.Notes.Add(new Note { DateCreated = DateTime.Now, Data = "sacha note1" });
            p.Notes.Add(new Note { DateCreated = DateTime.Now, Data = "sacha note2" });
            p.Notes.Add(new Note { DateCreated = DateTime.Now, Data = "sacha note3" });
            p.Notes.Add(new Note { DateCreated = DateTime.Now, Data = "xisro" });
            people.Add(p);


            Person p2 = new Person { PersonId = "bert" };
            p2.Notes.Add(new Note { DateCreated = DateTime.Now, Data = "bert note1" });
            people.Add(p2);

            lstPeople.ItemsSource = people;

            #endregion


            #region Wire up the NotesAdornerDecorator Events 
            
            //you could use these to persist Current Notes to DB

            //Note Added
            notesAdornerDecorator.NoteAdded += (s, ea) =>
            {
                Console.WriteLine(CreateNoteMessage(ea.Note));
            };

            //Note Removed
            notesAdornerDecorator.NoteRemoved += (s, ea) =>
            {
                Console.WriteLine(CreateNoteMessage(ea.Note));
            };

            //Note Changed
            notesAdornerDecorator.NoteChanged += (s, ea) =>
            {
                Console.WriteLine(CreateNoteMessage(ea.Note));
            };
            #endregion
        }


        private String CreateNoteMessage(Note note)
        {
            StringBuilder sb = new StringBuilder(1000);
            sb.AppendLine("This is where you could Persist the new Note");
            sb.AppendFormat("Date {0}{1}{2}",
                note.DateCreated,
                Environment.NewLine,
                note.Data);
            return sb.ToString();
        }

        private void lstPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            notesAdornerDecorator.DisplayNotes = (lstPeople.SelectedItem as Person).Notes;
        }





        #endregion
    }
}
