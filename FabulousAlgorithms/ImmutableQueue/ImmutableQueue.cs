using FabulousAlgorithms.Common;

namespace FabulousAlgorithms.ImmutableQueue;

/// <summary>
/// This class for utilities that print or demonstrate ImmutableQueue functionalities.
/// </summary>

public static class ImmutableQueue
{
    /// <summary>
    /// Demonstrates the usage of ImmutableQueue by performing a series of enqueue and dequeue operations,
    /// and printing the state of the queue after each operation.
    /// </summary>
    public static void DemonstrateImmutableQueue()
    {
        IImQueue<int> q1 = ImQueue<int>.Empty;
        IImQueue<int> q2 = q1.Enqueue(10);
        IImQueue<int> q3 = q2.Enqueue(20);
        IImQueue<int> q4 = q3.Enqueue(30);
        IImQueue<int> q5 = q4.Dequeue();
        IImQueue<int> q6 = q5.Dequeue();
        IImQueue<int> q7 = q6.Dequeue();
        Console.WriteLine(q1.Bracket());
        Console.WriteLine(q2.Bracket());
        Console.WriteLine(q3.Bracket());
        Console.WriteLine(q4.Bracket());
        Console.WriteLine(q5.Bracket());
        Console.WriteLine(q6.Bracket());
        Console.WriteLine(q7.Bracket());
    }
}
