using FabulousAlgorithms.ImmutableStack.Covariant;
using FabulousAlgorithms.ImmutableStack.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.ImmutableQueue;

/// <summary>
/// Represents an immutable, first-in, first-out (FIFO) queue of elements of a specified type.
/// </summary>
/// <remarks>
/// ImQueue<T> provides efficient, immutable queue operations. Each modification returns a new queue
/// instance, leaving the original unchanged. This type is thread-safe for concurrent read operations, as instances are
/// immutable.
/// </remarks>
/// <typeparam name="T">The type of elements contained in the queue.</typeparam>
public class ImQueue<T> : IImQueue<T>
{
    public static IImQueue<T> Empty { get; } = 
        new ImQueue<T>(ImmutableStackCovariant<T>.Empty, ImmutableStackCovariant<T>.Empty);

    private readonly IImmutableStackCovariant<T> enques;
    private readonly IImmutableStackCovariant<T> deques;

    /// <summary>
    /// Idea behind two stacks is to use one stack for enqueuing and another for dequeuing. 
    /// So we can do O(1) for operation against both ends
    /// </summary>
    /// <param name="enques"></param>
    /// <param name="deques"></param>
    private ImQueue(IImmutableStackCovariant<T> enques, IImmutableStackCovariant<T> deques)
    {
        this.enques = enques;
        this.deques = deques;
    }

    public bool IsEmpty => deques.IsEmpty;

    public IImQueue<T> Dequeue()
    {
        IImmutableStackCovariant<T> newDeq = deques.Pop();
        if (!newDeq.IsEmpty)
        {
            return new ImQueue<T>(enques, newDeq);
        }

        if (enques.IsEmpty)
        {
            return ImQueue<T>.Empty;
        }

        return new ImQueue<T>(ImmutableStackCovariant<T>.Empty, enques.Reverse());
    }

    public IImQueue<T> Enqueue(T item)
    {
        if (IsEmpty)
        {
            return new ImQueue<T>(enques, deques.Push(item));
        }
        else
        {
            return new ImQueue<T>(enques.Push(item), deques);
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in deques)
            yield return item;
        foreach (var item in enques)
            yield return item;
    }

    public T Peek() => deques.Peek();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
