using System;
using System.Collections.Generic;

namespace Eight_Puzzle___BFS
{
    class Node
    {
        //Set up a "graph"
        public List<Node> children = new List<Node>();  // Child Nodes
        public Node parent;                             // Parent Node
        const int elements = 9;                         // Elements on Matrix
        public int[] puzzle = new int[elements];        // Matrix
        public int columns = 3;
        public int x = 0;
        private int pattern;

        public Node(int[] puzzle, int pattern)
        {
            this.pattern = pattern;
            setPuzzle(puzzle);
        }

        //You know... Matrix set up...
        public void setPuzzle(int[] puzzle)
        {
            for (int i = 0; i < puzzle.Length; i++)
                this.puzzle[i] = puzzle[i];
        }

        public void expandNode()
        {
            for(int i = 0; i < puzzle.Length; i++)
                if (puzzle[i] == 0)
                {
                    x = i;  // Position of '0' - empty place
                    break;
                }        

            moveDown(puzzle, x);
            moveUp(puzzle, x);
            moveLeft(puzzle, x);
            moveRight(puzzle, x);
        }

        //Check if puzzles are already sorted
        public bool checkIfDone()
        {
            if(pattern == 1)
            {
                //Here we are checking if the order is like: 0, 1, 2, 3, 4, etc...
                for (int i = 1; i < this.puzzle.Length; i++)
                {
                    if (puzzle[0] != 0)             //0, X, X, X, X
                        return false;
                    if (puzzle[i - 1] > puzzle[i])  //Not sorted? Break.
                        return false;
                }
                return true;
            }

            if (pattern == 2)
            {
                //Here we are checking if the order is like: 1, 2, 0, 3, 4, etc...
                for (int i = 1; i < this.puzzle.Length; i++)
                {
                    if (i < 3 && puzzle[i - 1] != i)  //1, 2, X, X, X, ...
                        return false;
                    if (i == 3 && puzzle[2] != 0)     //X, X, 0, X, X, ...
                        return false;
                    if (i > 3 && puzzle[i] != i)      //X, X, X, 3, 4, ...
                        return false;
                }
                return true;
            }

            if (pattern == 3)
            {
                //Here we are checking if the order is like: 1, 2, etc..., 0, 7, 8
                for (int i = 1; i < this.puzzle.Length; i++)
                {
                    if (i < 7 && puzzle[i - 1] != i)  //1, 2, X, X, X, ...
                        return false;
                    if (i == 7 && puzzle[6] != 0)     //X, X, 0, X, X, ...
                        return false;
                    if (i > 7 && puzzle[i] != i)      //X, X, X, 3, 4, ...
                        return false;
                }
                return true;
            }

            else
            {
                //Here we are checking if the order is like: 1, 2, 3, etc..., 8, 0
                for (int i = 1; i < this.puzzle.Length; i++)
                {
                    if (puzzle[8] != 0)      //X, X, X, X, 0
                        return false;
                    if (i != 8 && puzzle[i - 1] > puzzle[i])  //1, 2, X, X, X, ...
                        return false;
                }
                return true;
            }

        }

        #region Available Moves on Matrix
        public void moveRight(int[] puzzle, int i)
        {
            if (i % columns < (columns - 1))
            {
                //Create a COPY of a puzzle
                int[] possibleMovePuzzle = new int[elements];
                copyPuzzle(puzzle, possibleMovePuzzle);

                //Move a brick on a COPIED puzzle
                Swap(ref possibleMovePuzzle[i], ref possibleMovePuzzle[i + 1]);

                Node child = new Node(possibleMovePuzzle, pattern);
                children.Add(child);
                child.parent = this;
            }
        }

        public void moveLeft(int[] puzzle, int i)
        {
            if (i % columns > 0)
            {
                //Create a COPY of a puzzle
                int[] possibleMovePuzzle = new int[elements];
                copyPuzzle(puzzle, possibleMovePuzzle);

                //Move a brick on a COPIED puzzle
                Swap(ref possibleMovePuzzle[i], ref possibleMovePuzzle[i - 1]);

                Node child = new Node(possibleMovePuzzle, pattern);
                children.Add(child);
                child.parent = this;
            }
        }

        public void moveUp(int[] puzzle, int i)
        {
            if (i - columns >= 0)
            {
                //Create a COPY of a puzzle
                int[] possibleMovePuzzle = new int[elements];
                copyPuzzle(puzzle, possibleMovePuzzle);

                //Move a brick on a COPIED puzzle
                Swap(ref possibleMovePuzzle[i], ref possibleMovePuzzle[i - 3]);

                Node child = new Node(possibleMovePuzzle, pattern);
                children.Add(child);
                child.parent = this;
            }
        }

        public void moveDown(int[] puzzle, int i)
        {
            if (i + columns < puzzle.Length)
            {
                //Create a COPY of a puzzle
                int[] possibleMovePuzzle = new int[elements];
                copyPuzzle(puzzle, possibleMovePuzzle);

                //Move a brick on a COPIED puzzle
                Swap(ref possibleMovePuzzle[i], ref possibleMovePuzzle[i + 3]);

                Node child = new Node(possibleMovePuzzle, pattern);
                children.Add(child);
                child.parent = this;
            }
        }
        #endregion

        public void printPuzzle()
        {
            Console.WriteLine();
            int position = -1;
            for(int i = 0; i < columns; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    if(puzzle[++position] == 0)
                        Console.Write("* ");
                    else
                        Console.Write(puzzle[position] + " ");
                }
                Console.WriteLine();
            }
        }

        public bool isSamePuzzle(int[] checkingPuzzle)
        {
            for (int i = 0; i < puzzle.Length; i++)
                if (puzzle[i] != checkingPuzzle[i])
                    return false;
            return true;
        }

        public void copyPuzzle(int[] originalPuzzle, int[] newPuzzle)
        {
            Array.Copy(originalPuzzle, newPuzzle, originalPuzzle.Length);
        }

        void Swap<T>(ref T A, ref T B) {
            T temp = A;
            A = B;
            B = temp;
        }
    }
}
