using System.Security.Cryptography;

namespace LeetCodeSolver
{
    class MiscellaneousProblems
    {
        //Name: Gas station

        //Description: A car is running in a circle through N gas stations
        //Each gas stations refuel the car by certain amount of fuel, but also cost certain amount of fuels to reach between station
        //Given an array of gas cost and gas refuel for each stations, return the station that the car should start to complete the cycle
        //Return -1 if the car can't complete the cycle.

        //Approach: If it costs more gas to reach all stations than all stations provide, it's not possible.
        //Vice versa, it's always possible. There's always at least 1 starting point that can go full circle.
        //If a car can't reach C from A (with B in between), 2 scenarios:
        //a) The car doesn't have enough to reach B from A. No problem, we will try to start at B next.
        //b) The car have enough gas to reach B from A. If it's determined that A cannot reach C, but A can reach B, then B cannot reach C.
        //An approach would be to examine whether we have enough gas to reach the next station.
        //If we have enough, continue to the next station and store the current starting point
        //If we don't have enough, locate the starting point at the current station.
        //There's no need to examine all the stations in between because if we can't reach C from A, we can't reach C from B either.
        //There's no need to go full circle because we are guaranteed at least 1 starting points if station has more gas than cost to reach them.
        //Loop through all starting points from 0. If cost more gas than accumulated, start at current index. If reach the end, return last index (guaranteed starting point).

        //Analysis:
        //Time: O(n). We loop through all gas stations.
        //Space: O(1). There is no data structures used.

        //Edge cases:
        //None

        
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
    }
}
