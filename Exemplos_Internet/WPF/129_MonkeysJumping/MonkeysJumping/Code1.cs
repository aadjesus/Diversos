using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Navigation;

namespace MonkeysJumping
{
    public partial class Scene2
    {

	  //starts music after splash screen
        MediaPlayer mambo = new MediaPlayer();
        private void OnLoadedInvalidated(object sender, EventArgs e)
        {
            Clock clock = (Clock)sender;
            if (clock.CurrentState != ClockState.Active)
            {
                splashcanvas.Visibility = Visibility.Collapsed;
                hittest2canvas.Visibility = Visibility.Collapsed;
                mambo.Open(new Uri("Gilberto Gil - Oslodum.mp3", UriKind.Relative));
                mambo.Volume = 1;
                mambo.MediaEnded += new EventHandler(mambo_MediaEnded);
                mambo.Play();
            }
        }


        //fires when there is a mousedown on our invisible hittest canvas
        //start our hittest storyboard which causes monkey to fall off bed, etc.
        MediaPlayer jump = new MediaPlayer();
        private void OnJumpingEnd(object sender, EventArgs e)
        {
            Clock clock = (Clock)sender;
            if (clock.CurrentState != ClockState.Active)
            {
                Storyboard s = (Storyboard)this.Resources["hittest"];
                this.BeginStoryboard(s);
                mambo.Pause();
                jump.Open(new Uri("fall.wav", UriKind.Relative));
                jump.Volume = 1;
                jump.Play();
            }
        }

	  //loop music infinitely	
        void mambo_MediaEnded(object sender, EventArgs e)
        {
            mambo.Stop();
            mambo.Position = TimeSpan.Zero;
            mambo.Play();
        }

        //fires when animation after hittest ends
        //kick off jumping 2 storyboard and resume music
        private void hittestend(object sender, EventArgs e)
        {
            Clock clock = (Clock)sender;
            if (clock.CurrentState != ClockState.Active)
            {
                //make the fallen monkey disappear
                monkey.Visibility = Visibility.Collapsed;
                Storyboard s = (Storyboard)this.Resources["jumping2"];
                mambo.Play();
                this.BeginStoryboard(s);
                hittest1canvas.Visibility = Visibility.Collapsed;
            }
        }

    }
}
