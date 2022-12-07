using Hsy.Core;
using Hsy.Geo;
using Hsy.HsMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hsy.Core.HS_ProgressReporter;

namespace Hsy.GyresMesh
{
    public class GE_MeshOp
    {
        private static HS_GeometryFactory gf = new HS_GeometryFactory();

        //public static HS_ProgressReporter reporter = new HS_ProgressReporter("E://log.txt");
        public static HS_ProgressReporter.HS_ProgressTracker tracker = HS_ProgressTracker.instance();
        protected GE_MeshOp()
        {

        }
        public static void splitEdge(GE_Mesh mesh, GE_Halfedge edge)
        {

            HS_Point v = gf.createMidpoint(edge.GetStart(), edge.GetEnd());
            splitEdge(mesh, edge, v);
        }


        /**
         * Split edge in two parts.
         *
         * @param mesh
         * @param edge
         *            edge to split
         * @param f
         *            fraction of first part (0..1)
         *
         * @return selection of new vertex and new edge
         */
        public static void splitEdge(GE_Mesh mesh, GE_Halfedge edge, double f)
        {
            HS_Point v = gf.createInterpolatedPoint(edge.GetStart(), edge.GetEnd(), edge.isEdge() ? f : 1.0 - f);
            //return splitEdge(mesh, edge, v);
        }

        /**
         * Insert vertex in edge.
         *
         * @param mesh
         * @param edge
         *            edge to split
         * @param x
         *            x-coordinate of new vertex
         * @param y
         *            y-coordinate of new vertex
         * @param z
         *            z-coordinate of new vertex
         */
        public static void splitEdge(GE_Mesh mesh, GE_Halfedge edge, double x, double y, double z)
        {
            splitEdge(mesh, edge, new HS_Point(x, y, z));
        }

        /**
         * Split edge in multiple parts.
         *
         * @param mesh
         * @param edge
         *            edge to split
         * @param f
         *            array of fractions (0..1)
         */
        public static void splitEdge(GE_Mesh mesh, GE_Halfedge edge, double[] f)
        {
            double[] fArray = new double[f.Length];
            Array.Copy(f, fArray, f.Length);
            Array.Sort(fArray);
            GE_Halfedge e = edge;
            GE_Halfedge he0 = edge.isEdge() ? edge : edge.Pair();
            GE_Halfedge he1 = he0.Pair();
            GE_Vertex v0 = he0.GetStart();
            GE_Vertex v1 = he1.GetStart();
            GE_Vertex v = new GE_Vertex();
            for (int i = 0; i < f.Length; i++)
            {
                double fi = fArray[i];
                if (fi > 0 && fi < 1)
                {
                    v = new GE_Vertex(gf.createInterpolatedPoint(v0, v1, fi));
                    splitEdge(mesh, e, v);
                }
            }
        }

        //something wrong........................
        /**
         * Split edge in multiple parts.
         *
         * @param mesh
         * @param edge
         *            edge to split
         * @param f
         *            array of fractions (0..1)
         */
        public static void splitEdge(GE_Mesh mesh, GE_Halfedge edge, float[] f)
        {
            float[] fArray = new float[f.Length];
            Array.Copy(f, fArray, f.Length);
            Array.Sort(fArray);
            GE_Halfedge e = edge;
            GE_Halfedge he0 = edge.isEdge() ? edge : edge.Pair();
            GE_Halfedge he1 = he0.Pair();
            GE_Vertex v0 = he0.GetStart();
            GE_Vertex v1 = he1.GetStart();
            GE_Vertex v = new GE_Vertex();
            for (int i = 0; i < f.Length; i++)
            {
                double fi = fArray[i];
                if (fi > 0 && fi < 1)
                {
                    v = new GE_Vertex(gf.createInterpolatedPoint(v0, v1, fi));
                    splitEdge(mesh, e, v);
                }
            }
        }

        /**
         * Insert vertex in edge.
         *
         * @param mesh
         * @param edge
         *            edge to split
         * @param v
         *            position of new vertex
         *
         * @return selection of new vertex and new edge
         */
        public static void splitEdge(GE_Mesh mesh, GE_Halfedge edge, HS_Coord v)
        {
            // void out = void.getSelection(mesh);
            GE_Halfedge he0 = edge.isEdge() ? edge : edge.Pair();
            GE_Halfedge he1 = he0.Pair();
            GE_Vertex vNew = new GE_Vertex(v);
            GE_Halfedge he0new = new GE_Halfedge();
            GE_Halfedge he1new = new GE_Halfedge();
            GE_Halfedge he0n = he0.GetNextInFace();
            GE_Halfedge he1n = he1.GetNextInFace();
            double d0 = he0.GetStart().GetPosition().dist(v);
            double d1 = he1.GetStart().GetPosition().dist(v);
            double f0 = d1 / (d0 + d1);
            double f1 = d0 / (d0 + d1);
            mesh.SetVertex(he0new, vNew);
            mesh.SetVertex(he1new, vNew);
            mesh.SetHalfedge(vNew, he0new);
            mesh.SetNext(he0new, he0n);
            he0new.Clone(he0);
            mesh.SetNext(he1new, he1n);
            he1new.Clone(he1);
            //if (he0.hasUVW() && he0n.hasUVW())
            //{
            //    he0new.setUVW(
            //            new GE_TextureCoordinate(f0, he0.getUVW(), he0n.getUVW()));
            //}
            //if (he1.hasUVW() && he1n.hasUVW())
            //{
            //    he1new.setUVW(
            //            new GE_TextureCoordinate(f1, he1.getUVW(), he1n.getUVW()));
            //}
            mesh.SetNext(he0, he0new);
            mesh.SetNext(he1, he1new);
            mesh.SetPair(he0, he1new);
            mesh.SetPair(he0new, he1);
            if (he0.GetFace() != null)
            {
                mesh.SetFace(he0new, he0.GetFace());
            }
            if (he1.GetFace() != null)
            {
                mesh.SetFace(he1new, he1.GetFace());
            }
            vNew.SetInternalLabel(1);
            mesh.addDerivedElement(vNew, edge);
            mesh.addDerivedElement(he0new, edge);
            mesh.addDerivedElement(he1new, edge);
            //out.addEdge(he0new.isEdge() ? he0new : he1new);
            //out.addEdge(he0.isEdge() ? he0 : he1);
            //out.add(vNew);
            //return out;
        }

        public static void splitEdge(GE_Mesh mesh, GE_Halfedge edge, GE_Vertex v)
        {
            // void out = void.getSelection(mesh);
            GE_Halfedge he0 = edge.isEdge() ? edge : edge.Pair();
            GE_Halfedge he1 = he0.Pair();
            GE_Vertex vNew = v;
            GE_Halfedge he0new = new GE_Halfedge();
            GE_Halfedge he1new = new GE_Halfedge();
            GE_Halfedge he0n = he0.GetNextInFace();
            GE_Halfedge he1n = he1.GetNextInFace();
            double d0 = he0.GetStart().GetPosition().dist(v);
            double d1 = he1.GetStart().GetPosition().dist(v);
            double f0 = d1 / (d0 + d1);
            double f1 = d0 / (d0 + d1);
            mesh.SetVertex(he0new, vNew);
            mesh.SetVertex(he1new, vNew);
            mesh.SetHalfedge(vNew, he0new);
            mesh.SetNext(he0new, he0n);
            he0new.Clone(he0);
            mesh.SetNext(he1new, he1n);
            he1new.Clone(he1);
            //if (he0.hasUVW() && he0n.hasUVW())
            //{
            //    he0new.setUVW(
            //            new GE_TextureCoordinate(f0, he0.getUVW(), he0n.getUVW()));
            //}
            //if (he1.hasUVW() && he1n.hasUVW())
            //{
            //    he1new.setUVW(
            //            new GE_TextureCoordinate(f1, he1.getUVW(), he1n.getUVW()));
            //}
            mesh.SetNext(he0, he0new);
            mesh.SetNext(he1, he1new);
            mesh.SetPair(he0, he1new);
            mesh.SetPair(he0new, he1);
            if (he0.GetFace() != null)
            {
                mesh.SetFace(he0new, he0.GetFace());
            }
            if (he1.GetFace() != null)
            {
                mesh.SetFace(he1new, he1.GetFace());
            }
            vNew.SetInternalLabel(1);
            mesh.addDerivedElement(vNew, edge);
            mesh.addDerivedElement(he0new, edge);
            mesh.addDerivedElement(he1new, edge);
            //out.addEdge(he0new.isEdge() ? he0new : he1new);
            //out.addEdge(he0.isEdge() ? he0 : he1);
            //out.add(vNew);
            //          return out;
        }

        public static void splitFace(GE_Mesh mesh, GE_Face face,
             GE_Vertex vi, GE_Vertex vj)
        {
            //GE_Selection out = GE_Selection.getSelection(mesh);
            GE_Halfedge hei = vi.GetHalfedge(face);
            GE_Halfedge hej = vj.GetHalfedge(face);
            //GE_TextureCoordinate ti = hei.hasUVW() ? hei.getUVW() : null;
            //GE_TextureCoordinate tj = hej.hasUVW() ? hej.getUVW() : null;
            double d = vi.GetPosition().getDistance(vj);
            bool degenerate = false;
            if (HS_Epsilon.isZero(d))
            {// happens when a collinear (part of a) face
             // is cut. Do not add a new edge connecting
             // these two points,rather collapse them into
             // each other and remove two-edge faces
                degenerate = true;
            }
            if (hei.GetNextInFace() != hej || hei.GetPrevInFace() != hej)
            {
                GE_Halfedge heiPrev;
                GE_Halfedge hejPrev;
                GE_Face faceNew;
                if (!degenerate)
                {
                    GE_Halfedge he0new;
                    GE_Halfedge he1new;
                    heiPrev = hei.GetPrevInFace();
                    hejPrev = hej.GetPrevInFace();
                    he0new = new GE_Halfedge();
                    he1new = new GE_Halfedge();
                    mesh.SetVertex(he0new, vj);
                    //if (tj != null)
                    //{
                    //    he0new.SetUVW(tj);
                    //}
                    mesh.SetVertex(he1new, vi);
                    //if (ti != null)
                    //{
                    //    he1new.setUVW(ti);
                    //}
                    mesh.SetNext(he0new, hei);
                    mesh.SetNext(he1new, hej);
                    mesh.SetNext(heiPrev, he1new);
                    mesh.SetNext(hejPrev, he0new);
                    mesh.SetPair(he0new, he1new);
                    he0new.SetInternalLabel(1);
                    he1new.SetInternalLabel(1);
                    mesh.SetFace(he0new, face);
                    faceNew = new GE_Face();
                    mesh.SetHalfedge(face, hei);
                    mesh.SetHalfedge(faceNew, hej);
                    faceNew.Clone(face);
                    assignFaceToLoop(mesh, faceNew, hej);
                    mesh.addDerivedElement(he0new, face);
                    mesh.addDerivedElement(he1new, face);
                    mesh.addDerivedElement(faceNew, face);
				//out.addEdge(he0new.isEdge() ? he0new : he1new);
				//out.add(faceNew);
    //                return out;
                }
                else
                {
                    heiPrev = hei.GetPrevInFace();
                    hejPrev = hej.GetPrevInFace();
                    foreach(GE_Halfedge hejs in vj.GetHalfedgeStar())
                    {
                        mesh.SetVertex(hejs, vi);
                    }
                    mesh.SetNext(heiPrev, hej);
                    mesh.SetNext(hejPrev, hei);
                    faceNew = new GE_Face();
                    mesh.SetHalfedge(face, hei);
                    mesh.SetHalfedge(faceNew, hej);
                    faceNew.Clone(face);
                    assignFaceToLoop(mesh, faceNew, hej);
                    mesh.addDerivedElement(faceNew, face);
                    mesh.remove(vj);
				//out.add(faceNew);
                    if (face.GetFaceDegree() == 2)
                    {
                        GET_Fixer.deleteTwoEdgeFace(mesh, face);
                    }
                    if (faceNew.GetFaceDegree() == 2)
                    {
                        GET_Fixer.deleteTwoEdgeFace(mesh, faceNew);
					//out.remove(faceNew);
                    }
                    //return out;
                }
            }
            //return null;
        }
        public static void assignFaceToLoop(GE_Mesh mesh,GE_Face face, GE_Halfedge halfedge)
        {
            GE_Halfedge he = halfedge;
            do
            {
                mesh.SetFace(he, face);
                he = he.GetNextInFace();
            } while (he != halfedge);
        }
        public static HS_AABB getAABB(GE_Mesh mesh)
        {
            double[] result = getLimits(mesh);
            HS_Point min = gf.createPoint(result[0], result[1], result[2]);
            HS_Point max = gf.createPoint(result[3], result[4], result[5]);
            return new HS_AABB(min, max);
        }
        public static double[] getLimits(GE_Mesh mesh)
        {
            double[] result = new double[6];

            for (int i = 0; i < 3; i++)
            {
                result[i] = double.PositiveInfinity;
            }
            for (int i = 3; i < 6; i++)
            {
                result[i] = double.NegativeInfinity;
            }

            GE_Vertex v;
            for (int i = 0; i < mesh.GetNumberOfVertices(); i++)
            {
                v = mesh.getVertexWithIndex(i);
                result[0] = Math.Min(result[0], v.xd);
                result[1] = Math.Min(result[1], v.yd);
                result[2] = Math.Min(result[2], v.zd);
                result[3] = Math.Max(result[3], v.xd);
                result[4] = Math.Max(result[4], v.yd);
                result[5] = Math.Max(result[5], v.zd);
            }
            return result;
        }
        public static HS_Coord getHalfedgeTangent(GE_Halfedge he)
        {
            if (he.Pair() != null && he.GetStart() != null && he.Pair().GetStart() != null)
            {
                HS_Vector v = HS_Vector.sub(he.Pair().GetStart(), he.GetStart());
                v.unit();
                return v;
            }
            return null;
        }
        public static HS_Coord getEdgeCenter(GE_Halfedge he)
        {
            return he.GetNextInFace() != null && he.GetStart() != null && he.GetNextInFace().GetStart() != null ? gf.createMidpoint(he.GetNextInFace().GetStart(), he.GetStart()) : null;
        }
        public static HS_Coord getFaceCenter(GE_Face f)
        {
            if (f.GetHalfedge() == null)
            {
                return null;
            }
            else
            {
                GE_Halfedge he = f.GetHalfedge();
                HS_Point center = new HS_Point();
                int c = 0;
                do
                {
                    center = (HS_Point)(center + he.GetStart());
                    c++;
                    he = he.GetNextInFace();
                } while (he != f.GetHalfedge());
                center = (HS_Point)(center / c);
                return center;


            }
        }

        public static HS_Coord getFaceNormal(GE_Face face)
        {
            GE_Halfedge he = face.GetHalfedge();
            if (he == null)
            {
                return null;
            }
            else
            {
                HS_Vector _normal = new HS_Vector();
                do
                {
                    GE_Vertex p0 = he.GetStart();
                    GE_Vertex p1 = he.GetNextInFace().GetStart();
                    _normal += new HS_Vector((p0.yd - p1.yd) * (p0.zd + p1.zd), (p0.zd - p1.zd) * (p0.xd + p1.xd), (p0.xd - p1.xd) * (p0.yd + p1.yd));
                    he = he.GetNextInFace();
                } while (he != face.GetHalfedge());
                _normal.united();
                return _normal;
            }
        }
        public static HS_Coord getHalfedgeCenter(GE_Halfedge he)
        {
            if (he.GetNextInFace() != null && he.GetStart() != null
                    && he.GetNextInFace().GetStart()!= null)
            {
                return gf.createMidpoint(he.GetNextInFace().GetStart(),
                        he.GetStart());
            }
            return null;
        }
        public static HS_Coord getEdgeNormal(GE_Halfedge he)
        {
            if (he.Pair() == null)
            {
                return null;
            }
            GE_Halfedge he1, he2;
            if (he.isEdge())
            {
                he1 = he;
                he2 = he.Pair();
            }
            else
            {
                he1 = he.Pair();
                he2 = he;
            }
            if (he1.GetFace() == null && he2.GetFace() == null)
            {
                return null;
            }
            HS_Coord n1 = he1.GetFace() != null
                    ? GE_MeshOp.getFaceNormal(he1.GetFace())
                    : new HS_Vector(0, 0, 0);
            HS_Coord n2 = he2.GetFace() != null
                    ? GE_MeshOp.getFaceNormal(he2.GetFace())
                    : new HS_Vector(0, 0, 0);
            HS_Vector n = new HS_Vector(n1.xd + n2.xd, n1.yd + n2.yd,
                    n1.zd + n2.zd);
            n.unit();
            return n;
        }
        public static double getEdgeDihedralAngle(GE_Halfedge he)
        {
            if (he.Pair() == null)
            {
                return 0.0;
            }
            GE_Halfedge he1, he2;
            if (he.isEdge())
            {
                he1 = he;
                he2 = he.Pair();
            }
            else
            {
                he1 = he.Pair();
                he2 = he;
            }
            if (he1.GetFace() == null || he2.GetFace() == null)
            {
                return 0.0;
            }
            else
            {
                HS_Coord n1 = GE_MeshOp.getFaceNormal(he1.GetFace());
                HS_Coord n2 = GE_MeshOp.getFaceNormal(he2.GetFace());
                return HS_GeometryOp3D.getDihedralAngleNorm(n1, n2);
            }
        }



        public static bool flipEdge(GE_Mesh mesh, GE_Halfedge he)
        {
            // boundary edge
            if (he.GetFace() == null)
            {
                return false;
            }
            // not a triangle
            if (he.GetFace().GetFaceDegree() != 3)
            {
                return false;
            }
            // unpaired edge
            if (he.Pair() == null)
            {
                return false;
            }
            // boundary edge
            if (he.Pair().GetFace() == null)
            {
                return false;
            }
            // not a triangle
            if (he.Pair().GetFace().GetFaceDegree() != 3)
            {
                return false;
            }
            // not planar
            if (Math.PI - getEdgeDihedralAngle(he) > HS_Epsilon.EPSILON)
            {
                return false;
            }
            // flip would result in overlapping triangles, this detected by
            // comparing the areas of the two triangles before and after.
            HS_Plane P = new HS_Plane(getHalfedgeCenter(he), getEdgeNormal(he));
            HS_Coord a = HS_GeometryOp3D.projectOnPlane(he.GetStart(), P);
            HS_Coord b = HS_GeometryOp3D
                    .projectOnPlane(he.GetNextInFace().GetStart(), P);
            HS_Coord c = HS_GeometryOp3D.projectOnPlane(
                    he.GetNextInFace().GetNextInFace().GetStart(), P);
            HS_Coord d = HS_GeometryOp3D.projectOnPlane(
                    he.Pair().GetNextInFace().GetNextInFace().GetStart(), P);
            double Ai = HS_GeometryOp3D.getArea(a, b, c);
            Ai += HS_GeometryOp3D.getArea(a, d, b);
            double Af = HS_GeometryOp3D.getArea(a, d, c);
            Af += HS_GeometryOp3D.getArea(c, d, b);
            double ratio = Ai / Af;
            if (ratio > 1.000001 || ratio < 0.99999)
            {
                return false;
            }
            // get the 3 edges of triangle t1 and t2, he1t1 and he1t2 is the edge to
            // be flipped
            GE_Halfedge he1t1 = he;
            GE_Halfedge he1t2 = he.Pair();
            GE_Halfedge he2t1 = he1t1.GetNextInFace();
            GE_Halfedge he2t2 = he1t2.GetNextInFace();
            GE_Halfedge he3t1 = he2t1.GetNextInFace();
            GE_Halfedge he3t2 = he2t2.GetNextInFace();
            GE_Face t1 = he1t1.GetFace();
            GE_Face t2 = he1t2.GetFace();
            // Fix vertex assignment
            // First make sure the original vertices get assigned another halfedge
            mesh.SetHalfedge(he1t1.GetStart(), he2t2);
            mesh.SetHalfedge(he1t2.GetStart(), he2t1);
            // Now assign the new vertices to the flipped edges
            mesh.SetVertex(he1t1, he3t1.GetStart());
            mesh.SetVertex(he1t2, he3t2.GetStart());
            // Reconstruct triangle t1
            mesh.SetNext(he2t1, he1t1);
            mesh.SetNext(he1t1, he3t2);
            mesh.SetNext(he3t2, he2t1);
            mesh.SetFace(he3t2, t1);
            mesh.SetHalfedge(t1, he1t1);
            // reconstruct triangle t2
            mesh.SetNext(he2t2, he1t2);
            mesh.SetNext(he1t2, he3t1);
            mesh.SetNext(he3t1, he2t2);
            mesh.SetFace(he3t1, t2);
            mesh.SetHalfedge(t2, he1t2);
            return true;
        }

        //my version
        public static void FlipEdge(GE_Mesh mesh, GE_Halfedge h0)
        {
            GE_Halfedge h1 = h0.GetNextInFace();
            GE_Halfedge h2 = h1.GetNextInFace();
            GE_Halfedge h3 = h0.Pair();
            GE_Halfedge h4 = h3.GetNextInFace();
            GE_Halfedge h5 = h4.GetNextInFace();
            GE_Halfedge h6 = h1.Pair();
            GE_Halfedge h7 = h2.Pair();
            GE_Halfedge h8 = h4.Pair();
            GE_Halfedge h9 = h5.Pair();

            GE_Vertex v0 = h0.GetStart();
            GE_Vertex v1 = h3.GetStart();
            GE_Vertex v2 = h8.GetStart();
            GE_Vertex v3 = h6.GetStart();

            GE_Face f0 = h0.GetFace();
            GE_Face f1 = h3.GetFace();


            //重新设置翻转半边起点
            mesh.SetVertex(h0, v2);
            mesh.SetVertex(h3, v3);

            mesh.SetVertex(h5, v2);
            mesh.SetVertex(h4, v0);
            mesh.SetVertex(h2, v3);
            mesh.SetVertex(h1, v1);

            mesh.SetVertex(h6, v3);
            mesh.SetVertex(h9, v1);
            mesh.SetVertex(h8, v2);
            mesh.SetVertex(h7, v0);


            //重设半边连接顺序关系
            mesh.SetNext(h0, h2);
            mesh.SetNext(h2, h4);
            mesh.SetNext(h4, h0);

            mesh.SetNext(h1, h3);
            mesh.SetNext(h3, h5);
            mesh.SetNext(h5, h1);
            //重新设置半边之间pair关系
            mesh.SetPair(h0, h3);
            mesh.SetPair(h1, h6);
            mesh.SetPair(h5, h9);
            mesh.SetPair(h2, h7);
            mesh.SetPair(h4, h8);
            //重设半边与面的关系
            mesh.SetFace(h4, f0);
            mesh.SetFace(h0, f0);
            mesh.SetFace(h2, f0);

            mesh.SetFace(h1, f1);
            mesh.SetFace(h5, f1);
            mesh.SetFace(h3, f1);
            //重设半边索引到的面
            mesh.SetHalfedge(f0, h0);
            mesh.SetHalfedge(f1, h3);


            mesh.SetHalfedge(v0, h7);
            mesh.SetHalfedge(v1, h9);
            mesh.SetHalfedge(v2, h8);
            mesh.SetHalfedge(v3, h6);
        }
        public static GE_Mesh ReverseFaces(GE_Mesh mesh)
        {
            mesh.clearUsedElements();
            GE_Halfedge[] prevHe = new GE_Halfedge[mesh.GetNumberOfHalfedges()];
            int i = 0;
            GE_HalfedgeEnumerator heEtr = mesh.heEtr();
            GE_Halfedge he;
            while (heEtr.MoveNext())
            {
                he = (GE_Halfedge)heEtr.Current;
                prevHe[i] = he.GetPrevInFace();
                i++;

            }
            i = 0;
            heEtr = mesh.heEtr();
            while (heEtr.MoveNext())
            {
                he = (GE_Halfedge)heEtr.Current;
                mesh.SetNext(he, prevHe[i]);
                i++;
            }

            GE_EdgeEnumerator eEtr = mesh.eEtr();
            while (eEtr.MoveNext())
            {
                GE_Halfedge he1 = eEtr.Current;
                GE_Halfedge he2 = he1.Pair();
                GE_Vertex tmp = he1.GetStart();
                mesh.SetVertex(he1, he2.GetStart());
                mesh.SetVertex(he2, tmp);
                mesh.SetHalfedge(he1.GetStart(), he1);
                mesh.SetHalfedge(he2.GetStart(), he2);
            }
            return mesh;
        }
        public static void cycleHalfedges(GE_Mesh mesh, List<GE_Halfedge> halfedges)
        {
            orderHalfedges(mesh, halfedges, true);
        }
        public static void cycleHalfedgesReverse(GE_Mesh mesh, List<GE_Halfedge> halfedges)
        {
            orderHalfedgesReverse(mesh, halfedges, true);
        }


        public static void orderHalfedges(GE_Mesh mesh, List<GE_Halfedge> halfedges, bool loop)
        {
            GE_Halfedge he;
            int n = halfedges.Count;
            if (n > 0)
            {
                for (int j = 0; j < n - 1; j++)
                {
                    he = halfedges[j];
                    mesh.SetNext(he, halfedges[j + 1]);
                }
                if (loop)
                {
                    he = halfedges[n - 1];
                    mesh.SetNext(he, halfedges[0]);
                }
            }
        }

        public static void orderHalfedgesReverse(GE_Mesh mesh, List<GE_Halfedge> halfedges, bool loop)
        {
            GE_Halfedge he;
            int n = halfedges.Count;
            if (n > 0)
            {
                if (loop)
                {
                    he = halfedges[0];
                    mesh.SetNext(he, halfedges[n - 1]);
                }
                for (int j = 1; j < n; j++)
                {
                    he = halfedges[j];
                    mesh.SetNext(he, halfedges[j - 1]);
                }
            }
        }

        public static void capHalfedges(GE_Mesh mesh)
        {
            //tracker.setStartStatus("GE_MeshOp", "Capping unpaired halfedges.");


            List<GE_Halfedge> unpairedHalfedges = mesh.getUnpairedHalfedges();
            int nuh = unpairedHalfedges.Count;
            GE_Halfedge[] newHalfedges = new GE_Halfedge[nuh];
            GE_Halfedge he1, he2;
            //HS_ProgressCounter counter = new HS_ProgressCounter(nuh, 10);
            //tracker.setCounterStatus("GE_MeshOp", "Capping unpaired halfedges.", counter);
            Dictionary<long, GE_Halfedge> starts = new Dictionary<long, GE_Halfedge>();
            for (int i = 0; i < nuh; i++)
            {
                he1 = unpairedHalfedges[i];
                he2 = new GE_Halfedge();
                mesh.SetVertex(he2, he1.GetNextInFace().GetStart());
                mesh.SetPair(he1, he2);
                newHalfedges[i] = he2;
                starts.Add(he2.GetStart().GetKey(), he2);
                //mesh.addDerivedElement(he2);
                //counter.increment();
            }


            //counter = new HS_ProgressCounter(nuh, 10);
            //tracker.setCounterStatus("GE_MeshOp", "Cycling new halfedges.", counter);

            for (int i = 0; i < nuh; i++)
            {
                he1 = newHalfedges[i];
                if (he1.GetNextInFace() == null)
                {
                    GE_Halfedge nxt;
                    starts.TryGetValue(he1.GetEnd().GetKey(), out nxt);
                    if (!nxt.isUsed())
                    {
                        mesh.SetNext(he1, nxt);
                        nxt.SetUsed();
                    }

                    //for (int j = 0; j < nuh; j++)
                    //{
                    //    he2 = newHalfedges[j];
                    //    if (!he2.isUsed())
                    //    {
                    //        if (he2.GetStart() == he1.Pair().GetStart())
                    //        {
                    //            mesh.SetNext(he1, he2);
                    //            he2.SetUsed();
                    //            break;
                    //        }
                    //    }
                    //}
                }

                //counter.increment();
            }
            //tracker.setStopStatus("GE_MeshOp", "Processed unpaired halfedges.");
        }
        public static void pairHalfedges(GE_Mesh mesh)
        {
            pairHalfedgesOnePass(mesh);
        }
        public static List<GE_Halfedge> pairHalfedgesOnePass(GE_Mesh mesh)
        {
            //tracker.setStartStatus("GE_MeshOp", "Pairing halfedges.");

            Dictionary<long, VertexInfo> vertexLists = new Dictionary<long, VertexInfo>();
            List<GE_Halfedge> unpairedHalfedges = mesh.getUnpairedHalfedges();
            //GE_Vertex v;
            VertexInfo vi;
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = 1;
            //HS_ProgressCounter counter = new HS_ProgressCounter(unpairedHalfedges.Count, 10);
            //tracker.setCounterStatus("GE_MeshOp", "Classifying unpaired halfedges.", counter);

            var var7 = unpairedHalfedges.GetEnumerator();
            GE_Halfedge he;
            System.Threading.Tasks.Parallel.ForEach(unpairedHalfedges, options, he =>
             {
                 vi = null;
                 //he = (GE_Halfedge)var7.Current;
                 GE_Vertex v = he.GetStart();

                 //if (!vertexLists.Keys.Contains(v.GetKey()))
                 //{

                 //}
                 //else
                 //{
                 //    vi = vertexLists[v.GetKey()];
                 //}
                 bool get = vertexLists.TryGetValue(v.GetKey(), out vi);
                 if (!get)
                 {
                     vi = new VertexInfo();
                     vertexLists.Add(v.GetKey(), vi);
                 }

                 vi.Out.Add(he);
                 v = he.GetNextInFace().GetStart();
                 //if (!vertexLists.Keys.Contains(v.GetKey()))
                 //{
                 //    vi = new VertexInfo();
                 //    vertexLists.Add(v.GetKey(), vi);
                 //}
                 //else
                 //{
                 //    vi = vertexLists[v.GetKey()];
                 //}
                 get = vertexLists.TryGetValue(v.GetKey(), out vi);
                 if (!get)
                 {
                     vi = new VertexInfo();
                     vertexLists.Add(v.GetKey(), vi);
                 }

                 vi.In.Add(he);
                 //counter.increment();
             }
            );
            //while (var7.MoveNext())
            //{
            //    vi = new VertexInfo();
            //    he = (GE_Halfedge)var7.Current;
            //    GE_Vertex v = he.GetStart();
            //    //try
            //    //{
            //    //    vi = vertexLists[v.GetKey()];
            //    //}
            //    //catch(Exception e)
            //    //{

            //    //}

            //    //Console.WriteLine(vi);

            //}
            GE_Halfedge he1;

            GE_Halfedge he2;
            //counter = new HS_ProgressCounter(vertexLists.Count, 10);
            //tracker.setCounterStatus("GE_MeshOp", "Pairing unpaired halfedges per vertex.", counter);
            var vitr = vertexLists.GetEnumerator();
            VertexInfo vInfo;
            List<GE_Halfedge> mismatchedHalfedges = new List<GE_Halfedge>();
            foreach (var vin in vertexLists)
            {
                vInfo = vin.Value;

                for (int i = 0; i < vInfo.Out.Count; i++)
                {
                    he1 = vInfo.Out[i];
                    if (he1.Pair() == null)
                    {
                        for (int j = 0; j < vInfo.In.Count; j++)
                        {
                            he2 = vInfo.In[j];
                            if (he2.Pair() == null)
                            {
                                if (he1.GetStart() == he2.GetNextInFace().GetStart() && he2.GetStart() == he1.GetNextInFace().GetStart())
                                {
                                    mesh.SetPair(he1, he2);
                                    break;
                                }
                            }
                        }
                        for (int j = 0; j < vInfo.Out.Count; j++)
                        {
                            he2 = vInfo.Out[j];
                            if (he2 != he1 && he2.Pair() == null)
                            {
                                if (he1.GetNextInFace().GetStart() == he2.GetNextInFace().GetStart())
                                {
                                    mismatchedHalfedges.Add(he1);
                                    mismatchedHalfedges.Add(he2);
                                    break;
                                }
                            }
                        }
                    }
                }
                //counter.increment();
            }
            //foreach (var vin in vertexLists)
            //{
            //    vin.Value.Dispose();
            //}
            //while (vitr.MoveNext())
            //{
            //    vInfo = vitr.Current.Value;

            //    //System.Threading.Tasks.Parallel.For(0,vInfo.Out.Count, i=> {
            //    //    he1 = vInfo.Out[i];
            //    //    if (he1.Pair() == null)
            //    //    {
            //    //        for (int j = 0; j < vInfo.In.Count; j++)
            //    //        {
            //    //            he2 = vInfo.In[j];
            //    //            if (he2.Pair() == null)
            //    //            {
            //    //                if (he1.GetStart() == he2.GetNextInFace().GetStart() && he2.GetStart() == he1.GetNextInFace().GetStart())
            //    //                {
            //    //                    mesh.SetPair(he1, he2);
            //    //                    break;
            //    //                }
            //    //            }
            //    //        }
            //    //        for (int j = 0; j < vInfo.Out.Count; j++)
            //    //        {
            //    //            he2 = vInfo.Out[j];
            //    //            if (he2 != he1 && he2.Pair() == null)
            //    //            {
            //    //                if (he1.GetNextInFace().GetStart() == he2.GetNextInFace().GetStart())
            //    //                {
            //    //                    mismatchedHalfedges.Add(he1);
            //    //                    mismatchedHalfedges.Add(he2);
            //    //                    break;
            //    //                }
            //    //            }
            //    //        }
            //    //    }
            //    //});

            //}
            //tracker.setStopStatus("GE_MeshOp", "Processed unpaired halfedges.");
            return mismatchedHalfedges;
        }

        public static GE_Mesh cleanUnusedElementsByFace(GE_Mesh mesh)
        {


            HashSet<GE_Vertex> cleanedVertices = new HashSet<GE_Vertex>();
            HashSet<GE_Halfedge> cleanedHalfedges = new HashSet<GE_Halfedge>();
            //tracker.setStartStatusStr("GE_MeshOp", "Cleaning unused elements.");
            GE_Halfedge he;
            //HS_ProgressCounter counter = new HS_ProgressCounter(mesh.GetNumberOfFaces(), 10);
            //tracker.setCounterStatusStr("GE_MeshOp", "Processing faces.", counter);
            //GE_Face f;
            IEnumerator<GE_Face> fetr = mesh.fEtr();
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = 1;
            var fs = mesh.GetFaces();
            //System.Threading.Tasks.Parallel.ForEach(fs, options,f =>
            //{
            foreach (GE_Face f in fs)
            {
                he = f.GetHalfedge();
                do
                {
                    //if (!cleanedVertices.Contains(he.GetStart()))
                    //{
                    if (cleanedVertices.Add(he.GetStart()))
                    {
                        mesh.SetHalfedge(he.GetStart(), he);
                    }


                    //}
                    //if (!cleanedHalfedges.Contains(he))
                    //{
                    cleanedHalfedges.Add(he);
                    //}
                    he = he.GetNextInFace();
                } while (he != f.GetHalfedge());
                //counter.increment();
            }
            //);

            //while (fetr.MoveNext())
            //{
            //    f = fetr.Current;

            //}
            //counter = new HS_ProgressCounter(cleanedHalfedges.Count, 10);
            //tracker.setCounterStatusStr("GE_MeshOp", "Processing halfedges.", counter);

            int n = cleanedHalfedges.Count;
            var cHEtr = cleanedHalfedges.GetEnumerator();
            while (cHEtr.MoveNext())
            {
                he = cHEtr.Current;
                if (!cleanedHalfedges.Contains(he.Pair()))
                {
                    mesh.ClearPair(he);
                    mesh.SetHalfedge(he.GetStart(), he);
                }
                //if (cleanedHalfedges.Add(he.Pair()))
                //{
                //    cleanedHalfedges.Remove(he.Pair());
                //    mesh.ClearPair(he);
                //    mesh.SetHalfedge(he.GetStart(), he);
                //}
                //counter.increment();
            }


            //for (int i = 0; i < n; i++)
            //{
            //    he = cleanedHalfedges[i];

            //}

            List<GE_Vertex> removev = new List<GE_Vertex>();
            var var10 = mesh.GetVertices().GetEnumerator();
            while (var10.MoveNext())
            {
                GE_Vertex v = var10.Current;
                if (cleanedVertices.Add(v))
                {
                    cleanedVertices.Remove(v);
                    removev.Add(v);
                }
            }
            //foreach(GE_Vertex v in mesh.GetVertices())
            //{
            //    if (!cleanedVertices.Contains(v))
            //    {
            //        removev.Add(v);
            //    }
            //}
            mesh.removeVertices(removev);
            GE_HalfedgeEnumerator heEtr = mesh.heEtr();
            List<GE_Halfedge> remove = new List<GE_Halfedge>();
            while (heEtr.MoveNext())
            {
                he = heEtr.Current;
                if (cleanedHalfedges.Add(he))
                {
                    cleanedHalfedges.Remove(he);
                    remove.Add(he);
                }
            }
            mesh.removeHalfedges(remove);
            //tracker.setStopStatusStr("GE_MeshOp", "Done cleaning unused elements.");
            return mesh;
        }


        //public static HS_Coord GetFaceCenter(GE_Face f)
        //{
        //    HS_Point center = new HS_Point();
        //    int c = 0;
        //    foreach(GE_Vertex v in f.GetFaceVertices()){

        //        c++;
        //    }
        //}

        public class VertexInfo : IDisposable
        {
            bool disposed;
            public FastList<GE_Halfedge> In;
            public FastList<GE_Halfedge> Out;
            public VertexInfo()
            {
                this.Out = new FastList<GE_Halfedge>();
                this.In = new FastList<GE_Halfedge>();
            }

            #region IDisposable Members

            public void Dispose()
            {
                if (!disposed)
                {

                    GC.Collect();

                    disposed = true;
                }
                GC.SuppressFinalize(this);
            }
            #endregion
        }
    }
}
