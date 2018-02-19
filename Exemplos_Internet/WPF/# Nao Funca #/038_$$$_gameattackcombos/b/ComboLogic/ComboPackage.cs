using System;
using System.IO;
using System.IO.Packaging;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Xml;

namespace GG.GameAttackCombos.Logic {

	/// <summary>
	/// Represents the package for attack combo definitions and their associated resources.
	/// </summary>
	/// <remarks>
	/// A typical combo package contains:
	/// <list type="bullet">
	///		<item>a combo definition XML file</item>
	///		<item>a user interface skin XAML file</item>
	///		<item>1 or more user interface image(s)</item>
	/// </list>
	/// </remarks>
	public class ComboPackage : IDisposable {

		#region Constants

		// The file extension for combo packages.
		public const string ComboPackagesFileExtension = ".gcp";

		// Package relationship type URIs.
		const string RelationshipBase = "http://schemas.gurugames.com/gameattackcombos/relationships";
		const string RelationshipComboDefinition = RelationshipBase + "/comboDefinition";
		const string RelationshipIcon = RelationshipBase + "/icon";
		const string RelationshipSkin = RelationshipBase + "/skin";
		const string RelationshipSkinResource = RelationshipSkin + "/resources";

		// Content types.
		const string ContentTypeXaml = "text/xaml";
		const string ContentTypePng = "image/png";
		const string ContentTypeUnknown = "application/octet-stream";

		// The pattern for a package version.
		const string VersionPattern = @"^[0-9]{1,3}(?:\.[0-9]{1,5}){1,3}$";

		#endregion

		/// <summary>
		/// Maintains a reference to the underlying Package.
		/// </summary>
		private Package _package;

		#region Properties

		/// <summary>
		/// Gets the creator of this combo package.
		/// </summary>
		public string Creator {
			get { return _package.PackageProperties.Creator; }
		}

		/// <summary>
		/// Gets or sets the game code used to download this combo package (via the Identifier property).
		/// </summary>
		public string GameCode {
			get { return _package.PackageProperties.Identifier; }
			set {
				_package.PackageProperties.Identifier = value;
			}
		}

		/// <summary>
		/// Gets the original file name of this combo package (via the Subject property).
		/// </summary>
		public string OriginalFileName {
			get { return _package.PackageProperties.Subject; }
		}

		/// <summary>
		/// Gets the title of this combo package.
		/// </summary>
		public string Title {
			get { return _package.PackageProperties.Title; }
		}

		/// <summary>
		/// Gets the version of this combo package.
		/// </summary>
		public string Version {
			get { return _package.PackageProperties.Version; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes an instance of ComboPackage with default properties.
		/// </summary>
		protected ComboPackage() { }
		
		/// <summary>
		/// Initializes an instance of ComboPackage with the specified Stream set to a combo package 
		/// and with the specified package access.
		/// </summary>
		/// <param name="stream">The package Stream to open.</param>
		public ComboPackage(Stream stream, FileAccess packageAccess)
			: this() {
			_package = Package.Open(stream, FileMode.Open, packageAccess);
		}

		/// <summary>
		/// Initializes an instance of ComboPackage with the specified Stream set to a combo package.
		/// </summary>
		/// <param name="stream">The package Stream to open.</param>
		public ComboPackage(Stream stream)
			: this(stream, FileAccess.Read) {
		}

		/// <summary>
		/// Initializes an instance of ComboPackage with the specified path to a combo package file.
		/// </summary>
		/// <param name="path">The path to a combo package file to open.</param>
		public ComboPackage(string path)
			: this() {
			_package = Package.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		#endregion

		#region IDisposable Members

		/// <summary>
		/// Cleans up by closing the underlying package and dereferencing it.
		/// </summary>
		public void Dispose() {
			if (_package != null) {
				_package.Close();
				_package = null;
			}
		}

		#endregion

		
		/// <summary>
		/// Creates a new package file containing the specified combo definition, skin, and skin
		/// resources files.
		/// </summary>
		/// <param name="packagePath">The path to the new package file to create.</param>
		/// <param name="packageTitle">The title of this package.</param>
		/// <param name="packageVersion">The version of this package.</param>
		/// <param name="comboDefinitionPath">The path to the combo definition file to include.</param>
		/// <param name="skinPath">The path to the skin file to include.</param>
		/// <param name="skinResourcePaths">The paths to any skin resource files to include.</param>
		/// <returns>true if a new package file was successfully created; false otherwise.</returns>
		public static bool CreatePackageFile(string packagePath, string packageTitle, string packageVersion, string comboDefinitionPath, string iconPath, string skinPath, params string[] skinResourcePaths) {
			bool Result = false;

			// Attempt to create the package first.
			using (Package Package = Package.Open(packagePath, FileMode.Create)) {
				// Set the package properties.
				Package.PackageProperties.Creator = "Guru Games";
				Package.PackageProperties.Title = packageTitle;
				Package.PackageProperties.Version = packageVersion;
				Package.PackageProperties.Subject = Path.GetFileName(packagePath);

				// Create the main combo definition part of the package.
				PackagePart MainPart = Package.CreatePart(
					PackUriHelper.CreatePartUri(new Uri("ComboDefinition.xml", UriKind.Relative)),
					MediaTypeNames.Text.Xml,
					CompressionOption.Maximum
				);

				// Copy the data from the specified file to the part and create the main relationship.
				using (Stream MainPartStream = MainPart.GetStream(FileMode.Create, FileAccess.Write)) {
					StreamHelper.CopyFileToStream(comboDefinitionPath, MainPartStream);
				}
				Package.CreateRelationship(MainPart.Uri, TargetMode.Internal, RelationshipComboDefinition);

				// Create the icon part of the package (no extension is used when saving for flexibility).
				PackagePart IconPart = Package.CreatePart(
					PackUriHelper.CreatePartUri(new Uri("Icon", UriKind.Relative)),
					GetContentTypeFromFileName(iconPath),
					CompressionOption.NotCompressed
				);

				// Copy the data from the specified file to the part and create the icon relationship.
				using (Stream IconPartStream = IconPart.GetStream(FileMode.Create, FileAccess.Write)) {
					StreamHelper.CopyFileToStream(iconPath, IconPartStream);
				}
				MainPart.CreateRelationship(IconPart.Uri, TargetMode.Internal, RelationshipIcon);


				// Create the skin part of the package.
				PackagePart SkinPart = Package.CreatePart(
					PackUriHelper.CreatePartUri(new Uri("Skin/ComboSkin.xaml", UriKind.Relative)),
					ContentTypeXaml,
					CompressionOption.Maximum
				);

				// Copy the data from the specified file to the part and create the skin relationship from the main part.
				using (Stream SkinPartStream = SkinPart.GetStream(FileMode.Create, FileAccess.Write)) {
					StreamHelper.CopyFileToStream(skinPath, SkinPartStream);
				}
				MainPart.CreateRelationship(SkinPart.Uri, TargetMode.Internal, RelationshipSkin);

				foreach(string SkinResourcePath in skinResourcePaths) {
					// Get the content type of the resource based on its file name.
					string ContentType = GetContentTypeFromFileName(SkinResourcePath);

					// Determine if compression is an viable option for this resource.
					// Binary files tend to *increase* in size with compression on, so it is 
					// restricted to text files only for now.
					CompressionOption Compression = CompressionOption.NotCompressed;
					if (ContentType.StartsWith("text")) {
						Compression = CompressionOption.Maximum;
					}

					// Create any skin resource parts of the package.
					PackagePart SkinResourcePart = Package.CreatePart(
						PackUriHelper.CreatePartUri(new Uri(string.Format("Skin/Resources/{0}", Path.GetFileName(SkinResourcePath)), UriKind.Relative)),
						ContentType,
						Compression
					);

					// Copy the data from the specified file to the part and create the skin resource relationship from the skin part.
					using (Stream SkinResourcePartStream = SkinResourcePart.GetStream(FileMode.Create, FileAccess.Write)) {
						StreamHelper.CopyFileToStream(SkinResourcePath, SkinResourcePartStream);
					}
					SkinPart.CreateRelationship(SkinResourcePart.Uri, TargetMode.Internal, RelationshipSkinResource);
				}

				// Finalize the contents.
				Package.Flush();

				// Indicate success.
				Result = true;
			}

			return Result;
		}

		/// <summary>
		/// Validates a specifie package version.
		/// </summary>
		/// <param name="packageVersion">The package version to validate.</param>
		/// <returns>true if the specified package version is validate; otherwise, false.</returns>
		public static bool ValidatePackageVersion(string packageVersion) {
			return Regex.IsMatch(packageVersion, VersionPattern);
		}


		/// <summary>
		/// Gets the binary icon data from the package.
		/// </summary>
		/// <returns></returns>
		public byte[] GetIconData() {
			byte[] IconData = null;

			// Get the icon package part, if it exists.
			Uri IconPartUri = PackUriHelper.CreatePartUri(new Uri("Icon", UriKind.Relative));
			if (_package.PartExists(IconPartUri)) {
				PackagePart IconPart = _package.GetPart(IconPartUri);

				// Open a stream to the part.
				using (Stream PartStream = IconPart.GetStream(FileMode.Open, FileAccess.Read)) {
					// Copy the part stream to memory.
					IconData = StreamHelper.CopyStreamToArray(PartStream);
				}
			}

			return IconData;
		}

		/// <summary>
		/// Opens the combo definition document from the package and loads it into an instance of XmlDocument.
		/// </summary>
		/// <returns></returns>
		public XmlDocument OpenComboDefinitionDocument() {
			XmlDocument Document = null;

			// Get the main package part, if it exists.
			Uri MainPartUri = PackUriHelper.CreatePartUri(new Uri("ComboDefinition.xml", UriKind.Relative));
			if (_package.PartExists(MainPartUri)) {
				PackagePart MainPart = _package.GetPart(MainPartUri);

				// Open a stream to the part.
				using (Stream MainPartStream = MainPart.GetStream(FileMode.Open, FileAccess.Read)) {
					// Create an XmlDocument and load the stream into it.
					Document = new XmlDocument();
					Document.Load(MainPartStream);
				}
			}

			return Document;
		}

		/// <summary>
		/// Opens the icon stream from the package.
		/// </summary>
		/// <param name="openAsCopy">
		/// A flag indicating whether or not to open the resource as a copy. A value of
		/// true will force the resource stream to be copied and returned; otherwise, a 
		/// direct reference to the part stream is returned.
		/// </param>
		/// <returns></returns>
		public Stream OpenIconStream(bool openAsCopy) {
			Stream IconStream = null;

			if (openAsCopy) {
				// Get the icon data and wrap it in a MemoryStream.
				byte[] IconData = GetIconData();
				if (IconData != null) {
					IconStream = new MemoryStream(IconData);
				}
			} else {
				// Get the icon package part, if it exists.
				Uri IconPartUri = PackUriHelper.CreatePartUri(new Uri("Icon", UriKind.Relative));
				if (_package.PartExists(IconPartUri)) {
					PackagePart IconPart = _package.GetPart(IconPartUri);

					// Open a stream to the part.
					IconStream = IconPart.GetStream(FileMode.Open, FileAccess.Read);
				}
			}

			return IconStream;
		}

		/// <summary>
		/// Opens the skin stream from the package.
		/// </summary>
		/// <returns></returns>
		public Stream OpenSkinStream(bool openAsCopy) {
			Stream SkinStream = null;

			// Get the skin package part, if it exists.
			Uri SkinPartUri = PackUriHelper.CreatePartUri(new Uri("Skin/ComboSkin.xaml", UriKind.Relative));
			if (_package.PartExists(SkinPartUri)) {
				PackagePart SkinPart = _package.GetPart(SkinPartUri);

				// Open a stream to the part.
				Stream PartStream = SkinPart.GetStream(FileMode.Open, FileAccess.Read);
				if (openAsCopy) {
					// Copy the part stream to memory.
					SkinStream = new MemoryStream();
					StreamHelper.CopyStream(PartStream, SkinStream);
					SkinStream.Position = 0;

					// Clean up the package part stream.
					PartStream.Dispose();
				} else {
					// Use the part stream directly.
					SkinStream = PartStream;
				}
			}

			return SkinStream;
		}

		/// <summary>
		/// Opens the specified skin resource stream from the package.
		/// </summary>
		/// <param name="skinResourceName">The name of the skin resource to retrieve.</param>
		/// <param name="openAsCopy">
		/// A flag indicating whether or not to open the skin resource as a copy. A value of
		/// true will force the resource stream to be copied and returned; otherwise, a 
		/// direct reference to the part stream is returned.
		/// </param>
		/// <returns></returns>
		public Stream OpenSkinResourceStream(string skinResourceName, bool openAsCopy) {
			Stream ResourceStream = null;

			// Get the skin resource package part, if it exists.
			Uri SkinResourcePartUri = PackUriHelper.CreatePartUri(new Uri(string.Format("Skin/Resources/{0}", skinResourceName), UriKind.Relative));
			if (_package.PartExists(SkinResourcePartUri)) {
				PackagePart SkinResourcePart = _package.GetPart(SkinResourcePartUri);

				// Open a stream to the part.
				Stream PartStream = SkinResourcePart.GetStream(FileMode.Open, FileAccess.Read);
				if (openAsCopy) {
					// Copy the part stream to memory.
					ResourceStream = new MemoryStream();
					StreamHelper.CopyStream(PartStream, ResourceStream);
					ResourceStream.Position = 0;

					// Clean up the package part stream.
					PartStream.Dispose();
				} else {
					// Use the part stream directly.
					ResourceStream = PartStream;
				}
			}

			return ResourceStream;
		}

		/// <summary>
		/// Saves the underlying package.
		/// </summary>
		public void Save() {
			if (_package != null) {
				_package.Flush();
			}
		}


		/// <summary>
		/// Gets the content type for a resource by its file name.
		/// </summary>
		/// <param name="fileName">The file name of the resource to get the content type for.</param>
		/// <returns></returns>
		private static string GetContentTypeFromFileName(string fileName) {
			if (!string.IsNullOrEmpty(fileName)) {
				string ContentType = "unknown";

				// Get the file's extension and attempt to determine the content type from it.
				string FileExtension = Path.GetExtension(fileName);
				switch (FileExtension) {
					case ".jpg":
					case ".jpeg":
						ContentType = MediaTypeNames.Image.Jpeg;
						break;

					case ".gif":
						ContentType = MediaTypeNames.Image.Gif;
						break;

					case ".tiff":
					case ".tif":
						ContentType = MediaTypeNames.Image.Tiff;
						break;

					case ".png":
						ContentType = ContentTypePng;
						break;

					case ".xaml":
						ContentType = ContentTypeXaml;
						break;

					case ".xml":
						ContentType = MediaTypeNames.Text.Xml;
						break;

					case ".txt":
					case ".log":
						ContentType = MediaTypeNames.Text.Plain;
						break;

					default:
						ContentType = ContentTypeUnknown;
						break;
				}

				return ContentType;
			} else {
				throw new ArgumentNullException("fileName");
			}
		}

	}

}