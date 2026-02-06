using FabulousAlgorithms.ImmutableStack.Covariant;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.ImmutableStack.Extensions
{
    public static class ImmutableStackExtensions
    {
        public static IImmutableStackCovariant<T> Push<T>(this IImmutableStackCovariant<T> stack, T item)
            => ImmutableStackCovariant<T>.Push(item, stack);

        public static IImmutableStackCovariant<T> Reverse<T>(this IImmutableStackCovariant<T> stack)
        {
            var result = ImmutableStackCovariant<T>.Empty;
            for (;!stack.IsEmpty;stack = stack.Pop())
            {
                result = result.Push(stack.Peek());
            }
            return result;
        }
    }
}
