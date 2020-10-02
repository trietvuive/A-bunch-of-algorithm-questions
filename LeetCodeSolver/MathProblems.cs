using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeetCodeSolver
{
    class MathProblems
    {
        //Name: Fraction to Recurring Decimal

        //Description: Given a numerator and denominator, return the result of the division.
        //If the decimal is recurring, put parentheses around the recurring part

        //Approach: Obviously the division isn't the difficult part, the recurring part is
        //When we do decimal division, we divide, get remainder, remainder *= 10 until remainder > denominator, and divide again
        //Notice that the recurring part would never begin with or end with 0.
        //Then, everytime we divide remainder by denominator, we store the remainder and its index in a dictionary
        //If we found the remainder already in the dictionary, that means there is a recurring part.
        //Break the loop, find the index of the beginning of the recurring part through map, put parenthese and return

        //Analysis:
        //Time: O(k), with k as the length of the recurring part. There's no way to determine how long the recurring part is
        //although it can be estimated to be around log(denominator)
        //Space: O(n). We use the map to track denominator and find pattern/recurring part.

        //Edge cases: All math problems should convert integer to long to avoid int.max and int.min
        //Also, we need to handle negative division.
        public static string FractionToDecimal(int numerator, int denominator)
        {
            long n = Math.Abs((long)numerator);
            long d = Math.Abs((long)denominator);
            StringBuilder res = new StringBuilder();
            if ((numerator > 0 && denominator < 0) || (numerator < 0 && denominator > 0))
                res.Append("-");
            long whole = n / d;
            res.Append(whole.ToString());
            long remainder = n % d;
            if (remainder == 0) return res.ToString();
            StringBuilder rem = new StringBuilder();
            bool repeat = false;
            Dictionary<long, int> remaindermap = new Dictionary<long, int>();
            remaindermap.Add(remainder, 0);
            int i = 0;
            while (remainder != 0)
            {
                i++;
                remainder *= 10;
                if (remainder > d)
                {
                    rem.Append((remainder / d).ToString());
                    remainder = remainder % d;
                    if (remaindermap.ContainsKey(remainder))
                    {
                        repeat = true;
                        break;
                    }
                    remaindermap.Add(remainder, i);
                }
                else
                {
                    rem.Append("0");
                }
            }
            if (repeat)
            {
                rem.Insert(remaindermap[remainder], "(");
                rem.Append(")");
            }
            res.Append(".");
            res.Append(rem);
            return res.ToString();
        }
        //Name: Largest Number

        //Description: Given a list of integer, return the largest integer (string representation) that can be formed

        //Approach: 
        //To form the largest integer, the largest combination must be in front. This implies some kind of sorting order.
        //To determine the ranking of string, determine their combination order. If a+b is larger (as an integer) than b+a, then a should be in front of b in the array
        //This can be done with ordinal ranking. Ordinal = comparing string on their ascii value, and larger digit has larger ascii.

        //Analysis:
        //Time: O(n log n). The most time-consuming operation is sorting, which is O(n log n)
        //Space: O(n). We convert the integer array to string array to compare them as string.

        //Edge cases: Leading zeros. Note that integer must be converted to string to avoid integer overflow.
        public string LargestNumber(int[] nums)
        {
            var strNums = nums.Select(i => i.ToString()).ToArray();
            Array.Sort(strNums, (a, b) => String.Compare(b + a, a + b, StringComparison.Ordinal));
            StringBuilder sb = new StringBuilder();
            foreach (string i in strNums)
                sb.Append(i);
            string ans = sb.ToString().TrimStart('0');
            return ans == "" ? "0" : ans;
        }
    }
}
