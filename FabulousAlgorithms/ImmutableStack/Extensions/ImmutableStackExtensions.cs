using FabulousAlgorithms.ImmutableStack.Covariant;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.ImmutableStack.Extensions
{
    public static class ImmutableStackExtensions
    {
        public static string Comma<T>(this IEnumerable<T> items) => string.Join(',', items);
        public static string Bracket<T>(this IEnumerable<T> items) => "[" + items.Comma() + "]";

        public static IImmutableStackCovariant<T> Push<T>(this IImmutableStackCovariant<T> stack, T item)
            => ImmutableStackCovariant<T>.Push(item, stack);
    }
}
