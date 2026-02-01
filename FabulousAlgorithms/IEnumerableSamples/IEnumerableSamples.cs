using System.Collections;

namespace FabulousAlgorithms.IEnumerableSamples;

public static class IEnumerableSamples
{
    public static void DemonstrateIEnumerableSample()
    {
        IEnumerable<int> enumerableGeneric = new EnumerableGeneric<int>();

        enumerableGeneric.GetEnumerator();
        ((IEnumerable)enumerableGeneric).GetEnumerator();

        IEnumerable c = enumerableGeneric;
    }
}

public class EnumerableGeneric<T> : IEnumerable<T>
{
    public IEnumerator<T> GetEnumerator()
    {
        Console.WriteLine($"Hello from {nameof(GetEnumerator)}");
        return null;
    }
    // This code will be transformed to this one
    /*
           IEnumerator e = GetEnumerator();   
           try
           {
               while (e.MoveNext()) // ← а вот тут цикл по элементам
               {
                   var x = e.Current;
                   Console.WriteLine(x);
               }
           }
           finally
           {
               (e as IDisposable)?.Dispose();
           }
     */

    // For 30.01.2026:
    // So as far as I understand idea of that is that new interface IEnumerable<T>
    // can be cast to IEnumerable and can be used this method
    // I have no idea why somebody would do that
    // 
    // So idea is like this, a lot of legacy code use IEnumerable.
    // For example
    // This methods accepts only IEnumerable, so when we pass IEnumerable<T> there won't be any errors
    //public void Foo(IEnumerable enumerable)
    //{
    //     .........
    //}

    IEnumerator IEnumerable.GetEnumerator()
    {
        Console.WriteLine($"Hello from {nameof(IEnumerable.GetEnumerator)}");
        return null;
    }
}
