//----------------------------------------------------
// StatePopulationData.cs (c) 2007 by Charles Petzold
//----------------------------------------------------
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Petzold.StatePopulationAnimator
{
    public class StatePopulationData
    {
        List<State> states = new List<State>();

        [XmlElement("State")]
        public List<State> States
        {
            set { states = value; }
            get { return states; }
        }
    }
}
