using Flowing;
using Hsy.Geo;
using Hsy.GyresMesh;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Render
{
    public class GE_Render
    {
        IApp home;

        public GE_Render(IApp home)
        {
            this.home= home;
        }

        public void drawPolygonEdges(HS_Polygon P)
        {
            int[] npc = P.GetNumberOfPointsPerContour();
            int index = 0;

            for (int i = 0; i < P.GetNumberOfContours(); ++i)
            {
                this.home.BeginShape();

                for (int j = 0; j < npc[i]; ++j)
                {
                    this.vertex(P.GetPoint(index++));
                }

                this.home.EndShape();
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
        public void drawPoint<T>(T p)where T:HS_Coord
        {
            //this.home.PushMatrix();

            //this.home.Translate(p.xf, p.yf, p.zf);
            this.Cube(10, 10, 10);
            //this.home.PopMatrix();
        }
        public void drawFace(GE_Face face)
        {
            this.home.BeginShape();
            foreach(GE_Vertex v in face.GetFaceVertices())
            {
                this.vertex(v);
            }
            this.home.EndShape();
        }

        public void Cube(float length, float width, float height)
        {
            this.home.BeginShape();

            this.home.Vertex(-length / 2, width / 2, height / 2);
            this.home.Vertex(-length / 2, width / 2, -height / 2);
            this.home.Vertex(-length / 2, -width / 2, -height / 2);
            this.home.Vertex(-length / 2, -width / 2, height / 2);
            this.home.EndShape();
            this.home.BeginShape();
            this.home.Vertex(length / 2, width / 2, height / 2);
            this.home.Vertex(length / 2, width / 2, -height / 2);
            this.home.Vertex(length / 2, -width / 2, -height / 2);
            this.home.Vertex(length / 2, -width / 2, height / 2);
            this.home.EndShape();
            this.home.BeginShape();
            this.home.Vertex(length / 2, -width / 2, height / 2);
            this.home.Vertex(length / 2, -width / 2, -height / 2);
            this.home.Vertex(-length / 2, -width / 2, -height / 2);
            this.home.Vertex(-length / 2, -width / 2, height / 2);
            this.home.EndShape();
            this.home.BeginShape();
            this.home.Vertex(length / 2, width / 2, height / 2);
            this.home.Vertex(length / 2, width / 2, -height / 2);
            this.home.Vertex(-length / 2, width / 2, -height / 2);
            this.home.Vertex(-length / 2, width / 2, height / 2);
            this.home.EndShape();
            this.home.BeginShape();

            this.home.Vertex(length / 2, width / 2, -height / 2);
            this.home.Vertex(length / 2, -width / 2, -height / 2);
            this.home.Vertex(-length / 2, -width / 2, -height / 2);
            this.home.Vertex(-length / 2, width / 2, -height / 2);
            this.home.EndShape();
            this.home.BeginShape();
            this.home.Vertex(length / 2, width / 2, height / 2);
            this.home.Vertex(length / 2, -width / 2, height / 2);
            this.home.Vertex(-length / 2, -width / 2, height / 2);
            this.home.Vertex(-length / 2, width / 2, height / 2);
            this.home.EndShape();
        }
        public void vertex(HS_Coord p)
        {
            this.home.Vertex(p.xf, p.yf, p.zf);
        }
        private void line(HS_Coord p, HS_Coord q)
        {
            this.home.Line(p.xf, p.yf, p.zf, q.xf, q.yf, q.zf);
        }
    }
}
