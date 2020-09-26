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
    }
}
