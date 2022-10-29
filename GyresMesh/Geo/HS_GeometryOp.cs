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
            return Math.Sqrt((q.X() - p.X()) * (q.X() - p.X()) + (q.Y() - p.Y()) * (q.Y() - p.Y())+ (q.Z() - p.Z()) * (q.Z() - p.Z()));
        }
    }
}
