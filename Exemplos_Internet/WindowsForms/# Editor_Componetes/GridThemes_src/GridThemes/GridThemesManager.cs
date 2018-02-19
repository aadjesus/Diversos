using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Web.Compilation;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;


namespace UNLV.IAP.GridThemes
{
    // --------------------------------------------------------------
    // GridThemesManager class
    // --------------------------------------------------------------
    // A static class providing utility methods for retrieving GridTheme
    // titles and method names.
    // --------------------------------------------------------------

    public static class GridThemesManager
    {
        private const string kTYPE = "UNLV.IAP.GridThemes.GridThemesMethods";


        public static string[] ThemeTitles
        {
            get
            {
                StringDictionary sd = GridThemesManager.Themes;
                string[] arr = new string[sd.Count];
                sd.Values.CopyTo(arr, 0);
                return arr;
            }            
        }

        public static string[] ThemeMethods
        {
            get
            {
                StringDictionary sd = GridThemesManager.Themes;
                string[] arr = new string[sd.Count];
                sd.Keys.CopyTo(arr, 0);
                return arr;
            }
        }

        public static StringDictionary Themes
        {
            get
            {
                StringDictionary _themes = new StringDictionary();

                // Since our GridThemesMethods class was dynamically created by the
                // GridThemesBuildProvider, use the BuildManager to retrieve the class
                // as a Type
                Type t = BuildManager.GetType("UNLV.IAP.GridThemes.GridThemesMethods", false);

                if (t != null)
                {
                    // retrieve a list of methods associated with the GridThemesMethods class
                    MethodInfo[] methods = t.GetMethods();

                    // inspect each method to see which ones have the GridThemeAttribute
                    // applied
                    foreach (MethodInfo method in methods)
                    {
                        object[] obj = method.GetCustomAttributes(typeof(GridThemeAttribute), false);
                        if (obj != null && obj.Length > 0)
                        {
                            // if we have a GridThemeAttribute, this is a valid GridTheme
                            // method; add it to the collection
                            GridThemeAttribute att = (obj[0] as GridThemeAttribute);
                            if (att != null)
                            {
                                _themes.Add(method.Name, att.Title);
                            }
                        }
                    }

                }

                return _themes;
            }
        }

        public static string GetThemeMethodNameFromTitle(string title)
        {
            if (title == "" || title == null) return "";

            StringDictionary sd = GridThemesManager.Themes;
            foreach (DictionaryEntry de in sd)
            {
                if (title.ToLower() == de.Value.ToString().ToLower())
                    return de.Key.ToString();
            }
            return "";
        }

        // --------------------------------------------------------------
        // GetThemeMethodFromTitle method
        // --------------------------------------------------------------
        // Return the event handling method given the gridTheme title; this
        // method will follow the signature defined by the 
        // GridViewRowEventHandler delegate type, as it is designed to 
        // handle events of type GridView.RowDataBound
        // --------------------------------------------------------------
        public static GridViewRowEventHandler GetThemeMethodFromTitle(string title)
        {
            if (title == "" || title == null) return null;

            // get the method name given its title
            string methodName = GridThemesManager.GetThemeMethodNameFromTitle(title);
            if (methodName != "")
            {
                // use the BuildManager to get the dynamically created type
                Type t = BuildManager.GetType(kTYPE, false);
                if (t != null)
                {
                    // retrieve the event handler method from the GridThemesMethods class
                    MethodInfo method = t.GetMethod(methodName, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);
                    if (method != null)
                    {
                        // instantiate a GridThemesMethods object
                        object instance = t.Assembly.CreateInstance(kTYPE, false);

                        // return the event handler as an appropriate delegate type
                        //GridViewRowEventHandler handler
                        //    = Delegate.CreateDelegate(typeof(GridViewRowEventHandler), method)
                        //       as GridViewRowEventHandler;

                        // return the event handler of the instance
                        GridViewRowEventHandler handler
                            = Delegate.CreateDelegate(typeof(GridViewRowEventHandler)
                              , instance, methodName, true) as GridViewRowEventHandler;

                        return handler;
                    }                    
                }

                return null;
            }
            else
            {
                throw new GridThemesException(
                    string.Format("The GridTheme '{0}' does not exist."
                                  , title)
                    );
            }

        }

        // --------------------------------------------------------------
        // AssignThemeToGridView
        // --------------------------------------------------------------
        // assign the appropriate method to the GridView.RowDataBound event
        // --------------------------------------------------------------
        public static void AssignThemeToGridView(string title, GridView gv)
        {
            GridViewRowEventHandler h = GridThemesManager.GetThemeMethodFromTitle(title);

            if (h != null)
                gv.RowDataBound += h;

        }

    }



}
