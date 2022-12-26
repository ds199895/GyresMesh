using Hsy.HsMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_CoordOp3D
    {

        /**
         *
         *
         * @param ux
         * @param uy
         * @param uz
         * @param vx
         * @param vy
         * @param vz
         * @return
         */
        public static double dot( double ux,  double uy,  double uz,  double vx,  double vy,
                 double vz)
        {
             double k0 = ux * vx;
             double k1 = uy * vy;
             double k2 = uz * vz;
             double exp0 = HS_Math.getExp(k0);
             double exp1 = HS_Math.getExp(k1);
             double exp2 = HS_Math.getExp(k2);
            if (exp0 < exp1)
            {
                if (exp0 < exp2)
                {
                    return k1 + k2 + k0;
                }
                else
                {
                    return k0 + k1 + k2;
                }
            }
            else
            {
                if (exp1 < exp2)
                {
                    return k0 + k2 + k1;
                }
                else
                {
                    return k0 + k1 + k2;
                }
            }
        }



        public static double getDistance3D(HS_Coord p1, HS_Coord p2)
        {
            return Math.Sqrt((p1.xd - p2.xd) * (p1.xd - p2.xd) + (p1.yd - p2.yd) * (p1.yd - p2.yd) + (p1.zd - p2.zd) * (p1.zd - p2.zd));
        }
        public static double getDistance3D(double px, double py,double pz,double qx,double qy,double qz)
        {
            return Math.Sqrt((px- qx) * (px - qx) + (py - qy) * (py - qy) + (pz - qz) * (pz - qz));
        }

        public static double getLength3D( double ux,  double uy,  double uz)
        {
            return Math.Sqrt(ux * ux + uy * uy + uz * uz);
        }

        /**
         *
         *
         * @param p
         * @return
         */
        public static double getLength3D( HS_Coord p)
        {
            return Math.Sqrt(p.xd * p.xd + p.yd * p.yd + p.zd * p.zd);
        }

        /**
         *
         *
         * @param ux
         * @param uy
         * @param uz
         * @return
         */
        public static double getSqLength3D( double ux,  double uy,  double uz)
        {
            return ux * ux + uy * uy + uz * uz;
        }

        /**
         *
         *
         * @param p
         * @return
         */
        public static double getSqLength3D( HS_Coord p)
        {
            return p.xd * p.xd + p.yd * p.yd + p.zd * p.zd;
        }

        /**
         *
         *
         * @param px
         * @param py
         * @param pz
         * @param qx
         * @param qy
         * @param qz
         * @return
         */
        public static double getSqDistance3D( double px,  double py,  double pz,  double qx,
                 double qy,  double qz)
        {
            return (qx - px) * (qx - px) + (qy - py) * (qy - py) + (qz - pz) * (qz - pz);
        }


        /**
         *
         *
         * @param p
         * @param q
         * @return
         */
        public static double getSqDistance3D(HS_Coord p, HS_Coord q)
        {
            return (q.xd - p.xd) * (q.xd - p.xd) + (q.yd - p.yd) * (q.yd - p.yd)+ (q.zd - p.zd) * (q.zd - p.zd);
        }
        public static double getAngleBetween(double ux, double uy, double uz, double vx,double vy, double vz)
        {
            HS_Vector v0 = new HS_Vector(ux, uy, uz);
            HS_Vector v1 = new HS_Vector(vx, vy, vz);
            v0.unit();
            v1.unit();
            double d = v0.dot(v1);
            if (d < -1.0)
            {
                d = -1.0;
            }
            if (d > 1.0)
            {
                d = 1.0;
            }
            return Math.Acos(d);
        }
        public static double getCosAngleBetween( double cx,  double cy,  double cz,  double px,double py,  double pz,  double qx,  double qy,  double qz)
        {
             HS_Vector v0 = new HS_Vector(px - cx, py - cy, pz - cz);
             HS_Vector v1 = new HS_Vector(qx - cx, qy - cy, qz - cz);
            v0.unit();
            v1.unit();
            double d = v0.dot(v1);
            if (d < -1.0)
            {
                d = -1.0;
            }
            if (d > 1.0)
            {
                d = 1.0;
            }
            return d;
        }

        public static double getCosAngleBetweenNorm( double ux,  double uy,  double uz,  double vx,
         double vy,  double vz)
        {
             HS_Vector v0 = new HS_Vector(ux, uy, uz);
             HS_Vector v1 = new HS_Vector(vx, vy, vz);
            double d = v0.dot(v1);
            if (d < -1.0)
            {
                d = -1.0;
            }
            if (d > 1.0)
            {
                d = 1.0;
            }
            return d;
        }

        /**
         *
         *
         * @param ux
         * @param uy
         * @param uz
         * @param vx
         * @param vy
         * @param vz
         * @return
         */
        public static double[] cross(double ux, double uy, double uz, double vx, double vy,
                 double vz)
        {
            return new double[] { uy * vz - uz * vy, uz * vx - ux * vz, ux * vy - uy * vx };
        }

        /**
         *
         *
         * @param px
         * @param py
         * @param qx
         * @param qy
         * @param t
         * @return
         */
        public static double[] interpolate( double px,  double py,  double qx,  double qy,
                 double t)
        {
            return new double[] { px + t * (qx - px), py + t * (qy - py) };
        }

        /**
         *
         *
         * @param px
         * @param py
         * @param pz
         * @param qx
         * @param qy
         * @param qz
         * @param t
         * @return
         */
        public static double[] interpolate( double px,  double py,  double pz,  double qx,
                 double qy,  double qz,  double t)
        {
            return new double[] { px + t * (qx - px), py + t * (qy - py), pz + t * (qz - pz) };
        }
        public static bool isCollinear(HS_Coord o, HS_Coord p, HS_Coord q)
        {
            if (HS_Epsilon.isZeroSq(HS_GeometryOp3D.getSqDistanceToPoint3D(p, q)))
            {
                return true;
            }
            if (HS_Epsilon.isZeroSq(HS_GeometryOp3D.getSqDistanceToPoint3D(o, q)))
            {
                return true;
            }
            if (HS_Epsilon.isZeroSq(HS_GeometryOp3D.getSqDistanceToPoint3D(o, p)))
            {
                return true;
            }
            return HS_Epsilon.isZeroSq(HS_Vector.sub(o, p).crossSelf(HS_Vector.sub(o, q)).len2());
        }

        public static bool isOrthogonal(HS_Coord v0, HS_Coord v1)
        {
            return HS_Epsilon.isZero(
                    dot(v0.xd, v0.yd, v0.zd, v1.xd, v1.yd, v1.zd) / (getLength3D(v0) * getLength3D(v1)));
        }

        public static bool isOrthogonal(HS_Coord v0, HS_Coord v1, double epsilon)
        {
            return Math.Abs(dot(v0.xd, v0.yd, v0.zd, v1.xd, v1.yd, v1.zd)
                    / (getLength3D(v0) * getLength3D(v1))) < epsilon;
        }

        public static bool isOrthogonalNorm(HS_Coord v0, HS_Coord v1)
        {
            return HS_Epsilon.isZero(dot(v0.xd, v0.yd, v0.zd, v1.xd, v1.yd, v1.zd));
        }

        public static bool isOrthogonalNorm(HS_Coord v0, HS_Coord v1, double epsilon)
        {
            return Math.Abs(dot(v0.xd, v0.yd, v0.zd, v1.xd, v1.yd, v1.zd)) < epsilon;
        }

        public static bool isParallel(HS_Coord v0, HS_Coord v1)
        {
            return Math.Abs(1.0 - dot(v0.xd, v0.yd, v0.zd, v1.xd, v1.yd, v1.zd)
                    / (getLength3D(v0) * getLength3D(v1))) < HS_Epsilon.EPSILON;
        }

        public static bool isParallel(HS_Coord v0, HS_Coord v1, double epsilon)
        {
            return Math.Abs(1.0 - dot(v0.xd, v0.yd, v0.zd, v1.xd, v1.yd, v1.zd)
                    / (getLength3D(v0) * getLength3D(v1))) < epsilon;
        }

        public static bool isParallelNorm(HS_Coord v0, HS_Coord v1)
        {
            return Math.Abs(1.0 - dot(v0.xd, v0.yd, v0.zd, v1.xd, v1.yd, v1.zd)) < HS_Epsilon.EPSILON;
        }

        public static bool isParallelNorm(HS_Coord v0, HS_Coord v1, double epsilon)
        {
            return Math.Abs(1.0 - dot(v0.xd, v0.yd, v0.zd, v1.xd, v1.yd, v1.zd)) < epsilon;
        }

        /**
         *
         * @param o
         * @param p
         * @return
         */
        public static bool isParallelNormX(HS_Coord o, HS_Coord p)
        {
            return HS_Vector.cross(o, p).len() < HS_Epsilon.EPSILON;
        }

        /**
         *
         * @param o
         * @param p
         * @param t
         * @return
         */
        public static bool isParallelNormX(HS_Coord o, HS_Coord p, double t)
        {
            return HS_Vector.cross(o, p).len() < t + HS_Epsilon.EPSILON;
        }

        /**
         *
         * @param o
         * @param p
         * @return
         */
        public static bool isParallelX(HS_Coord o, HS_Coord p)
        {
            double pm2 = p.xd * p.xd + p.yd * p.yd + p.zd * p.zd;
            return HS_Vector.cross(o, p).len2() / (pm2 * HS_Vector.getLen2(o)) < HS_Epsilon.SQEPSILON;
        }
        
        /**
         *
         * @param o
         * @param p
         * @param t
         * @return
         */
        public static bool isParallelX(HS_Coord o, HS_Coord p, double t)
        {
            double pm2 = p.xd * p.xd + p.yd * p.yd + p.zd * p.zd;
            return HS_Vector.cross(o, p).len2() / (pm2 * HS_Vector.getLen2(o)) < t + HS_Epsilon.SQEPSILON;
        }

        /**
         *
         *
         * @param ux
         * @param uy
         * @param uz
         * @return
         */
        public static bool isZero3D(double ux, double uy, double uz)
        {
            return getSqLength3D(ux, uy, uz) < HS_Epsilon.SQEPSILON;
        }

        /**
         *
         *
         * @param ux
         * @param uy
         * @param uz
         * @param vx
         * @param vy
         * @param vz
         * @param wx
         * @param wy
         * @param wz
         * @return
         */
        public static double scalarTriple(double ux, double uy, double uz, double vx,
                 double vy, double vz, double wx, double wy, double wz)
        {
            double[] c = cross(vx, vy, vz, wx, wy, wz);
            return dot(ux, uy, uz, c[0], c[1], c[2]);
        }

        /**
         *
         *
         * @param ux
         * @param uy
         * @param uz
         * @param vx
         * @param vy
         * @param vz
         * @return
         */
        public static double[][] tensor3D(double ux, double uy, double uz, double vx,
                 double vy, double vz)
        {
            return new double[][] { new double[]{ ux * vx, ux * vy, ux * vz }, new double[]{ uy * vx, uy * vy, uy * vz },
                new double[]{ uz * vx, uz * vy, uz * vz } };
        }
    }
}
