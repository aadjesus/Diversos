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
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Vidyano.Presentation.TranslationServices.GoogleTranslate
{
    /// <summary>
    /// The MarkupExtension that translates a text or the result of a Binding
    /// </summary>
    [MarkupExtensionReturnType(typeof(BindingExpression))]
    public class TranslateExtension : MarkupExtension
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TranslateExtension class
        /// </summary>
        public TranslateExtension()
        {
            Text = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the TranslateExtension class
        /// </summary>
        /// <param name="text">the text to translate</param>
        public TranslateExtension(string text)
        {
            Text = text;
        }

        #endregion

        #region MarkupExtension Implementation

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return !string.IsNullOrEmpty(Text) ? Text : "{Binding}";

            // Get the TargetObject and TargetProperty via the IProvideValueTarget service
            var provideValueService = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            if (provideValueService == null)
                return null;

            var targetObject = provideValueService.TargetObject as DependencyObject;
            var targetProperty = provideValueService.TargetProperty as DependencyProperty;

            if (targetObject != null && targetProperty != null)
            {
                // There might already be a Binding
                if (Binding == null)
                    Binding = new Binding();

                // Create the Converter, passing the targetObject and targetProperty
                var converter = new TranslateConverter(targetObject, targetProperty);

                Binding.Converter = converter;
                Binding.ConverterParameter = Text; // Text may be string.Empty if a the markup extension is created with a Binding

                // Bind the converter's SourceLanguageProperty and TargetLanguageProperty to the attached properties
                var sourceLanguageBinding = new Binding
                {
                    Path = new PropertyPath("(0)", LanguageSelector.SourceLanguageProperty),
                    Source = targetObject
                };

                var targetLanguageBinding = new Binding
                {
                    Path = new PropertyPath("(0)", LanguageSelector.TargetLanguageProperty),
                    Source = targetObject
                };

                converter.SetBinding(TranslateConverter.SourceLanguageProperty, sourceLanguageBinding);
                converter.SetBinding(TranslateConverter.TargetLanguageProperty, targetLanguageBinding);

                // Return the new/updated binding
                return Binding.ProvideValue(serviceProvider);
            }

            return null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text that needs to be translated.
        /// </summary>
        [ConstructorArgument("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the Binding for the data that needs to be translated.
        /// </summary>
        public Binding Binding { get; set; }

        #endregion
    }
}