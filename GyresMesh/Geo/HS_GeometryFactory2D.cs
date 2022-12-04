using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_GeometryFactory2D
    {
		 HS_Point              origin2D;
	/**
	 *
	 */
	 HS_Vector             X2D;
	/**
	 *
	 */
	 HS_Vector             Y2D;
	 HS_Vector             mX2D;
	/**
	 *
	 */
	 HS_Vector             mY2D;
	/**
	 *
	 */
	//private HS_JTS.ShapeReader shapereader;

		/**
		 *
		 */
		public HS_GeometryFactory2D()
		{
			origin2D = createPoint2D(0, 0);
			X2D = createVector2D(1, 0);
			Y2D = createVector2D(0, 1);
			mX2D = createVector2D(-1, 0);
			mY2D = createVector2D(0, -1);
		}

		public HS_Point createPoint2D(double x,double y)
		{
			return new HS_Point(x, y, 0);
		}

		public HS_Vector createVector2D(double x, double y)
		{
			return new HS_Vector(x, y, 0);
		}

		public HS_Polygon createSimplePolygon(HS_Coord[] points)
        {
			return new HS_Polygon(points);
        }

		public HS_Polygon createPolygonWithHoles<T,K>(List<T>points,List<K>[]innerpoints)where T:HS_Coord where K:HS_Coord
        {
			return new HS_Polygon().Create(points, innerpoints);
        }
        public HS_Polygon createPolygonWithHoles(HS_Coord[] points, HS_Coord[][] innerpoints)
        {
            return new HS_Polygon(points, innerpoints);
        }

        public HS_CoordinateSystem WORLD()
        {
			return HS_CoordinateSystem.WORLD();
        }
	}
}
