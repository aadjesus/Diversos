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

using System.Collections.Generic;
using System.Linq;

namespace Vidyano.TranslationServices.GoogleTranslate
{
    /// <summary>
    /// Static class containing the available language pairs.
    /// </summary>
    public static class Languages
    {
        #region Language Class

        public class Language
        {
            internal Language(string desc, string code) :
                this(desc, code, false) { }

            internal Language(string desc, string code, bool rightToLeft)
            {
                Description = desc;
                Code = code;
                RightToLeft = rightToLeft;
            }

            public string Description { get; private set; }
            public string Code { get; private set; }
            public bool RightToLeft { get; private set; }
        }

        #endregion

        #region Static Constructor

        static Languages()
        {
            List<Language> languages = new List<Language>();

            //Afrikaans = new Language("Afrikaans", "af"); languages.Add(Afrikaans);
            //Albanian = new Language("Albanian", "sq"); languages.Add(Albanian);
            //Amharic = new Language("Amharic", "am"); languages.Add(Amharic);
            Arabic = new Language("Arabic", "ar", true); languages.Add(Arabic);
            //Armenian = new Language("Armenian", "hy"); languages.Add(Armenian);
            //Azerbaijani = new Language("Azerbaijani", "az"); languages.Add(Azerbaijani);
            //Basque = new Language("Basque", "eu"); languages.Add(Basque);
            //Belarusian = new Language("Belarusian", "be"); languages.Add(Belarusian);
            //Bengali = new Language("Bengali", "bn"); languages.Add(Bengali);
            //Bihari = new Language("Bihari", "bh"); languages.Add(Bihari);
            Bulgarian = new Language("Bulgarian", "bg"); languages.Add(Bulgarian);
            //Burmese = new Language("Burmese", "my"); languages.Add(Burmese);
            //Catalan = new Language("Catalan", "ca"); languages.Add(Catalan);
            //Cherokee = new Language("Cherokee", "ch"); languages.Add(Cherokee);
            //Chinese = new Language("Chinese", "zh"); languages.Add(Chinese);
            Chinese_Simplified = new Language("Simplified Chinese", "zh-CN"); languages.Add(Chinese_Simplified);
            Chinese_Traditional = new Language("Traditional Chinese", "zh-TW"); languages.Add(Chinese_Traditional);
            Croatian = new Language("Croatian", "hr"); languages.Add(Croatian);
            Czech = new Language("Czech", "cs"); languages.Add(Czech);
            Danish = new Language("Danish", "da"); languages.Add(Danish);
            //Dhivehi = new Language("Dhivehi", "dv"); languages.Add(Dhivehi);
            Dutch = new Language("Dutch", "nl"); languages.Add(Dutch);
            English = new Language("English", "en"); languages.Add(English);
            //Esperanto = new Language("Esperanto", "eo"); languages.Add(Esperanto);
            //Estonian = new Language("Estonian", "et"); languages.Add(Estonian);
            //Filipino = new Language("Filipino", "tl"); languages.Add(Filipino);
            Finnish = new Language("Finnish", "fi"); languages.Add(Finnish);
            French = new Language("French", "fr"); languages.Add(French);
            //Galacian = new Language("Galacian", "gl"); languages.Add(Galacian);
            //Georgian = new Language("Georgian", "ka"); languages.Add(Georgian);
            German = new Language("German", "de"); languages.Add(German);
            Greek = new Language("Greek", "el"); languages.Add(Greek);
            //Guarani = new Language("Guarani", "gn"); languages.Add(Guarani);
            //Gujarati = new Language("Gujarati", "gu"); languages.Add(Gujarati);
            //Hebrew = new Language("Hebrew", "iw"); languages.Add(Hebrew);
            Hindi = new Language("Hindi", "hi"); languages.Add(Hindi);
            //Hungarian = new Language("Hungarian", "hu"); languages.Add(Hungarian);
            //Icelandic = new Language("Icelandic", "is"); languages.Add(Icelandic);
            //Indonesian = new Language("Indonesian", "id"); languages.Add(Indonesian);
            //Inuktitut = new Language("Inuktitut", "iu"); languages.Add(Inuktitut);
            Italian = new Language("Italian", "it"); languages.Add(Italian);
            Japanese = new Language("Japanese", "ja"); languages.Add(Japanese);
            //Kannada = new Language("Kannada", "kn"); languages.Add(Kannada);
            //Kazakh = new Language("Kazakh", "kk"); languages.Add(Kazakh);
            //Khmer = new Language("Khmer", "km"); languages.Add(Khmer);
            Korean = new Language("Korean", "ko"); languages.Add(Korean);
            //Kurdish = new Language("Kurdish", "ku"); languages.Add(Kurdish);
            //Kyrgyz = new Language("Kyrgyz", "ky'"); languages.Add(Kyrgyz);
            //Aothian = new Language("Aothian", "lo"); languages.Add(Aothian);
            //Latvian = new Language("Latvian", "lv"); languages.Add(Latvian);
            //Lithuanian = new Language("Lithuanian", "lt"); languages.Add(Lithuanian);
            //Macedonian = new Language("Macedonian", "mk"); languages.Add(Macedonian);
            //Malay = new Language("Malay", "ms"); languages.Add(Malay);
            //Malayalam = new Language("Malayalam", "ml"); languages.Add(Malayalam);
            //Maltese = new Language("Maltese", "mt"); languages.Add(Maltese);
            //Marathi = new Language("Marathi", "mr"); languages.Add(Marathi);
            //Mongolian = new Language("Mongolian", "mn"); languages.Add(Mongolian);
            //Nepali = new Language("Nepali", "ne"); languages.Add(Nepali);
            Norwegian = new Language("Norwegian", "no"); languages.Add(Norwegian);
            //Oriya = new Language("Oriya", "or"); languages.Add(Oriya);
            //Pashto = new Language("Pashto", "ps"); languages.Add(Pashto);
            //Persian = new Language("Persian", "fa"); languages.Add(Persian);
            Polish = new Language("Polish", "pl"); languages.Add(Polish);
            Porguguese = new Language("Porguguese", "pt-PT"); languages.Add(Porguguese);
            //Punjabi = new Language("Punjabi", "pa"); languages.Add(Punjabi);
            Romanian = new Language("Romanian", "ro"); languages.Add(Romanian);
            Russian = new Language("Russian", "ru"); languages.Add(Russian);
            //Sanskrit = new Language("Sanskrit", "sa"); languages.Add(Sanskrit);
            //Serbian = new Language("Serbian", "sr"); languages.Add(Serbian);
            //Sindhi = new Language("Sindhi", "sd"); languages.Add(Sindhi);
            //Sinhalese = new Language("Sinhalese", "si"); languages.Add(Sinhalese);
            //Slovak = new Language("Slovak", "sk"); languages.Add(Slovak);
            //Slovenian = new Language("Slovenian", "sl"); languages.Add(Slovenian);
            Spanish = new Language("Spanish", "es"); languages.Add(Spanish);
            //Swahili = new Language("Swahili", "sw"); languages.Add(Swahili);
            Swedish = new Language("Swedish", "sv"); languages.Add(Swedish);
            //Tajik = new Language("Tajik", "tg"); languages.Add(Tajik);
            //Tamil = new Language("Tamil", "ta"); languages.Add(Tamil);
            //Tagalog = new Language("Tagalog", "tl"); languages.Add(Tagalog);
            //Telugu = new Language("Telugu", "te"); languages.Add(Telugu);
            //Thai = new Language("Thai", "th"); languages.Add(Thai);
            //Tibetan = new Language("Tibetan", "bo"); languages.Add(Tibetan);
            //Turkish = new Language("Turkish", "tr"); languages.Add(Turkish);
            //Ukrainian = new Language("Ukrainian", "uk"); languages.Add(Ukrainian);
            //Urdu = new Language("Urdu", "ur"); languages.Add(Urdu);
            //Uzbek = new Language("Uzbek", "uz"); languages.Add(Uzbek);
            //Uighur = new Language("Uighur", "ug"); languages.Add(Uighur);
            //Vietnamese = new Language("Vietnamese", "vi"); languages.Add(Vietnamese);

            AvailableLanguages = languages.ToArray();
        }

        #endregion

        #region Static Properties

        public static Language[] AvailableLanguages { get; private set; }

        //public static Language Afrikaans { get; private set; }
        //public static Language Albanian { get; private set; }
        //public static Language Amharic { get; private set; }
        public static Language Arabic { get; private set; }
        //public static Language Armenian { get; private set; }
        //public static Language Azerbaijani { get; private set; }
        //public static Language Basque { get; private set; }
        //public static Language Belarusian { get; private set; }
        //public static Language Bengali { get; private set; }
        //public static Language Bihari { get; private set; }
        public static Language Bulgarian { get; private set; }
        //public static Language Burmese { get; private set; }
        //public static Language Catalan { get; private set; }
        //public static Language Cherokee { get; private set; }
        //public static Language Chinese { get; private set; }
        public static Language Chinese_Simplified { get; private set; }
        public static Language Chinese_Traditional { get; private set; }
        public static Language Croatian { get; private set; }
        public static Language Czech { get; private set; }
        public static Language Danish { get; private set; }
        //public static Language Dhivehi { get; private set; }
        public static Language Dutch { get; private set; }
        public static Language English { get; private set; }
        //public static Language Esperanto { get; private set; }
        //public static Language Estonian { get; private set; }
        //public static Language Filipino { get; private set; }
        public static Language Finnish { get; private set; }
        public static Language French { get; private set; }
        //public static Language Galacian { get; private set; }
        //public static Language Georgian { get; private set; }
        public static Language German { get; private set; }
        public static Language Greek { get; private set; }
        //public static Language Guarani { get; private set; }
        //public static Language Gujarati { get; private set; }
        //public static Language Hebrew { get; private set; }
        public static Language Hindi { get; private set; }
        //public static Language Hungarian { get; private set; }
        //public static Language Icelandic { get; private set; }
        //public static Language Indonesian { get; private set; }
        //public static Language Inuktitut { get; private set; }
        public static Language Italian { get; private set; }
        public static Language Japanese { get; private set; }
        //public static Language Kannada { get; private set; }
        //public static Language Kazakh { get; private set; }
        //public static Language Khmer { get; private set; }
        public static Language Korean { get; private set; }
        //public static Language Kurdish { get; private set; }
        //public static Language Kyrgyz { get; private set; }
        //public static Language Aothian { get; private set; }
        //public static Language Latvian { get; private set; }
        //public static Language Lithuanian { get; private set; }
        //public static Language Macedonian { get; private set; }
        //public static Language Malay { get; private set; }
        //public static Language Malayalam { get; private set; }
        //public static Language Maltese { get; private set; }
        //public static Language Marathi { get; private set; }
        //public static Language Mongolian { get; private set; }
        //public static Language Nepali { get; private set; }
        public static Language Norwegian { get; private set; }
        //public static Language Oriya { get; private set; }
        //public static Language Pashto { get; private set; }
        //public static Language Persian { get; private set; }
        public static Language Polish { get; private set; }
        public static Language Porguguese { get; private set; }
        //public static Language Punjabi { get; private set; }
        public static Language Romanian { get; private set; }
        public static Language Russian { get; private set; }
        //public static Language Sanskrit { get; private set; }
        //public static Language Serbian { get; private set; }
        //public static Language Sindhi { get; private set; }
        //public static Language Sinhalese { get; private set; }
        //public static Language Slovak { get; private set; }
        //public static Language Slovenian { get; private set; }
        public static Language Spanish { get; private set; }
        //public static Language Swahili { get; private set; }
        public static Language Swedish { get; private set; }
        //public static Language Tajik { get; private set; }
        //public static Language Tamil { get; private set; }
        //public static Language Tagalog { get; private set; }
        //public static Language Telugu { get; private set; }
        //public static Language Thai { get; private set; }
        //public static Language Tibetan { get; private set; }
        //public static Language Turkish { get; private set; }
        //public static Language Ukrainian { get; private set; }
        //public static Language Urdu { get; private set; }
        //public static Language Uzbek { get; private set; }
        //public static Language Uighur { get; private set; }
        //public static Language Vietnamese { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the Language object for a given language code or description.
        /// </summary>
        /// <param name="strLang">The language code or description</param>
        /// <returns>The corresponding Language object</returns>
        public static Language FromString(string strLang)
        {
            return AvailableLanguages.Where(lang => lang.Code == strLang || lang.Description == strLang).FirstOrDefault();
        }

        #endregion
    }
}