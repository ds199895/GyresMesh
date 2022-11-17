using Hsy.HsMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_Transform3D
    {
        private double _xt, _yt, _zt;
        private HS_GeometryFactory geometryFactory = new HS_GeometryFactory();

        private HS_Matrix44 T;
        private HS_Matrix44 invT;

        public HS_Transform3D()
        {
            T = new HS_Matrix44(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
            invT = new HS_Matrix44(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
        }

        public HS_Transform3D(HS_Transform3D Trans)
        {
            T = Trans.T.get();
            invT = Trans.invT.get();
        }

        public HS_Transform3D(HS_Coord sourceOrigin,HS_Coord sourceDirection,HS_Coord targetOrigin,HS_Coord targetDirection)
        {
            T = new HS_Matrix44(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
            invT = new HS_Matrix44(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
            addTranslate(-1, sourceOrigin);
            HS_Vector v1 = geometryFactory.createNormalizedVector(sourceDirection);
            HS_Vector v2 = geometryFactory.createNormalizedVector(targetDirection);
            HS_Vector axis = v1.cross(v2);
            double l = axis.len();
            if (HS_Epsilon.isZero(l)){
                if (v1.dot(v2) < 0.0D)
                {
                    axis = geometryFactory.createNormalizedVector(sourceDirection);
                    addRotateAboutOrigin(Math.PI, axis);
                }
            }
            else
            {
                double angle = Math.Atan2(l, v1.dot(v2));
                axis = axis.united();
                addRotateAboutOrigin(angle, axis);
            }
            addTranslate(targetOrigin);
        }

        public HS_Transform3D(HS_Coord sourceDirection,HS_Coord targetDirection)
        {
            T = new HS_Matrix44(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
            invT = new HS_Matrix44(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
            HS_Vector v1 = geometryFactory.createNormalizedVector(sourceDirection);
            HS_Vector v2 = geometryFactory.createNormalizedVector(targetDirection);
            HS_Vector axis = v1.cross(v2);
            double l = axis.len();
            if (HS_Epsilon.isZero(l))
            {
                if (v1.dot(v2) < 0.0D)
                {
                    axis = geometryFactory.createNormalizedVector(sourceDirection);
                    addRotateAboutOrigin(Math.PI, axis);
                }
                else
                {
                    double angle = Math.Atan2(l, v1.dot(v2));
                    axis = axis.united();
                    addRotateAboutOrigin(angle, axis);

                }
            }

        }

        public HS_Transform3D get()
        {
            return new HS_Transform3D(this);
        }


        public HS_Transform3D addTranslate(HS_Coord v)
        {
            T = new HS_Matrix44(1, 0, 0, v.xd, 0, 1, 0, v.yd, 0, 0, 1, v.zd, 0, 0, 0, 1) * T;
            invT = invT * new HS_Matrix44(1, 0, 0, -v.xd, 0, 1, 0, -v.yd, 0, 0, 1, -v.zd, 0, 0, 0, 1);
            return this;
        }

        public HS_Transform3D addTranslate(double f,HS_Coord v)
        {
            T = new HS_Matrix44(1, 0, 0, f * v.xd, 0, 1, 0, f * v.yd, 0, 0, 1, f * v.zd, 0, 0, 0, 1) * T;
            invT = invT * new HS_Matrix44(1, 0, 0, -f * v.xd, 0, 1, 0, -f * v.yd, 0, 0, 1, -f * v.zd, 0, 0, 0, 1);
            return this;
        }

        public HS_Transform3D addRotateAboutAxis( double angle,  HS_Coord p,  HS_Coord axis)
        {
            addTranslate(-1, p);
            addRotateAboutOrigin(angle, axis);
            addTranslate(p);
            return this;
        }

        public HS_Transform3D addRotateAboutOrigin(double angle,HS_Coord axis)
        {
            HS_Vector a = new HS_Vector(axis);
            a = a.united();
            double s = Math.Sin(angle);
            double c = Math.Cos(angle);
            HS_Matrix44 tmp = new HS_Matrix44(
                a.xd * a.xd + (1 - a.xd * a.xd) * c,
                a.xd * a.yd * (1 - c) - a.zd * s, 
                a.xd * a.zd * (1 - c) + a.yd * s, 
                0, 
                a.xd * a.yd * (1 - c) + a.zd * s, 
                a.yd * a.yd + (1 - a.yd * a.yd) * c, 
                a.yd * a.zd * (1 - c) - a.xd * s, 
                0,
                a.xd * a.zd * (1 - c) - a.yd * s,
                a.yd * a.zd * (1 - c) + a.xd * s,
                a.zd * a.zd + (1 - a.zd * a.zd) * c,
                0, 
                0,
                0, 
                0, 
                1
                );
            T = tmp * T;
            invT = invT * tmp.getTranspose();
            return this;
        }


        public void applyAsPointInto(HS_Coord p, HS_MutableCoord result)
        {
            _xt = T.m11 * p.xd + T.m12 * p.yd + T.m13 * p.zd + T.m14;
            _yt = T.m21 * p.xd + T.m22 * p.yd + T.m23 * p.zd + T.m24;
            _zt = T.m31 * p.xd + T.m32 * p.yd + T.m33 * p.zd + T.m34;
            double wp = T.m41 * p.xd + T.m42 * p.yd + T.m43 * p.zd + T.m44;
            wp = 1.0D / wp;
            result.Set(_xt * wp, _yt * wp, _zt * wp);
        }

        public void applyAsPointInto(double x, double y, double z, HS_MutableCoord result)
        {
            _xt = T.m11 * x + T.m12 * y + T.m13 * z + T.m14;
            _yt = T.m21 * x + T.m22 * y + T.m23 * z + T.m24;
            _zt = T.m31 * x + T.m32 * y + T.m33 * z + T.m34;
            double wp = T.m41 * x + T.m42 * y + T.m43 * z + T.m44;
            wp = 1.0D / wp;
            result.Set(_xt * wp, _yt * wp, _zt * wp);
        }

        public void applyAsVectorSelf( HS_MutableCoord p)
        {
             double x = T.m11 * p.xd + T.m12 * p.yd + T.m13 * p.zd;
             double y = T.m21 * p.xd + T.m22 * p.yd + T.m23 * p.zd;
             double z = T.m31 * p.xd + T.m32 * p.yd + T.m33 * p.zd;
            p.Set(x, y, z);
        }


        public void applyInvAsPointInto(HS_Coord p,HS_MutableCoord result)
        {
            Console.WriteLine("prev: "+p.xd + " " +p.yd+ " " +p.zd);
            Console.WriteLine(invT.m21 +" " + invT.m22 +" " + invT.m23+" "  + invT.m24);
            _xt = invT.m11 * p.xd + invT.m12 * p.yd + invT.m13 * p.zd + invT.m14;
            _yt= invT.m21 * p.xd + invT.m22 * p.yd + invT.m23 * p.zd + invT.m24;
            _zt=invT.m31* p.xd + invT.m32 * p.yd + invT.m33 * p.zd + invT.m34;
            double wp= invT.m41 * p.xd + invT.m42 * p.yd + invT.m43 * p.zd + invT.m44;
            Console.WriteLine("wp: "+wp);
            wp = 1.0D / wp;
            Console.WriteLine("post: " + _xt + " " + _yt + " " + _zt);
            result.Set(_xt * wp, _yt * wp, _zt * wp);
        }

        public void applyInvAsPointInto(double x,double y,double z, HS_MutableCoord result)
        {
            _xt = invT.m11 * x + invT.m12 * y+ invT.m13 * z + invT.m14;
            _yt = invT.m21 *x + invT.m22 * y + invT.m23 * z + invT.m24;
            _zt = invT.m31 * x + invT.m32 * y + invT.m33 *z + invT.m34;
            double wp = invT.m41 * x + invT.m42 * y + invT.m43 * z + invT.m44;
            wp = 1.0D / wp;
            result.Set(_xt * wp, _yt * wp, _zt * wp);
        }
        public void applyAsVectorInto(HS_Coord p, HS_MutableCoord result)
        {
            _xt = T.m11 * p.xd +T.m12 * p.yd + T.m13 * p.zd;
            _yt =T.m21 * p.xd + T.m22 * p.yd + T.m23 * p.zd;
            _zt = T.m31 * p.xd + T.m32 * p.yd + T.m33 * p.zd;
            result.Set(_xt, _yt, _zt);
        }

        public void applyAsVectorInto(double x,double y,double z, HS_MutableCoord result)
        {
            _xt = T.m11 * x + T.m12 * y+ T.m13 * z;
            _yt = T.m21 * x + T.m22 * y + T.m23 * z;
            _zt = T.m31 * x + T.m32 * y + T.m33 * z;
            result.Set(_xt, _yt, _zt);
        }

        public void applyInvAsVectorInto(HS_Coord p,HS_MutableCoord result)
        {
            _xt = invT.m11 * p.xd + invT.m12 * p.yd + invT.m13 * p.zd;
            _yt = invT.m21 * p.xd + invT.m22 * p.yd + invT.m23 * p.zd;
            _zt = invT.m31 * p.xd + invT.m32 * p.yd + invT.m33 * p.zd;
            result.Set(_xt, _yt, _zt);
        }
        public void applyInvAsVectorInto(double x, double y, double z, HS_MutableCoord result)
        {
            _xt = invT.m11 * x + invT.m12 * y + invT.m13 * z;
            _yt = invT.m21 * x + invT.m22 * y + invT.m23 * z;
            _zt = invT.m31 * x + invT.m32 * y + invT.m33 * z;
            result.Set(_xt, _yt, _zt);
        }

        public HS_Transform3D addFromCSToWorld(HS_CoordinateSystem CS)
        {
            HS_CoordinateSystem current = CS;
            while (!current.isWorld())
            {
                addFromCSToParent(current);

                current = current.getParent();
            }
            return this;
        }

        public HS_Transform3D addFromWorldToCS(HS_CoordinateSystem CS)
        {
            HS_Transform3D tmp = new HS_Transform3D();
            tmp.addFromCSToWorld(CS);
            T = tmp.invT * T;
            invT = invT * tmp.T;
            return this;
        }


        public HS_Transform3D addFromParentToCS(HS_CoordinateSystem CS)
        {
            if (CS.isWorld())
            {
                return this;
            }
            else
            {
                HS_Transform3D tmp = new HS_Transform3D();
                tmp.addFromCSToParent(CS);
                this.T = tmp.invT * this.T;
                this.invT = this.invT * tmp.T;
                return this;
            }
        }

        public HS_Transform3D addFromCSToParent(HS_CoordinateSystem CS)
        {
            HS_CoordinateSystem WCS = HS_CoordinateSystem.WORLD();
            if (CS.isWorld())
            {
                return this;
            }
            else
            {
                HS_Vector ex1 = CS.getX();
                HS_Vector ey1 = CS.getY();
                HS_Vector ez1 = CS.getZ();
                HS_Point o1 = CS.getOrigin();
                HS_Vector ex2 = WCS.getX();
                HS_Vector ey2 = WCS.getY();
                HS_Vector ez2 = WCS.getZ();
                HS_Point o2 = WCS.getOrigin();

                double xx = ex2.dot(ex1);
                double xy = ex2.dot(ey1);
                double xz = ex2.dot(ez1);
                double yx = ey2.dot(ex1);
                double yy = ey2.dot(ey1);
                double yz = ey2.dot(ez1);
                double zx = ez2.dot(ex1);
                double zy = ez2.dot(ey1);
                double zz = ez2.dot(ez1);

                HS_Matrix44 tmp = new HS_Matrix44(xx, xy, xz, 0, yx, yy, yz, 0, zx, zy, zz, 0, 0, 0, 0, 1);
                HS_Matrix44 invtmp = new HS_Matrix44(xx, yx, zx, 0, xy, yy, zy, 0, xz, yz, zz, 0, 0, 0, 0, 1);
                this.T = tmp * this.T;
                this.invT = this.invT * invtmp;
                this.addTranslate(o1 - o2);
                return this;
            }

        }


    }

    }
