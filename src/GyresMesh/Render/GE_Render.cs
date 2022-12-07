using Flowing;
using Hsy.Geo;
using Hsy.GyresMesh;
using Hsy.IO;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Hsy.Render
{
    public class GE_Render
    {
        IApp home;

        public GE_Render(IApp home)
        {
            this.home = home;
        }
        public void drawAABB(HS_AABB AABB)
        {
            home.PushMatrix();
            translate(AABB.getCenter());
            home.Cube((float)AABB.getWidth(), (float)AABB.getHeight(),
                    (float)AABB.getDepth());
            home.PopMatrix();
        }
        public void drawPolygonEdges(HS_Polygon P)
        {
            int[] npc = P.GetNumberOfPointsPerContour();
            int index = 0;

            for (int i = 0; i < P.GetNumberOfContours(); ++i)
            {
                if (P.GetNumberOfContours() >= 2)
                {
                    this.home.BeginShape(PrimitiveType.Triangles);
                }
                else
                {
                    this.home.BeginShape();
                }


                for (int j = 0; j < npc[i]; ++j)
                {
                    this.vertex(P.GetPoint(index++));
                }

                this.home.EndShape();
            }

        }

        public void drawPolyLine(HS_PolyLine P)
        {
            for (int i = 0; i < P.getNumberOfPoints() - 1; ++i)
            {
                this.line(P.getPoint(i), P.getPoint(i + 1));
            }

        }




        public void drawEdges(GE_Mesh mesh)
        {
            if (mesh != null)
            {
                var eItr = mesh.eEtr();
                while (eItr.MoveNext())
                {
                    GE_Halfedge e = (GE_Halfedge)eItr.Current;
                    if (e.isVisible())
                    {

                        this.line(e.GetStart(), e.GetEnd());
                    }
                }
            }
        }
        public void drawPoint<T>(T p) where T : HS_Coord
        {
            this.home.PushMatrix();

            this.home.Translate(p.xf, p.yf, p.zf);
            this.home.Cube(10, 10, 10);
            this.home.PopMatrix();
        }
        public void drawPoint<T>(T p, float r) where T : HS_Coord
        {
            this.home.PushMatrix();

            this.home.Translate(p.xf, p.yf, p.zf);
            this.home.Cube(r, r, r);
            this.home.PopMatrix();
        }
        public void drawFace(GE_Face face)
        {
            if (face.GetFaceVertices().Count > 3)
            {
                this.home.BeginShape();
            }
            else
            {
                this.home.BeginShape(PrimitiveType.Triangles);
            }

            var Etr = face.GetFaceVertices().GetEnumerator();
            while (Etr.MoveNext())
            {
                this.vertex(Etr.Current);
            }
            this.home.EndShape();
        }
        public void drawFaces(GE_Mesh mesh)
        {
            GE_FaceEnumerator fEtr = mesh.fEtr();
            while (fEtr.MoveNext())
            {
                drawFace(fEtr.Current);
            }
        }
        public void DisplayIObjects(List<IObject> objects, Camera cam, bool detail)
        {

            var oEtr = objects.GetEnumerator();
            while (oEtr.MoveNext())
            {
                var o = oEtr.Current;
                switch (o.name())
                {
                    case "Mesh":
                        displayHeMeshWithDegree((GE_Mesh)o, cam, detail);

                        break;
                    case "Point":
                        drawPoint((HS_Vector)o, 5);
                        break;
                    case "PolyLine":
                        drawPolyLine((HS_PolyLine)o);
                        break;

                }

                //if (o.name() == "Mesh")
                //{

                //        displayHeMeshWithDegree((GE_Mesh)o, cam,detail);


                //}
                //else if (o.name() == "Point")
                //{
                //    drawPoint((HS_Vector)o, 5);
                //}
            }


        }
        /**
     * 半边数据结构的显示
     *
     * He SiYuan
     * */
        public double calAverageEdgeLength(GE_Mesh mesh)
        {
            double average = 0;
            GE_EdgeEnumerator eEtr = mesh.eEtr();
            while (eEtr.MoveNext())
            {
                average += eEtr.Current.GetLength();

            }

            average /= mesh.GetNumberOfHalfedges();
            return average;
        }

        public double displayHeFaces(GE_Mesh mesh, Color color)
        {
            double average = 0;
            GE_FaceEnumerator fEtr = mesh.fEtr();
            while (fEtr.MoveNext())
            {
                displaySingleHeFace(fEtr.Current, color);
            }
            average /= mesh.GetNumberOfHalfedges();
            return average;
        }

        public void displaySingleHeFace(GE_Face face, Color color)
        {
            home.PushStyle();
            home.Fill(color.R, color.G, color.B);
            this.drawFace(face);
            home.PopStyle();
        }


        public double[] calEdgeLengthRange(GE_Mesh mesh)
        {
            double[] range = new double[2];
            double minLength = double.MaxValue;
            double maxLength = 0;
            GE_EdgeEnumerator eEtr = mesh.eEtr();
            while (eEtr.MoveNext())
            {
                var he = eEtr.Current;
                if (he.GetLength() < minLength)
                {
                    minLength = he.GetLength();
                }
                if (he.GetLength() > maxLength)
                {
                    maxLength = he.GetLength();
                }
            }
            range[0] = minLength;
            range[1] = maxLength;
            return range;
        }
        public void displayHeMesh(GE_Mesh mesh)
        {
            displayHalfEdges(mesh);
            displayHeVertices(mesh);
        }
        public void displayHeMeshWithDegree(GE_Mesh mesh, Camera cam, bool detail)
        {
            if (detail)
            {
                displayHeMeshWithDegree(mesh, (cam.Position - cam.target).Length);
            }
            else
            {
                //home.PushStyle();
                //home.NoFill();
                //home.Stroke(0, 80);

                //drawEdges(mesh);
                //home.PopStyle();

                home.PushStyle();
                home.Fill(255, 0, 0);
                //home.NoFill();
                home.Stroke(0);
                drawFaces(mesh);
                home.PopStyle();
            }


            //        displayHeFaces(mesh,color6);

        }

        public void displayHeMeshWithDegree(GE_Mesh mesh, double distance)
        {
            displayHeFacesWithDegree(mesh);
            displayHalfEdges(mesh);
            displayHeVerticesWithDegree(mesh, distance);
            Color colorless = Color.FromRgb(250, 250, 240);
            Color color5 = Color.FromRgb(250, 240, 240);
            Color color6 = Color.FromRgb(240, 230, 230);
            Color color7 = Color.FromRgb(230, 220, 240);
            Color colormore = Color.FromRgb(245, 235, 253);
            //        displayHeFaces(mesh,color6);

        }
        public void displayHeFacesWithDegree(GE_Mesh mesh)
        {
            home.PushStyle();
            Color colorless = Color.FromRgb(250, 250, 240);
            Color color5 = Color.FromRgb(250, 240, 240);
            Color color6 = Color.FromRgb(235, 253, 240);
            Color color7 = Color.FromRgb(190, 220, 240);
            Color colormore = Color.FromRgb(180, 200, 253);

            //        Color colorless=Color.FromRgb(250, 250, 240);
            //        Color color5=Color.FromRgb(250, 240, 240);
            //        Color color6=Color.FromRgb(240, 230, 230);
            //        Color color7=Color.FromRgb(230, 220, 240);
            //        Color colormore=Color.FromRgb(245, 235, 253);
            GE_FaceEnumerator fEtr = mesh.fEtr();
            while (fEtr.MoveNext())
            {
                var f = fEtr.Current;
                if (f.GetFaceDegree() == 5)
                {
                    displaySingleHeFace(f, color5);
                }
                else if (f.GetFaceDegree() == 6)
                {
                    displaySingleHeFace(f, color6);
                }
                else if (f.GetFaceDegree() == 7)
                {
                    displaySingleHeFace(f, color7);
                }
                else if (f.GetFaceDegree() < 5)
                {
                    displaySingleHeFace(f, colorless);
                }
                else if (f.GetFaceDegree() > 7)
                {
                    displaySingleHeFace(f, colormore);
                }
            }

            home.PopStyle();
        }

        public void displayHeVerticesWithDegree(GE_Mesh mesh, double distance)
        {
            //        double coefficient=27.4*calAverageEdgeLength(mesh)/distance;
            double r = 0;
            double averageLength = calAverageEdgeLength(mesh);
            //为实现在屏幕上的大小总为2mm
            double target = 2;
            r = (distance / 254) * target;

            //调控防止顶点显示与mesh显示比例过大或过小
            if (distance > averageLength * 8)
            {
                r = averageLength * 8 / 254 * target;
            }
            else if (distance < averageLength * 1.5D)
            {
                r = averageLength * 1.5D / 254 * target;
            }
            //            r=1000/coefficient;
            //        }else if(distance>10000) {
            //            r=10000/coefficient;
            //        }else {
            //            r=distance/coefficient;
            //        }

            home.PushStyle();
            Color colorless = Color.FromRgb(251, 199, 190);
            Color color5 = Color.FromRgb(249, 100, 100);
            Color color6 = Color.FromRgb(205, 237, 159);
            Color color7 = Color.FromRgb(95, 178, 199);
            Color colormore = Color.FromRgb(166, 149, 223);
            GE_VertexEnumerator vEtr = mesh.vEtr();
            while (vEtr.MoveNext())
            {
                var v = vEtr.Current;
                home.PushMatrix();
                if (v.GetVertexDegree() == 5)
                {
                    displaySingleHeVertex(v, color5, r);
                }
                else if (v.GetVertexDegree() == 6)
                {
                    displaySingleHeVertex(v, color6, r);
                }
                else if (v.GetVertexDegree() == 7)
                {
                    displaySingleHeVertex(v, color7, r);
                }
                else if (v.GetVertexDegree() < 5)
                {
                    displaySingleHeVertex(v, colorless, r);
                }
                else if (v.GetVertexDegree() > 7)
                {
                    displaySingleHeVertex(v, colormore, r);
                }

                home.PopMatrix();
            }

            home.PopStyle();
        }
        public void displayHeVertices(GE_Mesh mesh)
        {
            double r = calEdgeLengthRange(mesh)[0];
            home.PushStyle();
            Color color = Color.FromRgb(205, 237, 159);

            GE_VertexEnumerator vEtr = mesh.vEtr();
            while (vEtr.MoveNext())
            {
                var v = vEtr.Current;
                home.PushMatrix();

                displaySingleHeVertex(v, color, r);
                home.PopMatrix();
            }
            home.PopStyle();
        }
        public void displaySingleHeVertex(GE_Vertex v, Color color, double r)
        {
            home.PushStyle();
            home.Fill(color.R, color.G, color.B);
            home.NoStroke();
            home.PushMatrix();

            home.Translate(v.xf, v.yf, v.zf);
            home.Sphere((float)r, 5);

            home.PopMatrix();
            home.PopStyle();
        }
        public void displayHalfEdges(GE_Mesh mesh)
        {
            home.PushStyle();
            Color color = Color.FromRgb(95, 178, 199);
            home.Stroke(color.R, color.G, color.B);
            home.StrokeWeight(2);

            GE_EdgeEnumerator eEtr = mesh.eEtr();
            while (eEtr.MoveNext())
            {
                var he = eEtr.Current;
                displaySingleHalfEdge(he, color);
            }

            home.PopStyle();
        }
        public void displaySingleHalfEdge(GE_Halfedge he, Color color)
        {
            home.PushStyle();
            home.Stroke(0, 0, 0);
            home.StrokeWeight(1f);
            line(he.GetStart(), he.GetEnd());
            home.PopStyle();

            double offsetDis = he.GetLength() / 100.0D;
            home.PushStyle();
            home.Stroke(color.R, color.G, color.B);
            home.StrokeWeight(2.0f);
            GE_Face referFace = he.IsOuterBoundary() ? he.Pair().GetFace() : he.GetFace();

            HS_Vector vec = new HS_Vector(he.GetStart(), he.GetEnd());
            HS_Vector v = (HS_Vector)he.getHalfedgeDirection();
            HS_Vector v_ortho = v.rotateAboutAxis(90 * home.DEG_TO_RAD, new HS_Point(0, 0, 0), referFace.getFaceNormal()).mul(offsetDis);
            HS_Point ps = he.GetStart().GetPosition().add(v_ortho).add(vec.mul(1 / 3.0D));
            HS_Point pe = he.GetStart().GetPosition().add(v_ortho).add(vec.mul(2 / 3.0D));
            HS_Vector v_arrow = v.rotateAboutAxis(150 * home.DEG_TO_RAD, new HS_Point(0, 0, 0), referFace.getFaceNormal()).mul(he.GetLength() / 30.0D);

            HS_Point p_arrow = pe.add(v_arrow);
            line(ps.xf, ps.yf, ps.zf, pe.xf, pe.yf, pe.zf);
            line(pe.xf, pe.yf, pe.zf, p_arrow.xf, p_arrow.yf, p_arrow.zf);
            home.PopStyle();
        }
        public void translate(HS_Coord p)
        {
            home.Translate(p.xf, p.yf, p.zf);
        }
        public void vertex(HS_Coord p)
        {
            this.home.Vertex(p.xf, p.yf, p.zf);
        }
        public void vertex(double x, double y, double z)
        {
            this.home.Vertex((float)x, (float)y, (float)z);
        }
        private void line(HS_Coord p, HS_Coord q)
        {
            this.home.Line(p.xf, p.yf, p.zf, q.xf, q.yf, q.zf);
        }
        private void line(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            this.home.Line((float)x1, (float)y1, (float)z1, (float)x2, (float)y2, (float)z2);
        }
    }
}
