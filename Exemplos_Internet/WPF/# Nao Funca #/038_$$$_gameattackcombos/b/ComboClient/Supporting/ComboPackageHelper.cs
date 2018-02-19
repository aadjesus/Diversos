using System.Collections.Generic;
using System.IO;
using GG.GameAttackCombos.Client.ComboServices;
using GG.GameAttackCombos.Logic;

#if !Standalone
using System.IO.IsolatedStorage;
#endif

namespace GG.GameAttackCombos.Client {

	/// <summary>
	/// A helper class for dealing with combo packages.
	/// </summary>
	public static class ComboPackageHelper {

		/// <summary>
		/// Downloads any update to the package specified.
		/// </summary>
		/// <param name="packageInfo">
		/// The ComboPackageInfo representing the package to download any update for.
		/// </param>
		/// <returns>true if an update was available and downloaded; false otherwise.</returns>
		public static bool DownloadPackageUpdate(ComboPackageInfo packageInfo) {
			bool Result = false;

			// Prepare the data state.
			byte[] PackageData = null;

			// Create a reference to the remote service.
			using (ComboPackagesServiceClient Service = new ComboPackagesServiceClient()) {
				PackageData = Service.DownloadComboPackage(packageInfo.GameCode, packageInfo.Version);
			}

			if (PackageData != null) {
				using (MemoryStream PackageStream = new MemoryStream(PackageData.Length)) {
					// Copy the downloaded data to the memory stream and open it as a package.
					// * The MemoryStream is initialized with a capacity, then the data is
					//   copied to it in order to allow it to expand with the used game code.
					StreamHelper.CopyArrayToStream(PackageData, PackageStream);
					using (ComboPackage Package = new ComboPackage(PackageStream, FileAccess.ReadWrite)) {
						// Update the package's game code.
						Package.GameCode = packageInfo.GameCode;
						Package.Save();
					}

					// Save the updated package to file.
					using (Stream PackageFileStream = OpenPackageStream(packageInfo.FileName, FileMode.Create)) {
						StreamHelper.CopyStream(PackageStream, PackageFileStream);
						Result = true;
					}
				}
			}

			return Result;
		}

		/// <summary>
		/// Downloads a new package for the specified game code and returns the new package's information.
		/// </summary>
		/// <returns>true if the package was successfully downloaded; false otherwise.</returns>
		public static ComboPackageInfo DownloadNewPackage(string newGameCode) {
			// Prepare the data state.
			ComboPackageInfo DownloadedPackageInfo = null;
			byte[] PackageData = null;

			// Create a reference to the remote service.
			using (ComboPackagesServiceClient Service = new ComboPackagesServiceClient()) {
				PackageData = Service.DownloadComboPackage(newGameCode, null);
			}
			
			if (PackageData != null) {
				// Initialize a combo package with the downloaded data to get its 
				// original file name and update its information.
				using (MemoryStream PackageStream = new MemoryStream(PackageData.Length)) {
					// Copy the downloaded data to the memory stream and open it as a package.
					// * The MemoryStream is initialized with a capacity, then the data is
					//   copied to it in order to allow it to expand with the new game code.
					StreamHelper.CopyArrayToStream(PackageData, PackageStream);
					using (ComboPackage Package = new ComboPackage(PackageStream, FileAccess.ReadWrite)) {
						// Update the package's game code.
						Package.GameCode = newGameCode;
						Package.Save();

						// Create an information instance for the downloaded package.
						DownloadedPackageInfo = new ComboPackageInfo(Package.OriginalFileName, newGameCode, Package.Title, Package.Version);
					}

					// Save the updated package to file.
					using (Stream PackageFileStream = OpenPackageStream(DownloadedPackageInfo.FileName, FileMode.Create)) {
						StreamHelper.CopyStream(PackageStream, PackageFileStream);
					}
				}
			} else {
				// Return an empty ComboPackageInfo.
				DownloadedPackageInfo = ComboPackageInfo.Empty;
			}

			return DownloadedPackageInfo;
		}

		/// <summary>
		/// Gets a list of information data for all downloaded combo packages.
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<ComboPackageInfo> GetDownloadedPackageInfoList(bool includeIcon) {
			List<ComboPackageInfo> ComboPackageInfos = null;

			// Get the file names for all downloaded combo packages.
			string[] PackageFileNames = GetPackageFileNames();
			if (PackageFileNames.Length > 0) {
				// Create a list of information for the combo packages found.
				ComboPackageInfos = new List<ComboPackageInfo>(PackageFileNames.Length);
				foreach (string PackageFileName in PackageFileNames) {
					// Open each package to get the necessary information to display.
					using (Stream PackageStream = OpenPackageStream(PackageFileName)) {
						if (PackageStream.Length > 0) {
							using (ComboPackage Package = new ComboPackage(PackageStream)) {
								ComboPackageInfo Info = new ComboPackageInfo(Package, includeIcon);
								ComboPackageInfos.Add(Info);
							}
						}
					}
				}

				// Clear the list if nothing was actually added.
				if (ComboPackageInfos.Count == 0) {
					ComboPackageInfos = null;
				}
			}

			return ComboPackageInfos;
		}

		/// <summary>
		/// Gets a list of file names for game combo packages found in the appropriate location.
		/// </summary>
		/// <returns></returns>
		public static string[] GetPackageFileNames() {
			string[] PackageFileNames = null;

#if Standalone
			// Get a list of the files in the application directory with the appropriate combo
			// package file extension.
			DirectoryInfo Directory = new DirectoryInfo(Common.PackageDirectory);
			FileInfo[] Files = Directory.GetFiles(string.Format(@"*{0}", ComboPackage.ComboPackagesFileExtension), SearchOption.TopDirectoryOnly);
			if (Files.Length > 0) {
				PackageFileNames = new string[Files.Length];
				for (int i = 0; i < Files.Length; i++) {
					PackageFileNames[i] = Files[i].Name;
				}
			}
#else
			// Open isolated storage for the user and domain to retrieve a list of file names with 
			// the appropriate combo package file extension.
			using (IsolatedStorageFile Storage = IsolatedStorageFile.GetUserStoreForDomain()) {
				PackageFileNames = Storage.GetFileNames(string.Format(@"*{0}", ComboPackage.ComboPackagesFileExtension));
			}
#endif

			return PackageFileNames;
		}

		/// <summary>
		/// Opens a stream for the specified package with indicated mode, access, and share settings.
		/// </summary>
		/// <param name="packageFileName">The file name of the package to open.</param>
		/// <param name="mode">The file mode to use when opening a stream to the package.</param>
		/// <param name="access">The file access type to use when opening a stream to the package.</param>
		/// <param name="share">The file share type to use when opening a stream to the package.</param>
		/// <returns></returns>
		public static Stream OpenPackageStream(string packageFileName, FileMode mode, FileAccess access, FileShare share) {
#if Standalone
			// Open the file stream from the application directory.
			string FilePath = Path.Combine(Common.PackageDirectory, packageFileName);
			if (!OpenModeRequiresExistingFile(mode) || File.Exists(FilePath)) {
				return File.Open(FilePath, mode, access, share);
			} else {
				return null;
			}
#else
			// Open the file stream from isolated storage.
			try {
				return new IsolatedStorageFileStream(packageFileName, mode, access, share);
			} catch (FileNotFoundException) {
				return null;
			} catch (DirectoryNotFoundException) {
				return null;
			}
#endif
		}

		/// <summary>
		/// Opens a stream for the specified package with indicated mode and access.
		/// </summary>
		/// <param name="packageFileName">The file name of the package to open.</param>
		/// <param name="mode">The file mode to use when opening a stream to the package.</param>
		/// <param name="access">The file access type to use when opening a stream to the package.</param>
		/// <returns></returns>
		public static Stream OpenPackageStream(string packageFileName, FileMode mode, FileAccess access) {
#if Standalone
			// Open the file stream from the application directory.
			string FilePath = Path.Combine(Common.PackageDirectory, packageFileName);
			if (!OpenModeRequiresExistingFile(mode) || File.Exists(FilePath)) {
				return File.Open(FilePath, mode, access);
			} else {
				return null;
			}
#else
			// Open the file stream from isolated storage.
			try {
				return new IsolatedStorageFileStream(packageFileName, mode, access);
			} catch (FileNotFoundException) {
				return null;
			} catch (DirectoryNotFoundException) {
				return null;
			}
#endif
		}

		/// <summary>
		/// Opens a stream for the specified package with the indicated mode.
		/// </summary>
		/// <param name="packageFileName">The file name of the package to open.</param>
		/// <param name="mode">The file mode to use when opening a stream to the package.</param>
		/// <returns></returns>
		public static Stream OpenPackageStream(string packageFileName, FileMode mode) {
#if Standalone
			// Open the file stream from the application directory.
			string FilePath = Path.Combine(Common.PackageDirectory, packageFileName);
			if (!OpenModeRequiresExistingFile(mode) || File.Exists(FilePath)) {
				return File.Open(FilePath, mode);
			} else {
				return null;
			}
#else
			// Open the file stream from isolated storage.
			try {
				return new IsolatedStorageFileStream(packageFileName, mode);
			} catch (FileNotFoundException) {
				return null;
			} catch (DirectoryNotFoundException) {
				return null;
			}
#endif
		}

		/// <summary>
		/// Opens a stream for the specified package in Open file mode, Read file access,
		/// and Read file sharing.
		/// </summary>
		/// <param name="packageFileName">The file name of the package to open.</param>
		/// <returns></returns>
		public static Stream OpenPackageStream(string packageFileName) {
			return OpenPackageStream(packageFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
		}


		/// <summary>
		/// Determines if the specified FileMode requires an existing file.
		/// </summary>
		/// <param name="mode">The FileMode to test.</param>
		/// <returns></returns>
		private static bool OpenModeRequiresExistingFile(FileMode mode) {
			return (mode == FileMode.Append || mode == FileMode.Open || mode == FileMode.Truncate);
		}

	}

}