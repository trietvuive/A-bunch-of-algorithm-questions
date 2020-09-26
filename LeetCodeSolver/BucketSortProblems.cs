using System;
using System.Collections.Generic;

namespace LeetCodeSolver
{
    class BucketSortProblems
    {
        //Name: Contains Duplicate III

        //Description: Given an array of integers, find out whether 
        //there are two distinct indices i and j in the array such that 
        //the absolute difference between nums[i] and nums[j] is at most t
        //and the absolute difference between i and j is at most k.

        //Approach: Use bucket sort. Each bucket accepts a range of number and each bucket can have absolute difference of val
        //We will create a bucket map that contains all number within a sliding window from i-index to i
        //If 2 numbers in same sliding window belong in the same bucket, they have abs difference of val, return true
        //check neighboring buckets (bucket+1 and bucket-1) for value with abs difference of val. return true if found
        //put number in bucket and remove the bucket of the number that just gone out of the sliding window - i-index number
        //there is no need to check ahead because when we reach the number ahead, the current number would still be in the sliding window and would be checked

        //Analysis:
        //Time: O(n), n = length of array. We use sliding window and create the bucket as we go. We only loop through array once.
        //Space: O(index). The size of the bucket map never exceeds index+1 because we remove the elements that fall out of the sliding window

        //Edge cases: int.max, int.min, integer overflow, floating-point division, flooring of negative numbers, positive-negative numbers difference

        public bool withinTindexKval(int[] nums, int index, int val)
        {
            if (nums == null || index < 0 || val < 0) return false;
            Dictionary<int, int> buckets = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                double division = nums[i] / ((double)val + 1);
                double floor = Math.Floor(division);
                int bucket = (int)floor;
                if (buckets.ContainsKey(bucket))
                    return true;
                buckets.Add(bucket, nums[i]);
                if (buckets.ContainsKey(bucket - 1) && (long)nums[i] - buckets[bucket - 1] <= val)
                {
                    return true;
                }
                if (buckets.ContainsKey(bucket + 1) && buckets[bucket + 1] - (long)nums[i] <= val)
                {
                    return true;
                }
                if (buckets.Count > index)
                {
                    buckets.Remove((int)Math.Floor((double)(nums[i - index] / (long)(val + 1))));
                }

            }
            return false;
        }
    }
}
