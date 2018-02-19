using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using VMCommanding.Model;

namespace VMCommanding.ViewModel
{
    /// <summary>
    /// A ViewModel class that wraps a Person object
    /// and provides some UI-specific behavior via
    /// routed commands, such as speaking and dying.
    /// This class uses a CommandSink object, because 
    /// it does not derive from that class.  See
    /// the CommunityViewModel for an example of deriving
    /// from CommandSink.
    /// </summary>
    public class PersonViewModel :
        ICommandSink,
        INotifyPropertyChanged
    {
        #region Data

        readonly CommandSink _commandSink;
        readonly Person _person;        

        #endregion // Data

        #region Constructor

        public PersonViewModel(Person person)
        {
            _person = person;

            _commandSink = new CommandSink();

            _commandSink.RegisterCommand(
                DieCommand, 
                param => this.CanDie,
                param => this.Die());

            _commandSink.RegisterCommand(
                SpeakCommand,
                param => this.CanSpeak,
                param => this.Speak(param as string));
        }

        #endregion // Constructor

        #region Person Properties

        public int Age
        {
            get { return _person.Age; }
        }

        public string Name
        {
            get { return _person.Name; }
        }

        #endregion // Person Properties

        #region Die

        public static readonly RoutedCommand DieCommand = new RoutedCommand();

        public bool CanDie
        {
            get { return _person.IsAlive; }
        }

        public void Die()
        {
            _person.IsAlive = false;

            this.OnPropertyChanged("CanDie");
            this.OnPropertyChanged("CanSpeak");
        }

        #endregion // Die

        #region Speak

        public static readonly RoutedCommand SpeakCommand = new RoutedCommand();

        public bool CanSpeak
        {
            get { return _person.IsAlive; }
        }

        public void Speak(string whatToSay)
        {
            string msg = whatToSay ?? String.Empty;
            string title = _person.Name + " says...";
            MessageBox.Show(whatToSay, title);
        }

        #endregion // Speak        

        #region ICommandSink Members

        public bool CanExecuteCommand(ICommand command, object parameter, out bool handled)
        {
            return _commandSink.CanExecuteCommand(command, parameter, out handled);
        }

        public void ExecuteCommand(ICommand command, object parameter, out bool handled)
        {
            _commandSink.ExecuteCommand(command, parameter, out handled);
        }

        #endregion // ICommandSink Members

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion // INotifyPropertyChanged Members
    }
}