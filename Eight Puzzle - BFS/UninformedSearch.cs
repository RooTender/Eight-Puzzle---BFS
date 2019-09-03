using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eight_Puzzle___BFS
{
    class UninformedSearch
    {
        public List<Node> BFS(Node root)    // <-- root already known
        {
            //Set up all the lists of nodes...
            List<Node> PathToSolution = new List<Node>();   // Solution Path
            List<Node> OpenList = new List<Node>();         // InQueue nodes
            List<Node> ClosedList = new List<Node>();       // Visited nodes

            //Set up the ROOT
            OpenList.Add(root);     // <-- adding root
            bool goalFound = false;

            while(OpenList.Count > 0 && !goalFound)
            {
                // Current NODE <= InQueue first NODE
                Node currentNode = OpenList[0];

                // Add to visited that node & remove from InQueue
                ClosedList.Add(currentNode);
                OpenList.RemoveAt(0);

                // Check ALL possible moves
                currentNode.expandNode();

                for(int i = 0; i < currentNode.children.Count; i++)
                {
                    // Child of node declaration
                    Node currentChild = currentNode.children[i];

                    // Work done?
                    if (currentChild.checkIfDone())
                    {
                        Console.WriteLine("Solution found!");
                        goalFound = true;

                        //Trace the path
                        PathTrace(PathToSolution, currentChild);
                    }

                    // New node?
                    if(!Contains(OpenList, currentChild) && !Contains(ClosedList, currentChild))
                    {
                        OpenList.Add(currentChild);
                    }
                }
            }
            return PathToSolution;
        }

        public void PathTrace(List<Node> path, Node n)
        {
            Console.WriteLine("Tracing path...");
            Node current = n;
            path.Add(current);

            while(current.parent != null)
            {
                current = current.parent;
                path.Add(current);
            }
        }

        public static bool Contains(List<Node> list, Node nodeToCompare)
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i].isSamePuzzle(nodeToCompare.puzzle))
                    return true;

            return false;
        }
    }
}
