using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galador.Applications;

namespace MEFedMVVMDemo.Services.Models
{
	public class User : ViewModelBase
	{
		#region Name

		public string Name
		{
			get { return mName; }
			set
			{
				if (value == mName)
					return;
				mName = value;
				OnPropertyChanged(() => Name);
			}
		}
		string mName;

		#endregion

		#region Surname

		public string Surname
		{
			get { return mSurname; }
			set
			{
				if (value == mSurname)
					return;
				mSurname = value;
				OnPropertyChanged(() => Surname);
			}
		}
		string mSurname;

		#endregion

		#region Age

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
	}
}
