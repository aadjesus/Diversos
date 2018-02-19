using System.Web.Configuration;

namespace GG.GameAttackCombos.Services {

	/// <summary>
	/// Provides a convenient typed way to access connection strings and application settings.
	/// </summary>
	internal static class Settings {

		/// <summary>
		/// Gets the primary connection string for these services.
		/// </summary>
		internal static string ConnectionString {
			get {
				return WebConfigurationManager.ConnectionStrings["GameAttackCombosEntities"].ConnectionString;
			}
		}

		/// <summary>
		/// Gets the virtual path to the combo packages stored with these services.
		/// </summary>
		internal static string ComboPackagesVirtualPath {
			get {
				return WebConfigurationManager.AppSettings["ComboPackagesVirtualPath"];
			}
		}

	}

}