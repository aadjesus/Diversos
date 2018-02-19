using System;
using System.ComponentModel;
using System.Windows.Input;
using DemoApp.DataAccess;
using DemoApp.Model;
using DemoApp.Properties;
using Galador.Applications;
using Galador.Applications.Validation;

namespace DemoApp.ViewModel
{
	/// <summary>
	/// A UI-friendly wrapper for a Customer object.
	/// </summary>
	public class CustomerViewModel : WorkspaceViewModel
	{
		readonly Customer _customer;
		readonly ICustomerRepository _customerRepository;

		#region Constructor

		public CustomerViewModel(Customer customer, ICustomerRepository customerRepository)
		{
			if (customer == null)
				throw new ArgumentNullException("customer");

			if (customerRepository == null)
				throw new ArgumentNullException("customerRepository");

			_customer = customer;
			_customerRepository = customerRepository;
			_customer.PropertyChanged += delegate
			{
				if(_saveCommand != null)
					_saveCommand.RaiseCanExecuteChanged(); 
			};

			if (IsNewCustomer)
			{
				CustomerType = Strings.CustomerViewModel_CustomerTypeOption_NotSpecified;
			}
			else
			{
				CustomerType = Customer.IsCompany
					? Strings.CustomerViewModel_CustomerTypeOption_Company
					: Strings.CustomerViewModel_CustomerTypeOption_Person;
				TotalSales = Customer.TotalSales.ToString();
			}
		}

		#endregion // Constructor

		public Customer Customer { get { return _customer; } }

		#region CustomerType, CustomerTypeOptions, CustomerTypeError()

		/// <summary>
		/// Gets/sets a value that indicates what type of customer this is.
		/// This property maps to the IsCompany property of the Customer class,
		/// but also has support for an 'unselected' state.
		/// </summary>
		[DelegateValidation("CustomerTypeError")]
		public string CustomerType
		{
			get { return _customerType; }
			set
			{
				if (value == _customerType || String.IsNullOrEmpty(value))
					return;

				_customerType = value;

				if (_customerType == Strings.CustomerViewModel_CustomerTypeOption_Company)
				{
					_customer.IsCompany = true;
				}
				else if (_customerType == Strings.CustomerViewModel_CustomerTypeOption_Person)
				{
					_customer.IsCompany = false;
				}

				base.OnPropertyChanged(() => CustomerType);

				// Since this ViewModel object has knowledge of how to translate
				// a customer type (i.e. text) to a Customer object's IsCompany property,
				// it also must raise a property change notification when it changes
				// the value of IsCompany.  The LastName property is validated 
				// differently based on whether the customer is a company or not,
				// so the validation for the LastName property must execute now.
				_customer.RaisePropertyChanged(c => c.LastName);
			}
		}
		string _customerType;

		public string CustomerTypeError()
		{
			if (this.CustomerType == Strings.CustomerViewModel_CustomerTypeOption_Company ||
			   this.CustomerType == Strings.CustomerViewModel_CustomerTypeOption_Person)
				return null;

			return Strings.CustomerViewModel_Error_MissingCustomerType;
		}

		/// <summary>
		/// Returns a list of strings used to populate the Customer Type selector.
		/// </summary>
		public string[] CustomerTypeOptions
		{
			get
			{
				if (_customerTypeOptions == null)
				{
					_customerTypeOptions = new string[]
					{
						Strings.CustomerViewModel_CustomerTypeOption_NotSpecified,
						Strings.CustomerViewModel_CustomerTypeOption_Person,
						Strings.CustomerViewModel_CustomerTypeOption_Company
					};
				}
				return _customerTypeOptions;
			}
		}
		string[] _customerTypeOptions;

		#endregion

		#region TotalSales

		[ConversionValidation(typeof(double), ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "Customer_TotalSales_NotANumber")]
		[PropertyValidation("Customer.TotalSales")]
		public string TotalSales
		{
			get { return mTotalSales; }
			set
			{
				if (value == mTotalSales)
					return;
				mTotalSales = value;

				try { Customer.TotalSales = (decimal)ConversionValidationAttribute.TryConvert(value, typeof(decimal)); }
				catch { }

				OnPropertyChanged(() => TotalSales);
			}
		}
		string mTotalSales;

		#endregion

		#region DisplayName

		public override string DisplayName
		{
			get
			{
				if (this.IsNewCustomer)
				{
					return Strings.CustomerViewModel_DisplayName;
				}
				else if (_customer.IsCompany)
				{
					return _customer.FirstName;
				}
				else
				{
					return String.Format("{0}, {1}", _customer.LastName, _customer.FirstName);
				}
			}
		}

		#endregion

		#region IsSelected

		/// <summary>
		/// Gets/sets whether this customer is selected in the UI.
		/// </summary>
		public bool IsSelected
		{
			get { return _isSelected; }
			set
			{
				if (value == _isSelected)
					return;

				_isSelected = value;

				base.OnPropertyChanged(() => IsSelected);
			}
		}
		bool _isSelected;

		#endregion

		#region SaveCommand, Save()

		/// <summary>
		/// Returns a command that saves the customer.
		/// </summary>
		public ICommand SaveCommand
		{
			get
			{
				if (_saveCommand == null)
					_saveCommand = new DelegateCommand(Save, () => CanSave);
				return _saveCommand;
			}
		}
		DelegateCommand _saveCommand;

		/// <summary>
		/// Saves the customer to the repository.  This method is invoked by the SaveCommand.
		/// </summary>
		public void Save()
		{
			if (!_customer.IsValid)
				throw new InvalidOperationException(Strings.CustomerViewModel_Exception_CannotSave);

			if (this.IsNewCustomer)
				_customerRepository.AddCustomer(_customer);
			
			base.OnPropertyChanged("DisplayName");
		}

		#endregion

		#region private: IsNewCustomer, CanSave, PropertyErrors()

		/// <summary>
		/// Returns true if this customer was created by the user and it has not yet
		/// been saved to the customer repository.
		/// </summary>
		bool IsNewCustomer
		{
			get { return !_customerRepository.ContainsCustomer(_customer); }
		}

		/// <summary>
		/// Returns true if the customer is valid and can be saved.
		/// </summary>
		bool CanSave
		{
			get { return String.IsNullOrEmpty(this.ObjectErrors()) && _customer.IsValid; }
		}

		protected override string ObjectErrors()
		{
			var ce = ((IDataErrorInfo)_customer).Error;
			var my = base.ObjectErrors();
			if (ce == null && my == null)
				return null;
			if (ce == null)
				return my;
			if (my == null)
				return ce;
			return my + ", " + ce;
		}

		protected override string PropertyErrors(string propertyName)
		{
			// Dirty the commands registered with CommandManager,
			// such as our Save command, so that they are queried
			// to see if they can execute now.
			if (_saveCommand != null)
				_saveCommand.RaiseCanExecuteChanged();

			return base.PropertyErrors(propertyName);
		}

		#endregion // Private Helpers
	}
}