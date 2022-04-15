using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MileageManagerForms.Utilities
{
    public static class Sort
    {
        public static void SortOrder<TSource, TKey>(this Collection<TSource> source, Func<TSource, TKey> keySelector, bool ascending)
        {
            if (ascending)
            {
                List<TSource> sortedList = source.OrderBy(keySelector).ToList();
                source.Clear();
                foreach (var sortedItem in sortedList)
                    source.Add(sortedItem);
            }
            else
            {
                List<TSource> sortedList = source.OrderByDescending(keySelector).ToList();
                source.Clear();
                foreach (var sortedItem in sortedList)
                    source.Add(sortedItem);
            }
        }
    }
}
