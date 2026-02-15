using FabulousAlgorithms.ImmutableStack.Covariant;
using FabulousAlgorithms.ImmutableStack.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.UndoRedo
{
    public class UndoRedo<T>
    {
        private IImmutableStackCovariant<T> undo = ImmutableStackCovariant<T>.Empty;
        private IImmutableStackCovariant<T> redo = ImmutableStackCovariant<T>.Empty;

        public T State { get; private set; }

        public UndoRedo(T initialState)
        {
            State = initialState;
        }

        public void Do(T newState)
        {
            undo = undo.Push(State);
            State = newState;
            redo = ImmutableStackCovariant<T>.Empty;
        }

        public bool CanUndo => !undo.IsEmpty;

        public T Undo()
        {
            if (!CanUndo)
                throw new InvalidOperationException("No more states to undo.");
            redo = redo.Push(State);
            State = undo.Peek();
            undo = undo.Pop();
            return State;
        }

        public bool CanRedo => !redo.IsEmpty;
        
        public T Redo()
        {
            if (!CanRedo)
                throw new InvalidOperationException("No more states to redo.");
            undo = undo.Push(State);
            State = redo.Peek();
            redo = redo.Pop();
            return State;
        }
    }
}
