using System;
using System.Collections.Generic;
using System.Linq;

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
        //Name: Search in Rotated Sorted Array (Arrays w/ duplicates)

        //Description: Same as Search in Rotated Sorted Array, except array can contains duplicates

        //Approach: Same as above, except there's cases where left == mid == right and we can't determine where mid is relative to the pivot.
        //In that case, we just have to exclude those 2 elements by increment left and decrement right
        //This will results in O(n) worst case where an array is full of duplicates and doesn't contain target.

        //Analysis:
        //Time: O(n), Theta(log n). Same as above, except there's a worst case of O(n).
        //Space: O(1). We didn't use any extra data structure

        //Edge cases: If you continue to search after exclude first and last, there are cases where first == last which results in OOB.
        public int SearchDuplicates(int[] nums, int target)
        {
            int first = 0;
            int last = nums.Length - 1;
            while (first <= last)
            {
                int mid = first + (last - first) / 2;
                if (nums[mid] == target)
                    return mid;
                //if there's a series of duplicates, we cannot determine the pivot is left or right
                //[3,1,2,3,3,3,3] is the same as [3,3,3,3,1,2,3]
                if (nums[first] == nums[mid] && nums[mid] == nums[last])
                {
                    first++;
                    last--;
                    continue;
                    //continue to avoid case where first == last == end/beginning, thus OOB.
                }
                //mid pointer left of pivot, in larger partition
                if (nums[first] <= nums[mid])
                {
                    if (nums[first] <= target && target < nums[mid])
                    //target in [first,mid)
                    {
                        last = mid - 1;
                    }
                    else
                    //target in (mid,last]
                    {
                        first = mid + 1;
                    }
                }
                else
                //mid pointer right of pivot, in smaller partition
                {
                    if (nums[mid] < target && target <= nums[last])
                    //if target in (mid,last]
                    {
                        first = mid + 1;
                    }
                    //if target in [first,mid)
                    else
                    {
                        last = mid - 1;
                    }
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
        //Name: First Missing Positive

        //Description: Given an unsorted integer array, find the smallest positive integer
        //Use constant space and linear time

        //Approach: Normally if we were given space, we would create a temporary boolean array and marked it
        //Then we would scan that boolean array and find the first value that isn't in the list
        //Since we have to use constant space, we can mark the array provided instead of the boolean array
        //To do this, when we find a, marked nums[a] as negative
        //Loop through nums again and find which one we didn't mark as negative. Return that index
        //Note: The array can also have negative numbers. We need to exclude them, so dump them all on the left side

        //Analysis:
        //Time: O(n)
        //Space: O(1)

        //Edge cases: Remember to absolute everything when calculating index. When mark index as negative, don't do *=-1 or it will become positive again
        public int FirstMissingPositive(int[] nums)
        {
            int left = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] <= 0)
                {
                    int temp = nums[left];
                    nums[left] = nums[i];
                    nums[i] = temp;
                    left++;
                }
            }
            int limit = nums.Length - left;
            for (int i = left; i < nums.Length; i++)
            {
                if (Math.Abs(nums[i]) <= limit)
                {
                    int index = left + Math.Abs(nums[i]) - 1;
                    nums[index] = nums[index] < 0 ? nums[index] : -nums[index];
                }
            }
            for (int i = left; i < nums.Length; i++)
            {
                if (nums[i] > 0)
                {
                    return i - left + 1;
                }
            }
            return limit + 1;
        }
        //Name: Longest Mountain in Array
        //Description: A mountain is a continuous subarray where A[0] < A[1] < A[2] <...A[i] > A{i+1] > A[i+2] >...A[A.Length-1]
        //Note that i can be anywhere and doesn't have to be in the middle
        //Find the largest mountain in an array

        //Approach: As we traverse through the array, there are 3 scenarios:
        //The next element is smaller than the current element. If we're building the down slope, great! len++
        //If we're not building the down slope, scrap everything and start over with those 2 elements as the upward slope
        //The next element is larger than the current element. This is great if we're building upward or downward slope. len++
        //The next element is same as the current element. Scrap everything and start over.
        //This greedy approach works because you cannot build a mountain between 2 failed mountain. Every mountain in the array is distinct from each other, hence allows for one pass.
        //Remember to update max_len only when you have a mountain (i.e when you're building the downward slope) and not during construction

        //Analysis:
        //Time: O(n), n = length of A. This is one-pass.
        //Space: O(1). There is no extra data structure used.

        //Edge cases: The original prompt ask that the size of the mountain needs to be >= 3. This is unnecessary as all valid mountains's size >= 3.
        //Remember to only update max_len when you have a mountain, not during construction of mountain.
        public int LongestMountain(int[] A)
        {
            int state = 0;
            int length = 1;
            int max_len = 0;
            for (int i = 0; i < A.Length - 1; i++)
            {
                if (A[i + 1] > A[i] && (state == 0 || state == 1))
                {
                    state = 1;
                    length++;
                }
                else if (A[i + 1] < A[i] && (state == 1 || state == 2))
                {
                    state = 2;
                    length++;
                    max_len = Math.Max(max_len, length);
                }
                else if (A[i + 1] > A[i] && state == 2)
                {
                    state = 1;
                    length = 2;
                }
                else
                {
                    state = 0;
                    length = 1;
                }
            }
            return max_len;
        }
        //Name: Jump Game III

        //Description: Given an array (non-negative), you start at start. You can jump to start + arr[start] or start-arr[start]
        //You have infinite number of jumps. Return if you can encounter a value of 0

        //Approach: Either DFS or BFS with Stack or Queue
        //DFS, you pop from the stack and push the 2 squares you can jump to (if not out of bound). Mark the square you just pop as visited
        //so you don't visit them again
        //Return true if encounter 0. Return false when the queue/stack runs out of value (i.e visit all possible square without encountering 0)

        //Analysis:
        //Time: O(n). We can only land on at most n squares since we don't visit square twice.
        //Space: O(n). The stack/queue can contains n element if we can visit all square without backtracking.

        //Edge cases: None
        public bool CanReach(int[] arr, int start)
        {
            Stack<int> stackIndex = new Stack<int>();
            bool[] visited = new bool[arr.Length];
            stackIndex.Push(start);
            while (stackIndex.Count() != 0)
            {
                int idx = stackIndex.Pop();
                if (arr[idx] == 0)
                    return true;
                if (idx + arr[idx] < arr.Length && !(visited[idx + arr[idx]]))
                    stackIndex.Push(idx + arr[idx]);
                if (idx - arr[idx] > -1 && !(visited[idx - arr[idx]]))
                    stackIndex.Push(idx - arr[idx]);
                visited[idx] = true;
            }
            return false;
        }
        //Name: Smallest Range II

        //Description: Given an array and an integer K, you must either add or subtract K to every element on the array
        //Out of all possible configurations, pick a configuration so that the difference between the min and max of the array is as small as possible
        //return this difference


        //Approach:
        //Sort the array.
        //We pick a pivot so that we will add K to everything on the left of the pivot (inclusive) and subtract K from everything on the right of the pivot
        //The default pivot is at -1 (i.e we just subtract K to everything) - max-min here will be the same as A[n-1]-A[0]
        //We then pick pivot at 1, then at 2, then at 3,... until n-2. Picking pivot at n-1 will have the same result as picking the pivot at -1
        //At every pivot, get the new min & max and compare max-min to the prev max-min.
        //Profit!
        //The following code is greatly condensed to eliminate all unnecessary steps, so it might look kinda confusing.
        //Comment is made to restore the step we didn't take and why it's unnecessary.

        //Also, to not subtract and add k to every element at every pivot, we can just subtract K from everything after the array is sorted
        //and then add 2K to the element that we want to be +K.
        //however, notice that this step will not change max-min. Then, we can just keep the same array after sorting it.
        
        //Analysis:
        //Time: O(n log n). Sorting is n log n. There is a solution without sorting in linear time
        //Space: O(1). We didn't use any extra space.

        //Edge cases: Should be none
        public int SmallestRangeII(int[]A,int K)
        {
            Array.Sort(A);
            int n = A.Length;
            int min = A[0];
            int max = A[n - 1];
            //Default pivot at -1; when we just subtract k from everyone.
            int minDiff = max - min;
            for(int i = 0;i<n-1;i++)
            {
                //picking a pivot at i. everything before i (inclusive) will be +k, everything after i will be -k.

                //min = Math.Min(A[i]+2*K, A[0]+2*K); This step is unnecessary because the array is sorted, so A[0]+2*K < A[i]+2*K
                min = Math.Min(A[i + 1], A[0] + 2 * K);
                //max = Math.Max(A[i],A[n-1]); again, unneecssary because the array is sorted.
                max = Math.Max(A[n - 1], A[i] + 2 * K);
                minDiff = Math.Min(minDiff, max - min);
            }
            return minDiff;
        }
    }
}
