using System;
using System.Collections.Generic;
using System.Linq;

namespace MyKata.Test
{
    public static class EnumerableExtension
    {
        public static bool AreAllTheSameTitle<T>(this IEnumerable<T> enumerable, IEqualityComparer<T> comparer)
        {
            return enumerable.Distinct(comparer).Count() == 1;
        }
        public static bool AreAllDifferentTitle<T>(this IEnumerable<T> enumerable, IEqualityComparer<T> comparer)
        {
            return enumerable.Distinct(comparer).Count() == enumerable.Count();
        }
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

    }
}