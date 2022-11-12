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
            T = new HS_Matrix44();
            invT = new HS_Matrix44();
            addTranslate(-1, sourceOrigin);
            HS_Vector v1 = geometryFactory.createNormalizedVector(sourceDirection);
            HS_Vector v2 = geometryFactory.createNormalizedVector(targetDirection);
            HS_Vector axis = v1.cross(v2);
            double l = axis.len();
            if (HS_Epsilon.isZero(l){
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
            T = new HS_Matrix44();
            invT = new HS_Matrix44();
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
            T = new HS_Matrix44(1, 0, 0,  v.xd, 0, 1, 0,  v.yd, 0, 0, 1,  v.zd, 0, 0, 0, 1) * T;
            invT = invT * new HS_Matrix44(1, 0, 0, - v.xd, 0, 1, 0, - v.yd, 0, 0, 1, - v.zd, 0, 0, 0, 1);
            return this;
        }

        public HS_Transform3D addTranslate(double f,HS_Coord v)
        {
            T = new HS_Matrix44(1, 0, 0, f * v.xd, 0, 1, 0, f * v.yd, 0, 0, 1, f * v.zd, 0, 0, 0, 1) * T;
            invT = invT * new HS_Matrix44(1, 0, 0, -f * v.xd, 0, 1, 0, -f * v.yd, 0, 0, 1, -f * v.zd, 0, 0, 0, 1);
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



    }

    }
