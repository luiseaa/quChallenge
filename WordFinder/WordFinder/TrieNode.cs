using System;
namespace WordFinder
{
    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; } = new Dictionary<char, TrieNode>();
        public bool IsWord { get; set; }
    }
}

