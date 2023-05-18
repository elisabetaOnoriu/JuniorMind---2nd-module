using System.Collections.Generic;

namespace Delegates
{
    public static class Delegates
    {
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            ThrowArgumentNullExceptionIfNecessary(source);
            ThrowArgumentNullExceptionIfNecessary(predicate);
            foreach (TSource item in source)
            {
                if (!predicate(item))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            ThrowArgumentNullExceptionIfNecessary(source);
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            ThrowArgumentNullExceptionIfNecessary(source);
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            throw new InvalidOperationException();
        }

        public static IEnumerable<TResult> Select<TSource, TResult>
            (this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            ThrowArgumentNullExceptionIfNecessary(source);
            ThrowArgumentNullExceptionIfNecessary(selector);
            foreach (var item in source)
            {
                yield return selector(item);
            }
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>
            (this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            ThrowArgumentNullExceptionIfNecessary(source);
            ThrowArgumentNullExceptionIfNecessary(selector);
            foreach (var item in source)
            {
                foreach (var result in selector(item))
                {
                    yield return result;
                }
            }
        }

        public static IEnumerable<TSource> Where<TSource>
            (this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            ThrowArgumentNullExceptionIfNecessary(source);
            ThrowArgumentNullExceptionIfNecessary(predicate);
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector)
        {
            ThrowArgumentNullExceptionIfNecessary(source);
            ThrowArgumentNullExceptionIfNecessary(keySelector);
            ThrowArgumentNullExceptionIfNecessary(elementSelector);
            var dictionary = new Dictionary<TKey, TElement>();
            foreach (var item in source)
            {
                var key = keySelector(item);
                ThrowArgumentNullExceptionIfNecessary(key);
                try
                {
                    dictionary.Add(keySelector(item), elementSelector(item));
                }
                catch(ArgumentException)
                {
                    throw new ArgumentException(nameof(key));
                }               
            }

            return dictionary;
        }

        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(
            this IEnumerable<TFirst> first,
             IEnumerable<TSecond> second,
              Func<TFirst, TSecond, TResult> resultSelector)
        {
            ThrowArgumentNullExceptionIfNecessary(first);
            ThrowArgumentNullExceptionIfNecessary(second);
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
            {
                yield return resultSelector(firstEnumerator.Current, secondEnumerator.Current);
            }
        }

        public static TAccumulate Aggregate<TSource, TAccumulate>(
            this IEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> func)
        {
            ThrowArgumentNullExceptionIfNecessary(source);
            ThrowArgumentNullExceptionIfNecessary(func);
            var result = seed;
            foreach (var item in source)
            {
                result = func(result, item);
            }

            return result;
        }

        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
          IEnumerable<TInner> inner,
          Func<TOuter, TKey> outerKeySelector,
          Func<TInner, TKey> innerKeySelector,
          Func<TOuter, TInner, TResult> resultSelector)
        {
            ThrowArgumentNullExceptionIfNecessary(outer);
            ThrowArgumentNullExceptionIfNecessary(inner);
            ThrowArgumentNullExceptionIfNecessary(outerKeySelector);
            ThrowArgumentNullExceptionIfNecessary(innerKeySelector);
            ThrowArgumentNullExceptionIfNecessary(resultSelector);
            foreach (var item in outer)
            {
                foreach (var innerItem in inner)
                {
                    if (outerKeySelector(item).Equals(innerKeySelector(innerItem)))
                    {
                        yield return resultSelector(item, innerItem);
                    }
                }
            }
        }

        public static IEnumerable<TSource> Distinct<TSource>(
         this IEnumerable<TSource> source,
         IEqualityComparer<TSource> comparer)
        {
            ThrowArgumentNullExceptionIfNecessary(source);
            var distinct = new HashSet<TSource>(source, comparer);
            foreach (var item in distinct)
            {
                yield return item;
            }
        }

        public static IEnumerable<TSource> Union<TSource>(
         this IEnumerable<TSource> first,
         IEnumerable<TSource> second,
            IEqualityComparer<TSource> comparer)
        {
            ThrowArgumentNullExceptionIfNecessary(first);
            ThrowArgumentNullExceptionIfNecessary(second);
            var distinct = new HashSet<TSource>(first, comparer);
            var secondDistinct = new HashSet<TSource>(second, comparer);
            distinct.UnionWith(secondDistinct);
            foreach (var item in distinct)
            {
                yield return item;
            }
        }

        public static IEnumerable<TSource> Intersect<TSource>(
         this IEnumerable<TSource> first,
         IEnumerable<TSource> second,
         IEqualityComparer<TSource> comparer)
        {
            ThrowArgumentNullExceptionIfNecessary(first);
            ThrowArgumentNullExceptionIfNecessary(second);
            HashSet<TSource> distinct = new(first, comparer);
            var secondDistinct = new HashSet<TSource>(second, comparer);
            distinct.IntersectWith(secondDistinct);
            foreach (var item in distinct)
            {
                yield return item;
            }
        }

        public static IEnumerable<TSource> Except<TSource>(
          this IEnumerable<TSource> first,
          IEnumerable<TSource> second,
          IEqualityComparer<TSource> comparer)
        {
            ThrowArgumentNullExceptionIfNecessary(first);
            ThrowArgumentNullExceptionIfNecessary(second);
            var distinct = new HashSet<TSource>(second, comparer);
            foreach (var item in first)
            {
                if (distinct.Add(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(
         this IEnumerable<TSource> source,
          Func<TSource, TKey> keySelector,
           Func<TSource, TElement> elementSelector,
          Func<TKey, IEnumerable<TElement>, TResult> resultSelector,
          IEqualityComparer<TKey> comparer)
        {
            ThrowArgumentNullExceptionIfNecessary(source);
            ThrowArgumentNullExceptionIfNecessary(keySelector);
            ThrowArgumentNullExceptionIfNecessary(elementSelector);
            ThrowArgumentNullExceptionIfNecessary(resultSelector);
            Dictionary<TKey, List<TElement>> groups = new(comparer);
            foreach (var item in source)
            {
                TKey key = keySelector(item);
                TElement element = elementSelector(item);
                try
                {
                    groups[key].Add(element);
                }
                catch
                {
                    groups[key] = new List<TElement> { element };
                }              
            }

            foreach (var group in groups)
            {
                yield return resultSelector(group.Key, group.Value);
            }
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(
         this IEnumerable<TSource> source,
         Func<TSource, TKey> keySelector,
         IComparer<TKey> comparer)
        {
            ThrowArgumentNullExceptionIfNecessary(source);
            ThrowArgumentNullExceptionIfNecessary(keySelector);
            var list = new List<TSource>(source);
            InsertionSort(list, keySelector, comparer, false);
            return new OrderedEnumerable<TSource>(list);
        }

        public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(
          this IOrderedEnumerable<TSource> source,
         Func<TSource, TKey> keySelector,
         IComparer<TKey> comparer)
        {
            return new OrderedEnumerable<TSource>(source).CreateOrderedEnumerable(keySelector, comparer, false);
        }

        internal static void InsertionSort<T1, T2>
            (this List<T1> list, Func<T1, T2> keySelector, IComparer<T2> comparer, bool descending)
        {
            for (int i = 1; i < list.Count; i++)
            {
                for (int j = i; descending? j >= 0 : j > 0  && 
                     comparer.Compare(keySelector(list[j - 1]), keySelector(list[j])) == (descending ? -1 : 1); j--)
                {
                    (list[j - 1], list[j]) = (list[j], list[j - 1]);
                }
            }
        }

        private static void ThrowArgumentNullExceptionIfNecessary<T>(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
        }       
    }
}