using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        string filePath = "C:/Users/Stefan/Desktop/chitanka.txt";
        string text = File.ReadAllText(filePath, Encoding.Default);
        int wordCount = CountWords(text);
        string shortestWord = FindShortestWord(text);
        string longestWord = FindLongestWord(text);
        double averageWordLength = CalculateAverageWordLength(text);
        Dictionary<string, int> mostCommonWords = FindMostCommonWords(text, 5);
        Dictionary<string, int> leastCommonWords = FindLeastCommonWords(text, 5);
        Console.WriteLine($"1. Брой думи: {wordCount}");
        Console.WriteLine($"2. Най-кратка дума: {shortestWord}");
        Console.WriteLine($"3. Най-длга дума: {longestWord}");
        Console.WriteLine($"4. Средна дължина: {averageWordLength:F2}");
        Console.WriteLine("5. Петте най-чести думи:");
        PrintWordFrequency(mostCommonWords);
        Console.WriteLine("6. Петте най-редки думи:");
        PrintWordFrequency(leastCommonWords);
    }
    static int CountWords(string text)
    {
        string[] words = Regex.Split(text, @"\W+");
        int count = 0;
        foreach (var word in words)
        {
            if (word.Length >= 3)
            {
                count++;
            }
        }
        return count;
    }
    static string FindShortestWord(string text)
    {
        string[] words = Regex.Split(text, @"\W+");
        string shortestWord = null;
        int shortestLength = int.MaxValue;
        foreach (var word in words)
        {
            if (word.Length >= 3 && word.Length < shortestLength)
            {
                shortestWord = word;
                shortestLength = word.Length;
            }
        }
        return shortestWord;
    }
    static string FindLongestWord(string text)
    {
        string[] words = Regex.Split(text, @"\W+");
        string longestWord = null;
        int longestLength = 0;
        foreach (var word in words)
        {
            if (word.Length >= 3 && word.Length > longestLength)
            {
                longestWord = word;
                longestLength = word.Length;
            }
        }
        return longestWord;
    }
    static double CalculateAverageWordLength(string text)
    {
        string[] words = Regex.Split(text, @"\W+");
        int totalLength = 0;
        int wordCount = 0;
        foreach (var word in words)
        {
            if (word.Length >= 3)
            {
                totalLength += word.Length;
                wordCount++;
            }
        }
        return wordCount > 0 ? (double)totalLength / wordCount : 0;
    }
    static Dictionary<string, int> FindMostCommonWords(string text, int count)
    {
        string[] words = Regex.Split(text, @"\W+");
        Dictionary<string, int> wordFrequency = new Dictionary<string, int>();
        foreach (var word in words)
        {
            if (word.Length >= 3)
            {
                string cleanedWord = word.ToLower();
                if (wordFrequency.ContainsKey(cleanedWord))
                {
                    wordFrequency[cleanedWord]++;
                }
                else
                {
                    wordFrequency[cleanedWord] = 1;
                }
            }
        }
        SortedList<int, List<string>> sortedWords = new SortedList<int, List<string>>();

        foreach (var pair in wordFrequency)
        {
            if (sortedWords.ContainsKey(pair.Value))
            {
                sortedWords[pair.Value].Add(pair.Key);
            }
            else
            {
                sortedWords[pair.Value] = new List<string> { pair.Key };
            }
        }
        Dictionary<string, int> mostCommon = new Dictionary<string, int>();
        for (int i = sortedWords.Count - 1; i >= 0 && count > 0; i--)
        {
            var wordsList = sortedWords.Values[i];
            wordsList.Sort();
            foreach (var word in wordsList)
            {
                mostCommon[word] = sortedWords.Keys[i];
                count--;
                if (count == 0)
                    break;
            }
        }

        return mostCommon;
    }
    static Dictionary<string, int> FindLeastCommonWords(string text, int count)
    {
        string[] words = Regex.Split(text, @"\W+");
        Dictionary<string, int> wordFrequency = new Dictionary<string, int>();
        foreach (var word in words)
        {
            if (word.Length >= 3)
            {
                string cleanedWord = word.ToLower();
                if (wordFrequency.ContainsKey(cleanedWord))
                {
                    wordFrequency[cleanedWord]++;
                }
                else
                {
                    wordFrequency[cleanedWord] = 1;
                }
            }
        }
        SortedList<int, List<string>> sortedWords = new SortedList<int, List<string>>();
        foreach (var pair in wordFrequency)
        {
            if (sortedWords.ContainsKey(pair.Value))
            {
                sortedWords[pair.Value].Add(pair.Key);
            }
            else
            {
                sortedWords[pair.Value] = new List<string> { pair.Key };
            }
        }
        Dictionary<string, int> leastCommon = new Dictionary<string, int>();
        for (int i = 0; i < sortedWords.Count && count > 0; i++)
        {
            var wordsList = sortedWords.Values[i];
            wordsList.Sort();
            foreach (var word in wordsList)
            {
                leastCommon[word] = sortedWords.Keys[i];
                count--;
                if (count == 0)
                    break;
            }
        }
        return leastCommon;
    }
    static void PrintWordFrequency(Dictionary<string, int> wordFrequency)
    {
        foreach (var kvp in wordFrequency)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value} times");
        }
    }
}
