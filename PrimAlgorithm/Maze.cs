using System;
using System.Collections.Generic;
using System.Text;

namespace PrimAlgorithm
{
    class Maze
    {
        private List<Node> visitedNodes = new List<Node>();
        private List<Link> path = new List<Link>();
        private List<Link> links = new List<Link>();
        private List<Node> allNodes = new List<Node>();

        public int rows { get; }
        public int columns { get; }

        private Node begining;
        private Node ending;

        private Node[,] matrix;

        public Maze(int m, int n)
        {
            rows = m;
            columns = n;
            matrix = new Node[rows,columns];
        }

        private void InitializeNodesAndLinks(int startRow, int startColumn)
        {
            Utilities.initializationCount = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = new Node();
                    if (j > 0)
                    {
                        Link link = new Link(Utilities.RandomWeight(), matrix[i, j - 1], matrix[i, j]);
                        matrix[i, j].left = link;
                        matrix[i, j - 1].right = link;
                    }
                    if (i > 0)
                    {
                        Link link = new Link(Utilities.RandomWeight(), matrix[i - 1, j], matrix[i, j]);
                        matrix[i, j].up = link;
                        matrix[i - 1, j].down = link;
                    }
                    Utilities.initializationCount++;
                    allNodes.Add(matrix[i, j]);
                }
            }

            begining = matrix[startRow, startColumn];

            int endRow = rows - 1 - startRow, endColumn = columns - 1 - startColumn;
            ending = matrix[endRow, endColumn];
        }

        public void Generate(int startRow, int startColumn)
        {
            InitializeNodesAndLinks(startRow, startColumn);
            ExecutePrimsAlgorithm();
        }

        private void AddLinks(Node currentNode)
        {
            if (currentNode.left != null && !links.Contains(currentNode.left) && !path.Contains(currentNode.left))
            {
                currentNode.left.from = currentNode;
                links.Add(currentNode.left);
            }
            if (currentNode.up != null && !links.Contains(currentNode.up) && !path.Contains(currentNode.up))
            {
                currentNode.up.from = currentNode;
                links.Add(currentNode.up);
            }
            if (currentNode.right != null && !links.Contains(currentNode.right) && !path.Contains(currentNode.right))
            {
                currentNode.right.from = currentNode;
                links.Add(currentNode.right);
            }
            if (currentNode.down != null && !links.Contains(currentNode.down) && !path.Contains(currentNode.down))
            {
                currentNode.down.from = currentNode;
                links.Add(currentNode.down);
            }
        }

        private void ExecutePrimsAlgorithm()
        {
            Utilities.generationCount = 0;
            Node currentNode = begining, nextNode;
            visitedNodes.Add(currentNode);

            while (!HasVisitedAllNodes())
            {
                AddLinks(currentNode);

                bool foundGoodLink;
                do
                {
                    Utilities.generationCount++;
                    Link link = GetLightestLink();
                    currentNode = link.from;

                    if (Utilities.displayProcedure)
                        Console.WriteLine("Current Node : " + currentNode.id);

                    if (link.node1 == currentNode)
                        nextNode = link.node2;
                    else
                        nextNode = link.node1;

                    if (Utilities.displayProcedure)
                        Console.WriteLine("Current Link : " + currentNode.id + " to " + nextNode.id + " with a weight of " + link.weight);

                    if (! visitedNodes.Contains(nextNode))
                    {
                        foundGoodLink = true;

                        if(! visitedNodes.Contains(nextNode))
                            visitedNodes.Add(nextNode);

                        path.Add(link);

                        if (Utilities.displayProcedure)
                            Console.WriteLine("Added Link :\nNode " + currentNode.id + " to Node " + nextNode.id + "\n--------");

                        currentNode = nextNode;
                    }
                    else
                    {
                        foundGoodLink = false;
                        if (Utilities.displayProcedure)
                            Console.WriteLine("Unusable Link Removed :\nNode " + link.node1.id + " to Node " + link.node2.id + "\n---------");
                    }

                    links.Remove(link);
                } while (!foundGoodLink && links.Count != 0) ;
            }
        }

        private Link GetLightestLink()
        {
            links.Sort();
            return links[0];
        }

        private bool HasVisitedAllNodes()
        {
            foreach (Node n in allNodes)
            {
                if (!visitedNodes.Contains(n))
                {
                    return false;
                }
            }
            return true;
        }

        // Returns if starting coordinates are located on the edge of the maze
        public bool VerifyStartCoordinates(int iRow, int iColumn)
        {
            if (!(iColumn >= 1 && iColumn <= columns))
            {
                Console.WriteLine("Error - The column must be part of the maze");
                return false;
            }

            if (iColumn == 1 || iColumn == columns)
                return true;
            else
            {
                if (!(iRow == 1 || iRow == rows))
                {
                    Console.WriteLine("Error - Maze start must be on the edges");
                    return false;
                }
                else
                    return true;
            }
        }

        // Displays maze Path
        public void toString()
        {
            Utilities.displayCount = 0;
            Console.WriteLine("B: Begining");
            Console.WriteLine("E: Ending");
            Console.WriteLine("O: Path\n");

            Node firstOfLine = matrix[0, 0];
            Node currentNode = matrix[0, 0];
            string rightLinkLine = "", downLinkLine;

            while (currentNode != null)
            {
                rightLinkLine = "";
                downLinkLine = "";
                while (currentNode != null)
                {
                    if (currentNode == begining)
                        rightLinkLine += String.Format("{0,-3}", "B");
                    else if (currentNode == ending)
                        rightLinkLine += String.Format("{0,-3}", "E");
                    else
                        rightLinkLine += String.Format("{0,-3}", "O");

                    if (currentNode.down != null)
                        if (path.Contains(currentNode.down))
                            downLinkLine += String.Format("{0,-3}", "O");
                        else
                            downLinkLine += String.Format("{0,-3}", "#");

                    if (currentNode.right != null && currentNode.down != null)
                        downLinkLine += String.Format("{0,-3}", "#");

                    if (currentNode.right != null)
                    {
                        if (path.Contains(currentNode.right))       //If there's a right node
                            rightLinkLine += String.Format("{0,-3}", "O");
                        else
                            rightLinkLine += String.Format("{0,-3}", "#");
                        currentNode = currentNode.right.node2;
                    } else
                        currentNode = null;

                    Utilities.displayCount++;
                }

                Console.WriteLine(rightLinkLine);
                Console.WriteLine(downLinkLine);

                // Preps for next line
                if (firstOfLine.down != null)
                {
                    currentNode = firstOfLine.down.node2;
                    firstOfLine = currentNode;
                }
                else
                    currentNode = null;
            }

            string output = "";
            for (int i = 0; i < rightLinkLine.Length; i++)
                output += "-";
            Console.WriteLine(output);
        }

        //  Displays the maze with all weights and links
        public void FullMaze()
        {
            Node firstOfLine = matrix[0, 0];
            Node currentNode = matrix[0, 0];
            string rightLinkLine = "", downLinkLine;

            while (currentNode != null)
            {
                rightLinkLine = "";
                downLinkLine = "";
                while (currentNode != null)
                {
                    rightLinkLine += String.Format("{0,-4}", currentNode.id);

                    if(currentNode.right != null){                      //If there's a right link
                        if (path.Contains(currentNode.right))               //If right link part of the path
                        {
                            rightLinkLine += String.Format("{0,-8}", "--" + currentNode.right.weight + "--");
                        }
                        else
                            rightLinkLine += String.Format("{0,-8}", "  " + currentNode.right.weight + "  ");
                    }
                        
                    if(currentNode.down != null){                       //If there's a down link
                        if (path.Contains(currentNode.down))                //If down link part of the path
                        {
                            downLinkLine += String.Format("{0,-12}", "|" + currentNode.down.weight);
                        }else
                            downLinkLine += String.Format("{0,-12}", currentNode.down.weight);
                    }
                    
                    if (currentNode.right != null)                      //If there's a link on the right
                        currentNode = currentNode.right.node2;              //Go to that link 2nd node
                    else
                        currentNode = null;

                }

                Console.WriteLine(rightLinkLine);
                Console.WriteLine(downLinkLine);

                // Preps for next line
                if (firstOfLine.down != null)                           //If there's a link down
                {
                    currentNode = firstOfLine.down.node2;                   //Go to that next row of nodes
                    firstOfLine = currentNode;
                }
                else
                    currentNode = null;
            }

            string output = "";
            for (int i = 0; i < rightLinkLine.Length; i++)
                output += "-";
            Console.WriteLine(output);
        }
    }
}
