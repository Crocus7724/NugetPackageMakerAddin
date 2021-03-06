﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NuGetPackageMakerAddin.Extensions
{
    internal class CompareSelector<T, TKey> : IEqualityComparer<T>
    {
        private readonly Func<T, TKey> selector;

        public CompareSelector(Func<T, TKey> selector)
        {
            this.selector = selector;
        }

        public bool Equals(T x, T y)
        {
            return selector(x).Equals(selector(y));
        }

        public int GetHashCode(T obj)
        {
            return selector(obj).GetHashCode();
        }
    }

    internal static class DistinctExtension
    {
        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            return source.Distinct(new CompareSelector<T, TKey>(selector));
        }
    }
}