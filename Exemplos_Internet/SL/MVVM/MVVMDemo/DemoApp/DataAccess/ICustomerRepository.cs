using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DemoApp.Model;

namespace DemoApp.DataAccess
{
	public interface ICustomerRepository
	{
		event EventHandler<CustomerAddedEventArgs> CustomerAdded;
		void AddCustomer(Customer customer);
		bool ContainsCustomer(Customer customer);
		List<Customer> GetCustomers();
	}
}
