using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Infrastructure
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<Output> Map<T, Output>(this IEnumerable<T> itemsToMap,
            IMapper<T, Output> mapper)
        {
            return new EnumerableActions<T>(itemsToMap).MapAllUsing(mapper);
        }

        public static List<TOutput> Map<T, TOutput>(this IEnumerable<T> itemsToMap, Func<T, TOutput> mapper)
        {
            var result = new List<TOutput>();

            foreach (var item in itemsToMap)
            {
                var mappedItem = mapper.Invoke(item);

                if (mappedItem == null)
                {
                    throw new InvalidOperationException();
                }

                result.Add(mappedItem);
            }

            return result;
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
            {
                return;
            }

            foreach (var item in collection)
            {
                action(item);
            }
        }
    }
}