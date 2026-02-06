using System;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.ImmutableQueue;

/// <summary>
/// Represents an immutable first-in, first-out (FIFO) queue of elements.
/// </summary>
/// <remarks>
/// All operations on the queue return a new instance, leaving the original queue unchanged. This
/// interface is intended for use in functional or thread-safe scenarios where immutability is required.
/// </remarks>
/// <typeparam name="T">The type of elements contained in the queue.</typeparam>
public interface IImQueue<T> : IEnumerable<T>
{
    /// <summary>
    /// Adds the specified item to the end of the queue and returns a new queue instance that includes the item.
    /// </summary>
    /// <remarks>
    /// This method does not modify the existing queue instance. Instead, it returns a new
    /// instance with the item added, preserving immutability.
    /// </remarks>
    /// <param name="item">The item to add to the queue.</param>
    /// <returns>A new queue instance that contains the specified item appended to the end.</returns>
    IImQueue<T> Enqueue(T item);

    /// <summary>
    /// Returns the item at the top of the collection without removing it.
    /// </summary>
    /// <remarks>
    /// Use this method to examine the next item to be removed without modifying the collection. The
    /// behavior when the collection is empty depends on the implementation; some implementations may throw an exception
    /// if called on an empty collection.
    /// </remarks>
    /// <returns>The item at the top of the collection.</returns>
    T Peek();

    /// <summary>
    /// Removes the item at the front of the queue and returns a new queue with that item removed.
    /// </summary>
    /// <remarks>
    /// This method does not modify the current queue instance. Instead, it returns a new queue
    /// reflecting the removal, following immutable collection semantics.
    /// </remarks>
    /// <returns>A new queue with the front item removed.</returns>
    IImQueue<T> Dequeue();

    /// <summary>
    /// Gets a value indicating whether the collection contains no elements.
    /// </summary>
    bool IsEmpty { get; }
}
