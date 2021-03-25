using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PrimAlgorithm
{
    class Node
    {
        public Link up { get; set; }
        public Link down { get; set; }
        public Link left { get; set; }
        public Link right { get; set; }

        public int id { get; }

        public Node(Link u, Link d, Link l, Link r)
        {
            up = u;
            down = d;
            left = l;
            right = r;
        }

        public Node() { id = Utilities.NextIDNode(); }
    }
}
