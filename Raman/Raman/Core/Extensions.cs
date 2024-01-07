using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Raman.Core
{
    public static class Extensions
    {

        /// <summary>
        /// Extension method to have simple Get method on dictionary class.
        /// </summary>
        public static U Get<T, U>(this IDictionary<T, U> dic, T key)
        {
            if (key == null) return default(U);
            return dic.TryGetValue(key, out var ret) ? ret : default(U);
        }

        /// <summary>
        /// Gets the item, that has maximum property value in the collection.
        /// In case empty or null list it returns false and set the maxvalue to default value
        /// </summary>
        private static bool MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, out TSource maxValue)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            if (source == null)
            {
                maxValue = default(TSource);
                return false;
            }
            using (var sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                {
                    maxValue = default(TSource);
                    return false;
                }
                var max = sourceIterator.Current;
                var maxKey = selector(max);
                while (sourceIterator.MoveNext())
                {
                    var candidate = sourceIterator.Current;
                    var candidateProjected = selector(candidate);
                    if (Comparer<TKey>.Default.Compare(candidateProjected, maxKey) > 0)
                    {
                        max = candidate;
                        maxKey = candidateProjected;
                    }
                }
                maxValue = max;
                return true;
            }
        }

        /// <summary>
        /// Returns max value from given enumerable.
        /// If the enumerable is empty then it returns default value of the type.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">source</exception>
        public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            var enumerable = source as TSource[] ?? source.ToArray();
            if (enumerable.Any()) return enumerable.Max();
            return default(TSource);
        }

        /// <summary>
        /// Returns max value from given enumerable.
        /// If the enumerable is empty then it returns default value of the type.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">source</exception>
        public static T MaxOrDefault<TSource, T>(this IEnumerable<TSource> source, Func<TSource, T> selector)
        {
            if (source == null) throw new ArgumentNullException("source");

            var enumerable = source as TSource[] ?? source.ToArray();
            if (!enumerable.Any()) return default(T);
            return Enumerable.Max(Enumerable.Select(enumerable, selector));
        }

        /// <summary>
        /// Gets the item, that has maximum property value in the collection.
        /// In case of null or empty list return default value
        /// </summary>
        public static TSource MaxByOrDefault<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (selector == null) throw new ArgumentNullException("selector");
            if (MaxBy(source, selector, out var max))
            {
                return max;
            }
            return default(TSource);
        }

        /// <summary>
        /// Gets the item, that has minimum property value in the collection.
        /// In case of empty or null list it returns default value
        /// </summary>
        public static TSource MinByOrDefault<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            if (source == null) return default(TSource);
            using (var sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                {
                    return default(TSource);
                }
                var min = sourceIterator.Current;
                var minKey = selector(min);
                while (sourceIterator.MoveNext())
                {
                    var candidate = sourceIterator.Current;
                    var candidateProjected = selector(candidate);
                    if (Comparer<TKey>.Default.Compare(candidateProjected, minKey) < 0)
                    {
                        min = candidate;
                        minKey = candidateProjected;
                    }
                }
                return min;
            }
        }

        // https://stackoverflow.com/questions/19890301/distinctby-not-recognized-as-method
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }

        /// <summary>
        /// Returns average of the sequence of values, or null if the source sequence is empty or contains only values that are null.
        /// </summary>
        public static decimal? AverageOrNull<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        {
            // From documentation: returns average of the sequence of values, or null if the source sequence is empty or contains only values that are null.
            var ret = source.Average(selector);
            return ret;
        }

        /// <summary>
        /// Returns sum of the sequence of values, or null if the source sequence is empty or contains only values that are null.
        /// </summary>
        public static int? SumOrNull<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            var list = source.ToList();
            if (list.Count == 0)
            {
                return null;
            }
            var ret = source.Sum(selector);
            return ret;
        }

        /// <summary>
        /// Returns sum of the sequence of values, or null if the source sequence is empty or contains only values that are null.
        /// </summary>
        public static decimal? SumOrNull<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        {
            var array = source as TSource[] ?? source.ToArray();
            if (array.Select(selector).All(x => x == null))
            {
                return null;
            }
            return array.Length != 0 ? array.Select(selector).Sum() : null;
        }
        
        /// <summary>
        /// Returns sum of the sequence of values. It returns 0 if collection is empty. It returns null if there is any null value.
        /// </summary>
        public static decimal? SumOrNullIfAnyItemIsNull<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector, bool isEmptyArrayAsZero = true)
        {
            var array = source as TSource[] ?? source.ToArray();
            if (array.Length == 0)
            {
                return isEmptyArrayAsZero ? 0 : (decimal?) null;
            }
            if (array.Select(selector).Any(x => x == null))
            {
                return null;
            }
            return array.Select(selector).Sum();
        }

        /// <summary>
        /// Returns sum of the sequence of values, or null if the source sequence is empty or contains only values that are null.
        /// </summary>
        public static decimal? SumOrNull(this IEnumerable<decimal?> source)
        {
            var enumerable = source as decimal?[] ?? source.ToArray();
            if (enumerable.All(x => x == null))
            {
                return null;
            }
            return enumerable.Length != 0 ? enumerable.Sum() : null;
        }
        
        public static bool IsGreaterThan(this DateTime? first, DateTime? second)
        {
            var ret = (first ?? DateTime.MinValue) > (second ?? DateTime.MinValue);
            return ret;
        }
        
        public static bool Exists<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Any(predicate);
        }
        
        public static bool IsNullOrWhiteSpace(this string text) => string.IsNullOrWhiteSpace(text);
        
        public static void AddRange<T>(this ConcurrentBag<T> source, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                source.Add(item);
            }
        }

        public static string ToCommaString<TSource>(this IEnumerable<TSource> source, Func<TSource, string> selector = null)
        {
            if (selector == null)
            {
                var ret = string.Join(", ", source);
                return ret;
            }
            else
            {
                var ret = string.Join(", ", source.Select(selector));
                return ret;
            }
        }
    }
}
