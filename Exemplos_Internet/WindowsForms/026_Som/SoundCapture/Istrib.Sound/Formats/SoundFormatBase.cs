//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
//  REMAINS UNCHANGED.
//
//  Email:  lukasz@istrib.org
//
//  Copyright (C) 2008-2009 Lukasz Kwiecinski. 
//

using System;
using System.Collections.Generic;

using System.Text;

namespace Istrib.Sound.Formats
{
    /// <summary>
    /// Base class simplifying implementation of ISoundFormat.
    /// It reads SoundFormatClassAttribute to implement FormatClassName property.
    /// </summary>
    public abstract class SoundFormatBase
        : ISoundFormat
    {
        public string FormatClassName
        {
            get 
            {
                return GetSourceFormatClassName(GetType());
            }
        }

        public abstract string Description
        {
            get;
        }

        public static string GetSourceFormatClassName(Type soundFormatType)
        {
            SoundFormatClassAttribute classAtribute = (SoundFormatClassAttribute)
                    Attribute.GetCustomAttribute(soundFormatType, typeof(SoundFormatClassAttribute));

            if (classAtribute == null)
            {
                return soundFormatType.ToString();
            }

            return classAtribute.Name;
        }
    }
}
