using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.ImmutableStack.Covariant
{
    public class ImmutableStackCovariant<T> : IImmutableStackCovariant<T>
    {
        private readonly T item;
        private readonly IImmutableStackCovariant<T> tail;

        private ImmutableStackCovariant(T item, IImmutableStackCovariant<T> tail)
        {
            this.item = item;
            this.tail = tail;
        }

        public bool IsEmpty => false;

        public static IImmutableStackCovariant<T> Push(T item, IImmutableStackCovariant<T> tail)
            => new ImmutableStackCovariant<T>(item, tail);

        public IEnumerator<T> GetEnumerator()
        {
            for (IImmutableStackCovariant<T> s = this; !s.IsEmpty; s = s.Pop())
                yield return s.Peek();
        }

        public T Peek() => item;

        public IImmutableStackCovariant<T> Pop() => tail;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static IImmutableStackCovariant<T> Empty { get; } = new EmptyCovariantStack();

        /// <summary>
        /// Represents an immutable, empty stack in the collection.
        /// </summary>
        /// <remarks>
        /// I like the way that they have abstraction of empty stack 
        /// </remarks>
        private class EmptyCovariantStack : IImmutableStackCovariant<T>
        {
            public EmptyCovariantStack() { }

            public T Peek() => throw new InvalidOperationException();

            public IImmutableStackCovariant<T> Pop() => throw new InvalidOperationException();

            public bool IsEmpty => true;

            /// <summary>
            /// Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns>
            /// I wasn't sure why there is two enumerator methods here.
            /// As far as I understand it, the non-generic version is for compatibility with older code. 
            /// </returns>
            public IEnumerator<T> GetEnumerator()
            {
                yield break;
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
