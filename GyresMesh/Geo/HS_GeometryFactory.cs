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

    }
}
