using System.Collections.Generic;
using Galador.Applications;
using MEFedMVVMDemo.Services.Contracts;
using MEFedMVVMDemo.Services.Models;

namespace MEFedMVVMDemo.Services.DesignTime
{
	[ExportService(ServiceContext.DesignTime, typeof(IUsersService))]
	public class DesignTimeUsersService : IUsersService
	{
		#region IUsersService Members

		public IList<User> GetAllUsers()
		{
			return new List<User>
			{
				new User { Name = "DesignTime Marlon", Surname = "Grech", Age = 24 },
				new User { Name = "DesignTime Peter", Surname = "O'Hanlon", Age = 35 },
				new User { Name = "DesignTime Sasha", Surname = "Barber", Age = 28 },
				new User { Name = "DesignTime Glenn", Surname = "Block", Age = 31 },
				new User { Name = "DesignTime Josh", Surname = "Smith", Age = 60 },
				new User { Name = "DesignTime Lloyd", Surname = "Dupont", Age = 38 },
			};
		}

		#endregion
	}
}
