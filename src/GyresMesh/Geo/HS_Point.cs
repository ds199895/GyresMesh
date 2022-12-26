using Hsy.Core;
using Hsy.HsMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public class HS_Point : HS_Vector
    {
        public new static HS_Point origin = new HS_Point(0.0D, 0.0D, 0.0D);
        public new static HS_Point xaxis = new HS_Point(1.0D, 0.0D, 0.0D);
        public new static HS_Point yaxis = new HS_Point(0.0D, 1.0D, 0.0D);
        public new static HS_Point zaxis = new HS_Point(0.0D, 0.0D, 1.0D);
        public new double wd { get { return 1; } }
        public HS_Point(HS_Coord hs_coord) : base(hs_coord)
        {
        }
        public HS_Point()
        {
            this.xd = this.yd = this.zd = 0;
        }
        public HS_Point(float x, float y, float z) : base(x, y, z)
        {
        }


        public HS_Point(double x, double y, double z) : base(x, y, z)
        {
        }

        public HS_Point(double x, double y) : base(x, y)
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
        public new HS_Point addSelf(HS_Coord p)
        {
            Set(xd + p.xd, yd + p.yd, zd + p.zd);
            return this;
        }
        public new HS_Point subSelf(HS_Coord p)
        {
            Set(xd - p.xd, yd - p.yd, zd - p.zd);
            return this;
        }
        public double getDistance(HS_Coord p)
        {
            return HS_CoordOp3D.getDistance3D(xd, yd, zd, p.xd, p.yd, p.zd);
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
        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            if (o == this)
            {
                return true;
            }
            if (!(o is HS_Coord)) {
                return false;
            }
             HS_Coord p = (HS_Coord)o;
            if (!HS_Epsilon.isEqual(xd, p.xd))
            {
                return false;
            }
            if (!HS_Epsilon.isEqual(yd, p.yd))
            {
                return false;
            }
            if (!HS_Epsilon.isEqual(zd, p.zd))
            {
                return false;
            }
            if (!HS_Epsilon.isEqual(wd, p.wd))
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return HS_HashCode.calculateHashCode(this.xd, this.yd);
        }


        public override String ToString()
        {
            return "HS_Point " + " [x=" + xd + ", y=" + yd
        + ", z=" + zd + "]";
        }
        
    public new HS_Point apply( HS_Transform3D T)
        {
            return T.applyAsPoint(this);
        }

        /*
         * (non-Javadoc)
         * @see
         * HSlut.geom.HS_CoordinateTransform#applyAsPoint(HSlut.geom.HS_Transform)
         */
        
    public new HS_Point applyAsPoint( HS_Transform3D T)
        {
            return T.applyAsPoint(this);
        }

        /*
         * (non-Javadoc)
         * @see
         * HSlut.geom.HS_CoordinateTransform#applyAsNormal(HSlut.geom.HS_Transform)
         */
        
    public new HS_Point applyAsNormal( HS_Transform3D T)
        {
            return new HS_Point(T.applyAsNormal(this));
        }

        /*
         * (non-Javadoc)
         * @see
         * HSlut.geom.HS_CoordinateTransform#applyAsVector(HSlut.geom.HS_Transform)
         */
        
    public new HS_Point applyAsVector( HS_Transform3D T)
        {
            return new HS_Point(T.applyAsVector(this));
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_CoordinateTransform3D#translate(double, double)
         */
        
    public new HS_Point translate( double px,  double py,
             double pz)
        {
            return new HS_Point(this.xd + px, this.yd + py, this.zd + pz);
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_CoordinateTransform3D#translate(HSlut.geom.HS_Coord)
         */
        
    public new HS_Point translate( HS_Coord p)
        {
            return new HS_Point(xd + p.xd, yd + p.yd, zd + p.zd);
        }


        public new HS_Point apply2D(HS_Transform2D T)
        {
            return T.applyAsPoint2D(this);
        }


        public new HS_Point applyAsPoint2D(HS_Transform2D T)
        {
            return T.applyAsPoint2D(this);
        }


        public new HS_Point applyAsVector2D(HS_Transform2D T)
        {
            return new HS_Point(T.applyAsVector2D(this));
        }


        public new HS_Point applyAsNormal2D(HS_Transform2D T)
        {
            return new HS_Point(T.applyAsNormal2D(this));
        }

        /*
         * (non-Javadoc)
         * see HSlut.geom.HS_CoordinateTransform2D#translate2D(double, double)
         */

        public new HS_Point translate2D(double px, double py)
        {
            return new HS_Point(this.xd + px, this.yd + py);
        }

        /*
         * (non-Javadoc)
         * see HSlut.geom.HS_CoordinateTransform2D#translate2D(HSlut.geom.HS_Coord)
         */

        public new HS_Point translate2D(HS_Coord p)
        {
            return new HS_Point(this.xd + p.xd, this.yd + p.yd);
        }



        public new HS_Point apply2DSelf(HS_Transform2D T)
        {
            T.applyAsPoint2DSelf(this);
            return this;
        }


        public new HS_Point applyAsPoint2DSelf(HS_Transform2D T)
        {
            T.applyAsPoint2DSelf(this);
            return this;
        }


        public  new HS_Point applyAsVector2DSelf(HS_Transform2D T)
        {
            T.applyAsVector2DSelf(this);
            return this;
        }


        public new HS_Point applyAsNormal2DSelf(HS_Transform2D T)
        {
            T.applyAsNormal2DSelf(this);
            return this;
        }

        /*
         * (non-Javadoc)
         * see HSlut.geom.HS_MutableCoordinateTransform3D#translate2DSelf(double,
         * double)
         */

        public new HS_Point translate2DSelf(double px, double py)
        {
            Set(xd + px, yd + py);
            return this;
        }

        /*
         * (non-Javadoc)
         * see
         * HSlut.geom.HS_MutableCoordinateTransform3D#translate2DSelf(HSlut.geom.
         * HS_Coord)
         */

        public new HS_Point translate2DSelf(HS_Coord p)
        {
            Set(xd + p.xd, yd + p.yd);
            return this;
        }

        
    public new HS_Point applySelf( HS_Transform3D T)
        {
            return applyAsPointSelf(T);
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_MutableCoordinateMath#applyAsPointSelf(HSlut.geom.
         * HS_Transform )
         */
        
    public new  HS_Point applyAsPointSelf( HS_Transform3D T)
        {
            T.applyAsPointSelf(this);
            return this;
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_MutableCoordinateMath#applyAsVectorSelf(HSlut.geom.
         * HS_Transform )
         */
        
    public new HS_Vector applyAsVectorSelf( HS_Transform3D T)
        {
            T.applyAsVectorSelf(this);
            return this;
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_MutableCoordinateMath#applyAsNormalSelf(HSlut.geom.
         * HS_Transform )
         */
        
    public new HS_Vector applyAsNormalSelf( HS_Transform3D T)
        {
            T.applyAsNormalSelf(this);
            return this;
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_MutableCoordinateTransform3D#translateSelf(double,
         * double, double)
         */
        
    public new HS_Point translateSelf( double px,  double py,
             double pz)
        {
            Set(xd + px, yd + py, zd + pz);
            return this;
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_MutableCoordinateTransform3D#translateSelf(HSlut.geom.
         * HS_Coord)
         */
        
    public new  HS_Point translateSelf( HS_Coord p)
        {
            Set(xd + p.xd, yd + p.yd, zd + p.zd);
            return this;
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
