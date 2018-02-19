using System.ComponentModel;

/*
  Custom Type Converter Sample 2
  http://cyotek.com/blog/creating-a-custom-typeconverter-part-2
*/

namespace CustomTypeConverter2
{
    internal class SampleClass : Component
    {
        #region Properties

        [DefaultValue(typeof(Length), "")]
        public Length Length1 { get; set; }

        [DefaultValue(typeof(Length), "")]
        public Length Length2 { get; set; }

        [DefaultValue(typeof(Length), "")]
        public Length Length3 { get; set; }

        #endregion
    }
}
