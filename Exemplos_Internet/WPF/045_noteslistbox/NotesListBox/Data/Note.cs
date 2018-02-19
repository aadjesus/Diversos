using System.ComponentModel;
using System;

namespace NotesListBox
{
    /// <summary>
    /// A simple Bindable Note object, which is used within
    /// the <see cref="NotesListBoxControl">NotesListBoxControl</see>
    /// </summary>
    public class Note : INotifyPropertyChanged
    {
        #region Data
        private String data;
        private DateTime dateCreated;
        #endregion

        #region Public Properties

        public DateTime DateCreated
        {
            get { return dateCreated; }
            set
            {
                if (value == dateCreated)
                    return;

                dateCreated = value;
                this.OnPropertyChanged("DateCreated");
            }
        }
        
        public String Data
        {
            get { return data; }
            set
            {
                if (value == data)
                    return;

                data = value;
                this.OnPropertyChanged("Data");
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Overrides
        public override string ToString()
        {
            return this.DateCreated.ToShortDateString() + this.Data;
        }
        #endregion
    }
}
