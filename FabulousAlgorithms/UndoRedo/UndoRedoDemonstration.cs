using FabulousAlgorithms.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace FabulousAlgorithms.UndoRedo
{
    public static class UndoRedoDemonstration
    {
        public static void Demonstrate()
        {
            var u = new UndoRedoQueue<int>();

            u.Enqueue(10);
            Console.WriteLine(u.Bracket());
            u.Enqueue(20);
            Console.WriteLine(u.Bracket());
            u.Enqueue(30);
            Console.WriteLine(u.Bracket());
            u.Undo();
            Console.WriteLine(u.Bracket());
            u.Redo();
            Console.WriteLine(u.Bracket());
            u.Dequeue();
            Console.WriteLine(u.Bracket());
            u.Undo();
            Console.WriteLine(u.Bracket());
        }
    }
}
