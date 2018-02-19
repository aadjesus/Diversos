/*
The MIT License

Copyright (c) 2007 Kevin Moore (http://j832.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Diagnostics;

namespace J832.Common
{
    /// <summary>
    ///     Contains general purpose extention methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        ///     Returns true if all items in the list are unique using
        ///     <see cref="EqualityComparer{T}.Default">EqualityComparer&lt;T&gt;.Default</see>.
        /// </summary>
        /// <exception cref="ArgumentNullException">if <param name="source"/> is null.</exception>
        public static bool AllUnique<T>(this IList<T> source) where T : IEquatable<T>
        {
            Util.RequireNotNull(source, "source");

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            return source.TrueForAllPairs((a, b) => !comparer.Equals(a, b));
        }

        /// <summary>
        ///     Returns true if <paramref name="compare"/> returns
        ///     true for every pair of items in <paramref name="source"/>.
        /// </summary>
        public static bool TrueForAllPairs<T>(this IList<T> source, Func<T, T, bool> compare)
        {
            Util.RequireNotNull(source, "source");
            Util.RequireNotNull(compare, "compare");

            for (int i = 0; i < source.Count; i++)
            {
                for (int j = i + 1; j < source.Count; j++)
                {
                    if (!compare(source[i], source[j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        ///     Returns true if <paramref name="compare"/> returns true of every
        ///     adjacent pair of items in the <paramref name="source"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        ///     If there are n items in the collection, n-1 comparisons are done. 
        /// </para>
        /// <para>
        ///     Every valid [i] and [i+1] pair are passed into <paramref name="compare"/>.
        /// </para>
        /// <para>
        ///     If <paramref name="source"/> has 0 or 1 items, true is returned. 
        /// </para>
        /// </remarks>
        public static bool TrueForAllAdjacentPairs<T>(this IList<T> source, Func<T, T, bool> compare)
        {
            Util.RequireNotNull(source, "source");
            Util.RequireNotNull(compare, "compare");

            for (int i = 0; i < (source.Count - 1); i++)
            {
                if (!compare(source[i], source[i + 1]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Returns true if all of the items in <paramref name="source"/> are not 
        ///     null or empty.
        /// </summary>
        /// <exception cref="ArgumentNullException">if <param name="source"/> is null.</exception>
        public static bool AllNotNullOrEmpty(this IEnumerable<string> source)
        {
            Util.RequireNotNull(source, "source");
            return source.All(item => !string.IsNullOrEmpty(item));
        }

        /// <summary>
        ///     Returns true if all items in <paramref name="source"/> exist
        ///     in <paramref name="set"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">if <param name="source"/> or <param name="set"/> are null.</exception>
        public static bool AllExistIn<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> set)
        {
            Util.RequireNotNull(source, "source");
            Util.RequireNotNull(set, "set");

            return source.All(item => set.Contains(item));
        }

        /// <summary>
        ///     Returns true if <paramref name="source"/> has no items in it; otherwise, false.
        /// </summary>
        /// <remarks>
        /// <para>
        ///     If an <see cref="ICollection{TSource}"/> is provided,
        ///     <see cref="ICollection{TSource}.Count"/> is used.
        /// </para>
        /// <para>
        ///     Yes, this does basically the same thing as the
        ///     <see cref="System.Linq.Enumerable.Any{TSource}(IEnumerable{TSource})"/>
        ///     extention. The differences: 'IsEmpty' is easier to remember and it leverages 
        ///     <see cref="ICollection{TSource}.Count">ICollection.Count</see> if it exists.
        /// </para>    
        /// </remarks>
        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
        {
            Util.RequireNotNull(source, "source");

            if (source is ICollection<TSource>)
            {
                return ((ICollection<TSource>)source).Count == 0;
            }
            else
            {
                using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                {
                    return !enumerator.MoveNext();
                }
            }
        }

        /// <summary>
        ///     Returns the index of the first item in <paramref name="source"/>
        ///     for which <paramref name="predicate"/> returns true. If none, -1.
        /// </summary>
        /// <param name="source">The source enumerable.</param>
        /// <param name="predicate">The function to evaluate on each element.</param>
        public static int IndexOf<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            Util.RequireNotNull(source, "source");
            Util.RequireNotNull(predicate, "predicate");

            int index = 0;
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        /// <summary>
        ///     Returns a new <see cref="ReadOnlyCollection{TSource}"/> using the
        ///     contents of <paramref name="source"/>.
        /// </summary>
        /// <remarks>
        ///     The contents of <paramref name="source"/> are copied to
        ///     an array to ensure the contents of the returned value
        ///     don't mutate.
        /// </remarks>
        public static ReadOnlyCollection<TSource> ToReadOnlyCollection<TSource>(this IEnumerable<TSource> source)
        {
            Util.RequireNotNull(source, "source");
            return new ReadOnlyCollection<TSource>(source.ToArray());
        }

        /// <summary>
        ///     Performs the specified <paramref name="action"/>  
        ///     on each element of the specified <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The sequence to which is applied the specified <paramref name="action"/>.</param>
        /// <param name="action">The action applied to each element in <paramref name="source"/>.</param>
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            Util.RequireNotNull(source, "source");
            Util.RequireNotNull(action, "action");

            foreach (TSource item in source)
            {
                action(item);
            }
        }

        /// <summary>
        ///     Removes the last element from <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The list from which to remove the last element.</param>
        /// <returns>The last element.</returns>
        /// <remarks><paramref name="source"/> must have at least one element and allow changes.</remarks>
        public static TSource RemoveLast<TSource>(this IList<TSource> source)
        {
            Util.RequireNotNull(source, "source");
            Util.RequireArgument(source.Count > 0, "source", "source must have at least one element");
            TSource item = source[source.Count - 1];
            source.RemoveAt(source.Count - 1);
            return item;
        }

        /// <summary>
        ///     If <paramref name="source"/> is null, return an empty <see cref="IEnumerable{TSource}"/>;
        ///     otherwise, return <paramref name="source"/>.
        /// </summary>
        public static IEnumerable<TSource> EmptyIfNull<TSource>(this IEnumerable<TSource> source)
        {
            return source ?? Enumerable.Empty<TSource>();
        }

        /// <summary>
        ///     Recursively projects each nested element to an <see cref="IEnumerable{TSource}"/>
        ///     and flattens the resulting sequences into one sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="recursiveSelector">A transform to apply to each element.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> whose elements are the
        ///     result of recursively invoking the recursive transform function
        ///     on each element and nested element of the input sequence.
        /// </returns>
        public static IEnumerable<TSource> SelectRecursive<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TSource>> recursiveSelector)
        {
            Util.RequireNotNull(source, "source");
            Util.RequireNotNull(recursiveSelector, "recursiveSelector");

            Stack<IEnumerator<TSource>> stack = new Stack<IEnumerator<TSource>>();
            stack.Push(source.GetEnumerator());

            try
            {
                while (stack.Count > 0)
                {
                    if (stack.Peek().MoveNext())
                    {
                        TSource current = stack.Peek().Current;

                        yield return current;

                        stack.Push(recursiveSelector(current).GetEnumerator());
                    }
                    else
                    {
                        stack.Pop().Dispose();
                    }
                }
            }
            finally
            {
                while (stack.Count > 0)
                {
                    stack.Pop().Dispose();
                }
            }

        } //*** SelectRecursive

    }

} //*** namespace J832.Common