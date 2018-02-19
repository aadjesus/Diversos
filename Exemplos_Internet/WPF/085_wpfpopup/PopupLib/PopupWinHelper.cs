/************************************************************
 * Description:	PopupWinHelper								   *
 *			Copyright 2008 By TonyTonyQ					   *
 *													   *
 * Created:         2008.6.17, Verion 1.0.0							   *
 *													   *
 * Version:		1.0.0										   *
 *													   *
 * Modification:											   *
 *													   *
 ************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Forms;

namespace PopupLib
{
	/// <summary>
	/// A class for creating an irregular popup window for information
	/// </summary>
	public class PopupWinHelper
	{
		/// <summary>
		///  Show a fade in & fade out pop up window at the right lower corner of the primary screen
		/// </summary>
		/// <param name="winHeight">Pop up window's height</param>
		/// <param name="winWidth">Pop up window's width</param>
		/// <param name="bgImg">The background image of the pop up window</param>
		/// <param name="msgText">Message shown on the pop up window</param>
		/// <param name="msgPadding">The message's padding value to the edge of the window, it depends on the image's border</param>
		public static void ShowPopUp(double winHeight, double winWidth, ImageSource bgImg, string msgText, Thickness msgPadding)
		{
			//Create a Window
			Window popUp = new Window();
			popUp.Name = "PopUp";
			popUp.AllowsTransparency = true;
			popUp.Background = Brushes.Transparent;
			popUp.WindowStyle = WindowStyle.None;
			popUp.ShowInTaskbar = false;
			popUp.Topmost = true;
			popUp.Height = winHeight;
			popUp.Width = winWidth;
			popUp.Left = Screen.PrimaryScreen.Bounds.Width - winWidth;
			popUp.Top = Screen.PrimaryScreen.Bounds.Height - winHeight - 30;

			//Create a inner Grid
			Grid g = new Grid();

			//Create a Image for irregular background display
			Image img = new Image();
			img.Stretch = Stretch.Fill;
			img.Source = bgImg;
			img.BitmapEffect = new System.Windows.Media.Effects.DropShadowBitmapEffect();
			g.Children.Add(img);

			//Create a TextBlock for message display
			TextBlock msg = new TextBlock();
			msg.Padding = msgPadding;
			msg.VerticalAlignment = VerticalAlignment.Center;
			msg.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
			msg.TextWrapping = TextWrapping.Wrap;
			msg.Text = msgText;
			g.Children.Add(msg);

			popUp.Content = g;

			//Register the window's name, this is necessary for creating Storyboard using codes instead of XAML
			NameScope.SetNameScope(popUp, new NameScope());
			popUp.RegisterName(popUp.Name, popUp);

			//Create the fade in & fade out animation
			DoubleAnimation winFadeAni = new DoubleAnimation();
			winFadeAni.From = 0;
			winFadeAni.To = 1;
			winFadeAni.Duration = new Duration(TimeSpan.FromSeconds(1.5));		//Duration for 1.5 seconds
			winFadeAni.AutoReverse = true;
			winFadeAni.Completed += delegate(object sender, EventArgs e)			//Close the window when this animation is completed
			{
				popUp.Close();
			};

			// Configure the animation to target the window's opacity property
			Storyboard.SetTargetName(winFadeAni, popUp.Name);
			Storyboard.SetTargetProperty(winFadeAni, new PropertyPath(Window.OpacityProperty));

			// Add the fade in & fade out animation to the Storyboard
			Storyboard winFadeStoryBoard = new Storyboard();			
			winFadeStoryBoard.Children.Add(winFadeAni);

			// Set event trigger, make this animation played on window.Loaded
			popUp.Loaded += delegate(object sender, RoutedEventArgs e)
			{
				winFadeStoryBoard.Begin(popUp);
			};

			//Finally show the window
			popUp.Show();
		}

	
	}
}
