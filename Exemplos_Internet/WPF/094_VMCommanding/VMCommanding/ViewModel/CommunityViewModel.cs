using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using VMCommanding.Model;

namespace VMCommanding.ViewModel
{
    /// <summary>
    /// A ViewModel class that exposes a collection of
    /// PersonViewModel objects, and provides a routed
    /// command that, when executed, kills the people.
    /// This class derives from CommandSink, which is
    /// why it does not directly implement the ICommandSink
    /// interface.  See PersonViewModel for an example
    /// of implementing ICommandSink directly.
    /// </summary>
    public class CommunityViewModel : CommandSink
    {
        #region Constructor

        public CommunityViewModel()
        {
            // Populate the community with some people.
            Person[] people = Person.GetPeople();
            IEnumerable<PersonViewModel> peopleView = people.Select(p => new PersonViewModel(p));
            this.People = new ReadOnlyCollection<PersonViewModel>(peopleView.ToArray());

            // Register the command that kills all the people.
            base.RegisterCommand(
                KillAllMembersCommand,
                param => this.CanKillAllMembers,
                param => this.KillAllMembers());
        }

        #endregion // Constructor

        #region People

        public ReadOnlyCollection<PersonViewModel> People { get; private set; }

        #endregion // People

        #region KillAllMembers

        public static readonly RoutedCommand KillAllMembersCommand = new RoutedCommand();

        public bool CanKillAllMembers
        {
            get { return this.People.Any(p => p.CanDie); }
        }

        public void KillAllMembers()
        {
            foreach (PersonViewModel personView in this.People)
                if (personView.CanDie)
                    personView.Die();
        }

        #endregion // KillAllMembers
    }
}