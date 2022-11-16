using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_PlanarMap :HS_CoordinateSystem, HS_Map2D
    {
        private double offset;
        int id;
        private HS_Transform3D T2D3D;
        public int mode;
        public static int YZ = 0;
        public static int XZ= 1;
        public static int XY = 2;
        public static int YZrev = 3;
        public static int XZrev = 4;
        public static int XYrev = 5;
        public static int PLANE = 6;
        public static HS_GeometryFactory geometryFactory=new HS_GeometryFactory();

       

        public HS_PlanarMap()
        {
            this.mode = XY;
            this.offset = 0;

            if (mode < 0 || mode > 5)
            {
                throw new IndexOutOfRangeException();
            }
            if (mode == YZ)
            {
                set(geometryFactory.createPoint(offset, 0, 0), geometryFactory.Y(), geometryFactory.Z(), geometryFactory.X(), geometryFactory.WORLD());
                this.mode = YZ;
            }
            else if (mode == XZ)
            {
                set(geometryFactory.createPoint(0, offset, 0), geometryFactory.Z(), geometryFactory.X(), geometryFactory.Y(), geometryFactory.WORLD());
                this.mode = XZ;
            }
            else if (mode == YZrev)
            {
                set(geometryFactory.createPoint(0, offset, 0), geometryFactory.Z(), geometryFactory.Y(), geometryFactory.minX(), geometryFactory.WORLD());
                this.mode = YZrev;
            }
            else if (mode == XZrev)
            {
                set(geometryFactory.createPoint(0, offset, 0), geometryFactory.X(), geometryFactory.Z(), geometryFactory.minY(), geometryFactory.WORLD());
                this.mode = XZrev;
            }
            else if (mode == XYrev)
            {
                set(geometryFactory.createPoint(0, offset, 0), geometryFactory.Y(), geometryFactory.X(), geometryFactory.minZ(), geometryFactory.WORLD());
                this.mode = XYrev;
            }
            else
            {
                //XY
                set(geometryFactory.createPoint(0, 0, offset), geometryFactory.X(), geometryFactory.Y(), geometryFactory.Z(), geometryFactory.WORLD());
                this.mode = XY;
            }

            this.T2D3D = this.getTransformToWorld();
        }

        public HS_PlanarMap(HS_Coord c)
        {
            if (Math.Abs(c.xd) > Math.Abs(c.yd))
            {
                mode = Math.Abs(c.xd) > Math.Abs(c.zd) ? YZ : XY;
            }
            else
            {
                mode = Math.Abs(c.yd) > Math.Abs(c.zd) ? XZ : XY;
            }
            if (mode < 0 || mode > 5)
            {
                throw new IndexOutOfRangeException();
            }
            if (mode == YZ)
            {
                set(geometryFactory.createPoint(offset, 0, 0), geometryFactory.Y(), geometryFactory.Z(), geometryFactory.X(), geometryFactory.WORLD());
                this.mode = YZ;
            }else if (mode == XZ)
            {
                set(geometryFactory.createPoint(0,offset,  0), geometryFactory.Z(), geometryFactory.X(), geometryFactory.Y(), geometryFactory.WORLD());
                this.mode = XZ;
            }else if (mode == YZrev)
            {
                set(geometryFactory.createPoint(0, offset, 0), geometryFactory.Z(), geometryFactory.Y(), geometryFactory.minX(), geometryFactory.WORLD());
                this.mode = YZrev;
            }else if (mode == XZrev)
            {
                set(geometryFactory.createPoint(0, offset, 0), geometryFactory.X(), geometryFactory.Z(), geometryFactory.minY(), geometryFactory.WORLD());
                this.mode = XZrev;
            }else if (mode == XYrev)
            {
                set(geometryFactory.createPoint(0, offset, 0), geometryFactory.Y(), geometryFactory.X(), geometryFactory.minZ(), geometryFactory.WORLD());
                this.mode = XYrev;
            }
            else
            {
                //XY
                set(geometryFactory.createPoint(0, 0,offset), geometryFactory.X(), geometryFactory.Y(), geometryFactory.Z(), geometryFactory.WORLD());
                this.mode = XY;
            }

            this.T2D3D = this.getTransformToWorld();
        }
        public HS_PlanarMap(int mode ,double offset):base()
        {
            
            this.mode = mode;
            this.offset = offset;

            if (mode < 0 || mode > 5)
            {
                throw new IndexOutOfRangeException();
            }
            if (mode == YZ)
            {
                set(geometryFactory.createPoint(offset, 0, 0), geometryFactory.Y(), geometryFactory.Z(), geometryFactory.X(), geometryFactory.WORLD());
                this.mode = YZ;
            }
            else if (mode == XZ)
            {
                set(geometryFactory.createPoint(0, offset, 0), geometryFactory.Z(), geometryFactory.X(), geometryFactory.Y(), geometryFactory.WORLD());
                this.mode = XZ;
            }
            else if (mode == YZrev)
            {
                set(geometryFactory.createPoint(0, offset, 0), geometryFactory.Z(), geometryFactory.Y(), geometryFactory.minX(), geometryFactory.WORLD());
                this.mode = YZrev;
            }
            else if (mode == XZrev)
            {
                set(geometryFactory.createPoint(0, offset, 0), geometryFactory.X(), geometryFactory.Z(), geometryFactory.minY(), geometryFactory.WORLD());
                this.mode = XZrev;
            }
            else if (mode == XYrev)
            {
                set(geometryFactory.createPoint(0, offset, 0), geometryFactory.Y(), geometryFactory.X(), geometryFactory.minZ(), geometryFactory.WORLD());
                this.mode = XYrev;
            }
            else
            {
                //XY
                set(geometryFactory.createPoint(0, 0, offset), geometryFactory.X(), geometryFactory.Y(), geometryFactory.Z(), geometryFactory.WORLD());
                this.mode = XY;
            }

            this.T2D3D = this.getTransformToWorld();
        }


        public HS_PlanarMap(int mode)
        {
            this.mode = mode;
            this.offset =0;

            if (mode < 0 || mode > 5)
            {
                throw new IndexOutOfRangeException();
            }
            if (mode == YZ)
            {
                set(geometryFactory.createPoint(offset, 0, 0), geometryFactory.Y(), geometryFactory.Z(), geometryFactory.X(), geometryFactory.WORLD());
                this.mode = YZ;
            }
            else if (mode == XZ)
            {
                set(geometryFactory.createPoint(0, offset, 0), geometryFactory.Z(), geometryFactory.X(), geometryFactory.Y(), geometryFactory.WORLD());
                this.mode = XZ;
            }
            else if (mode == YZrev)
            {
                set(geometryFactory.createPoint(0, offset, 0), geometryFactory.Z(), geometryFactory.Y(), geometryFactory.minX(), geometryFactory.WORLD());
                this.mode = YZrev;
            }
            else if (mode == XZrev)
            {
                set(geometryFactory.createPoint(0, offset, 0), geometryFactory.X(), geometryFactory.Z(), geometryFactory.minY(), geometryFactory.WORLD());
                this.mode = XZrev;
            }
            else if (mode == XYrev)
            {
                set(geometryFactory.createPoint(0, offset, 0), geometryFactory.Y(), geometryFactory.X(), geometryFactory.minZ(), geometryFactory.WORLD());
                this.mode = XYrev;
            }
            else
            {
                //XY
                set(geometryFactory.createPoint(0, 0, offset), geometryFactory.X(), geometryFactory.Y(), geometryFactory.Z(), geometryFactory.WORLD());
                this.mode = XY;
            }

            this.T2D3D = this.getTransformToWorld();
        }

        public HS_PlanarMap(HS_Plane P) : base(P.getOrigin(), P.getU(), P.getV(), P.getW(), new HS_CoordinateSystem())
        {
            Console.WriteLine("map:  " + P.getU()+ " " + P.getV()+" "+P.getW());
            mode = PLANE;
            T2D3D = getTransformToWorld();
        }
        public HS_PlanarMap(HS_Plane P,double offset) : base(P.getOrigin().addMul(offset,P.getNormal()), P.getU(), P.getV(), P.getW(), new HS_CoordinateSystem())
        {
            mode = PLANE;
            T2D3D = getTransformToWorld();
        }

        public void mapPoint3D(HS_Coord p, HS_MutableCoord result)
        {
            switch (this.mode)
            {
                case 0:
                    result.Set(p.yd, p.zd, p.xd - this.offset);
                    break;
                case 1:
                    result.Set(p.zd, p.xd, p.yd - this.offset);
                    break;
                case 2:
                    result.Set(p.xd, p.yd, p.zd - this.offset);
                    break;
                case 3:
                    result.Set(p.zd, p.yd, this.offset - p.xd);
                    break;
                case 4:
                    result.Set(p.xd, p.zd, this.offset - p.yd);
                    break;
                case 5:
                    result.Set(p.yd, p.xd, this.offset - p.zd);
                    break;
                default:
                    this.T2D3D.applyInvAsPointInto(p, result);
                    break;
            }
        }

        public void mapPoint3D(double x, double y, double z, HS_MutableCoord result)
        {
            switch (this.mode)
            {
                case 0:
                    result.Set(y, z, x - this.offset);
                    break;
                case 1:
                    result.Set(z, x, y - this.offset);
                    break;
                case 2:
                    result.Set(x, y, z - this.offset);
                    break;
                case 3:
                    result.Set(z, y, this.offset - x);
                    break;
                case 4:
                    result.Set(x, z, this.offset - y);
                    break;
                case 5:
                    result.Set(y, x, this.offset - z);
                    break;
                default:
                    this.T2D3D.applyInvAsPointInto(x, y, z, result);
                    break;
            }
        }

        public HS_Coord mapPoint3D(HS_Coord p)
        {
            HS_Point result = new HS_Point();
            switch (this.mode)
            {
                case 0:
                    result.Set(p.yd, p.zd, p.xd - this.offset);
                    break;
                case 1:
                    result.Set(p.zd, p.xd, p.yd - this.offset);
                    break;
                case 2:
                    result.Set(p.xd, p.yd, p.zd - this.offset);
                    break;
                case 3:
                    result.Set(p.zd, p.yd, this.offset - p.xd);
                    break;
                case 4:
                    result.Set(p.xd, p.zd, this.offset - p.yd);
                    break;
                case 5:
                    result.Set(p.yd, p.xd, this.offset - p.zd);
                    break;
                default:
                    
                    this.T2D3D.applyInvAsPointInto(p, result);
                    break;
            }
            return result;
        }

        public HS_Coord mapPoint3D(double x, double y, double z)
        {
            HS_Point result = new HS_Point();
            switch (this.mode)
            {
                case 0:
                    result.Set(y, z, x - this.offset);
                    break;
                case 1:
                    result.Set(z, x, y - this.offset);
                    break;
                case 2:
                    result.Set(x, y, z - this.offset);
                    break;
                case 3:
                    result.Set(z, y, this.offset - x);
                    break;
                case 4:
                    result.Set(x, z, this.offset - y);
                    break;
                case 5:
                    result.Set(y, x, this.offset - z);
                    break;
                default:
                    this.T2D3D.applyInvAsPointInto(x, y, z, result);
                    break;
            }
            return result;
        }

        public void mapVector3D(HS_Coord v, HS_MutableCoord result)
        {
            switch (this.mode)
            {
                case 0:
                    result.Set(v.yd, v.zd, v.xd - this.offset);
                    break;
                case 1:
                    result.Set(v.zd, v.xd, v.yd - this.offset);
                    break;
                case 2:
                    result.Set(v.xd, v.yd, v.zd - this.offset);
                    break;
                case 3:
                    result.Set(v.zd, v.yd, this.offset - v.xd);
                    break;
                case 4:
                    result.Set(v.xd, v.zd, this.offset - v.yd);
                    break;
                case 5:
                    result.Set(v.yd, v.xd, this.offset - v.zd);
                    break;
                default:
                    this.T2D3D.applyInvAsVectorInto(v, result);
                    break;
            }
        }

        public void mapVector3D(double x, double y, double z, HS_MutableCoord result)
        {
            switch (this.mode)
            {
                case 0:
                    result.Set(y, z, x - this.offset);
                    break;
                case 1:
                    result.Set(z, x, y - this.offset);
                    break;
                case 2:
                    result.Set(x, y, z - this.offset);
                    break;
                case 3:
                    result.Set(z, y, this.offset - x);
                    break;
                case 4:
                    result.Set(x, z, this.offset - y);
                    break;
                case 5:
                    result.Set(y, x, this.offset - z);
                    break;
                default:
                    this.T2D3D.applyInvAsVectorInto(x, y, z, result);
                    break;
            }
        }

        public HS_Coord mapVector3D(HS_Coord v)
        {
            HS_Vector result = new HS_Vector();
            switch (this.mode)
            {
                case 0:
                    result.Set(v.yd, v.zd, v.xd - this.offset);
                    break;
                case 1:
                    result.Set(v.zd, v.xd, v.yd - this.offset);
                    break;
                case 2:
                    result.Set(v.xd, v.yd, v.zd - this.offset);
                    break;
                case 3:
                    result.Set(v.zd, v.yd, this.offset - v.xd);
                    break;
                case 4:
                    result.Set(v.xd, v.zd, this.offset - v.yd);
                    break;
                case 5:
                    result.Set(v.yd, v.xd, this.offset - v.zd);
                    break;
                default:
                    this.T2D3D.applyInvAsVectorInto(v, result);
                    break;
            }
            return result;
        }

        public HS_Coord mapVector3D(double x, double y, double z)
        {
            HS_Vector result = new HS_Vector();
            switch (this.mode)
            {
                case 0:
                    result.Set(y, z, x - this.offset);
                    break;
                case 1:
                    result.Set(z, x, y - this.offset);
                    break;
                case 2:
                    result.Set(x, y, z - this.offset);
                    break;
                case 3:
                    result.Set(z, y, this.offset - x);
                    break;
                case 4:
                    result.Set(x, z, this.offset - y);
                    break;
                case 5:
                    result.Set(y, x, this.offset - z);
                    break;
                default:
                    this.T2D3D.applyInvAsVectorInto(x, y, z, result);
                    break;
            }
            return result;
        }

        public void unmapPoint2D(HS_Coord p, HS_MutableCoord result)
        {
            switch (this.mode)
            {
                case 0:
                    result.Set(this.offset, (double)p.xf, (double)p.yf);
                    break;
                case 1:
                    result.Set((double)p.yf, this.offset, (double)p.xf);
                    break;
                case 2:
                    result.Set((double)p.xf, (double)p.yf, this.offset);
                    break;
                case 3:
                    result.Set(this.offset, (double)p.yf, (double)p.xf);
                    break;
                case 4:
                    result.Set((double)p.xf, this.offset, (double)p.yf);
                    break;
                case 5:
                    result.Set((double)p.yf, (double)p.xf, this.offset);
                    break;
                default:
                    this.T2D3D.applyAsPointInto((double)p.xf, (double)p.yf, 0.0D, result);
                    break;
            }
        }

        public void unmapPoint2D(double u, double v, HS_MutableCoord result)
        {
            switch (this.mode)
            {
                case 0:
                    result.Set(this.offset, u, v);
                    break;
                case 1:
                    result.Set(v, this.offset, u);
                    break;
                case 2:
                    result.Set(u, v, this.offset);
                    break;
                case 3:
                    result.Set(this.offset, v, u);
                    break;
                case 4:
                    result.Set(u, this.offset, v);
                    break;
                case 5:
                    result.Set(v, u, this.offset);
                    break;
                default:
                    this.T2D3D.applyAsPointInto(u, v, 0.0D, result);
                    break;
            }
        }

        public HS_Coord unmapPoint2D(HS_Coord p)
        {
            HS_Point result = new HS_Point();
            switch (this.mode)
            {
                case 0:
                    result.Set(this.offset, (double)p.xf, (double)p.yf);
                    break;
                case 1:
                    result.Set((double)p.yf, this.offset, (double)p.xf);
                    break;
                case 2:
                    result.Set((double)p.xf, (double)p.yf, this.offset);
                    break;
                case 3:
                    result.Set(this.offset, (double)p.yf, (double)p.xf);
                    break;
                case 4:
                    result.Set((double)p.xf, this.offset, (double)p.yf);
                    break;
                case 5:
                    result.Set((double)p.yf, (double)p.xf, this.offset);
                    break;
                default:
                    this.T2D3D.applyAsPointInto((double)p.xf, (double)p.yf, 0.0D, result);
                    break;
            }
            return result;
        }

        public HS_Coord unmapPoint2D(double u, double v)
        {
            HS_Point result = new HS_Point();
            switch (this.mode)
            {
                case 0:
                    result.Set(this.offset, u, v);
                    break;
                case 1:
                    result.Set(v, this.offset, u);
                    break;
                case 2:
                    result.Set(u, v, this.offset);
                    break;
                case 3:
                    result.Set(this.offset, v, u);
                    break;
                case 4:
                    result.Set(u, this.offset, v);
                    break;
                case 5:
                    result.Set(v, u, this.offset);
                    break;
                default:
                    this.T2D3D.applyAsPointInto(u, v, 0.0D, result);
                    break;
            }
            return result;
        }

        public void unmapPoint3D(HS_Coord p, HS_MutableCoord result)
        {
            switch (this.mode)
            {
                case 0:
                    result.Set(p.zd + this.offset, p.xd, p.yd);
                    break;
                case 1:
                    result.Set(p.yd, p.zd + this.offset, p.xd);
                    break;
                case 2:
                    result.Set(p.xd, p.yd, p.zd + this.offset);
                    break;
                case 3:
                    result.Set(this.offset - p.zd, p.yd, p.xd);
                    break;
                case 4:
                    result.Set(p.xd, this.offset - p.zd, p.yd);
                    break;
                case 5:
                    result.Set(p.yd, p.xd, this.offset - p.zd);
                    break;
                default:
                    this.T2D3D.applyAsPointInto(p, result);
                    break;
            }
        }

        public void unmapPoint3D(double u, double v, double w, HS_MutableCoord result)
        {
            switch (this.mode)
            {
                case 0:
                    result.Set(w + this.offset, u, v);
                    break;
                case 1:
                    result.Set(v, w + this.offset, u);
                    break;
                case 2:
                    result.Set(u, v, w + this.offset);
                    break;
                case 3:
                    result.Set(this.offset - w, v, u);
                    break;
                case 4:
                    result.Set(u, this.offset - w, v);
                    break;
                case 5:
                    result.Set(v, u, this.offset - w);
                    break;
                default:
                    this.T2D3D.applyAsPointInto(u, v, w, result);
                    break;
            }
        }

        public HS_Coord unmapPoint3D(HS_Coord p)
        {
            HS_Point result = new HS_Point();
            switch (this.mode)
            {
                case 0:
                    result.Set(p.zd + this.offset, p.xd, p.yd);
                    break;
                case 1:
                    result.Set(p.yd, p.zd + this.offset, p.xd);
                    break;
                case 2:
                    result.Set(p.xd, p.yd, p.zd + this.offset);
                    break;
                case 3:
                    result.Set(this.offset - p.zd, p.yd, p.xd);
                    break;
                case 4:
                    result.Set(p.xd, this.offset - p.zd, p.yd);
                    break;
                case 5:
                    result.Set(p.yd, p.xd, this.offset - p.zd);
                    break;
                default:
                    this.T2D3D.applyAsPointInto(p, result);
                    break;
            }
            return result;
        }

        public HS_Coord unmapPoint3D(double u, double v, double w)
        {
            HS_Point result = new HS_Point();
            switch (this.mode)
            {
                case 0:
                    result.Set(w + this.offset, u, v);
                    break;
                case 1:
                    result.Set(v, w + this.offset, u);
                    break;
                case 2:
                    result.Set(u, v, w + this.offset);
                    break;
                case 3:
                    result.Set(this.offset - w, v, u);
                    break;
                case 4:
                    result.Set(u, this.offset - w, v);
                    break;
                case 5:
                    result.Set(v, u, this.offset - w);
                    break;
                default:
                    this.T2D3D.applyAsPointInto(u, v, w, result);
                    break;
            }
            return result;
        }

        public void unmapVector2D(HS_Coord v, HS_MutableCoord result)
        {
            switch (this.mode)
            {
                case 0:
                    result.Set(this.offset, (double)v.xf, (double)v.yf);
                    break;
                case 1:
                    result.Set((double)v.yf, this.offset, (double)v.xf);
                    break;
                case 2:
                    result.Set((double)v.xf, (double)v.yf, this.offset);
                    break;
                case 3:
                    result.Set(this.offset, (double)v.yf, (double)v.xf);
                    break;
                case 4:
                    result.Set((double)v.xf, this.offset, (double)v.yf);
                    break;
                case 5:
                    result.Set((double)v.yf, (double)v.xf, this.offset);
                    break;
                default:
                    this.T2D3D.applyAsVectorInto((double)v.xf, (double)v.yf, 0.0D, result);
                    break;
            }
        }

        public void unmapVector2D(double u, double v, HS_MutableCoord result)
        {
            switch (this.mode)
            {
                case 0:
                    result.Set(0.0D, u, v + this.offset);
                    break;
                case 1:
                    result.Set(v, 0.0D, u + this.offset);
                    break;
                case 2:
                    result.Set(u, v, this.offset);
                    break;
                case 3:
                    result.Set(this.offset, v, u);
                    break;
                case 4:
                    result.Set(u, this.offset, v);
                    break;
                case 5:
                    result.Set(v, u, this.offset);
                    break;
                default:
                    this.T2D3D.applyAsVectorInto(u, v, 0.0D, result);
                    break;
            }
        }

        public HS_Coord unmapVector2D(HS_Coord v)
        {
            HS_Vector result = new HS_Vector();
            switch (this.mode)
            {
                case 0:
                    result.Set(this.offset, (double)v.xf, (double)v.yf);
                    break;
                case 1:
                    result.Set((double)v.yf, this.offset, (double)v.xf);
                    break;
                case 2:
                    result.Set((double)v.xf, (double)v.yf, this.offset);
                    break;
                case 3:
                    result.Set(this.offset, (double)v.yf, (double)v.xf);
                    break;
                case 4:
                    result.Set((double)v.xf, this.offset, (double)v.yf);
                    break;
                case 5:
                    result.Set((double)v.yf, (double)v.xf, this.offset);
                    break;
                default:
                    this.T2D3D.applyAsVectorInto((double)v.xf, (double)v.yf, 0.0D, result);
                    break;
            }
            return result;
        }

        public HS_Coord unmapVector2D(double u, double v)
        {
            HS_Vector result = new HS_Vector();
            switch (this.mode)
            {
                case 0:
                    result.Set(0.0D, u, v + this.offset);
                    break;
                case 1:
                    result.Set(v, 0.0D, u + this.offset);
                    break;
                case 2:
                    result.Set(u, v, this.offset);
                    break;
                case 3:
                    result.Set(this.offset, v, u);
                    break;
                case 4:
                    result.Set(u, this.offset, v);
                    break;
                case 5:
                    result.Set(v, u, this.offset);
                    break;
                default:
                    this.T2D3D.applyAsVectorInto(u, v, 0.0D, result);
                    break;
            }
            return result;
        }

        public void unmapVector3D(HS_Coord v, HS_MutableCoord result)
        {
            switch (this.mode)
            {
                case 0:
                    result.Set(v.zd + this.offset, v.xd, v.yd);
                    break;
                case 1:
                    result.Set(v.yd, v.zd + this.offset, v.xd);
                    break;
                case 2:
                    result.Set(v.xd, v.yd, v.zd + this.offset);
                    break;
                case 3:
                    result.Set(this.offset - v.zd, v.yd, v.xd);
                    break;
                case 4:
                    result.Set(v.xd, this.offset - v.zd, v.yd);
                    break;
                case 5:
                    result.Set(v.yd, v.xd, this.offset - v.zd);
                    break;
                default:
                    this.T2D3D.applyAsVectorInto(v, result);
                    break;
            }
        }

        public void unmapVector3D(double u, double v, double w, HS_MutableCoord result)
        {
            switch (this.mode)
            {
                case 0:
                    result.Set(w + this.offset, u, v);
                    break;
                case 1:
                    result.Set(v, w + this.offset, u);
                    break;
                case 2:
                    result.Set(u, v, w + this.offset);
                    break;
                case 3:
                    result.Set(this.offset - w, v, u);
                    break;
                case 4:
                    result.Set(u, this.offset - w, v);
                    break;
                case 5:
                    result.Set(v, u, this.offset - w);
                    break;
                default:
                    this.T2D3D.applyAsVectorInto(u, v, w, result);
                    break;
            }
        }

        public HS_Coord unmapVector3D(HS_Coord v)
        {
            HS_Vector result = new HS_Vector();
            switch (this.mode)
            {
                case 0:
                    result.Set(v.zd + this.offset, v.xd, v.yd);
                    break;
                case 1:
                    result.Set(v.yd, v.zd + this.offset, v.xd);
                    break;
                case 2:
                    result.Set(v.xd, v.yd, v.zd + this.offset);
                    break;
                case 3:
                    result.Set(this.offset - v.zd, v.yd, v.xd);
                    break;
                case 4:
                    result.Set(v.xd, this.offset - v.zd, v.yd);
                    break;
                case 5:
                    result.Set(v.yd, v.xd, this.offset - v.zd);
                    break;
                default:
                    this.T2D3D.applyAsVectorInto(v, result);
                    break;
            }
            return result;
        }

        public HS_Coord unmapVector3D(double u, double v, double w)
        {
            HS_Vector result = new HS_Vector();
            switch (this.mode)
            {
                case 0:
                    result.Set(w + this.offset, u, v);
                    break;
                case 1:
                    result.Set(v, w + this.offset, u);
                    break;
                case 2:
                    result.Set(u, v, w + this.offset);
                    break;
                case 3:
                    result.Set(this.offset - w, v, u);
                    break;
                case 4:
                    result.Set(u, this.offset - w, v);
                    break;
                case 5:
                    result.Set(v, u, this.offset - w);
                    break;
                default:
                    this.T2D3D.applyAsVectorInto(u, v, w, result);
                    break;
            }
            return result;
        }
    }
}
