using System;

namespace LeetCodeSolver
{
    class ArrayProblems
    {
        //Name: Search in Rotated Sorted Array

        //Description: A sorted array was partitioned into 2 parts and switched their place.
        //Example: [0,1,2,3,4,5,6,7] become [4,5,6,7,0,1,2,3]
        //Create a method that search for a number in this sorted rotated array

        //Approach: Use a modified version of binary search. Noticed that the larger partition is on the left and smaller partition on the right
        //Instead of assuming that first<=mid<=last, evaluate to see which partition mid fall in
        //if first<=mid, mid is in the left/large partition. If not, mid is in right/smaller partition
        //See if target fall in the mid's partition. If yes, search the partition, and if not, cut the partition
        //The code has comment to indicate the range that the binary search is trying to evaluate

        //Analysis:
        //Time: O(log n). This is a modified version of binary search and is still in logarithmic time
        //Space: O(1). We didn't use any extra structures

        //Edge cases: If you know the binary search template, you should be good. Otherwise, watch for integer overflow and floor division edge cases
        public int Search(int[] nums, int target)
        {
            int first = 0;
            int last = nums.Length - 1;
            while (first <= last)
            {
                int mid = first + (last - first) / 2;
                if (nums[mid] == target)
                    return mid;
                if (nums[first] <= nums[mid])
                //mid pointer before pivot and is in left/larger partition
                {
                    if (nums[first] <= target && target < nums[mid])
                        //target between [first,mid)
                        last = mid - 1;
                    else
                        //target between (mid,last] cuz already evaluate mid
                        first = mid + 1;

                }
                else
                {
                    //mid pointer after pivot and is in right/smaller partition
                    if (nums[mid] < target && target <= nums[last])
                        //target between (mid,last]
                        first = mid + 1;
                    else
                        //target between [first,mid)
                        last = mid - 1;
                }
            }
            return -1;
        }
        //Name: Maximum Product Subarray

        //Description: Given an integer array nums, find the subarray with the largest product.
        //Return the product

        //Approach: Subarray is also composed of multiple subarrays
        //We basically want to take as many numbers as possible, but it can be negative
        //Then, we will store 2 variables: one denote the largest positive product through taking everything not 0
        //and one denote the largest (negative) product
        //The below comment should explain pre & post-condition of the program's flow

        //Analysis:
        //Time: O(n), we loop through the array once and keep 2 variables
        //Space: O(1). Beside the two variables and 1 return, there's no usage of growing data structures

        //Edge cases: Arrays where largest product is 0, arrays with 0, single-element array, array of all -1 that can
        //confuse the algorithm to think that max_ret was never changed
        public int MaxProduct(int[] nums)
        {
            int n = nums.Length;
            if (n == 1) return nums[0];
            int flag = 0;
            long maxret = 1;
            //why 1? Because x*1 = x. It's like 0 for largest subarray sum
            long max_ending = 1;
            long min_ending = 1;
            for (int i = 0; i < n; i++)
            {
                long element = nums[i];
                //positive number. multiply it to max_ending and multiply it to min_ending only if min_ending < 0 
                if (element > 0)
                {
                    max_ending = element * max_ending;
                    min_ending = Math.Min(min_ending * element, 1);
                    //if found a positive number, no need to consider edge cases where largest product = 0
                    flag = 1;
                }
                //0. reset everything to 1; all subarray with 0 have product of 0 and we don't want that
                if (element == 0)
                {
                    max_ending = 1;
                    min_ending = 1;
                }
                if (element < 0)
                //negative number. Multiply max_ending (postive) with element (negative) and 
                //compare it to min_ending
                //Compare max_ending to min_ending (negative) * element (negative)
                //Rmb to keep temp variable because we are changing both variables at the same time
                {
                    long temp = max_ending;
                    //this is an edge cases line. This line means if we found 2 consecutive negative numbers
                    //they multiply to become negative. There's no way we can return 0 then.
                    //Similar to when we set flag = 1 when we found positive numbers, we will set flag = 1
                    //when we found 2 negative numbers.
                    if (min_ending < 0 && element < 0) flag = 1;
                    max_ending = Math.Max(min_ending * element, 1);
                    min_ending = temp * element;
                }
                maxret = Math.Max(maxret, max_ending);
            }
            if (maxret == 1 && flag == 0)
                return 0;
            return (int)maxret;
        }
    }
}
