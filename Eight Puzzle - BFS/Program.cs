using System;
using System.Collections.Generic;

namespace Eight_Puzzle___BFS
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> steps = new List<string>(); 

            #region Menu
            Console.WriteLine("How do you want to solve this puzzle?\n");
            Console.WriteLine("1. * X X     2. X X *    3. X X X    4. X X X");
            Console.WriteLine("   X X X        X X X       X X X       X X X");
            Console.WriteLine("   X X X        X X X       * X X       X X *");
            Console.Write("\nChoose combination from 1-4: ");

            int pattern = Convert.ToInt32(Console.Read()) - 48;

            FlushBuffer();

            if (pattern < 0 || pattern > 4)
            {
                Console.WriteLine("Wrong or no pattern selected. Exiting");
                Console.Read();
                Environment.Exit(1);
            }
            
            Console.WriteLine("Enter your puzzle:\n");

            int[] puzzle = new int[9];

            for (int i = 0; i < 9; i++)
            {
                int temp = Console.Read() - 48;
                if (temp >= 0 && temp <= 9)
                {
                    puzzle[i] = temp;
                }
                else
                {
                    --i;
                    continue;
                }
            }

            Console.Clear();
            FlushBuffer();
            #endregion

            Node root = new Node(puzzle, pattern);
            UninformedSearch ui = new UninformedSearch();

            List<Node> solution = ui.BFS(root);

            if (solution.Count > 0)
            {
                for (int i = (solution.Count - 1); i >= 0; i--)
                {
                    solution[i].printPuzzle();

                    if (i < solution.Count - 1)
                        setSteps(solution[i], solution[i + 1]);
                }

                Console.WriteLine('\n');
                Console.WriteLine("Steps: " + steps.Count);

                for (int i = 0; i < steps.Count; i++)
                {
                    if (i % 4 == 0) Console.WriteLine();
                    Console.WriteLine(steps[i]);

                }
            }
            else
                Console.WriteLine("No solution found!");

            Console.Read();
            void setSteps(Node start, Node end)
            {
                for (int i = 0; i < start.columns; i++)
                {
                    // LEFT
                    if ((start.puzzle[i * 3 + 1] == 0 && end.puzzle[i * 3] == 0) ||
                        (start.puzzle[i * 3 + 2] == 0 && end.puzzle[i * 3 + 1] == 0))
                    {
                        steps.Add("LEFT");
                        break;
                    }

                    // RIGHT
                    if ((    start.puzzle[i * 3] == 0 && end.puzzle[i * 3 + 1] == 0) ||
                        (start.puzzle[i * 3 + 1] == 0 && end.puzzle[i * 3 + 2] == 0))
                    {
                        steps.Add("RIGHT");
                        break;
                    }

                    // UP
                    if ((start.puzzle[i + 3] == 0 && end.puzzle[i] == 0) ||
                        (start.puzzle[i + 6] == 0 && end.puzzle[i + 3] == 0))
                    {
                        steps.Add("UP");
                        break;
                    }

                    // DOWN
                    if ((    start.puzzle[i] == 0 && end.puzzle[i + 3] == 0) ||
                        (start.puzzle[i + 3] == 0 && end.puzzle[i + 6] == 0))
                    {
                        steps.Add("DOWN");
                        break;
                    }
                }
            }
        }

        static void FlushBuffer()
        {
            while (Console.In.Peek() != -1)
                Console.In.Read();
        }
    }
}
