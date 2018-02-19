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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ImageAnnotationDemo
{
	/// <summary>
	/// Renders an image annotation.  This control has an IsInEditMode state
	/// which determines whether it allows the user to edit the annotation or not.
	/// When IsInEditMode is true the annotation is displayed in a TextBox, when
	/// it is false the annotation is displayed in a TextBlock.
	/// </summary>
    public partial class ImageAnnotationControl : ContentControl
	{
		#region Data

		private readonly ImageAnnotation _imageAnnotation;

		#endregion // Data

		#region Constructor

		public ImageAnnotationControl(
            ImageAnnotation imageAnnotation, 
            Style annotationStyle, 
            Style annotationEditorStyle)
        {
            InitializeComponent();

			if (imageAnnotation == null)
				throw new ArgumentNullException("imageAnnotation");

			_imageAnnotation = imageAnnotation;

			// Establish a binding between the ImageAnnotation's Text 
			// and our Content property so that changes in either 
			// property will update the other.
			Binding binding = new Binding("Text");
			binding.Source = _imageAnnotation;
			binding.Mode = BindingMode.TwoWay;
			this.SetBinding(ContentControl.ContentProperty, binding);

            // Allow this control to be focused,
            // so that we can steal focus from the
            // TextBox used to edit the annotation.
            base.Focusable = true;

            // Prevent the control from having a focus rect.
            base.FocusVisualStyle = null;

            if (annotationStyle != null)
            {
                base.Resources.Add("STYLE_Annotation", annotationStyle);
            }

            if (annotationEditorStyle != null)
            {
                base.Resources.Add("STYLE_AnnotationEditor", annotationEditorStyle);
            }

            this.IsInEditMode = true;
        }

        #endregion // Constructor
        
        #region Public Properties

        /// <summary>
        /// Gets/sets whether the annotation allows the user to edit the text or not.
        /// </summary>
        public bool IsInEditMode
        {
            get { return (bool)GetValue(IsInEditModeProperty); }
            set { SetValue(IsInEditModeProperty, value); }
        }

        /// <summary>
        /// Represents the IsInEditMode property.
        /// </summary>
        public static readonly DependencyProperty IsInEditModeProperty =
            DependencyProperty.Register(
            "IsInEditMode",
            typeof(bool),
            typeof(ImageAnnotationControl),
            new UIPropertyMetadata(false, OnIsInEditModeChanged));

        static void OnIsInEditModeChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            ImageAnnotationControl control = depObj as ImageAnnotationControl;
            if (control != null && !control.IsInEditMode)
            {
				// Take focus away from the TextBox within the ImageAnnotationControl, 
				// and give focus to the ImageAnnotationControl itself.
                if (control.IsKeyboardFocusWithin)
                    control.Focus();

                // If the user tried to delete the annotation, let the outside world know.
                control.AttemptToDelete();
            }
        }

        #endregion // Public Properties

        #region Event Handling Methods

        // Invoked by the MenuItem in the ContextMenu.
        void OnDeleteAnnotation(object sender, RoutedEventArgs e)
        {
            this.Delete();
        }

		// Invoked when we enter edit mode.
        void OnTextBoxLoaded(object sender, RoutedEventArgs e)
        {
            TextBox txt = sender as TextBox;

			// Give the TextBox input focus
            txt.Focus();

			// If the user is editing an existing annotation, figure out which
			// character was clicked on and put the caret there.
            int charIdx = txt.GetCharacterIndexFromPoint(Mouse.GetPosition(txt), true);
            if (charIdx > -1)
                txt.SelectionStart = charIdx;
        }

		// Invoked when we exit edit mode.
        void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            // Delay the call so that the change of focus can complete first.			
            base.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                (NoArgDelegate)delegate
                {
                    this.IsInEditMode = false;
                }
            );
        }

		// Invoked when the user edits the annotation.
        void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
				if (Keyboard.Modifiers == ModifierKeys.Shift)
				{
					// Insert a newline and move the caret down.
					TextBox txt = sender as TextBox;
					txt.AppendText(Environment.NewLine);
					++txt.SelectionStart;
				}
				else
				{
					this.IsInEditMode = false;
				}
            }
            else if (e.Key == Key.Escape)
            {
                // If the annotation is not deleted (it has text), 
				// then take us out of edit mode.
                if (!this.AttemptToDelete())
                    this.IsInEditMode = false;
            }
        }

		// Invoked when the user clicks the annotation while in display mode.
        void OnTextBlockMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsInEditMode = true;
        }

        #endregion // Event Handling Methods

        #region Private Helpers

        bool AttemptToDelete()
        {
			bool deleted = String.IsNullOrEmpty(_imageAnnotation.Text);
            if (deleted)
            {
                this.Delete();
            }
            return deleted;
        }

        void Delete()
        {
			_imageAnnotation.Delete();
        }

        #endregion // Private Helpers
    }
}