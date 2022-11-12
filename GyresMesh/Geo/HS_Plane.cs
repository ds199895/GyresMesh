using Hsy.HsMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_Plane:HS_Geometry
    {
        public static HS_Plane XY { get; } = new HS_Plane(0, 0, 0, 0, 0, 1);
        public static HS_Plane XZ { get; }
        public static HS_Plane YZ { get; }
        private HS_Vector n;
        private HS_Vector u, v;
        private HS_Point origin;
        public HS_Plane(HS_Coord p1,HS_Coord p2,HS_Coord p3)
        {
            HS_Vector v21 = new HS_Vector(p1, p2);
            HS_Vector v31 = new HS_Vector(p1, p3);
            n = new HS_Vector(v21.cross(v31)).united();
            origin = new HS_Point(p1);
            setAxes();
        }

        public HS_Plane(double ox,double oy,double oz,double nx,double ny,double nz)
        {
            origin = new HS_Point(ox, oy, oz);
            n = new HS_Vector(nx, ny, nz);
            n.united();
            setAxes();
        }
        public HS_Plane(HS_Coord o,HS_Coord n)
        {
            origin = new HS_Point(o);
            this.n = new HS_Vector(n);
            this.n.united();
            setAxes();
        }

        protected void set(HS_Coord o,HS_Coord n)
        {
            origin = new HS_Point(o);
            this.n = new HS_Vector(n);
            this.n.united();
            setAxes();
        }

        public HS_Plane(HS_Coord n,double d)
        {
            this.n = new HS_Vector(n);
            this.n.united();
            if (HS_Math.fastAbs(n.xd) > HS_Math.fastAbs(n.yd))
            {
                if (HS_Math.fastAbs(n.xd) > HS_Math.fastAbs(n.zd))
                {
                    origin = new HS_Point(d / n.xd, 0, 0);

                }
                else
                {
                    origin = new HS_Point(0, 0, d / n.zd);
                }
            }
            else
            {
                if (HS_Math.fastAbs(n.yd) > HS_Math.fastAbs(n.zd))
                {
                    origin = new HS_Point(0, d / n.yd, 0);
                }
                else
                {
                    origin = new HS_Point(0, 0, d / n.zd);
                }

            }
            setAxes();
        }

        public HS_Plane(double ox,double oy,double oz,double nx,double ny,double nz,double d)
        {
            origin = new HS_Point(ox, oy, oz);
            n = new HS_Vector(nx, ny, nz).united();
            //origin.addMulSelf(d, n);
            origin = (HS_Point)(origin +n*d);
            setAxes();
        }

        public HS_Plane(HS_Coord o, HS_Coord n, double d)
        {
            origin = new HS_Point(o);
            n = new HS_Vector(n).united();
            origin.addMulSelf(d, n);
            setAxes();
        }

        public HS_Plane(HS_Coord p1, HS_Coord p2, HS_Coord p3,double d)
        {
            HS_Vector v21 = new HS_Vector(p1, p2);
            HS_Vector v31 = new HS_Vector(p1, p3);
            n = new HS_Vector(v21.cross(v31)).united();
            origin = new HS_Point(p1);
            origin.addMulSelf(d, n);
            setAxes();
        }

        public HS_Plane get()
        {
            return new HS_Plane(origin, n);
        }

        public HS_Vector getNormal()
        {
            return n.copy();
        }

        public HS_Point getOrigin()
        {
            return origin.copy();
        }

        public void setAxes()
        {
            double x = HS_Math.fastAbs(n.xd);
            double y = HS_Math.fastAbs(n.yd);
            if (x >= y)
            {
                u = new HS_Vector(n.zd, 0, -n.xd);

            }
            else
            {
                v = new HS_Vector(0, n.zd, -n.yd);

            }
            u.united();
            v = n.cross(u);

        }
    }
}
