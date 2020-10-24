using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeSolver
{
    class MiscellaneousProblems
    {
        //Name: Gas Station

        //Description: Given n gas stations in a circular route
        //Each gas station give gas[i] amount of fuel and took cost[i] to travel to the next station
        //Return which gas station to start from to finish the route. If impossible return -1

        //Approach:
        //It's impossible to finish route if sum(gas) < sum(cost)
        //If not, it's guaranteed to be possible
        //Start from gas station 0. Calculate gas & cost to reach 1,2,3,...
        //If there's not enough gas to go 0-3 but enough to go 0-2, there's not enough gas to go 2-3.
        //Skip every route in between when gas marginal sum go to 0. Reset sum to 0 and continue the process at failed station

        //Analysis:
        //Time: O(n), we loop through the array once and didn't examine intermediate stations
        //Space: O(1). There's no additional data structures used.

        //Edge cases: None
        public int canCompleteCircuit(int[] gas, int[] cost)
        {
            int total = 0;
            int start = 0;
            int margin = 0;
            for (int i = 0; i < gas.Length; i++)
            {
                int remaining = gas[i] - cost[i];
                if (margin >= 0)
                    margin += remaining;
                else
                {
                    margin = remaining;
                    start = i;
                }
                total += remaining;

            }
            if (total >= 0)
                return start;
            return -1;

        }
        //Name: 132 Pattern

        //Description: Given an array of integer, determine if the pattern 132 exists
        //Pattern 132 meant that there are 3 ints, i<j<k, such that nums[i]<nums[k]<nums[j]

        //Approach: Pattern 132 implies that nums[i] is the smallest. 
        //There is a wider range for nums[i] if we pick the largest nums[j] that is < nums[k]

        //Traverse from the end. Put stuff in and don't pop unless there's no 32 pattern yet (i.e nums[j] < nums[k])
        //If nums[j] > nums[k], there could be a 32 pattern. Rmb that the stack are sorted ascending because 
        //things aren't pop if they are smaller than the top. Pop everything as long as nums[j]>pop to obtain the largest nums[k] that < nums[j]

        //Analysis:
        //Time: O(n). We loop through the array once
        //Space: O(n). We use stack to store the int. Max size of stack is n when list is sorted ascending.

        public bool Find132pattern(int[] nums)
        {
            Stack<int> s = new Stack<int>();
            int second = Int32.MinValue;
            for (int i = nums.Length - 1; i > -1; i--)
            {
                if (nums[i] < second)
                    return true;
                while (s.Count() != 0 && nums[i] > s.Peek())
                {
                    second = s.Pop();
                }
                s.Push(nums[i]);

            }
            return false;
        }
    }
}
