using System;
using System.Collections.Generic;
using System.Linq;

namespace Daimler.Lib.Helpers
{
    public static class JoinHelper
    {
        private const string RIGHT_SOURCE_NOT_PARALLEL_STR =
            "The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than "
            + "System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method "
            + "to convert the right data source to System.Linq.ParallelQuery<T>.";

        private const string LEFT_SOURCE_NOT_PARALLEL_STR =
            "The first data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than "
            + "System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method "
            + "to convert the first data source to System.Linq.ParallelQuery<T>.";

        public static IEnumerable<TOuter> Join<TOuter, TInner, TKey>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector)
        {
            IEnumerable<TOuter> results = outer.Join(inner, outerKeySelector, innerKeySelector, (x, y) => x);
            return results;
        }

        public static IEnumerable<TOuter> NotJoin<TOuter, TInner, TKey>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector)
        {
            IEnumerable<TOuter> results = outer.GroupJoin(inner, outerKeySelector, innerKeySelector, (x, y) => new { Outer = x, Inner = y.Any() }).Where(x => !x.Inner).Select(x => x.Outer);
            return results;
        }


        public static IEnumerable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
        {
            IEnumerable<TResult> results = outer.GroupJoin(inner, outerKeySelector, innerKeySelector, (x, y) => new { Outer = x, Inner = y.SingleOrDefault() }).Select(x => resultSelector(x.Outer, x.Inner));
            return results;
        }

        [Obsolete(LEFT_SOURCE_NOT_PARALLEL_STR)]
        public static ParallelQuery<TOuter> Join<TOuter, TInner, TKey>(this IEnumerable<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector)
        {
            throw new NotSupportedException("Outer_ParallelEnumerable_BinaryOpMustUseAsParallel");
        }

        [Obsolete(RIGHT_SOURCE_NOT_PARALLEL_STR)]
        public static ParallelQuery<TOuter> Join<TOuter, TInner, TKey>(this ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector)
        {
            throw new NotSupportedException("Inner_ParallelEnumerable_BinaryOpMustUseAsParallel");
        }

        public static ParallelQuery<TOuter> Join<TOuter, TInner, TKey>(this ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector)
        {
            ParallelQuery<TOuter> results = outer.Join(inner, outerKeySelector, innerKeySelector, (x, y) => x);
            return results;
        }

        [Obsolete(LEFT_SOURCE_NOT_PARALLEL_STR)]
        public static ParallelQuery<TOuter> NotJoin<TOuter, TInner, TKey>(this IEnumerable<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector)
        {
            throw new NotSupportedException("Outer_ParallelEnumerable_BinaryOpMustUseAsParallel");
        }

        [Obsolete(RIGHT_SOURCE_NOT_PARALLEL_STR)]
        public static ParallelQuery<TOuter> NotJoin<TOuter, TInner, TKey>(this ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector)
        {
            throw new NotSupportedException("Inner_ParallelEnumerable_BinaryOpMustUseAsParallel");
        }

        public static ParallelQuery<TOuter> NotJoin<TOuter, TInner, TKey>(this ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector)
        {
            ParallelQuery<TOuter> results = outer.GroupJoin(inner, outerKeySelector, innerKeySelector, (x, y) => new { Outer = x, Inner = y.Any() }).Where(x => !x.Inner).Select(x => x.Outer);
            return results;
        }

        [Obsolete(LEFT_SOURCE_NOT_PARALLEL_STR)]
        public static ParallelQuery<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
        {
            throw new NotSupportedException("Outer_ParallelEnumerable_BinaryOpMustUseAsParallel");
        }

        [Obsolete(RIGHT_SOURCE_NOT_PARALLEL_STR)]
        public static ParallelQuery<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
        {
            throw new NotSupportedException("Inner_ParallelEnumerable_BinaryOpMustUseAsParallel");
        }

        public static ParallelQuery<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
        {
            ParallelQuery<TResult> results = outer.GroupJoin(inner, outerKeySelector, innerKeySelector, (x, y) => new { Outer = x, Inner = y.SingleOrDefault() }).Select(x => resultSelector(x.Outer, x.Inner));
            return results;
        }

        [Obsolete(LEFT_SOURCE_NOT_PARALLEL_STR)]
        public static void ForEach<TOuter, TInner, TKey>(this IEnumerable<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Action<TOuter, TInner> resultSelector)
        {
            throw new NotSupportedException("Outer_ParallelEnumerable_BinaryOpMustUseAsParallel");

        }

        [Obsolete(RIGHT_SOURCE_NOT_PARALLEL_STR)]
        public static void ForEach<TOuter, TInner, TKey>(this ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Action<TOuter, TInner> resultSelector)
        {
            throw new NotSupportedException("Inner_ParallelEnumerable_BinaryOpMustUseAsParallel");

        }

        public static void ForEach<TOuter, TInner, TKey>(this ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Action<TOuter, TInner> resultSelector)
        {
            outer.GroupJoin(inner, outerKeySelector, innerKeySelector, (x, y) => new { Outer = x, Inner = y.SingleOrDefault() }).ForAll(x => resultSelector(x.Outer, x.Inner));
        }

        public static void ForEach<TOuter, TInner, TKey>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Action<TOuter, TInner> resultSelector)
        {
            var t1 = outer.GroupJoin(inner, outerKeySelector, innerKeySelector, (x, y) => new { Outer = x, Inner = y.SingleOrDefault() });
            foreach (var x in t1) resultSelector(x.Outer, x.Inner);
        }
    }
}