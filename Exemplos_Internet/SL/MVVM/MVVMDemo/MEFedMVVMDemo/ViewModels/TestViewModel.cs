using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Galador.Applications;
using MEFedMVVMDemo.Services.Contracts;
using MEFedMVVMDemo.Services.Models;

namespace MEFedMVVMDemo.ViewModels
{
	[Export]
	public class TestViewModel : ViewModelBase
	{
		private readonly ObservableCollection<User> _users = new ObservableCollection<User>();
		public IList<User> Users { get { return _users; } }

		/// <summary>
		/// Gets or sets the selected user
		/// </summary>
		public User SelectedUser
		{
			get { return _selectedUser; }
			set 
			{ 
				_selectedUser = value;
				Notifications.Publish<User>(value);
				OnPropertyChanged(() => SelectedUser);
			}
		}
		private User _selectedUser;

		#region ModelState

		public string ModelState
		{
			get { return mModelState; }
			set
			{
				if (value == mModelState)
					return;
				mModelState = value;
				OnPropertyChanged(() => ModelState);
			}
		}
		string mModelState;

		#endregion


		readonly IUsersService userService;

		[ImportingConstructor]
		public TestViewModel(IUsersService userService)
		{
			this.userService = userService;

			LoadUsers();
		}

		void LoadUsers()
		{
			foreach (var item in userService.GetAllUsers())
			{
				if (SelectedUser == null)
					SelectedUser = item;
				_users.Add(item);
			}
			ModelState = "Welcome";
		}
	}
}
