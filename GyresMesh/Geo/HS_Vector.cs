using System;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Hsy.Geo
{
    public class HS_Vector : HS_MutableCoord
    {
        public static  HS_Vector origin = new HS_Vector(0.0D, 0.0D, 0.0D);
        public static  HS_Vector xaxis = new HS_Vector(1.0D, 0.0D, 0.0D);
        public static  HS_Vector zaxis = new HS_Vector(0.0D, 1.0D, 0.0D);
        public static  HS_Vector yaxis = new HS_Vector(0.0D, 0.0D, 1.0D);
        public double x;
        public double y;
        public double z;

        public HS_Vector()
        {
            this.x = 0.0D;
            this.y = 0.0D;
            this.z = 0.0D;
        }

        public HS_Vector(HS_Coord hs_coord)
        {
            this.x = hs_coord.X();
            this.y = hs_coord.Y();
            this.z = hs_coord.Z();
        }

        public HS_Vector(HS_Coord from,HS_Coord to)
        {
            this.x = to.X() - from.X();
            this.y = to.Y() - from.Y();
            this.z = to.Z() - from.Z();
        }
        public HS_Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.z = 0.0;
        }
        public HS_Vector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public HS_Vector add(double x, double y, double z)
        {
            this.x += x;
            this.y += y;
            this.z += z;
            return this;
        }
        public HS_Vector get()
        {
            return this;
        }

        public double GetLength()
        {
            return this.dist(new HS_Vector());
        }
        public Vector3D ToVector3D()
        {
            return new Vector3D(this.X(), this.Y(), this.Z());
        }

        public Point3D ToPoint3D()
        {
            return new Point3D(this.X(), this.Y(), this.Z());
        }

        public HS_Vector dup()
        {
            return new HS_Vector(this.x, this.y, this.z);
        }
        public HS_Vector add(HS_Vector v)
        {
            this.x += v.x;
            this.y += v.y;
            this.z += v.z;
            return this;
        }
        public static HS_Vector ToHS_Vector3D(Vector3D v)
        {
            return new HS_Vector(v.X, v.Y, v.Z);
        }
        public static HS_Vector ToHS_Vector3D(Point3D p)
        {
            return new HS_Vector(p.X, p.Y, p.Z);
        }

        public HS_Vector sub(double x, double y, double z)
        {
            this.x -= x;
            this.y -= y;
            this.z -= z;
            return this;
        }

        public HS_Vector sub(HS_Vector v)
        {
            this.x -= v.x;
            this.y -= v.y;
            this.z -= v.z;
            return this;
        }

        public HS_Vector mul(double v)
        {
            this.x *= v;
            this.y *= v;
            this.z *= v;
            return this;
        }

        public HS_Vector div(double v)
        {
            this.x /= v;
            this.y /= v;
            this.z /= v;
            return this;
        }

        public HS_Vector neg()
        {
            this.x = -this.x;
            this.y = -this.y;
            this.z = -this.z;
            return this;
        }

        public HS_Vector rev()
        {
            return this.neg();
        }

        public HS_Vector flip()
        {
            return this.neg();
        }

        public HS_Vector zero()
        {
            this.x = 0.0D;
            this.y = 0.0D;
            this.z = 0.0D;
            return this;
        }

        public HS_Vector add(HS_Vector v, double f)
        {
            this.x += f * v.x;
            this.y += f * v.y;
            this.z += f * v.z;
            return this;
        }

        public HS_Vector add(double f, HS_Vector v)
        {
            return this.add(v, f);
        }

        public double dot(HS_Vector v)
        {
            return this.x * v.x + this.y * v.y + this.z * v.z;
        }

        public double dot(double vx, double vy, double vz)
        {
            return this.x * vx + this.y * vy + this.z * vz;
        }

        public HS_Vector cross(HS_Vector v)
        {
            return new HS_Vector(this.y * v.z - this.z * v.y, this.z * v.x - this.x * v.z, this.x * v.y - this.y * v.x);
        }

        public HS_Vector cross(double vx, double vy, double vz)
        {
            return new HS_Vector(this.y * vz - this.z * vy, this.z * vx - this.x * vz, this.x * vy - this.y * vx);
        }

        public HS_Vector icross(HS_Vector v)
        {
            double xt = this.y * v.z - this.z * v.y;
            double yt = this.z * v.x - this.x * v.z;
            double zt = this.x * v.y - this.y * v.x;
            this.x = xt;
            this.y = yt;
            this.z = zt;
            return this;
        }

        public HS_Vector icross(double vx, double vy, double vz)
        {
            double xt = this.y * vz - this.z * vy;
            double yt = this.z * vx - this.x * vz;
            double zt = this.x * vy - this.y * vx;
            this.x = xt;
            this.y = yt;
            this.z = zt;
            return this;
        }

        public double len()
        {
            return Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
        }

        public double len2()
        {
            return this.x * this.x + this.y * this.y + this.z * this.z;
        }

        public HS_Vector len(double l)
        {
            l /= this.len();
            this.x *= l;
            this.y *= l;
            this.z *= l;
            return this;
        }

        public HS_Vector unit()
        {
            double l = this.len();
            if (l != 0.0D)
            {
                this.x /= l;
                this.y /= l;
                this.z /= l;
            }

            return this;
        }

        public HS_Vector united()
        {
            HS_Vector vn = new HS_Vector();
            double l = this.len();
            if (l != 0.0D)
            {
                vn.x= this.x /l;
                vn.x=this.y /l;
                vn.x=this.z /l;
            }

            return vn;
        }

        public double dist(HS_Vector v)
        {
            return Math.Sqrt((this.x - v.x) * (this.x - v.x) + (this.y - v.y) * (this.y - v.y) + (this.z - v.z) * (this.z - v.z));
        }

        public double dist(double vx, double vy, double vz)
        {
            return Math.Sqrt((this.x - vx) * (this.x - vx) + (this.y - vy) * (this.y - vy) + (this.z - vz) * (this.z - vz));
        }

        public double dist2(HS_Vector v)
        {
            return (this.x - v.x) * (this.x - v.x) + (this.y - v.y) * (this.y - v.y) + (this.z - v.z) * (this.z - v.z);
        }

        public double dist2(double vx, double vy, double vz)
        {
            return (this.x - vx) * (this.x - vx) + (this.y - vy) * (this.y - vy) + (this.z - vz) * (this.z - vz);
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
            return Math.Abs(this.x - v.x) <= tolerance;
        }

        public bool eqY(HS_Vector v, double tolerance)
        {
            return Math.Abs(this.y - v.y) <= tolerance;
        }

        public bool eqZ(HS_Vector v, double tolerance)
        {
            return Math.Abs(this.z - v.z) <= tolerance;
        }

        public bool eqX(double vx, double tolerance)
        {
            return Math.Abs(this.x - vx) <= tolerance;
        }

        public bool eqY(double vy, double tolerance)
        {
            return Math.Abs(this.y - vy) <= tolerance;
        }

        public bool eqZ(double vz, double tolerance)
        {
            return Math.Abs(this.z - vz) <= tolerance;
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
                double[,] mat = new double[3,3];
                HS_Vector ax = axis.dup().unit();
                double Sin = Math.Sin(angle);
                double Cos = Math.Cos(angle);
                double iCos = 1.0D - Cos;
                mat[0,0] = ax.x * ax.x * iCos + Cos;
                mat[0,1] = ax.x * ax.y * iCos - ax.z * Sin;
                mat[0,2] = ax.x * ax.z * iCos + ax.y * Sin;
                mat[1,0] = ax.y * ax.x * iCos + ax.z * Sin;
                mat[1,1] = ax.y * ax.y * iCos + Cos;
                mat[1,2] = ax.y * ax.z * iCos - ax.x * Sin;
                mat[2,0] = ax.z * ax.x * iCos - ax.y * Sin;
                mat[2,1] = ax.z * ax.y * iCos + ax.x * Sin;
                mat[2,2] = ax.z * ax.z * iCos + Cos;
                double xt = this.x;
                double yt = this.y;
                this.x = mat[0,0] * xt + mat[0,1] * yt + mat[0,2] * this.z;
                this.y = mat[1,0] * xt + mat[1,1] * yt + mat[1,2] * this.z;
                this.z = mat[2,0] * xt + mat[2,1] * yt + mat[2,2] * this.z;
                return this;
            }
        }

        public HS_Vector rot(double axisX, double axisY, double axisZ, double angle)
        {
            double[,] mat = new double[3,3];
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
                mat[0,0] = axisX * axisX * iCos + Cos;
                mat[0,1] = axisX * axisY * iCos - axisZ * Sin;
                mat[0,2] = axisX * axisZ * iCos + axisY * Sin;
                mat[1,0] = axisY * axisX * iCos + axisZ * Sin;
                mat[1,1] = axisY * axisY * iCos + Cos;
                mat[1,2] = axisY * axisZ * iCos - axisX * Sin;
                mat[2,0] = axisZ * axisX * iCos - axisY * Sin;
                mat[2,1] = axisZ * axisY * iCos + axisX * Sin;
                mat[2,2] = axisZ * axisZ * iCos + Cos;
                double xt = this.x;
                double yt = this.y;
                this.x = mat[0,0] * xt + mat[0,1] * yt + mat[0,2] * this.z;
                this.y = mat[1,0] * xt + mat[1,1] * yt + mat[1,2] * this.z;
                this.z = mat[2,0] * xt + mat[2,1] * yt + mat[2,2] * this.z;
                return this;
            }
        }

        public HS_Vector rot(double angle)
        {
            double Sin = Math.Sin(angle);
            double Cos = Math.Cos(angle);
            double xt = this.x;
            this.x = Cos * xt - Sin * this.y;
            this.y = Sin * xt + Cos * this.y;
            return this;
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

        public HS_Vector Ref (HS_Vector planeDir) 
       {
        return this.add(planeDir.dup().mul(this.dot(planeDir) / planeDir.len2() * -2.0D));
        }


        public HS_Vector Ref (double planeX, double planeY, double planeZ) 
       {
            double d = this.dot(planeX, planeY, planeZ) / (planeX * planeX + planeY * planeY + planeZ * planeZ) * -2.0D;
            this.x += planeX* d;
            this.y += planeY* d;
            this.z += planeZ* d;
            return this;
        }

        public HS_Vector Ref (HS_Vector center, HS_Vector planeDir) {
            return center == this ? this : this.sub(center).Ref (planeDir).add(center);
        }

        public HS_Vector Ref (double centerX, double centerY, double centerZ, double planeX, double planeY, double planeZ) 
        {
            return this.sub(centerX, centerY, centerZ).Ref (planeX, planeY, planeZ).add(centerX, centerY, centerZ);
        }

        public HS_Vector mirror(HS_Vector planeDir)
        {
            return this.Ref (planeDir);
        }

        public HS_Vector mirror(double planeX, double planeY, double planeZ)
        {
            return this.Ref (planeX, planeY, planeZ);
        }

        public HS_Vector mirror(HS_Vector center, HS_Vector planeDir)
        {
            return this.Ref (center, planeDir);
        }

        public HS_Vector mirror(double centerX, double centerY, double centerZ, double planeX, double planeY, double planeZ)
        {
            return this.Ref (centerX, centerY, centerZ, planeX, planeY, planeZ);
        }


        public double[] toDoubleArray()
        {
            return new double[] { this.x, this.y, this.z };
        }

        public static HS_Vector randomVec(double len)
        {
            double[] cood = new double[3];

            for (int i = 0; i < cood.Length; ++i)
            {
                Random random = new Random();
                cood[i] =random.NextDouble() - 0.5D;
            }

            return (new HS_Vector(cood[0], cood[1], cood[2])).unit().mul(len);
        }

        public static HS_Vector randomVec2d(double len)
        {
            double[] cood = new double[2];

            for (int i = 0; i < cood.Length; ++i)
            {
                Random random = new Random();
                cood[i] = random.NextDouble() - 0.5D;
            }

            return (new HS_Vector(cood[0], cood[1], 0.0D)).unit().mul(len);
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
            HS_Vector ptp1 = p3.dup().sub(p1).unit();
            HS_Vector ptp2 = p3.dup().sub(p2).unit();
            return ptp1.eq(ptp2, 0.001D) || ptp1.eq(ptp2.dup().mul(-1.0D), 0.002D);
        }

        public static bool Betw(HS_Vector p, HS_Vector p1, HS_Vector p2)
        {
            return Betw(p.x, p1.x, p2.x) && Betw(p.y, p1.y, p2.y) && Betw(p.z, p1.z, p2.z);
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
            return new HS_Vector((p1.x + p2.x) / 2.0D, (p1.y + p2.y) / 2.0D, (p1.z + p2.z) / 2.0D);
        }

        public float X()
        {
            return (float)this.x;
        }

        public float Y()
        {
            return (float)this.y;
        }

        public float Z()
        {
            return (float)this.z;
        }


        public void SetX(float x)
        {
            this.x = x;
        }

        public void SetY(float y)
        {
            this.y = y;
        }

        public void SetZ(float z)
        {
            this.z = z;
        }

        public void Set(HS_Coord z)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(HS_Coord other)
        {
            throw new NotImplementedException();
        }

        override
        public string ToString()
        {
            return "HS_Vector : " + x + " , " + y + " , " + z;
        }
    }
}
