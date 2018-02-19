using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ImageAnnotationDemo
{
	public partial class Window1 : System.Windows.Window
	{
		readonly List<ImageAnnotation> _xrayAnnotations = new List<ImageAnnotation>();
		readonly List<ImageAnnotation> _habitatAnnotations = new List<ImageAnnotation>();

		public Window1()
		{
			InitializeComponent();
		}

		void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Image image = sender as Image;

			// Get the location of the mouse cursor relative 
			// to the Image. Offset the location a bit so 
			// that the adorner placement feels more natural.
			Point textLocation = e.GetPosition(image);
			textLocation.Offset(-4, -4);

			// Get the Style applied to the ImageAnnotationControl's TextBlock.
			Style annotationStyle =
				base.FindResource("AnnotationStyle") as Style;

			// Get the Style applied to the ImageAnnotationControl's TextBox.
			Style annotationEdtiorStyle =
				base.FindResource("AnnotationEditorStyle") as Style;

			// Create an annotation where the mouse cursor is located.
			ImageAnnotation imgAnn = ImageAnnotation.Create(
				image,
				textLocation,
				annotationStyle,
				annotationEdtiorStyle);

			this.CurrentAnnotations.Add(imgAnn);
		}

		void OnDeleteButtonClick(object sender, RoutedEventArgs e)
		{
			if (this.CurrentAnnotations.Count > 0)
			{
				this.CurrentAnnotations.ForEach(delegate(ImageAnnotation imgAnn) { imgAnn.Delete(); });
				this.CurrentAnnotations.Clear();
			}
		}

		List<ImageAnnotation> CurrentAnnotations
		{
			get
			{
				if (this.tabControl.SelectedIndex == 0)
					return _xrayAnnotations;

				return _habitatAnnotations;
			}
		}
	}
}