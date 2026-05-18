using FabulousAlgorithms.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.Life;

using static Quad;
public class HashLife : ILife
{
    private IQuad cells = Empty(9);

    private int stepCallCount = 0;

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

    public void Step(int speed = 0)
    {
        throw new NotImplementedException();
    }

    private static IQuad Rule(IQuad q, int count)
        => count switch
        {
            2 => q,
            3 => Alive,
            _ => Dead
        };

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
}
