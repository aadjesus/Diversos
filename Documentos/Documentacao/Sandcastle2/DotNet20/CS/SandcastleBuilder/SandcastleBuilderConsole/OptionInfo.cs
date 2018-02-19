//=============================================================================
// System  : Sandcastle Help File Builder
// File    : OptionInfo.cs
// Author  : Eric Woodruff
// Updated : 02/23/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the command line option information class.
//
// This code may be used in compiled form in any way you desire.  This file
// may be redistributed unmodified by any means PROVIDING it is not sold for
// profit without the author's written consent, and providing that this notice
// and the author's name and all copyright notices remain intact.
//
// This code is provided "as is" with no warranty either express or implied.
// The author accepts no liability for any damage or loss of business that
// this product may cause.
//
// Version     Date     Who  Comments
// ============================================================================
// 1.3.4.0  12/27/2006  EFW  Created the code
// 1.4.0.0  02/23/2007  EFW  Added support for a third value option
//=============================================================================

using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SandcastleBuilderConsole
{
    /// <summary>
    /// This is the console mode version of the Sandcastle Help File Builder.
    /// </summary>
    class OptionInfo
    {
        #region Private data members
        //=====================================================================
        // Private data members

        private static Regex reSplit =
            new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        private string optionText, name, value, secondValue, thirdValue;
        #endregion

        #region Properties
        //=====================================================================
        // Properties

        /// <summary>
        /// Get the option text as specified
        /// </summary>
        public string OptionText
        {
            get { return optionText; }
        }

        /// <summary>
        /// Get the option name
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Get the option value
        /// </summary>
        public string Value
        {
            get { return value; }
        }

        /// <summary>
        /// Get the second option value if there was one
        /// </summary>
        public string SecondValue
        {
            get { return secondValue; }
        }

        /// <summary>
        /// Get the third option value if there was one
        /// </summary>
        public string ThirdValue
        {
            get { return thirdValue; }
        }
        #endregion

        //=====================================================================
        // Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public OptionInfo(string option)
        {
            int pos;

            optionText = option.Trim();

            // Is is a project file
            if(optionText[0] != '-' && optionText[0] != '/')
            {
                name = "project";
                value = optionText;
                return;
            }

            pos = optionText.IndexOf('=');

            if(pos < 2)
                name = optionText.Substring(1).ToLower(
                    CultureInfo.InvariantCulture);
            else
            {
                name = optionText.Substring(1, pos - 1).Trim().ToLower(
                    CultureInfo.InvariantCulture);
                value = optionText.Substring(pos + 1).Trim();

                if(reSplit.IsMatch(value))
                {
                    string[] parts = reSplit.Split(value);
                    value = parts[0].Trim();
                    secondValue = parts[1].Trim();

                    if(parts.Length > 2)
                        thirdValue = parts[2].Trim();
                }

                // Strip quotes from around the values
                if(value[0] == '\"')
                    value = value.Substring(1, value.Length - 2).Trim();

                if(!String.IsNullOrEmpty(secondValue) && secondValue[0] == '\"')
                    secondValue = secondValue.Substring(1,
                        secondValue.Length - 2).Trim();

                if(!String.IsNullOrEmpty(thirdValue) && thirdValue[0] == '\"')
                    thirdValue = thirdValue.Substring(1,
                        thirdValue.Length - 2).Trim();
            }
        }
    }
}
