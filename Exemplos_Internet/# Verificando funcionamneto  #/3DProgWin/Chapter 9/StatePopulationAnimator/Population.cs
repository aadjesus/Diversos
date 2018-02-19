using System;
using System.Xml.Serialization;

namespace Petzold.StatePopulationAnimator
{
    public class Population
    {
        int year = 0;
        int count = 0;

        [XmlAttribute]
        public int Year
        {
            set { year = value; }
            get { return year; }
        }

        [XmlAttribute]
        public int Count
        {
            set { count = value; }
            get { return count; }
        }
    }
}