using FabulousAlgorithms.Common;

namespace FabulousAlgorithms.Life;

using static Quad;
public class HashLife : ILife
{
    private IQuad cells = Empty(9);
    private int stepCallCount = 0;
    private int maxCache = 200000;

    public HashLife() { }

    public void Clear() => cells = Empty(9);

    public bool this[long x, long y]
    {
        get => cells.Contains((x, y)) && cells.Get((x, y)) != Dead;
        set
        {
            while (!cells.Contains((x, y)))
                cells = cells.Embiggen();
            cells = cells.Set((x, y), value ? Alive : Dead);
        }
    }

    private static IQuad StepBaseCase(IQuad q)
    {
        int a = (q.NW.NW == Dead) ? 0 : 1;
        int b = (q.NW.NE == Dead) ? 0 : 1;
        int c = (q.NE.NW == Dead) ? 0 : 1;
        int d = (q.NE.NE == Dead) ? 0 : 1;
        int e = (q.NW.SW == Dead) ? 0 : 1;
        int f = (q.NW.SE == Dead) ? 0 : 1;
        int g = (q.NE.SW == Dead) ? 0 : 1;
        int h = (q.NE.SE == Dead) ? 0 : 1;
        int i = (q.SW.NW == Dead) ? 0 : 1;
        int j = (q.SW.NE == Dead) ? 0 : 1;
        int k = (q.SE.NW == Dead) ? 0 : 1;
        int l = (q.SE.NE == Dead) ? 0 : 1;
        int m = (q.SW.SW == Dead) ? 0 : 1;
        int n = (q.SW.SE == Dead) ? 0 : 1;
        int o = (q.SE.SW == Dead) ? 0 : 1;
        int p = (q.SE.SE == Dead) ? 0 : 1;
        int nf = a + b + c + e + g + i + j + k;
        int ng = b + c + d + f + h + j + k + l;
        int nj = f + g + h + j + l + n + o + p;
        int nk = e + f + g + i + k + m + n + o;
        return Make(Rule(q.NW.SE, nf), Rule(q.NE.SW, ng), Rule(q.SE.NW, nj), Rule(q.SW.NE, nk));
    }

    private static IQuad Rule(IQuad q, int count)
        => count switch
        {
            2 => q,
            3 => Alive,
            _ => Dead
        };

    private static IQuad UnmemoizedStep((IQuad q, int speed) args)
    {
        IQuad q = args.q;
        int speed = args.speed;

        IQuad r;

        if (q.IsEmpty)
            r = Empty(q.Level - 1);
        else if (speed == 0 && q.Level == 2)
            r = StepBaseCase(q);
        else
        {
            int nineSpeed = (speed == q.Level - 2) ? speed - 1 : speed;

            IQuad q9nw = Step(q.NW, nineSpeed);
            IQuad q9n = Step(q.North(), nineSpeed);
            IQuad q9ne = Step(q.NE, nineSpeed);
            IQuad q9w = Step(q.West(), nineSpeed);
            IQuad q9c = Step(q.Center(), nineSpeed);
            IQuad q9e = Step(q.East(), nineSpeed);
            IQuad q9sw = Step(q.SW, nineSpeed);
            IQuad q9s = Step(q.South(), nineSpeed);
            IQuad q9se = Step(q.SE, nineSpeed);
            IQuad q4nw = Make(q9nw, q9n, q9c, q9w);
            IQuad q4ne = Make(q9n, q9ne, q9e, q9c);
            IQuad q4se = Make(q9c, q9e, q9se, q9s);
            IQuad q4sw = Make(q9w, q9c, q9s, q9sw);
            if (speed == q.Level - 2)
            {
                IQuad rnw = Step(q4nw, speed - 1);
                IQuad rne = Step(q4ne, speed - 1);
                IQuad rse = Step(q4se, speed - 1);
                IQuad rsw = Step(q4sw, speed - 1);
                r = Make(rnw, rne, rse, rsw);
            }
            else
            {
                IQuad rnw = q4nw.Center();
                IQuad rne = q4ne.Center();
                IQuad rse = q4se.Center();
                IQuad rsw = q4sw.Center();
                r = Make(rnw, rne, rse, rsw);
            }
        }
        return r;
    }

    static Memoizer<(IQuad, int), IQuad> StepSpeedMemoizer { get; } = new(UnmemoizedStep);

    private static IQuad Step(IQuad q, int speed) => StepSpeedMemoizer.MemoizedFunc((q, speed));

    public void Step(int speed = 0)
    {
        if (speed < 0 || speed > MaxLevel - 2)
            throw new InvalidOperationException();
        bool resetMaxCache = false;
        stepCallCount += 1;
        if ((stepCallCount & 0x1ff) == 0)
        {
            int cacheSize = MakeQuadMemoizer.Count + StepSpeedMemoizer.Count;

            if (cacheSize > maxCache)
            {
                resetMaxCache = true;
                ResetCaches();
            }
        }

        IQuad current = cells;
        if (!current.HasAllEmptyEdges())
            current = current.Embiggen().Embiggen();
        else if (current.Center().HasAllEmptyEdges())
            current = current.Embiggen();

        while (current.Level < speed + 2)
            current = current.Embiggen();

        IQuad next = Step(current, speed);
        cells = next.Embiggen();

        if (resetMaxCache)
        {
            int cacheSize = MakeQuadMemoizer.Count + StepSpeedMemoizer.Count;

            maxCache = Math.Max(maxCache, cacheSize * 2);
        }
    }

    private static void ResetCaches()
    {
        MakeQuadMemoizer.Clear();
        EmptyMemoizer.Clear();
        StepSpeedMemoizer.Clear();
    }
}
