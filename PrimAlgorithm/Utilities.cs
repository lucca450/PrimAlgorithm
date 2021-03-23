using System;
using System.Collections.Generic;
using System.Text;

namespace PrimAlgorithm
{
    static class Utilities
    {
        public static int nextIDNode = 1;
        public static bool displayProcedure;

        public static int generationCount = 0;
        public static int initializationCount = 0;
        public static int displayCount = 0;

        public static int NextIDNode()
        {
            return nextIDNode++;
        }

        public static int RandomWeight()
        {
            Random rnd = new Random();
            return rnd.Next(1, 11);
        }
    }
}
