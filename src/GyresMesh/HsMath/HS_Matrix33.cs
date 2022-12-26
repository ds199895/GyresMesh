using Hsy.Geo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.HsMath
{
    public class HS_Matrix33
    {

        /** First row. */
        public double m11, m12, m13;
        /** Second row. */
        public double m21, m22, m23;
        /** Third row. */
        public double m31, m32, m33;

        /**
		 * Instantiates a new HS_Matrix33.
		 */
        public HS_Matrix33()
        {
        }

        /**
		 * Instantiates a new HS_Matrix33.
		 *
		 * @param matrix33
		 *            double[3][3] array of values
		 */
        public HS_Matrix33(double[][] matrix33)
        {
            m11 = matrix33[0][0];
            m12 = matrix33[0][1];
            m13 = matrix33[0][2];
            m21 = matrix33[1][0];
            m22 = matrix33[1][1];
            m23 = matrix33[1][2];
            m31 = matrix33[2][0];
            m32 = matrix33[2][1];
            m33 = matrix33[2][2];
        }

        /**
		 * Instantiates a new HS_Matrix33.
		 *
		 * @param m11
		 *            m11
		 * @param m12
		 *            m12
		 * @param m13
		 *            m13
		 * @param m21
		 *            m21
		 * @param m22
		 *            m22
		 * @param m23
		 *            m23
		 * @param m31
		 *            m31
		 * @param m32
		 *            m32
		 * @param m33
		 *            m33
		 */
        public HS_Matrix33(double m11, double m12, double m13, double m21, double m22,
                 double m23, double m31, double m32, double m33)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;
            this.m31 = m31;
            this.m32 = m32;
            this.m33 = m33;
        }

        /**
		 * Set values.
		 *
		 * @param matrix33
		 *            double[3][3] array of values
		 */
        public void Set(double[][] matrix33)
        {
            m11 = matrix33[0][0];
            m12 = matrix33[0][1];
            m13 = matrix33[0][2];
            m21 = matrix33[1][0];
            m22 = matrix33[1][1];
            m23 = matrix33[1][2];
            m31 = matrix33[2][0];
            m32 = matrix33[2][1];
            m33 = matrix33[2][2];
        }

        /**
		 * Set values.
		 *
		 * @param matrix33
		 *            float[3][3] array of values
		 */
        public void Set(float[][] matrix33)
        {
            m11 = matrix33[0][0];
            m12 = matrix33[0][1];
            m13 = matrix33[0][2];
            m21 = matrix33[1][0];
            m22 = matrix33[1][1];
            m23 = matrix33[1][2];
            m31 = matrix33[2][0];
            m32 = matrix33[2][1];
            m33 = matrix33[2][2];
        }

        /**
		 * Set values.
		 *
		 * @param matrix33
		 *            int[3][3] array of values
		 */
        public void Set(int[][] matrix33)
        {
            m11 = matrix33[0][0];
            m12 = matrix33[0][1];
            m13 = matrix33[0][2];
            m21 = matrix33[1][0];
            m22 = matrix33[1][1];
            m23 = matrix33[1][2];
            m31 = matrix33[2][0];
            m32 = matrix33[2][1];
            m33 = matrix33[2][2];
        }

        /**
		 * Set values.
		 *
		 * @param m11
		 *            m11
		 * @param m12
		 *            m12
		 * @param m13
		 *            m13
		 * @param m21
		 *            m21
		 * @param m22
		 *            m22
		 * @param m23
		 *            m23
		 * @param m31
		 *            m31
		 * @param m32
		 *            m32
		 * @param m33
		 *            m33
		 */
        public void Set(double m11, double m12, double m13, double m21, double m22,
                 double m23, double m31, double m32, double m33)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;
            this.m31 = m31;
            this.m32 = m32;
            this.m33 = m33;
        }

        /**
		 * Set values.
		 *
		 * @param m
		 *            matrix HS_Matrix33
		 */
        public void Set(HS_Matrix33 m)
        {
            m11 = m.m11;
            m12 = m.m12;
            m13 = m.m13;
            m21 = m.m21;
            m22 = m.m22;
            m23 = m.m23;
            m31 = m.m31;
            m32 = m.m32;
            m33 = m.m33;
        }

        /**
		 * Get copy.
		 *
		 * @return copy
		 */
        public HS_Matrix33 get()
        {
            return new HS_Matrix33(m11, m12, m13, m21, m22, m23, m31, m32, m33);
        }

        /**
		 * Get row as HS_Vector.
		 *
		 * @param i
		 *            0,1,2
		 * @return row
		 */
        public HS_Vector row(int i)
        {
            if (i == 0)
            {
                return new HS_Vector(m11, m12, m13);
            }
            if (i == 1)
            {
                return new HS_Vector(m21, m22, m23);
            }
            if (i == 2)
            {
                return new HS_Vector(m31, m32, m33);
            }
            return null;
        }

        /**
		 * Return row into provided HS_Vector.
		 *
		 * @param i
		 *            0,1,2
		 * @param result
		 *            HS_Point to store the row in
		 */
        public void rowInto(int i, HS_MutableCoord result)
        {
            if (i == 0)
            {
                result.Set(m11, m12, m13);
            }
            if (i == 1)
            {
                result.Set(m21, m22, m23);
            }
            if (i == 2)
            {
                result.Set(m31, m32, m33);
            }
            result.Set(0, 0, 0);
        }

        /**
		 * Get column as HS_Vector.
		 *
		 * @param i
		 *            0,1,2
		 * @return col
		 */
        public HS_Vector col(int i)
        {
            if (i == 0)
            {
                return new HS_Vector(m11, m21, m31);
            }
            if (i == 1)
            {
                return new HS_Vector(m12, m22, m32);
            }
            if (i == 2)
            {
                return new HS_Vector(m13, m23, m33);
            }
            return null;
        }

        /**
		 * Return col into provided HS_Vector.
		 *
		 * @param i
		 *            0,1,2
		 * @param result
		 *            HS_Point to store the col in
		 */
        public void colInto(int i, HS_MutableCoord result)
        {
            if (i == 0)
            {
                result.Set(m11, m21, m31);
            }
            if (i == 1)
            {
                result.Set(m12, m22, m32);
            }
            if (i == 2)
            {
                result.Set(m13, m23, m33);
            }
            result.Set(0, 0, 0);
        }

        /**
		 * Add matrix.
		 *
		 * @param m
		 *            matrix
		 */
        public void add(HS_Matrix33 m)
        {
            m11 += m.m11;
            m12 += m.m12;
            m13 += m.m13;
            m21 += m.m21;
            m22 += m.m22;
            m23 += m.m23;
            m31 += m.m31;
            m32 += m.m32;
            m33 += m.m33;
        }

        /**
		 * Subtract matrix.
		 *
		 * @param m
		 *            matrix
		 */
        public void sub(HS_Matrix33 m)
        {
            m11 -= m.m11;
            m12 -= m.m12;
            m13 -= m.m13;
            m21 -= m.m21;
            m22 -= m.m22;
            m23 -= m.m23;
            m31 -= m.m31;
            m32 -= m.m32;
            m33 -= m.m33;
        }

        /**
		 * Multiply with scalar.
		 *
		 * @param f
		 *            factor
		 */
        public void mul(double f)
        {
            m11 *= f;
            m12 *= f;
            m13 *= f;
            m21 *= f;
            m22 *= f;
            m23 *= f;
            m31 *= f;
            m32 *= f;
            m33 *= f;
        }

        /**
		 * Divide by scalar.
		 *
		 * @param f
		 *            factor
		 */
        public void div(double f)
        {
            double invf = HS_Epsilon.isZero(f) ? Double.NaN : 1.0 / f;
            m11 *= invf;
            m12 *= invf;
            m13 *= invf;
            m21 *= invf;
            m22 *= invf;
            m23 *= invf;
            m31 *= invf;
            m32 *= invf;
            m33 *= invf;
        }

        /**
		 * Add matrix into provided matrix.
		 *
		 * @param m
		 *            matrix
		 * @param result
		 *            result
		 */
        public void addInto(HS_Matrix33 m, HS_Matrix33 result)
        {
            result.m11 = m11 + m.m11;
            result.m12 = m12 + m.m12;
            result.m13 = m13 + m.m13;
            result.m21 = m21 + m.m21;
            result.m22 = m22 + m.m22;
            result.m23 = m23 + m.m23;
            result.m31 = m31 + m.m31;
            result.m32 = m32 + m.m32;
            result.m33 = m33 + m.m33;
        }

        /**
		 * Subtract matrix into provided matrix.
		 *
		 * @param m
		 *            matrix
		 * @param result
		 *            result
		 */
        public void subInto(HS_Matrix33 m, HS_Matrix33 result)
        {
            result.m11 = m11 - m.m11;
            result.m12 = m12 - m.m12;
            result.m13 = m13 - m.m13;
            result.m21 = m21 - m.m21;
            result.m22 = m22 - m.m22;
            result.m23 = m23 - m.m23;
            result.m31 = m31 - m.m31;
            result.m32 = m32 - m.m32;
            result.m33 = m33 - m.m33;
        }

        /**
		 * Multiply with scalar into provided matrix.
		 *
		 * @param f
		 *            factor
		 * @param result
		 *            result
		 */
        public void multInto(double f, HS_Matrix33 result)
        {
            result.m11 = f * m11;
            result.m12 = f * m12;
            result.m13 = f * m13;
            result.m21 = f * m21;
            result.m22 = f * m22;
            result.m23 = f * m23;
            result.m31 = f * m31;
            result.m32 = f * m32;
            result.m33 = f * m33;
        }

        /**
		 * Divide with scalar into provided matrix.
		 *
		 * @param f
		 *            factor
		 * @param result
		 *            result
		 */
        public void divInto(double f, HS_Matrix33 result)
        {
            double invf = HS_Epsilon.isZero(f) ? 0 : 1.0 / f;
            result.m11 = invf * m11;
            result.m12 = invf * m12;
            result.m13 = invf * m13;
            result.m21 = invf * m21;
            result.m22 = invf * m22;
            result.m23 = invf * m23;
            result.m31 = invf * m31;
            result.m32 = invf * m32;
            result.m33 = invf * m33;
        }

        /**
		 * Multiply matrices into new matrix.
		 *
		 * @param m
		 *            matrix
		 * @param n
		 *            matrix
		 * @return result
		 */
        public static HS_Matrix33 mul(HS_Matrix33 m, HS_Matrix33 n)
        {
            return new HS_Matrix33(m.m11 * n.m11 + m.m12 * n.m21 + m.m13 * n.m31, m.m11 * n.m12 + m.m12 * n.m22 + m.m13 * n.m32,
                    m.m11 * n.m13 + m.m12 * n.m23 + m.m13 * n.m33, m.m21 * n.m11 + m.m22 * n.m21 + m.m23 * n.m31,
                    m.m21 * n.m12 + m.m22 * n.m22 + m.m23 * n.m32, m.m21 * n.m13 + m.m22 * n.m23 + m.m23 * n.m33,
                    m.m31 * n.m11 + m.m32 * n.m21 + m.m33 * n.m31, m.m31 * n.m12 + m.m32 * n.m22 + m.m33 * n.m32,
                    m.m31 * n.m13 + m.m32 * n.m23 + m.m33 * n.m33);
        }

        /**
		 * Multiply matrices into provided matrix.
		 *
		 * @param m
		 *            matrix
		 * @param n
		 *            matrix
		 * @param result
		 *            result
		 */
        public static void mulInto(HS_Matrix33 m, HS_Matrix33 n, HS_Matrix33 result)
        {
            result.Set(m.m11 * n.m11 + m.m12 * n.m21 + m.m13 * n.m31, m.m11 * n.m12 + m.m12 * n.m22 + m.m13 * n.m32,
                    m.m11 * n.m13 + m.m12 * n.m23 + m.m13 * n.m33, m.m21 * n.m11 + m.m22 * n.m21 + m.m23 * n.m31,
                    m.m21 * n.m12 + m.m22 * n.m22 + m.m23 * n.m32, m.m21 * n.m13 + m.m22 * n.m23 + m.m23 * n.m33,
                    m.m31 * n.m11 + m.m32 * n.m21 + m.m33 * n.m31, m.m31 * n.m12 + m.m32 * n.m22 + m.m33 * n.m32,
                    m.m31 * n.m13 + m.m32 * n.m23 + m.m33 * n.m33);
        }

        /**
		 * Multiply with matrix into new matrix.
		 *
		 * @param n
		 *            matrix
		 * @return result
		 */
        public HS_Matrix33 mul(HS_Matrix33 n)
        {
            return new HS_Matrix33(m11 * n.m11 + m12 * n.m21 + m13 * n.m31, m11 * n.m12 + m12 * n.m22 + m13 * n.m32,
                    m11 * n.m13 + m12 * n.m23 + m13 * n.m33, m21 * n.m11 + m22 * n.m21 + m23 * n.m31,
                    m21 * n.m12 + m22 * n.m22 + m23 * n.m32, m21 * n.m13 + m22 * n.m23 + m23 * n.m33,
                    m31 * n.m11 + m32 * n.m21 + m33 * n.m31, m31 * n.m12 + m32 * n.m22 + m33 * n.m32,
                    m31 * n.m13 + m32 * n.m23 + m33 * n.m33);
        }

        /**
		 * Multiply matrix into provided matrix.
		 *
		 * @param n
		 *            matrix
		 * @param result
		 *            result
		 */
        public void multInto(HS_Matrix33 n, HS_Matrix33 result)
        {
            result.Set(m11 * n.m11 + m12 * n.m21 + m13 * n.m31, m11 * n.m12 + m12 * n.m22 + m13 * n.m32,
                    m11 * n.m13 + m12 * n.m23 + m13 * n.m33, m21 * n.m11 + m22 * n.m21 + m23 * n.m31,
                    m21 * n.m12 + m22 * n.m22 + m23 * n.m32, m21 * n.m13 + m22 * n.m23 + m23 * n.m33,
                    m31 * n.m11 + m32 * n.m21 + m33 * n.m31, m31 * n.m12 + m32 * n.m22 + m33 * n.m32,
                    m31 * n.m13 + m32 * n.m23 + m33 * n.m33);
        }

        /**
		 * Multiply matrix and vector into provided vector.
		 *
		 * @param m
		 *            matrix
		 * @param v
		 *            vector
		 * @param result
		 *            result
		 */
        public static void mulInto(HS_Matrix33 m, HS_Coord v, HS_MutableCoord result)
        {
            result.Set(v.xd * m.m11 + v.yd * m.m12 + v.zd * m.m13, v.xd * m.m21 + v.yd * m.m22 + v.zd * m.m23,
                    v.xd * m.m31 + v.yd * m.m32 + v.zd * m.m33);
        }

        /**
		 *
		 *
		 * @param v
		 * @param m
		 * @param result
		 */
        public static void mulInto(HS_Coord v, HS_Matrix33 m, HS_MutableCoord result)
        {
            result.Set(v.xd * m.m11 + v.yd * m.m21 + v.zd * m.m31, v.xd * m.m12 + v.yd * m.m22 + v.zd * m.m32,
                    v.xd * m.m13 + v.yd * m.m23 + v.zd * m.m33);
        }

        /**
		 * Multiply matrix and point into new point.
		 *
		 * @param m
		 *            matrix
		 * @param v
		 *            point
		 * @return result
		 */
        public static HS_Point mulToPoint(HS_Matrix33 m, HS_Coord v)
        {
            return new HS_Point(v.xd * m.m11 + v.yd * m.m12 + v.zd * m.m13,
                    v.xd * m.m21 + v.yd * m.m22 + v.zd * m.m23, v.xd * m.m31 + v.yd * m.m32 + v.zd * m.m33);
        }

        /**
		 * Multiply point and matrix into new point.
		 *
		 * @param v
		 *            point
		 * @param m
		 *            matrix
		 * @return result
		 */
        public static HS_Point mulToPoint(HS_Coord v, HS_Matrix33 m)
        {
            return new HS_Point(v.xd * m.m11 + v.yd * m.m21 + v.zd * m.m31,
                    v.xd * m.m12 + v.yd * m.m22 + v.zd * m.m32, v.xd * m.m13 + v.yd * m.m23 + v.zd * m.m33);
        }

        /**
		 *
		 *
		 * @param m
		 * @param v
		 * @return
		 */
        public static HS_Vector mulToVector(HS_Matrix33 m, HS_Coord v)
        {
            return new HS_Vector(v.xd * m.m11 + v.yd * m.m12 + v.zd * m.m13,
                    v.xd * m.m21 + v.yd * m.m22 + v.zd * m.m23, v.xd * m.m31 + v.yd * m.m32 + v.zd * m.m33);
        }

        /**
		 *
		 *
		 * @param v
		 * @param m
		 * @return
		 */
        public static HS_Vector mulToVector(HS_Coord v, HS_Matrix33 m)
        {
            return new HS_Vector(v.xd * m.m11 + v.yd * m.m21 + v.zd * m.m31,
                    v.xd * m.m12 + v.yd * m.m22 + v.zd * m.m32, v.xd * m.m13 + v.yd * m.m23 + v.zd * m.m33);
        }

        /**
		 * Get determinant of matrix.
		 *
		 * @return determinant
		 */
        public double det()
        {
            return m11 * (m22 * m33 - m23 * m32) + m12 * (m23 * m31 - m21 * m33) + m13 * (m21 * m32 - m22 * m31);
        }

        /**
		 * Transpose matrix.
		 */
        public void transpose()
        {
            double tmp = m12;
            m12 = m21;
            m21 = tmp;
            tmp = m13;
            m13 = m31;
            m31 = tmp;
            tmp = m23;
            m23 = m32;
            m32 = tmp;
        }

        /**
		 * Get the transpose.
		 *
		 * @return transposed matrix
		 */
        public HS_Matrix33 getTranspose()
        {
            return new HS_Matrix33(m11, m21, m31, m12, m22, m32, m13, m23, m33);
        }

        /**
		 * Put transposed matrix into provide matrix.
		 *
		 * @param result
		 *            result
		 */
        public void transposeInto(HS_Matrix33 result)
        {
            result.Set(m11, m21, m31, m12, m22, m32, m13, m23, m33);
        }

        /**
		 * Inverse matrix.
		 *
		 * @return inverse
		 */
        public HS_Matrix33 inverse()
        {
            double d = det();
            if (HS_Epsilon.isZero(d))
            {
                return null;
            }
            HS_Matrix33 I = new HS_Matrix33(m22 * m33 - m23 * m32, m13 * m32 - m12 * m33, m12 * m23 - m13 * m22,
                   m23 * m31 - m21 * m33, m11 * m33 - m13 * m31, m13 * m21 - m11 * m23, m21 * m32 - m22 * m31,
                   m12 * m31 - m11 * m32, m11 * m22 - m12 * m21);
            I.div(d);
            return I;
        }

        /**
		 * Cramer rule for solving 3 linear equations and 3 unknowns.
		 *
		 * @param a1
		 *            the a1
		 * @param b1
		 *            the b1
		 * @param c1
		 *            the c1
		 * @param d1
		 *            the d1
		 * @param a2
		 *            the a2
		 * @param b2
		 *            the b2
		 * @param c2
		 *            the c2
		 * @param d2
		 *            the d2
		 * @param a3
		 *            the a3
		 * @param b3
		 *            the b3
		 * @param c3
		 *            the c3
		 * @param d3
		 *            the d3
		 * @return the w b_ vector
		 */
        public static HS_Vector Cramer3(double a1, double b1, double c1, double d1, double a2,
                 double b2, double c2, double d2, double a3, double b3, double c3,
                 double d3)
        {
            HS_Matrix33 m = new HS_Matrix33(a1, b1, c1, a2, b2, c2, a3, b3, c3);
            double d = m.det();
            if (HS_Epsilon.isZero(d))
            {
                return null;
            }
            m.Set(d1, b1, c1, d2, b2, c2, d3, b3, c3);
            double x = m.det();
            m.Set(a1, d1, c1, a2, d2, c2, a3, d3, c3);
            double y = m.det();
            m.Set(a1, b1, d1, a2, b2, d2, a3, b3, d3);
            return new HS_Vector(x, y, m.det());
        }

        /**
		 * Symmetric schur2 subfunction of Jacobi().
		 *
		 * @param p
		 *            the p
		 * @param q
		 *            the q
		 * @param m
		 *            the m
		 * @return the double[]
		 */
        private double[] symSchur2(int p, int q, double[][] m)
        {
            double[] result = new double[2];
            if (!HS_Epsilon.isZero(HS_Math.fastAbs(m[p][q])))
            {
                double r = (m[q][q] - m[p][p]) / (2 * m[p][q]);
                double t;
                if (r >= 0)
                {
                    t = 1 / (r + Math.Sqrt(1 + r * r));
                }
                else
                {
                    t = -1 / (-r + Math.Sqrt(1 + r * r));
                }
                result[0] = 1 / Math.Sqrt(1 + t * t);
                result[1] = t * result[0];
            }
            else
            {
                result[0] = 1;
                result[1] = 0;
            }
            return result;
        }

        /**
		 * Jacobi.
		 *
		 * @return the w b_ m33
		 */
        public HS_Matrix33 Jacobi()
        {
            int i, j, n, p, q;
            double prevoff = 0;
            double[] cs = new double[2];
            double[][] Jm = new double[][] { new double[] { 0, 0, 0 }, new double[] { 0, 0, 0 }, new double[] { 0, 0, 0 } };
            HS_Matrix33 a = get();
            double[][] am = a.toArray();
            HS_Matrix33 J = new HS_Matrix33(1, 0, 0, 0, 1, 0, 0, 0, 1);
            HS_Matrix33 JT = new HS_Matrix33();
            HS_Matrix33 v = new HS_Matrix33(1, 0, 0, 0, 1, 0, 0, 0, 1);
            int MAX_ITERATIONS = 50;
            for (n = 0; n < MAX_ITERATIONS; n++)
            {
                p = 0;
                q = 1;
                for (i = 0; i < 3; i++)
                {
                    for (j = 0; j < 3; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }
                        if (HS_Math.fastAbs(am[i][j]) > HS_Math.fastAbs(am[p][q]))
                        {
                            p = i;
                            q = j;
                        }
                    }
                }
                cs = symSchur2(p, q, am);
                for (i = 0; i < 3; i++)
                {
                    Jm[i][0] = Jm[i][i] = Jm[i][2];
                    Jm[i][i] = 1;
                }
                Jm[p][p] = cs[0];
                Jm[p][q] = cs[1];
                Jm[q][p] = -cs[1];
                Jm[q][q] = cs[0];
                J.Set(Jm);
                v.mul(J);
                JT = J.getTranspose();
                JT.mul(a);
                JT.mul(J);
                a.Set(JT);
                double off = 0;
                am = a.toArray();
                for (i = 0; i < 3; i++)
                {
                    for (j = 0; j < 3; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }
                        off += am[i][j] * am[i][j];
                    }
                }
                if (n > 2 && off >= prevoff)
                {
                    return a;
                }
                prevoff = off;
            }
            return a;
        }

        /**
		 * Return matrix as array.
		 *
		 * @return double[3][3]
		 */
        public double[][] toArray()
        {
            return new double[][] { new double[] { m11, m12, m13 }, new double[] { m21, m22, m23 }, new double[] { m31, m32, m33 } };
        }

        /**
		 * Get covariance matrix of an array of HS_Coord.
		 *
		 * @param points
		 * @return covariance matrix
		 */
        public static HS_Matrix33 covarianceMatrix(HS_Coord[] points)
        {
            int n = points.Length;
            double oon = 1 / (double)n;
            HS_Point c = new HS_Point();
            HS_Point p = new HS_Point();
            double e00, e11, e22, e01, e02, e12;
            for (int i = 0; i < n; i++)
            {
                c.addSelf(points[i]);
            }
            c.mulSelf(oon);
            e00 = e11 = e22 = e01 = e02 = e12 = 0;
            for (int i = 0; i < n; i++)
            {
                p = HS_Point.sub(points[i], c);
                e00 += p.xd * p.xd;
                e11 += p.yd * p.yd;
                e22 += p.zd * p.zd;
                e01 += p.xd * p.yd;
                e02 += p.xd * p.zd;
                e12 += p.yd * p.zd;
            }
            HS_Matrix33 cov = new HS_Matrix33(e00, e01, e02, e01, e11, e12, e02, e12, e22);
            cov.mul(oon);
            return cov;
        }


        public bool equals(Object o)
        {
            if (o == null)
            {
                return false;
            }
            if (o == this)
            {
                return true;
            }
            if (!(o is HS_Matrix33))
            {
                return false;
            }
            HS_Matrix33 p = (HS_Matrix33)o;
            if (!HS_Epsilon.isEqualAbs(m11, p.m11))
            {
                return false;
            }
            if (!HS_Epsilon.isEqualAbs(m12, p.m12))
            {
                return false;
            }
            if (!HS_Epsilon.isEqualAbs(m13, p.m13))
            {
                return false;
            }
            if (!HS_Epsilon.isEqualAbs(m21, p.m21))
            {
                return false;
            }
            if (!HS_Epsilon.isEqualAbs(m22, p.m22))
            {
                return false;
            }
            if (!HS_Epsilon.isEqualAbs(m23, p.m23))
            {
                return false;
            }
            if (!HS_Epsilon.isEqualAbs(m31, p.m31))
            {
                return false;
            }
            if (!HS_Epsilon.isEqualAbs(m32, p.m32))
            {
                return false;
            }
            if (!HS_Epsilon.isEqualAbs(m33, p.m33))
            {
                return false;
            }

            return true;
        }


    }
}
