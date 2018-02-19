// <copyright file="FileParser.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-03-02</date>
// <summary>OpenWPFChart library. Data File Parser.</summary>

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using OpenWPFChart;
using OpenWPFChart.Parts;

namespace CurveChartControlSample
{
	/// <summary>
	/// Data File Parser.
	/// </summary>
	static class FileParser
	{
		/// <summary>
		/// Parses the Data file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="xAxisLabel">The x axis label.</param>
		/// <returns></returns>
		public static ObservableCollection<ItemData> Parse(string fileName, out string xAxisLabel)
		{
			xAxisLabel = null;
			char[] separators = { ' ', '\t' };
			string[] curveNames = null; // File header.
			ArrayList[] columns = null; // File data table columns.
			int n = 0;
			string ln;
			Type xType = null; // Abscissa data type.

			using (StreamReader reader = new StreamReader(fileName))
			{
				while ((ln = reader.ReadLine()) != null)
				{
					n++;
					string[] tokens = ln.Split(separators, StringSplitOptions.RemoveEmptyEntries);
					if (tokens.Length == 0)
						continue;

					if (curveNames == null)
					{
						curveNames = tokens;
						xAxisLabel = tokens[0];

						columns = new ArrayList[curveNames.Length];
						for (int i = 0; i < columns.Length; ++i)
							columns[i] = new ArrayList();
					}
					else
					{
						if (tokens.Length != curveNames.Length)
							throw new Exception(string.Format("Invalid token count at line {0}", n));

						// Get abscissa - either double, DateTime or string.
						object x = parse(tokens[0], ref xType);
						if (x == null)
							throw new Exception(string.Format("Invalid token 0 at line {0}", n));
						else
							columns[0].Add(x);

						// Get ordinate - double only.
						double y;
						for (int i = 1; i < tokens.Length; ++i)
						{
							if (!double.TryParse(tokens[i], out y))
								throw new Exception(string.Format("Invalid token {0} at line {1}", i, n));

							columns[i].Add(y);
						}
					}
				}

				ObservableCollection<ItemData> curveDataList = new ObservableCollection<ItemData>();
				for (int i = 1; i < curveNames.Length; ++i)
				{
					if (xType == typeof(double))
					{
						ObservableCollection<DataPoint<double, double>> points = new ObservableCollection<DataPoint<double, double>>();
						for (int j = 0; j < columns[0].Count; ++j)
						{
							points.Add(new DataPoint<double, double>((double)columns[0][j], (double)columns[i][j]));
						}
						SampledCurveData<double, double> curveData = new SampledCurveData<double, double>()
						{
							ItemName = curveNames[i],
							Points = points
						};
						curveDataList.Add(curveData);
					}
					else if (xType == typeof(DateTime))
					{
						ObservableCollection<DataPoint<DateTime, double>> points
							= new ObservableCollection<DataPoint<DateTime, double>>();
						for (int j = 0; j < columns[0].Count; ++j)
						{
							points.Add(new DataPoint<DateTime, double>((DateTime)columns[0][j], (double)columns[i][j]));
						}
						SampledCurveData<DateTime, double> curveData = new SampledCurveData<DateTime, double>()
						{
							ItemName = curveNames[i],
							Points = points
						};
						curveDataList.Add(curveData);
					}
					else // xType == typeof(string)
					{
						ObservableCollection<DataPoint<object, double>> points
							= new ObservableCollection<DataPoint<object, double>>();
						for (int j = 0; j < columns[0].Count; ++j)
						{
							points.Add(new DataPoint<object, double>(columns[0][j], (double)columns[i][j]));
						}
						SampledCurveData<object, double> curveData = new SampledCurveData<object, double>()
						{
							ItemName = curveNames[i],
							Points = points
						};
						curveDataList.Add(curveData);
					}
				}

				return curveDataList;
			}
		}

		/// <summary>
		/// Parses the token.
		/// Its type could be either double, DateTime or string.
		/// </summary>
		/// <param name="token">The token.</param>
		/// <param name="tokenType">Type of the token.</param>
		/// <returns></returns>
		static object parse(string token, ref Type tokenType)
		{
			if (tokenType != null && tokenType == typeof(string))
				return token;

			double doubleX;
			if (double.TryParse(token, out doubleX))
			{
				if (tokenType == null)
					tokenType = typeof(double);
				else if (tokenType != typeof(double))
					return null;
				return doubleX;
			}
			DateTime dateTimeX;
			if (DateTime.TryParse(token, out dateTimeX))
			{
				if (tokenType == null)
					tokenType = typeof(DateTime);
				else if (tokenType != typeof(DateTime))
					return null;
				return dateTimeX;
			}
			tokenType = typeof(string);
			return token;
		}
	}
}
