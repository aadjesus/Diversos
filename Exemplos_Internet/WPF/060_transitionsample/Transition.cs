using System.Windows;

public class Transition : FrameworkElement
{
	public enum TransitionState
	{
		A,
		B
	}

	public static DependencyProperty SourceProperty;
	public static DependencyProperty DisplayAProperty;
	public static DependencyProperty DisplayBProperty;
	public static DependencyProperty StateProperty;

	public object Source
	{
		get { return this.GetValue(SourceProperty); }
		set { this.SetValue(SourceProperty, value); }
	}

	public object DisplayA
	{
		get { return this.GetValue(DisplayAProperty); }
		set { this.SetValue(DisplayAProperty, value); }
	}

	public object DisplayB
	{
		get { return this.GetValue(DisplayBProperty); }
		set { this.SetValue(DisplayBProperty, value); }
	}

	public TransitionState State
	{
		get { return (TransitionState) this.GetValue(StateProperty); }
		set { this.SetValue(StateProperty, value); }
	}

	private void Swap()
	{
		if (this.State == TransitionState.A)
		{
			this.DisplayB = this.Source;
			this.State = TransitionState.B;
		}
		else
		{
			this.DisplayA = this.Source;
			this.State = TransitionState.A;
		}
	}

	static Transition()
	{
		SourceProperty = DependencyProperty.Register(
			"Source", 
			typeof(object), 
			typeof(Transition), 
			new PropertyMetadata(
				delegate(DependencyObject obj, DependencyPropertyChangedEventArgs args)
				{
					((Transition)obj).Swap();
				}));

		DisplayAProperty = DependencyProperty.Register(
			"DisplayA", 
			typeof(object), 
			typeof(Transition));

		DisplayBProperty = DependencyProperty.Register(
			"DisplayB", 
			typeof(object), 
			typeof(Transition));

		StateProperty = DependencyProperty.Register(
			"State", 
			typeof(TransitionState), 
			typeof(Transition), 
			new PropertyMetadata(TransitionState.A));
	}
}