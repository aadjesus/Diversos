using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Reflection;
using System;
using System.IO;

namespace GG.GameAttackCombos.Client {

	/// <summary>
	/// Common information and actions used by the client application.
	/// </summary>
	public static class Common {

		/// <summary>
		/// Gets the directory containing the application.
		/// </summary>
		public static string ApplicationDirectory {
			get { return AppDomain.CurrentDomain.BaseDirectory; }
		}

#if Standalone
		private static string _packageDirectory;
		/// <summary>
		/// Gets the directory where packages are stored.
		/// </summary>
		public static string PackageDirectory {
			get {
				if (string.IsNullOrEmpty(_packageDirectory)) {
					_packageDirectory = Path.Combine(ApplicationDirectory, "Packages");
				}
				return _packageDirectory;
			}
		}
#endif


		/// <summary>
		/// Finds a child with the requested name and type within the specified object's template.
		/// </summary>
		/// <typeparam name="T">The type of the child to find.</typeparam>
		/// <param name="name">The name of the child to find.</param>
		/// <param name="obj">The object to act as the parent reference when finding the child.</param>
		/// <returns>A child of obj with type T and the specified name, or null if none is found.</returns>
		public static T FindTemplateChild<T>(string name, DependencyObject obj) where T : DependencyObject {
			// Find the content presenter for the specified object.
			ContentPresenter Presenter = Common.FindVisualChild<ContentPresenter>(obj);
			if (Presenter != null) {
				// Find the child element within the presenter's ContentTemplate.
				return Presenter.ContentTemplate.FindName(name, Presenter) as T;
			} else {
				return null;
			}
		}
		
		/// <summary>
		/// Finds the first visual child of the specified object of the indicated type.
		/// </summary>
		/// <typeparam name="T">The type of the child to find.</typeparam>
		/// <param name="obj">The object to act as the parent reference when finding the child.</param>
		/// <returns>The first child of obj with type T, or null if none is found.</returns>
		public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject {
			// Traverse the visual children of the specified object.
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++) {
				// Test each child for the type we are looking for.
				DependencyObject Child = VisualTreeHelper.GetChild(obj, i);
				if (Child != null && Child is T)
					return (T)Child;
				else {
					// Look in the child's children for a match.
					T ChildOfChild = FindVisualChild<T>(Child);
					if (ChildOfChild != null)
						return ChildOfChild;
				}
			}
			return null;
		}

	}

}