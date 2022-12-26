using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_IntersectionResult
    {
        public double t1 = double.NegativeInfinity;

        public double t2 = double.NegativeInfinity;

        public bool intersection = false;

        public double sqDist = double.PositiveInfinity;

        public Object obj;

        public int dimension = -1;

    }
}
