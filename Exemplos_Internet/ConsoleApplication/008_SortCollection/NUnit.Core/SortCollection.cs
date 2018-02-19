using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NUnit.Core
{
    public static class SortCollection
    {

        #region Fields
        //================================================================================
        private enum MinMax { Min, Max };
        //================================================================================
        #endregion

        #region Methods
        //================================================================================
        public static TSource MaxItem<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector)
        {
            return MinMaxItem<TSource, TValue>(source, selector, Comparer<TValue>.Default, MinMax.Max);
        }
        //================================================================================
        public static TSource MinItem<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector)
        {
            return MinMaxItem<TSource, TValue>(source, selector, Comparer<TValue>.Default, MinMax.Min);
        }
        //================================================================================
        public static C SortAscending<TSource, TValue, C>(this IEnumerable<TSource> source, Func<TSource, TValue> selector)
        where C : IList<TSource>, new()
        {
            return ConvertToCollection<C, TSource>(Sort<TSource, TValue>(source, selector, Comparer<TValue>.Default));
        }
        //================================================================================
        public static C SortDescending<TSource, TValue, C>(this IEnumerable<TSource> source, Func<TSource, TValue> selector)
        where C : IList<TSource>, new()
        {
            return ConvertToCollection<C, TSource>(Sort<TSource, TValue>(source, selector, Comparer<TValue>.Default).Reverse());
        }
        //================================================================================
        public static C ConvertToCollection<C, T>(this IEnumerable linqCollection)
        where C : IList<T>, new()
        {
            C collection = new C();
            //
            foreach (var item in linqCollection)
            {
                collection.Add((T)item);
            }
            return collection;
        }
        //================================================================================
        #endregion

        #region Helpers
        //================================================================================
        private static IEnumerable<TSource> Sort<TSource, TValue>(
            this IEnumerable<TSource> source,
            Func<TSource, TValue> selector,
            IComparer<TValue> comparer)
        {
            try
            {
                IEnumerable<TSource> newSource = source.OrderBy<TSource, TValue>(selector, comparer);
                return newSource;
            }
            catch (System.Exception exception) { throw new FormatException(exception.ToString()); }
        }
        //================================================================================
        private static TSource MinMaxItem<TSource, TValue>(
            this IEnumerable<TSource> source,
            Func<TSource, TValue> selector,
            IComparer<TValue> comparer,
            MinMax element)
        {
            try
            {
                TSource minMaxItem = default(TSource);
                TValue minMaxValue = default(TValue);

                using (var enumerator = source.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        minMaxItem = enumerator.Current;
                        minMaxValue = selector(minMaxItem);
                        while (enumerator.MoveNext())
                        {
                            TValue value = selector(enumerator.Current);
                            if (element == MinMax.Max ? comparer.Compare(value, minMaxValue) > 0 : comparer.Compare(value, minMaxValue) < 0)
                            {
                                minMaxItem = enumerator.Current;
                                minMaxValue = value;
                            }
                        }
                    }
                }
                return minMaxItem;
            }
            catch (System.Exception exception) { throw new FormatException(exception.ToString()); }
        }
        //================================================================================
        #endregion
    }
}
