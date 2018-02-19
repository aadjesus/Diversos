// <copyright file="CubicPolynomialPolylineApproximation.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2008-12-21</date>
// <summary>OpenWPFChart  library. Approximating Cubic Polynomial with PolyLine.</summary>
// <revision>$Id: CubicPolynomialPolylineApproximation.cs 18093 2009-03-16 04:15:06Z unknown $</revision>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using NumericalRecipes;

namespace OpenWPFChart.Parts
{
	/// <summary>
	/// Approximating Cubic Polynomial with PolyLine.
	/// </summary>
	public static class CubicPolynomialPolylineApproximation
	{
		/// <summary>
		/// Gets the approximation of the polynomial with polyline.
		/// </summary>
		/// <param name="polynomial">The polynomial.</param>
		/// <param name="x1">The abscissas start.</param>
		/// <param name="x2">The abscissas stop.</param>
		/// <param name="tolerance">The tolerance is the maximum distance from the cubic 
		/// polynomial to the approximating polyline.</param>
		/// <returns></returns>
		public static Collection<Point> Approximate(Polynomial polynomial, double x1, double x2, double tolerance)
		{
			Debug.Assert(x1 <= x2, "x1 <= x2");
			Debug.Assert(polynomial.Order == 3, "polynomial.Order == 3");

			Collection<Point> points = new Collection<Point>();

			// Get difference between given polynomial and the straight line passing its node points.
			Polynomial deviation = DeviationPolynomial(polynomial, x1, x2);
			Debug.Assert(deviation.Order == 3, "diff.Order == 3");
			if (deviation[0] == 0 && deviation[1] == 0 && deviation[2] == 0 && deviation[3] <= double.Epsilon)
			{
				points.Add(new Point(x1, polynomial.GetValue(x1)));
				points.Add(new Point(x2, polynomial.GetValue(x2)));
				return points;
			}

			// Get previouse polynomial first derivative
			Polynomial firstDerivative = new Polynomial(new double[] { deviation[1], 2 * deviation[2], 3 * deviation[3] });

			// Difference polinomial extremums.
			// Fing first derivative roots.
			Complex[] complexRoots = firstDerivative.Solve();
			// Get real roots in [x1, x2].
			List<double> roots = new List<double>();
			foreach (Complex complexRoot in complexRoots)
			{
				if (complexRoot.Imaginary == 0)
				{
					double r = complexRoot.Real;
					if (r > x1 && r < x2)
						roots.Add(r);
				}
			}
			//Debug.Assert(roots.Count > 0, "roots.Count > 0");
			if (roots.Count == 0)
			{
				points.Add(new Point(x1, polynomial.GetValue(x1)));
				points.Add(new Point(x2, polynomial.GetValue(x2)));
				return points;
			}
			Debug.Assert(roots.Count <= 2, "roots.Count <= 2");

			// Check difference polynomial extremal values.
			bool approximates = true;
			foreach (double x in roots)
			{
				if (Math.Abs(deviation.GetValue(x)) > tolerance)
				{
					approximates = false;
					break;
				}
			}
			if (approximates)
			{// Approximation is good enough.
				points.Add(new Point(x1, polynomial.GetValue(x1)));
				points.Add(new Point(x2, polynomial.GetValue(x2)));
				return points;
			}

			if (roots.Count == 2)
			{
				if (roots[0] == roots[1])
					roots.RemoveAt(1);
				else if (roots[0] > roots[1])
				{// Sort the roots
					// Swap roots
					double x = roots[0];
					roots[0] = roots[1];
					roots[1] = x;
				}
			}
			// Add the end abscissas.
			roots.Add(x2);

			// First subinterval.
			Collection<Point> pts = Approximate(polynomial, x1, roots[0], tolerance);
			// Copy all points.
			foreach (Point pt in pts)
			{
				points.Add(pt);
			}
			// The remnant of subintervals.
			for (int i = 0; i < roots.Count - 1; ++i)
			{
				pts = Approximate(polynomial, roots[i], roots[i + 1], tolerance);
				// Copy all points but the first one.
				for (int j = 1; j < pts.Count; ++j)
				{
					points.Add(pts[j]);
				}
			}
			return points;
		}

		/// <summary>
		/// Gets the difference between given polynomial and the straight line passing through its node points.
		/// </summary>
		/// <param name="polynomial">The polynomial.</param>
		/// <param name="x1">The abscissas start.</param>
		/// <param name="x2">The abscissas stop.</param>
		/// <returns></returns>
		static Polynomial DeviationPolynomial(Polynomial polynomial, double x1, double x2)
		{
			double y1 = polynomial.GetValue(x1);
			double y2 = polynomial.GetValue(x2);
			double a = (y2 - y1) / (x2 - x1);
			double b = y1 - a * x1;
			if (a != 0)
				return polynomial.Subtract(new Polynomial(new double[] { b, a }));
			else if (b != 0)
				return polynomial.Subtract(new Polynomial(new double[] { b }));
			else
				return polynomial;
		}
	}
}
