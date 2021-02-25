using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PrimAlgorithm
{
    class Link : IComparable<Link>
    {
        int weight, linkID;
        public Node from { get; set; }
        public Node to { get; set; }

        public Link(int w, Node n1, Node n2)
        {
            linkID = Utilities.NextLinkID();
            weight = w;
            from = n1;
            to = n2;
        }

        public int CompareTo([AllowNull] Link other)
        {
            if (other == null)
                return 1;

            else
                return weight.CompareTo(other.weight);
        }
    }
}
