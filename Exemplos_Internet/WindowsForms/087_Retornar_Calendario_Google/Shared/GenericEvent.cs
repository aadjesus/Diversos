using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericEntities
{
    public class GenericEvent : IEquatable<GenericEvent>
    {
        public string Title { get; set; }
        public string Contents { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        #region IEquatable<GenericEvent> Members

        public bool Equals(GenericEvent other)
        {
            //Compare all fields to check equality
            if (this.Title == other.Title &&
                this.Contents == other.Contents &&
                this.Location == other.Location &&
                this.StartTime == other.StartTime &&
                this.EndTime == other.EndTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
