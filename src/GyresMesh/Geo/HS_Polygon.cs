using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_Polygon : HS_PolyLine
    {
        static bool USE_JTS = true;
        static bool OPTIMIZE_DEFAULT = true;

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
        private static HS_GeometryFactory gf = new HS_GeometryFactory();

        public HS_Polygon(List<HS_Coord> points)
        {
            //numberOfPoints = points.Count;
            //numberOfShellPoints = points.Count;
            //this.points = new List<HS_Point>();
            //foreach (HS_Coord p in points)
            //{
            //    this.points.Add(new HS_Point(p));

            //}
            //numberOfContours = 1;
            //numberOfPointsPerContour = new int[] { numberOfPoints };
            Create(points);
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
        //public HS_Polygon(FastList<HS_Coord> points)
        //{
        //    numberOfPoints = points.Count;
        //    numberOfShellPoints = points.Count;
        //    this.points = new List<HS_Point>();
        //    foreach (HS_Coord p in points)
        //    {
        //        this.points.Add(new HS_Point(p));

        //    }
        //    numberOfContours = 1;
        //    numberOfPointsPerContour = new int[] { numberOfPoints };

        //}
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
            numberOfPointsPerContour = new int[] { numberOfPoints };
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



        public HS_Polygon(HS_Coord[] points, List<HS_Coord>[] innerpoints)
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
        //simple
        public HS_Polygon Create<T>(List<T> points) where T : HS_Coord
        {
            this.numberOfPoints = points.Count;
            this.numberOfShellPoints = points.Count;
            this.points = new List<HS_Point>();
            foreach (HS_Coord p in points)
            {
                this.points.Add(new HS_Point(p));

            }
            this.numberOfContours = 1;
            this.numberOfPointsPerContour = new int[] { numberOfPoints };
            return this;
        }

        public HS_Polygon Create<T>(T[] points) where T : HS_Coord
        {
            this.numberOfPoints = points.Length;
            this.numberOfShellPoints = points.Length;
            this.points = new List<HS_Point>();
            foreach (HS_Coord p in points)
            {
                this.points.Add(new HS_Point(p));

            }
            this.numberOfContours = 1;
            this.numberOfPointsPerContour = new int[] { numberOfPoints };
            return this;
        }


        //one hole
        public HS_Polygon Create<T, K>(T[] points, K[] innerpoints) where T : HS_Coord where K : HS_Coord
        {
            HS_Vector sn = resetOrientation(points);
            //Console.WriteLine("shell normal:  " + sn);

            HS_Vector hn = GetOrientation(innerpoints);
            //Console.WriteLine("hole normal: " + hn);
            if ((hn + sn).len() != 0)
            {
                Array.Reverse(innerpoints);
            }


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
            return this;
        }
        public HS_Polygon Create<T, K>(T[] points, List<K> innerpoints) where T : HS_Coord where K : HS_Coord
        {
            HS_Vector sn = resetOrientation(points);
            //Console.WriteLine("shell normal:  " + sn);

            HS_Vector hn = GetOrientation(innerpoints);
            //Console.WriteLine("hole normal: " + hn);
            if ((hn + sn).len() != 0)
            {
                innerpoints.Reverse();
            }

            numberOfShellPoints = points.Length;
            numberOfPoints = points.Length + innerpoints.Count;
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
            return this;
        }
        public HS_Polygon Create<T, K>(List<T> points, K[] innerpoints) where T : HS_Coord where K : HS_Coord
        {
            HS_Vector sn = resetOrientation(points);
            //Console.WriteLine("shell normal:  " + sn);

            HS_Vector hn = GetOrientation(innerpoints);
            //Console.WriteLine("hole normal: " + hn);
            if ((hn + sn).len() != 0)
            {
                Array.Reverse(innerpoints);
            }

            numberOfShellPoints = points.Count;
            numberOfPoints = points.Count + innerpoints.Length;
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
            return this;
        }
        public HS_Polygon Create<T, K>(List<T> points, List<K> innerpoints) where T : HS_Coord where K : HS_Coord
        {
            HS_Vector sn = resetOrientation(points);
            //Console.WriteLine("shell normal:  " + sn);

            HS_Vector hn = GetOrientation(innerpoints);
            //Console.WriteLine("hole normal: " + hn);
            if ((hn + sn).len() != 0)
            {
                innerpoints.Reverse();
            }

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
            return this;
        }

        //many holes
        public HS_Polygon Create<T, K>(List<T> points, List<K>[] innerpoints) where T : HS_Coord where K : HS_Coord
        {

            HS_Vector sn = resetOrientation(points);
            //Console.WriteLine("shell normal:  " + sn);
            for (int j = 0; j < innerpoints.Length; j++)
            {

                HS_Vector hn = GetOrientation(innerpoints[j]);
                //Console.WriteLine("hole normal: " + hn);
                if ((hn + sn).len() != 0)
                {
                    innerpoints[j].Reverse();
                }
            }


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
            foreach (List<K> hole in innerpoints)
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
            return this;
        }

        public HS_Polygon Create<T, K>(List<T> points, K[][] innerpoints) where T : HS_Coord where K : HS_Coord
        {
            HS_Vector sn = resetOrientation(points);
            //Console.WriteLine("shell normal:  " + sn);
            for (int j = 0; j < innerpoints.Length; j++)
            {

                HS_Vector hn = GetOrientation(innerpoints[j]);
                //Console.WriteLine("hole normal: " + hn);
                if ((hn + sn).len() != 0)
                {
                    Array.Reverse(innerpoints[j]);
                }
            }
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
            foreach (K[] hole in innerpoints)
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
            return this;
        }


        public HS_Polygon Create<T, K>(T[] points, K[][] innerpoints) where T : HS_Coord where K : HS_Coord
        {
            HS_Vector sn = resetOrientation(points);
            //Console.WriteLine("shell normal:  " + sn);
            for (int j = 0; j < innerpoints.Length; j++)
            {

                HS_Vector hn = GetOrientation(innerpoints[j]);
                //Console.WriteLine("hole normal: " + hn);
                if ((hn + sn).len() != 0)
                {
                    Array.Reverse(innerpoints[j]);
                }
            }
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
            foreach (K[] hole in innerpoints)
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
            return this;
        }
        public HS_Polygon Create<T, K>(T[] points, List<K>[] innerpoints) where T : HS_Coord where K : HS_Coord
        {
            HS_Vector sn = resetOrientation(points);
            //Console.WriteLine("shell normal:  " + sn);
            for (int j = 0; j < innerpoints.Length; j++)
            {

                HS_Vector hn = GetOrientation(innerpoints[j]);
                //Console.WriteLine("hole normal: " + hn);
                if ((hn + sn).len() != 0)
                {
                    innerpoints[j].Reverse();
                }
            }



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
            foreach (List<K> hole in innerpoints)
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
            return this;
        }

        public HS_Vector resetOrientation<T>(List<T> points) where T : HS_Coord
        {
            HS_Vector snormal = GetOrientation(points);
            HS_OrthoProject o = new HS_OrthoProject(snormal);
            Console.WriteLine("sv: " + snormal);
            Console.WriteLine("mode: " + o.mode);
            //if (o.mode > 2)
            //{

            //    points.Reverse();
            //    return snormal * (-1);
            //}
            //else
            //{
            return snormal;

            
        }

        public HS_Vector resetOrientation<T>(T[] points) where T : HS_Coord
        {

            HS_Vector snormal = GetOrientation(points);
            HS_OrthoProject o = new HS_OrthoProject(snormal);
            Console.WriteLine("sv: " + snormal);
            Console.WriteLine("mode: " + o.mode);
            if (o.mode > 2)
            {
                Array.Reverse(points);
                return snormal * (-1);
            }
            else
            {
                return snormal;
            }

        }
        public HS_Vector GetOrientation<T>(List<T> points) where T : HS_Coord
        {
            HS_Vector normal = gf.createVector();
            int ni;
            int nsp = points.Count;
            for (int i = 0; i < nsp; i++)
            {
                ni = (i + 1) % nsp;
                normal += new HS_Vector((points[i].yd - points[ni].yd) * (points[i].zd + points[ni].zd), (points[i].zd - points[ni].zd) * (points[i].xd + points[ni].xd), (points[i].xd - points[ni].xd) * (points[i].yd + points[ni].yd));

            }
            normal = normal.united();
            return normal;
        }
        public HS_Vector GetOrientation<T>(T[] points) where T : HS_Coord
        {
            HS_Vector normal = gf.createVector();
            int ni;
            int nsp = points.Length;
            for (int i = 0; i < nsp; i++)
            {
                ni = (i + 1) % nsp;
                normal += new HS_Vector((points[i].yd - points[ni].yd) * (points[i].zd + points[ni].zd), (points[i].zd - points[ni].zd) * (points[i].xd + points[ni].xd), (points[i].xd - points[ni].xd) * (points[i].yd + points[ni].yd));

            }
            normal = normal.united();
            return normal;
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
                    HS_Vector v = new HS_Vector(points[offset + i], points[inN]);
                    incLengths[offset + i] = i == 0 ? v.GetLength()
                            : incLengths[offset + i - 1] + v.GetLength();
                    v.unit();
                    directions.Add(v);
                }
                offset += n;
            }
        }

        public new int getNumberOfPoints()
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
            for (int i = 0; i < nsp; i++)
            {
                ni = (i + 1) % nsp;
                normal += new HS_Vector((points[i].yd - points[ni].yd) * (points[i].zd + points[ni].zd), (points[i].zd - points[ni].zd) * (points[i].xd + points[ni].xd), (points[i].xd - points[ni].xd) * (points[i].yd + points[ni].yd));

            }
            normal = normal.united();
            return normal;
        }

        public HS_Plane GetPlane(double d)
        {
            HS_Vector normal = GetNormal();
            //Console.WriteLine("normal: " + normal);
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
            for (int i = 0; i < nsp; i++)
            {
                center = (HS_Point)(center + points[i]);

            }
            center = (HS_Point)(center / nsp);
            return center;
        }
        public bool isSimple()
        {
            return numberOfContours == 1;
        }

        public HS_Polygon toPolygon2DOrtho()
        {
            List<HS_Point> shellpoints = new List<HS_Point>();
            HS_Plane P = GetPlane(0);
            //Console.WriteLine("plane: " + P.getNormal());
            HS_OrthoProject OP = new HS_OrthoProject(P);
            for (int i = 0; i < numberOfShellPoints; i++)
            {
                HS_Point p2D = new HS_Point();
                OP.mapPoint3D(points[i], p2D);
                shellpoints.Add(p2D);
            }
            if (isSimple())
            {
                return new HS_Polygon().Create(shellpoints);
            }
            else
            {
                List<HS_Point>[] holepoints = new List<HS_Point>[numberOfContours - 1];
                int index = numberOfShellPoints;
                for (int i = 0; i < numberOfContours - 1; i++)
                {
                    holepoints[i] = new List<HS_Point>();
                    for (int j = 0; j < numberOfPointsPerContour[i + 1]; j++)
                    {
                        HS_Point p2D = new HS_Point();
                        //Console.WriteLine("mode: " + OP.mode);
                        //Console.WriteLine("pre: " + points[1]);
                        OP.mapPoint3D(points[index++], p2D);
                        //Console.WriteLine(p2D);
                        holepoints[i].Add(p2D);
                    }
                }
                return new HS_Polygon().Create(shellpoints, holepoints);
            }
        }
        public int[] getTriangles()
        {
            return this.getTriangles(OPTIMIZE_DEFAULT);
        }
        public int[] getTriangles(bool optimize)
        {
            if (triangles == null)
            {
                if (numberOfShellPoints == 0)
                {
                    return new int[] { };
                }
                else if (numberOfShellPoints < 3)
                {
                    return new int[] { 0, 0, 0 };
                }
                else if (numberOfShellPoints == 3 && numberOfContours == 1)
                {
                    return new int[] { 0, 1, 2 };
                }
                else if (numberOfShellPoints == 4 && numberOfContours == 1)
                {
                    return HS_JTS.PolygonTriangulatorJTS.triangulateQuad(points[0], points[1], points[2], points[3]);
                }
                else
                {
                    if (USE_JTS)
                    {
                        HS_Triangulation2D triangulation = new HS_JTS.PolygonTriangulatorJTS().triangulatePolygon2D(this.toPolygon2DOrtho(), optimize);
                        triangles = triangulation.getTriangles();
                    }
                    else
                    {
                        //triangles
                    }
                }
            }
            return triangles;
        }
    }
}
