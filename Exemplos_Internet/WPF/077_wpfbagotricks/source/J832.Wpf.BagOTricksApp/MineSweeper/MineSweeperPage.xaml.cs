using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Microsoft.Samples.KMoore.WPFSamples.MineSweeper
{
    public partial class MineSweeperPage : Page
    {
        //private MineFieldElement _mineFieldElement;
        public MineSweeperPage()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            _playingBrush = (GradientBrush)FindResource("PlayingBackgroundBrush");
            _wonBrush = (GradientBrush)FindResource("WonBackgroundBrush");
            _lostBrush = (GradientBrush)FindResource("LostBackgroundBrush");

            Binding binding = new Binding();
            binding.Source = _mineFieldElement;
            binding.Path = new PropertyPath("MinesLeft");

            minesLeft.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding();
            binding.Source = _mineFieldElement;
            binding.Path = new PropertyPath("SecondsElapsed");
            time.SetBinding(TextBlock.TextProperty, binding);
        }

        private void _mineFieldElement_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == MineField.StatePropertyName)
            {
                if (_mineFieldElement.State == WinState.Won)
                {
                    this.Background = _wonBrush;
                }
                else if (_mineFieldElement.State == WinState.Lost)
                {
                    this.Background = _lostBrush;
                }
                else if (_mineFieldElement.State == WinState.Unknown)
                {
                    this.Background = _playingBrush;
                }
            }
        }

        void PageLoaded(object sender, RoutedEventArgs args)
        {
        }

        void NewGame(object sender, RoutedEventArgs e)
        {
            _mineFieldElement.NewGame();
        }

        Brush _playingBrush;
        Brush _wonBrush;        
        Brush _lostBrush;
    }
}