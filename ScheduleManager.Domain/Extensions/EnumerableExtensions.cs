using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ScheduleManager.Domain.Extensions
{
    public static class EnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            foreach (var item in source)
                action(item);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }

        public static bool IsSimilarAs<TModel>(this IEnumerable<TModel> source, IEnumerable<TModel> second,
            Func<TModel, TModel, bool> itemEqulityComparer = null)
        {
            if (source.IsNullOrEmpty() && second.IsNullOrEmpty())
                return true;

            if (source.IsNullOrEmpty() || second.IsNullOrEmpty())
                return false;

            var itemsEqual = itemEqulityComparer ?? new Func<TModel, TModel, bool>((x, y) => Object.ReferenceEquals(x, y));
            using (var leftEnumerator = source.GetEnumerator())
            {
                using (var rightEnumerator = second.GetEnumerator())
                {
                    bool leftMoved = false;
                    bool rightMoved = false;
                    do
                    {
                        if (!itemsEqual(leftEnumerator.Current, rightEnumerator.Current))
                            return false;

                        leftMoved = leftEnumerator.MoveNext();
                        rightMoved = rightEnumerator.MoveNext();
                        if (leftMoved != rightMoved)
                            return false;
                    } while (leftMoved && rightMoved);
                }
            }

            return true;
        }

        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> second)
        {
            if (second == null)
                throw new ArgumentNullException(nameof(second));

            foreach (var item in second)
                source.Add(item);
        }

        public static IQueryable<T> FilterIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);

            return source;
        }

        public static IQueryable<T> TakeIf<T>(this IQueryable<T> source, bool condition, int count)
        {
            if (condition)
                return source.Take(count);

            return source;
        }

        public static IQueryable<T> SkipIf<T>(this IQueryable<T> source, bool condition, int count)
        {
            if (condition)
                return source.Skip(count);

            return source;
        }
    }
}