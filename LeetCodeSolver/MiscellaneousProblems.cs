using System.Security.Cryptography;

namespace LeetCodeSolver
{
    class MiscellaneousProblems
    {
        //Name: Gas Station

        //Description: There is a circular route with N gas stations. Each station has certain gas unit
        //To reach other stations, it also costs certain gas unit.
        //Given an array of gas station's gas units and cost of reaching them, return whether a car with unlimited gas tank can go full circle

        //Approach:
        //
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
