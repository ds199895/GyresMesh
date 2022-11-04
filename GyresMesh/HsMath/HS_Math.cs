using Hsy.Geo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.HsMath
{
    public class HS_Math
    {
        static  double DEGTORAD = 0.017453292519943295D;
        static  double RADTODEG = 57.29577951308232D;

        public HS_Math()
        {
        }

        public static double fastAbs(double x)
        {
            return x > 0.0D ? x : -x;
        }

        public static double max(double x, double y)
        {
            return y > x ? y : x;
        }

        public static double min(double x, double y)
        {
            return y < x ? y : x;
        }

        public static float max(float x, float y)
        {
            return y > x ? y : x;
        }

        public static float min(float x, float y)
        {
            return y < x ? y : x;
        }

        public static int max(int x, int y)
        {
            return y > x ? y : x;
        }

        public static int min(int x, int y)
        {
            return y < x ? y : x;
        }

        public static double max(double x, double y, double z)
        {
            return y > x ? (z > y ? z : y) : (z > x ? z : x);
        }

        public static double min(double x, double y, double z)
        {
            return y < x ? (z < y ? z : y) : (z < x ? z : x);
        }

        public static float max(float x, float y, float z)
        {
            return y > x ? (z > y ? z : y) : (z > x ? z : x);
        }

        public static float min(float x, float y, float z)
        {
            return y < x ? (z < y ? z : y) : (z < x ? z : x);
        }

        public static int max(int x, int y, int z)
        {
            return y > x ? (z > y ? z : y) : (z > x ? z : x);
        }

        public static int min(int x, int y, int z)
        {
            return y < x ? (z < y ? z : y) : (z < x ? z : x);
        }

        public static  int max(params int[] numbers)
        {
            int maxValue = numbers[0];

            for (int i = 1; i < numbers.Length; ++i)
            {
                if (numbers[i] > maxValue)
                {
                    maxValue = numbers[i];
                }
            }

            return maxValue;
        }

        public static  int min(params int[] numbers)
        {
            int minValue = numbers[0];

            for (int i = 1; i < numbers.Length; ++i)
            {
                if (numbers[i] < minValue)
                {
                    minValue = numbers[i];
                }
            }

            return minValue;
        }

        public static  float max(params float[] numbers)
        {
            float maxValue = numbers[0];

            for (int i = 1; i < numbers.Length; ++i)
            {
                if (numbers[i] > maxValue)
                {
                    maxValue = numbers[i];
                }
            }

            return maxValue;
        }

        public static  float min(params float[] numbers)
        {
            float minValue = numbers[0];

            for (int i = 1; i < numbers.Length; ++i)
            {
                if (numbers[i] < minValue)
                {
                    minValue = numbers[i];
                }
            }

            return minValue;
        }

        public static  double max(params double[] numbers)
        {
            double maxValue = numbers[0];

            for (int i = 1; i < numbers.Length; ++i)
            {
                if (numbers[i] > maxValue)
                {
                    maxValue = numbers[i];
                }
            }

            return maxValue;
        }

        public static  double min(params double[] numbers)
        {
            double minValue = numbers[0];

            for (int i = 1; i < numbers.Length; ++i)
            {
                if (numbers[i] < minValue)
                {
                    minValue = numbers[i];
                }
            }

            return minValue;
        }

        public static  int floor(float x)
        {
            return x >= 0.0F ? (int)x : (int)x - 1;
        }

        public static unsafe float fastLog2(float i)
        {
            //float x =(float)(UInt32)i;
            //float x = (float)float.floatToRawIntBits(i);
            float x = (float)*(int*)&i;
            x *= 1.1920929E-7F;
            x -= 127.0F;
            float y = x - (float)floor(x);
            y = (y - y * y) * 0.346607F;
            return x + y;

        }

        public static unsafe float fastPow2(float i)
        {
            float x = i - (float)floor(i);
            x = (x - x * x) * 0.33971F;
            int toInt = (int)((i + 127.0F - x) * 8388608.0F);
            float toFloat = *(float*)&toInt;
            return toFloat;
        }

        public static  float fastPow(float a, float b)
        {
            return fastPow2(b * fastLog2(a));
        }

        public static unsafe float fastInvSqrt(float x)
        {
            float half = 0.5F * x;
            //int i = Float.floatToIntBits(x);
            int i = *(int*)&x;
            i = 1597463174 - (i >> 1);
            //x = Float.intBitsToFloat(i);
            x = *(float*)&i;
            return x * (1.5F - half * x * x);
            //long i;
            //float x2, y;
            //const float threehalfs = 1.5F;

            //x2 = x * 0.5F;
            //y = x;
            //i = *(long*)&y;                       // evil floating point bit level hacking
            //i = 0x5f3759df - (i >> 1);               // what the fuck?
            //y = *(float*)&i;
            //y = y * (threehalfs - (x2 * y * y));   // 1st iteration
            //                                       //    y  = y * ( threehalfs - ( x2 * y * y ) );   // 2nd iteration, this can be removed

            //return y;

        }

        public static  float fastSqrt(float x)
        {
            return 1.0F / fastInvSqrt(x);
        }

        public static unsafe int getExp(double v)
        {
            long tolong = *(long*)&v;
            return v == 0.0D ? 0 : (int)((9218868437227405312L & tolong) >> 52) - 1022;
        }

        public static double hypot(double a, double b)
        {
            double r;
            if (Math.Abs(a) > Math.Abs(b))
            {
                r = b / a;
                r = Math.Abs(a) * Math.Sqrt(1.0D + r * r);
            }
            else if (b != 0.0D)
            {
                r = a / b;
                r = Math.Abs(b) * Math.Sqrt(1.0D + r * r);
            }
            else
            {
                r = 0.0D;
            }

            return r;
        }

        public static double logBase2(double value)
        {
            return Math.Log(value) / Math.Log(2.0D);
        }

        public static bool isPowerOfTwo(int value)
        {
            return value == powerOfTwoCeiling(value);
        }

        public static int powerOfTwoCeiling(int reference)
        {
            
            int power = (int)Math.Ceiling(Math.Log((double)reference) / Math.Log(2.0D));
            return (int)Math.Pow(2.0D, (double)power);
        }

        public static int powerOfTwoFloor(int reference)
        {
            int power = (int)Math.Floor(Math.Log((double)reference) / Math.Log(2.0D));
            return (int)Math.Pow(2.0D, (double)power);
        }

        public static double clamp(double v, double min, double max)
        {
            return v < min ? min : (v > max ? max : v);
        }

        public static int clamp(int v, int min, int max)
        {
            return v < min ? min : (v > max ? max : v);
        }

        public static double radians(double degrees)
        {
            return degrees * 0.017453292519943295D;
        }

        public static double degrees(double radians)
        {
            return radians * 57.29577951308232D;
        }

        public static HS_Coord abs(HS_Coord v)
        {
            return new HS_Vector(Math.Abs(v.xd), Math.Abs(v.yd), Math.Abs(v.zd));
        }

        public static HS_Coord sign(HS_Coord v)
        {
            return new HS_Vector(Math.Sign(v.xd), Math.Sign(v.yd), Math.Sign(v.zd));
        }

        public static HS_Coord floor(HS_Coord v)
        {
            return new HS_Vector(Math.Floor(v.xd), Math.Floor(v.yd), Math.Floor(v.zd));
        }

        public static HS_Coord ceiling(HS_Coord v)
        {
            return new HS_Vector(Math.Ceiling(v.xd), Math.Ceiling(v.yd), Math.Ceiling(v.zd));
        }

        public static double fract(double x)
        {
            return x - Math.Floor(x);
        }

        public static HS_Coord fract(HS_Coord v)
        {
            return new HS_Vector(fract(v.xd), fract(v.yd), fract(v.zd));
        }

        public static HS_Coord mod(HS_Coord u, HS_Coord v)
        {
            return new HS_Vector(u.xd % v.xd, u.yd % v.yd, u.zd % v.zd);
        }

        public static HS_Coord mod(HS_Coord u, double v)
        {
            return new HS_Vector(u.xd % v, u.yd % v, u.zd % v);
        }

        public static HS_Coord min(HS_Coord u, HS_Coord v)
        {
            return new HS_Vector(min(u.xd, v.xd), min(u.yd, v.yd), min(u.zd, v.zd));
        }

        public static HS_Coord min(HS_Coord u, double v)
        {
            return new HS_Vector(min(u.xd, v), min(u.yd, v), min(u.zd, v));
        }

        public static HS_Coord max(HS_Coord u, HS_Coord v)
        {
            return new HS_Vector(max(u.xd, v.xd), max(u.yd, v.yd), max(u.zd, v.zd));
        }

        public static HS_Coord max(HS_Coord u, double v)
        {
            return new HS_Vector(max(u.xd, v), max(u.yd, v), max(u.zd, v));
        }

        public static HS_Coord clamp(HS_Coord u, HS_Coord min, HS_Coord max)
        {
            return new HS_Vector(clamp(u.xd, min.xd, max.xd), clamp(u.yd, min.yd, max.yd), clamp(u.zd, min.zd, max.zd));
        }

        public static HS_Coord clamp(HS_Coord u, double min, double max)
        {
            return new HS_Vector(clamp(u.xd, min, max), clamp(u.yd, min, max), clamp(u.zd, min, max));
        }

        public static double mix(double x, double y, double a)
        {
            return (1.0D - a) * x + a * y;
        }

        public static HS_Coord mix(HS_Coord u, HS_Coord v, double a)
        {
            return new HS_Vector(mix(u.xd, v.xd, a), mix(u.yd, v.yd, a), mix(u.zd, v.zd, a));
        }

        public static HS_Coord mix(HS_Coord u, HS_Coord v, HS_Coord a)
        {
            return new HS_Vector(mix(u.xd, v.xd, a.xd), mix(u.yd, v.yd, a.yd), mix(u.zd, v.zd, a.zd));
        }

        public static double step(double edge, double x)
        {
            return x < edge ? 0.0D : 1.0D;
        }

        public static HS_Vector step(double edge, HS_Coord v)
        {
            return new HS_Vector(step(v.xd, edge), step(v.yd, edge), step(v.zd, edge));
        }

        public static HS_Vector step(HS_Coord edge, HS_Coord v)
        {
            return new HS_Vector(step(v.xd, edge.xd), step(v.yd, edge.yd), step(v.zd, edge.zd));
        }

        public static double smoothstep(double edge0, double edge1, double x)
        {
            double y = clamp((x - edge0) / (edge1 - edge0), 0.0D, 1.0D);
            return y * y * (3.0D - 2.0D * y);
        }

        public static HS_Vector smoothstep(double edge0, double edge1, HS_Coord v)
        {
            return new HS_Vector(smoothstep(edge0, edge1, v.xd), smoothstep(edge0, edge1, v.yd), smoothstep(edge0, edge1, v.zd));
        }

        public static HS_Vector smoothstep(HS_Coord edge0, HS_Coord edge1, HS_Coord v)
        {
            return new HS_Vector(smoothstep(edge0.xd, edge1.xd, v.xd), smoothstep(edge0.yd, edge1.yd, v.yd), smoothstep(edge0.zd, edge1.zd, v.zd));
        }
    }
}
