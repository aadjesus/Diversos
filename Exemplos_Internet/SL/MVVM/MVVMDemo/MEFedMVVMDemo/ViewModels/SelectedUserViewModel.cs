using System.ComponentModel.Composition;
using Galador.Applications;
using MEFedMVVMDemo.Services.Models;

namespace MEFedMVVMDemo.ViewModels
{
	[Export]
	public class SelectedUserViewModel : ViewModelBase, IDesignTimeAware
	{
		public SelectedUserViewModel()
		{
			Notifications.Register(this, ThreadOption.UIThread);
		}

		#region SelectedUser

		public User SelectedUser
		{
			get { return mSelectedUser; }
			set
			{
				if (value == mSelectedUser)
					return;
				mSelectedUser = value;
				OnPropertyChanged(() => SelectedUser);
			}
		}
		User mSelectedUser;

		#endregion

		[NotificationHandler()]
		public void OnSelectedUserChanged(User selectedUser)
		{
			SelectedUser = selectedUser;
		}

		void IDesignTimeAware.DesignTimeInitialization()
		{
			SelectedUser = new User
			{
				Name = "Dupont",
				Surname = "Lloyd",
				Age = 38,
			};
		}
	}
}
