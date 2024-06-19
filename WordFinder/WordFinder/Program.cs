using System;
using System.Collections.Generic;
using System.Linq;

public class WordFinder
{
    private readonly char[][] _matrix;
    private readonly int _rows;
    private readonly int _cols;
    private readonly TrieNode _trie;

    public WordFinder(IEnumerable<string> matrix)
    {
        _matrix = matrix.Select(row => row.ToCharArray()).ToArray();
        _rows = _matrix.Length;
        _cols = _matrix[0].Length;
        _trie = new TrieNode();
    }

    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        var uniqueWords = new HashSet<string>(wordstream, StringComparer.OrdinalIgnoreCase);
        foreach (var word in uniqueWords)
        {
            AddWordToTrie(word);
        }

        var foundWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cols; c++)
            {
                DFS(r, c, _trie, string.Empty, foundWords);
            }
        }

        return foundWords.OrderByDescending(word => word).Take(10);
    }

    private void AddWordToTrie(string word)
    {
        var node = _trie;
        foreach (var ch in word)
        {
            if (!node.Children.ContainsKey(ch))
            {
                node.Children[ch] = new TrieNode();
            }
            node = node.Children[ch];
        }
        node.IsWord = true;
    }

    private void DFS(int r, int c, TrieNode node, string prefix, HashSet<string> foundWords)
    {
        if (r < 0 || r >= _rows || c < 0 || c >= _cols || _matrix[r][c] == '#' || !node.Children.ContainsKey(_matrix[r][c]))
        {
            return;
        }

        char ch = _matrix[r][c];
        node = node.Children[ch];
        prefix += ch;

        if (node.IsWord)
        {
            foundWords.Add(prefix);
        }

        char temp = _matrix[r][c];
        _matrix[r][c] = '#'; // Mark as visited

        DFS(r + 1, c, node, prefix, foundWords); // Vertical
        DFS(r, c + 1, node, prefix, foundWords); // Horizontal

        _matrix[r][c] = temp; // Restore original value
    }
}

public class TrieNode
{
    public Dictionary<char, TrieNode> Children { get; } = new Dictionary<char, TrieNode>();
    public bool IsWord { get; set; }
}

// Example Usage
public class Program
{
    public static void Main()
    {
        var matrix = new List<string>
        {
            "abcdc",
            "fgwio",
            "chill",
            "pqnsd",
            "uvdxy"
        };

        var wordstream = new List<string> { "chill", "cold", "wind", "rain" };

        var wordFinder = new WordFinder(matrix);
        var foundWords = wordFinder.Find(wordstream);

        foreach (var word in foundWords)
        {
            Console.WriteLine(word);
        }
    }
}
