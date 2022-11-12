using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.HsMath
{
    public class HS_Matrix44
    {
        public double m11, m12, m13, m14;

        public double m21, m22, m23, m24;

        public double m31, m32, m33, m34;

        public double m41, m42, m43, m44;

        public HS_Matrix44()
        {

        }

        public HS_Matrix44(double[][] matrix44)
        {
            m11 = matrix44[0][0];
            m12 = matrix44[0][1];
            m13 = matrix44[0][2];
            m14 = matrix44[0][3];
            m21 = matrix44[1][0];
            m22 = matrix44[1][1];
            m23 = matrix44[1][2];
            m24 = matrix44[1][3];
            m31 = matrix44[2][0];
            m32 = matrix44[2][1];
            m33 = matrix44[2][2];
            m34 = matrix44[2][3];
            m41 = matrix44[3][0];
            m42 = matrix44[3][1];
            m43 = matrix44[3][2];
            m44 = matrix44[3][3];
        }

        public HS_Matrix44(double m11,double m12,double m13,double m14, double m21, double m22, double m23, double m24, double m31, double m32, double m33, double m34, double m41, double m42, double m43, double m44)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m14 = m14;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;
            this.m24 = m24;
            this.m31 = m31;
            this.m32 = m32;
            this.m33 =m33;
            this.m34 = m34;
            this.m41 =m41;
            this.m42 = m42;
            this.m43 = m43;
            this.m44 = m44;
        }

        public void set(double[][] matrix44)
        {
            m11 = matrix44[0][0];
            m12 = matrix44[0][1];
            m13 = matrix44[0][2];
            m14 = matrix44[0][3];
            m21 = matrix44[1][0];
            m22 = matrix44[1][1];
            m23 = matrix44[1][2];
            m24 = matrix44[1][3];
            m31 = matrix44[2][0];
            m32 = matrix44[2][1];
            m33 = matrix44[2][2];
            m34 = matrix44[2][3];
            m41 = matrix44[3][0];
            m42 = matrix44[3][1];
            m43 = matrix44[3][2];
            m44 = matrix44[3][3];
        }

        public void set(float[][] matrix44)
        {
            m11 = matrix44[0][0];
            m12 = matrix44[0][1];
            m13 = matrix44[0][2];
            m14 = matrix44[0][3];
            m21 = matrix44[1][0];
            m22 = matrix44[1][1];
            m23 = matrix44[1][2];
            m24 = matrix44[1][3];
            m31 = matrix44[2][0];
            m32 = matrix44[2][1];
            m33 = matrix44[2][2];
            m34 = matrix44[2][3];
            m41 = matrix44[3][0];
            m42 = matrix44[3][1];
            m43 = matrix44[3][2];
            m44 = matrix44[3][3];
        }

        public void set(int[][] matrix44)
        {
            m11 = matrix44[0][0];
            m12 = matrix44[0][1];
            m13 = matrix44[0][2];
            m14 = matrix44[0][3];
            m21 = matrix44[1][0];
            m22 = matrix44[1][1];
            m23 = matrix44[1][2];
            m24 = matrix44[1][3];
            m31 = matrix44[2][0];
            m32 = matrix44[2][1];
            m33 = matrix44[2][2];
            m34 = matrix44[2][3];
            m41 = matrix44[3][0];
            m42 = matrix44[3][1];
            m43 = matrix44[3][2];
            m44 = matrix44[3][3];
        }

        public void set(double m11, double m12, double m13, double m14, double m21, double m22, double m23, double m24, double m31, double m32, double m33, double m34, double m41, double m42, double m43, double m44)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m14 = m14;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;
            this.m24 = m24;
            this.m31 = m31;
            this.m32 = m32;
            this.m33 = m33;
            this.m34 = m34;
            this.m41 = m41;
            this.m42 = m42;
            this.m43 = m43;
            this.m44 = m44;
        }

        public HS_Matrix44 get()
        {
            return new HS_Matrix44(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
        }

        public static HS_Matrix44 operator +(HS_Matrix44 left,HS_Matrix44 right)
        {
            return new HS_Matrix44(left.m11 + right.m11, left.m12 + right.m12, left.m13 + right.m13, left.m14 + right.m14, left.m21 + right.m21, left.m22 + right.m22, left.m23 + right.m23, left.m24 + right.m24, left.m31 + right.m31, left.m32 + right.m32, left.m33 + right.m33, left.m34 + right.m34, left.m41 + right.m41, left.m42 + right.m42, left.m43 + right.m43, left.m44 + right.m44);
        }
        public static HS_Matrix44 operator -(HS_Matrix44 left, HS_Matrix44 right)
        {
            return new HS_Matrix44(left.m11 - right.m11, left.m12 - right.m12, left.m13 - right.m13, left.m14 - right.m14, left.m21 - right.m21, left.m22 - right.m22, left.m23 - right.m23, left.m24 - right.m24, left.m31 - right.m31, left.m32 - right.m32, left.m33 - right.m33, left.m34 - right.m34, left.m41 - right.m41, left.m42 - right.m42, left.m43 - right.m43, left.m44 - right.m44);
        }

        public static HS_Matrix44 operator *(HS_Matrix44 left, double f)
        {
            return new HS_Matrix44(left.m11*f, left.m12*f, left.m13*f, left.m14*f, left.m21*f, left.m22*f, left.m23*f, left.m24*f, left.m31*f, left.m32*f, left.m33*f, left.m34*f, left.m41*f, left.m42*f, left.m43*f, left.m44*f);
        }

        public static HS_Matrix44 operator *(HS_Matrix44 left, HS_Matrix44 right)
        {
            double M11 = left.m11 * right.m11 + left.m12 * right.m21 + left.m13 * right.m31 + left.m14 * right.m41;
            double M12 = left.m11 * right.m12 + left.m12 * right.m22 + left.m13 * right.m32 + left.m14 * right.m42;
            double M13 = left.m11 * right.m13 + left.m12 * right.m23 + left.m13 * right.m33 + left.m14 * right.m43;
            double M14 = left.m11 * right.m14 + left.m12 * right.m24 + left.m13 * right.m34 + left.m14 * right.m44;

            double M21 = left.m21 * right.m11 + left.m22 * right.m21 + left.m23 * right.m31 + left.m24 * right.m41;
            double M22 = left.m21 * right.m12 + left.m22 * right.m22 + left.m23 * right.m32 + left.m24 * right.m42;
            double M23 = left.m21 * right.m13 + left.m22 * right.m23 + left.m23 * right.m33 + left.m24 * right.m43;
            double M24 = left.m21 * right.m14 + left.m22 * right.m24 + left.m23 * right.m34 + left.m24 * right.m44;

            double M31 = left.m31 * right.m11 + left.m32 * right.m21 + left.m33 * right.m31 + left.m34 * right.m41;
            double M32 = left.m31 * right.m12 + left.m32 * right.m22 + left.m33 * right.m32 + left.m34 * right.m42;
            double M33 = left.m31 * right.m13 + left.m32 * right.m23 + left.m33 * right.m33 + left.m34 * right.m43;
            double M34 = left.m31 * right.m14 + left.m32 * right.m24 + left.m33 * right.m34 + left.m34 * right.m44;

            double M41 = left.m41 * right.m11 + left.m42 * right.m21 + left.m43 * right.m31 + left.m44 * right.m41;
            double M42 = left.m41 * right.m12 + left.m42 * right.m22 + left.m43 * right.m32 + left.m44 * right.m42;
            double M43 = left.m41 * right.m13 + left.m42 * right.m23 + left.m43 * right.m33 + left.m44 * right.m43;
            double M44 = left.m41 * right.m14 + left.m42 * right.m24 + left.m43 * right.m34 + left.m44 * right.m44;


            return new HS_Matrix44(M11, M12, M13, M14, M21, M22, M23, M24, M31, M32, M33, M34, M41, M42, M43, M44);
        }


        public static HS_Matrix44 operator /(HS_Matrix44 left, double f)
        {
            double invf = HS_Epsilon.isZero(f) ? 0 : 1.0D / f;
            return left * invf;
        }

        public void addInto(HS_Matrix44 m,HS_Matrix44 result)
        {
            result = this + m;
        }
        public void subInto(HS_Matrix44 m, HS_Matrix44 result)
        {
            result = this - m;
        }

        public void multInto(double f, HS_Matrix44 result)
        {
            result = this*f;
        }


        public void multInto(HS_Matrix44 m, HS_Matrix44 result)
        {
            result = this * m;
        }


        public void divInto(double f, HS_Matrix44 result)
        {
            double invf = HS_Epsilon.isZero(f) ? 0 : 1.0D / f;
            result = this * invf;
        }
        

        public HS_Matrix44 inverse()
        {
            HS_Matrix44 result = new HS_Matrix44();
            double a = this.m11, b = this.m21, c = this.m31, d = this.m41;
            double e = this.m12, f = this.m22, g = this.m32, h = this.m42;
            double  i = this.m13, j = this.m23, k = this.m33, l = this.m43;
            double  m = this.m14, n = this.m24, o = this.m34, p = this.m44;

            double kp_lo = k * p - l * o;
            double jp_ln = j * p - l * n;
            double jo_kn = j * o - k * n;
            double ip_lm = i * p - l * m;
            double io_km = i * o - k * m;
            double in_jm = i * n - j * m;

            double a11 = +(f * kp_lo - g * jp_ln + h * jo_kn);
            double a12 = -(e * kp_lo - g * ip_lm + h * io_km);
            double a13 = +(e * jp_ln - f * ip_lm + h * in_jm);
            double a14 = -(e * jo_kn - f * io_km + g * in_jm);

            double det = a * a11 + b * a12 + c * a13 + d * a14;

            if (Math.Abs(det) < HS_Epsilon.EPSILON)
            {
                throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
            }
            double invDet = 1.0D / det;

            result.m11 = a11 * invDet;
            result.m12 = a12 * invDet;
            result.m13 = a13 * invDet;
            result.m14 = a14 * invDet;


            result.m21 = -(b * kp_lo - c * jp_ln + d * jo_kn) * invDet;
            result.m22 = +(a * kp_lo - c * ip_lm + d * io_km) * invDet;
            result.m23 = -(a * jp_ln - b * ip_lm + d * in_jm) * invDet;
            result.m24 = +(a * jo_kn - b * io_km + c * in_jm) * invDet;


            double gp_ho = g * p - h * o;
            double fp_hn = f * p - h * n;
            double fo_gn = f * o - g * n;
            double ep_hm = e * p - h * m;
            double eo_gm = e * o - g * m;
            double en_fm = e * n - f * m;

            result.m31 = +(b * gp_ho - c * fp_hn + d * fo_gn) * invDet;
            result.m32 = -(a * gp_ho - c * ep_hm + d * eo_gm) * invDet;
            result.m33 = +(a * fp_hn - b * ep_hm + d * en_fm) * invDet;
            result.m34 = -(a * fo_gn - b * eo_gm + c * en_fm) * invDet;

            double gl_hk = g * l - h * k;
            double fl_hj = f * l - h * j;
            double fk_gj = f * k - g * j;
            double el_hi = e * l - h * i;
            double ek_gi = e * k - g * i;
            double ej_fi = e * j - f * i;

            result.m41 = -(b * gl_hk - c * fl_hj + d * fk_gj) * invDet;
            result.m42 = +(a * gl_hk - c * el_hi + d * ek_gi) * invDet;
            result.m43 = -(a * fl_hj - b * el_hi + d * ej_fi) * invDet;
            result.m44 = +(a * fk_gj - b * ek_gi + c * ej_fi) * invDet;

            return result;
        }

        public void transpose()
        {
            double tmp = m12;
            m12 = m21;
            m21 = tmp;
            tmp = m13;
            m13 = m31;
            m31 = tmp;
            tmp = m14;
            m14 = m41;
            m41 = tmp;
            tmp = m23;
            m23 = m32;
            m32 = tmp;
            tmp = m24;
            m24 = m42;
            m42 = tmp;
            tmp = m34;
            m34 = m43;
            m43 = tmp;
        }

        public HS_Matrix44 getTranspose()
        {
            return new HS_Matrix44(m11, m21, m31, m41, m12, m22, m32, m42, m13, m23, m33, m43, m14, m24, m34, m44);
        }

        public void transposeInto(HS_Matrix44 result)
        {
            result.set(m11, m21, m31, m41, m12, m22, m32, m42, m13, m23, m33, m43, m14, m24, m34, m44);
        }
    }
}
