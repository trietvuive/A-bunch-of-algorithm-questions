using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCodeSolver
{
    class BFSBacktracking
    {
        //Name: Largest Time for Given Digits

        //Description: Given 4 digits, construct the largest 24 hour time possible (00:00 -> 23:59)

        //Approach: It's a backtracking problem.
        //Sort the array to ensure we get the largest time on the first run.
        //Use a for loop to try and append each int to string. Check if string is in 24 hours format. If not skip
        //If it is in 24 hours format, marks the number it used as -1 so that the method won't use it again
        //Call recursively another method with the string that pass format test and the marked array. Try append, test format, and call again
        //If none of the string tried is in 24 hours format, return blank string. The recursion should also handles the blank string
        //and interpret it as no solution for that string, even though it is in correct format. The recursion method will skip to next iteration
        //Base case is when string's length is 5 and in correct format. If yes, return all the way to calling method.
        //Base case isn't when string's length is 2. When string's length is 2, add a : and call another method to handle the new string.

        //Analysis:
        //Time: O(n!), n = length of string for worst case. Worst case is when the string fails format test at the last digit every time.
        //Most of the time, the format test should detect faulty string and not examining it. We don't test all
        //possible combination at the end; rather we test string as we append digit to it.
        //Space: O(n), n = length of string. Calling stack's depth increase by 1 every time we append another digit to string and call the recursive method
        //Calling stack will free up memory if the string fails format test and return blank string.

        //Edge cases: None
        public static string LargestTimeFromDigits(int[] A)
        {
            Array.Sort(A);
            StringBuilder s = new StringBuilder();
            return RecurHour(A, s).ToString();

        }
        public static StringBuilder RecurHour(int[] A, StringBuilder recurs)
        {
            if (recurs.Length == 2) return RecurHour(A, recurs.Append(":"));
            if (recurs.Length == 5 && isValid(recurs))
                return recurs;
            for (int i = A.Length - 1; i > -1; i--)
            {
                if (A[i] != -1)
                {
                    StringBuilder temps = new StringBuilder(recurs.ToString());
                    temps.Append(A[i].ToString());
                    if (isValid(temps))
                    {
                        int[] temp = (int[])A.Clone();
                        temp[i] = -1;
                        StringBuilder build = RecurHour(temp, temps);
                        if (build.Length != 0)
                            return build;
                    }
                }
            }
            return new StringBuilder();

        }
        public static bool isValid(StringBuilder time)
        {
            if (time.Length > 0 && time[0] > '2')
                return false;
            if (time.Length > 1 && time[0] == '2' && time[1] > '3')
                return false;
            if (time.Length > 3 && time[3] > '5')
                return false;
            if (time.Length > 4 && time[4] > '9')
                return false;
            return true;
        }
        //Name: Sequential Digits

        //Description: Return all sequences (sequences defined as number with consecutive digits (i.e 12345))
        //that is >= low and <= high. Return a sorted list of these sequences.

        //Approach: We can do recursive, but it doesn't guarantee to be sorted
        //For it to be sorted, we must examine all numbers with less digit before moving on to more digits
        //We can do this using a queue. Put the longer sequence at the end of the queue to ensure that we will examine all less-digit sequences before it
        //Use a queue and add the number to list when it falls within low and high
        //Otherwise, as long as it's smaller than high and last digit isn't 9 (because we can't do 7890), enqueue *= 10 + lastdigit + 1
        //Loop until the queue is empty

        //Analysis:
        //Time: O(n)
        //Space: O(9), since there would be maximum 9 elements in the queue as we remove and add 1 number at a time




        public static IList<int> SequentialDigits(int low, int high)
        {
            List<int> sequences = new List<int>();
            Queue<int> q = new Queue<int>();
            for (int i = 1; i < 10; i++)
            {
                q.Enqueue(i);
            }
            while (q.Count != 0)
            {
                int num = q.Dequeue();
                if (num >= low && num <= high)
                {
                    sequences.Add(num);
                }
                int lastdigit = num % 10;
                if (lastdigit != 9 && num < high)
                {
                    q.Enqueue(num * 10 + lastdigit + 1);
                }
            }
            return sequences;
        }
        //Name: Unique Path

        //Description: Return the number of 4-dimensional walks from the starting square to the end square
        //while walking over every non-obstacle square exactly once.

        //Approach: It's a backtracking problem.
        //Find the starting square, count all square that we need to walk over
        //Since we can only walk through square once, mark square that we walk in as an obstacle (-1)
        //If we reach obstacle square or out of bounds, return 0
        //If we reach target square and haven't walked through all square, return 0

        //Analysis:
        //Time: O(4^mn), m and n is width and height of matrix. It's a matrix and we branch out to 4 directions of the matrix.
        //Space: O(mn). Maximum depth of the call stack would be m*n, the size of the matrix, since the longest path would be the size of the matrix.

        //Edge cases: None.
        public int UniquePathsIII(int[][] grid)
        {
            int sx = -1;
            int sy = -1;
            int n = 1;//the starting square also counts toward n

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    if (grid[i][j] == 0) n++;
                    else if (grid[i][j] == 1)
                    {
                        sx = j;
                        sy = i;
                    }
                }
            }
            return UniquePathIIIDFS(grid, sx, sy, n);
        }
        public int UniquePathIIIDFS(int[][] grid, int x, int y, int n)
        {
            if (x < 0 || y < 0 || x == grid[0].Length || y == grid.Length || grid[y][x] == -1)
                return 0;
            if (grid[y][x] == 2) return n == 0 ? 1 : 0;
            grid[y][x] = -1;
            int paths = UniquePathIIIDFS(grid, x + 1, y, n - 1) +
                UniquePathIIIDFS(grid, x - 1, y, n - 1) +
                UniquePathIIIDFS(grid, x, y + 1, n - 1) +
                UniquePathIIIDFS(grid, x, y - 1, n - 1);
            grid[y][x] = 0;
            return paths;
        }
    }
}
