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
        public static HS_Point projectOnPlane(HS_Coord p, HS_Plane P)
        {
            HS_Point projection = new HS_Point(p);
            HS_Vector po = new HS_Vector(P.getOrigin(), p);
            HS_Vector n = P.getNormal();
            return projection.subSelf(n.mulSelf(n.dot(po)));
        }

        public static double getArea(HS_Coord p1,HS_Coord p2,
        HS_Coord p3)
        {
            return HS_Math.fastAbs(getSignedArea(p1, p2, p3));
        }

        public static double getSignedArea(HS_Coord p1, HS_Coord p2,
        HS_Coord p3)
        {
            HS_Plane P = new HS_Plane(p1, p2, p3);
            if (P.getNormal().len2() < HS_Epsilon.SQEPSILON)
            {
                return 0.0;
            }
            HS_Vector n = P.getNormal();
            double x = HS_Math.fastAbs(n.xd);
            double y = HS_Math.fastAbs(n.yd);
            double z = HS_Math.fastAbs(n.zd);
            double area = 0;
            int coord = 3;
            if (x >= y && x >= z)
            {
                coord = 1;
            }
            else if (y >= x && y >= z)
            {
                coord = 2;
            }
            switch (coord)
            {
                case 1:
                    area = p1.yd * (p2.zd - p3.zd)
                            + p2.yd * (p3.zd - p1.zd)
                            + p3.yd * (p1.zd - p2.zd);
                    break;
                case 2:
                    area = p1.xd * (p2.zd - p3.zd)
                            + p2.xd * (p3.zd - p1.zd)
                            + p3.xd * (p1.zd - p2.zd);
                    break;
                case 3:
                    area = p1.xd * (p2.yd - p3.yd)
                            + p2.xd * (p3.yd - p1.yd)
                            + p3.xd * (p1.yd - p2.yd);
                    break;
            }
            switch (coord)
            {
                case 1:
                    area *= 0.5 / x;
                    break;
                case 2:
                    area *= 0.5 / y;
                    break;
                case 3:
                    area *= 0.5 / z;
                    break;
            }
            return area;
        }

        public static double getDihedralAngleNorm(HS_Coord n1,
        HS_Coord n2)
        {
            return Math.Acos(getCosDihedralAngleNorm(n1, n2));
        }

        public static double getCosDihedralAngleNorm(HS_Coord n1,
        HS_Coord n2)
        {
            return -HS_Math.clamp(HS_Vector.dot(n1, n2), -1, 1);
        }

    }
}
