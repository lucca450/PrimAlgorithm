using System;
using System.Collections.Generic;
using System.Text;

namespace PrimAlgorithm
{
    static class Utilities
    {
        public static int nextIDNode = 1;

        public static int NextIDNode()
        {
            return nextIDNode++;
        }
    }
}
