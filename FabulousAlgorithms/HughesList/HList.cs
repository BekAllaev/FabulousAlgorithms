using FabulousAlgorithms.Common;
using FabulousAlgorithms.ImmutableStack.Covariant;
using FabulousAlgorithms.ImmutableStack.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.HughesList
{
    public struct HList<T> : IEnumerable<T>
    {
        private readonly Concat c;
        delegate IImmutableStackCovariant<T> Concat(IImmutableStackCovariant<T> stack);

        private HList(Concat c) => this.c = c;

        private static HList<T> Make(Concat c) => new HList<T>(c);

        public static HList<T> Empty { get; } = Make(stack => stack);

        public bool IsEmpty => ReferenceEquals(c, Empty.c);

        public static HList<T> FromStack(IImmutableStackCovariant<T> fromStack)=>
            fromStack.IsEmpty?
                Empty : 
                Make(stack => fromStack.Concatenate(stack));

        private static HList<T> Concatenate(HList<T> hl1, HList<T> hl2) =>
            hl1.IsEmpty ? hl2 : Make(stack => hl1.c(hl2.c(stack)));

        public static HList<T> Single(T item) => Make(stack => stack.Push(item));

        public HList<T> Push(T item) => Concatenate(Single(item), this);
        public HList<T> Append(T item) => Concatenate(this, Single(item));
        public HList<T> Concatenate(HList<T> hl) => Concatenate(this, hl);

        public IImmutableStackCovariant<T> ToStack() => c(ImmutableStackCovariant<T>.Empty);
        public T Peek() => ToStack().Peek();
        public HList<T> Pop() => FromStack(ToStack().Pop());

        public IEnumerator<T> GetEnumerator() => ToStack().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
