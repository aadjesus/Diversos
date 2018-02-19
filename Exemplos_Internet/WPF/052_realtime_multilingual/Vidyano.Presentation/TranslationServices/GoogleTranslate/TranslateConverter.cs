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

using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;
using Vidyano.TranslationServices.GoogleTranslate;

namespace Vidyano.Presentation.TranslationServices.GoogleTranslate
{
    /// <summary>
    /// The converter that is added to bindings that need translation services
    /// </summary>
    [ValueConversion(typeof(string), typeof(string))]
    class TranslateConverter : FrameworkElement, IValueConverter
    {
        #region Dependency Properties

        public static readonly DependencyProperty SourceLanguageProperty = DependencyProperty.Register("SourceLanguage", typeof(string), typeof(TranslateConverter), new PropertyMetadata(string.Empty, SourceLanguageChanged));
        public static readonly DependencyProperty TargetLanguageProperty = DependencyProperty.Register("TargetLanguage", typeof(string), typeof(TranslateConverter), new PropertyMetadata(string.Empty, TargetLanguageChanged));

        #endregion

        #region Private Fields

        private readonly DependencyObject targetObject;
        private readonly DependencyProperty targetProperty;

        private string previousValue = string.Empty;
        private string translation = string.Empty;
        private bool languagesChanged = true;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the TranslateConverter class
        /// </summary>
        /// <param name="targetObject">the object for which this translation is needed</param>
        /// <param name="targetProperty">the property on the targetObject that will hold the translation</param>
        public TranslateConverter(DependencyObject targetObject, DependencyProperty targetProperty)
        {
            this.targetObject = targetObject;
            this.targetProperty = targetProperty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the source language
        /// </summary>
        public string SourceLanguage
        {
            get { return (string)GetValue(SourceLanguageProperty); }
            set { SetValue(SourceLanguageProperty, value); }
        }

        /// <summary>
        /// Gets or sets the target language
        /// </summary>
        public string TargetLanguage
        {
            get { return (string)GetValue(TargetLanguageProperty); }
            set { SetValue(TargetLanguageProperty, value); }
        }

        #endregion

        #region IValueConverter Implementation

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as string ?? parameter as string;

            if (text != null)
            {
                if (languagesChanged || previousValue != text)
                {
                    previousValue = text;
                    languagesChanged = false;

                    Languages.Language sourceLang = Languages.FromString(SourceLanguage);
                    Languages.Language targetLang = Languages.FromString(TargetLanguage);

                    if (sourceLang != null && targetLang != null && sourceLang != targetLang)
                    {
                        Action translate = () => translation = text.Translate(sourceLang, targetLang);
                        translate.BeginInvoke(Translated, null);

                        return LanguageSelector.GetLoadingString(targetObject);
                    }

                    return text;
                }

                return translation;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Callback method that is invoked when the translation is completed.
        /// </summary>
        /// <param name="result">the result</param>
        private void Translated(IAsyncResult result)
        {
            targetObject.Dispatcher.Invoke(DispatcherPriority.DataBind, new Action(() => Invalidate(this)));
        }

        /// <summary>
        /// Invalidates the binding on the given DependencyObject
        /// </summary>
        /// <param name="obj">the DependencyObject that needs to be updated</param>
        private static void Invalidate(DependencyObject obj)
        {
            var converter = obj as TranslateConverter;
            if (converter != null)
            {
                BindingExpressionBase expression = BindingOperations.GetBindingExpressionBase(converter.targetObject, converter.targetProperty);
                if (expression != null)
                    expression.UpdateTarget();
            }
        }

        private static void SourceLanguageChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var converter = obj as TranslateConverter;
            if (converter != null)
                converter.languagesChanged = true;

            Invalidate(obj);
        }

        private static void TargetLanguageChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var converter = obj as TranslateConverter;
            if (converter != null)
                converter.languagesChanged = true;

            Invalidate(obj);
        }

        #endregion
    }
}