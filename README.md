# Word Finder

## Description

The Word Finder class is designed to search for words from a given word stream in a character matrix. Words may appear horizontally (left to right) or vertically (top to bottom). This implementation ensures efficient searching using advanced data structures and algorithms.

## Constructor

The WordFinder constructor receives a set of strings representing a character matrix. The matrix size must not exceed 64x64. Each string in the matrix contains the same number of characters.

## Find Method

The Find method takes a word stream as input and returns the top 10 most repeated words found in the matrix. If no words are found, it returns an empty set. The method ensures that each word in the word stream is counted only once, regardless of its multiple occurrences in the stream.

## Implementation Details

### Trie Data Structure

A Trie (prefix tree) is used to store the words from the word stream, enabling efficient searching of words in the matrix.

### Depth-First Search (DFS)

DFS is used to traverse the matrix, checking for words both horizontally and vertically.

### Matrix Size Verification

The constructor includes a verification step to ensure that the matrix size does not exceed 64x64. If the matrix exceeds this size, an `ArgumentException` is thrown.

## Example Usage

```csharp
var matrix = new List<string>
{
    "abcdc",
    "fgwio",
    "chill",
    "pqnsd",
    "uvdxy"
};

var wordstream = new List<string> { "chill", "cold", "wind", "rain" };

try
{
    var wordFinder = new WordFinder(matrix);
    var foundWords = wordFinder.Find(wordstream);

    foreach (var word in foundWords)
    {
        Console.WriteLine(word);
    }
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}


