namespace LeetCodeSolver
{
    class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    class LinkedListProblems
    {
        //Name: Reorder List

        //Description: Given a singly linked list L: L0→L1→…→Ln-1→Ln,
        //reorder it to: L0→Ln→L1→Ln-1→L2→Ln-2→…

        //Approach: Chop the linked list into two parts using Tortoise and Hare method
        //Reverse the right park and insert the left and right node in alternate order

        //Analysis: Time: O(n), n = length of list. We chop it twice, reverse the right and insert in alternate order < O(n)
        //Space: O(1). All the ListNode, by themselves, are O(1).

        //Edge cases: Head node might be null
        public void ReorderList(ListNode head)
        {
            if (head == null) return;
            ListNode mid = head, end = mid.next;
            while (end != null && end.next != null)
            {
                mid = mid.next;
                end = end.next.next;
            }
            ListNode left = head, right = mid.next;
            mid.next = null;

            right = reverseList(right);

            head = new ListNode();

            ListNode curr = head;

            while (left != null || right != null)
            {
                if (left != null)
                {
                    curr.next = left;
                    curr = curr.next;
                    left = left.next;
                }
                if (right != null)
                {
                    curr.next = right;
                    curr = curr.next;
                    right = right.next;
                }

            }
            head = head.next;
        }
        //Name: Reverse List

        //Description: Given a linkedlist represents by a head node, reverse the linkedlist

        //Approach: This is a visualization of how we work:
        // Prev (null) -> Curr -> Next
        // Curr -> Prev(null) | Next
        // Prev -> null | Curr

        // Prev -> null | Curr -> Next
        // Curr -> Prev -> null | Next
        // Prev -> i -> null | Next
        //Prev -> i -> null | Curr

        //As one might observer, we are just tossing the next node to the beginning of a new list and swap variables to go backward

        //Analysis: Time: O(n), n = length of list. The algorithm ends when curr, or next, becomes null, signals the end of the list
        //Space: O(1). All the ListNode variables are O(1).
        //Edge cases: Should be none.
        ListNode reverseList(ListNode head)
        {
            ListNode prev = null, curr = head, next;
            while (curr != null)
            {
                next = curr.next;
                curr.next = prev;
                prev = curr;
                curr = next;
            }
            head = prev;
            return head;
        }
        //Name: Sort List

        //Description: Sort a linked list, plz

        //Approach: We can't do quicksort because we don't have O(1) access
        //We can't do heapsort either because we can't swap element since O(n) access
        //However, we can merge sort since merge travels iteratively through 2 lists
        //Implement merge sort. Divide the list by two by finding the middle and setting the middle's next to null
        //The rest should be standard merge sort.

        //Analysis: 
        //Time: O(n log n). It chop the array into log n number of subarrays and merge all subarrays in O(n) time.
        //Space: O(n). The call stack merge upwards and doesn't have every call at once. It executes each branch and merge upward.
        //Link: https://stackoverflow.com/questions/10342890/merge-sort-time-and-space-complexity

        //Edge cases: There's a lot of recursion. Use base cases when node == null. Also catch when head.next == null.

        public ListNode SortList(ListNode head)
        {
            if (head == null || head.next == null)
                return head;
            ListNode middle = getMiddle(head);
            ListNode nextmid = middle.next;

            middle.next = null;

            ListNode left = SortList(head);

            ListNode right = SortList(nextmid);

            return sortedMerge(left, right);
        }
        //Find the middle of the linkedlist
        //Have a fast node that travels to the end of list and a slow node travels at half speed
        //When fast reaches end, slow reaches middle.
        public ListNode getMiddle(ListNode head)
        {
            if (head == null)
                return head;
            ListNode fast = head.next;
            ListNode slow = head;
            while (fast != null)
            {
                fast = fast.next;
                if (fast != null)
                {
                    fast = fast.next;
                    slow = slow.next;
                }
            }
            return slow;
        }
        ListNode sortedMerge(ListNode a, ListNode b)
        {
            ListNode result = null;
            if (a == null)
                return b;
            if (b == null)
                return a;

            if (a.val <= b.val)
            {
                result = a;
                result.next = sortedMerge(a.next, b);
            }
            else
            {
                result = b;
                result.next = sortedMerge(a, b.next);
            }
            return result;
        }
        //Name: Linked List Cycle II

        //Description: Given a linked list, return where the cycle begins. If no cycle, return -1

        //Approach: We use rabbit-and-hare, a fast and slow pointer
        //The fast pointer will travel 2x nodes while the slow pointer travel x nodes
        //Let n be length of cycle, a as the index of cycle's start, x as # of node slow pointer goes
        //2x = a+n+(x-a), x-a due to the fact that the fast pointer goes to node a after the end instead of node 0.
        //x = n. If m is the length of the entire LL, m = n+b = x+b, with b = # of node from x to end of LL & beginning of cycle & # of node from head to beginning of cycle
        //Have another pointer from head. Walk alongside slow pointer b nodes until 2 met. Return b.

        //Analysis:
        //Time: O(n). We goes x nodes, x<n, and we goes to the end & beginning of cycle.
        //Space: O(1). There's no data structure used.

        //Edge case: If there's no cycle, make sure the rabbit doesn't run into null node.
        public ListNode DetectCycle(ListNode head)
        {
            ListNode rabbit = head;
            ListNode hare = head;
            ListNode seeker = head;
            while(rabbit!= null && rabbit.next != null)
            {
                rabbit = rabbit.next.next;
                hare = hare.next;
                if(rabbit == hare)
                {
                    while(seeker!=hare)
                    {
                        seeker = seeker.next;
                        hare = hare.next;
                    }
                    return seeker;
                }
            }
            return null;
        }
    }
}
