using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Galador.Applications;
using Galador.Applications.Validation;

namespace Galador.Application.Tests
{
	public class Person : ViewModelBase
	{
		#region Name

		[Required]
		public string Name
		{
			get { return mName; }
			set
			{
				if (value == mName)
					return;
				mName = value;
				OnPropertyChanged(() => Name);

				UpdateInitials();
			}
		}
		string mName;

		#endregion

		#region LastName

		[Required]
		public string LastName
		{
			get { return mLastName; }
			set
			{
				if (value == mLastName)
					return;
				mLastName = value;
				OnPropertyChanged(() => LastName);

				UpdateInitials();
			}
		}
		string mLastName;

		#endregion

		#region Age

		[OpenRangeValidation(0, null)]
		public int Age
		{
			get { return mAge; }
			set
			{
				if (value == mAge)
					return;
				mAge = value;
				OnPropertyChanged(() => Age);
			}
		}
		int mAge;

		#endregion

		#region Boss

		public Person Boss
		{
			get { return mBoss; }
			set
			{
				if (value == mBoss)
					return;
				mBoss = value;
				OnPropertyChanged(() => Boss);
			}
		}
		Person mBoss;

		#endregion

		#region Initials, InitialsError()

		[PropertyValidation("Name")]
		[PropertyValidation("LastName")]
		[DelegateValidation("InitialsError")]
		public string Initials
		{
			get { return mInitials; }
			private set
			{
				if (value == mInitials)
					return;
				mInitials = value;
				OnPropertyChanged(() => Initials);
			}
		}
		string mInitials;

		public string InitialsError()
		{
			if (Initials == null || Initials.Length != 2)
				return "Initials is not a 2 letter string";
			return null;
		}

		void UpdateInitials()
		{
			if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(LastName))
				Initials = "" + Name[0] + LastName[0];
			else
				Initials = null;
		}

		#endregion

		public void Save()
		{
			if (Saved != null)
				Saved(this, EventArgs.Empty);
		}
		public event EventHandler Saved;

		protected override void OnPropertyChanged(string propertyName)
		{
			base.OnPropertyChanged("CanSave");
			// CanSave first, because the lastPropertyChanged is used as a check in unit test
			base.OnPropertyChanged(propertyName);
		}

		public bool CanSave
		{
			get { return ObjectErrors() == null; }
		}
	}
}
