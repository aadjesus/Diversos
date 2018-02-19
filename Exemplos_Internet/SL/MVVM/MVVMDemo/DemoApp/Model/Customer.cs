using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using DemoApp.Properties;
using Galador.Applications;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Galador.Applications.Validation;

namespace DemoApp.Model
{
	/// <summary>
	/// Represents a customer of a company.  This class
	/// has built-in validation logic. It is wrapped
	/// by the CustomerViewModel class, which enables it to
	/// be easily displayed and edited by a WPF user interface.
	/// </summary>
	public class Customer : ViewModelBase
	{
		public Customer()
		{
		}

		public Customer(decimal totalSales, string firstName, string lastName, bool isCompany, string email)
		{
			TotalSales = totalSales;
			FirstName = firstName;
			LastName = lastName;
			IsCompany = isCompany;
			Email = email;
		}

		#region Email

		/// <summary>
		/// Gets/sets the e-mail address for the customer.
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "Customer_Error_MissingEmail")]
		// This regex pattern came from: http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx
		[RegularExpression(
			@"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$"
			, ErrorMessageResourceType = typeof(Strings)
			, ErrorMessageResourceName = "Customer_Error_InvalidEmail")]
		public string Email
		{
			get { return mEmail; }
			set
			{
				if (value == mEmail)
					return;
				mEmail = value;
				OnPropertyChanged(() => Email);
			}
		}
		string mEmail;

		#endregion

		#region FirstName

		/// <summary>
		/// Gets/sets the customer's first name.  If this customer is a 
		/// company, this value stores the company's name.
		/// </summary>
		[Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "Customer_Error_MissingFirstName")]
		public string FirstName
		{
			get { return mFirstName; }
			set
			{
				if (value == mFirstName)
					return;
				mFirstName = value;
				OnPropertyChanged(() => FirstName);
			}
		}
		string mFirstName;

		#endregion

		#region IsCompany

		/// <summary>
		/// Gets/sets whether the customer is a company or a person.
		/// The default value is false.
		/// </summary>
		public bool IsCompany
		{
			get { return mIsCompany; }
			set
			{
				if (value == mIsCompany)
					return;
				mIsCompany = value;
				OnPropertyChanged(() => IsCompany);
			}
		}
		bool mIsCompany;

		#endregion

		#region LastName, LastNameErrors()

		/// <summary>
		/// Gets/sets the customer's last name.  If this customer is a 
		/// company, this value is not set.
		/// </summary>
		[DelegateValidation("LastNameErrors")]
		public string LastName
		{
			get { return mLastName; }
			set
			{
				if (value == mLastName)
					return;
				mLastName = value;
				OnPropertyChanged(() => LastName);
			}
		}
		string mLastName;

		public string LastNameErrors()
		{
			if (this.IsCompany)
			{
				if (!string.IsNullOrWhiteSpace(this.LastName))
					return Strings.Customer_Error_CompanyHasNoLastName;
			}
			else
			{
				if (string.IsNullOrWhiteSpace(this.LastName))
					return Strings.Customer_Error_MissingLastName;
			}
			return null;
		}

		#endregion

		#region TotalSales

		/// <summary>
		/// Returns the total amount of money spent by the customer.
		/// </summary>
		[OpenRangeValidation(0, null, ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = "Customer_TotalSales_Negative")]
		public decimal TotalSales
		{
			get { return mTotalSales; }
			set
			{
				if (value == mTotalSales)
					return;
				mTotalSales = value;
				OnPropertyChanged(() => TotalSales);
			}
		}
		decimal mTotalSales;

		#endregion

		public bool IsValid { get { return ObjectErrors() == null; } }

		internal void RaisePropertyChanged<TProperty>(Expression<Func<Customer, TProperty>> p)
		{
			base.OnPropertyChanged(p);
		}
	}
}