using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeSolver
{
    class BitProblems
    {
        //Name: Repeated DNA sequences

        //Description: Given a string of DNA, return all the 10-char substring that repeated at least twice in the string

        //Approach: Normally we would do a sliding window and add string we already encounter into hashset
        //Then, if we see it again, we will add them to the main list
        //However, this approach can take a lot of space, even with StringBuilder
        //Instead, we represent the 10-char substring with 20-byte int, 2 bytes for each char
        //We keep shifting 2 left and add the next 2 bytes. However, we only want to keep 20 bytes at any time
        //To keep the rightmost 20 bytes, & the hash with 1<<20 - 1 (11111111...). Any bits to the left would be set to 0
        //The rest is simple sliding window implementation

        //Analysis:
        //Time: O(n), n = s.Length. We loop through the string once and keep a sliding window in O(1)
        //Space: O(n), n = s.Length. 20-bytes int is still an int, and we only keep n-9 number of 20-bytes ints

        //Edge case: Don't add the same sequence to the list twice. Use another hashmap to keep track of the list

        public IList<string> FindRepeatedDnaSequences(string s)
        {
            List<String> seq = new List<String>();
            if (s == null || s.Length < 10)
                return seq;
            int mask = (1 << 20) - 1;
            int hash = 0; 

            Dictionary<char, int> gene = new Dictionary<char, int>();
            gene.Add('A', 0);
            gene.Add('G', 1);
            gene.Add('C', 2);
            gene.Add('T', 3);
            HashSet<int> inlist = new HashSet<int>();
            HashSet<int> seen = new HashSet<int>();
            for(int i = 0;i<s.Length;i++)
            {
                hash = (hash << 2) + gene[s[i]];
                if(i>=9)
                {
                    hash &= mask;
                    if(seen.Contains(hash) && !inlist.Contains(hash))
                    {
                        seq.Add(s.Substring(i - 9, 10));
                        inlist.Add(hash);
                    }
                    seen.Add(hash);
                }
            }
            return seq;
        }
    }
}
