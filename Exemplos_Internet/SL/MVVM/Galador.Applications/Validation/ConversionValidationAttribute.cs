using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Galador.Applications.Validation
{
	public class ConversionValidationAttribute : ValidationAttribute
	{
		public ConversionValidationAttribute(Type targetType)
		{
			AcceptsNull = true;
			this.TargetType = targetType;
		}

		public Type TargetType { get; private set; }
		/// <summary>
		/// An optional public instance method name in the validated object taking on argument (of the type of the property).
		/// The conversion will be deemed to fail if calling the method an exception or if it returns false.
		/// </summary>
		public string ConversionMethod { get; set; }
		public bool AcceptsNull { get; set; }

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (ConversionMethod != null)
			{
				var mi = validationContext.ObjectInstance.GetType().GetMethod(ConversionMethod);
				try
				{
					var cr = mi.Invoke(validationContext.ObjectInstance, new object[] { value });
					if (cr is bool)
					{
						if ((bool)cr)
							return ValidationResult.Success;
					}
					else
					{
						return ValidationResult.Success;
					}
				}
				catch { }
			}
			else
			{
				try
				{
					var nv = TryConvert(value, TargetType);
					if (TargetType.IsInstanceOfType(nv))
						return ValidationResult.Success;
				}
				catch { }
			}
			return new ValidationResult(base.FormatErrorMessage(validationContext.MemberName));
		}

		#region "TryConvert()"

		public static object TryConvert(object value, Type targetType)
		{
			if (targetType.IsInstanceOfType(value))
				return value;

			if (value == null)
				return null;

			var ic = value as IConvertible;
			if (ic != null && !targetType.IsInstanceOfType(ic))
			{
				var t = targetType;
				if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
					t = Nullable.GetUnderlyingType(t);

				if (t.IsInstanceOfType(value))
					return value;

				if (t == typeof(string))
				{
					return value.ToString();
				}
				else if (t == typeof(Guid))
				{
					var s = value.ToString();
					Guid g;
					if (Guid.TryParse(value.ToString(), out g))
						return g;
				}
				else if (t == typeof(DateTimeOffset))
				{
					DateTimeOffset dt;
					if (DateTimeOffset.TryParse(value.ToString(), null, DateTimeStyles.AssumeLocal, out dt))
						return dt;
				}
				else if (t == typeof(DateTime))
				{
					DateTime dt;
					if (DateTime.TryParse(value.ToString(), null, DateTimeStyles.AssumeLocal, out dt))
						return dt;
				}
				else if (t == typeof(decimal))
					return ic.ToDecimal(null);
				else if (t == typeof(float))
					return ic.ToSingle(null);
				else if (t == typeof(double))
					return ic.ToDouble(null);
				else if (t == typeof(bool))
					return ic.ToBoolean(null);
				else if (t == typeof(byte))
					return ic.ToByte(null);
				else if (t == typeof(char))
					return ic.ToChar(null);
				else if (t == typeof(short))
					return ic.ToInt16(null);
				else if (t == typeof(int))
					return ic.ToInt32(null);
				else if (t == typeof(long))
					return ic.ToInt64(null);
				else if (t == typeof(sbyte))
					return ic.ToSByte(null);
				else if (t == typeof(ushort))
					return ic.ToUInt16(null);
				else if (t == typeof(uint))
					return ic.ToUInt32(null);
				else if (t == typeof(ulong))
					return ic.ToUInt64(null);
			}

			return value;
		}

		#endregion
	}
}
