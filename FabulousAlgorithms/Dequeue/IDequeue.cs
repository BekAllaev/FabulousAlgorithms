using System;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.Dequeue
{
    public interface IDequeue<T> : IEnumerable<T>
    {
        IDequeue<T> PushLeft(T item);

        IDequeue<T> PushRight(T item);

        IDequeue<T> PopLeft();

        IDequeue<T> PopRight();

        T Left();

        T Right();

        bool IsEmpty { get; }
    }
}
