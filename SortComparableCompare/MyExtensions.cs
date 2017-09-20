using System;
using System.Collections.Generic;

namespace SortComparableCompare
{
    public static class MyExtensions
    {
        static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
                action(item);
        }
    }
}
