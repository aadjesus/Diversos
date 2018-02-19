using System;
using System.IO;
using GG.GameAttackCombos.Logic;

namespace GG.GameAttackCombos.Client {

	/// <summary>
	/// Works with resources of the current skin of the application.
	/// </summary>
	public class CurrentSkinResource : IDisposable {

		// Last requested skin resource information.
		private string LastNameRequested;
		private Stream LastStreamRequested;


		/// <summary>
		/// Gets a Stream to a resource for the currently viewed skin of the application.
		/// </summary>
		/// <param name="resourceName">The name of the skin resource to retrieve.</param>
		/// <returns></returns>
		public Stream LoadResourceByName(string resourceName) {
			// Check if this resource was last requested.
			if (string.Compare(resourceName, LastNameRequested) != 0 || LastStreamRequested == null) {
				// Dispose of any previously requested stream to be safe.
				if (LastStreamRequested != null) {
					LastStreamRequested.Dispose();
				}

				// Store the name of the resource requested for comparison later.
				LastNameRequested = resourceName;

				// Open any current combo package.
				using (ComboPackage Package = App.Current.OpenCurrentComboPackage()) {
					if (Package != null) {
						// Open the requested skin resource from the combo package as a copy.
						LastStreamRequested = Package.OpenSkinResourceStream(resourceName, true);
					} else {
						throw new ApplicationException("There is no current combo package being viewed.");
					}
				}
			}

			return LastStreamRequested;
		}
		
		#region IDisposable Members

		/// <summary>
		/// Disposes of any remaining resources as a fail safe.
		/// </summary>
		public void Dispose() {
			// Dispose of any read stream that is still lingering.
			if (LastStreamRequested != null &&
				LastStreamRequested.CanSeek &&
				LastStreamRequested.Position >= LastStreamRequested.Length - 1) {
				// This should never be necessary because of the BitmapImage property 
				// CacheOption. It should be set to OnLoad from a skin file to dispose of the
				// stream itself. This is just a fail safe in case it is forgotten.
				LastStreamRequested.Dispose();
			}
		}

		#endregion

	}

}