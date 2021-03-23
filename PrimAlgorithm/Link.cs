using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PrimAlgorithm
{
    class Link : IComparable<Link>
    {
        public int weight;
        public Node node1 { get; set; }
        public Node node2 { get; set; }
        public Node from { get; set; }

        public Link(int w, Node n1, Node n2)
        {
            weight = w;
            node1 = n1;
            node2 = n2;
        }

        public int CompareTo([AllowNull] Link other) //Override List<Link>.Sort()
        {
            Utilities.generationCount++;
            if (other == null)
                return 1;

            else
                return weight.CompareTo(other.weight);
        }
    }
}
