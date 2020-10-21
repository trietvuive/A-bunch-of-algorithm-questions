using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LeetCodeSolver
{
    class GraphProblems
    {
        public class Node
        {
            public int val;
            public IList<Node> neighbors;
            public Node()
            {
                val = 0;
                neighbors = new List<Node>();
            }
            public Node(int _val)
            {
                val = _val;
                neighbors = new List<Node>();
            }
            public Node(int _val, List<Node> _neighbors)
            {
                val = _val;
                neighbors = _neighbors;
            }
        }
        //Name: Clone Graph
        
        //Description: Give a graph, return another graph with clone value. 

        //Approach: Trace every node with a dictionary/hash map.
        //Visit every neighbors of each node and look at the dictionary to see if it was explored or not
        //Explore all neighbors of a node and add them to that node, then explore all the neighbors through a queue

        //Analysis:
        //Time: O(k), k = number of edges
        //Space: O(n), n = number of vertices

        //Edge case: node can be null
        public Node CloneGraph(Node node)
        {
            if (node == null)
                return node;
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(node);
            Dictionary<Node, Node> clonedict = new Dictionary<Node, Node>();
            clonedict.Add(node, new Node(node.val));
            while (q.Count() != 0)
            {
                Node u = q.Dequeue();
                Node cloneU = clonedict[u];
                foreach (Node neighbor in u.neighbors)
                {
                    if (!clonedict.ContainsKey(neighbor))
                    {
                        q.Enqueue(neighbor);
                        Node neighborclone = new Node(neighbor.val);
                        clonedict.Add(neighbor, neighborclone);
                        cloneU.neighbors.Add(neighborclone);
                    }
                    else
                        cloneU.neighbors.Add(clonedict[neighbor]);
                }
            }
            return clonedict[node];
        }
    }
}
