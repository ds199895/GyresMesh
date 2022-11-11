using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_Polygon :HS_Polyline
    {

        public int[] triangles;
        /**
         *
         */
        public int numberOfContours;
        /**
         *
         */
        public int[] numberOfPointsPerContour;
        /**
         *
         */
        public int numberOfShellPoints;
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
        public HS_Polygon(HS_Coord[] points)
        {
            numberOfPoints = points.Length;
            numberOfShellPoints = points.Length;
            this.points = new List<HS_Point>();
            foreach (HS_Coord p in points)
            {
                this.points.Add(new HS_Point(p));

            }
            numberOfContours = 1;
            numberOfPointsPerContour = new int[] { numberOfPoints };
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

        public HS_Polygon(List<HS_Coord> points, List<HS_Coord> innerpoints)
        {
            numberOfShellPoints = points.Count;
            numberOfPoints = points.Count + innerpoints.Count;
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
                innerpoints.Count };
        }



        public HS_Polygon(HS_Coord[] points,List<HS_Coord>[] innerpoints)
        {
            numberOfShellPoints = points.Length;
            numberOfPoints = points.Length;
            List<HS_Coord> tmp = new List<HS_Coord>();
            foreach( HS_Coord p in points)
            {
                tmp.Add(p);
            }
            numberOfContours = innerpoints.Length + 1;
            numberOfPointsPerContour = new int[innerpoints.Length + 1];
            numberOfPointsPerContour[0] = numberOfShellPoints;
            int i = 1;
            foreach ( List<HS_Coord > hole in innerpoints)
            {
                foreach( HS_Coord p in hole)
                {
                    tmp.Add(p);
                }
                numberOfPointsPerContour[i++] = hole.Count;
                numberOfPoints += hole.Count;
            }
            this.points = new List<HS_Point>();
            foreach (HS_Coord p in tmp)
            {
                this.points.Add(new HS_Point(p));
            }
            calculateDirections();
        }
        public HS_Polygon(List<HS_Coord> points, List<HS_Coord>[] innerpoints)
        {
            numberOfShellPoints = points.Count;
            numberOfPoints = points.Count;
            List<HS_Coord> tmp = new List<HS_Coord>();
            foreach (HS_Coord p in points)
            {
                tmp.Add(p);
            }
            numberOfContours = innerpoints.Length + 1;
            numberOfPointsPerContour = new int[innerpoints.Length + 1];
            numberOfPointsPerContour[0] = numberOfShellPoints;
            int i = 1;
            foreach (List<HS_Coord> hole in innerpoints)
            {
                foreach (HS_Coord p in hole)
                {
                    tmp.Add(p);
                }
                numberOfPointsPerContour[i++] = hole.Count;
                numberOfPoints += hole.Count;
            }
            this.points = new List<HS_Point>();
            foreach (HS_Coord p in tmp)
            {
                this.points.Add(new HS_Point(p));
            }
            calculateDirections();
        }

        public HS_Polygon(HS_Coord[] points, HS_Coord[][] innerpoints)
        {
            numberOfShellPoints = points.Length;
            numberOfPoints = points.Length;
            List<HS_Coord> tmp = new List<HS_Coord>();
            foreach (HS_Coord p in points)
            {
                tmp.Add(p);
            }
            numberOfContours = innerpoints.Length + 1;
            numberOfPointsPerContour = new int[innerpoints.Length + 1];
            numberOfPointsPerContour[0] = numberOfShellPoints;
            int i = 1;
            foreach (HS_Coord[] hole in innerpoints)
            {
                foreach (HS_Coord p in hole)
                {
                    tmp.Add(p);
                }
                numberOfPointsPerContour[i++] = hole.Length;
                numberOfPoints += hole.Length;
            }
            this.points = new List<HS_Point>();
            foreach (HS_Coord p in tmp)
            {
                this.points.Add(new HS_Point(p));
            }
            calculateDirections();
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

        public int getNumberOfPoints()
            {
                return points.Count;
            }

        /**
         *
         *
         * @return
         */
        public int getNumberOfShellPoints()
        {
            return numberOfShellPoints;
        }

        /**
         *
         *
         * @return
         */
        public int getNumberOfHoles()
        {
            return numberOfContours - 1;
        }

        /**
         *
         *
         * @return
         */
        public int GetNumberOfContours()
        {
            return numberOfContours;
        }

        /**
         *
         *
         * @return
         */
        public int[] GetNumberOfPointsPerContour()
        {
            return numberOfPointsPerContour;
        }
        public HS_Point GetPoint(int i)
        {
            return points[i];
        }
        public HS_Vector GetNormal()
        {
            HS_Vector normal = gf.createVector();
            int ni;
            int nsp = getNumberOfShellPoints();
            for(int i = 0; i < nsp; i++)
            {
                ni = (i + 1)% nsp;
                normal += new HS_Vector((points[i].yd - points[ni].yd) * (points[i].zd + points[ni].zd), (points[i].zd - points[ni].zd) * (points[i].xd + points[ni].xd), (points[i].xd - points[ni].xd) * (points[i].yd + points[ni].yd));

            }
            normal.united();
            return normal;
        }

        public HS_Plane GetPlane(double d)
        {
            HS_Vector normal = GetNormal();
            if (normal.len2() < 0.5)
            {
                return null;
            }
            return gf.createPlane(points[0].addMul(d, normal), normal);
        }

        public HS_Point GetCenter()
        {
            int nsp = getNumberOfShellPoints();
            HS_Point center = new HS_Point();
            for(int i=0; i < nsp; i++)
            {
                center = (HS_Point)(center +points[i]);

            }
            center /= nsp;
            return center; 
        }
        //public int[] getTriangles(bool optimize)
        //{
        //    if (triangles == null)
        //    {
        //        if (numberOfShellPoints == 0)
        //        {
        //            return new int[] { };
        //        }
        //        else if (numberOfShellPoints < 3)
        //        {
        //            return new int[] { 0, 0, 0 };
        //        }
        //        else if (numberOfShellPoints == 3 && numberOfContours == 1)
        //        {
        //            return new int[] { 0, 1, 2 };
        //        }
        //        else if (numberOfShellPoints == 4 && numberOfContours == 1)
        //        {
        //            return
        //        }
        //        {

        //        }
        //    }
        //}
    }
}
