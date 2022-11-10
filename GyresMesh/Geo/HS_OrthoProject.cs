using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_OrthoProject : HS_Map2D
    {

        public static  int YZ = 0;
        /**
         *
         */
        public static  int XZ = 1;
        /**
         *
         */
        public static  int XY = 2;
        /**
         *
         */
        public static  int YZrev = 3;
        /**
         *
         */
        public static  int XZrev = 4;
        /**
         *
         */
        public static  int XYrev = 5;
        private int mode;
        public HS_OrthoProject()
        {
            this(XY);
        }
        public HS_OrthoProject(HS_Coord v)
        {
            set(v);
        }
        public HS_OrthoProject( HS_Plane P)
        {
            set(P.getNormal());
        }
        public void set( HS_Coord c)
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
        public void mapPoint3D(HS_Coord p,HS_MutableCoord result)
        {
            
            switch (mode) {
                case XY:
                    result.Set(p.xd, p.yd, 0);
                    break;
                case YZ:
            }

        }

        public void mapPoint3D(double x, double y, double z, out HS_MutableCoord result)
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

        public void mapVector3D(HS_Coord p, out HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public void mapVector3D(double x, double y, double z, out HS_MutableCoord result)
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

        public void unmapPoint2D(HS_Coord p, out HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public void unmapPoint2D(double x, double y, double z, out HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public HS_Coord unmapPoint2D(HS_Coord p)
        {
            throw new NotImplementedException();
        }

        public HS_Coord unmapPoint2D(double x, double y, double z)
        {
            throw new NotImplementedException();
        }

        public void unmapPoint3D(HS_Coord p, out HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public void unmapPoint3D(double x, double y, double z, out HS_MutableCoord result)
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

        public void unmapVector2D(HS_Coord p, out HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public void unmapVector2D(double x, double y, double z, out HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public HS_Coord unmapVector2D(HS_Coord p)
        {
            throw new NotImplementedException();
        }

        public HS_Coord unmapVector2D(double x, double y, double z)
        {
            throw new NotImplementedException();
        }

        public void unmapVector3D(HS_Coord p, out HS_MutableCoord result)
        {
            throw new NotImplementedException();
        }

        public void unmapVector3D(double x, double y, double z, out HS_MutableCoord result)
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
