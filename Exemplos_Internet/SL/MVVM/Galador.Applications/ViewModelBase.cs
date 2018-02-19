using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
#if !WINDOWS_PHONE
using System.ComponentModel.DataAnnotations;
#endif

namespace Galador.Applications
{
	public class ViewModelBase : INotifyPropertyChanged
#if !WINDOWS_PHONE
		, IDataErrorInfo
#endif
	{
#if !WINDOWS_PHONE
		#region IDataErrorInfo (based on ValidationAttribute) + ValidateObject(), ValidateProperty()

		// based on ValidationAttribute
		string IDataErrorInfo.Error
		{
			get { return ObjectErrors(); }
		}
		
		/// <summary>
		/// Override this method to provide your own validation.
		/// The default implementation relied on <see cref="ValidationAttribute"/>s on the model's properties.
		/// </summary>
		/// <returns>An error message or null.</returns>
		protected virtual string ObjectErrors()
		{
			var context = new ValidationContext(this, null, null);
			var results = new List<ValidationResult>();
			if (Validator.TryValidateObject(this, context, results, true))
				return null;

			var sb = new StringBuilder();
			results.Aggregate(sb, (aSB, r) => 
			{
				if (aSB.Length > 0)
					aSB.Append(", ");
				aSB.Append(r.ErrorMessage);
				return aSB;
			});
			return sb.ToString();
		}

		string IDataErrorInfo.this[string propertyName]
		{
			get { return PropertyErrors(propertyName); }
		}

		protected string PropertyErrors<TProperty>(Expression<Func<TProperty>> property)
		{
			return PropertyErrors(PropertyUtils.GetName(property));
		}

		/// <summary>
		/// Override this method to provide your own validation.
		/// The default implementation relied on <see cref="ValidationAttribute"/>s on the property.
		/// </summary>
		/// <returns>An error message or null.</returns>
		protected virtual string PropertyErrors(string propertyName)
		{
			var value = GetType().GetProperty(propertyName).GetValue(this, null);
			var context = new ValidationContext(this, null, null) { MemberName = propertyName };
			var results = new List<ValidationResult>();
			if (Validator.TryValidateProperty(value, context, results))
				return null;

			var sb = new StringBuilder();
			results.Aggregate(sb, (aSB, r) =>
			{
				if (aSB.Length > 0)
					aSB.Append(", ");
				aSB.Append(r.ErrorMessage);
				return aSB;
			});
			return sb.ToString();
		}

		#endregion
#endif

		#region INotifyPropertyChanged (strongly typed: OnPropertyChanged(() => MyProperty))

		protected void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> property)
		{
			OnPropertyChanged(PropertyUtils.GetName(property));
		}
		protected void OnPropertyChanged<TTarget, TProperty>(Expression<Func<TTarget, TProperty>> property)
		{
			OnPropertyChanged(PropertyUtils.GetName(property));
		}
		protected virtual void OnPropertyChanged(string propertyName)
		{
			var e = PropertyChanged;
			if (e != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
