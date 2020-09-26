using System.Security.Cryptography;

namespace LeetCodeSolver
{
    class MiscellaneousProblems
    {
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
