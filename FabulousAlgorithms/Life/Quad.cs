using System;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.Life;

public interface IQuad
{
    IQuad NW { get; }
    IQuad NE { get; }
    IQuad SE { get; }
    IQuad SW { get; }

    /// <summary>
    /// This returns the value of n for a given n-quad.
    /// </summary>
    int Level { get; }

    /// <summary>
    /// It will be convenient to know if a quad is entirely dead cells
    /// </summary>
    bool IsEmpty { get; }
}

sealed class Quad : IQuad
{
    private sealed class Leaf : IQuad
    {
        public IQuad NW => throw new InvalidOperationException();

        public IQuad NE => throw new InvalidOperationException();

        public IQuad SE => throw new InvalidOperationException();

        public IQuad SW => throw new InvalidOperationException();

        public int Level => 0;

        public bool IsEmpty => this == Dead;
    }

    public static IQuad Dead { get; } = new Leaf();

    public static IQuad Alive { get; } = new Leaf();

    // A 60-quad ought to be enough for anyone!
    public const int MaxLevel = 60;

    public IQuad NW { get; }

    public IQuad NE { get; }

    public IQuad SE { get; }

    public IQuad SW { get; }

    public int Level { get; }

    // Make the constructor private so that callers must use a memoized factory method
    private Quad(IQuad nw, IQuad ne, IQuad se, IQuad sw)
    {
        NW = nw;
        NE = ne;
        SE = se;
        SW = sw;
        Level = nw.Level + 1;
    }

    public bool IsEmpty => this == Empty(Level);

    // One argument is sufficient for anything if it can be a tuple
    // These are public because we will need to clear the caches when they get too big
    public static Memoizer<(IQuad, IQuad, IQuad, IQuad), IQuad> MakeQuadMemoizer { get; } = new Memoizer<(IQuad, IQuad, IQuad, IQuad), IQuad>(UnmemoizedMake);

    private static IQuad UnmemoizedMake((IQuad nw, IQuad ne, IQuad se, IQuad sw) args)
    {
        // The four children of an n-quad must all be (n-1)-quads
        if (args.nw.Level != args.ne.Level || 
            args.ne.Level != args.se.Level || 
            args.se.Level != args.sw.Level || 
            args.nw.Level >= MaxLevel)
            throw new InvalidOperationException();

        return new Quad(args.nw, args.ne, args.se, args.sw);
    }

    public static IQuad Make(IQuad nw, IQuad ne, IQuad se, IQuad sw) => MakeQuadMemoizer.MemoizedFunc((nw, ne, se, sw));

    public static Memoizer<int, IQuad> EmptyMemoizer { get; } = new Memoizer<int, IQuad>(UnmemoizedEmpty);

    private static IQuad UnmemoizedEmpty(int level)
    {
        if (level == 0)
            return Dead;
        var e = Empty(level - 1);
        return Make(e, e, e, e);
    }

    public static IQuad Empty(int level) => EmptyMemoizer.MemoizedFunc(level);
}
