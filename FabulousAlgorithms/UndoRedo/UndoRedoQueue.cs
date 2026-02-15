using FabulousAlgorithms.ImmutableQueue;
using FabulousAlgorithms.ImmutableStack.Covariant;
using FabulousAlgorithms.ImmutableStack.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.UndoRedo
{
    public class UndoRedoQueue<T> : IEnumerable<T>
    {
        private UndoRedo<IImQueue<T>> q =
            new UndoRedo<IImQueue<T>>(ImQueue<T>.Empty);

        public void Enqueue(T item) => q.Do(q.State.Enqueue(item));

        public T Peek() => q.State.Peek();

        public T Dequeue()
        {
            T item = q.State.Peek();
            q.Do(q.State.Dequeue());
            return item;
        }

        public bool IsEmpty => q.State.IsEmpty;

        public bool CanUndo => q.CanUndo;

        public void Undo() => q.Undo();

        public bool CanRedo => q.CanRedo;

        public void Redo() => q.Redo();

        public IEnumerator<T> GetEnumerator() => q.State.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
