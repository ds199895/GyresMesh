using Hsy.Core;
using Hsy.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_PolyLine : HS_Geometry
    {
        internal List<HS_Point> points;

        internal int numberOfPoints;

        internal List<HS_Vector> directions;

        internal double[] incLengths;
        int hashcode;
        private HS_GeometryFactory geometryfactory = new HS_GeometryFactory();
        public HS_PolyLine()
        {
        }
    public override string Type { get { return "PolyLine"; } }
    /**
 *
 *
 * @param points
 */
    public HS_PolyLine Create<T>(ICollection<T> points) where T : HS_Coord
        {
      //Console.WriteLine("Create the polyline!");
      this.numberOfPoints = points.Count;
      this.points = new FastList<HS_Point>();
      foreach (HS_Coord p in points)
      {
        this.points.Add(new HS_Point(p));
      }
      getDirections();
      this.hashcode = -1;
      return this;
        }

    /**
     *
     *
     * @param points
     */
    public HS_PolyLine(params HS_Coord[] points)
    {
      this.numberOfPoints = points.Length;
      this.points = new FastList<HS_Point>();
      foreach (HS_Coord p in points)
      {
        this.points.Add(new HS_Point(p));
      }
      getDirections();
      hashcode = -1;

    }
    public HS_PolyLine(List<HS_Point> points)
    {
      numberOfPoints = points.Count;
      this.points = new FastList<HS_Point>();
      foreach (HS_Coord p in points)
      {
        this.points.Add(new HS_Point(p));
      }
      getDirections();
      hashcode = -1;

    }
    public void addPoint(HS_Coord p)
        {
            numberOfPoints++;
            points.Add(new HS_Point(p));
            getDirections();
            hashcode = -1;
        }

        public void removePoint(int i)
        {
            numberOfPoints--;
            points.RemoveAt(i);
            getDirections();
            hashcode = -1;
        }

        /**
		 *
		 */
        private void getDirections()
        {
            directions = new FastList<HS_Vector>();
            incLengths = new double[points.Count - 1];
            for (int i = 0; i < points.Count - 1; i++)
            {
                HS_Vector v = new HS_Vector(points[i], points[i + 1]);
                incLengths[i] = i == 0 ? v.len() : (incLengths[i - 1] + v.len());
                v.unit();
                directions.Add(v);
            }
        }
        public int getNumberOfPoints()
        {
            return numberOfPoints;
        }

        public HS_Segment getSegment(int i)
        {
            if (i < 0 || i > numberOfPoints - 2)
            {
                throw new ArgumentException("Parameter must between 0 and "
                        + (numberOfPoints - 2) + ".");
            }
            return geometryfactory.createSegment(getPoint(i), getPoint(i + 1));
        }
        public HS_Point getPoint(int i)
        {
            if (i < 0 || i > numberOfPoints - 1)
            {
                throw new ArgumentException("Parameter " + i
                        + " must between 0 and " + (numberOfPoints - 1) + ".");
            }
            return points[i];
        }
        public int getNumberSegments()
        {
            return points.Count - 1;
        }
        /*
		 * (non-Javadoc)
		 * @see java.lang.Object#equals(java.lang.Object)
		 */

        public override bool Equals(Object o)
        {
            if (o == this)
            {
                return true;
            }
            if (!(o is HS_PolyLine))
            {
                return false;
            }
            HS_PolyLine L = (HS_PolyLine)o;
            if (getNumberOfPoints() != L.getNumberOfPoints())
            {
                return false;
            }
            for (int i = 0; i < numberOfPoints; i++)
            {
                if (!getPoint(i).Equals(L.getPoint(i)))
                {
                    return false;
                }
            }
            return true;
        }

        /*
		 * (non-Javadoc)
		 * @see java.lang.Object#hashCode()
		 */
        public override int GetHashCode()
        {
            if (hashcode == -1)
            {
                hashcode = points[0].GetHashCode();
                for (int i = 1; i < points.Count; i++)
                {
                    hashcode = 31 * hashcode + points[i].GetHashCode();
                }
            }
            return hashcode;
        }

        /**
		 *
		 *
		 * @return
		 */
        //public HS_CoordCollection getPoints()
        //{
        //	return HS_CoordCollection.getCollection(points);
        //}


    }
}
