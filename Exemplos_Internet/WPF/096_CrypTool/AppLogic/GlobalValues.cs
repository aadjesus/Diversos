using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CrypTool.AppLogic
{
    public static class GlobalValues
    {
        static private ArrayList searchValues;
        static private ArrayList replaceValues;
        static private bool bHighLightText;
        static private bool bWhiteSpaces;

        static GlobalValues()
        {
            searchValues = new ArrayList();
            replaceValues = new ArrayList();
            bHighLightText = false;
        }
        public static void addSearchValue(String strSearchValue)
        {
            searchValues.Remove(strSearchValue);
            searchValues.Insert(0, strSearchValue);
        }
        public static void addReplaceValue(String strReplaceValue)
        {
            replaceValues.Remove(strReplaceValue);
            replaceValues.Insert(0, strReplaceValue);
        }
        public static String getCurrentSearchValue()
        {
            return searchValues[0].ToString();
        }
        public static String getCurrentReplaceValue()
        {
            return replaceValues[0].ToString();
        }
        public static ArrayList getSearchValues()
        {
            return searchValues;
        }
        public static ArrayList getReplaceValues()
        {
            return replaceValues;
        }
        public static void setHighLightText(bool bHighLight)
        {
            bHighLightText = bHighLight;
        }
        public static bool getHighLightText()
        {
            return bHighLightText;
        }
        public static void setWhiteSpaces(bool bWhtSpcs)
        {
            bWhiteSpaces = bWhtSpcs;
        }
        public static bool getWhiteSpaces()
        {
            return bWhiteSpaces;
        }
    }
}
