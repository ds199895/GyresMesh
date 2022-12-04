using Hsy.HsMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_Line : HS_Geometry, HS_Curve
    {
        protected HS_Point origin;
        protected HS_Vector direction;
        public static HS_Line X()
        {
            return new HS_Line(0, 0, 0, 1, 0, 0);
        }
        public static HS_Line Y()
        {
            return new HS_Line(0, 0, 0, 0, 1, 0);
        }
        public static HS_Line Z()
        {
            return new HS_Line(0, 0, 0, 0, 0, 1);
        }
        public HS_Line()
        {
            origin = new HS_Point();
           HS_Vector dn = new HS_Vector(0, 0, 1);
            direction = dn;
        }
        public HS_Line(HS_Coord o, HS_Coord d)
        {
            origin = new HS_Point(o);
            HS_Vector dn = new HS_Vector(d);
            dn.unit();
            direction = dn;
        }
        public HS_Line(double ox, double oy, double oz, double dx,double dy, double dz): this(new HS_Point(ox, oy, oz), new HS_Vector(dx, dy, dz))
        {
            
        }
        public HS_Line(double ox,double oy,double dx,double dy): this(new HS_Point(ox, oy), new HS_Vector(dx, dy))
        {
            
        }





        public void setFromPoints(HS_Coord p1, HS_Coord p2)
        {
            set(p1, p2);
        }
        protected void set(HS_Coord o,HS_Coord d)
        {
            origin = new HS_Point(o);
            HS_Vector dn = new HS_Vector(d);
            dn.unit();
            direction = dn;
        }
        /**
 *
 *
 * @return
 */
        public HS_Coord getDirection()
        {
            return direction;
        }

        /**
         * Get vector perpendicular to the line
         *
         * @return
         */
        public HS_Vector getNormal()
        {
            HS_Vector n = new HS_Vector(0, 0, 1);
            n = n.cross(direction);
            //double d = n.unit();
            if (n.unit())
            {
                n = new HS_Vector(1, 0, 0);
            }
            return n;
        }

        /**
         *
         *
         * @return
         */
        public HS_Coord getOrigin()
        {
            return origin;
        }
        public virtual HS_Vector getDerivative(double u)
        {
            throw new NotImplementedException();
        }

        public virtual  HS_Vector getDirectionOnCurve(double u)
        {
            throw new NotImplementedException();
        }

        public virtual double getLowerU()
        {
            throw new NotImplementedException();
        }

        public virtual HS_Point getPointOnCurve(double u)
        {
            throw new NotImplementedException();
        }

        public virtual double getUpperU()
        {
            throw new NotImplementedException();
        }
    }
}
