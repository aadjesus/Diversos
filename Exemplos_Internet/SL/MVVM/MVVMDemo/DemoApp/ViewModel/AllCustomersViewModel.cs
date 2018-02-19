using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using DemoApp.DataAccess;
using DemoApp.Properties;
using System.ComponentModel.Composition;
using DemoApp.Model;
using System.Windows.Input;
using Galador.Applications;

namespace DemoApp.ViewModel
{
	/// <summary>
	/// Represents a container of CustomerViewModel objects
	/// that has support for staying synchronized with the
	/// CustomerRepository.  This class also provides information
	/// related to multiple selected customers.
	/// </summary>
	public class AllCustomersViewModel : WorkspaceViewModel
	{
		readonly ICustomerRepository customerRepository;

		public AllCustomersViewModel(ICustomerRepository customerRepository)
		{
			if (customerRepository == null)
				throw new ArgumentNullException("customerRepository");
			this.customerRepository = customerRepository;
			base.DisplayName = Strings.AllCustomersViewModel_DisplayName;

			AllCustomers = new ObservableCollection<CustomerViewModel>();
			customerRepository.CustomerAdded += (o, args) => AddCustomer(args.NewCustomer);
			foreach (var c in customerRepository.GetCustomers())
				AddCustomer(c);

			Composition.Compose(this);
		}

		[Import("EditCustomerCommand", typeof(ICommand))]
		public ICommand EditCommand { get; set; }
		public ObservableCollection<CustomerViewModel> AllCustomers { get; private set; }
		public decimal TotalSelectedSales { get { return this.AllCustomers.Sum(custVM => custVM.IsSelected ? custVM.Customer.TotalSales : 0.0M); } }

		void AddCustomer(Customer c)
		{
			var cvm = new CustomerViewModel(c, customerRepository);
			cvm.PropertyChanged += OnCustomerPropertyChanged;
			AllCustomers.Add(cvm);
		}
		void OnCustomerPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			// When a customer is selected or unselected, we must let the
			// world know that the TotalSelectedSales property has changed,
			// so that it will be queried again for a new value.
			if (args.PropertyName == "IsSelected")
				this.OnPropertyChanged(() => TotalSelectedSales);
		}

		protected override void OnDispose(bool disposing)
		{
			if (!disposing)
				return;
			foreach (CustomerViewModel custVM in this.AllCustomers)
			{
				custVM.Dispose();
				custVM.PropertyChanged -= OnCustomerPropertyChanged;
			}
			this.AllCustomers.Clear();
		}
	}
}