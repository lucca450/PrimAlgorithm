using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PrimAlgorithm
{
    class Node
    {
        public int nodeID;
        public Link up { get; set; }
        public Link down { get; set; }
        public Link left { get; set; }
        public Link right { get; set; }

        public Node(Link u, Link d, Link l, Link r)
        {
            up = u;
            down = d;
            left = l;
            right = r;
        }

        public Node() { nodeID = Utilities.NextNodeID(); }
    }
}
