using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Galador.Applications.Validation
{
	public class OpenRangeValidationAttribute : ValidationAttribute
	{
		public OpenRangeValidationAttribute()
		{
		}
		public OpenRangeValidationAttribute(object minimum, object maximum)
		{
			Minimum = minimum;
			Maximum = maximum;
		}

		public object Minimum { get; set; }
		public object Maximum { get; set; }

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var cv = value as IComparable;
			if (value == null)
			{
				if (Minimum == null && Maximum == null)
					return ValidationResult.Success;
				return new ValidationResult(base.FormatErrorMessage(validationContext.MemberName));
			}
			else
			{
				var t = value.GetType();
				if (Minimum != null)
				{
					var min = ConversionValidationAttribute.TryConvert(Minimum, t) as IComparable;
					if (min.CompareTo(cv) > 0)
						return new ValidationResult(base.FormatErrorMessage(validationContext.MemberName));
				}
				if (Maximum != null)
				{
					var max = ConversionValidationAttribute.TryConvert(Maximum, t) as IComparable;
					if (cv.CompareTo(max) < 0)
						return new ValidationResult(base.FormatErrorMessage(validationContext.MemberName));
				}
			}
			return ValidationResult.Success;
		}
	}
}
