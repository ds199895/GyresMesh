using Hsy.Core;
using Hsy.Geo;
using Hsy.HsMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_Segment : HS_Line, IComparable<HS_Segment>
    {

        protected double length;
        /**
         *
         */
        protected HS_Point endpoint;
        private HS_GeometryFactory geometryfactory = new HS_GeometryFactory();

        public HS_Segment() : base()
        {
            endpoint = new HS_Point();
            length = 0;
        }

        /**
		 *
		 *
		 * @return
		 */
        public new static HS_Segment X()
        {
            return new HS_Segment(0, 0, 0, 1, 0, 0);
        }

        /**
		 *
		 *
		 * @return
		 */
        public static HS_Segment Y()
        {
            return new HS_Segment(0, 0, 0, 0, 1, 0);
        }

        /**
		 *
		 *
		 * @return
		 */
        public static HS_Segment Z()
        {
            return new HS_Segment(0, 0, 0, 0, 0, 1);
        }

        public HS_Segment(HS_Coord o, HS_Coord d, double l) : base(o, d)
        {

            length = l;
            endpoint = new HS_Point(direction).mul(l).add(origin);
        }

        /**
		 *
		 *
		 * @param p1
		 * @param p2
		 */
        public HS_Segment(HS_Coord p1, HS_Coord p2) : base(p1, new HS_Vector(p1, p2))
        {

            endpoint = new HS_Point(p2);
            length = Math.Sqrt(HS_CoordOp3D.getSqDistance3D(p1, p2));
        }

        /**
		 *
		 *
		 * @param p1x
		 * @param p1y
		 * @param p1z
		 * @param p2x
		 * @param p2y
		 * @param p2z
		 */
        public HS_Segment(double p1x, double p1y, double p1z, double p2x, double p2y, double p2z) : base(new HS_Point(p1x, p1y, p1z), new HS_Vector(p2x - p1x, p2y - p1y, p2z - p1z))
        {

            endpoint = new HS_Point(p2x, p2y, p2z);
            length = Math.Sqrt(HS_CoordOp3D.getSqDistance3D(origin, endpoint));
        }

        /**
		 *
		 *
		 * @param t
		 * @return
		 */

        public HS_Point getParametricPoint(double t)
        {
            HS_Point result = new HS_Point(direction);
            result.scaleSelf(HS_Math.clamp(t, 0, 1) * length);
            result.addSelf(origin);
            return result;
        }

        /**
		 *
		 *
		 * @param t
		 * @param result
		 */

        public void getParametricPointInto(double t, HS_MutableCoord result)
        {
            result.Set(new HS_Vector(direction).mulSelf(HS_Math.clamp(t, 0, 1) * length).addSelf(origin));
        }

        /**
		 *
		 *
		 * @return
		 */
        public HS_Coord getEndpoint()
        {
            return endpoint;
        }

        public HS_Point getCenter()
        {
            return new HS_Point(endpoint).addSelf(origin).mulSelf(0.5);
        }

        /**
		 *
		 *
		 * @return
		 */
        public double getLength()
        {
            return length;
        }

        /**
		 *
		 *
		 * @param segs
		 * @return
		 */
        public static List<HS_Segment> negate(List<HS_Segment> segs)
        {
            List<HS_Segment> neg = new FastList<HS_Segment>();
            for (int i = 0; i < segs.Count; i++)
            {
                neg.Add(segs[i].negate());
            }
            return neg;
        }

        /**
		 *
		 *
		 * @return
		 */
        public HS_Segment negate()
        {
            return new HS_Segment(endpoint, origin);
        }

        /**
		 *
		 */
        public void reverse()
        {
            HS_Coord tmp = origin;
            origin = new HS_Point(endpoint);
            endpoint = new HS_Point(tmp);
            direction.mulSelf(-1);
        }

        /*
		 * (non-Javadoc)
		 * @see HSlut.geom.HS_Curve#curvePoint(double)
		 */

        public override HS_Point getPointOnCurve(double u)
        {
            if (u < 0 || u > 1)
            {
                return null;
            }
            return this.getParametricPoint(u);
        }


        public override double getLowerU()
        {
            return 0;
        }


        public override double getUpperU()
        {
            return 1;
        }

        /*
		 * (non-Javadoc)
		 * @see HSlut.geom.HS_Curve#curveDirection(double)
		 */

        public override HS_Vector getDirectionOnCurve(double u)
        {
            return new HS_Vector(direction);
        }

        /*
		 * (non-Javadoc)
		 * @see HSlut.geom.HS_Curve#curveDerivative(double)
		 */

        public override HS_Vector getDerivative(double u)
        {
            return new HS_Vector(direction);
        }

        public int CompareTo(HS_Segment other)
        {
            throw new NotImplementedException();
        }
    }
}
