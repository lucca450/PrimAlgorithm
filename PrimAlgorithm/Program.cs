using System;

namespace PrimAlgorithm
{
    class Program
    {
        static void Main(string[] args)
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
                    if(rows >= minimumSize)
                        ok = true;
                    else
                    {
                        ok = false;
                        Console.WriteLine("Error - Rows must be higher or equal than "+ minimumSize + " - Error");
                    }
                }
                catch
                {
                    ok = false;
                    Console.WriteLine("Error - Rows must be an integer - Error");
                }

            } while (!ok);
            do
            {
                Console.Write("Enter maze column (minimum 4): ");
                try
                {
                    columns = Int32.Parse(Console.ReadLine());
                    if(columns >= minimumSize)
                        ok = true;
                    else
                    {
                        ok = false;
                        Console.WriteLine("Error - Columns must be higher or equal than " + minimumSize + " - Error");
                    }
                }
                catch
                {
                    ok = false;
                    Console.WriteLine("Error - Columns must be an integer - Error");
                }

            } while (!ok);

            Maze maze = new Maze(rows, columns);

            do
            {
                Console.Write("Enter maze start row : ");
                try
                {
                    rows = Int32.Parse(Console.ReadLine());
                    if (rows >= 1 && rows <= maze.columns)
                        ok = true;
                    else
                    {
                        ok = false;
                        Console.WriteLine("Error - Rows must be part of the maze - Error");
                    }
                }
                catch
                {
                    ok = false;
                    Console.WriteLine("Error - Rows must be an integer - Error");
                }

            } while (!ok);
            do
            {
                Console.Write("Enter maze start column : ");
                try
                {
                    columns = Int32.Parse(Console.ReadLine());
                    if (columns >= 1 && columns <= maze.rows)
                        ok = true;
                    else
                    {
                        ok = false;
                        Console.WriteLine("Error - Columns must be part of the maze - Error");
                    }
                }
                catch
                {
                    ok = false;
                    Console.WriteLine("Error - Rows must be an integer - Error");
                }

            } while (!ok);

            int nbOperation = maze.Generate(rows-1, columns-1);

            Console.WriteLine("Maze generated in " + nbOperation + " operations :\n");

            maze.toString();

            Console.WriteLine("Nodes with links : \n");
            maze.FullMaze();
            int i = 0;

        }
    }
}
