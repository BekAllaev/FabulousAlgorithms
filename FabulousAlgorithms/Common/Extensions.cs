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

        public static IImmutableStack<T> ReverseOnto<T>(
            this IImmutableStack<T> stack, IImmutableStack<T> tail)
        {
            var result = tail;
            for (;!stack.IsEmpty;stack = stack.Pop())
                result = result.Push(stack.Peek());
            return result;
        }

        public static IImmutableStack<T> Reverse<T>(this IImmutableStack<T> stack) 
            => stack.ReverseOnto(ImmutableStack<T>.Empty);

        public static IImmutableStack<T> Concatenate<T>(this IImmutableStack<T> xs, IImmutableStack<T> ys)
            => ys.IsEmpty? xs : xs.Reverse().ReverseOnto(ys);

        public static IImmutableStack<T> Append<T>(this IImmutableStack<T> stack, T item)
            => stack.Concatenate(ImmutableStack<T>.Empty.Push(item));
    }
}
