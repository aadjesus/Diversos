using System.ComponentModel;
using System.Globalization;

/*
  Custom Type Converter Sample 2
  http://cyotek.com/blog/creating-a-custom-typeconverter-part-2
*/

namespace CustomTypeConverter2
{
    [TypeConverter(typeof(LengthConverter))]
    internal class Length
    {
        #region Constructors

        public Length()
        { }

        public Length(float value, Unit unit)
            : this()
        {
            this.Value = value;
            this.Unit = unit;
        }

        #endregion

        #region Overridden Members

        public override string ToString()
        {
            string value;
            string unit;

            value = this.Value.ToString(CultureInfo.InvariantCulture);
            unit = this.Unit.ToString();

            return string.Concat(value, unit);
        }

        #endregion

        #region Properties

        [DefaultValue(typeof(Unit), "None")]
        public Unit Unit { get; set; }

        [DefaultValue(0F)]
        public float Value { get; set; }

        #endregion
    }
}
