using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_CoordOp3D
    {
        public static double getSqDistance3D(HS_Coord p1,HS_Coord p2)
        {
            return (p1.xd - p2.xd) * (p1.xd - p2.xd) + (p1.yd - p2.yd) * (p1.yd - p2.yd) + (p1.zd - p2.zd) * (p1.zd - p2.zd);
        }
        public static double getDistance3D(HS_Coord p1, HS_Coord p2)
        {
            return Math.Sqrt((p1.xd - p2.xd) * (p1.xd - p2.xd) + (p1.yd - p2.yd) * (p1.yd - p2.yd) + (p1.zd - p2.zd) * (p1.zd - p2.zd));
        }
        public static double getDistance3D(double px, double py,double pz,double qx,double qy,double qz)
        {
            return Math.Sqrt((px- qx) * (px - qx) + (py - qy) * (py - qy) + (pz - qz) * (pz - qz));
        }

        public static double getAngleBetween(double ux, double uy, double uz, double vx,
        double vy, double vz)
        {
            HS_Vector v0 = new HS_Vector(ux, uy, uz);
            HS_Vector v1 = new HS_Vector(vx, vy, vz);
            v0.unit();
            v1.unit();
            double d = v0.dot(v1);
            if (d < -1.0)
            {
                d = -1.0;
            }
            if (d > 1.0)
            {
                d = 1.0;
            }
            return Math.Acos(d);
        }
    }
}
