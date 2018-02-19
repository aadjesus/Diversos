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
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Vidyano.TranslationServices.GoogleTranslate;

namespace Vidyano.Presentation.TranslationServices.GoogleTranslate
{
    /// <summary>
    /// Defines the translation scope.
    /// </summary>
    public class LanguageSelector : ContentControl
    {
        #region Dependency Properties

        private static readonly DependencyPropertyKey AvailableLanguagesPropertyKey = DependencyProperty.RegisterReadOnly("AvailableLanguages", typeof(string[]), typeof(LanguageSelector), new PropertyMetadata(Languages.AvailableLanguages.Select(lang => lang.Description).ToArray()));
        public static readonly DependencyProperty AvailableLanguagesProperty = AvailableLanguagesPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey LanguagesStringPropertyKey = DependencyProperty.RegisterReadOnly("LanguagesString", typeof(string), typeof(LanguageSelector), new PropertyMetadata("Language: "));
        public static readonly DependencyProperty LanguagesStringProperty = LanguagesStringPropertyKey.DependencyProperty;

        public static readonly DependencyProperty SourceLanguageProperty = DependencyProperty.RegisterAttached("SourceLanguage", typeof(string), typeof(LanguageSelector), new FrameworkPropertyMetadata(String.Empty, FrameworkPropertyMetadataOptions.Inherits));
        public static readonly DependencyProperty TargetLanguageProperty = DependencyProperty.RegisterAttached("TargetLanguage", typeof(string), typeof(LanguageSelector), new FrameworkPropertyMetadata(String.Empty, FrameworkPropertyMetadataOptions.Inherits, TargetLanguageChanged));

        public static readonly DependencyProperty LoadingStringProperty = DependencyProperty.RegisterAttached("LoadingString", typeof(string), typeof(LanguageSelector), new FrameworkPropertyMetadata("Loading...", FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #region Constructor

        /// <summary>
        /// Static constructor
        /// </summary>
        static LanguageSelector()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LanguageSelector), new FrameworkPropertyMetadata(typeof(LanguageSelector)));
        }

        /// <summary>
        /// Initializes a new instance of the LanguageSelector class.
        /// </summary>
        public LanguageSelector()
        {
            // Setting default source and target language
            SetSourceLanguage(this, "English");

            // Get the target language from the current UI culture
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
            Languages.Language targetLanguage = Languages.FromString(culture.TwoLetterISOLanguageName);
            if (targetLanguage != null)
                SetTargetLanguage(this, targetLanguage.Description);
            else
                SetTargetLanguage(this, "English");
        }

        #endregion

        #region Overriden Methods

        protected override void OnInitialized(EventArgs e)
        {
            // Set the source language if the xml:lang attribute is defined
            if (Language != null && !string.IsNullOrEmpty(Language.IetfLanguageTag))
            {
                var sourceCulture = new CultureInfo(Language.IetfLanguageTag);
                Languages.Language sourceLanguage = Languages.FromString(sourceCulture.TwoLetterISOLanguageName);
                if (sourceLanguage != null)
                    SetSourceLanguage(this, sourceLanguage.Description);
            }

            base.OnInitialized(e);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the loading string for the current target language.
        /// </summary>
        /// <param name="obj">the DependencyObject</param>
        /// <returns>The loading string for the current language</returns>
        public static string GetLoadingString(DependencyObject obj)
        {
            return (string)obj.GetValue(LoadingStringProperty);
        }

        /// <summary>
        /// Sets the loading string for the current target language.
        /// </summary>
        /// <param name="obj">the DependencyObject</param>
        /// <param name="value">the translated loading string</param>
        private static void SetLoadingString(DependencyObject obj, string value)
        {
            obj.SetValue(LoadingStringProperty, value);
        }

        /// <summary>
        /// Gets the source language
        /// </summary>
        /// <param name="obj">the DependencyObject</param>
        /// <returns>the source language</returns>
        public static string GetSourceLanguage(DependencyObject obj)
        {
            return (string)obj.GetValue(SourceLanguageProperty);
        }

        /// <summary>
        /// Sets the source language
        /// </summary>
        /// <param name="obj">the DependencyObject</param>
        /// <param name="value">the source language</param>
        public static void SetSourceLanguage(DependencyObject obj, string value)
        {
            obj.SetValue(SourceLanguageProperty, value);
        }

        /// <summary>
        /// Gets the target language
        /// </summary>
        /// <param name="obj">the DependencyObject</param>
        /// <returns>the target language</returns>
        public static string GetTargetLanguage(DependencyObject obj)
        {
            return (string)obj.GetValue(TargetLanguageProperty);
        }

        /// <summary>
        /// Sets the target language
        /// </summary>
        /// <param name="obj">the DependencyObject</param>
        /// <param name="value">the target language</param>
        public static void SetTargetLanguage(DependencyObject obj, string value)
        {
            obj.SetValue(TargetLanguageProperty, value);
        }

        /// <summary>
        /// Returns the available languages.
        /// </summary>
        public string[] AvailableLanguages
        {
            get
            {
                return (string[])GetValue(AvailableLanguagesProperty);
            }
        }

        /// <summary>
        /// Returns the Language string for the current target language.
        /// </summary>
        public string LanguagesString
        {
            get
            {
                return (string)GetValue(LanguagesStringProperty);
            }
        }

        #endregion

        #region Helper Methods

        private static void TargetLanguageChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            var selector = obj as LanguageSelector;
            if (selector != null)
            {
                Languages.Language targetLanguage = Languages.FromString((string)e.NewValue);
                if (targetLanguage != null)
                {
                    // These string are needed in the new targetlanguage
                    obj.Dispatcher.Invoke(DispatcherPriority.Send, new Action(() => SetLoadingString(selector, "Loading...".Translate(Languages.English, targetLanguage))));
                    obj.Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => selector.SetValue(LanguagesStringPropertyKey, "Language :".Translate(Languages.English, targetLanguage))));

                    selector.FlowDirection = targetLanguage.RightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
                }
            }
        }

        #endregion
    }
}