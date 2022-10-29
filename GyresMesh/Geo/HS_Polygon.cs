using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_Polygon :HS_Polyline
    {

        int[] triangles;
        /**
         *
         */
        int numberOfContours;
        /**
         *
         */
        int[] numberOfPointsPerContour;
        /**
         *
         */
        int numberOfShellPoints;
        /**
         *
         */
        private static  HS_GeometryFactory gf	= new HS_GeometryFactory();

        public HS_Polygon(List<HS_Coord>points)
        {
            numberOfPoints = points.Count;
            numberOfShellPoints = points.Count;
            this.points = new List<HS_Point>();
            foreach(HS_Coord p in points)
            {
                this.points.Add(new HS_Point(p));

            }
            numberOfContours = 1;
            numberOfPointsPerContour = new int[] { numberOfPoints};
        }


        public HS_Polygon()
        {
            numberOfPoints = 0;
            numberOfShellPoints = 0;
            this.points = new List<HS_Point>();
            numberOfContours = 0;
            numberOfPointsPerContour = new int[] { };
        }

    }
}
