using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Reflection;

namespace MyEffects
{
    public class AnaglyphEffect : ShaderEffect
    {
        #region Constructors

        static string effectFile = "AnaglyphEffect.ps";

        static AnaglyphEffect()
        {
            Assembly a = typeof(AnaglyphEffect).Assembly;
            string assemblyShortName = a.ToString().Split(',')[0];
            string uriString = "pack://application:,,,/" + assemblyShortName + ";component/" + effectFile;
            _pixelShader.UriSource = new Uri(uriString);
        }

        public AnaglyphEffect()
        {
            this.PixelShader = _pixelShader;

            // Update each DependencyProperty that's registered with a shader register.  This
            // is needed to ensure the shader gets sent the proper default value.
            UpdateShaderValue(RightInputProperty);
            UpdateShaderValue(LeftInputProperty);
        }

        #endregion

        #region Dependency Properties

        public Brush RightInput
        {
            get { return (Brush)GetValue(RightInputProperty); }
            set { SetValue(RightInputProperty, value);  }
        }

        // Brush-valued properties turn into sampler-property in the shader.
        // This helper sets "ImplicitInput" as the default, meaning the default
        // sampler is whatever the rendering of the element it's being applied to is.
        public static readonly DependencyProperty RightInputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("RightInput", typeof(AnaglyphEffect), 0);


        public Brush LeftInput
        {
            get { return (Brush)GetValue(LeftInputProperty); }
            set { SetValue(LeftInputProperty, value); }
        }

        // Brush-valued properties turn into sampler-property in the shader.
        // This helper sets "ImplicitInput" as the default, meaning the default
        // sampler is whatever the rendering of the element it's being applied to is.
        public static readonly DependencyProperty LeftInputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("LeftInput", typeof(AnaglyphEffect), 1);

        #endregion

        #region Member Data

        private static PixelShader _pixelShader = new PixelShader();

        #endregion

    }
}
