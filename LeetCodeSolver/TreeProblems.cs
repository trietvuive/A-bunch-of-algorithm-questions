using System.Collections.Generic;

namespace LeetCodeSolver
{
    class TreeProblems
    {
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(TreeNode left = null, TreeNode right = null, int val = 0)
            {
                this.left = left;
                this.right = right;
                this.val = val;
            }
        }
        //Name: All Elements in Two Binary Search Tree

        //Description: Given two binary search trees, return a sorted list of all elements in two trees

        //Approach: Inorder traversal and create 2 lists for each tree.
        //This will create 2 sorted lists. Merge these 2 sorted lists to the main list. Return the main list.

        //Analysis:
        //Time: O(m+n), with m and n as two tree's length. Inorder traversal and merge took O(n)
        //Space: O(m+n). The main list will have the size of m+n.

        //Edge cases: None
        public IList<int> GetAllElements(TreeNode root1, TreeNode root2)
        {
            List<int> list = new List<int>();
            List<int> one = new List<int>();
            List<int> two = new List<int>();
            Stack<TreeNode> s = new Stack<TreeNode>();
            TreeNode root = root1;
            while (s.Count != 0 || root != null)
            {
                if (root != null)
                {
                    s.Push(root);
                    root = root.left;
                }
                else
                {
                    TreeNode obj = s.Pop();
                    one.Add(obj.val);
                    root = obj.right;
                }
            }
            root = root2;
            while (s.Count != 0 || root != null)
            {
                if (root != null)
                {
                    s.Push(root);
                    root = root.left;
                }
                else
                {
                    TreeNode obj = s.Pop();
                    two.Add(obj.val);
                    root = obj.right;
                }
            }
            int i = 0;
            int j = 0;
            int m = one.Count;
            int n = two.Count;
            while (i < m && j < n)
            {
                if (one[i] < two[j])
                {
                    list.Add(one[i]);
                    i++;
                }
                else
                {
                    list.Add(two[j]);
                    j++;
                }
            }

            while (i < m)
            {
                list.Add(one[i]);
                i++;
            }

            while (j < n)
            {
                list.Add(two[j]);
                j++;
            }
            return list;
        }


    }

}
