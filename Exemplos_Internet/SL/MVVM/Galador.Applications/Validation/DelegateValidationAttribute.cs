using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Galador.Applications.Validation
{
	public class DelegateValidationAttribute : ValidationAttribute
	{
		/// <summary>
		/// Create a DelegateValidationAttribute with an instance method to call for validation.
		/// </summary>
		/// <param name="methodName">
		/// Name of a public instance method without parameter in the validated object.
		/// The method should return the error string or null.
		/// </param>
		public DelegateValidationAttribute(string methodName)
		{
			if (string.IsNullOrWhiteSpace(methodName))
				throw new ArgumentNullException("methodName");
			ValidationMethod = methodName;
		}

		/// <summary>
		/// Name of a public instance method without parameter in the validated object.
		/// The method should return the error string or null.
		/// </summary>
		public string ValidationMethod { get; private set; }

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (ValidationMethod == null || validationContext == null || validationContext.ObjectInstance == null)
				return ValidationResult.Success;

			var ctxt = validationContext.ObjectInstance;
			var mi = ctxt.GetType().GetMethod(ValidationMethod);
			var s = (string)mi.Invoke(ctxt, null);
			if (s == null)
				return ValidationResult.Success;
			return new ValidationResult(s);
		}
	}
}
