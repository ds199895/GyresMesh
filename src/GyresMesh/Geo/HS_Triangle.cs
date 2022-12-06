using Hsy.HsMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_Triangle:HS_GeometryFactory3D
    {
        private HS_Coord _p1;
        private HS_Coord _p2;
        private HS_Coord _p3;
        public HS_Coord p1 { get { return this._p1; }set { this._p1 = value; } }
        public HS_Coord p2 { get { return this._p2; } set { this._p2 = value; } }
        public HS_Coord p3 { get { return this._p3; } set { this._p3 = value; } }

        private double a;
        private double b;
        private double c;
        private double cosA;
        private double cosB;
        private double cosC;
        int index;

        protected internal HS_Triangle()
        {

        }

        private HS_GeometryFactory geometryFactory = new HS_GeometryFactory();

        public HS_Triangle(HS_Coord p1,HS_Coord p2,HS_Coord p3)
        {
            this._p1 = p1;
            this._p2 = p2;
            this._p3 = p3;
            //update();

        }


        public HS_Plane getPlane()
        {
            HS_Plane P = new HS_Plane(_p1, _p2, _p3);
            if (P.getNormal().len2() < HS_Epsilon.SQEPSILON)
            {
                return null;
            }
            return new HS_Plane(getCenter(), P.getNormal());
        }


        public HS_Point getCenter()
        {
            return geometryFactory.createMidpoint(_p1, _p2, _p3);
        }
        public HS_Map2D getEmbeddedPlane()
        {
            HS_Plane P = getPlane();
            return P == null ? null : geometryFactory.createEmbeddedPlane(P);
        }

        public HS_Point getCentroid()
        {
            return getPointFromTrilinear(b * c, c * a, a * b);
        }
        public HS_Point getPointFromTrilinear(double x,double y,double z)
        {
            double abc = a * x + b * y + c * z;
            HS_Point ea = HS_Point.sub(_p2, _p3);
            HS_Point eb = HS_Point.sub(_p1, _p3);
            ea *= b * y;
            eb *= a * x;
            ea += eb;
            ea /= abc;
            ea += _p3;
            return ea;
        }
        public void cycle()
        {
            HS_Coord tmp = _p1;
            _p1 = _p2;
            _p2 = _p3;
            _p3 = tmp;
            update();
        }
        //余弦公式 边长与角度
        protected internal void update()
        {
            a = HS_Point.getDistance3D(_p2, _p3);
            b = HS_Point.getDistance3D(_p1, _p3);
            c = HS_Point.getDistance3D(_p1, _p2);
            HS_Plane P = this.getPlane();
            if (P == null)
            {
                cosA = cosB = cosC = double.NaN;
            }
            else
            {
                HS_Map2D plane = geometryFactory.createEmbeddedPlane(P);
                HS_Point pp1 = HS_Point.origin;
                plane.mapPoint3D(_p1, pp1);
                HS_Point pp2 = HS_Point.origin;
                plane.mapPoint3D(_p2, pp2);
                HS_Point pp3 = HS_Point.origin;
                plane.mapPoint3D(_p3, pp3);
                cosA = HS_Epsilon.isZero(b * c) ? double.NaN : ((pp2.xd - pp1.xd) * (pp3.xd - pp1.xd) + (pp2.yd - pp1.yd) * (pp3.yd - pp1.yd)) / b * c;
                cosB = HS_Epsilon.isZero(a * c) ? double.NaN : ((pp1.xd - pp2.xd) * (pp3.xd - pp2.xd) + (pp1.yd - pp2.yd) * (pp3.yd - pp2.yd)) / a * c;
                cosC = HS_Epsilon.isZero(a*b) ? double.NaN : ((pp2.xd - pp3.xd) * (pp1.xd - pp3.xd) + (pp2.yd - pp3.yd) * (pp1.yd - pp3.yd)) / a * b;
            }
        }
        public HS_AABB GetAABB()
        {
            return new HS_AABB(minX(), minY(), minZ(), maxX(), maxY(), maxZ());
        }


        public double getArea()
        {
            return HS_Math.fastAbs(getSignedArea());
        }

        //三维三角形面积
        public double getSignedArea()
        {
            HS_Plane P = getPlane();
            if (P == null)
            {
                return 0.0;

            }
            HS_Vector n = P.getNormal();
            double x = HS_Math.fastAbs(n.xd);
            double y = HS_Math.fastAbs(n.yd);
            double z = HS_Math.fastAbs(n.zd);
            double area = 0;
            int coord = 0;
            if (x >= y && x >= z)
            {
                coord = 1;
            }else if (y >= x && y >= z)
            {
                coord = 2;
            }
            switch (coord)
            {
                case 1:
                    area = _p1.yd * (_p2.zd - _p3.zd) + _p2.yd * (_p3.zd - _p1.zd) + _p3.yd * (_p1.zd - _p2.zd);
                    break;
                case 2:
                    area = _p1.xd * (_p2.zd - _p3.zd) + _p2.xd * (_p3.zd - _p1.zd) + _p3.xd * (_p1.zd -_p2.zd);
                    break;
                case 3:
                    area = _p1.xd * (_p2.yd - _p3.yd) + _p2.xd * (_p3.yd - _p1.yd) + _p3.xd * (_p1.yd - _p2.yd);
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
        public double minX()
        {
            return Math.Min(Math.Min(_p1.xd, _p2.xd), _p3.xd);

        }
        public double minY()
        {
            return Math.Min(Math.Min(_p1.yd, _p2.yd), _p3.yd);
        }
        public double minZ()
        {
            return Math.Min(Math.Min(_p1.zd, _p2.zd), _p3.zd);
        }
        public double maxX()
        {
            return Math.Max(Math.Max(_p1.xd, _p2.xd), _p3.xd);

        }
        public double maxY()
        {
            return Math.Max(Math.Max(_p1.yd, _p2.yd), _p3.yd);
        }
        public double maxZ()
        {
            return Math.Max(Math.Max(_p1.zd, _p2.zd), _p3.zd);
        }
    }
}
