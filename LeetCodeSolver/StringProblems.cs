using System.Collections.Generic;

namespace LeetCodeSolver
{
    class StringProblems
    {
        //Name: KMP Preprocessing Tool

        //Description: Find the longest prefix-suffix, that is, the longest length 
        //where both prefix and suffix of a string equals one another
        //Example: "aabaabaa", length = 2 because first 2 chars = last 2 chars
        //Derive an integer array that can tells this length from 0 to any index of string s

        //Approach: We have 2 pointers, one at end of prefix, one at end of suffix
        //if j = i, end of prefix = end of suffix, increase length for both and record it in lps[i]
        //if j != i, end of prefix != end of suffix, find a prefix that matches suffix
        //if can't find/j is at 0 and prefix doesn't exist, records lps[i] as 0 and move on

        //Analysis:
        //Time: O(n), we loop through string once.
        //Space: O(n), n = string length. We use an array to keep track of longest prefix-suffix at index i

        //Edge cases: None
        public int[] LongestPrefixSuffix(string s)
        {
            if (s.Length == 0) return new int[] { };
            if (s.Length == 1) return new int[] { 0 };
            int[] lps = new int[s.Length];
            int j = 0;
            int i = 1;
            while (i < s.Length)
            {
                if (s[i] == s[j])
                {
                    j++;
                    lps[i] = j;
                    i++;
                }
                else
                {
                    if (j != 0)
                    {
                        //When j != 0, we have a prefix that matched a suffix at i-1 & j-1
                        //For i-1, prefix = suffix => prefix's suffix = suffix's suffix Ex: abcab... abcab where last 2 of suffix = last 2 of prefix = first 2 of prefix 
                        //We can use lps array to determine what's the longest length where prefix's prefix = prefix's suffix = suffix's suffix
                        //Go back to the end of prefix's prefix. This is now our new prefix and where j ends. +1 it (0-based index already assumed +1)
                        //and compare our new prefix characters to the current i
                        j = lps[j - 1];
                    }
                    else
                    {
                        lps[i] = 0;
                        i++;
                    }
                }
            }
            return lps;
        }
        //Name: Repeated Substring Pattern

        //Description: Given a string, return true if it was formed by multiple substrings and false
        //if it isn't

        //Approach: We use KMP pre-processing to determines the longest prefix-suffix within a string
        //A string of many substrings will have overlap prefix and suffix
        //The length of first pattern will be string length - longest suffix since j of KMP only start increasing
        //when s[i] == s[j] => length of suffix started at the second pattern
        //See if string length is divisible by pattern length. If it isn't, there's no way to construct a non-integer amount of pattern, return false
        //If it is, we can construct such string, and prefix's first pattern = suffix's first pattern, prefix's 2nd = suffix's 2nd... and everything match. Return true

        //Analysis:
        //Time: O(n), n = length of string. The preprocess of this string is O(n) - See KMP Preprocessing
        //Space: O(n), n = length of KMP's array = length of string array. We create a temporary array to determine longest prefix-suffix

        //Edge cases: None
        public bool repeatedSubstringPattern(string s)
        {
            int[] lps = LongestPrefixSuffix(s);
            return (lps[lps.Length - 1] > 0 && s.Length % (s.Length - lps[lps.Length - 1]) == 0)
                               ? true : false;
        }
        //Name: Partition Labels

        //Description: A string S of lowercase English letters is given
        //We want to partition this string into as many parts as possible 
        //so that each letter appears in at most one part
        //and return a list of integers representing the size of these parts.

        //Approach: We partition the string when we encounter a character that isn't present in the rest of the string
        //i.e, when the count of that characters reach 0 since each letter can appear in at most one part
        //Create a dictionary that maps unique characters in the string to how many of each of them in the string
        //Loop characters through the string, decrease that character's count and add it to a HashSet.
        //When the character's count reachs 0, remove it from the HashSet.
        //This makes sure that this character doesn't appears in other subsequent substrings
        //When this HashSet becomes empty, all count of unique characters appear in the string reachs 0 and won't appear in subsequent substrings
        //Mark the length of the partition. Return the array of all substring's length

        //Analysis:
        //Time: O(n), n = string's length. We loop through the string twice, once to create the map of unique characters to its count, once to partition the string
        //Space: O(n), n = string's length. The dictionary is the biggest structure and increases linearly with the string's length

        //Edge cases: None
        public IList<int> PartitionLabels(string S)
        {
            List<int> ret = new List<int>();
            Dictionary<char, int> countChar = new Dictionary<char, int>();
            HashSet<char> seen = new HashSet<char>();
            foreach (char c in S)
            {
                if (countChar.ContainsKey(c)) countChar[c]++;
                else
                    countChar.Add(c, 1);
            }
            int count = 0;
            foreach (char c in S)
            {
                count++;
                seen.Add(c);
                countChar[c]--;
                if (countChar[c] == 0)
                {
                    seen.Remove(c);
                    if (seen.Count == 0)
                    {
                        ret.Add(count);
                        count = 0;
                    }
                }
            }
            return ret;
        }
        //Name: Word Pattern

        //Description: Given a pattern and a string str, find if str follows the same pattern.
        //Here follow means a full match, such that 
        //there is a bijection between a letter in pattern and a non-empty word in str.

        //Approach: Split the string for spaces. Loop through both the string array and the pattern array at the same time
        //Put the character and its corresponding word into a map. If the correspoding char-words doesn't match the one in the map
        //return false (break pattern)
        //Keep a HashSet to prevent duplicates in str (when 2 chars point to the same words)

        //Analysis:
        //Time: O(n). We loop through both string at the same time and add map as we go.
        //Space: O(n). The map's maximum size will be the amount of characters in pattern and words in str

        //Edge cases: Pattern must has the same char as str has words. If length 0 return false. Use HashSet.
        public bool WordPattern(string pattern, string str)
        {
            if (pattern.Length == 0 || str.Length == 0) return false;
            string[] words = System.Text.RegularExpressions.Regex.Split(str, @"\s+");
            if (words.Length != pattern.Length) return false;
            Dictionary<char, string> charToWord = new Dictionary<char, string>();
            HashSet<string> duplicate = new HashSet<string>();
            int i = 0;
            while (i < pattern.Length && i < words.Length)
            {
                if (charToWord.ContainsKey(pattern[i]))
                {
                    if (charToWord[pattern[i]] != words[i]) return false;
                }
                else
                {
                    if (!duplicate.Contains(words[i]))
                    {
                        charToWord.Add(pattern[i], words[i]);
                        duplicate.Add(words[i]);
                    }
                    else return false;
                }
                i++;
            }
            return true;
        }
        //Name: Word Break
        
        //Description: Given a string and a list of words, determine if the string can be form by the list of words
        //The words in the list can be used multiple times

        //Approach: We observed that a string "abcdef" can be broken to "abc" and "def". If both "abc" and "def" is in the list, "abcdef" can be constructed
        //Then, this is a dynamic programming that break the problems into subproblems
        //Create a boolean array to record whether str[0:x] can be constructed. Start at length 0, which is true.
        //Attempt to partition it at 0...x. If dp[j] == true and str[j:x] is in the list, str[0:x] can be constructed
        //Construct the array from 0 to s.len. Return dp[-1]

        //Analysis:
        //Time: O(n^2), n as length of string. We run O(n) to build the bool array and partition the substring at all index to determine bool at certain index
        //Space: O(max(n,k)). We use a set that contains all string in wordDict with length k for faster search and a dp array with length n.
        
        //Edge cases: None

        public bool WordBreak(string s, IList<string> wordDict)
        {
            bool[] dp = new bool[s.Length + 1];
            HashSet<string> set = new HashSet<string>();
            foreach (string i in wordDict)
                set.Add(i);
            dp[0] = true;
            for(int len = 1;len<s.Length;len++)
            {
                for(int i = 0;i<len;i++)
                {
                    if (dp[i] && set.Contains(s.Substring(i, len-i)))
                    {
                        dp[len] = true;
                        break;
                    }
                }
            }
            return dp[s.Length];
        }

    }
}
