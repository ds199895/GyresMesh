using Hsy.HsMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_Transform2D
    {
        private double _xt, _yt;
        /** Transform matrix. */
        private HS_Matrix33 T;
        /** Inverse transform matrix. */
        private HS_Matrix33 invT;

        /**
		 * Instantiates a new HS_Transfrom.
		 */
        public HS_Transform2D()
        {
            T = new HS_Matrix33(1, 0, 0, 0, 1, 0, 0, 0, 1);
            invT = new HS_Matrix33(1, 0, 0, 0, 1, 0, 0, 0, 1);
        }

        /**
		 *
		 *
		 * @param Trans
		 */
        public HS_Transform2D(HS_Transform2D Trans)
        {
            T = Trans.T.get();
            invT = Trans.invT.get();
        }

        /**
		 *
		 *
		 * @return
		 */
        public HS_Transform2D get()
        {
            return new HS_Transform2D(this);
        }

        /**
		 * Add translation to transform.
		 *
		 * @param v
		 *            vector
		 * @return self
		 */
        public HS_Transform2D addTranslate2D(HS_Coord v)
        {
            T = new HS_Matrix33(1, 0, v.xd, 0, 1, v.yd, 0, 0, 1).mul(T);
            invT = invT.mul(new HS_Matrix33(1, 0, -v.xd, 0, 1, -v.yd, 0, 0, 1));
            return this;
        }

        /**
		 *
		 *
		 * @param f
		 * @param v
		 * @return
		 */
        public HS_Transform2D addTranslate2D(double f, HS_Coord v)
        {
            T = new HS_Matrix33(1, 0, f * v.xd, 0, 1, f * v.yd, 0, 0, 1).mul(T);
            invT = invT
                    .mul(new HS_Matrix33(1, 0, -f * v.xd, 0, 1, -f * v.yd, 0, 0, 1));
            return this;
        }

        /**
		 * Add uniform scale to transform.
		 *
		 * @param s
		 *            scaling vector
		 * @return self
		 */
        public HS_Transform2D addScale2D(HS_Coord s)
        {
            T = new HS_Matrix33(s.xd, 0, 0, 0, s.yd, 0, 0, 0, 1).mul(T);
            invT = invT.mul(
                    new HS_Matrix33(1.0 / s.xd, 0, 0, 0, 1.0 / s.yd, 0, 0, 0, 1));
            return this;
        }

        /**
		 * Add non-uniform scale to transform.
		 *
		 * @param sx
		 *            scaling vector
		 * @param sy
		 *            scaling vector
		 * @return self
		 */
        public HS_Transform2D addScale2D(double sx, double sy)
        {
            T = new HS_Matrix33(sx, 0, 0, 0, sy, 0, 0, 0, 1).mul(T);
            invT = invT.mul(new HS_Matrix33(1.0 / sx, 0, 0, 0, 1.0 / sy, 0, 0, 0, 1));
            return this;
        }

        /**
		 * Add uniform scale to transform.
		 *
		 * @param s
		 *            scaling point
		 * @return self
		 */
        public HS_Transform2D addScale2D(double s)
        {
            T = new HS_Matrix33(s, 0, 0, 0, s, 0, 0, 0, 1).mul(T);
            invT = invT.mul(new HS_Matrix33(1 / s, 0, 0, 0, 1 / s, 0, 0, 0, 1));
            return this;
        }

        /**
		 * Add rotation around origin.
		 *
		 * @param angle
		 *            angle in radians
		 * @return self
		 */
        public HS_Transform2D addRotateAboutOrigin(double angle)
        {
            double s = Math.Sin(angle);
            double c = Math.Cos(angle);
            HS_Matrix33 tmp = new HS_Matrix33(c, -s, 0, s, c, 0, 0, 0, 1);
            T = tmp.mul(T);
            invT = invT.mul(tmp.getTranspose());
            return this;
        }

        /**
		 * Add rotation around point.
		 *
		 * @param angle
		 *            angle in radians
		 * @param p
		 *            point
		 * @return self
		 */
        public HS_Transform2D addRotateAboutPoint(double angle,
                 HS_Coord p)
        {
            addTranslate2D(-1, p);
            addRotateAboutOrigin(angle);
            addTranslate2D(p);
            return this;
        }

        /**
		 * Adds the reflect x.
		 *
		 * @return
		 */
        public HS_Transform2D addReflectX()
        {
            addScale2D(-1, 1);
            return this;
        }

        /**
		 * Adds the reflect y.
		 *
		 * @return
		 */
        public HS_Transform2D addReflectY()
        {
            addScale2D(1, -1);
            return this;
        }

        /**
		 * Adds the invert.
		 *
		 * @return
		 */
        public HS_Transform2D addInvert2D()
        {
            addScale2D(-1);
            return this;
        }

        /**
		 * Adds the reflect x.
		 *
		 * @param p
		 *            the p
		 * @return
		 */
        public HS_Transform2D addReflectX(HS_Coord p)
        {
            addTranslate2D(-1, p);
            addScale2D(-1, 1);
            addTranslate2D(p);
            return this;
        }

        /**
		 * Adds the reflect y.
		 *
		 * @param p
		 *            the p
		 * @return
		 */
        public HS_Transform2D addReflectY(HS_Coord p)
        {
            addTranslate2D(-1, p);
            addScale2D(1, -1);
            addTranslate2D(p);
            return this;
        }

        /**
		 * Adds the invert.
		 *
		 * @param p
		 *            the p
		 * @return
		 */
        public HS_Transform2D addInvert2D(HS_Coord p)
        {
            addTranslate2D(-1, p);
            addScale2D(-1, -1);
            addTranslate2D(p);
            return this;
        }

        /**
		 *
		 * @param p1
		 * @param p2
		 * @return
		 */
        public HS_Transform2D addReflect2D(HS_Coord p1, HS_Coord p2)
        {
            HS_Vector u = HS_Vector.sub(p2, p1);
            HS_Vector v = new HS_Vector(-u.yd, u.xd);
            HS_Matrix33 Tr = new HS_Matrix33(1, 0, p1.xd, 0, 1, p1.yd, 0, 0, 1);
            Tr.mul(new HS_Matrix33(u.xd, v.xd, 0, u.yd, v.yd, 0, 0, 0, 1));
            Tr.mul(new HS_Matrix33(1, 0, 0, 0, -1, 0, 0, 0, 1));
            Tr.mul(new HS_Matrix33(u.xd, u.yd, 0, v.xd, v.yd, 0, 0, 0, 1));
            Tr.mul(new HS_Matrix33(1, 0, -p1.xd, 0, 1, -p1.yd, 0, 0, 1));
            T = Tr.mul(T);
            invT = invT.mul(Tr);
            return this;
        }

        /**
		 *
		 * @return
		 */
        public HS_Transform2D addShear2D(double shx, double shy)
        {
            HS_Matrix33 Tr = new HS_Matrix33(1, shx, 0, shy, 1, 0, 0, 0, 1);
            T = Tr.mul(T);
            invT = invT.mul(Tr.inverse());
            return this;
        }

        /**
		 *
		 * @param CS1
		 *
		 * @param CS2
		 *
		 * @return
		 */
        public HS_Transform2D addFromCSToCS2D(HS_CoordinateSystem CS1,
                 HS_CoordinateSystem CS2)
        {
            addFromCSToWorld2D(CS1);
            addFromWorldToCS2D(CS2);
            return this;
        }

        /**
		 *
		 *
		 * @param CS
		 *
		 * @return
		 */
        public HS_Transform2D addFromCSToWorld2D(HS_CoordinateSystem CS)
        {
            HS_CoordinateSystem current = CS;
            while (!current.isWorld())
            {
                addFromCSToParent2D(current);
                current = current.getParent();
            }
            return this;
        }

        /**
		 *
		 * @param CS
		 *
		 * @return
		 */
        public HS_Transform2D addFromWorldToCS2D(HS_CoordinateSystem CS)
        {
            HS_Transform2D tmp = new HS_Transform2D();
            tmp.addFromCSToWorld2D(CS);
            T = tmp.invT.mul(T);
            invT = invT.mul(tmp.T);
            return this;
        }

        /**
		 *
		 * @param CS
		 *
		 * @return
		 */
        public HS_Transform2D addFromCSToParent2D(HS_CoordinateSystem CS)
        {
            HS_CoordinateSystem WCS = HS_CoordinateSystem.WORLD();
            if (CS.isWorld())
            {
                return this;
            }
            HS_Vector ex1 = CS.getX(), ey1 = CS.getY();
            HS_Point o1 = CS.getOrigin();
            HS_Vector ex2 = WCS.getX(), ey2 = WCS.getY();
            HS_Point o2 = WCS.getOrigin();
            double xx = ex2.dot(ex1);
            double xy = ex2.dot(ey1);
            double yx = ey2.dot(ex1);
            double yy = ey2.dot(ey1);
            HS_Matrix33 tmp = new HS_Matrix33(xx, xy, 0, yx, yy, 0, 0, 0, 1);
            HS_Matrix33 invtmp = new HS_Matrix33(xx, yx, 0, xy, yy, 0, 0, 0, 1);
            T = tmp.mul(T);
            invT = invT.mul(invtmp);
            addTranslate2D(o1.subSelf(o2));
            return this;
        }

        /**
		 *
		 * @param CS
		 *
		 * @return
		 */
        public HS_Transform2D addFromParentToCS2D(HS_CoordinateSystem CS)
        {
            if (CS.isWorld())
            {
                return this;
            }
            HS_Transform2D tmp = new HS_Transform2D();
            tmp.addFromCSToParent2D(CS);
            T = tmp.invT.mul(T);
            invT = invT.mul(tmp.T);
            return this;
        }

        /**
		 * Invert transform.
		 */
        public void inverse()
        {
            HS_Matrix33 tmp;
            tmp = T;
            T = invT;
            invT = tmp;
        }

        /**
		 * Clear transform.
		 */
        public void clear()
        {
            T = new HS_Matrix33(1, 0, 0, 0, 1, 0, 0, 0, 1);
            invT = new HS_Matrix33(1, 0, 0, 0, 1, 0, 0, 0, 1);
        }

        /**
		 * Apply transform to point.
		 *
		 * @param p
		 *            point
		 * @return new HS_XYZ
		 */
        public HS_Point applyAsPoint2D(HS_Coord p)
        {
            double xp = T.m11 * p.xd + T.m12 * p.yd + T.m13;
            double yp = T.m21 * p.xd + T.m22 * p.yd + +T.m23;
            double wp = T.m31 * p.xd + T.m32 * p.yd + +T.m33;
            if (HS_Epsilon.isZero(wp))
            {
                return new HS_Point(xp, yp);
            }
            wp = 1.0 / wp;
            return new HS_Point(xp * wp, yp * wp);
        }

        /**
		 * Apply transform to point.
		 *
		 * @param p
		 *            point
		 */
        public void applyAsPoint2DSelf(HS_MutableCoord p)
        {
            double x = T.m11 * p.xd + T.m12 * p.yd + T.m13;
            double y = T.m21 * p.xd + T.m22 * p.yd + T.m23;
            double wp = T.m31 * p.xd + T.m32 * p.yd + T.m33;
            wp = 1.0 / wp;
            p.Set(x * wp, y * wp);
        }

        /**
		 *
		 *
		 * @param p
		 * @param result
		 */
        public void applyAsPoint2DInto(HS_Coord p,
                 HS_MutableCoord result)
        {
            _xt = T.m11 * p.xd + T.m12 * p.yd + T.m13;
            _yt = T.m21 * p.xd + T.m22 * p.yd + T.m23;
            double wp = T.m31 * p.xd + T.m32 * p.yd + T.m33;
            wp = 1.0 / wp;
            result.Set(_xt * wp, _yt * wp);
        }

        /**
		 * Apply as point.
		 *
		 * @param x
		 *
		 * @param y
		 *
		 * @return
		 */
        public HS_Point applyAsPoint2D(double x, double y)
        {
            double xp = T.m11 * x + T.m12 * y + T.m13;
            double yp = T.m21 * x + T.m22 * y + T.m23;
            double wp = T.m31 * x + T.m32 * y + T.m33;
            if (HS_Epsilon.isZero(wp))
            {
                return new HS_Point(xp, yp);
            }
            wp = 1.0 / wp;
            return new HS_Point(xp * wp, yp * wp);
        }

        /**
		 *
		 *
		 * @param x
		 * @param y
		 *
		 * @param result
		 */
        public void applyAsPoint2DInto(double x, double y,
                 HS_MutableCoord result)
        {
            _xt = T.m11 * x + T.m12 * y + T.m13;
            _yt = T.m21 * x + T.m22 * y + T.m23;
            double wp = T.m31 * x + T.m32 * y + T.m33;
            wp = 1.0 / wp;
            result.Set(_xt * wp, _yt * wp);
        }

        /**
		 *
		 * @param p
		 * @return
		 */
        public HS_Point applyInvAsPoint2D(HS_Coord p)
        {
            _xt = invT.m11 * p.xd + invT.m12 * p.yd + invT.m13;
            _yt = invT.m21 * p.xd + invT.m22 * p.yd + invT.m23;
            double wp = invT.m31 * p.xd + invT.m32 * p.yd + invT.m33;
            wp = 1.0 / wp;
            return new HS_Point(_xt * wp, _yt * wp);
        }

        /**
		 *
		 *
		 * @param p
		 */
        public void applyInvAsPoint2DSelf(HS_MutableCoord p)
        {
            _xt = invT.m11 * p.xd + invT.m12 * p.yd + invT.m13;
            _yt = invT.m21 * p.xd + invT.m22 * p.yd + invT.m23;
            double wp = invT.m31 * p.xd + invT.m32 * p.yd + invT.m33;
            wp = 1.0 / wp;
            p.Set(_xt * wp, _yt * wp);
        }

        /**
		 *
		 *
		 * @param p
		 * @param result
		 */
        public void applyInvAsPoint2DInto(HS_Coord p,
                 HS_MutableCoord result)
        {
            _xt = invT.m11 * p.xd + invT.m12 * p.yd + invT.m13;
            _yt = invT.m21 * p.xd + invT.m22 * p.yd + invT.m23;
            double wp = invT.m31 * p.xd + invT.m32 * p.yd + invT.m33;
            wp = 1.0 / wp;
            result.Set(_xt * wp, _yt * wp);
        }

        /**
		 *
		 * @param x
		 * @param y
		 *
		 * @return
		 */
        public HS_Point applyInvAsPoint2D(double x, double y)
        {
            _xt = invT.m11 * x + invT.m12 * y + invT.m13;
            _yt = invT.m21 * x + invT.m22 * y + invT.m23;
            double wp = invT.m31 * x + invT.m32 * y + invT.m33;
            wp = 1.0 / wp;
            return new HS_Point(_xt * wp, _yt * wp);
        }

        /**
		 *
		 *
		 * @param x
		 * @param y
		 *
		 * @param result
		 */
        public void applyInvAsPoint2DInto(double x, double y,
                 HS_MutableCoord result)
        {
            _xt = invT.m11 * x + invT.m12 * y + invT.m13;
            _yt = invT.m21 * x + invT.m22 * y + invT.m23;
            double wp = invT.m31 * x + invT.m32 * y + invT.m33;
            wp = 1.0 / wp;
            result.Set(_xt * wp, _yt * wp);
        }

        /**
		 * Apply transform to vector.
		 *
		 * @param p
		 *            vector
		 * @return new HS_Vector
		 */
        public HS_Vector applyAsVector2D(HS_Coord p)
        {
            double xp = T.m11 * p.xd + T.m12 * p.yd;
            double yp = T.m21 * p.xd + T.m22 * p.yd;
            return new HS_Vector(xp, yp);
        }

        /**
		 * Apply transform to vector.
		 *
		 * @param p
		 *            vector
		 */
        public void applyAsVector2DSelf(HS_MutableCoord p)
        {
            double x = T.m11 * p.xd + T.m12 * p.yd;
            double y = T.m21 * p.xd + T.m22 * p.yd;
            p.Set(x, y);
        }

        /**
		 *
		 *
		 * @param p
		 * @param result
		 */
        public void applyAsVector2DInto(HS_Coord p,
                 HS_MutableCoord result)
        {
            _xt = T.m11 * p.xd + T.m12 * p.yd;
            _yt = T.m21 * p.xd + T.m22 * p.yd;
            result.Set(_xt, _yt);
        }

        /**
		 * Apply as vector.
		 *
		 * @param x
		 *
		 * @param y
		 *
		 * @return
		 */
        public HS_Vector applyAsVector2D(double x, double y)
        {
            double xp = T.m11 * x + T.m12 * y;
            double yp = T.m21 * x + T.m22 * y;
            return new HS_Vector(xp, yp);
        }

        /**
		 *
		 *
		 * @param x
		 * @param y
		 *
		 * @param result
		 */
        public void applyAsVector2DInto(double x, double y,
                 HS_MutableCoord result)
        {
            _xt = T.m11 * x + T.m12 * y;
            _yt = T.m21 * x + T.m22 * y;
            result.Set(_xt, _yt);
        }

        /**
		 *
		 *
		 * @param p
		 */
        public HS_Vector applyInvAsVector2D(HS_Coord p)
        {
            _xt = invT.m11 * p.xd + invT.m12 * p.yd;
            _yt = invT.m21 * p.xd + invT.m22 * p.yd;
            return new HS_Vector(_xt, _yt);
        }

        /**
		 *
		 * @param v
		 */
        public void applyInvAsVector2DSelf(HS_MutableCoord v)
        {
            _xt = invT.m11 * v.xd + invT.m12 * v.yd;
            _yt = invT.m21 * v.xd + invT.m22 * v.yd;
            v.Set(_xt, _yt);
        }

        /**
		 *
		 *
		 * @param p
		 * @param result
		 */
        public void applyInvAsVector2DInto(HS_Coord p,
                 HS_MutableCoord result)
        {
            _xt = invT.m11 * p.xd + invT.m12 * p.yd;
            _yt = invT.m21 * p.xd + invT.m22 * p.yd;
            result.Set(_xt, _yt);
        }

        /**
		 *
		 *
		 * @param x
		 * @param y
		 *
		 */
        public HS_Vector applyInvAsVector2D(double x, double y)
        {
            _xt = invT.m11 * x + invT.m12 * y;
            _yt = invT.m21 * x + invT.m22 * y;
            return new HS_Vector(_xt, _yt);
        }

        /**
		 *
		 *
		 * @param x
		 * @param y
		 *
		 * @param result
		 */
        public void applyInvAsVector2DInto(double x, double y,
                 HS_MutableCoord result)
        {
            _xt = invT.m11 * x + invT.m12 * y;
            _yt = invT.m21 * x + invT.m22 * y;
            result.Set(_xt, _yt);
        }

        /**
		 * Apply as normal.
		 *
		 * @param p
		 *
		 * @return
		 */
        public HS_Vector applyAsNormal2D(HS_Coord p)
        {
            double nx = invT.m11 * p.xd + invT.m21 * p.yd;
            double ny = invT.m12 * p.xd + invT.m22 * p.yd;
            return new HS_Vector(nx, ny);
        }

        /**
		 * Apply transform to normal.
		 *
		 * @param n
		 *            normal
		 */
        public void applyAsNormal2DSelf(HS_MutableCoord n)
        {
            double x = invT.m11 * n.xd + invT.m21 * n.yd;
            double y = invT.m12 * n.xd + invT.m22 * n.yd;
            n.Set(x, y);
        }

        /**
		 *
		 *
		 * @param n
		 * @param result
		 */
        public void applyAsNormal2DInto(HS_Coord n,
                 HS_MutableCoord result)
        {
            _xt = invT.m11 * n.xd + invT.m21 * n.yd;
            _yt = invT.m12 * n.xd + invT.m22 * n.yd;
            result.Set(_xt, _yt);
        }

        /**
		 * Apply as normal.
		 *
		 * @param x
		 *
		 * @param y
		 *
		 * @return
		 */
        public HS_Vector applyAsNormal2D(double x, double y)
        {
            double nx = invT.m11 * x + invT.m21 * y;
            double ny = invT.m12 * x + invT.m22 * y;
            return new HS_Vector(nx, ny);
        }

        /**
		 *
		 *
		 * @param x
		 * @param y
		 *
		 * @param result
		 */
        public void applyAsNormal2DInto(double x, double y,
                 HS_MutableCoord result)
        {
            _xt = invT.m11 * x + invT.m21 * y;
            _yt = invT.m12 * x + invT.m22 * y;
            result.Set(_xt, _yt);
        }

        /**
		 *
		 *
		 * @param n
		 */
        public HS_Vector applyInvAsNormal2D(HS_Coord n)
        {
            _xt = T.m11 * n.xd + T.m21 * n.yd;
            _yt = T.m12 * n.xd + T.m22 * n.yd;
            return new HS_Vector(_xt, _yt);
        }

        /**
		 *
		 *
		 * @param n
		 */
        public void applyInvAsNormal2DSelf(HS_MutableCoord n)
        {
            _xt = T.m11 * n.xd + T.m21 * n.yd;
            _yt = T.m12 * n.xd + T.m22 * n.yd;
            n.Set(_xt, _yt);
        }

        /**
		 *
		 *
		 * @param n
		 * @param result
		 */
        public void applyInvAsNormal2DInto(HS_Coord n,
                 HS_MutableCoord result)
        {
            _xt = T.m11 * n.xd + T.m21 * n.yd;
            _yt = T.m12 * n.xd + T.m22 * n.yd;
            result.Set(_xt, _yt);
        }

        /**
		 *
		 *
		 * @param x
		 * @param y
		 *
		 */
        public HS_Vector applyInvAsNormal2D(double x, double y)
        {
            _xt = T.m11 * x + T.m21 * y;
            _yt = T.m12 * x + T.m22 * y;
            return new HS_Vector(_xt, _yt);
        }

        /**
		 *
		 *
		 * @param x
		 * @param y
		 *
		 * @param result
		 */
        public void applyInvAsNormal2DInto(double x, double y,
                 HS_MutableCoord result)
        {
            _xt = T.m11 * x + T.m21 * y;
            _yt = T.m12 * x + T.m22 * y;
            result.Set(_xt, _yt);
        }

        /*
		 * (non-Javadoc)
		 * @see java.lang.Object#toString()
		 */

        public override String ToString()
        {
            String s = "HS_Transform2D T:" + "\n" + "[" + T.m11 + ", " + T.m12
                   + ", " + T.m13 + "]" + "\n" + "[" + T.m21 + ", " + T.m22 + ", "
                   + T.m23 + "]" + "\n" + "[" + T.m31 + ", " + T.m32 + ", " + T.m33
                   + "]";
            return s;
        }



    }
}
