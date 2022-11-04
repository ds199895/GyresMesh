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
        public HS_Line()
        {
            origin = new HS_Point();
           HS_Vector dn = new HS_Vector(0, 0, 1);
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
        public HS_Vector getDerivative(double u)
        {
            throw new NotImplementedException();
        }

        public HS_Vector getDirectionOnCurve(double u)
        {
            throw new NotImplementedException();
        }

        public double getLowerU()
        {
            throw new NotImplementedException();
        }

        public HS_Point getPointOnCurve(double u)
        {
            throw new NotImplementedException();
        }

        public double getUpperU()
        {
            throw new NotImplementedException();
        }
    }
}
