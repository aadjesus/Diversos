using System;

namespace Asaasoft.DigitalMeter
{
    internal static class HelperClass
    {
        /// <summary>
        /// Format decimal value
        /// <example>result of value=80.2, precision=5, scalingFactor=2 will be 080.20 </example>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="precision"></param>
        /// <param name="scalingFactor"></param>
        /// <returns>formatted decimal value</returns>
        internal static string FormatDecimalValue( decimal value, int precision, int scalingFactor )
        {
            string valueText = "";

            if ( scalingFactor == 0 )
            {
                valueText = Math.Round(value, 0).ToString().PadLeft(precision, '0');
            }
            else
            {
                decimal integralValue = Decimal.Truncate(value);

                decimal fractionalValue = Math.Round(value - integralValue, scalingFactor);
                string fractionalValueText = fractionalValue.ToString();
                if ( fractionalValueText.IndexOf( '.' ) > 0 )
                    fractionalValueText = fractionalValueText.Remove(0, 2);
                valueText = integralValue.ToString().PadLeft(precision - scalingFactor, '0');
                valueText = string.Format("{0}.{1}", valueText, fractionalValueText.PadRight(scalingFactor, '0'));
            }

            if ( ( scalingFactor == 0 && valueText.Length > precision ) || ( scalingFactor > 0 && valueText.Length > precision + 1 ) )
                valueText = string.Empty.PadLeft(precision - scalingFactor, '#') + "." + string.Empty.PadLeft(scalingFactor, '#');

            return valueText;

        }
    }
}
