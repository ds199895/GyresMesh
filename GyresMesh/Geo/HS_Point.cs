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
        //public static HS_Point operator +(HS_Point left, HS_Coord right)
        //{
        //    return (HS_Point)left.add(right);
        //}
        //public static HS_Point operator -(HS_Point vec)
        //{
        //    return (HS_Point)vec.mul(-1D);
        //}
        //public static HS_Point operator -(HS_Point left, HS_Point right)
        //{
        //    return (HS_Point)left.sub(right);
        //}
        //public static HS_Point operator -(HS_Point left,HS_Coord right)
        //{
        //    return new HS_Point(left.xd - right.xd, left.yd - right.yd, left.zd - right.zd);
        //}
        ////public static HS_Vector operator *(Quaternion quat, HS_Vector vec)
        ////{

        ////}
        //public static HS_Point operator *(HS_Point vec, float scale)
        //{
        //    return (HS_Point)vec.mul(scale);
        //}
        //public static HS_Point operator* (HS_Point vec, double scale)
        //{
        //    return (HS_Point)vec.mul(scale);
        //}
        //public static HS_Point operator* (float scale, HS_Point vec)
        //{
        //    return (HS_Point)vec.mul(scale);
        //}
        //public static HS_Point operator* (double scale, HS_Point vec)
        //{
        //    return (HS_Point)vec.mul(scale);
        //}
        //public static HS_Point operator *(HS_Point vec, HS_Point scale)
        //{
        //    return new HS_Point(vec.xd * scale.xd, vec.yd * scale.yd, vec.zd * scale.zd);
        //}
        ////public static HS_Vector operator *(HS_Vector vec, Matrix3 mat)
        ////{

        ////}
        ////public static HS_Vector operator *(Matrix3 mat, HS_Vector vec);
        //public static HS_Point operator /(HS_Point vec, float scale)
        //{
        //    return (HS_Point)vec.mul(1 / scale);
        //}
        //public static HS_Point operator /(HS_Point vec, double scale)
        //{
        //    return (HS_Point)vec.mul(1 / scale);
        //}
        //public static bool operator ==(HS_Point left, HS_Point right)
        //{
        //    //if(HS_Epsilon.isZero(left.xd - right.xd) && HS_Epsilon.isZero(left.y - right.y) && HS_Epsilon.isZero(left.z - right.z))
        //    //{
        //    //    return true;
        //    //}
        //    //else
        //    //{
        //    //    return false;
        //    //}
        //    return HS_Epsilon.isZero(left.xd - right.xd) && HS_Epsilon.isZero(left.yd - right.yd) && HS_Epsilon.isZero(left.zd - right.zd);
        //}
        //public static bool operator !=(HS_Point left, HS_Point right)
        //{

        //    return !(HS_Epsilon.isZero(left.xd - right.xd) && HS_Epsilon.isZero(left.yd - right.yd) && HS_Epsilon.isZero(left.zd - right.zd));
        //}

    }
}
