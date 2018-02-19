using System;

namespace Research.MVP.PresentersImpl
{

    public interface IDateTimeFormatChanger
    {

        // Events

        event DateTimeFormatEventHandler DateTimeFormatChanged;

    }

    public delegate void DateTimeFormatEventHandler(object sender, DateTimeFormatEventArgs e);

    public class DateTimeFormatEventArgs : EventArgs
    {

        // Fields

        private string format;


        // Methods

        public DateTimeFormatEventArgs(string format)
        {
            this.format = format;
        }


        // Properties

        public string Format
        {
            get { return this.format; }
        }

    }

}
