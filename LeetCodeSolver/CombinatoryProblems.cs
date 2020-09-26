using System;
using System.Collections.Generic;

namespace LeetCodeSolver
{
    class CombinatoryProblems
    {
        public static IList<IList<int>> CombinationSum3(int k, int n)
        {
            IList<IList<int>> list = new List<IList<int>>();
            if (n > k * 9 || n < k) return list;
            List<int> curr = new List<int>();
            recursiveAppending(list, curr, k, 1, n);
            return (IList<IList<int>>)list;
        }
        public static void recursiveAppending(IList<IList<int>> list, List<int> curr, int k, int start, int sum)
        {
            if (sum < 0 || k < curr.Count)
                return;
            if (curr.Count == k && sum == 0)
            {
                list.Add(new List<int>(curr));
                return;
            }
            for (int i = start; i <= Math.Min(sum, 9); i++)
            {
                curr.Add(i);
                recursiveAppending(list, curr, k, i + 1, sum - i);
                curr.RemoveAt(curr.Count - 1);
            }
        }
    }
}
