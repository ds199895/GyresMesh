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
    }
}
