using System;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace tabtest
{
	
	/// <summary>
	///    <para>
	///       Collection of Points.
	///    </para>
	/// </summary>
	public sealed class PointsCollection : CollectionBase 
	{

		FunkyButton owner;
		bool bulkInvalidate = false ;
		bool clearOnAdd = false;


        internal PointsCollection() { }

		/// <summary>
		/// Here, we hold our owning control and setup any initial
		/// values.
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="initialValue"></param>
		internal PointsCollection(FunkyButton owner, Point[] initialValue) 
		{
			
			AddRange(initialValue);
			this.owner=owner;			
		}

		/// <summary>
		/// We store ClearOnAdd to know when to clear the array on an add.
		/// We do this in InitializeComponent so we don't just append all the values
		/// in there to our default 4 values.
		/// </summary>
		internal bool ClearOnAdd
		{
			get
			{
				return clearOnAdd;
			}
			set 
			{
				clearOnAdd = value;
			}

		}

		/// <summary>
		/// Sets or gets the point at a given index.
		/// </summary>
		public Point this[int index] 
		{
			get 
			{
				return(Point)(List[index]);
			}
			set 
			{
				List[index] = value;
				owner.UpdateRegion();
			}
		}
        
		/// <summary>
		/// Adds a Point and updates the owner.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public int Add(Point value) 
		{
			if (ClearOnAdd) 
			{
				Clear();
				ClearOnAdd = false;
			}

			int index = List.Add(value);
			if (owner != null && !bulkInvalidate)
			owner.UpdateRegion();
			return index;
		}

		/// <summary>
		/// Adds an array of points and updates the owner.
		/// </summary>
		/// <param name="value"></param>
		public void AddRange(Point[] value) 
		{
			try 
			{
				bulkInvalidate = true ; 
                    
				if (value != null && value.Length > 0) 
				{
					foreach (Point at in value) 
					{
						this.Add(at);
					}
				}
			}
			finally 
			{
				bulkInvalidate = false;
				if (owner != null)
				owner.UpdateRegion();
			}
		}

		public bool Contains(Point value) 
		{
			return List.Contains(value);
		}

		public void CopyTo(Point[] array, int index) 
		{
			List.CopyTo(array, index);
		}

		public int IndexOf(Point value) 
		{
			return List.IndexOf(value);
		}

	
		public void Insert(int index, Point value) 
		{

			List.Insert(index, value);
			owner.UpdateRegion();
		}


		public void Remove(Point value) 
		{
			List.Remove(value);
			owner.UpdateRegion();
		}

		/// <summary>
		/// Scales all the Points based on the given ratios
		/// and updates the owner.  
		/// </summary>
		/// <param name="x">The ratio to scale each X coordinate by</param>
		/// <param name="y">The ratio to scale each Y coordinate by</param>
		internal void Scale(double x, double y) 
		{
			for (int i = 0; i < List.Count; i++) {
				Point pt = (Point)List[i];
				pt.X = (int)(x * pt.X);
				pt.Y = (int)(y * pt.Y);

				// we have to push the value back in because
				// this is a value type.
				//
				List[i] = pt;
			}
		}
	}
}
