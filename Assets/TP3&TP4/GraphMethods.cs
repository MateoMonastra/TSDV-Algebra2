using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class GraphMethods
{
    /// <summary>
    /// Determines whether all elements of a sequence satisfy a condition.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool All<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        foreach (var element in source)
        {
            if (!predicate.Invoke(element))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Determines whether any element of a sequence satisfies a condition.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool Any<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        foreach (var element in source)
        {
            if (predicate.Invoke(element))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Determines whether a sequence contains a specified element by using the default equality comparer.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static bool Contains<TSource>(IEnumerable<TSource> source, TSource item)
    {
        return Contains(source, item, EqualityComparer<TSource>.Default);
    }

    /// <summary>
    /// Determines whether a sequence contains a specified element by using a specified IEqualityComparer<T>.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="item"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static bool Contains<TSource>(IEnumerable<TSource> source, TSource item, IEqualityComparer<TSource> comparer)
    {
        foreach (var element in source)
        {
            if (comparer.Equals(element, item))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Returns distinct elements from a sequence by using the default equality comparer to compare values.
    /// Time: O(n^2)
    /// Memory: O(n)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Distinct<TSource>(IEnumerable<TSource> source)
    {
        return Distinct(source, EqualityComparer<TSource>.Default);
    }

    /// <summary>
    /// Returns distinct elements from a sequence by using a specified IEqualityComparer<T> to compare values.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Distinct<TSource>(IEnumerable<TSource> source,
        IEqualityComparer<TSource> comparer)
    {
        var result = new List<TSource>();

        foreach (var element in source)
        {
            if (result.Count == 0)
                result.Add(element);

            var isDistinct = true;

            foreach (var t in result)
            {
                if (comparer.Equals(element, t))
                {
                    isDistinct = false;
                    break;
                }
            }

            if (isDistinct)
            {
                result.Add(element);
            }
        }

        return result;
    }

    /// <summary>
    /// Returns the element at a specified index in a sequence.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static TSource ElementAt<TSource>(IEnumerable<TSource> source, int index)
    {
        var currentIndex = 0;

        foreach (var element in source)
        {
            if (currentIndex == index)
            {
                return element;
            }

            currentIndex++;
        }

        return default;
    }

    /// <summary>
    /// Produces the set difference of two sequences by using the default equality comparer to compare values.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source1"></param>
    /// <param name="source2"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Except<TSource>(IEnumerable<TSource> source1, IEnumerable<TSource> source2)
    {
        return Except(source1, source2, EqualityComparer<TSource>.Default);
    }

    /// <summary>
    /// Produces the set difference of two sequences by using the specified IEqualityComparer<T> to compare values.
    /// Time: O(n1 * n2)
    /// Memory: O(n)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source1"></param>
    /// <param name="source2"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Except<TSource>(IEnumerable<TSource> source1, IEnumerable<TSource> source2,
        IEqualityComparer<TSource> comparer)
    {
        var result = new List<TSource>();

        foreach (var element in source1)
        {
            foreach (var element2 in source2)
            {
                if (!comparer.Equals(element2, element) && !result.Contains(element))
                {
                    result.Add(element);
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Returns the first element in a sequence that satisfies a specified condition.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static TSource First<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        foreach (var element in source)
        {
            if (predicate.Invoke(element))
            {
                return element;
            }
        }

        return default(TSource);
    }

    /// <summary>
    /// Returns the last element of a sequence that satisfies a specified condition.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static TSource Last<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        var index = 0;
        var lastFoundIndex = 0;

        foreach (var element in source)
        {
            if (predicate.Invoke(element))
            {
                lastFoundIndex = index;
            }

            index++;
        }
        
        if (lastFoundIndex != -1)
        {
            return ElementAt(source, lastFoundIndex);
        }

        return default(TSource);
    }

    /// <summary>
    /// Produces the set intersection of two sequences by using the default equality comparer to compare values.
    /// Time: O(n1 * n2)
    /// Memory: O(n)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source1"></param>
    /// <param name="source2"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Intersect<TSource>(IEnumerable<TSource> source1, IEnumerable<TSource> source2)
    {
        return Intersect(source1, source2, EqualityComparer<TSource>.Default);
    }

    /// <summary>
    /// Produces the set intersection of two sequences by using the specified IEqualityComparer<T> to compare values.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source1"></param>
    /// <param name="source2"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Intersect<TSource>(IEnumerable<TSource> source1, IEnumerable<TSource> source2,
        IEqualityComparer<TSource> comparer)
    {
        var result = new List<TSource>();

        foreach (var element in source1)
        {
            foreach (var element2 in source2)
            {
                if (comparer.Equals(element2, element) && !result.Contains(element))
                {
                    result.Add(element);
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static int Count<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        var resultCount = 0;

        foreach (var element in source)
        {
            if (predicate.Invoke(element))
            {
                resultCount++;
            }
        }

        return resultCount;
    }

    /// <summary>
    /// Determines whether two sequences are equal by comparing their elements by using a specified IEqualityComparer<T>.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source1"></param>
    /// <param name="source2"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static bool SequenceEqual<TSource>(IEnumerable<TSource> source1, IEnumerable<TSource> source2,
        IEqualityComparer<TSource> comparer)
    {
        foreach (var element in source1)
        {
            foreach (var element2 in source2)
            {
                if (!comparer.Equals(element2, element))
                {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// Returns the only element of a sequence that satisfies a specified condition, and throws an exception if more than one such element exists.
    /// Time: O(n)
    /// Memory: O(1)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static TSource Single<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        var result = default(TSource);

        foreach (var element in source)
        {
            if (predicate.Invoke(element))
            {
                if (result == null)
                {
                    result = element;
                }
                else
                {
                    throw new Exception("There are more than one matching");
                }
            }
        }

        if (result != null)
        {
            return result;
        }

        return default(TSource);
    }

    /// <summary>
    /// Bypasses elements in a sequence as long as a specified condition is true and then returns the remaining elements.
    /// Time: O(n)
    /// Memory: O(n)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> SkipWhile<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        var result = new List<TSource>();

        bool save = false;

        foreach (var element in source)
        {
            if (!predicate.Invoke(element))
            {
                save = true;
            }

            if (save)
            {
                result.Add(element);
            }
        }

        return result;
    }

    /// <summary>
    /// Produces the set union of two sequences by using the default equality comparer.
    /// Time: O(n^2)
    /// Memory: O(n)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source1"></param>
    /// <param name="source2"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Union<TSource>(IEnumerable<TSource> source1, IEnumerable<TSource> source2)
    {
        return Union(source1, source2, EqualityComparer<TSource>.Default);
    }

    /// <summary>
    /// Produces the set union of two sequences by using a specified IEqualityComparer<T>.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source1"></param>
    /// <param name="source2"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Union<TSource>(IEnumerable<TSource> source1, IEnumerable<TSource> source2,
        IEqualityComparer<TSource> comparer)
    {
        var result = new List<TSource>();

        foreach (var element1 in source1)
        {
            result.Add(element1);
        }

        foreach (var element2 in source2)
        {
            foreach (var element3 in result.ToList())
            {
                if (!comparer.Equals(element3, element2) && !result.Contains(element2))
                {
                    result.Add(element2);
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Filters a sequence of values based on a predicate. Each element's index is used in the logic of the predicate function.
    /// Time: O(n)
    /// Memory: O(n)
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Where<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        var result = new List<TSource>();

        bool save = true;

        foreach (var element in source)
        {
            if (!predicate.Invoke(element))
            {
                save = false;
            }

            if (save)
            {
                result.Add(element);
            }
        }

        return result;
    }

    public static List<TSource> ToList<TSource>(IEnumerable<TSource> source)
    {
        List<TSource> list = new List<TSource>();
        foreach (TSource data in source)
        {
            list.Add(data);
        }

        return list;
    }
}