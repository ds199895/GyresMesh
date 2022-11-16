using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_OrthoProject : HS_Map2D
    {

        public static int YZ = 0;
        /**
         *
         */
        public static int XZ = 1;
        /**
         *
         */
        public static int XY = 2;
        /**
         *
         */
        public static int YZrev = 3;
        /**
         *
         */
        public static int XZrev = 4;
        /**
         *
         */
        public static int XYrev = 5;
        public int mode;
        public HS_OrthoProject()
        {
            new HS_OrthoProject(XY);
        }
        public HS_OrthoProject(int mode)
        {
            if (mode < 0 || mode > 2)
            {
                throw new IndexOutOfRangeException();
            }
            this.mode = mode;
        }

        public HS_OrthoProject(HS_Coord v)
        {
            set(v);
        }
        public HS_OrthoProject(HS_Plane P)
        {
            set(P.getNormal());
        }
        public void set(HS_Coord c)
        {
            if (Math.Abs(c.xd) > Math.Abs(c.yd))
            {
                mode = Math.Abs(c.xd) > Math.Abs(c.zd) ? YZ : XY;
            }
            else
            {
                mode = Math.Abs(c.yd) > Math.Abs(c.zd) ? XZ : XY;
            }
            if (mode == XY && c.zd < 0)
            {
                mode = XYrev;
            }
            if (mode == YZ && c.xd < 0)
            {
                mode = YZrev;
            }
            if (mode == XZ && c.yd < 0)
            {
                mode = XZrev;
            }
        }
        public void mapPoint3D(HS_Coord p,  HS_MutableCoord result)
        {

            if (mode == XY) result.Set(p.xd, p.yd, 0);
            if (mode == YZ) result.Set(p.yd, p.zd, 0);
            if (mode == XZ) result.Set(p.zd, p.xd, 0);
            if (mode == XYrev) result.Set(p.yd, p.xd, 0);
            if (mode == YZrev) result.Set(p.zd, p.yd, 0);
            if (mode == XZrev) result.Set(p.xd, p.zd, 0);
            Console.WriteLine("result: " +result);
        }

        public void mapPoint3D(double x, double y, double z, HS_MutableCoord result)
        {
            result = new HS_Point();
            if (mode == XY) result.Set(x, y, 0);
            if (mode == YZ) result.Set(y, z, 0);
            if (mode == XZ) result.Set(z, x, 0);
            if (mode == XYrev) result.Set(y, x, 0);
            if (mode == YZrev) result.Set(z, y, 0);
            if (mode == XZrev) result.Set(x, z, 0);
        }

        public HS_Coord mapPoint3D(HS_Coord p)
        {
            HS_Point result = new HS_Point();
            if (mode == XY) result.Set(p.xd, p.yd, 0);
            if (mode == YZ) result.Set(p.yd, p.zd, 0);
            if (mode == XZ) result.Set(p.zd, p.xd, 0);
            if (mode == XYrev) result.Set(p.yd, p.xd, 0);
            if (mode == YZrev) result.Set(p.zd, p.yd, 0);
            if (mode == XZrev) result.Set(p.xd, p.zd, 0);
            return result;
        }

        public HS_Coord mapPoint3D(double x, double y, double z)
        {
            HS_Point result = new HS_Point();
            if (mode == XY) result.Set(x, y, 0);
            if (mode == YZ) result.Set(y, z, 0);
            if (mode == XZ) result.Set(z, x, 0);
            if (mode == XYrev) result.Set(y, x, 0);
            if (mode == YZrev) result.Set(z, y, 0);
            if (mode == XZrev) result.Set(x, z, 0);
            return result;
        }

        public void mapVector3D(HS_Coord p, HS_MutableCoord result)
        {
            result = new HS_Point();
            if (mode == XY) result.Set(p.xd, p.yd, 0);
            if (mode == YZ) result.Set(p.yd, p.zd, 0);
            if (mode == XZ) result.Set(p.zd, p.xd, 0);
            if (mode == XYrev) result.Set(p.yd, p.xd, 0);
            if (mode == YZrev) result.Set(p.zd, p.yd, 0);
            if (mode == XZrev) result.Set(p.xd, p.zd, 0);
        }

        public void mapVector3D(double x, double y, double z, HS_MutableCoord result)
        {
            result = new HS_Point();
            if (mode == XY) result.Set(x, y, 0);
            if (mode == YZ) result.Set(y, z, 0);
            if (mode == XZ) result.Set(z, x, 0);
            if (mode == XYrev) result.Set(y, x, 0);
            if (mode == YZrev) result.Set(z, y, 0);
            if (mode == XZrev) result.Set(x, z, 0);
        }

        public HS_Coord mapVector3D(HS_Coord p)
        {
            HS_Point result = new HS_Point();
            if (mode == XY) result.Set(p.xd, p.yd, 0);
            if (mode == YZ) result.Set(p.yd, p.zd, 0);
            if (mode == XZ) result.Set(p.zd, p.xd, 0);
            if (mode == XYrev) result.Set(p.yd, p.xd, 0);
            if (mode == YZrev) result.Set(p.zd, p.yd, 0);
            if (mode == XZrev) result.Set(p.xd, p.zd, 0);
            return result;
        }

        public HS_Coord mapVector3D(double x, double y, double z)
        {
            HS_Point result = new HS_Point();
            if (mode == XY) result.Set(x, y, 0);
            if (mode == YZ) result.Set(y, z, 0);
            if (mode == XZ) result.Set(z, x, 0);
            if (mode == XYrev) result.Set(y, x, 0);
            if (mode == YZrev) result.Set(z, y, 0);
            if (mode == XZrev) result.Set(x, z, 0);
            return result;
        }

        public void unmapPoint2D(HS_Coord p, HS_MutableCoord result)
        {
            result = new HS_Point();
            if (mode == XY) result.Set(p.xd, p.yd, 0);
            if (mode == YZ) result.Set(0, p.xd, p.yd);
            if (mode == XZ) result.Set(p.yd, 0, p.xd);
            if (mode == XYrev) result.Set(p.yd, p.xd, 0);
            if (mode == YZrev) result.Set(0, p.yd, p.xd);
            if (mode == XZrev) result.Set(p.xd, 0, p.yd);

        }

        public void unmapPoint2D(double x, double y,  HS_MutableCoord result)
        {
            result = new HS_Point();
            if (mode == XY) result.Set(x,y, 0);
            if (mode == YZ) result.Set(0, x, y);
            if (mode == XZ) result.Set(y, 0, x);
            if (mode == XYrev) result.Set(y, x, 0);
            if (mode == YZrev) result.Set(0, y, x);
            if (mode == XZrev) result.Set(x, 0, y);
        }

        public HS_Coord unmapPoint2D(HS_Coord p)
        {
            HS_Point result = new HS_Point();
            if (mode == XY) result.Set(p.xd, p.yd, 0);
            if (mode == YZ) result.Set(0, p.xd, p.yd);
            if (mode == XZ) result.Set(p.yd, 0, p.xd);
            if (mode == XYrev) result.Set(p.yd, p.xd, 0);
            if (mode == YZrev) result.Set(0, p.yd, p.xd);
            if (mode == XZrev) result.Set(p.xd, 0, p.yd);
            return result;
        }

        public HS_Coord unmapPoint2D(double x, double y)
        {
            HS_Point result = new HS_Point();
            if (mode == XY) result.Set(x, y, 0);
            if (mode == YZ) result.Set(0, x, y);
            if (mode == XZ) result.Set(y, 0, x);
            if (mode == XYrev) result.Set(y, x, 0);
            if (mode == YZrev) result.Set(0, y, x);
            if (mode == XZrev) result.Set(x, 0, y);
            return result;
        }

        public void unmapPoint3D(HS_Coord p, HS_MutableCoord result)
        {
            result = new HS_Point();
            if (mode == XY) result.Set(p.xd, p.yd, 0);
            if (mode == YZ) result.Set(0, p.xd, p.yd);
            if (mode == XZ) result.Set(p.yd, 0, p.xd);
            if (mode == XYrev) result.Set(p.yd, p.xd, 0);
            if (mode == YZrev) result.Set(0, p.yd, p.xd);
            if (mode == XZrev) result.Set(p.xd, 0, p.yd);
            
        }

        public void unmapPoint3D(double x, double y, double z, HS_MutableCoord result)
        {
            result = new HS_Point();
            if (mode == XY) result.Set(x, y, z);
            if (mode == YZ) result.Set(z, x, y);
            if (mode == XZ) result.Set(y, z, x);
            if (mode == XYrev) result.Set(y, x, -z);
            if (mode == YZrev) result.Set(-z, y, x);
            if (mode == XZrev) result.Set(x, -z, y);
        }

        public HS_Coord unmapPoint3D(HS_Coord p)
        {
            HS_Point result = new HS_Point();
            if (mode == XY) result.Set(p.xd, p.yd, 0);
            if (mode == YZ) result.Set(0, p.xd, p.yd);
            if (mode == XZ) result.Set(p.yd, 0, p.xd);
            if (mode == XYrev) result.Set(p.yd, p.xd, 0);
            if (mode == YZrev) result.Set(0, p.yd, p.xd);
            if (mode == XZrev) result.Set(p.xd, 0, p.yd);
            return result;
        }

        public HS_Coord unmapPoint3D(double x, double y,double z)
        {
            HS_Point result = new HS_Point();
            if (mode == XY) result.Set(x, y, z);
            if (mode == YZ) result.Set(z, x, y);
            if (mode == XZ) result.Set(y, z, x);
            if (mode == XYrev) result.Set(y, x, -z);
            if (mode == YZrev) result.Set(-z, y, x);
            if (mode == XZrev) result.Set(x, -z, y);
            return result;
        }

        public void unmapVector2D(HS_Coord p, HS_MutableCoord result)
        {
            result = new HS_Point();
            if (mode == XY) result.Set(p.xd, p.yd, 0);
            if (mode == YZ) result.Set(0, p.xd, p.yd);
            if (mode == XZ) result.Set(p.yd, 0, p.xd);
            if (mode == XYrev) result.Set(p.yd, p.xd, 0);
            if (mode == YZrev) result.Set(0, p.yd, p.xd);
            if (mode == XZrev) result.Set(p.xd, 0, p.yd);
        }

        public void unmapVector2D(double x, double y, HS_MutableCoord result)
        {
            result = new HS_Point();
            if (mode == XY) result.Set(x, y, 0);
            if (mode == YZ) result.Set(0, x, y);
            if (mode == XZ) result.Set(y, 0, x);
            if (mode == XYrev) result.Set(y, x, 0);
            if (mode == YZrev) result.Set(0, y, x);
            if (mode == XZrev) result.Set(x, 0, y);
        }

        public HS_Coord unmapVector2D(HS_Coord p)
        {
            HS_Point result = new HS_Point();
            if (mode == XY) result.Set(p.xd, p.yd, 0);
            if (mode == YZ) result.Set(0, p.xd, p.yd);
            if (mode == XZ) result.Set(p.yd, 0, p.xd);
            if (mode == XYrev) result.Set(p.yd, p.xd, 0);
            if (mode == YZrev) result.Set(0, p.yd, p.xd);
            if (mode == XZrev) result.Set(p.xd, 0, p.yd);
            return result;
        }

        public HS_Coord unmapVector2D(double x, double y)
        {
            HS_Point result = new HS_Point();
            if (mode == XY) result.Set(x, y, 0);
            if (mode == YZ) result.Set(0, x, y);
            if (mode == XZ) result.Set(y, 0, x);
            if (mode == XYrev) result.Set(y, x, 0);
            if (mode == YZrev) result.Set(0, y, x);
            if (mode == XZrev) result.Set(x, 0, y);
            return result;
        }

        public void unmapVector3D(HS_Coord p, HS_MutableCoord result)
        {
            result = new HS_Point();
            if (mode == XY) result.Set(p.xd, p.yd, 0);
            if (mode == YZ) result.Set(0, p.xd, p.yd);
            if (mode == XZ) result.Set(p.yd, 0, p.xd);
            if (mode == XYrev) result.Set(p.yd, p.xd, 0);
            if (mode == YZrev) result.Set(0, p.yd, p.xd);
            if (mode == XZrev) result.Set(p.xd, 0, p.yd);
        }

        public void unmapVector3D(double x, double y, double z, HS_MutableCoord result)
        {
            result = new HS_Point();
            if (mode == XY) result.Set(x, y, z);
            if (mode == YZ) result.Set(z, x, y);
            if (mode == XZ) result.Set(y, z, x);
            if (mode == XYrev) result.Set(y, x, -z);
            if (mode == YZrev) result.Set(-z, y, x);
            if (mode == XZrev) result.Set(x, -z, y);
        }

        public HS_Coord unmapVector3D(HS_Coord p)
        {
            HS_Point result = new HS_Point();
            if (mode == XY) result.Set(p.xd, p.yd, 0);
            if (mode == YZ) result.Set(0, p.xd, p.yd);
            if (mode == XZ) result.Set(p.yd, 0, p.xd);
            if (mode == XYrev) result.Set(p.yd, p.xd, 0);
            if (mode == YZrev) result.Set(0, p.yd, p.xd);
            if (mode == XZrev) result.Set(p.xd, 0, p.yd);
            return result;
        }

        public HS_Coord unmapVector3D(double x, double y,double z)
        {
            HS_Point result = new HS_Point();
            if (mode == XY) result.Set(x, y, z);
            if (mode == YZ) result.Set(z, x, y);
            if (mode == XZ) result.Set(y, z, x);
            if (mode == XYrev) result.Set(y, x, -z);
            if (mode == YZrev) result.Set(-z, y, x);
            if (mode == XZrev) result.Set(x, -z, y);
            return result;
        }
    }
}
