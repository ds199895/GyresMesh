using GeoAPI.Geometries;
using Hsy.HsMath;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_JTS
    {
        private static GeometryFactory JTSgf=new GeometryFactory(new PrecisionModel(HS_Epsilon.SCALE));
        //private static HS_Map2D XY=(new HS_GeometryFactory()).CreateSimplePolygon;

        private HS_JTS()
        {
        }
        public static Coordinate toJTSCoordinate2D(HS_Coord point,int i)
        {
            return new Coordinate(point.X(), point.Y(), i);
        }
        public static Polygon toJTSPolygon2D(HS_Polygon poly)
        {
            int[] npc = poly.GetNumberOfPointsPerContour();
            Coordinate[] coords = new Coordinate[npc[0] + 1];
            int i = 0;
            for (i = 0; i < npc[0]; ++i)
            {
                coords[i] = toJTSCoordinate2D(poly.GetPoint(i), i);
            }
            
            coords[i] = toJTSCoordinate2D(poly.GetPoint(0), 0);
            //LinearRing shell = JTSgf.CreateLinearRing(coords) as LinearRing;
            LinearRing shell = new LinearRing(coords);
            LinearRing[] holes = new LinearRing[poly.getNumberOfHoles()];
            int index = poly.getNumberOfShellPoints();

            for (i = 0; i < poly.getNumberOfHoles(); ++i)
            {
                coords = new Coordinate[npc[i + 1] + 1];
                coords[npc[i + 1]] = toJTSCoordinate2D(poly.GetPoint(index), index);

                for (int j = 0; j < npc[i + 1]; ++j)
                {
                    coords[j] = toJTSCoordinate2D(poly.GetPoint(index), index);
                    ++index;
                }


                //holes[i] = JTSgf.CreateLinearRing(coords) as LinearRing;
                holes[i] = new LinearRing(coords);
            }

            //return JTSgf.CreatePolygon(shell, holes) as Polygon;
            return new Polygon(shell, holes) ;
        }


    }
}
