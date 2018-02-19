//--------------------------------------
// State.cs (c) 2007 by Charles Petzold
//--------------------------------------
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Petzold.StatePopulationAnimator
{
    public class State
    {
        string name = "N/A";
        string capital = "N/A";
        float longitude = 0;
        float latitude = 0;
        List<Population> pops = new List<Population>();

        [XmlAttribute]
        public string Name
        {
            set { name = value; }
            get { return name; }
        }

        [XmlAttribute]
        public string Capital
        {
            set { capital = value; }
            get { return capital; }
        }

        [XmlAttribute]
        public float Longitude
        {
            set { longitude = value; }
            get { return longitude; }
        }

        [XmlAttribute]
        public float Latitude
        {
            set { latitude = value; }
            get { return latitude; }
        }

        [XmlElement("Population")]
        public List<Population> Populations
        {
            set { pops = value; }
            get { return pops; }
        }
    }
}