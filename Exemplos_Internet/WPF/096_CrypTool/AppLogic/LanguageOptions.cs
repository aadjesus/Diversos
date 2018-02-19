using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace CrypTool.AppLogic
{
    public static class LanguageOptions
    {
        private static String selLang;
        private static String[] langFiles;

        static LanguageOptions()
        {
            readLangFiles();
            readSelLang();
        }

        public static void readLangFiles()
        {
          //  String langName;
            DirectoryInfo di = new DirectoryInfo("lng");
            FileInfo[] files = di.GetFiles("*.xml");

            langFiles = new String[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                langFiles[i] = files[i].Name;
                langFiles[i] = langFiles[i].Substring(0, langFiles[i].IndexOf(".xml"));
            }
        }
        public static void readSelLang()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("CrypTool.xml");

            XmlElement root = doc.DocumentElement;

            selLang = root.SelectSingleNode("./Language").InnerText;
        }
        public static String[] getLangFiles()
        {
            return langFiles;
        }
        public static String getSelLang()
        {
            return selLang;
        }
        public static String getSelLangFullPath()
        {
            String selLangFullPath;
            selLangFullPath  = "/lng/";
            selLangFullPath += selLang;
            selLangFullPath += ".xml";
            return selLangFullPath;
        }
        public static void setLang(String selectedLang)
        { 
            selLang = selectedLang;

            //save to CrypTool.xml
            XmlDocument doc = new XmlDocument();
            doc.Load("CrypTool.xml");

            XmlElement root = doc.DocumentElement;

            XmlNode node = root.SelectSingleNode("./Language");
            
            node.InnerText = selLang;

            doc.Save("CrypTool.xml");

           
        }
    }
}
