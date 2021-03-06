using System;
using System.Collections.Generic;
using System.Text;

namespace PrimAlgorithm
{
    class Maze
    {
        public List<Node> visitedNodes = new List<Node>();
        public List<Link> path = new List<Link>();
        public List<Link> links = new List<Link>();
        public List<Link> removedLinks = new List<Link>();
        public int rows { get; set; }
        public int columns { get; set; }

        public Node begining { get; set; }
        public Node ending { get; set; }

        public Node[,] matrix { get; set; }

        public Maze(int m, int n)
        {
            rows = m;
            columns = n;
            matrix = new Node[rows,columns];
        }

        private int RandomWeight()
        {
            Random rnd = new Random();
            return rnd.Next(1, 11);
        }

        public int Generate(int startRow, int startColumn)
        {
            int count = 0;
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0;j< columns; j++)
                {
                    count++;
                    matrix[i, j] = new Node();
                    if(j > 0)
                    {
                        Link link = new Link(RandomWeight(), matrix[i, j - 1], matrix[i, j]);
                        matrix[i, j].left = link;
                        matrix[i, j - 1].right = link;
                    }
                    if(i > 0)
                    {
                        Link link = new Link(RandomWeight(), matrix[i - 1, j], matrix[i, j]);
                        matrix[i, j].up = link;
                        matrix[i - 1, j].down = link;
                    }
                }
            }

            begining = matrix[startRow, startColumn];

            int endRow = rows-1 - startRow, endColumn = columns-1 - startColumn;
            ending = matrix[endRow, endColumn];

            PrimsPathFinder(ref count, begining);

            return count;
        }

        private void PrimsPathFinder(ref int count, Node currentNode, bool checkLinks = true)
        {
            Console.WriteLine("Current Node : " + currentNode.id);
            Console.WriteLine();
            if (checkLinks)
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

                links.Sort();
            }
            if (links.Count == 0)
                return;

            Link toNext;
            Node nextNode;

            count++;
            toNext = links[0];

            if (toNext.node1 == currentNode)
                nextNode = toNext.node2;
            else if (toNext.node2 == currentNode)
                nextNode = toNext.node1;
            else
            {
                Console.WriteLine("Link with Node :" + toNext.node1.id + " and Node :" + toNext.node2.id + " Doesnt belong to me");
                Console.WriteLine("--------");
                nextNode = toNext.from;
                PrimsPathFinder(ref count, nextNode, false);
                return;
            }

            if (nextNode.IsAvailableToGoToNext())
            {
                toNext.to = nextNode;

                currentNode.outboundLink = toNext;
                nextNode.inboundLink = toNext;

                path.Add(toNext);
                links.Remove(toNext);

                Console.WriteLine("Link with Node : " + toNext.node1.id + " and " + toNext.node2.id + " added to " + currentNode.id);
                Console.WriteLine("---Next---");
                PrimsPathFinder(ref count, nextNode);
            }
            else
            {
                links.Remove(toNext);
                Console.WriteLine("Unusable Link Removed :\nNode : " + toNext.node1.id + " and Node : " + toNext.node2.id);
                Console.WriteLine("--------");
                PrimsPathFinder(ref count, currentNode, false); // readd false
            }
        }

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

        public void toString()
        {
            Console.WriteLine("B: Begining");
            Console.WriteLine("E: Ending");
            Console.WriteLine("O: Path\n");

            Node firstOfLine = matrix[0,0];
            Node currentNode = matrix[0, 0];
            string rightLinkLine = "", downLinkLine;

            while (currentNode != null)
            {
                rightLinkLine = "";
                downLinkLine = "";
                while (currentNode != null)
                {
                    if(currentNode == begining)
                        rightLinkLine += String.Format("{0,-3}", "B");
                    else if(currentNode == ending)
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
                        }else
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
