using System;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.Dequeue
{
    public interface IDeque<T> : IEnumerable<T>
    {
        IDeque<T> PushLeft(T item);

        IDeque<T> PushRight(T item);

        IDeque<T> PopLeft();

        IDeque<T> PopRight();

        T Left();

        T Right();

        IDeque<T> Concatenate(IDeque<T> dequeue);

        bool IsEmpty { get; }
    }
}
