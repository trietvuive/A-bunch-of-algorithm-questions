using System;

namespace LeetCodeSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] hour = new int[] { 2, 0, 6, 6 };
            //Console.WriteLine(BFSBacktracking.LargestTimeFromDigits(hour));
            //int numerator = int.MinValue;
            //int denominator = 1;
            //Console.WriteLine(MathProblems.FractionToDecimal(numerator, denominator));
            //Console.WriteLine(CombinatoryProblems.CombinationSum3(3, 9));
            //Console.WriteLine(DynamicProgramming.WinnerSquareGame(4));
            Console.WriteLine(String.Join(", ", BFSBacktracking.SequentialDigits(100, 13000)));
        }
    }
}
