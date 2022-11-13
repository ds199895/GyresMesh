using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_GeometryFactory3D:HS_GeometryFactory2D
    {
        HS_Point origin = new HS_Point(0.0D, 0.0D, 0.0D);
        HS_Vector x = new HS_Vector(1.0D, 0.0D, 0.0D);
        HS_Vector y = new HS_Vector(0.0D , 1.0D, 0.0D);
        HS_Vector z = new HS_Vector(0.0D, 0.0D, 1.0D);
        HS_Vector mX = new HS_Vector(-1.0D, 0.0D, 0.0D);
        HS_Vector mY = new HS_Vector(0.0D, -1.0D, 0.0D);
        HS_Vector mZ = new HS_Vector(0.0D, 0.0D, -1.0D);
        HS_Plane xy;
        HS_Plane yz;
        HS_Plane zx;
        HS_Plane yx;
        HS_Plane zy;
        HS_Plane xz;

        public HS_GeometryFactory3D()
        {

        }
        public static HS_GeometryFactory3D instance()
        {
            return new HS_GeometryFactory3D();
        }
        public HS_Polygon CreateSimplePolygon(List<HS_Coord> points)
        {
            
            return new HS_Polygon(points);
        }

        public HS_Point Origin()
        {
            return this.origin;
        }
        public HS_Vector X()
        {
            return this.x;

        }
        public HS_Vector Y()
        {
            return this.y;

        }

        public HS_Vector Z()
        {
            return this.z;

        }
        public HS_Vector minX()
        {
            return this.mX;
        }

        public HS_Vector minY()
        {
            return this.mY;
        }

        public HS_Vector minZ()
        {
            return this.mZ;
        }

        public HS_Plane XY()
        {
            if (this.xy == null)
            {
                this.xy = this.createPlane(this.Origin(), this.Z());
            }
            return this.xy;
        }

        public HS_Plane YZ()
        {
            if (this.yz == null)
            {
                this.yz = this.createPlane(this.Origin(), this.X());
            }
            return this.yz;
        }


        public HS_Plane ZX()
        {
            if (this.zx == null)
            {
                this.zx = this.createPlane(this.Origin(), this.Y());
            }
            return this.zx;
        }

        public HS_Plane YX()
        {
            if (this.yx == null)
            {
                this.yx = this.createPlane(this.Origin(), this.minZ());
            }
            return this.yx;
        }

        public HS_Plane ZY()
        {
            if (this.zy == null)
            {
                this.zy = this.createPlane(this.Origin(), this.minX());
            }
            return this.zy;
        }
        public HS_Plane XZ()
        {
            if (this.xz == null)
            {
                this.xz = this.createPlane(this.Origin(), this.minY());
            }
            return this.xz;
        }

        //public HS_Map2D createEmbeddedPlane()
        //{
        //    return new HS_PlanarMap();
        //}

        public HS_Plane createPlane(HS_Coord origin, HS_Coord normal)
        {
            return new HS_Plane(origin, normal);
        }

        public HS_Point createPoint(double _x,double _y,double _z)
        {
            return new HS_Point(_x, _y, _z);
        } 
        public HS_Vector createVector()
        {
            return createVector(0, 0, 0);
        }

        public HS_Vector createVector(double _x, double _y, double _z)
        {
            return new HS_Vector(_x, _y, _z);
        }

        public HS_Vector createVector(HS_Coord p)
        {
            return new HS_Vector(p.xd, p.yd, p.zd);
        }

        public HS_Vector createNormalizedVector(double _x, double _y, double _z)
        {
            HS_Vector vec = createVector(_x, _y, _z);
            vec = vec.united();
            return vec;
        }
        public HS_Vector createNormalizedVector(HS_Coord p)
        {
            HS_Vector vec = createVector(p.xd, p.yd, p.zd);
            vec = vec.united();
            return vec;
        }
        public HS_Map2D createEmbeddedPlane(HS_Plane P)
        {
            return new HS_PlanarMap(P);
        }

    }
}
