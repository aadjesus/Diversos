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
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace Vidyano.TranslationServices.GoogleTranslate
{
    /// <summary>
    /// Method extensions that allow you to translate via Google Ajax Language API.
    /// </summary>
    public static class GoogleTranslateExtensions
    {
        #region TranslationResponse Class

        /// <summary>
        /// JSON Response Class.
        /// </summary>
        class TranslationResponse
        {
            [JsonProperty("responseData")]
            public Translation Data { get; set; }

            [JsonProperty("responseDetails")]
            public string Details { get; set; }

            [JsonProperty("responseStatus")]
            public int Status { get; set; }
        }

        #endregion

        #region Translation

        /// <summary>
        /// JSON Translation Response.
        /// </summary>
        class Translation
        {
            [JsonProperty("translatedText")]
            public string TranslatedText { get; set; }
        }

        #endregion

        #region Constructor

        static GoogleTranslateExtensions()
        {
            Cache = InitializeCache();
        }

        #endregion

        #region Static Properties

        /// <summary>
        /// Optional Proxy which will be used to query the Google Ajax Service.
        /// </summary>
        public static WebProxy Proxy { get; set; }
        private static TranslateCache Cache { get; set; }

        #endregion

        #region Extension Methods

        /// <summary>
        /// Translates the string using the specified language pair.
        /// </summary>
        /// <param name="str">The string to translate</param>
        /// <param name="source">The source language</param>
        /// <param name="target">The target language</param>
        /// <returns></returns>
        public static string Translate(this string str, Languages.Language source, Languages.Language target)
        {
            if (str == null || source == null || target == null || source == target || str.Length > 500)
                return str;

            Source langSource;
            lock (Cache)
            {
                // Look for the text block in the Source table.
                langSource = Cache.Source.FirstOrDefault(s => s.LangCode == source.Code && s.Value == str);
                if (langSource != null)
                {
                    // Get the translation for the text block and the target language from the Translations table.
                    Translations trans = langSource.Translations.FirstOrDefault(t => t.LangCode == target.Code);
                    if (trans != null)
                        return trans.Value;
                }
                else
                {
                    // Insert the text block in the Source table.
                    langSource = new Source { LangCode = source.Code, Value = str };
                    Cache.Source.InsertOnSubmit(langSource);

                    Cache.SubmitChanges();
                }
            }

            // Create a WebRequest, passing in the text to translate along with the source and target language code
            var req = (HttpWebRequest)WebRequest.Create(string.Format("http://ajax.googleapis.com/ajax/services/language/translate?v=1.0&q={0}&langpair={1}%7C{2}", str, source.Code, target.Code));
            // req.Referer = Get your Google API Key at http://code.google.com/apis/ajaxsearch/key.html
            // NOTE: Google documentation dictates you must specifiy a valid Referer !
            req.Proxy = Proxy;

            WebResponse response = req.GetResponse();
            var streamReader = new StreamReader(response.GetResponseStream());

            var serializer = new JsonSerializer();
            var translationResponse = (TranslationResponse)serializer.Deserialize(new StringReader(streamReader.ReadToEnd()), typeof(TranslationResponse));
            if (translationResponse.Data != null)
            {
                lock (Cache)
                {
                    // Some other thread might already added this information, so check first.
                    Translations trans = langSource.Translations.FirstOrDefault(t => t.LangCode == target.Code);
                    if (trans == null)
                    {
                        // Add the new translation for the text block.
                        langSource.Translations.Add(new Translations { LangCode = target.Code, Value = translationResponse.Data.TranslatedText });
                        Cache.SubmitChanges();
                    }
                }

                return translationResponse.Data.TranslatedText;
            }

            return string.Format("ERROR {0}: {1}", translationResponse.Status, translationResponse.Details);
        }

        #endregion

        #region Helper Methods

        private static TranslateCache InitializeCache()
        {
            if (!File.Exists("TranslateCache.sdf"))
            {
                Type thisType = typeof(GoogleTranslateExtensions);
                using (Stream inStream = thisType.Assembly.GetManifestResourceStream(thisType, "Resources.TranslateCache.sdf"))
                {
                    if (inStream != null)
                    {
                        using (Stream outStream = File.Create("TranslateCache.sdf"))
                        {
                            const int bufferSize = 8 * 1024; // 8K

                            var buffer = new byte[bufferSize];
                            int read;
                            while ((read = inStream.Read(buffer, 0, bufferSize)) > 0)
                            {
                                outStream.Write(buffer, 0, read);
                            }
                        }
                    }
                }
            }

            return new TranslateCache("Data Source=TranslateCache.sdf");
        }

        #endregion
    }
}