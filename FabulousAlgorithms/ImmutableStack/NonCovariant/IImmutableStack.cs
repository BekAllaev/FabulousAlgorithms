namespace FabulousAlgorithms.ImmutableStack.NonCovariant;

/// <summary>
/// This is a non-covariant immutable stack interface.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IImmutableStack<T> : IEnumerable<T>
{
    IImmutableStack<T> Push(T item);
    IImmutableStack<T> Pop();
    T Peek();
    bool IsEmpty { get; }
}