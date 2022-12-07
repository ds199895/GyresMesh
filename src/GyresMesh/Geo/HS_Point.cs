using Hsy.HsMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_Point:HS_Vector
    {
        public new static HS_Point origin = new HS_Point(0.0D, 0.0D, 0.0D);
        public new static HS_Point xaxis = new HS_Point(1.0D, 0.0D, 0.0D);
        public new  static HS_Point yaxis = new HS_Point(0.0D, 1.0D, 0.0D);
        public new static HS_Point zaxis = new HS_Point(0.0D, 0.0D, 1.0D);
        public HS_Point(HS_Coord hs_coord):base(hs_coord)
        {
        }
        public HS_Point()
        {
            this.xd = this.yd = this.zd = 0;
        }
        public HS_Point(float x, float y, float z):base(x,y,z)
        {
        }


        public HS_Point(double x, double y, double z):base(x,y,z)
        {
        }

        public HS_Point(double x,double y) : base(x, y)
        {

        }
        public HS_Point(double[] x) : base(x)
        {
        }

        public new HS_Point copy()
        {
            return new HS_Point(this.xd, this.yd, this.zd);
        }

        public new HS_Point add(HS_Coord p)
        {
            return new HS_Point(xd + p.xd, yd + p.yd, zd + p.zd);
        }

        public new HS_Point mul(double f)
        {
            return new HS_Point(xd * f, yd * f, zd * f);
        }
        public HS_Point sub(HS_Coord v)
        {
            return new HS_Point(this.xd - v.xd, this.yd - v.yd, this.zd - v.zd);
        }
        public new static HS_Point sub(HS_Coord v1, HS_Coord v2)
        {
            return new HS_Point(v1.xd - v2.xd, v1.yd - v2.yd, v1.zd - v2.zd);
        }

        public new HS_Point mulSelf(double f)
        {
            Set(f * xd, f * yd, f * zd);
            return this;
        }

        public HS_Point scaleSelf(double f)
        {
            mulSelf(f);
            return this;
        }
        public new  HS_Point addSelf(HS_Coord p)
        {
            Set(xd + p.xd, yd+ p.yd, zd+ p.zd);
            return this;
        }
        public new HS_Point subSelf(HS_Coord p)
        {
            Set(xd - p.xd, yd - p.yd, zd - p.zd);
            return this;
        }
        public double getDistance(HS_Coord p)
        {
            return HS_CoordOp3D.getDistance3D(xd, yd, zd, p.xd, p.yd,p.zd);
        }

        //public void Set(HS_Coord c)
        //{
        //    this.x = c.X();
        //    this.y = c.Y();
        //    this.z = c.Z();
        //}

        //public void SetX(float x)
        //{
        //    this.x = x;
        //}

        //public void SetY(float y)
        //{
        //    this.y = y;
        //}

        //public void SetZ(float z)
        //{
        //    this.z = z;
        //}

        //public int CompareTo(HS_Coord p)
        //{
        //    int cmp = this.X().CompareTo(p.X());
        //    if (cmp != 0)
        //    {
        //        return cmp;
        //    }
        //    cmp = this.Y().CompareTo(p.Y());
        //    if (cmp != 0)
        //    {
        //        return cmp;
        //    }
        //    cmp = this.Z().CompareTo(p.Z());
        //    if (cmp != 0)
        //    {
        //        return cmp;
        //    }
        //    return 0;
        //}

        override
        public String ToString()
        {
            return "HS_Point "  + " [x=" + xd + ", y=" + yd
        + ", z=" + zd+ "]";
        }

        //public static HS_Point operator +(HS_Point left, HS_Point right)
        //{
        //    return (HS_Point)left.add(right);
        //}
        public static HS_Point operator +(HS_Point left, HS_Vector right)
        {
            return (HS_Point)left.add(right);
        }
        public static HS_Point operator +(HS_Point left, HS_Coord right)
        {
            return (HS_Point)left.add(right);
        }
        public static HS_Point operator -(HS_Point vec)
        {
            return (HS_Point)vec.mul(-1D);
        }
        public static HS_Point operator -(HS_Point left, HS_Point right)
        {
            return (HS_Point)left.sub(right);
        }
        public static HS_Point operator -(HS_Point left, HS_Coord right)
        {
            return new HS_Point(left.xd - right.xd, left.yd - right.yd, left.zd - right.zd);
        }
        //public static HS_Vector operator *(Quaternion quat, HS_Vector vec)
        //{

        //}
        public static HS_Point operator *(HS_Point vec, float scale)
        {
            return (HS_Point)vec.mul(scale);
        }
        public static HS_Point operator *(HS_Point vec, double scale)
        {
            return (HS_Point)vec.mul(scale);
        }
        public static HS_Point operator *(float scale, HS_Point vec)
        {
            return (HS_Point)vec.mul(scale);
        }
        public static HS_Point operator *(double scale, HS_Point vec)
        {
            return (HS_Point)vec.mul(scale);
        }
        public static HS_Point operator *(HS_Point vec, HS_Point scale)
        {
            return new HS_Point(vec.xd * scale.xd, vec.yd * scale.yd, vec.zd * scale.zd);
        }
        //public static HS_Vector operator *(HS_Vector vec, Matrix3 mat)
        //{

        //}
        //public static HS_Vector operator *(Matrix3 mat, HS_Vector vec);
        public static HS_Point operator /(HS_Point vec, float scale)
        {
            return (HS_Point)vec.mul(1 / scale);
        }
        public static HS_Point operator /(HS_Point vec, double scale)
        {
            return (HS_Point)vec.mul(1 / scale);
        }
        public static bool operator ==(HS_Point left, HS_Point right)
        {
            //if(HS_Epsilon.isZero(left.xd - right.xd) && HS_Epsilon.isZero(left.y - right.y) && HS_Epsilon.isZero(left.z - right.z))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return HS_Epsilon.isZero(left.xd - right.xd) && HS_Epsilon.isZero(left.yd - right.yd) && HS_Epsilon.isZero(left.zd - right.zd);
        }
        public static bool operator !=(HS_Point left, HS_Point right)
        {

            return !(HS_Epsilon.isZero(left.xd - right.xd) && HS_Epsilon.isZero(left.yd - right.yd) && HS_Epsilon.isZero(left.zd - right.zd));
        }

    }
}
