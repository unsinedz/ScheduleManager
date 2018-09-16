using System;
using System.Collections.Generic;
using System.Linq;

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
            if (source.IsNullOrEmpty() || second.IsNullOrEmpty())
                return true;

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
    }
}