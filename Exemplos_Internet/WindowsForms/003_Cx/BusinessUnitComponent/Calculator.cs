using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cx.Attributes;
using Cx.EventArgs;
using Cx.Interfaces;

namespace BusinessUnitComponent
{
	[CxComponentName("Calculator Business Unit")]
	public class Calculator : ICxBusinessComponentClass
	{
		[CxEvent] public event CxStringDlgt CurrentValueChanged;

		protected enum PendingOperation
		{
			None,
			Add,
			Subtract,
			Multiply,
			Divide,
		}

		protected PendingOperation pendingOperation;
		protected string lastValue;
		protected string currentValue;

		public string CurrentValue
		{
			get { return currentValue; }
			set
			{
				if (currentValue != value)
				{
					currentValue = value;
					OnCurrentValueChanged();
				}
			}
		}

		public Calculator()
		{
			pendingOperation = PendingOperation.None;
		}

		[CxConsumer]
		public void SetCurrentValue(object sender, CxEventArgs<string> args)
		{
			CurrentValue = args.Data;
		}

		[CxConsumer]
		public void Add(object sender, EventArgs e)
		{
			Calculate();
			lastValue = currentValue;
			pendingOperation = PendingOperation.Add;
		}

		[CxConsumer]
		public void Subtract(object sender, EventArgs e)
		{
			Calculate();
			lastValue = currentValue;
			pendingOperation = PendingOperation.Subtract;
		}

		[CxConsumer]
		public void Multiply(object sender, EventArgs e)
		{
			Calculate();
			lastValue = currentValue;
			pendingOperation = PendingOperation.Multiply;
		}

		[CxConsumer]
		public void Divide(object sender, EventArgs e)
		{
			Calculate();
			lastValue = currentValue;
			pendingOperation = PendingOperation.Divide;
		}

		[CxConsumer]
		public void Equal(object sender, EventArgs e)
		{
			Calculate();
			pendingOperation = PendingOperation.None;
		}

		[CxConsumer]
		public void Clear(object sender, EventArgs e)
		{
			CurrentValue = "0";
			pendingOperation = PendingOperation.None;
		}

		protected void Calculate()
		{
			try
			{
				switch (pendingOperation)
				{
					case PendingOperation.Add:
						CurrentValue = Convert.ToString(Convert.ToDouble(lastValue) + Convert.ToDouble(currentValue));
						break;

					case PendingOperation.Subtract:
						CurrentValue = Convert.ToString(Convert.ToDouble(lastValue) - Convert.ToDouble(currentValue));
						break;

					case PendingOperation.Multiply:
						CurrentValue = Convert.ToString(Convert.ToDouble(lastValue) * Convert.ToDouble(currentValue));
						break;

					case PendingOperation.Divide:
						CurrentValue = Convert.ToString(Convert.ToDouble(lastValue) / Convert.ToDouble(currentValue));
						break;
				}
			}
			catch
			{
				CurrentValue = "Error";
			}

			pendingOperation = PendingOperation.None;
		}

		protected void OnCurrentValueChanged()
		{
			EventHelpers.Fire(CurrentValueChanged, this, new CxEventArgs<string>(currentValue));
		}
	}
}
