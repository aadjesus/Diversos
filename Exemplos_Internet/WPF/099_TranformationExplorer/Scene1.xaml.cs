using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TranformationExplorer
{
	public partial class Scene1
	{
        private TransformGroup trGrp;
        private SkewTransform trSkw;
        private RotateTransform trRot;
        private TranslateTransform trTns;
        private ScaleTransform trScl;

		public Scene1()
		{
			this.InitializeComponent();

            trSkw = new SkewTransform(0, 0);
            trRot = new RotateTransform(0);
            trTns = new TranslateTransform(0, 0);
            trScl = new ScaleTransform(1, 1);

            trGrp = new TransformGroup();
            trGrp.Children.Add(trSkw);
            trGrp.Children.Add(trRot);
            trGrp.Children.Add(trTns);
            trGrp.Children.Add(trScl);
        }

        private void OnSceneLoaded(object sender,
                                 System.Windows.RoutedEventArgs e)
        {
            rect.RenderTransform = trGrp;

            slSclX.Value = slSclY.Value = 1;
        }

        protected void OnValueChanged(object sender,
                                 System.Windows.RoutedEventArgs e)
        {
            trRot.Angle = slRot.Value;
            trRot.CenterX = slRotX.Value;
            trRot.CenterY = slRotY.Value;
            trScl.ScaleX = slSclX.Value;
            trScl.ScaleY = slSclY.Value;
            trScl.CenterX = slSclOX.Value;
            trScl.CenterY = slSclOY.Value;

            trSkw.AngleX = slSkwX.Value;
            trSkw.AngleY = slSkwY.Value;
            trSkw.CenterX = slSkwOX.Value;
            trSkw.CenterY = slSkwOY.Value;

            trTns.X = slTrnX.Value;
            trTns.Y = slTrnY.Value;
        }
    }
}
