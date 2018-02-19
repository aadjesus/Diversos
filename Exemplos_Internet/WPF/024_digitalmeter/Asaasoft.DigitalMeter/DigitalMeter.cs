using System.Windows;
using System.Windows.Controls;

namespace Asaasoft.DigitalMeter
{
    public class DigitalMeter : Control
    {
        #region Constructors
        public DigitalMeter()
        {
            DefaultStyleKey = typeof(DigitalMeter);
        }
        #endregion

        #region Dependency Properties

        public int Precision
        {
            get
            {
                return (int)GetValue(PrecisionProperty);
            }
            set
            {
                SetValue(PrecisionProperty, value);
            }
        }

        public static readonly DependencyProperty PrecisionProperty =
            DependencyProperty.Register("Precision", typeof(int), typeof(DigitalMeter), new PropertyMetadata( 5, new PropertyChangedCallback(SetValueText)));

        public int ScalingFactor
        {
            get
            {
                return (int)GetValue(ScalingFactorProperty);
            }
            set
            {
                SetValue(ScalingFactorProperty, value);
            }
        }

        public static readonly DependencyProperty ScalingFactorProperty =
            DependencyProperty.Register("ScalingFactor", typeof(int), typeof(DigitalMeter), new PropertyMetadata(0, new PropertyChangedCallback(SetValueText)) );

        public decimal Value
        {
            get
            {
                return (decimal)GetValue(ValueProperty);
            }
            set
            {
                decimal oldValue = Value;
                SetValue(ValueProperty, value);

                if ( oldValue != value )
                    OnValueChanged( oldValue, value );
            }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(decimal), typeof(DigitalMeter), new PropertyMetadata( 0M, new PropertyChangedCallback( SetValueText ) ) );

        public string ValueText
        {
            get
            {
                return (string)GetValue(ValueTextProperty);
            }
            set
            {
                SetValue(ValueTextProperty, value);
            }
        }

        public static readonly DependencyProperty ValueTextProperty =
            DependencyProperty.Register("ValueText", typeof(string), typeof(DigitalMeter), new PropertyMetadata( "00000" ) );



        public string MeasurementUnit
        {
            get
            {
                return (string)GetValue(MeasurementUnitProperty);
            }
            set
            {
                SetValue(MeasurementUnitProperty, value);
            }
        }

        public static readonly DependencyProperty MeasurementUnitProperty =
            DependencyProperty.Register("MeasurementUnit", typeof(string), typeof(DigitalMeter), new UIPropertyMetadata("km"));


        #endregion

        #region ValueChanged Routed Event
        public delegate void ValueChangedEventHandler( object sender, ValueChangedEventArgs e );

        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(ValueChangedEventHandler), typeof(DigitalMeter));

        public event ValueChangedEventHandler ValueChanged
        {
            add
            {
                AddHandler(ValueChangedEvent, value);
            }
            remove
            {
                RemoveHandler(ValueChangedEvent, value);
            }
        }

        protected void OnValueChanged( decimal oldValue, decimal newValue )
        {
            ValueChangedEventArgs e = new ValueChangedEventArgs(DigitalMeter.ValueChangedEvent, oldValue, newValue );
            RaiseEvent(e);
        }
        #endregion

        #region Private Methods
        private static void SetValueText( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            DigitalMeter dm = (DigitalMeter)d;
            dm.ValueText = HelperClass.FormatDecimalValue(dm.Value, dm.Precision, dm.ScalingFactor );
        }
        #endregion
    }

    #region ValueChangedEventArgs
    public class ValueChangedEventArgs : RoutedEventArgs
    {
        public ValueChangedEventArgs( RoutedEvent routedEvent, decimal oldValue, decimal newValue ) : base( routedEvent )
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        public decimal OldValue
        {
            get;
            private set;
        }

        public decimal NewValue
        {
            get;
            private set;
        }
    }
    #endregion
}
