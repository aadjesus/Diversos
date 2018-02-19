// Ported from "Numerical Recipes in C, 2-nd Edition" to C# 3.0.
// Chapter 5. Evaluation of Functions
// 5.3 Polynomials and Rational Functions
// file Polynomial.cs
// <revision>$Id: Polynomial.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Text;

namespace NumericalRecipes
{
	/// <summary>
	/// Polynomial class.
	/// </summary>
	public class Polynomial : ICloneable
	{
		/// <summary>
		/// Coefficients of a polynomial of degree m_c.Length-1.
		/// Coefficient follows from the lowest to the higest order (i.e. c[0] is the free member).
		/// c0 + c1*x + c2*x^2 + ... + cn*x^n
		/// </summary>
		double[] m_c;

		/// <summary>
		/// class constructor
		/// </summary>
		/// <param name="c">coefficients of a polynomial of degree c.Length-1;
		///		coefficient follows from the lowest to the higest order
		///		(i.e. c[0] is the free member)</param>
		public Polynomial(double[] c)
		{
			if (c == null)
				throw new ArgumentNullException("c");
			if (c.Length == 0)
				throw new ArgumentException("array length must be > 0", "c");
			if (c[c.Length - 1] == 0.0)
				throw new ArgumentException("Higest coefficient can't be 0", "c");
			m_c = (double[])c.Clone();
		}

		/// <summary>
		/// Order of polynomial
		/// </summary>
		/// <remarks>This is a coefficient count - 1</remarks>
		public int Order
		{
			get { return m_c.Length - 1; }
		}

		/// <summary>
		/// Polynomial coefficient indexer
		/// </summary>
		/// <param name="i">index must be &lt;= Order</param>
		/// <returns>ith polynomial coefficient</returns>
		public double this[int i]
		{
			get { return m_c[i]; }
		}

		/// <summary>
		/// Evaluates polynomial value
		/// </summary>
		/// <param name="x">value to evaluate at</param>
		public double GetValue(double x)
		{
			int nc = m_c.Length - 1;

			double val = m_c[nc];
			for (int i = nc - 1; i >= 0; --i)
			{
				val = val * x + m_c[i];
			}
			return val;
		}

		/// <summary>
		/// Evaluates polynomial value and its derivatives at x
		/// </summary>
		/// <param name="x">value to evaluate at</param>
		/// <param name="pd">[out] polynomial evaluated at x as pd[0] and
		/// nd derivatives as pd[1..nd]</param>
		/// <remarks>
		/// Given the nc+1 coefficients of a polynomial of degree nc as an array c[0..nc]
		/// with c[0] being the constant term, and given a value x, and given a value nd&gt;1,
		/// this routine returns the polynomial evaluated at x as pd[0] and nd derivatives
		/// as pd[1..nd].
		/// </remarks>
		public void GetValue(double x, double[] pd)
		{
			if (m_c.Length == 0 || pd.Length == 0)
				throw new ArgumentException("array length must be > 0", "m_c|pd");
			int nc = m_c.Length - 1;
			int nd = pd.Length - 1;

			pd[0] = m_c[nc];
			for (int i = 1; i <= nd; ++i) 
				pd[i] = 0.0;
			for (int i = nc - 1; i >= 0; --i)
			{
				int nnd = (nd < (nc - i) ? nd : nc - i);
				for (int j = nnd; j >= 1; --j)
					pd[j] = pd[j] * x + pd[j - 1];
				pd[0] = pd[0] * x + m_c[i];
			}
			double cnst = 1.0;
			for (int i = 2; i <= nd; ++i)
			{ // After the first derivative, factorial constants come in.
				cnst *= i;
				pd[i] *= cnst;
			}
		}

		#region ICloneable Members
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>
		/// A new object that is a copy of this instance.
		/// </returns>
		public Object Clone()
		{
			return new Polynomial(m_c);
		}
		#endregion ICloneable Members

		/// <summary>
		/// Add a polynomial
		/// </summary>
		/// <param name="p">polynomial to add</param>
		public Polynomial Add(Polynomial p)
		{
			int n = Math.Max(m_c.Length, p.m_c.Length);

			double[] q = new double[n];

			for (int i = 0; i < n; ++i)
			{
				double c1 = 0, c2 = 0;
				if (i < m_c.Length)
					c1 = m_c[i];
				if (i < p.m_c.Length)
					c2 = p.m_c[i];
				q[i] = c1 + c2;
			}
			return new Polynomial(q);
		}

		/// <summary>
		/// Subtract a polynomial
		/// </summary>
		/// <param name="p">polynomial to subtract</param>
		public Polynomial Subtract(Polynomial p)
		{
			int n = Math.Max(m_c.Length, p.m_c.Length);

			double[] q = new double[n];

			for (int i = 0; i < n; ++i)
			{
				double c1 = 0, c2 = 0;
				if (i < m_c.Length)
					c1 = m_c[i];
				if (i < p.m_c.Length)
					c2 = p.m_c[i];
				q[i] = c1 - c2;
			}
			return new Polynomial(q);
		}

		/// <summary>
		/// Multiply this by a polynomial
		/// </summary>
		/// <param name="p">polynomial</param>
		public Polynomial Multiply(Polynomial p)
		{
			double[] q = new double[Order + p.Order + 1];

			for(int i = 0; i < m_c.Length; i++)
			{
				for (int j = 0; j < p.m_c.Length; j++)
				{
					q[i + j] += m_c[i] * p.m_c[j];
				}
			}

			return new Polynomial(q);
		}

		/// <summary>
		/// DivideResult class.
		/// </summary>
		public struct DivideResult
		{
			internal Polynomial q; // quotient
			internal Polynomial r; // residual

			/// <summary>
			/// Gets the quotient.
			/// </summary>
			/// <value>The quotient.</value>
			public Polynomial Quotient
			{
				get { return q; }
			}
			/// <summary>
			/// Gets the residual.
			/// </summary>
			/// <value>The residual.</value>
			public Polynomial Residual
			{
				get { return r; }
			}
		}

		/// <summary>
		/// Given the n+1 coefficients of a polynomial of degree n in u[0..n], and 
		/// the nv+1 coefficients of another polynomial of degree nv in v[0..nv], 
		/// divide the polynomial u by the polynomial v ("u"/"v") giving a quotient 
		/// polynomial whose coefficients are returned in q[0..n], and a
		/// remainder polynomial whose coefficients are returned in r[0..n]. 
		/// The elements r[nv..n] and q[n-nv+1..n] are returned as zero.
		/// </summary>
		/// <param name="div">deviser</param>
		public DivideResult Divide(Polynomial div)
		{
			int n = m_c.Length - 1;
			double[] v = div.m_c;
			int nv = v.Length - 1;

			double[] q = new double[n];
			double[] r = (double[])m_c.Clone();
			DivideResult res = new DivideResult();

			for (int k = n - nv; k >= 0; k--)
			{
				q[k] = r[nv + k] / v[nv];
				for (int j = nv + k - 1; j >= k; j--) 
					r[j] -= q[k] * v[j - k];
			}
			//for (int j = nv; j <= n; j++)
			//    r[j] = 0.0;
			res.q = new Polynomial(q);

			// find last residual item != 0
			int nn;
			for (nn = nv - 1; nn >= 0; --nn)
			{
				if (r[nn] != 0.0)
					break;
			}
			nn++;
			if (nn > 0)
			{
				double[] r1 = new double[nn];
				Array.Copy(r, r1, nn);
				res.r = new Polynomial(r1);
			}
			else
				res.r = null;

			return res;
		}

		/// <summary>
		/// Polynomial coefficient shift. 
		/// Given a coefficient array d[0..n-1], this routine generates a
		/// coefficient array g[0..n-1] such that \sum^{n-1}_{k=0} d_k y^k = \sum^{n-1}_{k=0} g_k x^k, 
		/// where x and y are related by (5.8.10), i.e., the interval 
		/// -1 less than y less than 1 is mapped 
		/// to the interval a less than x less than b. 
		/// The array g is returned in d.
		/// </summary>
		public Polynomial Shift(double a, double b)
		{
			int n = m_c.Length;
			double[] d = (double[])m_c.Clone();
			double cnst = 2.0 / (b - a);
			double fac = cnst;
			for (int j = 1; j < n; j++)
			{
				d[j] *= fac;
				fac *= cnst;
			}
			cnst = 0.5 * (a + b);
			for (int j = 0; j <= n - 2; j++)
				for (int k = n - 2; k >= j; k--)
					d[k] -= cnst * d[k + 1];
			return new Polynomial(d);
		}

		#region Find Polynomial roots
		/// <summary>
		/// Find Polynomial roots
		/// </summary>
		/// <returns>Root collection</returns>
		public Complex[] Solve()
		{
			// if this polynomial have zero low-order coefficients, then it have
			// trivial zero roots; they should be excluded before solving
			
			// find first non-zero coefficients
			int n = 0;
			for (;n < m_c.Length;++n)
			{
				if (m_c[n] != 0.0)
					break;
			}
			if (n == 0)
			{// normal flow
				switch (m_c.Length)
				{
					case 1:
						return new Complex[0];
					case 2:
						return Equation1(m_c[1], m_c[0]);
					case 3:
						return Equation2(m_c[2], m_c[1], m_c[0]);
					case 4:
						return Equation3(m_c[2] / m_c[3], m_c[1] / m_c[3], m_c[0] / m_c[3]);
					default:
						throw new NotImplementedException();
				}
			}
			else
			{// has n low-order zero coeff.
				// strip leading zero coefficients
				double[] c = new double[m_c.Length - n];
				Array.Copy(m_c, n, c, 0, m_c.Length - n);

				// find remaining polynomial roots
				Polynomial p = new Polynomial(c);
				Complex[] r = p.Solve();

				// add n zero roots at the beginning
				Complex[] rts = new Complex[n + r.Length];
				for (int i = 0; i < n; ++i)
					rts[i] = new Complex();
				for (int i = 0; i < r.Length; ++i)
					rts[i + n] = r[i];

				return rts;
			}
		}

		/// <summary>
		/// Find a root of Linear Equation
		/// ax + b
		/// </summary>
		/// <param name="a">coeff. at x</param>
		/// <param name="b">free member</param>
		/// <returns></returns>
		private static Complex[] Equation1(double a, double b)
		{
			Complex[] r = new Complex[1];
			r[0] = new Complex(-b / a);
			return r;
		}

		/// <summary>
		/// Find roots of Quadratic Equation
		/// ax^2 + bx + c
		/// </summary>
		/// <param name="a">coeff. at x^2</param>
		/// <param name="b">coeff. at x</param>
		/// <param name="c">free member</param>
		/// <returns></returns>
		private static Complex[] Equation2(double a, double b, double c)
		{
			Complex[] r = new Complex[2];
			// special cases
			if (c == 0.0)
			{
				r[0] = new Complex();
				r[1] = new Complex(-b / a, 0);
			}
			else if (b == 0.0)
			{
				Complex cc = new Complex(-c / a, 0.0);
				r[0] = Complex.Sqrt(cc);
				r[1] = r[0];
			}
			else
			{
				double sign_b = b > 0.0 ? 1.0 : -1.0;
				double q = (-b - sign_b * Math.Sqrt(b * b - 4.0 * a * c)) / 2.0;
				r[0] = new Complex(q / a, 0.0);
				r[1] = new Complex(c / q, 0.0);
			}
			return r;
		}

		/// <summary>
		/// Find roots of Cubic Equation
		/// </summary>
		/// <remarks>
		/// Coeff. at x^3 is suposed to be 1
		/// 
		/// from http://doors.infor.ru/allsrs/alg/index.html
		/// </remarks>
		/// <param name="a">coeff. at x^2</param>
		/// <param name="b">coeff. at x</param>
		/// <param name="c">free member</param>
		/// <returns></returns>
		private static Complex[] Equation3(double a, double b, double c)
		{
			Complex[] r = new Complex[3];

			double p = -(a * a) / 3.0 + b;
			double q = a / 3.0;
			q = 2.0 * q * q * q - a * b / 3.0 + c;
			double tmp = p / 3.0;
			double Q = tmp * tmp * tmp;
			tmp = q / 2.0;
			Q += tmp * tmp;

			if (Q < 0.0)
			{ // three real roots
				tmp = -p / 3.0;
				tmp = tmp * tmp * tmp;
				double u = Math.Acos(-q / (2.0 * Math.Sqrt(tmp)));
				tmp = 2.0 * Math.Sqrt(-p / 3.0);
				r[0] = new Complex(tmp * Math.Cos(u / 3.0) - a / 3.0, 0);
				r[1] = new Complex(-tmp * Math.Cos((u + Math.PI) / 3.0) - a / 3.0, 0);
				r[2] = new Complex(-tmp * Math.Cos((u - Math.PI) / 3.0) - a / 3.0, 0);
			}
			else if (Q == 0.0)
			{ // three real roots
				tmp = Math.Pow(q / 2.0, 1.0 / 3.0);
				r[0] = new Complex(2.0 * tmp - a / 3.0, 0);
				r[1] = new Complex(-tmp - a / 3.0, 0);
				r[2] = new Complex(r[1].Real, 0);
			}
			else
			{ // one real and two complex roots
				tmp = Math.Sqrt(Q);
				double A = Math.Pow(-q / 2.0 + tmp, 1.0 / 3.0);
				double B = Math.Pow(-q / 2.0 - tmp, 1.0 / 3.0);
				r[0] = new Complex(A + B - a / 3.0, 0);
				tmp = -(A + B) / 2.0 - a / 3.0;
				double im = (A - B) * Math.Sqrt(3.0) / 2.0;
				r[1] = new Complex(tmp, im);
				r[2] = new Complex(tmp, -im);
			}
			return r;

			///// CODE below is from Numerical recipes in C, but it not works
			//Complex[] r = new Complex[3];
			//double q = (a * a - 3.0 * b) / 9.0;
			//double q3 = q * q * q;
			//double r = (2.0 * a * a * a - 9.0 * a * b + 27.0 * c) / 54.0;
			//double r2 = r * r;
			//if (r2 < q3)
			//{
			//    double th = Math.Acos(r / Math.Sqrt(q3));
			//    double qq = -2.0 * Math.Sqrt(q);
			//    res.r1 = new Complex(qq * Math.Cos(th / 3.0) - a / 3.0);
			//    res.r2 = new Complex(qq * Math.Cos((th + 2.0 * Math.PI) / 3.0) - a / 3.0);
			//    res.r3 = new Complex(qq * Math.Cos((th - 2.0 * Math.PI) / 3.0) - a / 3.0);
			//    return res;
			//}
			//double aa = Math.Pow(Math.Abs(r) + Math.Sqrt(r2 - q3), 1.0 / 3.0);
			//if (r > 0.0)
			//    aa = -aa;
			//double bb = aa == 0.0 ? 0.0 : q / a;
			//aa += bb - a / 3.0;
			//res.r1 = new Complex(aa);
			//res.r2 = new Complex(aa);
			//res.r3 = new Complex(aa);
			//return res;
		}
		#endregion Find Polynomial roots

		/// <summary>
		/// String polynomial representation
		/// c0 + c1*x + c2*x^2 + ... + cn*x^n
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < m_c.Length; ++i)
			{
				if (i == 0)
					sb.Append(m_c[i].ToString());
				else if (i == 1)
					sb.Append(string.Format(" {1} {0}*x", Math.Abs(m_c[i])
						, m_c[i] < 0 ? "-" : "+"));
				else
					sb.Append(string.Format(" {2} {0}*x^{1}", Math.Abs(m_c[i]), i
						, m_c[i] < 0 ? "-" : "+"));
			}
			return sb.ToString();
		}
	}
}
