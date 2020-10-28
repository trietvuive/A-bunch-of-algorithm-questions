using System;

namespace LeetCodeSolver
{
    class DynamicProgramming
    {
        //Name: House Robber

        //Description: Given an array, find a non-consecutive subsequence with the max sum and return the sum
        //Non-consecutive subsequence means all number in the array must not be close to each other (must have elements in between)

        //Approach: Dynamic programming is dividing problems into sub-problems.
        //In order to determine if we should rob house n, we compare robbing vs not robbing house n
        //total of robbing house n is total of robbing house n-2  (i.e not robbing house n-1) + value of house n
        //total of not robbing house n is total of robbing house n-1
        //compare these 2 and put max money from rob into array
        //start from the beginning, robbing 2 houses is max(house1,house2),...

        //Note: the following algorithm can be optimized by just storing 3 values instead

        //Analysis: O(n), n = number of houses. We examine each house and determine if we should rob it or not
        //Space: O(n), can be O(1).

        //Edge cases: Watch out when there's only 1 or no house.
        public int Rob(int[] nums)
        {
            if (nums.Length == 0) return 0;
            if (nums.Length == 1) return nums[0];
            int robhouse = 0;
            int notrobhouse = 0;
            foreach (int i in nums)
            {
                int temp = robhouse;
                robhouse = Math.Max(notrobhouse + i, robhouse);
                notrobhouse = robhouse;
            }
            return robhouse;
        }
        //Name: Stone Game IV

        //Description: Bob and Alice played a game
        //Players will attempt to remove a non-zero square integer of rocks. If the rocks get to 0 whoever remove last win
        //Assume both player plays optimally and Alice goes first, return whether Alice will win the game

        //Approach: If Alice can remove n stones so that the number of stone become a number that is unwinnable, Alice will win
        //Then, this become multiple subproblems where we evaluate the winning condition of n stones by evaluating the winning condition of n-1, n-4, n-9,... stones
        //Start from 0 which is unwinnable. Loop until n stones and examine all winning condition of n-j*j stones. If one of the conditions is unwinnable, n stones is winning

        //Analysis:
        //Time: O(sigma 1->n n*sqr(n)). We loop from 1 to n stones and evaluate all subconditions that we have previously evaluated => O(1) process
        //Space: O(n), n = number of stones. We create an array to store the winning conditions at n stones.

        //Edge cases: None
        public static bool WinnerSquareGame(int n)
        {
            int[] dp = new int[n + 1];
            dp[0] = 0;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j * j <= i; j++)
                {
                    if (dp[i - j * j] == 0)
                    {
                        dp[i] = 1;
                        break;
                    }
                }
            }
            return true ? dp[n] == 1 : false;
        }
        //Name: Best Time to Buy And Sell Stocks with K transactions

        //Descriptions: Given an array of stock prices and k transactions, return the maximum profit by buying low and selling high

        //Approach:
        //We can divide this into subproblems: We solve max for 0 transaction, 1 transaction, 2 transactions,... k transactions
        //We can divide this into even smaller subproblems: We solve max for i transactions after 0 day, 1 day, 2 days,... prices.Length days
        //Note that $ is made from selling, not buying
        //At i transaction and j days, the max profit would be:
        //We don't sell our stocks. Max profit would be the same as max profit in i transactions and j-1 days
        //We sell our stocks. The max profit would be:
        //currentprice + max(max profit at i-1 transactions and j days - prices[j] with j from 1,2,... current days)
        //Examine it from bottom-up, from k = 1 transactions (we don't make money with 0 transaction) and j = 1 days up until the end
        //return the max at the end
        //Note: There is a possible optimization for examining max profit from selling stocks
        //The explanation is commented below

        //Analysis:
        //Time: O(k*n), k = number of transations and n = number of days. We build the whole max 2d array to determine the max profit
        //Space: O(k*n), k = number of transactions and n = number of days. The size of the 2D array is k*n.

        //Edge cases: None
        public int MaxProfit(int[] prices, int k)
        {
            k = Math.Min(k, prices.Length);
            if (prices.Length == 0)
                return 0;
            //Edge cases optimization
            //If we are allow transactions > prices.Length/2, we can just buy and sell the next day, assume positive days
            //There's no need to max profit because we have enough transactions to just buy and sell every single day
            //Note: We don't have enough transactions to buy and sell on multiple consecutive positive days with only k > prices.Length/2
            //However, we only return maxProfit, and if there's consecutive positive days, we would just buy at first and sell at last positive day.
            //The method caller doesn't need to know that we are actually buying and selling at the same day for multiple days =)
            if (k >= prices.Length / 2)
            {
                int profit = 0;
                for (int i = 1; i < prices.Length; i++)
                {
                    profit += Math.Max(0, prices[i] - prices[i - 1]);
                }
                return profit;
            }

            int[,] dp = new int[k + 1, prices.Length];
            for (int i = 1; i <= k; i++)
            {
                //We note that if selling stocks, the max profit would be max(dp[i-1,x] - prices[x] + prices[j]) with x from 0 to j
                //We also note that when we are looping through it, we are examining dp[i-1,x] - prices[x] multiple times as we increase j from 0 to days.Length-1
                //Instead, we can determine the max of dp[i-1,x] - prices[x] as we go so we don't examine it multiple times
                //The profit from selling, then, would be prices[j] + maxDiff with maxDiff = max(dp[i-1,x] - prices[x]) as we go
                int maxDiff = dp[i - 1, 0] - prices[0];
                for (int j = 1; j < prices.Length; j++)
                {
                    dp[i, j] = Math.Max(prices[j] + maxDiff, dp[i, j - 1]);
                    maxDiff = Math.Max(maxDiff, dp[i - 1, j] - prices[j]);
                }
            }
            return dp[k, prices.Length - 1];

        }
    }
}
