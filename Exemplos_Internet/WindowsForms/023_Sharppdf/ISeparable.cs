using System;
using System.Collections;

using sharpPDF.Collections;

namespace sharpPDF
{
	/// <summary>
	/// Interface for all separable objects.
	/// </summary>
	public interface ISeparable
	{
		/// <summary>
		/// This Method returns the standard elements which the composite element is made of.
		/// </summary>
		/// <returns>Collection of basic elements</returns>
		elementList GetBasicElements();
	}
}
