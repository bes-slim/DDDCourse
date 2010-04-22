using System;
using System.Collections.Generic;

namespace Sample.Utilities
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var e in enumerable)
            {
                action(e);
            }

            return enumerable;
        }
    }
}