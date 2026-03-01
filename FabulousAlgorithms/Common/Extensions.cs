using FabulousAlgorithms.ImmutableStack.Covariant;
using FabulousAlgorithms.ImmutableStack.Extensions;
using FabulousAlgorithms.ImmutableStack.NonCovariant;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.Common
{
    public static class Extensions
    {
        public static string Comma<T>(this IEnumerable<T> items) => string.Join(',', items);
        public static string Bracket<T>(this IEnumerable<T> items) => "[" + items.Comma() + "]";

        public static IImmutableStackCovariant<T> ReverseOnto<T>(
            this IImmutableStackCovariant<T> stack, IImmutableStackCovariant<T> tail)
        {
            var result = tail;
            for (;!stack.IsEmpty;stack = stack.Pop())
                result = result.Push(stack.Peek());
            return result;
        }

        public static IImmutableStackCovariant<T> Reverse<T>(this IImmutableStackCovariant<T> stack) 
            => stack.ReverseOnto(ImmutableStackCovariant<T>.Empty);

        public static IImmutableStackCovariant<T> Concatenate<T>(this IImmutableStackCovariant<T> xs, IImmutableStackCovariant<T> ys)
            => ys.IsEmpty? xs : xs.Reverse().ReverseOnto(ys);

        public static IImmutableStackCovariant<T> Append<T>(this IImmutableStackCovariant<T> stack, T item)
            => stack.Concatenate(ImmutableStackCovariant<T>.Empty.Push(item));
    }
}
