using Hsy.HsMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_GeometryOp3D : HS_GeometryOp2D
    {
        private static HS_GeometryFactory gf = new HS_GeometryFactory();

        public HS_GeometryOp3D()
        {

        }
        public static double getSqDistanceToLine2D(HS_Coord p,
        HS_Coord a, HS_Coord b)
        {
             HS_Vector ab = new HS_Vector(a, b);
             HS_Vector ac = new HS_Vector(a, p);
             double e = ac.dot2D(ab);
             double f = ab.dot2D(ab);
            return ac.dot2D(ac) - e * e / f;
        }


        public static double getSqDistance3D(HS_Coord p, HS_Line L)
        {
            HS_Coord ab = L.getDirection();
            HS_Vector ac = new HS_Vector(L.getOrigin(), p);
            double e = ac.dot(ab);
            double f = HS_Vector.dot(ab, ab);
            return ac.dot(ac) - e * e / f;
        }
        public static bool pointInTriangleBary3D(HS_Coord p, HS_Coord A, HS_Coord B, HS_Coord C)
        {
            if (p == A)
            {
                return false;
            }
            else if (p == B)
            {
                return false;
            }
            else if (p == C)
            {
                return false;
            }
            else if (HS_Epsilon.isZeroSq(getSqDistanceToLine2D(A, B, C)))
            {
                return false;
            }
            else
            {
                HS_Vector v0 = (new HS_Vector(C)) - A;
                HS_Vector v1 = (new HS_Vector(B))-A;
                HS_Vector v2 = (new HS_Vector(p))-A;
                double dot00 = v0.dot(v0);
                double dot01 = v0.dot(v1);
                double dot02 = v0.dot(v2);
                double dot11 = v1.dot(v1);
                double dot12 = v1.dot(v2);
                double invDenom = 1.0D / (dot00 * dot11 - dot01 * dot01);
                double u = (dot11 * dot02 - dot01 * dot12) * invDenom;
                double v = (dot00 * dot12 - dot01 * dot02) * invDenom;
                return u > HS_Epsilon.EPSILON && v > HS_Epsilon.EPSILON && u + v < 1.0D - HS_Epsilon.EPSILON;
            }
        }

    }
}
