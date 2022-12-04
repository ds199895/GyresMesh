using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_GeometryOp
    {

        public static double GetDistance3D(HS_Coord p, HS_Coord q)
        {
            return Math.Sqrt((q.xd - p.xd) * (q.xd - p.xd) + (q.yd - p.yd) * (q.yd - p.yd)+ (q.zd - p.zd) * (q.zd - p.zd));
        }
    }
}
