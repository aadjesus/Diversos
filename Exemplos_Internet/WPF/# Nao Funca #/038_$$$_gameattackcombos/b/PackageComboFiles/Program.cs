using System;
using System.IO;
using GG.GameAttackCombos.Logic;

namespace PackageComboFiles {

	class Program {

		const int ExpectedStaticArgCount = 6;

		/// <summary>
		/// The main entry point to this console application.
		/// </summary>
		/// <param name="args">
		/// Command line arguments:
		/// <list type="number">
		///		<item>the path and file name of the combo package file to create</item>
		///		<item>the path to the combo definition XML file</item>
		///		<item>the path to the skin XAML file</item>
		///		<item>the path(s) to any skin resource file</item>
		/// </list>
		/// </param>
		static void Main(string[] args) {
			if (args == null || args.Length < ExpectedStaticArgCount) {
				Console.WriteLine("Please specify the paths and version necessary to package the combo files.");
				DisplayUsage();
			} else {
				try {
					// Check that the first argument is a valid path.
					FileInfo PackageFile = new FileInfo(args[0]);
				} catch {
					Console.WriteLine("The specified <PackageFileName> is not a valid file path and name.");
					DisplayUsage();
					return;
				}

				try {
					// Check that the third argument is a valid version.
					Version PackageVersion = new Version(args[2]);
				} catch {
					Console.WriteLine("The specified <Version> is not a valid version.");
					DisplayUsage();
					return;
				}

				// Make sure each of the other args reference existing file paths.
				for (int i = 3; i < args.Length; i++) {
					if (!File.Exists(args[i])) {
						Console.WriteLine("The specified paths of files to be packaged must exist.");
						Console.WriteLine();
						return;
					}
				}

				// Separate any skin resource paths specified in the arguments.
				string[] SkinResourcePaths = null;
				if (args.Length > ExpectedStaticArgCount) {
					SkinResourcePaths = new string[args.Length - ExpectedStaticArgCount];
					Array.Copy(args, ExpectedStaticArgCount, SkinResourcePaths, 0, SkinResourcePaths.Length);
				}

				// Create the package.
				if (ComboPackage.CreatePackageFile(args[0], args[1], args[2], args[3], args[4], args[5], SkinResourcePaths)) {
					Console.WriteLine("The package was successfully created!");
					Console.WriteLine();
				} else {
					Console.WriteLine("An error occurred creating the package.");
					Console.WriteLine();
				}
			}
		}

		static void DisplayUsage() {
			Console.WriteLine("Usage:");
			Console.WriteLine();
			Console.WriteLine("PackageComboFiles <PackageFileName> <Title> <Version> <ComboDefinitionFileName> <SkinFileName> <SkinResourceFile1> ... <SkinResourceFileN>");
			Console.WriteLine("-----------------------------------------------------------");
			Console.WriteLine("<PackageFileName>         The full path and file name of the combo package file");
			Console.WriteLine("                          to create.");
			Console.WriteLine("<Title>                   The title of the package file to create.");
			Console.WriteLine("<Version>                 The version of the package file to create. This value");
			Console.WriteLine("                          must be in the format: major.minor[.build[.revision]]");
			Console.WriteLine("<ComboDefinitionFileName> The full path to the combo definition XML file to");
			Console.WriteLine("                          include.");
			Console.WriteLine("<SkinFileName>            The full path to the skin XAML file to include.");
			Console.WriteLine("<SkinResourceFile1..N>    The full path to any skin resource files to include.");
			Console.WriteLine();
		}

	}

}