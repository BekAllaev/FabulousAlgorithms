using FabulousAlgorithms.Common;
using FabulousAlgorithms.ImmutableStack.Covariant;
using FabulousAlgorithms.ImmutableStack.Extensions;

namespace FabulousAlgorithms.HughesList
{
    public static class HListDemonstration
    {
        public static void Run()
        {
            var s = ImmutableStackCovariant<int>.Empty.Push(2).Push(3).Push(4);
            var hl432 = HList<int>.FromStack(s);
            var hl = hl432.Push(5).Append(1).Concatenate(hl432).Append(0);
            Console.WriteLine(hl.Bracket());

            //var s = ImmutableStackCovariant<int>.Empty.Push(2).Push(3).Push(4);

            //Console.WriteLine(s.Bracket());

            //var hl432 = HList<int>.FromStack(s);
            //Console.WriteLine(hl432.Bracket());

            //var hl = hl432.Push(5);
            //Console.WriteLine(hl.Bracket());

            //var hl1 = hl432.Append(1);
            //Console.WriteLine(hl1.Bracket());
        }
    }
}
