using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows;

namespace ShaderEffectTemplate.MyEffect1
{
    public class MyEffect1 : ShaderEffect
    {
        /// <summary>
        /// Pixel shader that this effect is using
        /// </summary>
        private static PixelShader _shader = new PixelShader() { UriSource = Utilities.GetResourcePackUri("MyEffect1/MyEffect1.ps") };

        public MyEffect1()
        {
            PixelShader = _shader;

            // remember to call UpdateShaderValue() for all shader input arguments here
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(SomeInputProperty);
        }

        /// <summary>
        /// The default shader input - the visual on which the shader operates
        /// Note: you can add more texture inputs in a similar way as Input is added
        /// </summary>
        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        /// <summary>
        /// The WPF dependency property that backs up the shader input, assigned to sampler register s0
        /// </summary>
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(MyEffect1), 0);

        /// <summary>
        /// Some other input property that our shader will use
        /// </summary>
        public double SomeInput
        {
            get { return (float)GetValue(SomeInputProperty); }
            set { SetValue(SomeInputProperty, value); }
        }

        /// <summary>
        /// Using Dependency property to store SomeInput in order to enable binding, animation and other
        /// use PixelShaderConstantCallback() to assing to a shader register - in this case it's register c0
        /// </summary>
        public static readonly DependencyProperty SomeInputProperty = DependencyProperty.Register("SomeInput", typeof(double), typeof(MyEffect1),
            new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)));
    }
}
