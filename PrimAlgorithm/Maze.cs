using System;
using System.Collections.Generic;
using System.Text;

namespace PrimAlgorithm
{
    class Maze
    {
        public List<Link> path = new List<Link>();
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

            PrimsPathFinder(ref count);

            return count;
        }

        private void PrimsPathFinder(ref int count)
        {
            Node currentNode = begining;
            List<Link> links = new List<Link>();

            while(currentNode != ending)
            {
                if(currentNode.left != null && !links.Contains(currentNode.left) && !path.Contains(currentNode.left))
                {
                    links.Add(currentNode.left);
                }
                if (currentNode.up != null && !links.Contains(currentNode.up) && !path.Contains(currentNode.up))
                {
                    links.Add(currentNode.up);
                }
                if (currentNode.right != null && !links.Contains(currentNode.right) && !path.Contains(currentNode.right))
                {
                    links.Add(currentNode.right);
                }
                if (currentNode.down != null && !links.Contains(currentNode.down) && !path.Contains(currentNode.down))
                {
                    links.Add(currentNode.down);
                }

                links.Sort();

                Link next = links[0];
                links.Remove(next);
                path.Add(next);

                if (next.from == currentNode)
                    currentNode = next.to;
                else
                    currentNode = next.from;
            }
        }

        public void toString()
        {

        }

    }
}
