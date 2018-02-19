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
    /// Decorates a type which represents some format class (like WAV or MP3).
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, 
        Inherited = false, AllowMultiple = false)]
    public sealed class SoundFormatClassAttribute : Attribute
    {
        public string Name
        {
            get;
            private set;
        }

        // This is a positional argument
        public SoundFormatClassAttribute(string name)
        {
            this.Name = name;
        }
    }
}
