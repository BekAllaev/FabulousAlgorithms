using FabulousAlgorithms.Dequeue;
using FabulousAlgorithms.ImmutableStack.Covariant;
using FabulousAlgorithms.ImmutableStack.Extensions;
using FabulousAlgorithms.Life;
using System.Numerics;

namespace FabulousAlgorithms.Common;

using QuadPoint = (long X, long Y);

public static class Extensions
{
    public static string Comma<T>(this IEnumerable<T> items) => string.Join(',', items);
    public static string Bracket<T>(this IEnumerable<T> items) => "[" + items.Comma() + "]";

    public static IImmutableStackCovariant<T> ReverseOnto<T>(
        this IImmutableStackCovariant<T> stack, IImmutableStackCovariant<T> tail)
    {
        var result = tail;
        for (; !stack.IsEmpty; stack = stack.Pop())
            result = result.Push(stack.Peek());
        return result;
    }

    public static IImmutableStackCovariant<T> Reverse<T>(this IImmutableStackCovariant<T> stack)
        => stack.ReverseOnto(ImmutableStackCovariant<T>.Empty);

    public static IImmutableStackCovariant<T> Concatenate<T>(this IImmutableStackCovariant<T> xs, IImmutableStackCovariant<T> ys)
        => ys.IsEmpty ? xs : xs.Reverse().ReverseOnto(ys);

    public static IImmutableStackCovariant<T> Append<T>(this IImmutableStackCovariant<T> stack, T item)
        => stack.Concatenate(ImmutableStackCovariant<T>.Empty.Push(item));

    public static IDeque<T> PushRightMany<T>(this IDeque<T> dequeu, IEnumerable<T> items) =>
        items.Aggregate(dequeu, (d, item) => d.PushRight(item));

    //The width of an n-quad is 2n
    public static long Width(this IQuad quad) => 1L << quad.Level;

    public static bool Contains(this IQuad quad, QuadPoint p)
    {
        if (quad.Level == 0)
            return p == (0, 0);
        long w = quad.Width() / 2;
        return -w <= p.X && p.X < w && -w <= p.Y && p.Y < w;
    }

    public static IQuad Get(this IQuad quad, QuadPoint p)
    {
        if (!quad.Contains(p))
            throw new InvalidOperationException();

        if (quad.Level == 0) // The moment when we find the Leaf(the cell)
            return quad;

        if (quad.IsEmpty) //A nice little optimization here to avoid unnecessary recursion
            return Quad.Dead;

        long w = quad.Width() / 4; // /4 because we want half-width of sub-quad

        QuadPoint newp = ( //This is the tricky bit!
            quad.Level == 1 ? 0 : 0 <= p.X ? -w : p.X + w,
            quad.Level == 1 ? 0 : 0 <= p.Y ? p.Y - w : p.Y + w);

        if (0 <= p.X)
            if (0 <= p.Y)
                return quad.NE.Get(newp);
            else
                return quad.SE.Get(newp);
        else if (0 <= p.Y)
            return quad.NW.Get(newp);
        else
            return quad.SW.Get(newp);
    }

    public static IQuad Set(this IQuad quad, QuadPoint p, IQuad q)
    {
        if (!quad.Contains(p))
            throw new InvalidOperationException();

        if (quad.Level == 0) // “Changing” a 0-quad is simply returning the new value
            return q;

        if (q == Quad.Dead && quad.IsEmpty) // Another nice optimization to skip a recursion
            return quad;

        long w = quad.Width() / 4;

        QuadPoint newp = (
            quad.Level == 1 ? 0 : 0 <= p.X ? p.X - w : p.X + w, // As before, the tricky bit is working out the target point in the next quad down
            quad.Level == 1 ? 0 : 0 <= p.Y ? p.Y - w : p.Y + w);

        var nw = quad.NW;
        var ne = quad.NE;
        var se = quad.SE;
        var sw = quad.SW;

        if (0 <= p.X)
            if (0 <= p.Y)
                ne = quad.NE.Set(newp, q);
            else
                se = quad.SE.Set(newp, q);
        else if (0 <= p.Y)
            nw = quad.NW.Set(newp, q);
        else
            sw = quad.SW.Set(newp, q);

        //Make is memoized, so if the result is unchanged then the result will be identical to the input
        return Quad.Make(nw, ne, se, sw);
    }

    public static IQuad Embiggen(this IQuad quad)
    {
        if (quad.Level == 0)
            throw new InvalidOperationException();

        if (quad.Level >= Quad.MaxLevel)
            return quad;

        var e = Quad.Empty(quad.Level - 1);
        return Quad.Make(
            Quad.Make(e, e, quad.NW, e),
            Quad.Make(e, e, e, quad.NE),
            Quad.Make(quad.SE, e, e, e),
            Quad.Make(e, quad.SW, e, e));
    }

    public static bool HasAllEmptyEdges(this IQuad quad) =>
        quad.NW.NW.IsEmpty &&
        quad.NW.NE.IsEmpty &&
        quad.NE.NW.IsEmpty &&
        quad.NE.NE.IsEmpty &&
        quad.NE.SE.IsEmpty &&
        quad.SE.NE.IsEmpty &&
        quad.SE.SE.IsEmpty &&
        quad.SE.SW.IsEmpty &&
        quad.SW.SE.IsEmpty &&
        quad.SW.SW.IsEmpty &&
        quad.SW.NW.IsEmpty &&
        quad.NW.SW.IsEmpty;

    public static IQuad Center(this IQuad quad) =>
        Quad.Make(quad.NW.SE, quad.NE.SW, quad.SE.NW, quad.SW.NE);

    public static IQuad North(this IQuad quad) =>
        Quad.Make(quad.NW.NE, quad.NE.NW, quad.NE.SW, quad.NW.SE);

    public static IQuad East(this IQuad quad) =>
        Quad.Make(quad.NE.SW, quad.NE.SE, quad.SE.NE, quad.SE.NW);

    public static IQuad South(this IQuad quad) =>
        Quad.Make(quad.SW.NE, quad.SE.NW, quad.SE.SW, quad.SW.SE);

    public static IQuad West(this IQuad quad) =>
        Quad.Make(quad.NW.SW, quad.NW.SE, quad.SW.NE, quad.SW.NW);

    public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
    {
        IEnumerable<IDeque<T>> result = [Deque<T>.Empty];

        foreach (var sequence in sequences)
            result = from deque in result
                     from item in sequence
                     select deque.PushRight(item);

        return result;
    }

    /// <summary>
    /// This method do the same job as <see cref="CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/>
    /// but the only differences is that it do without the loop that mentioned method has. It does using
    /// Aggregate method from basic library
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sequences"></param>
    /// <returns></returns>
    public static IEnumerable<IEnumerable<T>> CartesianProductWithAppend<T>
        (this IEnumerable<IEnumerable<T>> sequences) =>
        sequences.Aggregate(
            (IEnumerable<IDeque<T>>)[Deque<T>.Empty],
            (accumulator, sequence) =>
                from deque in accumulator
                from item in sequence
                select deque.PushRight(item));

    public static string Concat<T>(this IEnumerable<T> items) =>
        string.Join("", items);

    public static bool Greater<T>(this IList<T> items, int x, int y) where T : IComparable<T>
        => items[x].CompareTo(items[y]) > 0;

    public static void Swap<T>(this IList<T> items, int x, int y) => (items[x], items[y]) = (items[y], items[x]);

    public static void ReverseRange<T>(this IList<T> items, int start, int end)
    {
        for (; start < end; start += 1, end -= 1)
            items.Swap(start, end);
    }

    public static bool NextLexiPremutation<T>(this IList<T> items) where T: IComparable<T>
    {
        int first = items.Count - 2;

        while (first >= 0 && !items.Greater(first + 1, first))
            first -= 1;

        if (first < 0)
            return false;

        int second = items.Count - 1;

        while (!items.Greater(second, first))
            second -= 1;

        items.Swap(first, second);
        items.ReverseRange(first + 1, items.Count - 1);

        return true;
    }

    public static BigInteger Factorial(this int n)
    {
        BigInteger result = 1;

        for (int i = 2; i <= n; i += 1)
            result *= i;

        return result;
    }
}
