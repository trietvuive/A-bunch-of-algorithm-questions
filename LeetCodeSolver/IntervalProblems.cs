using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeSolver
{
    class IntervalProblems
    {
        //Name: Image Overlap

        //Description: Given two images represented as binary (0s and 1s),
        //move images so that we have the most overlap, with overlap defined as when both images have 1 in the same place
        //Return the number of overlap

        //Approach: Since only 1s overlap, we only care about the coordinate of 1 in both images. Use a list to store their coordinates
        //In order to move so that we get the most overlap, we must find the offset of all coordinates of 1 between both images (Cartesian products)
        //and find the most frequent offset
        //We will use a dictionary to record the offset and how many time it occurs. Return the largest amount of occurences

        //Analysis:
        //Time: O(n*n). We are given matrices, so we loop through both matrices, find 1 and loop through them again to find the Cartesian products
        //Space: O(n*n) worst case. Worst case is when both images have all 1s and we have to record all coordinates in our list

        //Edge cases: If you're using map.Values.Max, beware that if map is empty, this method won't return 0 but rather throw an error.
        public int LargestOverlap(int[][] A, int[][] B)
        {
            int n = A.Length;
            List<int> A1 = new List<int>();
            List<int> B1 = new List<int>();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (A[i][j] == 1)
                        A1.Add(i * 100 + j);
                    if (B[i][j] == 1)
                        B1.Add(i * 100 + j);

                }
            }
            Dictionary<int, int> offSetCount = new Dictionary<int, int>();
            foreach (int a in A1)
            {
                foreach (int b in B1)
                {
                    int offset = a - b;
                    if (offSetCount.ContainsKey(offset)) offSetCount[offset]++;
                    else
                        offSetCount.Add(offset, 1);
                }
            }
            return offSetCount.Count > 0 ? offSetCount.Values.Max() : 0;
        }
        //Name: Interval List Intersection

        //Description: Given two sorted and disjoint intervals, return their intersection

        //Approach: Simply loop through both intervals (with separate index) and add to list when they intersect
        //The code below has comment with precondition and what's happening

        //Analysis:
        //Time: O(min(n,m)). We loop through both intervals until one interval ran out, because there will be no intersection
        //with only one interval
        //Space: O(n). We use a list instead of directly inserting into a jagged array, which might have been a mistake

        //Edge cases: None really, as long as you handle start/end of each interval correctly
        public int[][] IntervalIntersection(int[][] A, int[][] B)
        {
            List<List<int>> intersection = new List<List<int>>();
            int aindex = 0;
            int bindex = 0;
            while (aindex < A.Length && bindex < B.Length)
            {
                int[] intA = A[aindex];
                int[] intB = B[bindex];
                //A start before B
                if (intA[0] < intB[0])
                {
                    //A end before B start. No intersection. Increment A
                    if (intA[1] < intB[0])
                        aindex++;
                    else
                    //A end at or after B start. Intersection at start of B and min(B's end, A's end)
                    {
                        intersection.Add(new List<int> { intB[0], Math.Min(intB[1], intA[1]) });
                        //if A ends before B ends, increment A. otherwise increment B
                        if (intA[1] < intB[1])
                            aindex++;
                        else
                            bindex++;
                    }
                }
                //B starts at or before A starts
                else
                {
                    //B ends before A start. No intersection. Increment B
                    if (intB[1] < intA[0])
                        bindex++;
                    //B ends at or after A start. Intersection at A start to min(B's end, A's end)
                    else
                    {
                        intersection.Add(new List<int> { intA[0], Math.Min(intB[1], intA[1]) });
                        //if A ends before B ends, increment A. Otherwise increment B
                        if (intA[1] < intB[1])
                            aindex++;
                        else
                            bindex++;
                    }
                }
            }
            int[][] jag = new int[intersection.Count][];
            for (int i = 0; i < intersection.Count; i++)
            {
                jag[i] = intersection[i].ToArray();
            }
            return jag;
        }
        //Name: Insert Interval

        //Description: Given a list of sorted non-overlapping intervals, insert a new interval and merge if necessary

        //Approach: Loop through the list
        //Insert intervals[i] if intervals[i]<newInterval
        //Insert newInterval if intervals[i-1]<newInterval<intervals[i]
        //Merge if they overlap. Remember that one or more intervals can overlap with newInterval.
        //To merge, create an inner while loop as long as newInterval overlaps with intervals[i]
        //Store the mininum start time and maximum end time as you go. Insert this new interval
        //into the return list and resume from the position where the inner loop ends

        //Analysis:
        //Time: O(n), n = numbers of intervals. We loop once through all intervals even though we have an inner while loop
        //since they use the same i, so inner loop + outer loop = i
        //Space: O(n), n = number of intervals. We use a temporary list to store the new intervals

        //Edge cases: Watch for newInterval inserted at start, end, and merging over multiple intervals.
        public int[][] Insert(int[][] intervals, int[] newInterval)
        {
            List<int[]> interval = new List<int[]>();
            int i = 0;
            int prevCompare = -1;//assume that newInterval is larger than intervals[0]
            //if not, we will insert it to position 0
            //also use to detect change from interval smaller than newInterval to interval larger than newInterval for insertion
            while (i < intervals.Length)
            {
                int compare = Compare(intervals[i], newInterval);
                if (compare == -1)
                    interval.Add(intervals[i]);
                if (compare == 0)
                {
                    //intervals[i[ overlap with inserted intervals. Start merging
                    prevCompare = 0;
                    int end = int.MinValue;
                    int start = int.MaxValue;
                    while (i < intervals.Length && Compare(intervals[i], newInterval) == 0)
                    {
                        start = Math.Min(start, Math.Min(intervals[i][0], newInterval[0]));
                        end = Math.Max(end, Math.Max(intervals[i][1], newInterval[1]));
                        i++;
                    }
                    interval.Add(new int[2] { start, end });
                    continue;
                }
                if (compare == 1)
                {
                    if (prevCompare == -1)
                        interval.Add(newInterval);
                    interval.Add(intervals[i]);
                    prevCompare = 1;
                }
                i++;
            }
            if (prevCompare == -1)
                interval.Add(newInterval);
            return interval.ToArray();
        }
        public int Compare(int[] interval1, int[] interval2)
        {
            if (interval1[0] > interval2[1]) return 1;
            if (interval2[0] > interval1[1]) return -1;
            return 0;
        }
        //Name: Car Pooling

        //Description: A bus is driving straight and taking in passenger and releasing them as it goes.
        //Given the bus's capacity and all the passenger it must pick along its way (at certain points), return whether the bus will exceed
        //its capactiy at any point

        //Approach: 
        //Create a bucket of all location in trips. Record the change in amount of passengers for each location in the bucket
        //Loop through the bucket and keep a count of passenger. If this count exceed capacity, return false. Else return true

        //Analysis:
        //Time: O(k), k is the maximum element in the trips array (the furthest destination). We loop until the end, hence k.
        //Space: O(k), since we store a bucket of all locations until the last location.
        //Note: This algorithm can degrade badly if the trips become longer. Consider sorting interval as an alternative

        //Edge cases: None.
        public bool CarPooling(int[][] trips, int capacity)
        {
            int max = 0;
            foreach (int[] i in trips)
            {
                max = max > i[2] ? max : i[2];
            }
            int[] bucket = new int[max + 1];
            foreach (int[] i in trips)
            {
                bucket[i[1]] += i[0];
                bucket[i[2]] -= i[0];
            }
            int sum = 0;
            for (int i = 0; i < bucket.Length; i++)
            {
                sum += bucket[i];
                if (sum > capacity) return false;
            }
            return true;
        }
    }

}