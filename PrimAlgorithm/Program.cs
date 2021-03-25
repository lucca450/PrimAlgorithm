using System;

namespace PrimAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Welcome to the mazes generator 2000 !!");
                bool ok;
                int rows = 0, columns = 0, minimumSize = 4;

                do
                {
                    Console.Write("Enter maze row (minimum 4): ");
                    try
                    {
                        rows = Int32.Parse(Console.ReadLine());
                        if (rows >= minimumSize)
                            break;

                        Console.WriteLine("Error - Rows must be higher or equal than " + minimumSize + " - Error");
                    }
                    catch
                    {
                        Console.WriteLine("Error - Rows must be an integer - Error");
                    }

                } while (true);
                do
                {
                    Console.Write("Enter maze column (minimum 4): ");
                    try
                    {
                        columns = Int32.Parse(Console.ReadLine());
                        if (columns >= minimumSize)
                            break;

                        Console.WriteLine("Error - Columns must be higher or equal than " + minimumSize + " - Error");
                    }
                    catch
                    {
                        Console.WriteLine("Error - Columns must be an integer - Error");
                    }

                } while (true);

                Maze maze = new Maze(rows, columns);

                do
                {
                    Console.Write("Enter maze start row : ");
                    try
                    {
                        rows = Int32.Parse(Console.ReadLine());
                        if (rows >= 1 && rows <= maze.rows)
                            break;

                        Console.WriteLine("Error - Rows must be part of the maze - Error");
                    }
                    catch
                    {
                        Console.WriteLine("Error - Rows must be an integer - Error");
                    }

                } while (true);
                do
                {
                    Console.Write("Enter maze start column : ");
                    try
                    {
                        columns = Int32.Parse(Console.ReadLine());
                        if (maze.VerifyStartCoordinates(rows, columns))
                            break;

                        Console.WriteLine("Error - Columns must be part of the maze - Error");
                    }
                    catch
                    {
                        Console.WriteLine("Error - Rows must be an integer - Error");
                    }

                } while (true);

                Console.WriteLine("\nBefore generating the maze, do you want the procedure to be printed in the console ? (yes/no)");
                string answer = Console.ReadLine();
                Utilities.displayProcedure = answer.ToLower().Equals("yes");

                maze.Generate(rows - 1, columns - 1);

                Console.WriteLine("Maze initialized in " + Utilities.initializationCount + " operations :\n");
                Console.WriteLine("Maze generated in " + Utilities.generationCount + " operations :\n");

                maze.toString();
                Console.WriteLine("Maze displayed in " + Utilities.displayCount + " operations :\n");

                Console.WriteLine("Nodes with links : \n");
                maze.FullMaze();
            } while (true);
        }
    }
}
