using System;
using System.IO;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web.Hosting;
using GG.GameAttackCombos.Data;
using GG.GameAttackCombos.Logic;

namespace GG.GameAttackCombos.Services {

	/// <summary>
	/// The service for requesting game attack combo packages for download.
	/// </summary>
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class ComboPackagesService : IComboPackagesService {

		/// <summary>
		/// Requests a download of a combo package for the specified game guide code, if there is a
		/// new version available.
		/// </summary>
		/// <param name="gameCode">A unique code to identify a known published game disc.</param>
		/// <param name="clientPackageVersion">
		/// The version the client has for the requested combo package. If a new
		/// version of the combo package exists on the server, it is returned; otherwise,
		/// nothing is returned to the client (i.e. the client's file is up-to-date).
		/// </param>
		/// <returns>A byte array containing the requested combo package.</returns>
		public byte[] DownloadComboPackage(string gameCode, string clientPackageVersion) {
			// Prepare a binary data array.
			byte[] FileData = null;

			// Parse the client version.
			Version ClientVersion = null;
			if (!string.IsNullOrEmpty(clientPackageVersion) && ComboPackage.ValidatePackageVersion(clientPackageVersion)) {
				ClientVersion = new Version(clientPackageVersion);
			} else {
				ClientVersion = new Version(0, 0);
			}

			// Get the file name for the specified code.
			string PackageFileName = null;
			if (!string.IsNullOrEmpty(gameCode)) {
				PackageFileName = GetComboPackageFileNameByGameCode(gameCode);
			}
			if (!string.IsNullOrEmpty(PackageFileName)) {
				// Build the full path to the package file.
				string ComboPackagesPath = HostingEnvironment.MapPath(Settings.ComboPackagesVirtualPath);
				PackageFileName = Path.Combine(ComboPackagesPath, PackageFileName);

				// Read the entire file into the array.
				using (FileStream PackageFile = File.Open(PackageFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
					// Open the package to get its version.
					Version CurrentVersion = null;
					using (ComboPackage Package = new ComboPackage(PackageFile)) {
						CurrentVersion = new Version(Package.Version);
					}

					// Check the version of the combo package file against the current one specified.
					if (CurrentVersion > ClientVersion) {
						// Copy the combo package file to the data array.
						FileData = StreamHelper.CopyStreamToArray(PackageFile);
					}
				}
			}

			// Return the file data.
			return FileData;
		}

		/// <summary>
		/// Gets the combo package file name for a matching specified game code.
		/// </summary>
		/// <param name="gameCode">A game code to lookup.</param>
		/// <returns></returns>
		private string GetComboPackageFileNameByGameCode(string gameCode) {
			string ComboPackageFileName = null;

			// Lookup the game code.
			using (GameAttackCombosEntities DataContext = new GameAttackCombosEntities()) {
				// Query for the corresponding game for a game disc with a matching code.
				var Query = from d in DataContext.GameDisc
							where d.GameDiscCode == gameCode
							select d.Game;

				// There should be only one result.
				Game Game = Query.FirstOrDefault();
				if (Game != null) {
					ComboPackageFileName = Game.ComboPackageFileName;
				}
			}

			return ComboPackageFileName;
		}

	}

}