using System;
using System.Collections.Generic;

namespace LeetCodeSolver
{
    class Utility
    {
        public static void printMap<K, V>(Dictionary<K, V> dict)
        {
            foreach (KeyValuePair<K, V> pair in dict)
            {
                Console.WriteLine("Key {0} Value {1} ", pair.Key.ToString(), pair.Value.ToString());
            }
        }
        //if we don't need to return index immediately, set a boolean to break the while loop and continue searching
        public static int FindSortedIndex(int[] array, int val)
        {
            int n = array.Length;
            int low = 0;
            int high = n - 1;
            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                int current = array[mid];
                if (current == val)
                {
                    return mid;
                }
                else if (current < val)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }
            return low;
        }

    }
}
