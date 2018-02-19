using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Galador.Applications
{
	public static class PropertyUtils
	{
		public static MemberInfo[] GetPath<TSource, TProperty>(this Expression<Func<TSource, TProperty>> e)
		{
			return GetLambdaPath(e);
		}
		public static MemberInfo[] GetPath<TProperty>(this Expression<Func<TProperty>> e)
		{
			return GetLambdaPath(e);
		}
		public static MemberInfo[] GetPath<TSource, TProperty>(this Expression<Func<TSource, TProperty>> e, out object root)
		{
			return GetLambdaPath(e, out root);
		}
		public static MemberInfo[] GetPath<TProperty>(this Expression<Func<TProperty>> e, out object root)
		{
			return GetLambdaPath(e, out root);
		}

		public static MemberInfo[] GetLambdaPath(LambdaExpression e)
		{
			object root;
			return GetLambdaPath(e, out root);
		}

		public static MemberInfo[] GetLambdaPath(LambdaExpression e, out object root)
		{
			if (e == null)
				throw new ArgumentNullException();

			root = null;
			var list = new List<MemberInfo>();
			var me = e.Body as MemberExpression;
			while (me != null)
			{
				bool ok = me.Member is PropertyInfo || me.Member is FieldInfo;
				if (!ok)
					throw new NotSupportedException();
				list.Add(me.Member);
				switch (me.NodeType)
				{
					case ExpressionType.Parameter:
						//case ExpressionType.Index://.NET4 it seems...
						throw new NotImplementedException();
					case ExpressionType.MemberAccess:
						if (me.Expression is ConstantExpression)
						{
							var ce = (ConstantExpression)me.Expression;
							root = ce.Value;
						}
						me = me.Expression as MemberExpression;
						break;
					default:
						me = null;
						break;
				}
			}
			list.Reverse();
			return list.ToArray();
		}

		public static string GetName<TProperty>(this Expression<Func<TProperty>> e)
		{
			var l = GetLambdaPath(e);
			if (l.Length == 0)
				throw new ArgumentException();
			return l[l.Length - 1].Name;
		}

		public static string GetName<TSource, TProperty>(this Expression<Func<TSource, TProperty>> e)
		{
			var l = GetLambdaPath(e);
			if (l.Length == 0)
				throw new ArgumentException();
			return l[l.Length - 1].Name;
		}

		public static Type GetValueType(this MemberInfo mi)
		{
			if (mi is PropertyInfo)
			{
				var pi = (PropertyInfo)mi;
				return pi.PropertyType;
			}
			else if (mi is FieldInfo)
			{
				var fi = (FieldInfo)mi;
				return fi.FieldType;
			}
			else if (mi is EventInfo)
			{
				var ei = (EventInfo)mi;
				return ei.EventHandlerType;
			}
			return null;
		}
	}
}
