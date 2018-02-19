using System;
using System.Reflection;

namespace GG.GameAttackCombos.Client {

	/// <summary>
	/// Gets the attribute values of an assembly.
	/// </summary>
	public class AssemblyAttributes {

		#region Properties

		public string Company { get; private set; }
		public string Copyright { get; private set; }
		public string Description { get; private set; }
		public string Product { get; private set; }
		public string Title { get; private set; }
		public string Trademark { get; private set; }
		public string Version { get; private set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance AssemblyAttributes with the specified assembly.
		/// </summary>
		/// <param name="assembly">An assembly to get attributes for.</param>
		public AssemblyAttributes(Assembly assembly) {
			GetAttributeValues(assembly);
		}

		/// <summary>
		/// Creates an instance of AssemblyAttributes with the default assembly.
		/// </summary>
		public AssemblyAttributes() : this(GetDefaultAssembly()) { }

		#endregion

		/// <summary>
		/// Gets the attribute values for the specified assembly and stores them in this 
		/// instance's properties.
		/// </summary>
		/// <param name="assembly">The assembly to retrieve from.</param>
		protected virtual void GetAttributeValues(Assembly assembly) {
			// Get the necessary attributes.
			AssemblyCompanyAttribute CompanyAttribute = GetCustomAttribute<AssemblyCompanyAttribute>(assembly);
			AssemblyCopyrightAttribute CopyrightAttribute = GetCustomAttribute<AssemblyCopyrightAttribute>(assembly);
			AssemblyDescriptionAttribute DescriptionAttribute = GetCustomAttribute<AssemblyDescriptionAttribute>(assembly);
			AssemblyProductAttribute ProductAttribute = GetCustomAttribute<AssemblyProductAttribute>(assembly);
			AssemblyTitleAttribute TitleAttribute = GetCustomAttribute<AssemblyTitleAttribute>(assembly);
			AssemblyTrademarkAttribute TrademarkAttribute = GetCustomAttribute<AssemblyTrademarkAttribute>(assembly);
			AssemblyVersionAttribute VersionAttribute = GetCustomAttribute<AssemblyVersionAttribute>(assembly);

			// Set the necessary properties to the attribute values.
			Company = (CompanyAttribute != null ? CompanyAttribute.Company : null);
			Copyright = (CopyrightAttribute != null ? CopyrightAttribute.Copyright : null);
			Description = (DescriptionAttribute != null ? DescriptionAttribute.Description : null);
			Product = (ProductAttribute != null ? ProductAttribute.Product : null);
			Title = (TitleAttribute != null ? TitleAttribute.Title : null);
			Trademark = (TrademarkAttribute != null ? TrademarkAttribute.Trademark : null);
			Version = (VersionAttribute != null ? VersionAttribute.Version : null);
		}

		/// <summary>
		/// Gets the first custom attribute of type T found for the specified Assembly.
		/// </summary>
		/// <typeparam name="T">The attribute type to find.</typeparam>
		/// <param name="assembly">The assembly to search on.</param>
		/// <returns></returns>
		protected T GetCustomAttribute<T>(Assembly assembly) where T : Attribute {
			T CustomAttribute = null;

			if (assembly != null) {
				object[] CustomAttributes = assembly.GetCustomAttributes(typeof(T), false);
				if (CustomAttributes != null && CustomAttributes.Length > 0) {
					CustomAttribute = (T)CustomAttributes[0];
				}
			}

			return CustomAttribute;
		}

		/// <summary>
		/// Gets the default assembly.
		/// </summary>
		/// <returns></returns>
		private static Assembly GetDefaultAssembly() {
			// First, attempt to get the entry assembly.
			Assembly DefaultAssembly = Assembly.GetEntryAssembly();
			if (DefaultAssembly == null) {
				// Next, attempt to get the executing assembly.
				DefaultAssembly = Assembly.GetExecutingAssembly();
				if (DefaultAssembly == null) {
					// Finally, attempt to get the calling assembly.
					DefaultAssembly = Assembly.GetCallingAssembly();
				}
			}

			return DefaultAssembly;
		}

	}

}