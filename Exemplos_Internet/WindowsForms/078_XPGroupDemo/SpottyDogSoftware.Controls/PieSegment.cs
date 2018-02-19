using System;
using System.ComponentModel;

namespace SpottyDogSoftware.Controls
{
	/// <summary>
	/// A class that encapsulats the data for a Pie Graph segment
	/// </summary>
	[TypeConverterAttribute(typeof(PieSegmentConverter))]
	public class PieSegment
	{
		static Random rand = new Random(DateTime.Now.Second);

		private string _path;
		/// <summary>
		/// The path of the segment;
		/// </summary>
		public string Path
		{
			get { return _path; }
			set { _path = value; }
		}

		private long _size;
		/// <summary>
		/// The Size of the segment
		/// </summary>
		public long Size
		{
			get { return _size; }
			set { _size = value; }
		}

		private System.Drawing.Color _segmentColor;
		/// <summary>
		/// The segment color.
		/// </summary>
		public System.Drawing.Color SegmentColor
		{
			get { return _segmentColor; }
			set { _segmentColor = value; }
		}

		private System.Drawing.Region _region;
		/// <summary>
		/// The region for the PieSegment
		/// </summary>
		public System.Drawing.Region Region
		{
			get { return _region; }
			set { _region = value; }
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public PieSegment()
		{
		}

		/// <summary>
		/// Pie Segment constructor that instantiates the member values
		/// </summary>
		/// <param name="path">Segment Path</param>
		/// <param name="size">Segment Size</param>
		public PieSegment(string path, long size)
		{
			_path = path;
			_size = size;
			// Random color for the segment
			_segmentColor = System.Drawing.Color.FromArgb(rand.Next(255), rand.Next(255), 
				rand.Next(255));

		}

	}
}
