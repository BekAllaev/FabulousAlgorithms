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
}
