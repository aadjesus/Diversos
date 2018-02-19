using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;
using System.Collections.Generic;
using System.Diagnostics;

namespace Galador.Wpf
{
	public class DelegateConverter : IValueConverter
	{
		public DelegateConverter()
		{
		}

		public static void Register(Assembly ass)
		{
			if (!assemblies.Contains(ass))
				assemblies.Add(ass);
		}
		static List<Assembly> assemblies = new List<Assembly>();
		static Dictionary<string, Type> types = new Dictionary<string, Type>();

		#region ConverterTypeName, MethodName

		public string ConverterTypeName
		{
			get { return sourceType; }
			set
			{
				if (value == sourceType)
					return;
				sourceType = value;
				FindMethod();
			}
		}
		string sourceType;

		public Type ConverterType
		{
			get
			{
				if (ConverterTypeName == null)
					return null;
				Type t = null;
				if (!types.TryGetValue(ConverterTypeName, out t))
				{
					foreach (var ass in assemblies)
					{
						t = ass.GetType(ConverterTypeName, false);
						if (t != null)
						{
							types[ConverterTypeName] = t;
							return t;
						}
					}
				}
				return t;
			}
		}

		public string MethodName
		{
			get { return methodName; }
			set
			{
				if (value == methodName)
					return;
				methodName = value;
				FindMethod();
			}
		}
		string methodName;

		Type argType;
		MethodInfo mConvert;

		void FindMethod()
		{
			var ct = ConverterType;
			if (ct == null || string.IsNullOrEmpty(MethodName))
			{
				mConvert = null;
			}
			else
			{
				mConvert = GetSingleArgumentMethod(ct, MethodName, out argType);
			}
		}

		public static MethodInfo GetSingleArgumentMethod(Type type, string name, out Type arg)
		{
			var mi = type.GetMethod(name);
			if (mi == null)
				throw new ArgumentException("No such method", "MethodName");
			if (!mi.IsStatic)
				throw new ArgumentException("Method is not static", "MethodName");
			var pis = mi.GetParameters();
			if (pis.Length != 1)
				throw new ArgumentException("Method is not single argument", "MethodName");
			arg = pis[0].ParameterType;
			return mi;
		}

		#endregion

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (mConvert == null)
			{
				Debug.WriteLine(
					string.Format("DelegateConverter {{ConverterTypeName = {0} , MethodName = {1}}} is null", ConverterTypeName, MethodName),
					"Conversion Failure");
				return null;
			}
			value = System.Convert.ChangeType(value, argType, culture);
			value = mConvert.Invoke(null, new object [] { value });
			value = System.Convert.ChangeType(value, targetType, culture);
			return value;
		}

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
