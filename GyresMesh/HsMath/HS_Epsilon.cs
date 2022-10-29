using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.HsMath
{
    public class HS_Epsilon
    {
        static public double EPSILON = 1e-9;
        static public double SQEPSILON = 1e-18;
        static public double EPSILONANGLE = 1e-5 * Math.PI / 180.0;

        public static bool isZero(double x)
        {
            return Math.Abs(x) < HS_Epsilon.EPSILON;
        }

        public static bool isEqual(double x,double y)
        {
            return Math.Abs(x - y) < HS_Epsilon.EPSILON;
        }
    }
}
