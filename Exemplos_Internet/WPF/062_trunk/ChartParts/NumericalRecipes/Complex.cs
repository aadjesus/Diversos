// Ported from "Numerical Recipes in C, 2-nd Edition" to C# 3.0.
// Chapter 5.4 Complex Arithmetic
// file Complex.cs
// <revision>$Id: Complex.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Globalization; // For CultureInfo

namespace NumericalRecipes
{

	/// <summary>
	/// Complex Number
	/// </summary>
	/// <exclude />
	public struct Complex
	{
		/// <summary>
		/// Zero (0, 0) Complex Number.
		/// </summary>
		public static readonly Complex Zero = new Complex();

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Complex"/> struct.
		/// </summary>
		/// <param name="re">The re.</param>
		/// <param name="im">The im.</param>
		/// <overloads>This method has two overloads</overloads>
		public Complex(double re, double im)
		{
			this.re = re;
			this.im = im;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Complex"/> struct.
		/// </summary>
		/// <param name="re">The re.</param>
		public Complex(double re) : this(re, 0d)
		{
		}
		#endregion Constructors

		private double re;
		/// <summary>
		/// Gets or sets the Real part of the Complex value.
		/// </summary>
		/// <value/>
		public double Real
		{
			get { return re; }
		}

		private double im;
		/// <summary>
		/// Gets or sets the Imaginary part of the Complex value.
		/// </summary>
		/// <value/>
		public double Imaginary
		{
			get { return im; }
		}

		#region Object overrides
		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != typeof(Complex))
				return false;
			Complex c = (Complex)obj;
			if (re == c.Real && im == c.Imaginary)
				return true;
			return false;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return re.GetHashCode() ^ im.GetHashCode();
		}

		/// <inheritdoc />
		public override string ToString()
		{
			if (im == 0.0)
				return re.ToString(CultureInfo.CurrentCulture);
			if (re == 0.0)
				return string.Format(CultureInfo.CurrentCulture, "{0}i{1}", im < 0.0 ? "-" : "+", Math.Abs(im));
			return string.Format(CultureInfo.CurrentCulture, "{0}{1}i{2}", re, im < 0.0 ? "-" : "+", Math.Abs(im));
		}
		#endregion Object overrides

		#region Operators
		/// <summary>
		/// Implements the operator -.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the operator.</returns>
		public static Complex operator-(Complex value)
		{
			return Negate(value);
		}
		/// <summary>
		/// Implements the operator +.
		/// </summary>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns>The result of the operator.</returns>
		public static Complex operator+(Complex first, Complex second)
		{
			return Add(first, second);
		}
		/// <summary>
		/// Implements the operator -.
		/// </summary>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns>The result of the operator.</returns>
		public static Complex operator-(Complex first, Complex second)
		{
			return Subtract(first, second);
		}
		/// <summary>
		/// Implements the operator *.
		/// </summary>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns>The result of the operator.</returns>
		public static Complex operator*(Complex first, Complex second)
		{
			return Multiply(first, second);
		}
		/// <summary>
		/// Implements the operator *.
		/// </summary>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns>The result of the operator.</returns>
		public static Complex operator*(double first, Complex second)
		{
			return Multiply(first, second);
		}
		/// <summary>
		/// Implements the operator /.
		/// </summary>
		/// <param name="dividend">The dividend.</param>
		/// <param name="divisor">The divisor.</param>
		/// <returns>The result of the operator.</returns>
		public static Complex operator/(Complex dividend, Complex divisor)
		{
			return Divide(dividend, divisor);
		}
		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator==(Complex first, Complex second)
		{
			return Equals(first, second);
		}
		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="first">The first.</param>
		/// <param name="second">The second.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator!=(Complex first, Complex second)
		{
			return !(first == second);
		}
		#endregion Operators

		/// <summary>
		/// Negates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static Complex Negate(Complex value)
		{
			return new Complex(-value.Real, -value.Imaginary);
		}

		/// <summary>
		/// Sums up the first and the second arguments.
		/// </summary>
		/// <param name="first">The first item.</param>
		/// <param name="second">The second item.</param>
		/// <returns>The Sum.</returns>
		public static Complex Add(Complex first, Complex second)
		{
			return new Complex(first.Real + second.Real, first.Imaginary + second.Imaginary);
		}

		/// <summary>
		/// Subtracts the specified second argument from the first argument.
		/// </summary>
		/// <param name="first">The first item.</param>
		/// <param name="second">The second item.</param>
		/// <returns>The Difference.</returns>
		public static Complex Subtract(Complex first, Complex second)
		{
			return new Complex(first.Real - second.Real, first.Imaginary - second.Imaginary);
		}

		/// <summary>
		/// Multiplies the first and the second arguments.
		/// </summary>
		/// <param name="first">The first item.</param>
		/// <param name="second">The second item.</param>
		/// <returns>The Product.</returns>
		public static Complex Multiply(Complex first, Complex second)
		{
			return new Complex(first.Real * second.Real - first.Imaginary * second.Imaginary
				, first.Imaginary * second.Real + first.Real * second.Imaginary);
		}

		/// <summary>
		/// Multiplies the first and the second arguments.
		/// </summary>
		/// <param name="first">The first item.</param>
		/// <param name="second">The second item.</param>
		/// <returns>The Product.</returns>
		public static Complex Multiply(double first, Complex second)
		{
			return new Complex(second.Real * first, second.Imaginary * first);
		}

		/// <summary>
		/// Divides the specified dividend by the divisor.
		/// </summary>
		/// <param name="dividend">The dividend item.</param>
		/// <param name="divisor">The divisor item.</param>
		/// <returns>The Quotient.</returns>
		public static Complex Divide(Complex dividend, Complex divisor)
		{
			double div = divisor.Real * divisor.Real + divisor.Imaginary * divisor.Imaginary;
			return new Complex((dividend.Real * divisor.Real + dividend.Imaginary * divisor.Imaginary) / div, (dividend.Imaginary * divisor.Real - dividend.Real * divisor.Imaginary) / div);
		}

		/// <summary>
		/// Tests the specified arguments for equality.
		/// </summary>
		/// <param name="first">The first item.</param>
		/// <param name="second">The second item.</param>
		/// <returns></returns>
		public static bool Equals(Complex first, Complex second)
		{
			return first.Equals(second);
		}

		/// <summary>
		/// Gets the Conjugate value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static Complex Conjugate(Complex value)
		{
			return new Complex(value.Real, -value.Imaginary);
		}

		/// <summary>
		/// Gets the Absolute value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static double Abs(Complex value)
		{
			double x = Math.Abs(value.Real);
			double y = Math.Abs(value.Imaginary);
			double ans, temp;
			if (x == 0.0)
				ans = y;
			else if (y == 0.0)
				ans = x;
			else if (x > y)
			{
				temp = y / x;
				ans = x * Math.Sqrt(1.0 + temp * temp);
			}
			else
			{
				temp = x / y;
				ans = y * Math.Sqrt(1.0 + temp * temp);
			}
			return ans;
		}

		/// <summary>
		/// Gets the Square root value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static Complex Sqrt(Complex value)
		{
			if (value.Real == 0.0 && value.Imaginary == 0.0)
			{
				return new Complex();
			}
			else
			{
				double x = Math.Abs(value.Real);
				double y = Math.Abs(value.Imaginary);
				double w, r;
				if (x >= y)
				{
					r = y / x;
					w = Math.Sqrt(x) * Math.Sqrt(0.5 * (1.0 + Math.Sqrt(1.0 + r * r)));
				}
				else
				{
					r = x / y;
					w = Math.Sqrt(y) * Math.Sqrt(0.5 * (r + Math.Sqrt(1.0 + r * r)));
				}

				if (value.Real >= 0.0)
					return new Complex(w, value.Imaginary / (2.0 * w));
				else
				{
					double im = (value.Imaginary >= 0) ? w : -w;
					return new Complex(value.Imaginary / (2.0 * im), im);
				}
			}
		}
	}
}
