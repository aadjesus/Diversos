//        Another Demo from Andy L. & MissedMemo.com
// Borrow whatever code seems useful - just don't try to hold
// me responsible for any ill effects. My demos sometimes use
// licensed images which CANNOT legally be copied and reused.

using System.Windows;
using System.Collections.Generic;
using System.Reflection;


namespace ClientUI
{
    // This class stores a copy of a control's current property values,
    // and can re-apply them to the control. The WPF 'ClearValue' option
    // can only revert to original DependencyProperty settings, while we
    // can cache a snapshot of property settings at ANY point in time.
     
    class PropertyValuesSnapshot
    {
        private Dictionary<string, object> _originalValues = new Dictionary<string, object>();


        internal void CopyValues( FrameworkElement element )
        {
            foreach( PropertyInfo prop in element.GetType().GetProperties() )
            {
                _originalValues.Add( prop.Name, prop.GetValue( element, null ) );
            }
        }


        internal void RestoreValues( FrameworkElement element )
        {
            foreach( PropertyInfo prop in element.GetType().GetProperties() )
            {
                object o = _originalValues[prop.Name];

                if( prop.CanWrite && o != prop.GetValue( element, null ) )
                {
                    prop.SetValue( element, o, null );
                }
            }
        }
    }
}
