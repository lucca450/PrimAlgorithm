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

        public Link inboundLink = null;
        public Link outboundLink = null;

        public int id;

        public Node(Link u, Link d, Link l, Link r)
        {
            up = u;
            down = d;
            left = l;
            right = r;
        }

        public Node() { id = Utilities.NextIDNode(); }

        public bool IsAvailableToGoToNext()
        {
            return inboundLink == null /*&& HasAvailableLink()*/ && outboundLink == null;
        }

        private bool HasAvailableLink()
        {
            if (up != null && up.from == null && up.to == null)
            {
                return true;
            }

            if (down != null && down.from == null && down.to == null)
            {
                return true;
            }

            if (left != null && left.from == null && left.to == null)
            {
                return true;
            }

            if (right != null && right.from == null && right.to == null)
            {
                return true;
            }
            return false;
        }
    }
}
