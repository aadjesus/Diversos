namespace GG.GameAttackCombos.Logic {

	/// <summary>
	/// Represents select information about a combo package.
	/// </summary>
	public class ComboPackageInfo {

		/// <summary>
		/// Represents a ComboPackageInfo that is empty.
		/// </summary>
		public static readonly ComboPackageInfo Empty;

		#region Properties

		/// <summary>
		/// Gets a flag indicating whether or not this instance is empty.
		/// </summary>
		public bool IsEmpty {
			get { return (FileName == null && GameCode == null && Title == null && Version == null); }
		}

		/// <summary>
		/// Gets the file name of the combo package represented by this instance.
		/// </summary>
		public string FileName { get; private set; }

		/// <summary>
		/// Gets the game code used to download the combo package represented by this instance.
		/// </summary>
		public string GameCode { get; private set; }

		/// <summary>
		/// Gets the title of the combo package represented by this instance.
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// Gets the version of the combo package represented by this instance.
		/// </summary>
		public string Version { get; private set; }

		/// <summary>
		/// Gets the binary data for the icon of the combo package represented by this instance.
		/// </summary>
		public byte[] IconData { get; private set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes the class.
		/// </summary>
		static ComboPackageInfo() {
			Empty = new ComboPackageInfo();
		}

		/// <summary>
		/// Creates an empty instance of ComboPackageInfo.
		/// </summary>
		private ComboPackageInfo() { }

		/// <summary>
		/// Creates an instance of ComboPackageInfo with the specified file name and title.
		/// </summary>
		/// <param name="fileName">The file name of the combo package info to create.</param>
		/// <param name="gameCode">The game code of the combo package info to create.</param>
		/// <param name="title">The title of the combo package info to create.</param>
		/// <param name="version">The version of the combo package info to create.</param>
		/// <param name="iconData">The binary data for the icon of the combo package info to create.</param>
		public ComboPackageInfo(string fileName, string gameCode, string title, string version, byte[] iconData)
			: this() {
			FileName = fileName;
			GameCode = gameCode;
			Title = title;
			Version = version;
			IconData = iconData;
		}

		/// <summary>
		/// Creates an instance of ComboPackageInfo with the specified file name and title.
		/// </summary>
		/// <param name="fileName">The file name of the combo package info to create.</param>
		/// <param name="gameCode">The game code of the combo package info to create.</param>
		/// <param name="title">The title of the combo package info to create.</param>
		/// <param name="version">The version of the combo package info to create.</param>
		public ComboPackageInfo(string fileName, string gameCode, string title, string version)
			: this(fileName, gameCode, title, version, null) {
		}

		/// <summary>
		/// Creates an instance of ComboPackageInfo from the specified ComboPackage.
		/// </summary>
		/// <param name="package">The ComboPackage to be represented.</param>
		/// <param name="includeIcon">A flag indicating whether or not to include the icon.</param>
		public ComboPackageInfo(ComboPackage package, bool includeIcon)
			: this(package.OriginalFileName, package.GameCode, package.Title, package.Version) {
			if (includeIcon) {
				IconData = package.GetIconData();
			}
		}

		/// <summary>
		/// Creates an instance of ComboPackageInfo from the specified ComboPackage.
		/// </summary>
		/// <param name="package">The ComboPackage to be represented.</param>
		public ComboPackageInfo(ComboPackage package)
			: this(package, false) {
		}

		#endregion

	}

}