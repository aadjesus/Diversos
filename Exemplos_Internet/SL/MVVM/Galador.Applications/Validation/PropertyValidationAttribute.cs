using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Galador.Applications.Validation
{
	/// <summary>
	/// Also validate another property that can be find from this instance following the <see cref="PropertyPath"/>
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class PropertyValidationAttribute : ValidationAttribute
	{
		public PropertyValidationAttribute(string propertyPath)
		{
			if (string.IsNullOrWhiteSpace(propertyPath))
				throw new ArgumentNullException("propertyPath");
			PropertyPath = propertyPath;
		}

		/// <summary>
		/// The property path from the current instance that needs to be validated too
		/// </summary>
		public string PropertyPath { get; private set; }

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (PropertyPath == null || validationContext == null || validationContext.ObjectInstance == null)
				return ValidationResult.Success;

			var path = PropertyPath.Split('.');
			var ctxt = validationContext.ObjectInstance;
			value = ctxt;
			for (int i = 0; i < path.Length; i++)
			{
				ctxt = value;
				var pi = ctxt.GetType().GetProperty(path[i]);
				value = pi.GetGetMethod().Invoke(ctxt, null);
			}

			var context = new ValidationContext(ctxt, null, null) { MemberName = path[path.Length-1]};
			var results = new List<ValidationResult>();
			if (Validator.TryValidateProperty(value, context, results) || results.Count == 0)
				return ValidationResult.Success;

			var sb = new StringBuilder();
			results.Aggregate(sb, (aSB, r) =>
			{
				if (aSB.Length > 0)
					aSB.Append(", ");
				aSB.Append(r.ErrorMessage);
				return aSB;
			});
			return new ValidationResult(sb.ToString());
		}
	}
}
