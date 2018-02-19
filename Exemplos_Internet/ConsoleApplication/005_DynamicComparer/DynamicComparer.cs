using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace DynamicComparer
{
    /// <summary>
    /// A comparer that enables the user to define a list of properties to
    /// compare by. This comparer will cause value type properties that are
    /// included in the sort order to be boxed when comparing.
    /// </summary>
    /// <typeparam name="T">The type this comparer acts on</typeparam>
    public class DynamicComparer<T> : IComparer<T>
    {
        Dictionary<string, Func<T, IComparable>> properties = new Dictionary<string, Func<T, IComparable>>();
        List<Func<T, IComparable>> sortOrder = new List<Func<T, IComparable>>();
        List<string> sortOrderNames = new List<string>();
        List<string> uncomparable = new List<string>();

        /// <summary>
        /// Create a DynamicComparer object.
        /// </summary>
        public DynamicComparer()
        {
            var ps = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            foreach (var p in ps)
            {
                // Only consider properties whose type is comparable (implements IComparable).
                if (!typeof(IComparable).IsAssignableFrom(p.PropertyType))
                {
                    // Save the names of uncomparable properties for later reference
                    // in case an attempt is made to sort by them.
                    uncomparable.Add(p.Name);
                    continue;
                }
                ParameterExpression pe = Expression.Parameter(typeof(T), "x");
                MemberExpression me = Expression.MakeMemberAccess(pe, p);
                Expression<Func<T, IComparable>> le;
                if (!p.PropertyType.IsValueType)
                {
                    le = Expression.Lambda<Func<T, IComparable>>(me, pe);
                }
                else
                {
                    UnaryExpression ue = Expression.Convert(me, typeof(IComparable));
                    le = Expression.Lambda<Func<T, IComparable>>(ue, pe);
                }
                var f = le.Compile();
                properties[p.Name] = (Func<T, IComparable>)f;
            }
        }

        string GetNameFromExpression(Expression<Func<T, IComparable>> x)
        {
            LambdaExpression le = (LambdaExpression)x;
            UnaryExpression ue = le.Body as UnaryExpression;
            MemberExpression me;
            if (ue != null)
                me = (MemberExpression)ue.Operand;
            else
                me = (MemberExpression)le.Body;
            return me.Member.Name;
        }

        /// <summary>
        /// Reset the sort order (unsorted).
        /// </summary>
        public void Reset()
        {
            sortOrder.Clear();
            sortOrderNames.Clear();
        }

        /// <summary>
        /// Specify the sort order as a sequence of strings representing property
        /// names. This is unsafe as no compile time checks are made to make sure
        /// the property names actually exist in the type being compared, or that
        /// they are of a comparable type (implementing IComparable).
        /// </summary>
        /// <param name="sortProperties">The property names to sort by</param>
        public void SortOrder(params string[] sortProperties)
        {
            string err = sortProperties.FirstOrDefault(x => uncomparable.Contains(x));
            if (err != null)
                throw new InvalidOperationException("Property '" + err + "' does not implement IComparable");
            err = sortProperties.FirstOrDefault(x => !properties.ContainsKey(x));
            if (err != null)
                throw new InvalidOperationException("Property '" + err + "' does not exist");
            sortOrder.Clear();
            sortOrderNames.Clear();
            sortOrder.AddRange(sortProperties.Select(x => properties[x]));
            sortOrderNames.AddRange(sortProperties);
        }

        /// <summary>
        /// Specify the sort order as a sequence of member access lambda expressions.
        /// This is type safe; a compiler error will ensue if the lambda expressions
        /// don't represent valid properties that implement IComparable. It also
        /// ensures that the properties are accessible to the caller.
        /// </summary>
        /// <param name="sortProperties">The properties to sort by</param>
        public void SortOrder(params Expression<Func<T, IComparable>>[] sortProperties)
        //public static IEnumerable<TSource> Update<TSource>(this IEnumerable<TSource> source, Func<TSource> update)

        {
            sortOrder.Clear();
            sortOrderNames.Clear();
            sortOrder.AddRange(sortProperties.Select(x => x.Compile()));
            sortOrderNames.AddRange(sortProperties.Select(x => GetNameFromExpression(x)));            
        }

        /// <summary>
        /// Returns a string representation of this object. The string representation
        /// includes the type argument, and the list of property names in the sort
        /// order list.
        /// </summary>
        /// <returns>A string representation of this object. The string representation
        /// includes the type argument, and the list of property names in the sort
        /// order list.</returns>
        public override string ToString()
        {
            var desc = string.Join(", ", sortOrderNames.ToArray());
            return "Comparing " + typeof(T).ToString() + " by " + desc;
        }

        #region IComparer<T> Members

        /// <summary>
        /// Compares two instances of the type argument.
        /// </summary>
        /// <param name="x">The first instance</param>
        /// <param name="y">The second instance</param>
        /// <returns>Returns 0 if the objects are equal, -1 if the first is less
        /// than the second, and 1 if the first is greater than the second.</returns>
        public int Compare(T x, T y)
        {
            foreach (var l in sortOrder)
            {
                int c = l(x).CompareTo(l(y));
                if (c != 0) return c;
            }
            return 0;
        }

        #endregion
    }
}
