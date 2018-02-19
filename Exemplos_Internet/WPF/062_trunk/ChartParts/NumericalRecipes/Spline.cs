// OpenWPFChart library.
// Ported from "Numerical Recipes in C, 2-nd Edition" to C# 3.0.
// Chapter 3.3 Cubic Spline Interpolation
// file Spline.cs
// <revision>$Id: Spline.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace NumericalRecipes
{
	/// <summary>
	/// Cubic spline class
	/// </summary>
	public class Spline
	{
		/// <summary>
		/// Constructs a natural spline (with zero second derivative on boundaries)
		/// </summary>
		/// <param name="points">Function points.</param>
		public Spline(IEnumerable<Point> points) : this(points, double.NaN, double.NaN)
		{
		}

		/// <summary>
		/// Constructs a spline with first derivatives specified at the x-boundaries.
		/// </summary>
		/// <param name="points">Function points.</param>
		/// <param name="yp1">First derivative at start (0) point. Should be NaN if
		/// natural spline (with zero second derivative condition) required</param>
		/// <param name="ypn">First derivative at end (n - 1) point. Should be NaN if
		/// natural spline (with zero second derivative condition) required</param>
		public Spline(IEnumerable<Point> points, double yp1, double ypn)
		{
			this.points = (from pt in points orderby pt.X select pt).ToArray();
			construct(yp1, ypn);
		}

		/// <summary>
		/// Constructs a spline 
		/// Given array points[0..n-1] containing a tabulated function, 
		/// i.e., .y[i] = f(.x[i]), with x is stricly acsending, 
		/// and given values yp1 and ypn for the first derivative of the interpolating 
		/// function at points 0 and n-1, respectively, this routine creates an array 
		/// m_y2[0..n-1] that contains the second derivatives of the interpolating function 
		/// at the tabulated points x[i]. If yp1 and/or ypn are NaN's, the routine is 
		/// signaled to set the corresponding boundary condition for a natural spline, 
		/// with zero second derivative on that boundary.
		/// </summary>
		/// <param name="yp1">First derivative at start (0) point. If is NaN than 
		///		natural (zero second derivative) condition is used</param>
		/// <param name="ypn">First derivative at end (n - 1) point. If is NaN than 
		///		natural (zero second derivative) condition is used</param>
		private void construct(double yp1, double ypn)
		{
			int n = points.Length;

			m_y2 = new double[n];
			double[] x = new double[n]; // temp storage

			if (double.IsNaN(yp1))
				// The lower boundary condition is set either to be “natural”
				m_y2[0] = x[0] = 0.0;
			else
			{ // or else to have a specified first derivative.
				m_y2[0] = -0.5;
				x[0] = (3.0 / (points[1].X - points[0].X)) * ((points[1].Y - points[0].Y)
					/ (points[1].X - points[0].X) - yp1);
			}
			
			// This is the decomposition loop of the tridiagonal algorithm.
			// m_y2 and x are used for temporary storage of the decomposed factors.
			for (int i = 1; i < n - 1; i++)
			{
				double sig = (points[i].X - points[i - 1].X) / (points[i + 1].X - points[i - 1].X);
				double p = sig * m_y2[i - 1] + 2.0;
				m_y2[i] = (sig - 1.0) / p;
				x[i] = (points[i + 1].Y - points[i].Y) / (points[i + 1].X - points[i].X)
					- (points[i].Y - points[i - 1].Y) / (points[i].X - points[i - 1].X);
				x[i] = (6.0 * x[i] / (points[i + 1].X - points[i - 1].X) - sig * x[i - 1]) / p;
			}

			double qn, un;
			if (double.IsNaN(ypn))
				// The upper boundary condition is set either to be “natural”
				qn = un = 0.0;
			else
			{ // or else to have a specified first derivative.
				qn = 0.5;
				un = (3.0 / (points[n - 1].X - points[n - 2].X)) * (ypn - (points[n - 1].Y - points[n - 2].Y)
					/ (points[n - 1].X - points[n - 2].X));
			}
			m_y2[n - 1] = (un - qn * x[n - 2]) / (qn * m_y2[n - 2] + 1.0);

			// backsubstitution loop of the tridiagonal algorithm
			for (int k = n - 2; k >= 0; k--)
				m_y2[k] = m_y2[k] * m_y2[k + 1] + x[k];
		}

		Point[] points;
		/// <summary>
		/// Gets the input ordered points.
		/// </summary>
		/// <value>The points.</value>
		internal Point[] Points
		{
			get { return points; }
		}

		private double[] m_y2; // second derivatives
		/// <summary>
		/// Gets the second derivative.
		/// </summary>
		/// <value>The second derivative.</value>
		internal double[] SecondDerivative
		{
			get { return m_y2; }
		}

		/// <summary>
		/// Returns a cubic-spline interpolated value
		/// </summary>
		/// <param name="x">The x.</param>
		/// <returns>interpolated value or NaN if out of range.</returns>
		public double GetValue(double x)
		{
			int n = Points.Length;
			if (x < Points[0].X || x > Points[n - 1].X)
				return double.NaN;

			int klo = 0;
			int khi = n - 1;
			while (khi - klo > 1)
			{
				int k = ((khi + klo + 2) >> 1) - 1;
				if (Points[k].X > x)
					khi = k;
				else
					klo = k;
			}

			double h = Points[khi].X - Points[klo].X;
			double a = (Points[khi].X - x) / h;
			double b = (x - Points[klo].X) / h;
			return a * Points[klo].Y + b * Points[khi].Y + ((a * a * a - a) * m_y2[klo]
				+ (b * b * b - b) * m_y2[khi]) * (h * h) / 6.0;
		}
	}
}
