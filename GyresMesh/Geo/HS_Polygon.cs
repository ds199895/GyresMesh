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
            numberOfPointsPerContour = new int[] { numberOfPoints};
        }


        public HS_Polygon(HS_Coord[] points, HS_Coord[] innerpoints)
        {
            numberOfShellPoints = points.Length;
            numberOfPoints = points.Length + innerpoints.Length;
            List<HS_Coord> tmp = new List<HS_Coord>();
            foreach (HS_Coord p in points)
            {
                tmp.Add(p);
            }
            foreach (HS_Coord p in innerpoints)
            {
                tmp.Add(p);
            }
            this.points = new List<HS_Point>();
            foreach (HS_Coord p in tmp)
            {
                this.points.Add(new HS_Point(p));
            }
            calculateDirections();
            numberOfContours = 2;
            numberOfPointsPerContour = new int[] { numberOfShellPoints,
                innerpoints.Length };
        }

        private void calculateDirections()
        {
            directions = new List<HS_Vector>();
            incLengths = new double[numberOfPoints];
            int offset = 0;
            for (int j = 0; j < numberOfContours; j++)
            {
                int n = numberOfPointsPerContour[j];
                for (int i = 0; i < n; i++)
                {
                    int inN = offset + (i + 1) % n;
                    HS_Vector v = new HS_Vector(points[offset + i],points[inN]);
                    incLengths[offset + i] = i == 0 ? v.GetLength()
                            : incLengths[offset + i - 1] + v.GetLength();
                    v.unit();
                    directions.Add(v);
                }
                offset += n;
            }
        }


        public int[] getTriangles(bool optimize)
        {
            if (triangles == null)
            {
                if (numberOfShellPoints == 0)
                {
                    return new int[] { };
                }else if (numberOfShellPoints < 3)
                {
                    return new int[] { 0, 0, 0 };
                }else if (numberOfShellPoints == 3 && numberOfContours == 1)
                {
                    return new int[] { 0, 1, 2 };
                }else if (numberOfShellPoints == 4 && numberOfContours == 1)
                {
                    return 
                }
                {

                }
            }
        }
    }
}
