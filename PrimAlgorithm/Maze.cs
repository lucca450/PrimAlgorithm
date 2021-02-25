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

            toString();


            return count;
        }

        private void PrimsPathFinder(ref int count, Node currentNode, bool checkLinks = true)
        {
            Console.WriteLine("Current Node : " + currentNode.id);
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
                nextNode = toNext.from;
                PrimsPathFinder(ref count, nextNode);
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
                Console.WriteLine("Next");
                PrimsPathFinder(ref count, nextNode);
            }
            else
            {
                links.Remove(toNext);
                Console.WriteLine("Unusable Link Removed :\nNode : " + toNext.node1.id + " and Node : " + toNext.node2.id);
                PrimsPathFinder(ref count, currentNode, false); // readd false
            }
        }

        public void toString()
        {
            DisplayNode();
        }

        private void DisplayNode()
        {
            Node firstOfLine = begining;
            Node currentNode = begining;
            string rightLinkLine = "", downLinkLine = "";

            while (currentNode != null)
            {
                rightLinkLine = " ";
                downLinkLine = "";
                while (currentNode != null)
                {
                    rightLinkLine += String.Format("{0,-6}", currentNode.id);


                    if (path.Contains(currentNode.right))       //If there's a right node
                    {
                        rightLinkLine += String.Format("{0,-6}", "--" + currentNode.right.weight + "--");
                    }
                    else
                        rightLinkLine += String.Format("{0,-6}", " ");

                    if (path.Contains(currentNode.down))
                    {
                        downLinkLine += String.Format("{0,-12}", "|" + currentNode.down.weight);   
                    }else
                        downLinkLine += String.Format("{0,-12}", " ");

                    if (currentNode.right != null)
                        currentNode = currentNode.right.node2;
                    else
                        currentNode = null;
                    
                } 

                Console.WriteLine(rightLinkLine);
                Console.WriteLine(downLinkLine);
                
                // Preps for next line
                if (firstOfLine.down != null)
                {
                    currentNode = firstOfLine.down.node2;
                    firstOfLine = currentNode;
                }else
                    currentNode = null;
            }







        }

    }
}
