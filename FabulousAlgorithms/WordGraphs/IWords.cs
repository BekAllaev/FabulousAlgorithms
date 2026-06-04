namespace FabulousAlgorithms.WordGraphs;

public interface IWords : IEnumerable<string>
{
    bool Contains(string word);
    IEnumerable<string> StartsWith(string prefix);
}
