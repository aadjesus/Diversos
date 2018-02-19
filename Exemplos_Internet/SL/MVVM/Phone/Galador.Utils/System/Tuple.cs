
namespace System
{
	public static class Tuple
	{
		public static Tuple<T1> Create<T1>(T1 item1) { return new Tuple<T1>(item1); }
		public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2) { return new Tuple<T1, T2>(item1, item2); }
		public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3) { return new Tuple<T1, T2, T3>(item1, item2, item3); }

		internal static int CombineHashCodes(this int h1, int h2)
		{
			return (((h1 << 5) + h1) ^ h2);
		}
	}

	#region class Tuple<T1>

	public class Tuple<T1>
	{
		public Tuple(T1 item1)
		{
			this.Item1 = item1;
		}
		public T1 Item1 { get; private set; }

		public override bool Equals(object obj)
		{
			var t = obj as Tuple<T1>;
			if (t == null)
				return false;
			return Equals(Item1, t.Item1);
		}
		public override int GetHashCode()
		{
			return ((object)Item1 ?? 0).GetHashCode();
		}
		public override string ToString()
		{
			return "(" + Item1 + ")";
		}
	}

	#endregion

	#region class Tuple<T1, T2>

	public class Tuple<T1, T2>
	{
		public Tuple(T1 item1, T2 item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
		}

		public T1 Item1 { get; private set; }
		public T2 Item2 { get; private set; }

		public override bool Equals(object obj)
		{
			var t = obj as Tuple<T1, T2>;
			if (t == null)
				return false;
			return Equals(Item1, t.Item1)
				&& Equals(Item2, t.Item2);
		}
		public override int GetHashCode()
		{
			return ((object)Item1 ?? 0).GetHashCode().CombineHashCodes(((object)Item2 ?? 0).GetHashCode());
		}
		public override string ToString()
		{
			return "(" + Item1 + ", " + Item2 + ")";
		}
	}

	#endregion

	#region class Tuple<T1, T2, T3>

	public class Tuple<T1, T2, T3>
	{
		public Tuple(T1 item1, T2 item2, T3 item3)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
		}

		public T1 Item1 { get; private set; }
		public T2 Item2 { get; private set; }
		public T3 Item3 { get; private set; }

		public override bool Equals(object obj)
		{
			var t = obj as Tuple<T1, T2, T3>;
			if (t == null)
				return false;
			return Equals(Item1, t.Item1)
				&& Equals(Item2, t.Item2)
				&& Equals(Item3, t.Item3);
		}
		public override int GetHashCode()
		{
			return ((object)Item1 ?? 0).GetHashCode().CombineHashCodes(((object)Item2 ?? 0).GetHashCode()).CombineHashCodes(((object)Item3 ?? 0).GetHashCode());
		}
		public override string ToString()
		{
			return "(" + Item1 + ", " + Item2 + ", " + Item3 + ")";
		}
	}

	#endregion
}
