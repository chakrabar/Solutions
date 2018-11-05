using System;
using System.Collections.Generic;
using System.Linq;

namespace Wikipedia.Helpers
{
    public static class MoreLinq
    {
        public static TSource MaxBy<TSource, TProperty>(this IEnumerable<TSource> collection, Func<TSource, TProperty> selector)
            where TSource : class
            where TProperty : IComparable<TProperty>
        {
            if (selector == null)
                throw new ArgumentException("propertySelector");
            if (collection == null || collection.Count() == 0)
                return null;

            TSource maxItem = null;
            foreach (var item in collection)
            {
                if (maxItem == null || selector(item).CompareTo(selector(maxItem)) > 0)
                {
                    maxItem = item;
                }
            }
            return maxItem;
        }
    }
}
