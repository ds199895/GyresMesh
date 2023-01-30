using Hsy.Core;
using Hsy.HsMath;
using Hsy.IO;
using System;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Media.Media3D;

namespace Hsy.Geo
{
    public class HS_Vector : IObject, HS_MutableCoord
    {
        public static HS_Vector origin = new HS_Vector(0.0D, 0.0D, 0.0D);
        public static HS_Vector xaxis = new HS_Vector(1.0D, 0.0D, 0.0D);
        public static HS_Vector yaxis = new HS_Vector(0.0D, 1.0D, 0.0D);
        public static HS_Vector zaxis = new HS_Vector(0.0D, 0.0D, 1.0D);
        internal double _xd;
        internal double _yd;
        internal double _zd;
        internal float _xf;
        internal float _yf;
        internal float _zf;
        public float xf
        {
            get
            {
                return this._xf;
            }
            set
            {
                this._xf = value;
                this._xd = value;
            }
        }
        public float yf
        {
            get
            {
                return this._yf;
            }
            set
            {
                this._yf = value;
                this._yd = value;
            }
        }
        public float zf
        {
            get
            {
                return this._zf;
            }
            set
            {
                this._zf = value;
                this._zd = value;
            }
        }
        public double xd { get { return this._xd; } set { this._xd = value; this._xf = (float)value; } }
        public double yd { get { return this._yd; } set { this._yd = value; this._yf = (float)value; } }
        public double zd { get { return this._zd; } set { this._zd = value; this._zf = (float)value; } }
        public double wd { get { return 0; } }
        public HS_Vector()
        {
            this.xd = 0.0D;
            this.yd = 0.0D;
            this.zd = 0.0D;
            this.attribute.name = "Point";
        }

        public HS_Vector(HS_Coord hs_coord) : base()
        {
            this.xd = hs_coord.xd;
            this.yd = hs_coord.yd;
            this.zd = hs_coord.zd;
            this.attribute.name = "Point";
        }

        public HS_Vector(HS_Coord from, HS_Coord to) : base()
        {
            this.xd = to.xd - from.xd;
            this.yd = to.yd - from.yd;
            this.zd = to.zd - from.zd;
        }
        public HS_Vector(double x, double y) : base()
        {
            this.xd = x;
            this.yd = y;
            this.zd = 0.0;
        }
        public HS_Vector(double x, double y, double z) : base()
        {
            this.xd = x;
            this.yd = y;
            this.zd = z;
        }
        public HS_Vector(float x, float y, float z) : base()
        {
            this.xf = x;
            this.yf = y;
            this.zf = z;
        }
        public HS_Vector(double[] x) : base()
        {
            this.xd = x[0];
            this.yd = x[1];
            this.zd = x[2];
        }
        public HS_Vector add(double x, double y, double z)
        {
            this.xd += x;
            this.yd += y;
            this.zd += z;

            return this;
        }

        /**
         *
         *
         * @param p
         * @param q
         * @return
         */
        public static HS_Vector add( HS_Coord p,  HS_Coord q)
        {
            return new HS_Vector(q.xd + p.xd, q.yd + p.yd, q.zd + p.zd);
        }


        public HS_Vector get()
        {
            return this;
        }

        public double GetLength()
        {
            return this.dist(new HS_Vector());
        }


        public new HS_Vector dup()
        {
            return new HS_Vector(this.xd, this.yd, this.zd);
        }
        public HS_Vector add(HS_Vector v)
        {
            return new HS_Vector(this.xd + v.xd, this.yd + v.yd, this.zd + v.zd);
        }
        public HS_Vector add(HS_Coord v)
        {
            return new HS_Vector(this.xd + v.xd, this.yd + v.yd, this.zd + v.zd);
        }
        public HS_Vector sub(double x, double y, double z)
        {
            return new HS_Vector(this.xd - x, this.yd - y, this.zd - z);
        }

        public HS_Vector sub(HS_Vector v)
        {
            return new HS_Vector(this.xd - v.xd, this.yd - v.yd, this.zd - v.zd);
        }
        public static HS_Vector sub(HS_Coord v1, HS_Coord v2)
        {
            return new HS_Vector(v1.xd - v2.xd, v1.yd - v2.yd, v1.zd - v2.zd);
        }


        /**
         *
         *
         * @param p
         * @param q
         * @return
         */
        public static HS_Vector subToVector2D(HS_Coord p, HS_Coord q)
        {
            return new HS_Vector(p.xd - q.xd, p.yd - q.yd);
        }

        /**
         *
         *
         * @param p
         * @param q
         * @return
         */
        public static HS_Vector subToVector3D(HS_Coord p, HS_Coord q)
        {
            return new HS_Vector(p.xd - q.xd, p.yd - q.yd, p.zd - q.zd);
        }

        public static HS_Vector subToVector2D(HS_Coord p, double x,
                double y)
        {
            return new HS_Vector(p.xd - x, p.yd - y);
        }

        public static HS_Vector subToVector3D(HS_Coord p, double x,
                double y, double z)
        {
            return new HS_Vector(p.xd - x, p.yd - y, p.zd - z);
        }


        public HS_Vector mul(double v)
        {
            return new HS_Vector(this.xd * v, this.yd * v, this.zd * v);
        }

        public static HS_Vector mul(HS_Vector ve,double v)
        {
            return new HS_Vector(ve.xd * v, ve.yd * v,ve.zd * v);
        }
        public HS_Vector addSelf(HS_Coord p)
        {
            Set(xd + p.xd, yd + p.yd, zd + p.zd);
            return this;
        }
        public HS_Vector mulSelf(double f)
        {
            Set(f * xd, f * yd, f * zd);
            return this;
        }
        public HS_Vector subSelf(HS_Coord p)
        {
            Set(xd - p.xd, yd - p.yd, zd - p.zd);
            return this;
        }
        public HS_Vector addMulSelf(double f, HS_Coord p)
        {
            return new HS_Vector(this.xd + f * p.xd, this.yd + f * p.yd, this.zd + f * p.zd);
        }
        public HS_Vector addMul(double f, HS_Coord p)
        {

            return new HS_Vector(this.xd + f * p.xd, this.yd + f * p.yd, this.zd + f * p.zd);
        }

        public HS_Vector crossSelf(HS_Coord p)
        {
            Set(yd * p.zd - this.zd * p.yd,
                    this.zd * p.xd - this.xd * p.zd,
                    this.xd * p.yd - yd * p.xd);
            return this;
        }


        public HS_Vector div(double v)
        {
            return new HS_Vector(this.xd / v, this.yd / v, this.zd / v);
        }

        public HS_Vector negtive()
        {
            this.xd = -this.xd;
            this.yd = -this.yd;
            this.zd = -this.zd;
            return this;
        }

        public HS_Vector reverse()
        {
            return this.negtive();
        }

        public HS_Vector flip()
        {
            return this.negtive();
        }

        public HS_Vector zero()
        {
            this.xd = 0.0D;
            this.yd = 0.0D;
            this.zd = 0.0D;
            return this;
        }

        public HS_Vector add(HS_Vector v, double f)
        {
            return new HS_Vector(this.xd + f * v.xd, this.yd + f * v.yd, this.zd + f * v.zd);
        }

        public HS_Vector add(double f, HS_Vector v)
        {
            return this.add(v, f);
        }

        //public double dot(HS_Vector v)
        //{
        //    return this.xd * v.xd + this.yd * v.yd + this.zd * v.zd;
        //}
        public double dot2D(HS_Coord v)
        {
            return this.xd * v.xd + this.yd * v.yd;
        }
        public static double dot(HS_Coord v1, HS_Coord v2)
        {
            return v1.xd * v2.xd + v1.yd * v2.yd + v1.zd * v2.zd;
        }
        public double dot(HS_Coord v)
        {
            double k0 = this.xd * v.xd;
            double k1 = this.yd * v.yd;
            double k2 = this.zd * v.zd;
            double exp0 = HS_Math.getExp(k0);
            double exp1 = HS_Math.getExp(k1);
            double exp2 = HS_Math.getExp(k2);
            if (exp0 < exp1)
            {
                if (exp0 < exp2)
                {
                    return k1 + k2 + k0;
                }
                else
                {
                    return k0 + k1 + k2;
                }
            }
            else
            {
                if (exp1 < exp2)
                {
                    return k0 + k2 + k1;
                }
                else
                {
                    return k0 + k1 + k2;
                }
            }


            //return this.xd * v.xd + this.yd * v.yd + this.zd * v.zd;
        }
        public double dot(double vx, double vy, double vz)
        {
            return this.xd * vx + this.yd * vy + this.zd * vz;
        }

        public HS_Vector cross(HS_Vector v)
        {
            return new HS_Vector(this.yd * v.zd - this.zd * v.yd, this.zd * v.xd - this.xd * v.zd, this.xd * v.yd - this.yd * v.xd);
        }

        public HS_Vector cross(double vx, double vy, double vz)
        {
            return new HS_Vector(this.yd * vz - this.zd * vy, this.zd * vx - this.xd * vz, this.xd * vy - this.yd * vx);
        }

        public double cross2D(HS_Vector v)
        {
            return this.xd * v.yd - this.yd * v.xd;
        }
        public static HS_Vector cross(HS_Coord v1, HS_Coord v2)
        {
            return new HS_Vector(v1.yd * v2.zd - v1.zd * v2.yd, v1.zd * v2.xd - v1.xd * v2.zd, v1.xd * v2.yd - v1.yd * v2.xd);
        }

        public HS_Vector icross(HS_Vector v)
        {
            double xt = this.yd * v.zd - this.zd * v.yd;
            double yt = this.zd * v.xd - this.xd * v.zd;
            double zt = this.xd * v.yd - this.yd * v.xd;
            this.xd = xt;
            this.yd = yt;
            this.zd = zt;
            return this;
        }

        public HS_Vector icross(double vx, double vy, double vz)
        {
            double xt = this.yd * vz - this.zd * vy;
            double yt = this.zd * vx - this.xd * vz;
            double zt = this.xd * vy - this.yd * vx;
            this.xd = xt;
            this.yd = yt;
            this.zd = zt;
            return this;
        }

        public double len()
        {
            return Math.Sqrt(this.xd * this.xd + this.yd * this.yd + this.zd * this.zd);
        }

        public double len2()
        {
            return this.xd * this.xd + this.yd * this.yd + this.zd * this.zd;
        }

        public HS_Vector len(double l)
        {
            l /= this.len();
            this.xd *= l;
            this.yd *= l;
            this.zd *= l;
            return this;
        }

        public bool unit()
        {
            double l = this.len();
            if (l != 0.0D)
            {
                this.xd /= l;
                this.yd /= l;
                this.zd /= l;
                return true;
            }

            return false;
        }

        public HS_Vector united()
        {
            HS_Vector vn = new HS_Vector();
            double l = this.len();
            if (l != 0.0D)
            {
                vn.xd = this.xd / l;
                vn.yd = this.yd / l;
                vn.zd = this.zd / l;
            }

            return vn;
        }
        public static double getDistance3D(HS_Coord p1, HS_Coord p2)
        {
            return HS_CoordOp3D.getDistance3D(p1, p2);
        }

        public double dist(HS_Coord v)
        {
            return Math.Sqrt((this.xd - v.xd) * (this.xd - v.xd) + (this.yd - v.yd) * (this.yd - v.yd) + (this.zd - v.zd) * (this.zd - v.zd));
        }

        public double dist(double vx, double vy, double vz)
        {
            return Math.Sqrt((this.xd - vx) * (this.xd - vx) + (this.yd - vy) * (this.yd - vy) + (this.zd - vz) * (this.zd - vz));
        }

        public double dist2(HS_Vector v)
        {
            return (this.xd - v.xd) * (this.xd - v.xd) + (this.yd - v.yd) * (this.yd - v.yd) + (this.zd - v.zd) * (this.zd - v.zd);
        }

        public double dist2(double vx, double vy, double vz)
        {
            return (this.xd - vx) * (this.xd - vx) + (this.yd - vy) * (this.yd - vy) + (this.zd - vz) * (this.zd - vz);
        }

        public bool eq(HS_Vector v, double tolerance)
        {
            return this.dist2(v) <= tolerance * tolerance;
        }

        public bool eq(double vx, double vy, double vz, double tolerance)
        {
            return this.dist2(vx, vy, vz) <= tolerance * tolerance;
        }

        public bool eqX(HS_Vector v, double tolerance)
        {
            return Math.Abs(this.xd - v.xd) <= tolerance;
        }

        public bool eqY(HS_Vector v, double tolerance)
        {
            return Math.Abs(this.yd - v.yd) <= tolerance;
        }

        public bool eqZ(HS_Vector v, double tolerance)
        {
            return Math.Abs(this.zd - v.zd) <= tolerance;
        }

        public bool eqX(double vx, double tolerance)
        {
            return Math.Abs(this.xd - vx) <= tolerance;
        }

        public bool eqY(double vy, double tolerance)
        {
            return Math.Abs(this.yd - vy) <= tolerance;
        }

        public bool eqZ(double vz, double tolerance)
        {
            return Math.Abs(this.zd - vz) <= tolerance;
        }
        public static double angle(HS_Coord q, HS_Coord p)
        {
            return HS_CoordOp3D.GetAngleBetween(q.xd, q.yd, q.zd, p.xd, p.yd, p.zd);
        }
        public static double getLen2(HS_Coord v)
        {
            return HS_CoordOp3D.getSqLength3D(v.xd, v.yd, v.zd);
        }

        public double angle(HS_Vector v)
        {
            double len1 = this.len();
            if (len1 == 0.0D)
            {
                return 0.0D;
            }
            else
            {
                double len2 = v.len();
                if (len2 == 0.0D)
                {
                    return 0.0D;
                }
                else
                {
                    double Cos = this.dot(v) / (len1 * len2);
                    if (Cos > 1.0D)
                    {
                        Cos = 1.0D;
                    }
                    else if (Cos < -1.0D)
                    {
                        Cos = -1.0D;
                    }

                    return Math.Acos(Cos);
                }
            }
        }

        public double angle(double vx, double vy, double vz)
        {
            double len1 = this.len();
            if (len1 == 0.0D)
            {
                return 0.0D;
            }
            else
            {
                double len2 = Math.Sqrt(vx * vx + vy * vy + vz * vz);
                if (len2 == 0.0D)
                {
                    return 0.0D;
                }
                else
                {
                    double Cos = this.dot(vx, vy, vz) / (len1 * len2);
                    if (Cos > 1.0D)
                    {
                        Cos = 1.0D;
                    }
                    else if (Cos < -1.0D)
                    {
                        Cos = -1.0D;
                    }

                    return Math.Acos(Cos);
                }
            }
        }

        public double angle(HS_Vector v, HS_Vector axis)
        {
            double len1 = this.len();
            if (len1 == 0.0D)
            {
                return 0.0D;
            }
            else
            {
                double len2 = v.len();
                if (len2 == 0.0D)
                {
                    return 0.0D;
                }
                else
                {
                    double Cos = this.dot(v) / (len1 * len2);
                    HS_Vector cross = this.cross(v);
                    if (Cos > 1.0D)
                    {
                        Cos = 1.0D;
                    }
                    else if (Cos < -1.0D)
                    {
                        Cos = -1.0D;
                    }

                    double angle = Math.Acos(Cos);
                    return cross.dot(axis) < 0.0D ? -angle : angle;
                }
            }
        }

        public double angle(double vx, double vy, double vz, double axisX, double axisY, double axisZ)
        {
            double len1 = this.len();
            if (len1 == 0.0D)
            {
                return 0.0D;
            }
            else
            {
                double len2 = Math.Sqrt(vx * vx + vy * vy + vz * vz);
                if (len2 == 0.0D)
                {
                    return 0.0D;
                }
                else
                {
                    double Cos = this.dot(vx, vy, vz) / (len1 * len2);
                    HS_Vector cross = this.cross(vx, vy, vz);
                    if (Cos > 1.0D)
                    {
                        Cos = 1.0D;
                    }
                    else if (Cos < -1.0D)
                    {
                        Cos = -1.0D;
                    }

                    double angle = Math.Acos(Cos);
                    return cross.dot(axisX, axisY, axisZ) < 0.0D ? -angle : angle;
                }
            }
        }

        public HS_Vector rot(HS_Vector axis, double angle)
        {
            if (axis == null)
            {
                return this.rot(angle);
            }
            else
            {
                double[,] mat = new double[3, 3];
                HS_Vector ax = axis.dup().united();
                double Sin = Math.Sin(angle);
                double Cos = Math.Cos(angle);
                double iCos = 1.0D - Cos;
                mat[0, 0] = ax.xd * ax.xd * iCos + Cos;
                mat[0, 1] = ax.xd * ax.yd * iCos - ax.zd * Sin;
                mat[0, 2] = ax.xd * ax.zd * iCos + ax.yd * Sin;
                mat[1, 0] = ax.yd * ax.xd * iCos + ax.zd * Sin;
                mat[1, 1] = ax.yd * ax.yd * iCos + Cos;
                mat[1, 2] = ax.yd * ax.zd * iCos - ax.xd * Sin;
                mat[2, 0] = ax.zd * ax.xd * iCos - ax.yd * Sin;
                mat[2, 1] = ax.zd * ax.yd * iCos + ax.xd * Sin;
                mat[2, 2] = ax.zd * ax.zd * iCos + Cos;
                double xt = this.xd;
                double yt = this.yd;
                this.xd = mat[0, 0] * xt + mat[0, 1] * yt + mat[0, 2] * this.zd;
                this.yd = mat[1, 0] * xt + mat[1, 1] * yt + mat[1, 2] * this.zd;
                this.zd = mat[2, 0] * xt + mat[2, 1] * yt + mat[2, 2] * this.zd;
                return this;
            }
        }

        public HS_Vector rot(double axisX, double axisY, double axisZ, double angle)
        {
            double[,] mat = new double[3, 3];
            double len = Math.Sqrt(axisX * axisX + axisY * axisY + axisZ * axisZ);
            if (len == 0.0D)
            {
                return this;
            }
            else
            {
                axisX /= len;
                axisY /= len;
                axisZ /= len;
                double Sin = Math.Sin(angle);
                double Cos = Math.Cos(angle);
                double iCos = 1.0D - Cos;
                mat[0, 0] = axisX * axisX * iCos + Cos;
                mat[0, 1] = axisX * axisY * iCos - axisZ * Sin;
                mat[0, 2] = axisX * axisZ * iCos + axisY * Sin;
                mat[1, 0] = axisY * axisX * iCos + axisZ * Sin;
                mat[1, 1] = axisY * axisY * iCos + Cos;
                mat[1, 2] = axisY * axisZ * iCos - axisX * Sin;
                mat[2, 0] = axisZ * axisX * iCos - axisY * Sin;
                mat[2, 1] = axisZ * axisY * iCos + axisX * Sin;
                mat[2, 2] = axisZ * axisZ * iCos + Cos;
                double xt = this.xd;
                double yt = this.yd;
                this.xd = mat[0, 0] * xt + mat[0, 1] * yt + mat[0, 2] * this.zd;
                this.yd = mat[1, 0] * xt + mat[1, 1] * yt + mat[1, 2] * this.zd;
                this.zd = mat[2, 0] * xt + mat[2, 1] * yt + mat[2, 2] * this.zd;
                return this;
            }
        }

        public HS_Vector rot(double angle)
        {
            double Sin = Math.Sin(angle);
            double Cos = Math.Cos(angle);
            double xt = this.xd;
            this.xd = Cos * xt - Sin * this.yd;
            this.yd = Sin * xt + Cos * this.yd;
            return this;
        }
        public HS_Vector rotateAboutAxis(double angle, HS_Coord p,
          HS_Coord a)
        {
            HS_Vector result = new HS_Vector(this);
            HS_Transform3D raa = new HS_Transform3D();
            raa.addRotateAboutAxis(angle, p, a);
            raa.applyAsVectorSelf(result);
            return result;
        }
        public HS_Vector rot(HS_Vector center, HS_Vector axis, double angle)
        {
            return center == this ? this : this.sub(center).rot(axis, angle).add(center);
        }

        public HS_Vector rot(double centerX, double centerY, double centerZ, double axisX, double axisY, double axisZ, double angle)
        {
            return this.sub(centerX, centerY, centerZ).rot(axisX, axisY, axisZ, angle).add(centerX, centerY, centerZ);
        }

        public HS_Vector rot(HS_Vector axis, HS_Vector destDir)
        {
            return this.rot(axis, destDir.cross(axis).angle(this.cross(axis)));
        }

        public HS_Vector rot2(double angle)
        {
            return this.rot(angle);
        }

        public HS_Vector rot2(HS_Vector center, double angle)
        {
            return center == this ? this : this.sub(center).rot(angle).add(center);
        }

        public HS_Vector rot2(double centerX, double centerY, double angle)
        {
            return this.sub(centerX, centerY, 0.0D).rot(angle).add(centerX, centerY, 0.0D);
        }

        public HS_Vector rot2(HS_Vector destDir)
        {
            return this.rot(destDir.cross(zaxis).angle(this.cross(zaxis)));
        }

        public HS_Vector scale(double f)
        {
            return this.mul(f);
        }

        public HS_Vector scale(HS_Vector center, double f)
        {
            return center == this ? this : this.sub(center).scale(f).add(center);
        }

        public HS_Vector scale(double centerX, double centerY, double centerZ, double f)
        {
            return this.sub(centerX, centerY, centerZ).scale(f).add(centerX, centerY, centerZ);
        }

        public HS_Vector Ref(HS_Vector planeDir)
        {
            return this.add(planeDir.dup().mul(this.dot(planeDir) / planeDir.len2() * -2.0D));
        }


        public HS_Vector Ref(double planeX, double planeY, double planeZ)
        {
            double d = this.dot(planeX, planeY, planeZ) / (planeX * planeX + planeY * planeY + planeZ * planeZ) * -2.0D;
            this.xd += planeX * d;
            this.yd += planeY * d;
            this.zd += planeZ * d;
            return this;
        }

        public HS_Vector Ref(HS_Vector center, HS_Vector planeDir)
        {
            return center == this ? this : this.sub(center).Ref(planeDir).add(center);
        }

        public HS_Vector Ref(double centerX, double centerY, double centerZ, double planeX, double planeY, double planeZ)
        {
            return this.sub(centerX, centerY, centerZ).Ref(planeX, planeY, planeZ).add(centerX, centerY, centerZ);
        }

        public HS_Vector mirror(HS_Vector planeDir)
        {
            return this.Ref(planeDir);
        }

        public HS_Vector mirror(double planeX, double planeY, double planeZ)
        {
            return this.Ref(planeX, planeY, planeZ);
        }

        public HS_Vector mirror(HS_Vector center, HS_Vector planeDir)
        {
            return this.Ref(center, planeDir);
        }

        public HS_Vector mirror(double centerX, double centerY, double centerZ, double planeX, double planeY, double planeZ)
        {
            return this.Ref(centerX, centerY, centerZ, planeX, planeY, planeZ);
        }


        public double[] toDoubleArray()
        {
            return new double[] { this.xd, this.yd, this.zd };
        }

        public static HS_Vector randomVec(double len)
        {
            double[] cood = new double[3];

            for (int i = 0; i < cood.Length; ++i)
            {
                Random random = new Random();
                cood[i] = random.NextDouble() - 0.5D;
            }

            return (new HS_Vector(cood[0], cood[1], cood[2])).united().mul(len);
        }

        public static HS_Vector randomVec2d(double len)
        {
            double[] cood = new double[2];

            for (int i = 0; i < cood.Length; ++i)
            {
                Random random = new Random();
                cood[i] = random.NextDouble() - 0.5D;
            }

            return (new HS_Vector(cood[0], cood[1], 0.0D)).united().mul(len);
        }

        public static HS_Vector randomVec(double x0, double x1, double y0, double y1)
        {
            return randomVec(x0, x1, y0, y1, 0.0D, 0.0D);
        }

        public static HS_Vector randomVec(double x0, double x1, double y0, double y1, double z0, double z1)
        {
            Random random = new Random();

            return new HS_Vector(random.NextDouble() * (x1 - x0) + x0, random.NextDouble() * (y1 - y0) + y0, random.NextDouble() * (z1 - z0) + z0);
        }

        public static bool inSameLine(HS_Vector p1, HS_Vector p2, HS_Vector p3)
        {
            HS_Vector ptp1 = p3.dup().sub(p1).united();
            HS_Vector ptp2 = p3.dup().sub(p2).united();
            return ptp1.eq(ptp2, 0.001D) || ptp1.eq(ptp2.dup().mul(-1.0D), 0.002D);
        }

        public static bool Betw(HS_Vector p, HS_Vector p1, HS_Vector p2)
        {
            return Betw(p.xd, p1.xd, p2.xd) && Betw(p.yd, p1.yd, p2.yd) && Betw(p.zd, p1.zd, p2.zd);
        }

        private static bool Betw(double a, double b, double c)
        {
            return a >= b && a <= c || a >= c && a <= b;
        }

        public static double distToLine(HS_Vector p, HS_Vector p1, HS_Vector p2)
        {
            if (inSameLine(p, p1, p2))
            {
                return 0.0D;
            }
            else
            {
                HS_Vector direct = p2.dup().sub(p1);
                HS_Vector ptp = p.dup().sub(p1);
                direct.unit();
                return Math.Sqrt(Math.Pow(ptp.len(), 2.0D) - Math.Pow(direct.dot(ptp), 2.0D));
            }
        }

        public static double distToSegment(HS_Vector p, HS_Vector p1, HS_Vector p2)
        {
            if (inSameLine(p, p1, p2))
            {
                return Betw(p, p1, p2) ? 0.0D : Math.Min(p.dist(p1), p.dist(p2));
            }
            else
            {
                HS_Vector direct = p2.dup().sub(p1);
                HS_Vector ptp = p.dup().sub(p1);
                double t = ptp.dot(direct) / Math.Pow(direct.len(), 2.0D);
                if (t < 0.0D)
                {
                    return p1.dist(p);
                }
                else
                {
                    return t > 1.0D ? p2.dist(p) : distToLine(p, p1, p2);
                }
            }
        }

        public static HS_Vector midPt(HS_Vector p1, HS_Vector p2)
        {
            return new HS_Vector((p1.xd + p2.xd) / 2.0D, (p1.yd + p2.yd) / 2.0D, (p1.zd + p2.zd) / 2.0D);
        }
        public HS_Vector copy()
        {
            return new HS_Vector(xd, yd, zd);
        }
        //public float X
        //{
        //    get
        //    {
        //        return (float)this.xd;
        //    }
        //    set
        //    {
        //        this.xd = value;
        //    }
        //}

        //public float Y
        //{
        //    get
        //    {
        //        return (float)this.xd;
        //    }
        //    set
        //    {
        //        this.y = value;
        //    }
        //}

        //public float Z
        //{
        //    get
        //    {
        //        return (float)this.xd;
        //    }
        //    set{
        //        this.z = value;
        //    }
        //}



        public void SetX(float x)
        {
            this._xf = x;
        }

        public void SetY(float y)
        {
            this._yf = y;
        }

        public void SetZ(float z)
        {
            this._zf = z;
        }
        public void SetX(double x)
        {
            this._xd = x;
        }

        public void SetY(double y)
        {
            this._yd = y;
        }

        public void SetZ(double z)
        {
            this._zd = z;
        }



        public void Set(HS_Coord z)
        {
            this._xd = z.xd;
            this._yd = z.yd;
            this._zd = z.zd;
        }
        public void Set(double x, double y)
        {
            this._xd = x;
            this._yd = y;
            this._zd = 0;
        }
        public void Set(double x, double y, double z)
        {
            this._xd = x;
            this._yd = y;
            this._zd = z;
        }
        public int CompareTo(HS_Coord p)
        {
            int cmp = this.xd.CompareTo(p.xd);
            if (cmp != 0)
            {
                return cmp;
            }
            cmp = this.yd.CompareTo(p.yd);
            if (cmp != 0)
            {
                return cmp;
            }
            cmp = this.zd.CompareTo(p.zd);
            if (cmp != 0)
            {
                return cmp;
            }
            return 0;
        }




        public HS_Vector apply2D(HS_Transform2D T)
        {
            return T.applyAsVector2D(this);
        }


        public HS_Point applyAsPoint2D(HS_Transform2D T)
        {
            return T.applyAsPoint2D(this);
        }


        public HS_Vector applyAsVector2D(HS_Transform2D T)
        {
            return T.applyAsVector2D(this);
        }


        public HS_Vector applyAsNormal2D(HS_Transform2D T)
        {
            return T.applyAsNormal2D(this);
        }


        public HS_Vector translate2D(double px, double py)
        {
            return new HS_Vector(this.xd + px, this.yd + py);
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_CoordinateTransform2D#translate2D(HSlut.geom.HS_Coord)
         */

        public HS_Vector translate2D(HS_Coord p)
        {
            return new HS_Vector(this.xd + p.xd, this.yd + p.yd);
        }


        public HS_Vector apply2DSelf(HS_Transform2D T)
        {
            T.applyAsVector2DSelf(this);
            return this;
        }


        public HS_Vector applyAsPoint2DSelf(HS_Transform2D T)
        {
            T.applyAsPoint2DSelf(this);
            return this;
        }


        public HS_Vector applyAsVector2DSelf(HS_Transform2D T)
        {
            T.applyAsVector2DSelf(this);
            return this;
        }


        public HS_Vector applyAsNormal2DSelf(HS_Transform2D T)
        {
            T.applyAsNormal2DSelf(this);
            return this;
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_MutableCoordinateTransform3D#translate2DSelf(double,
         * double)
         */

        public HS_Vector translate2DSelf(double px, double py)
        {
            Set(xd + px, yd + py, 0);
            return this;
        }

        /*
         * (non-Javadoc)
         * @see
         * HSlut.geom.HS_MutableCoordinateTransform3D#translate2DSelf(HSlut.geom.
         * HS_Coord)
         */

        public HS_Vector translate2DSelf(HS_Coord p)
        {
            Set(xd + p.xd, yd + p.yd, 0);
            return this;
        }


        public HS_Vector apply(HS_Transform3D T)
        {
            return T.applyAsVector(this);
        }

        /*
         * (non-Javadoc)
         * @see
         * HSlut.geom.HS_CoordinateTransform#applyAsPoint(HSlut.geom.HS_Transform)
         */

        public HS_Point applyAsPoint(HS_Transform3D T)
        {
            return T.applyAsPoint(this);
        }

        /*
         * (non-Javadoc)
         * @see
         * HSlut.geom.HS_CoordinateTransform#applyAsNormal(HSlut.geom.HS_Transform)
         */

        public HS_Vector applyAsNormal(HS_Transform3D T)
        {
            return T.applyAsNormal(this);
        }

        /*
         * (non-Javadoc)
         * @see
         * HSlut.geom.HS_CoordinateTransform#applyAsVector(HSlut.geom.HS_Transform)
         */

        public HS_Vector applyAsVector(HS_Transform3D T)
        {
            return T.applyAsVector(this);
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_CoordinateTransform3D#translate(double, double)
         */

        public HS_Vector translate(double px, double py,
                 double pz)
        {
            return new HS_Vector(this.xd + px, this.yd + py, this.zd + pz);
        }

        /*
         * 
         * @see HSlut.geom.HS_CoordinateTransform3D#translate(HSlut.geom.HS_Coord)
         */

        public HS_Vector translate(HS_Coord p)
        {
            return new HS_Vector(xd + p.xd, yd + p.yd, zd + p.zd);
        }



        public HS_Vector applySelf(HS_Transform3D T)
        {
            return applyAsVectorSelf(T);
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_MutableCoordinateMath#applyAsPointSelf(HSlut.geom.
         * HS_Transform )
         */

        public HS_Vector applyAsPointSelf(HS_Transform3D T)
        {
            T.applyAsPointSelf(this);
            return this;
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_MutableCoordinateMath#applyAsVectorSelf(HSlut.geom.
         * HS_Transform )
         */

        public HS_Vector applyAsVectorSelf(HS_Transform3D T)
        {
            T.applyAsVectorSelf(this);
            return this;
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_MutableCoordinateMath#applyAsNormalSelf(HSlut.geom.
         * HS_Transform )
         */

        public HS_Vector applyAsNormalSelf(HS_Transform3D T)
        {
            T.applyAsNormalSelf(this);
            return this;
        }

        /*
         * (non-Javadoc)
         * @see HSlut.geom.HS_MutableCoordinateTransform3D#translateSelf(double,
         * double, double)
         */

        public HS_Vector translateSelf(double px, double py,
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

        public HS_Vector translateSelf(HS_Coord p)
        {
            Set(xd + p.xd, yd + p.yd, zd + p.zd);
            return this;
        }
        override
        public string ToString()
        {
            return "HS_Vector : (" + xd + " , " + yd + " , " + zd + ")";
        }

        public double getd(int i)
        {
            if (i < 0 || i > 3)
            {
                throw new IndexOutOfRangeException("Index should larger than 0 and less than 3");
            }
            switch (i)
            {

                case 0:
                    return this._xd;
                    //break;
                case 1:
                    return this._yd;
                    //break;
                case 2:
                    return this._zd;
                    //break;
            }
            return double.NaN;

        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HS_HashCode.calculateHashCode(this.xd, this.yd);
        }

        public static HS_Vector operator +(HS_Vector left, HS_Vector right)
        {
            return left.add(right);
        }
        public static HS_Vector operator +(HS_Vector left, HS_Coord right)
        {
            return left.add(right);
        }
        public static HS_Vector operator -(HS_Vector vec)
        {
            return vec.mul(-1D);
        }
        public static HS_Vector operator -(HS_Vector left, HS_Vector right)
        {
            return left.sub(right);
        }
        public static HS_Vector operator -(HS_Vector left, HS_Coord right)
        {
            return new HS_Vector(left.xd - right.xd, left.yd - right.yd, left.zd - right.zd);
        }
        //public static HS_Vector operator *(Quaternion quat, HS_Vector vec)
        //{

        //}
        public static HS_Vector operator *(HS_Vector vec, float scale)
        {
            return vec.mul(scale);
        }
        public static HS_Vector operator *(HS_Vector vec, double scale)
        {
            return vec.mul(scale);
        }
        public static HS_Vector operator *(float scale, HS_Vector vec)
        {
            return vec.mul(scale);
        }
        public static HS_Vector operator *(double scale, HS_Vector vec)
        {
            return vec.mul(scale);
        }
        public static HS_Vector operator *(HS_Vector vec, HS_Vector scale)
        {
            return new HS_Vector(vec.xd * scale.xd, vec.yd * scale.yd, vec.zd * scale.zd);
        }
        //public static HS_Vector operator *(HS_Vector vec, Matrix3 mat)
        //{

        //}
        //public static HS_Vector operator *(Matrix3 mat, HS_Vector vec);
        public static HS_Vector operator /(HS_Vector vec, float scale)
        {
            return vec.mul(1 / scale);
        }
        public static HS_Vector operator /(HS_Vector vec, double scale)
        {
            return vec.mul(1 / scale);
        }
        public static bool operator ==(HS_Vector left, HS_Vector right)
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
        public static bool operator !=(HS_Vector left, HS_Vector right)
        {

            return !(HS_Epsilon.isZero(left.xd - right.xd) && HS_Epsilon.isZero(left.yd - right.yd) && HS_Epsilon.isZero(left.zd - right.zd));
        }


    }
}
