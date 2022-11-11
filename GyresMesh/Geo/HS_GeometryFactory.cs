using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_GeometryFactory
    {
        public HS_GeometryFactory()
        {

        }

        public static HS_GeometryFactory instance()
        {
            return new HS_GeometryFactory();
        }

        public HS_Polygon CreateSimplePolygon(List<HS_Coord>points)
        {
            return new HS_Polygon(points);
        }
        //public HS_Map2D createEmbeddedPlane()
        //{
        //    return new HS_PlanarMap();
        //}

        public HS_Plane createPlane( HS_Coord origin,  HS_Coord normal)
        {
            return new HS_Plane(origin, normal);
        }
        public HS_Vector createVector()
        {
            return createVector(0, 0, 0);
        }

        public HS_Vector createVector(double _x, double _y, double _z)
        {
            return new HS_Vector(_x, _y, _z);
        }

    }
}
