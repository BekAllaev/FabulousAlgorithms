using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace FabulousAlgorithms.WordGraphs;

public class DawgBuilder
{
    private class NodeEquality : IEqualityComparer<Node>
    {
        public bool Equals(Node? x, Node? y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            if (x.IsEnd != y.IsEnd || x.Count != y.Count)
                return false;

            return x.EdgeLabels.All(e => y.HasEdge(e) && ReferenceEquals(x.FollowEdge(e), y.FollowEdge(e)));
        }

        public int GetHashCode([DisallowNull] Node obj)
        {
            var h = new HashCode();

            h.Add(obj.IsEnd);

            foreach(var e in obj.EdgeLabels.OrderBy(c => c))
            {
                h.Add(e);
                h.Add(obj.FollowEdge(e));
            }

            return h.ToHashCode();
        }
    }

    private Node root = new();

    private HashSet<Node> canonicalNodes = new HashSet<Node>(new NodeEquality());

    private DawgBuilder() { }

    public static Node MakeDawg(IEnumerable<string> words)
    {
        var db = new DawgBuilder();
        foreach (var word in words)
            db.Add(word);
        db.FixUpLastWord(db.root);
        return db.root;
    }

    private void Add(string word)
    {
        int i = 0;
        Node n = root;

        for (; i < word.Length; i += 1)
        {
            if (!n.HasEdge(word[i]))
                break;
            n = n.FollowEdge(word[i]);
        }

        FixUpLastWord(n);

        for (; i < word.Length; i += 1)
            n = n.AddNewNode(word[i]);

        n.IsEnd = true;
    }

    private void FixUpLastWord(Node n)
    {
        if (n.Count == 0)
            return;

        char c = n.LastEdgeLabel;
        Node s = n.FollowEdge(c);
        FixUpLastWord(s);

        Node? canonical;
        canonicalNodes.TryGetValue(s, out canonical);

        if (canonical == null)
            canonicalNodes.Add(s);
        else
            n.ReplaceEdge(c, canonical);
    }
}
