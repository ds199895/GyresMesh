using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_PlanarMap : HS_Map2D
    {
        private double offset;
        int id;
        private HS_Transform3D T2D3D;
        private int mode;
        public static int YZ = 0;
        public static int XZ= 1;
        public static int XY = 2;
        public static int YZrev = 3;
        public static int XZrev = 4;
        public static int XYrev = 5;
        public static int PLANE = 6;
        public static HS_GeometryFactory geometryFactory;

        public HS_Point _origin;
        public HS_Vector _x;
        public HS_Vector _y;
        public HS_Vector _z;

       



        public HS_PlanarMap(HS_Coord c)
        {
            if (Math.Abs(c.xd) > Math.Abs(c.yd))
            {
                mode = Math.Abs(c.xd) > Math.Abs(c.zd) ? YZ : XY;
            }
            else
            {
                mode = Math.Abs(c.yd) > Math.Abs(c.zd) ? XZ : XY;
            }
            if (mode < 0 || mode > 5)
            {
                throw new IndexOutOfRangeException();
            }
            if (mode == YZ)
            {
                
            }


        }



        public void mapPoint3D(HS_Coord p, HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public void mapPoint3D(double x, double y, double z, HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public HS_Coord mapPoint3D(HS_Coord p)
        {
            throw new NotImplementedException();
        }

        public HS_Coord mapPoint3D(double x, double y, double z)
        {
            throw new NotImplementedException();
        }

        public void mapVector3D(HS_Coord p, HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public void mapVector3D(double x, double y, double z, HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public HS_Coord mapVector3D(HS_Coord p)
        {
            throw new NotImplementedException();
        }

        public HS_Coord mapVector3D(double x, double y, double z)
        {
            throw new NotImplementedException();
        }

        public void unmapPoint2D(HS_Coord p, HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public void unmapPoint2D(double x, double y, HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public HS_Coord unmapPoint2D(HS_Coord p)
        {
            throw new NotImplementedException();
        }

        public HS_Coord unmapPoint2D(double x, double y)
        {
            throw new NotImplementedException();
        }

        public void unmapPoint3D(HS_Coord p, HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public void unmapPoint3D(double x, double y, double z, HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public HS_Coord unmapPoint3D(HS_Coord p)
        {
            throw new NotImplementedException();
        }

        public HS_Coord unmapPoint3D(double x, double y, double z)
        {
            throw new NotImplementedException();
        }

        public void unmapVector2D(HS_Coord p, HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public void unmapVector2D(double x, double y, HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public HS_Coord unmapVector2D(HS_Coord p)
        {
            throw new NotImplementedException();
        }

        public HS_Coord unmapVector2D(double x, double y)
        {
            throw new NotImplementedException();
        }

        public void unmapVector3D(HS_Coord p, HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public void unmapVector3D(double x, double y, double z, HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public HS_Coord unmapVector3D(HS_Coord p)
        {
            throw new NotImplementedException();
        }

        public HS_Coord unmapVector3D(double x, double y, double z)
        {
            throw new NotImplementedException();
        }
    }
}
