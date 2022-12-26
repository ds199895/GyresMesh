using Hsy.HsMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_Ray : HS_Line
    {
        public new static HS_Ray X() { return new HS_Ray(0, 0, 0, 1, 0, 0); }
        public new static HS_Ray Y() { return new HS_Ray(0, 0, 0, 0, 1, 0); }
        public new static HS_Ray Z() { return new HS_Ray(0, 0, 0, 0, 0, 1); }

        /**
		 *
		 */
        public HS_Ray()
        {
            origin = new HS_Point();
            direction = new HS_Vector(1, 0, 0);
        }

        /**
         *
         *
         * @param o
         * @param d
         */
        public HS_Ray(HS_Coord o, HS_Coord d)
        {
            origin = new HS_Point(o);
            direction = new HS_Vector(d);
            HS_Vector dn = new HS_Vector(d);
            dn.unit();
            direction = dn;
        }

        /**
         *
         *
         * @param ox
         * @param oy
         * @param oz
         * @param dx
         * @param dy
         * @param dz
         */
        public HS_Ray(double ox, double oy, double oz,
                 double dx, double dy, double dz)
        {
            origin = new HS_Point(ox, oy, oz);
            HS_Vector dn = new HS_Vector(dx, dy, dz);
            dn.unit();
            direction = dn;
        }

        /*
         * (non-Javadoc)
         * @see java.lang.Object#toString()
         */

        public override String ToString()
        {
            return "Ray: " + origin.ToString() + " " + direction.ToString();
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_Linear#set(HSlut.geom.HS_Coordinate,
         * HSlut.geom.HS_Coordinate)
         */

        public new void set(HS_Coord o, HS_Coord d)
        {
            origin = new HS_Point(o);
            HS_Vector dn = new HS_Vector(d);
            dn.unit();
            direction = dn;
        }

        /**
         *
         *
         * @param p1
         * @param p2
         */

        public new void setFromPoints(HS_Coord p1, HS_Coord p2)
        {
            origin = new HS_Point(p1);
            HS_Vector dn = new HS_Vector(p1, p2);
            dn.unit();
            direction = dn;
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_Linear#getPointOnLine(double)
         */

        public HS_Point getPoint(double t)
        {
            HS_Point result = new HS_Point(direction);
            result.scaleSelf(HS_Math.max(0, t));
            result.addSelf(origin);
            return result;
        }


        public void getPointInto(double t, HS_MutableCoord p)
        {
            if (t > 0)
            {
                p.Set(direction.mul(t).addSelf(origin));
            }
            else
            {
                p.Set(origin);
            }
        }


        public HS_Point getParametricPoint(double t)
        {
            HS_Point result = new HS_Point(direction);
            result.scaleSelf(HS_Math.max(0, t));
            result.addSelf(origin);
            return result;
        }


        public void getParametricPointInto(double t,
                 HS_MutableCoord p)
        {
            if (t > 0)
            {
                p.Set(direction.mul(t).addSelf(origin));
            }
            else
            {
                p.Set(origin);
            }
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_Linear#getOrigin()
         */

        public new HS_Coord getOrigin()
        {
            return origin;
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_Linear#getDirection()
         */

        public new HS_Coord getDirection()
        {
            return direction;
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_Curve#curvePoint(double)
         */

        public new HS_Point getPointOnCurve(double u)
        {
            if (u < 0)
            {
                return null;
            }
            return this.getPoint(u);
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_Curve#curveDirection(double)
         */

        public new HS_Vector getDirectionOnCurve(double u)
        {
            return new HS_Vector(direction);
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_Curve#curveDerivative(double)
         */

        public new HS_Vector getDerivative(double u)
        {
            return new HS_Vector(direction);
        }


        public new double getLowerU()
        {
            return 0;
        }


        public new double getUpperU()
        {
            return Double.PositiveInfinity;
        }

        /*
         * (non-Javadoc)
         * @see java.lang.Object#Equals(java.lang.Object)
         */

        public override bool Equals(Object o)
        {
            if (o == this)
            {
                return true;
            }
            if (!(o is HS_Ray))
            {
                return false;
            }
            return origin.Equals(((HS_Ray)o).getOrigin())
                    && direction.Equals(((HS_Ray)o).getDirection());
        }

        /*
         * (non-Javadoc)
         * @see java.lang.Object#hashCode()
         */

        public override int GetHashCode()
        {
            return 31 * origin.GetHashCode() + direction.GetHashCode();
        }


        public HS_Ray apply2D(HS_Transform2D T)
        {
            return new HS_Ray(origin.apply2D(T), direction.apply2D(T));
        }


        public HS_Ray apply2DSelf(HS_Transform2D T)
        {
            origin.apply2DSelf(T);
            direction.apply2DSelf(T);
            return this;
        }


        public HS_Ray apply(HS_Transform3D T)
        {
            return new HS_Ray(origin.apply(T), direction.apply(T));
        }


        public HS_Ray applySelf(HS_Transform3D T)
        {
            origin.applySelf(T);
            direction.applySelf(T);
            return this;
        }


    }
}
