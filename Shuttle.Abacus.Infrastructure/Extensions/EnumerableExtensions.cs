using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Infrastructure
{
    public static class EnumerableExtensions
    {
        public static void VisitAllItemsUsing<T>(this IEnumerable<T> items, IVisitor<T> visitor)
        {
            new EnumerableActions<T>(items).VisitAllItemsUsing(visitor);
        }

        public static Result GetResultOfVisitingAllItemsWith<T, Result>(this IEnumerable<T> items,
                                                                        IValueReturningVisitor<Result, T> visitor)
        {
            return new EnumerableActions<T>(items).GetResultOfVisitingAllItemsWith(visitor);
        }

        public static IEnumerable<T> AllSatisfying<T>(this IEnumerable<T> items, Specification<T> specification)
        {
            return new EnumerableActions<T>(items).AllMatching(specification);
        }

        public static IEnumerable<Output> MapAllUsing<T, Output>(this IEnumerable<T> itemsToMap,
                                                                 IMapper<T, Output> mapper)
        {
            return new EnumerableActions<T>(itemsToMap).MapAllUsing(mapper);
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T item in collection)
                action(item);
        }

        public static void ForEach<T>(this IEnumerator<T> collection, Action<T> action)
        {
            while (collection.MoveNext())
                action(collection.Current);
        }

        public static IEnumerable<TOutput> ConvertTo<TInput, TOutput>(this IEnumerable<TInput> collection) where TInput : TOutput 
        {
            var result = new List<TOutput>();

            collection.ForEach(item => result.Add(item));

            return result;
        }
    }
}
