using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.ImmutableStack.NonCovariant;

/// <summary>
/// Represents an immutable, persistent stack of elements of a specified type.
/// </summary>
/// <remarks>
/// Immutability is kept by ensuring that all operations that would modify the stack instead return a new stack
/// </remarks>
/// <typeparam name="T">The type of elements stored in the stack.</typeparam>
public class ImmutableStack<T> : IImmutableStack<T>
{
    private readonly T item;
    private readonly IImmutableStack<T> tail;
    private ImmutableStack(T item, IImmutableStack<T> tail)
    {
        this.item = item;
        this.tail = tail;
    }

    /// <summary>
    /// Gets an empty immutable stack of type <typeparamref name="T"/>.
    /// </summary>
    /// <remarks>
    /// This empty stack is shared across all instances of <see cref="ImmutableStack{T}"/>.
    /// </remarks>
    public static IImmutableStack<T> Empty { get; } = new EmptyStack();

    public IImmutableStack<T> Push(T item) => new ImmutableStack<T>(item, this);

    public T Peek() => item;

    public IImmutableStack<T> Pop() => tail;

    public bool IsEmpty => false;

    public IEnumerator<T> GetEnumerator()
    {
        for (IImmutableStack<T> s = this; !s.IsEmpty; s = s.Pop())
            yield return s.Peek();
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Represents an immutable, empty stack in the collection.
    /// </summary>
    /// <remarks>
    /// I like the way that they have abstraction of empty stack 
    /// </remarks>
    private class EmptyStack : IImmutableStack<T>
    {
        public EmptyStack() { }
        public IImmutableStack<T> Push(T item) => new ImmutableStack<T>(item, this);
        public T Peek() => throw new InvalidOperationException();
        public IImmutableStack<T> Pop() => throw new InvalidOperationException();
        public bool IsEmpty => true;
        public IEnumerator<T> GetEnumerator()
        {
            yield break;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
