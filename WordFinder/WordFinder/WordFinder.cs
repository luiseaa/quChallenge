using System;
namespace WordFinder
{
    public class WordFinder
    {
        private const int MaxSize = 64;
        private readonly char[][] _matrix;
        private readonly int _rows;
        private readonly int _cols;
        private readonly TrieNode _trie;

        /// <summary>
        /// The WordFinder constructor receives a set of strings which represents a character matrix. The matrix size does not exceed 64x64, all strings contain the same number of characters.
        /// </summary>
        /// <param name="matrix"></param>
        /// <exception cref="ArgumentException"></exception>
        public WordFinder(IEnumerable<string> matrix)
        {

            _matrix = matrix.Select(row => row.ToCharArray()).ToArray();
            _rows = _matrix.Length;
            _cols = _matrix[0].Length;

            if (_rows > MaxSize || _cols > MaxSize)
            {
                throw new ArgumentException($"Matrix size exceeds the maximum allowed size of {MaxSize}x{MaxSize}");
            }

            _trie = new TrieNode();
        }

        /// <summary>
        /// The "Find" method should return the top 10 most repeated words from the word stream found in the matrix. If no words are found, the "Find" method should return an empty set of strings
        /// </summary>
        /// <param name="wordstream"></param>
        /// <returns></returns>
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            int TopNumberOfRepeatedWords = 10; // Per requirement Top 10

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

            return foundWords.OrderByDescending(word => word).Take(TopNumberOfRepeatedWords);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
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

        /// <summary>
        /// Depth-First Search (DFS): Use DFS to traverse the matrix and look for words. As we traverse, we can simultaneously check both horizontal and vertical words using the Trie
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <param name="node"></param>
        /// <param name="prefix"></param>
        /// <param name="foundWords"></param>
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
}

