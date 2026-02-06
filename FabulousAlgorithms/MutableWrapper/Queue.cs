using FabulousAlgorithms.ImmutableQueue;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.MutableWrapper
{
    /// <summary>
    /// This is mutable wrapper for <see cref="ImmutableQueue.IImQueue{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Queue<T> : IEnumerable<T>
    {
        private IImQueue<T> queue = ImQueue<T>.Empty;

        public void Enqueue(T item)
        {
            queue = queue.Enqueue(item);
        }

        public T Peek() => queue.Peek();

        public T Dequeue()
        {
            T item = queue.Peek();
            queue = queue.Dequeue();
            return item;
        }

        public bool IsEmpty => queue.IsEmpty;

        public IEnumerator<T> GetEnumerator() => queue.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
