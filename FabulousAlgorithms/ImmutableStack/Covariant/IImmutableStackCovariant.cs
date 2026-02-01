using System;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.ImmutableStack.Covariant
{
    /// <summary>
    /// Represents an immutable, covariant, read-only stack of elements of a specified type.
    /// </summary>
    /// <remarks>
    /// Deference from <see cref="ImmutableStack.NonCovariant.IImmutableStack{T}"/> is that this one is covariant
    /// </remarks>
    /// <typeparam name="T">The type of elements contained in the stack.</typeparam>
    public interface IImmutableStackCovariant<out T> : IEnumerable<T>
    {
        T Peek();

        IImmutableStackCovariant<T> Pop();

        bool IsEmpty { get; }
    }
}
