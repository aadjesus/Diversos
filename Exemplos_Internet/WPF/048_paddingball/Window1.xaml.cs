using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PaddingBall
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        //static double xValue=10, yValue=10;
        Thickness BallStartPos = new Thickness(116, 160, 0, 0);
        Thickness BallCurrentPos;
        Thickness BallNextPos = new Thickness();
        Thickness PadStartPos = new Thickness(200, 390, 0, 0);
        Thickness PadCurrentPos;
        ThicknessAnimation moveTheBall;
        ThicknessAnimation moveThePad;
        Storyboard PlayGame = new Storyboard();
        Storyboard PlayPad;
        private void StartGame(object sender, RoutedEventArgs e)
        {
            BallCurrentPos = BallStartPos;
            BallNextPos.Left = playground.Width;
            BallNextPos.Top = playground.Height;
            AnimateBall(BallNextPos, BallCurrentPos);


            playground.KeyDown += new KeyEventHandler(playground_KeyDown);
            ball.LayoutUpdated += new EventHandler(ball_LayoutUpdated);
        }

        void ball_LayoutUpdated(object sender, EventArgs e)
        {
            BallCurrentPos = ball.Margin;
            if (((ball.Margin.Top - ball.Height + 20) >= pad.Margin.Top) && ball.Margin.Left >= (pad.Margin.Left -30 ) && ball.Margin.Left <= (pad.Margin.Left + 30))
            {
                BallNextPos.Top = 0;
                BallNextPos.Left = BallCurrentPos.Left - 200;
                AnimateBall(BallNextPos, BallCurrentPos);
            }
            else if (((ball.Margin.Top - ball.Height + 20) >= pad.Margin.Top) && ball.Margin.Left >= (pad.Margin.Left + 30) && ball.Margin.Left <= (pad.Margin.Left + 60))
            {
                BallNextPos.Top = 0;
                AnimateBall(BallNextPos, BallCurrentPos);
            }
            else if (((ball.Margin.Top - ball.Height + 20) >= pad.Margin.Top) && ball.Margin.Left >= (pad.Margin.Left + 60) && ball.Margin.Left <= (pad.Margin.Left + 100))
            {
                BallNextPos.Top = 0;
                BallNextPos.Left = BallCurrentPos.Left + 200;
                AnimateBall(BallNextPos, BallCurrentPos);
            }
            //else if (((ball.Margin.Top - ball.Height + 20) >= pad.Margin.Top) && (ball.Margin.Left + ball.Width) >= pad.Margin.Left)
            //{
            //    BallNextPos.Top = 0;
            //    BallNextPos.Left = BallCurrentPos.Left - 100;
            //    AnimateBall(BallNextPos, BallCurrentPos);
            //}
            else if (ball.Margin.Top <= 25)
            {
                BallNextPos.Top = playground.Height;
                AnimateBall(BallNextPos, BallCurrentPos);
            }
            else if (ball.Margin.Left <= 0)
            {
                BallNextPos.Left = playground.Width;
                AnimateBall(BallNextPos, BallCurrentPos);
            }
            else if (ball.Margin.Left >= playground.Width - 50)
            {
                BallNextPos.Left = 0;
                AnimateBall(BallNextPos, BallCurrentPos);
            }
            else if (ball.Margin.Top >= pad.Margin.Top + ball.Height)
            {
                MessageBox.Show("Game Over!", "Padding Ball");
                Application.Current.Shutdown();
            }
        }

        void playground_KeyDown(object sender, KeyEventArgs e)
        {
            PadCurrentPos = pad.Margin;
            double xPadValue=0;
            if (e.Key == Key.Left)
                if (pad.Margin.Left > -100)
                    xPadValue = -50;
            if (e.Key == Key.Right)
                if (pad.Margin.Left <= (playground.Width - pad.Width))
                    xPadValue = 50;

            AnimatePad(xPadValue);
        }
        void AnimateBall(Thickness next, Thickness current)
        {
            moveTheBall = new ThicknessAnimation();
            moveTheBall.From = current ;
            moveTheBall.To = next;
            moveTheBall.Duration = new Duration(TimeSpan.FromSeconds(3));
            Storyboard.SetTargetName(moveTheBall, "ball");
            Storyboard.SetTargetProperty(moveTheBall, new PropertyPath(Rectangle.MarginProperty));
            if (PlayGame.Children.Count > 0)
                PlayGame.Children.RemoveAt(0);
            PlayGame.Children.Add(moveTheBall);
            PlayGame.Begin(this, true);
        }
        void AnimatePad(double x)
        {
            moveThePad = new ThicknessAnimation();
            moveThePad.From = PadCurrentPos;
            moveThePad.To = new Thickness(pad.Margin.Left+x, pad.Margin.Top, 0, 0);
            moveThePad.Duration = new Duration(TimeSpan.FromSeconds(0));
            Storyboard.SetTargetName(moveThePad, "pad");
            Storyboard.SetTargetProperty(moveThePad, new PropertyPath(Rectangle.MarginProperty));
            PlayPad = new Storyboard();
            if (PlayPad.Children.Count > 0)
                PlayPad.Children.RemoveAt(0);
            PlayPad.Children.Add(moveThePad);
            PlayPad.Begin(this, true);
        }

        private void ShowAboutBox(object sender, RoutedEventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog();
        }

        private void ExitGame(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


    }
}
