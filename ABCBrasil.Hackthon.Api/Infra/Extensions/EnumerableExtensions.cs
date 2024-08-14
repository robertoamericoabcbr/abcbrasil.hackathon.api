using System;
using System.Collections.Generic;
using System.Linq;

namespace ABCBrasil.Hackthon.Api.Infra.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool HasItem<T>(this IEnumerable<T> list)
        {
            return list?.Any() ?? false;
        }

        public static bool HasItem<T>(this IEnumerable<T> list, Func<T, bool> filtro)
        {
            return list?.Any(filtro) ?? false;
        }

        public static bool AmountsEqual<T, Z>(this IEnumerable<T> listA, IEnumerable<Z> listB)
        {
            if (!listA.HasItem() && !listB.HasItem())
            {
                return true;
            }

            if (listA.HasItem() && !listB.HasItem() || !listA.HasItem() && listB.HasItem())
            {
                return false;
            }

            return listA.Count() == listB.Count();
        }

        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (T item in values)
            {
                action(item);
            }
        }
    }
}