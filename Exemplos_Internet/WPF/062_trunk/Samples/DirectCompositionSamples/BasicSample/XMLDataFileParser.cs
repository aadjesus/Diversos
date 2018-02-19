// <copyright file="XMLDataFileParser.cs" company="Oleg V. Polikarpotchkin">
// Copyright © 2008-2009 Oleg V. Polikarpotchkin. All Right Reserved
// </copyright>
// <author>Oleg V. Polikarpotchkin</author>
// <email>ov-p@yandex.ru</email>
// <date>2009-03-09</date>
// <summary>OpenWPFChart library. XML Data File Parser.</summary>
// <revision>$Id: XMLDataFileParser.cs 19480 2009-03-23 07:27:31Z unknown $</revision>

using System;
using System.Collections; // For IList
using System.Collections.ObjectModel; // For ObservableCollection
using System.Xml;
using System.Diagnostics;
using OpenWPFChart.Parts;

namespace BasicSample
{
	/// <summary>
	/// XML Data File Parser
	/// </summary>
	static class XMLDataFileParser
	{
		/// <summary>
		/// Parses the <paramref name="fileName"/> file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns><see cref="ItemData"/> collection from the <paramref name="fileName"/> file.</returns>
		/// <remarks>
		/// <para>XML data format doesn't presume any DTD or schema and all data validation is accoplished by the 
		/// application XML data parser. The XML, however, should be well-formed.</para>
		/// <para>Data file contents look like follows:</para>
		/// <code >
		/// &lt;?xml version="1.0" encoding="utf-8"?&gt;
		/// &lt;Items&gt;
		/// 	&lt;SampledCurve Name="y=x2" XAxisLabel="x"&gt;
		/// 		&lt;Point x="0" y="0"/&gt;
		/// 		...
		/// 		&lt;Point x="1" y="1"/&gt;
		/// 	&lt;/SampledCurve&gt;
		/// 	...
		/// 	&lt;ScatteredPoints Name="Point Set 2"&gt;
		/// 		&lt;Point x="0.01" y="0.5"/&gt;
		/// 		...
		/// 		&lt;Point x="0.95" y="0.3"/&gt;
		/// 	&lt;/ScatteredPoints&gt;
		/// 	...
		/// &lt;/Items&gt;
		/// </code>
		/// <para>The root content element is introduced by Items tag. This element can contain any number of series. 
		/// In the sample snippet above two series are presented by their SampledCurve and ScatteredPoints elements.</para>
		/// <para>Name of the series element tag expresses the intention on the series usage. E.g. the SampledCurve would be treated by 
		/// an application as the set of the sampled curve points and the ScatteredPoints - as the set of points of the points cloud.
		/// Every series element may have Name and XAxisLabel attributes. The former corresponds to OpenWPFChart.Parts.ItemData.ItemName
		/// property and the latter can be used by an application to show the abscissas axis label.
		/// Specific series element may imply additional restrictions: e.g. SampledCurve element require its Point elements be
		/// ordered by abscissa (x) values.</para>
		/// <para>A series element must contain one or more Point elements.</para>
		/// <para>Every Point element must have x (for abscissa) and y (for ordinate) attributes.
		/// Point coordinate in the series values must be convertible to the same base type.
		/// Base type is determined by the first Point element attribute (x or y) values in the following manner:</para>
		/// <list type="ordered">
		/// <item>If the value can be converted to double, the base type is double.</item>
		/// <item>If the value can be converted to DateTime, the base type is DateTime.</item>
		/// <item>Otherwise the base type is string.</item>
		/// </list>
		/// </remarks>
		public static ObservableCollection<ItemData> Parse(string fileName)
		{
			ObservableCollection<ItemData> items = new ObservableCollection<ItemData>();
			using (XmlTextReader reader = new XmlTextReader(fileName))
			{
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						ItemData item = null;
						if (reader.Name == "SampledCurve")
							item = ParseSampledCurve(reader);
						else if (reader.Name == "ScatteredPoints")
							item = ParseScatteredPoints(reader);

						if (item != null)
							items.Add(item);
					}
				}
			}
			return items;
		}

		/// <summary>
		/// Parses the SampledCurve XML element.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns></returns>
		static ItemData ParseSampledCurve(XmlTextReader reader)
		{
			Debug.Assert(reader.NodeType == XmlNodeType.Element, "reader.NodeType == XmlNodeType.Element");
			Debug.Assert(reader.Name == "SampledCurve", "reader.Name == \"SampledCurve\"");
			
			// Item name.
			string name = reader["Name"];
			
			// Base types.
			Type xBaseType = BaseType(reader["XBaseType"]), yBaseType = BaseType(reader["YBaseType"]);
			// Point collection.
			IList points = TypedPointCollection(xBaseType, yBaseType);

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					if (reader.Name == "Point")
					{
						string strX = reader["x"];
						if (strX == null)
							throw new Exception("SampledCurve Point element x attribute missed.");
						string strY = reader["y"];
						if (strY == null)
							throw new Exception("SampledCurve Point element y attribute missed.");

						AddTypedPoint(points, strX, strY, xBaseType, yBaseType);
					}
					else
						throw new Exception(string.Format("Invalid SampledCurve element name {0}.", reader.Name));
				}
				else if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.Name == "SampledCurve")
						break;
				}
			}
			if (points.Count == 0)
				return null;

			if (xBaseType == typeof(double))
			{
				if (yBaseType == typeof(double))
				{
					SampledCurveData<double, double> itemData = new SampledCurveData<double, double>()
					{
						ItemName = name,
						Points = (ObservableCollection<DataPoint<double, double>>)points
					};
					return itemData;
				}
				else if (yBaseType == typeof(DateTime))
				{
					SampledCurveData<double, DateTime> itemData = new SampledCurveData<double, DateTime>()
					{
						ItemName = name,
						Points = (ObservableCollection<DataPoint<double, DateTime>>)points
					};
					return itemData;
				}
				else if (yBaseType == typeof(object))
				{
					SampledCurveData<double, object> itemData = new SampledCurveData<double, object>()
					{
						ItemName = name,
						Points = (ObservableCollection<DataPoint<double, object>>)points
					};
					return itemData;
				}
			}
			else if (xBaseType == typeof(DateTime))
			{
				if (yBaseType == typeof(double))
				{
					SampledCurveData<DateTime, double> itemData = new SampledCurveData<DateTime, double>()
					{
						ItemName = name,
						Points = (ObservableCollection<DataPoint<DateTime, double>>)points
					};
					return itemData;
				}
				else if (yBaseType == typeof(DateTime))
				{
					SampledCurveData<DateTime, DateTime> itemData = new SampledCurveData<DateTime, DateTime>()
					{
						ItemName = name,
						Points = (ObservableCollection<DataPoint<DateTime, DateTime>>)points
					};
					return itemData;
				}
				else if (yBaseType == typeof(object))
				{
					SampledCurveData<DateTime, object> itemData = new SampledCurveData<DateTime, object>()
					{
						ItemName = name,
						Points = (ObservableCollection<DataPoint<DateTime, object>>)points
					};
					return itemData;
				}
			}
			else if (xBaseType == typeof(object))
			{
				if (yBaseType == typeof(double))
				{
					SampledCurveData<object, double> itemData = new SampledCurveData<object, double>()
					{
						ItemName = name,
						Points = (ObservableCollection<DataPoint<object, double>>)points
					};
					return itemData;
				}
				else if (yBaseType == typeof(DateTime))
				{
					SampledCurveData<object, DateTime> itemData = new SampledCurveData<object, DateTime>()
					{
						ItemName = name,
						Points = (ObservableCollection<DataPoint<object, DateTime>>)points
					};
					return itemData;
				}
				else if (yBaseType == typeof(object))
				{
					SampledCurveData<object, object> itemData = new SampledCurveData<object, object>()
					{
						ItemName = name,
						Points = (ObservableCollection<DataPoint<object, object>>)points
					};
					return itemData;
				}
			}
			return null;
		}

		/// <summary>
		/// Parses the ScatteredPoints XML element.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns></returns>
		static ItemData ParseScatteredPoints(XmlTextReader reader)
		{
			Debug.Assert(reader.NodeType == XmlNodeType.Element, "reader.NodeType == XmlNodeType.Element");
			Debug.Assert(reader.Name == "ScatteredPoints", "reader.Name == \"ScatteredPoints\"");
			
			// Item name.
			string name = reader["Name"];
			
			// Base types.
			Type xBaseType = BaseType(reader["XBaseType"]), yBaseType = BaseType(reader["YBaseType"]);
			// Point collection.
			IList points = TypedPointCollection(xBaseType, yBaseType);

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					if (reader.Name == "Point")
					{
						string strX = reader["x"];
						if (strX == null)
							throw new Exception("ScatteredPoints Point element x attribute missed.");
						string strY = reader["y"];
						if (strY == null)
							throw new Exception("ScatteredPoints Point element y attribute missed.");

						AddTypedPoint(points, strX, strY, xBaseType, yBaseType);
					}
					else
						throw new Exception(string.Format("Invalid ScatteredPoints element name {0}.", reader.Name));
				}
				else if (reader.NodeType == XmlNodeType.EndElement)
				{
					if (reader.Name == "ScatteredPoints")
						break;
				}
			}
			if (points.Count == 0)
				return null;

			if (xBaseType == typeof(double))
			{
				if (yBaseType == typeof(double))
				{
					ScatteredPointsData<double, double> itemData = new ScatteredPointsData<double, double>()
					{
						ItemName = name,
						Points = (ObservableCollection<DataPoint<double, double>>)points
					};
					return itemData;
				}
				else if (yBaseType == typeof(DateTime))
				{
					ScatteredPointsData<double, DateTime> itemData = new ScatteredPointsData<double, DateTime>()
					{
						ItemName = name,
						Points = (ObservableCollection<DataPoint<double, DateTime>>)points
					};
					return itemData;
				}
				else if (yBaseType == typeof(object))
				{
					ScatteredPointsData<double, object> itemData = new ScatteredPointsData<double, object>()
					{
						ItemName = name,
						Points = (ObservableCollection<DataPoint<double, object>>)points
					};
					return itemData;
				}
			}
			else if (xBaseType == typeof(DateTime))
			{
				if (yBaseType == typeof(double))
				{
					ScatteredPointsData<DateTime, double> itemData = new ScatteredPointsData<DateTime, double>()
					{
						ItemName = name,
						Points = (ObservableCollection<DataPoint<DateTime, double>>)points
					};
					return itemData;
				}
				else if (yBaseType == typeof(DateTime))
				{
					ScatteredPointsData<DateTime, DateTime> itemData = new ScatteredPointsData<DateTime, DateTime>()
					{
						ItemName = name,
						Points = (ObservableCollection<DataPoint<DateTime, DateTime>>)points
					};
					return itemData;
				}
				else if (yBaseType == typeof(object))
				{
					ScatteredPointsData<DateTime, object> itemData = new ScatteredPointsData<DateTime, object>()
					{
						ItemName = name,
						Points = (ObservableCollection<DataPoint<DateTime, object>>)points
					};
					return itemData;
				}
			}
			else if (xBaseType == typeof(object))
			{
				if (yBaseType == typeof(double))
				{
					ScatteredPointsData<object, double> itemData = new ScatteredPointsData<object, double>()
					{
						ItemName = name,
						Points = (ObservableCollection<DataPoint<object, double>>)points
					};
					return itemData;
				}
				else if (yBaseType == typeof(DateTime))
				{
					ScatteredPointsData<object, DateTime> itemData = new ScatteredPointsData<object, DateTime>()
					{
						ItemName = name,
						Points = (ObservableCollection<DataPoint<object, DateTime>>)points
					};
					return itemData;
				}
				else if (yBaseType == typeof(object))
				{
					ScatteredPointsData<object, object> itemData = new ScatteredPointsData<object, object>()
					{
						ItemName = name,
						Points = (ObservableCollection<DataPoint<object, object>>)points
					};
					return itemData;
				}
			}
			return null;
		}

		/// <summary>
		/// Gets the Base Type from the string.
		/// </summary>
		/// <param name="strXBaseType">Type name string.</param>
		/// <returns></returns>
		static Type BaseType(string strBaseType)
		{
			Type baseType = typeof(double);
			switch (strBaseType)
			{
				case "DateTime":
					baseType = typeof(DateTime);
					break;
				case "String":
					baseType = typeof(object);
					break;
			}
			return baseType;
		}

		/// <summary>
		/// Gets the Typed point collection.
		/// </summary>
		/// <param name="xBaseType">Base x Type.</param>
		/// <param name="yBaseType">Base y Type.</param>
		/// <returns></returns>
		static IList TypedPointCollection(Type xBaseType, Type yBaseType)
		{
			// Point collection.
			IList points = null;
			if (xBaseType == typeof(double))
			{
				if (yBaseType == typeof(double))
					points = new ObservableCollection<DataPoint<double, double>>();
				else if (yBaseType == typeof(DateTime))
					points = new ObservableCollection<DataPoint<double, DateTime>>();
				else if (yBaseType == typeof(object))
					points = new ObservableCollection<DataPoint<double, object>>();
			}
			else if (xBaseType == typeof(DateTime))
			{
				if (yBaseType == typeof(double))
					points = new ObservableCollection<DataPoint<DateTime, double>>();
				else if (yBaseType == typeof(DateTime))
					points = new ObservableCollection<DataPoint<DateTime, DateTime>>();
				else if (yBaseType == typeof(object))
					points = new ObservableCollection<DataPoint<DateTime, object>>();
			}
			else if (xBaseType == typeof(object))
			{
				if (yBaseType == typeof(double))
					points = new ObservableCollection<DataPoint<object, double>>();
				else if (yBaseType == typeof(DateTime))
					points = new ObservableCollection<DataPoint<object, DateTime>>();
				else if (yBaseType == typeof(object))
					points = new ObservableCollection<DataPoint<object, object>>();
			}
			return points;
		}

		/// <summary>
		/// Adds the typed point to the typed point collection.
		/// </summary>
		/// <param name="points">The points.</param>
		/// <param name="strX">X string value.</param>
		/// <param name="strY">Y string value.</param>
		/// <param name="xBaseType">Base x Type.</param>
		/// <param name="yBaseType">Base y Type.</param>
		static void AddTypedPoint(IList points, string strX, string strY, Type xBaseType, Type yBaseType)
		{
			if (xBaseType == typeof(double))
			{
				if (yBaseType == typeof(double))
				{
					double x = double.Parse(strX);
					double y = double.Parse(strY);
					points.Add(new DataPoint<double, double>(x, y));
				}
				else if (yBaseType == typeof(DateTime))
				{
					double x = double.Parse(strX);
					DateTime y = DateTime.Parse(strY);
					points.Add(new DataPoint<double, DateTime>(x, y));
				}
				else if (yBaseType == typeof(object))
				{
					double x = double.Parse(strX);
					points.Add(new DataPoint<double, object>(x, strY));
				}
			}
			else if (xBaseType == typeof(DateTime))
			{
				if (yBaseType == typeof(double))
				{
					DateTime x = DateTime.Parse(strX);
					double y = double.Parse(strY);
					points.Add(new DataPoint<DateTime, double>(x, y));
				}
				else if (yBaseType == typeof(DateTime))
				{
					DateTime x = DateTime.Parse(strX);
					DateTime y = DateTime.Parse(strY);
					points.Add(new DataPoint<DateTime, DateTime>(x, y));
				}
				else if (yBaseType == typeof(object))
				{
					DateTime x = DateTime.Parse(strX);
					points.Add(new DataPoint<DateTime, object>(x, strY));
				}
			}
			else if (xBaseType == typeof(object))
			{
				if (yBaseType == typeof(double))
				{
					double y = double.Parse(strY);
					points.Add(new DataPoint<object, double>(strX, y));
				}
				else if (yBaseType == typeof(DateTime))
				{
					DateTime y = DateTime.Parse(strY);
					points.Add(new DataPoint<object, DateTime>(strX, y));
				}
				else if (yBaseType == typeof(object))
				{
					points.Add(new DataPoint<object, object>(strX, strY));
				}
			}
		}
	}
}
