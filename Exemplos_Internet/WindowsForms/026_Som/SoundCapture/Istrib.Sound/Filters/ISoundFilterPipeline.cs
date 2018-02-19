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

namespace Istrib.Sound.Filters
{
    /// <summary>
    /// Interface of filter pipeline component.
    /// </summary>
    public interface ISoundFilterPipeline
    {
        /// <summary>
        /// Specifies pipeline filters and their order.
        /// This list cannot be modified after the pipeline has been wired up
        /// by the pipeline component (e.g. SoundFilter wires up its pipeline on
        /// first call to Start).
        /// </summary>
        IList<SoundFilter> Filters
        {
            get;
        }
    }
}
