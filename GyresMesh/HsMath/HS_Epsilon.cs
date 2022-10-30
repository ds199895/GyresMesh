using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.HsMath
{
    public class HS_Epsilon
    {
        public static double EPSILON = 1.0E-9D;
        public static double SCALE = 1.0E9D;
        public static double SQEPSILON = 1.0E-18D;
        public static double EPSILONANGLE = 1.7453292519943297E-7D;

        public HS_Epsilon()
        {
        }

        public static double clampEpsilon(double x, double min, double max)
        {
            if (x < min + EPSILON)
            {
                return min;
            }
            else
            {
                return x > max - EPSILON ? max : x;
            }
        }

        public static bool isEqualHybrid(double x, double y)
        {
            return HS_Math.fastAbs(x - y) < EPSILON * HS_Math.max(HS_Math.fastAbs(x), HS_Math.fastAbs(y), 1.0D);
        }

        public static bool isEqual(double x, double y)
        {
            return HS_Math.fastAbs(x - y) < EPSILON;
        }

        public static bool isEqualAbs(double x, double y)
        {
            return HS_Math.fastAbs(x - y) < EPSILON;
        }

        public static bool isEqualAbs(double x, double y, double threshold)
        {
            return HS_Math.fastAbs(x - y) < threshold + EPSILON;
        }

        public static bool isEqualRel(double x, double y)
        {
            return HS_Math.fastAbs(x - y) < EPSILON * HS_Math.max(HS_Math.fastAbs(x), HS_Math.fastAbs(y));
        }

        public static bool isZero(double x)
        {
            return HS_Math.fastAbs(x) < EPSILON;
        }

        public static bool isZeroSq(double x)
        {
            return HS_Math.fastAbs(x) < SQEPSILON;
        }

        public static int compareHybrid(double x, double y)
        {
            if (isEqualHybrid(x, y))
            {
                return 0;
            }
            else
            {
                return x > y ? 1 : -1;
            }
        }

        public static int compareAbs(double x, double y)
        {
            if (isEqualAbs(x, y))
            {
                return 0;
            }
            else
            {
                return x > y ? 1 : -1;
            }
        }

        public static int compare(double x, double y)
        {
            if (isEqual(x, y))
            {
                return 0;
            }
            else
            {
                return x > y ? 1 : -1;
            }
        }

        public static int compareRel(double x, double y)
        {
            if (isEqualRel(x, y))
            {
                return 0;
            }
            else
            {
                return x > y ? 1 : -1;
            }
        }

    }
}
