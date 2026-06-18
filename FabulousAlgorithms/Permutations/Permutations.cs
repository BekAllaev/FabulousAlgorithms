using FabulousAlgorithms.Common;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace FabulousAlgorithms.Permutations;

public class Permutations
{
    static IList<int> GetLexiPermutation(int n, BigInteger p)
    {
        var remaining = new List<int>(Enumerable.Range(0, n));
        var result = new List<int>(n);
        var f = n.Factorial();

        for (int cur = n; cur > 0; cur -= 1)
        {
            f /= cur;
            var r = (int)(p / f);
            p %= f;
            result.Add(remaining[r]);
            remaining.RemoveAt(r);
        }

        return result;
    }
}
