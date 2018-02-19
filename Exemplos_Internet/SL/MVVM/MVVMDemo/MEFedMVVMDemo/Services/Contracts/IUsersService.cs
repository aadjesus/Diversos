using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEFedMVVMDemo.Services.Models;

namespace MEFedMVVMDemo.Services.Contracts
{
    public interface IUsersService
    {
        IList<User> GetAllUsers();
    }
}
