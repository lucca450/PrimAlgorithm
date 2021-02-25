using System;
using System.Collections.Generic;
using System.Text;

namespace PrimAlgorithm
{
    static class Utilities
    {
        private static int nextLinkID = 1;
        private static int nextNodeID = 1;

        public static int NextLinkID(){ return nextLinkID++; }
        public static int NextNodeID() { return nextNodeID++; }
    }
}
