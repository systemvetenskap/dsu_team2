using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GolfBokning.Classes
{
    public class Holes : IComparable<Holes>
    {
        public int holeIndex { get; set; }
        public int hcpIndex { get; set; }
        public int erhallna { get; set; }
        public int par { get; set; }

        public int CompareTo(Holes other)
        {
            if (this.holeIndex > other.holeIndex)
            {
                return 1;
            }
            else if (this.holeIndex < other.holeIndex)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

    }
}