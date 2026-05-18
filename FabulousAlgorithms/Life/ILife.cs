using System;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.Life;

public interface ILife
{
    void Clear();
    bool this[long x, long y] { get; set; }
    void Step(int speed = 0);
}
