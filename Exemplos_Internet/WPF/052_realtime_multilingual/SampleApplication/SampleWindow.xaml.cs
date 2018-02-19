/* *************************************************************************************
 *
 * Copyright (c) Rhea NV.
 *
 * This source code is subject to terms and conditions of the Code Project Open License.
 * A copy of the license can be found at http://www.codeproject.com/info/cpol10.aspx.
 *
 * You must not remove this notice, or any other, from this software.
 *
 * ************************************************************************************/

using System.Windows;
using Vidyano.Presentation.TranslationServices.GoogleTranslate;
using Vidyano.TranslationServices.GoogleTranslate;

namespace SampleApplication
{
    /// <summary>
    /// Interaction logic for SampleWindow.xaml
    /// </summary>
    public partial class SampleWindow : Window
    {
        public SampleWindow()
        {
            InitializeComponent();
        }

        private void Translate_Click(object sender, RoutedEventArgs e)
        {
            translateResult.Text = textToTranslate.Text.Translate(Languages.English, Languages.FromString(LanguageSelector.GetTargetLanguage(sender as DependencyObject)));
        }
    }
}