using FabulousAlgorithms.Common;
using FabulousAlgorithms.ImmutableStack.Covariant;
using FabulousAlgorithms.ImmutableStack.Extensions;
using FabulousAlgorithms.ImmutableStack.NonCovariant;

namespace FabulousAlgorithms.ImmutableStack;

public static class ImmutableStack
{
    public static void DemonstrateImmutableStack()
    {
        var s1 = ImmutableStack<int>.Empty;
        var s2 = s1.Push(10);
        var s3 = s2.Push(20);
        var s4 = s2.Push(30);
        var s5 = s4.Pop();
        var s6 = s5.Pop();
        Console.WriteLine(s1.Bracket());
        Console.WriteLine(s2.Bracket());
        Console.WriteLine(s3.Bracket());
        Console.WriteLine(s4.Bracket());
        Console.WriteLine(s5.Bracket());
        Console.WriteLine(s6.Bracket());
    }

    public static void DemonstrateCovariantImmutableStack()
    {
        IImmutableStackCovariant<Tiger> s1 = ImmutableStackCovariant<Tiger>.Empty;
        IImmutableStackCovariant<Tiger> s2 = s1.Push(new Tiger());
        IImmutableStackCovariant<Tiger> s3 = s2.Push(new Tiger());
        IImmutableStackCovariant<Animal> s4 = s3;
        IImmutableStackCovariant<Animal> s5 = s4.Push(new Giraffe());
        Console.WriteLine(s5.Bracket());
    }

    class Animal
    {
        public override string ToString() => GetType().Name;
    }

    class Tiger: Animal { }
    class Giraffe : Animal { }
}
