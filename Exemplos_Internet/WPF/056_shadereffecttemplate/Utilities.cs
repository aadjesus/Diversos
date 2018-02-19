using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ShaderEffectTemplate {
    class Utilities {
        /// <summary>
        /// Returns a pack URI for a given resource within the current assembly
        /// </summary>
        /// <param name="resourceFileName">folder (if any) and name of the resource, e.g. MyEffect1/MyEffect1.ps</param>
        /// <returns></returns>
        public static Uri GetResourcePackUri(string resourceFileName) {
            string uriString = "pack://application:,,,/" + CurrentAssemblyName + ";component/" + resourceFileName;
            return new Uri(uriString);
        }

        private static string _currentAssemblyName = null;
        private static string CurrentAssemblyName {
            get {
                if (_currentAssemblyName == null) {
                    Assembly a = typeof(Utilities).Assembly;
                    _currentAssemblyName = a.ToString().Split(',')[0];
                }

                return _currentAssemblyName;
            }
        }

    }
}
