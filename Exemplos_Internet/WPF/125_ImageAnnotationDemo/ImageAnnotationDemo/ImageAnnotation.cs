using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace ImageAnnotationDemo
{
    /// <summary>
    /// An annotation which maintains a ImageAnnotationAdorner's relative location within an Image.
    /// </summary>
    public class ImageAnnotation : BindableObject
    {
        #region Data

        private ImageAnnotationAdorner _adorner;
        private AdornerLayer _adornerLayer;
        private double _horizPercent, _vertPercent;
        private Image _image;
        private bool _isDeleted;
        private string _text;

        #endregion // Data

        #region Private Constructor

        private ImageAnnotation(
            Point textLocation,
            Image image,
            Style annontationStyle,
            Style annotationEditorStyle)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            _image = image;
            this.HookImageEvents(true);

            Size imageSize = _image.RenderSize;
            if (imageSize.Height == 0 || imageSize.Width == 0)
                throw new ArgumentException("image has invalid dimensions");

            // Determine the relative location of the TextBlock.
            _horizPercent = textLocation.X / imageSize.Width;
            _vertPercent = textLocation.Y / imageSize.Height;

            // Create the adorner which displays the annotation.
            _adorner = new ImageAnnotationAdorner(
                this,
                _image,
                annontationStyle,
                annotationEditorStyle,
                textLocation);

            this.InstallAdorner();
        }

        #endregion // Private Constructor

        #region Create/Delete

        /// <summary>
        /// Creates an instance of ImageAnnotation and sets the IsDeleted flag.
        /// </summary>
        /// <param name="image">The Image which contains the item being annotated and adorned.</param>
        /// <param name="textLocation">The location of the text relative to the Image.</param>
        /// <param name="annontationStyle">The Style applied to the TextBlock in the adorner.</param>
        /// <param name="annotationEditorStyle">The Style applied to the TextBox in the adorner.</param>
        /// <returns>The new instance.</returns>
        public static ImageAnnotation Create(Image image, Point textLocation, Style annontationStyle, Style annotationEditorStyle)
        {
            return new ImageAnnotation(textLocation, image, annontationStyle, annotationEditorStyle);
        }

        /// <summary>
        /// Removes the adorner from the element tree.
        /// </summary>
        public void Delete()
        {
            this.IsDeleted = true;
        }

        #endregion // Create/Delete

        #region IsDeleted

        /// <summary>
        /// Returns true if the Delete method has been called on this object.
        /// </summary>
        public bool IsDeleted
        {
            get { return _isDeleted; }
            private set
            {
                if (!value)
                    throw new InvalidOperationException("Cannot set IsDeleted to false.");

                if (_isDeleted)
                    return;

                _isDeleted = true;

                this.HookImageEvents(false);

                _adornerLayer.Remove(_adorner);
                _adornerLayer = null;
                _adorner = null;

                base.RaisePropertyChanged("IsDeleted");
            }
        }

        #endregion // IsDeleted

        #region Text

        /// <summary>
        /// Gets/sets the annotation's text.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value)
                    return;

                _text = value;

                base.RaisePropertyChanged("Text");
            }
        }

        #endregion // Text

        #region Private Helpers

        Point CalculateEquivalentTextLocation()
        {
            double x = _image.RenderSize.Width * _horizPercent;
            double y = _image.RenderSize.Height * _vertPercent;

            return new Point(x, y);
        }

        void HookImageEvents(bool hook)
        {
            if (hook)
            {
                // Monitor changes to the size of the Image, so that
                // we know when to relocate the annotation text.
                _image.SizeChanged += this.OnImageSizeChanged;
                _image.Loaded += this.OnImageLoaded;
            }
            else
            {
                _image.SizeChanged -= this.OnImageSizeChanged;
                _image.Loaded -= this.OnImageLoaded;
            }
        }

        void InstallAdorner()
        {
            if (_isDeleted)
                return;

            _adornerLayer = AdornerLayer.GetAdornerLayer(_image);
            if (_adornerLayer == null)
                throw new ArgumentException("image does not have have an adorner layer.");

            // Add the adorner to the Image's adorner layer.
            _adornerLayer.Add(_adorner);
        }

        void OnImageLoaded(object sender, RoutedEventArgs e)
        {
            // If the Image element is in loaded/unloaded more than once
            // then we need to put our adorner in its adorner layer each
            // time it's loaded.  This can happen if the Image is in a 
            // TabControl, when the user switches between tabs.
            this.InstallAdorner();
        }

        void OnImageSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Point newLocation = this.CalculateEquivalentTextLocation();
            _adorner.UpdateTextLocation(newLocation);
        }

        #endregion // Private Helpers
    }
}