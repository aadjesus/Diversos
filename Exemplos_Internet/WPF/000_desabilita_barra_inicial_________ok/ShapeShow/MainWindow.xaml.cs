using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ShapeShow.Library;

namespace ShapeShow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        private const Int32 MaxShapeCount = 25;

        private bool IsRequestingOptions
        {
            get
            {
                return (Keyboard.Modifiers == (ModifierKeys.Alt | ModifierKeys.Control) && Keyboard.IsKeyDown(Key.O));
            }
        }

        #endregion

        public MainWindow()
        {
            InitializeComponent();

            // Route all events in the options control to the main window.
            Options.ButtonPanel.AddHandler(Button.ClickEvent, new RoutedEventHandler(OptionsButton_Click));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (IsRequestingOptions)
            {
                ShowOptions();
            }
            else if (CanDraw(e))
            {
                DrawShape(ShapeFactory.CreateShape());
            }

            base.OnKeyDown(e);
        }

        private void DrawShape(IShape shape)
        {
            if (DrawingSurface.Children.Count == MaxShapeCount)
            {
                DrawingSurface.Children.RemoveAt(0);
            }

            DrawingSurface.Children.Add(shape.UIElement);
        }

        private void ShowOptions()
        {
            DrawingSurface.Opacity = 0.2;
            Options.Visibility = Visibility.Visible;
        }

        private void HideOptions()
        {
            DrawingSurface.Opacity = 1.0;
            Options.Visibility = Visibility.Collapsed;
        }

        private void CloseApplication()
        {
            Application.Current.Shutdown();
        }

        private bool CanDraw(KeyEventArgs e)
        {
            return (!Options.IsVisible && e.Key != Key.System &&
                e.Key != Key.LeftAlt && e.Key != Key.RightAlt &&
                e.Key != Key.LeftCtrl && e.Key != Key.RightCtrl);
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!e.Handled)
            {
                FrameworkElement feSource = e.Source as FrameworkElement;

                switch (feSource.Name)
                {
                    case "CloseButton":
                        CloseApplication();
                        break;

                    case "ReturnButton":
                        HideOptions();
                        break;

                    case "ClearScreen":
                        DrawingSurface.Children.Clear();
                        break;

                    default:
                        return;
                }

                e.Handled = true;
            }
        }
    }
}
