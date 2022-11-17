using Flowing;
using GeoAPI.Geometries;
using Hsy.Geo;
using Hsy.GyresMesh;
using Hsy.HsMath;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK;
using Hsy.Render;

namespace TestApp
{
    class Program : IApp
    {
        static void Main(string[] args)
        {
            IApp.main();
        }
        List<GE_Vertex> vers = new List<GE_Vertex>();
        List<GE_Halfedge> hes = new List<GE_Halfedge>();
        List<GE_Halfedge> heps = new List<GE_Halfedge>();
        List<HS_Coord> ps = new List<HS_Coord>();

        GE_Face face = new GE_Face();
        //GE_Mesh mesh = new GE_Mesh();
        HS_Polygon hsPoly;
        Polygon JtsPoly;
        CamController cam;
        HS_Vector[] vertices = new HS_Vector[9];
        List<HS_Vector> vecs = new List<HS_Vector>();
        int[] triangles;
        List<HS_Polygon> polys = new List<HS_Polygon>();
        GE_Mesh mesh;
        GE_Render render;
        public override void SetUp()
        {
            Size(1200, 1000);
            cam = new CamController(this);
            render = new GE_Render(this);
            //HS_Coord c = new HS_Coord();
            this.vertices[0] = new HS_Vector(-400, 600);
            this.vertices[1] = new HS_Vector(0, 200);
            this.vertices[2] = new HS_Vector(200, 500);
            //this.vertices[3] = new HS_Vector(-200, 100);

            this.vertices[3] = new HS_Vector(700, 0);
            this.vertices[4] = new HS_Vector(500, -600);
            this.vertices[5] = new HS_Vector(300, 300);
            this.vertices[6] = new HS_Vector(0, -500);
            this.vertices[7] = new HS_Vector(-600, 0);
            this.vertices[8] = new HS_Vector(-200, 100);


            vecs.Add(new HS_Vector(0, 0, 0));
            vecs.Add(new HS_Vector(500, 0, 0));
            vecs.Add(new HS_Vector(500, 500, 500));
            vecs.Add(new HS_Vector(300, 300, 300));
            vecs.Add(new HS_Vector(200, 600, 600));
            vecs.Add(new HS_Vector(100, 300, 300));
            vecs.Add(new HS_Vector(0, 500, 500));
            //for (int i = 0; i < vertices.Length; i++)
            //{
            //    Print("first: "+vertices[i]);
            //}
            //for (int i = 0; i < 10; i++)
            //{
            //    float y = 0;
            //    if (i % 2 == 0)
            //    {
            //        y = 20;
            //    }
            //    int nxt = (i + 1) % 10;

            //    HS_Point p1 = new HS_Point(500 + (float)Math.Sin(i * Math.PI * 2 / 10) * 100, 500 + (float)Math.Cos(i * Math.PI * 2 / 10) * 100, 0);
            //    HS_Point p2 = new HS_Point(500 + (float)Math.Sin(nxt * Math.PI * 2 / 10) * 100, 500 + (float)Math.Cos(nxt * Math.PI * 2 / 10) * 100, 0);


            //    GE_Vertex vs = new GE_Vertex(p1);
            //    GE_Vertex ve = new GE_Vertex(p2);

            //    GE_Halfedge h = new GE_Halfedge();
            //    GE_Halfedge hp = new GE_Halfedge();
            //    h.SetVertex(vs);
            //    hp.SetVertex(ve);
            //    h.SetPair(hp);

            //    hp.SetFace(face);

            //    hes.Add(h);
            //    heps.Add(hp);
            //    Print("vertex: "+vs.GetPosition());

            //    mesh.Add(vs);
            //}
            //for(int i = 0; i < hes.Count; i++)
            //{
            //    int nxt = (i + 1) % 10;
            //    int pre = (i - 1 + 10) % 10;

            //    GE_Halfedge h = hes[i];
            //    GE_Halfedge hp = hes[pre];
            //    GE_Halfedge hn= hes[nxt];

            //    h.SetNext(hn);
            //    h.SetPrev(hp);

            //    h.Pair().SetNext(hp.Pair());
            //    h.Pair().SetPrev(hn.Pair());
            //}
            //face.SetHalfedge(hes[0]);


            //for (int i = 0; i <hes.Count; i++)
            //{
            //    Print(hes[i]);
            //    Print(hes[i].GetStart());
            //    mesh.Add(hes[i]);
            //    mesh.Add(hes[i].Pair());
            //}
            //Print(face);
            //mesh.Add(face);
            //Print(mesh);


            //hsPoly = HS_GeometryFactory.instance().CreateSimplePolygon(ps);


            //if (!Triangulator.Triangulate(vertices, out triangles, out string errorMessage))
            //{
            //    throw new Exception("unable to create a polygon");
            //}
            //for (int i = 0; i < triangles.Length; i += 3)
            //{
            //    int a = triangles[i];
            //    int b = triangles[i + 1];
            //    int c = triangles[i + 2];
            //    Print("triangle " + i/3 + " :" + a + " " + b + " " + c + " ");
            //}

            //JtsPoly = HS_JTS.toJTSPolygon2D(hsPoly);
            //Print(JtsPoly.Coordinates.Length);
            //cam.Front();
            HS_Point center = new HS_Point(100, 100);
            //for(int i = 0; i < 10; i++)
            //{
            //    List<HS_Point> pts = new List<HS_Point>();
            //    int nxt = (i + 1) % 10;
            //    HS_Point p1 = new HS_Point(center.xf + (float)Math.Sin(i * Math.PI * 2 / 10) * 100, center.yf + (float)Math.Cos(i * Math.PI * 2 / 10) * 100, 0);
            //    HS_Point p2 = new HS_Point(center.xf + (float)Math.Sin(nxt * Math.PI * 2 / 10) * 100, center.yf + (float)Math.Cos(nxt * Math.PI * 2 / 10) * 100, 0); ;

            //    pts.Add(center);
            //    pts.Add(p1);
            //    pts.Add(p2);
            //    HS_Polygon poly = HS_GeometryFactory.instance().CreateSimplePolygon(pts);
            //    polys.Add(poly);
            //}
            List<HS_Point> hole = new List<HS_Point>();
            //hole.Add(new HS_Point(-50, 50, 0));
            //hole.Add(new HS_Point(50, 50, 0));
            //hole.Add(new HS_Point(100, 250, 0));

            //hole.Add(new HS_Point(50, 50, 0));
            //hole.Add(new HS_Point(-50, 50, 0));
            //hole.Add(new HS_Point(100, 250, 0));

            hole.Add(new HS_Point(200, 350, 350));
            hole.Add(new HS_Point(150, 350, 350));
            hole.Add(new HS_Point(200, 400, 400));



            List<HS_Point>[] holes = new List<HS_Point>[] { hole };
            //HS_Polygon poly = HS_GeometryFactory.instance().createPolygonWithHoles(vertices, holes);
            List<HS_Vector> vec = vertices.ToList();
            //vec.Reverse();
            //vecs.Reverse();
            //HS_Polygon poly = new HS_Polygon().Create(vecs, holes);
            HS_Polygon poly = new HS_Polygon().Create(vecs);
            polys.Add(poly);
            GEC_FromPolygons gecp = new GEC_FromPolygons();
            gecp.setPolygons(polys);
            mesh = new GE_Mesh(gecp);
            foreach (GE_Vertex v in mesh.GetVertices())
            {
                Print(v);
            }
            foreach (GE_Halfedge he in mesh.GetHalfedges())
            {
                Print(he);
            }
            //Print(mesh);
        }

        public override void Draw()
        {
            Background(255);
            Fill(240);
            cam.DrawSystem(this, 200);
            Stroke(0);
            //render.drawEdges(mesh);
            foreach (GE_Face f in mesh.GetFaces())
            {
                render.drawFace(f);
            }
            //PushMatrix();

            //PopMatrix();
            //foreach (GE_Vertex v in mesh.GetVertices())
            //{
            //    PushStyle();
            //    Fill(0);

            //    render.drawPoint(v);

            //    PopStyle();
            //}
            render.disPlayHeMeshWithDegree(mesh, cam.CurrentView);
            //DrawPolygonTriangles(vertices, triangles);
            //BeginShape(OpenTK.Graphics.OpenGL.PrimitiveType.TriangleFan);
            ////foreach (GE_Halfedge he in mesh.GetHalfedges())
            ////{
            ////    Vertex(he.GetStart().xf, he.GetStart().yf, he.GetStart().zf);
            ////}
            //foreach (HS_Vector v in vertices)
            //{
            //    Vertex(v.xf, v.yf, v.zf);
            //}
            //GL.Begin(PrimitiveType.TriangleStrip);
            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            //GL.Color4(Color4.Black);
            //BeginShape();
            ////Print(vertices.Length);
            //for (int i = 0; i < vertices.Length; i++)
            //{
            //    Vertex(vertices[i].xf, vertices[i].yf, 0);
            //}
            //EndShape();
            //GL.End();
            //foreach (Coordinate c in JtsPoly.Coordinates)
            //{
            //    Vertex((float)c.X, (float)c.Y);
            //}
            //for (int i=0;i<hsPoly.getNumberOfPoints();i++)
            //{
            //    Vertex(hsPoly.GetPoint(i).X(), hsPoly.GetPoint(i).Y(), hsPoly.GetPoint(i).Z());
            //}
            //EndShape();


        }

        public void DrawPolygonTriangles(HS_Vector[] vertices, int[] triangles)
        {

            for (int i = 0; i < triangles.Length; i += 3)
            {
                int a = triangles[i];
                int b = triangles[i + 1];
                int c = triangles[i + 2];
                HS_Vector va = vertices[a];
                HS_Vector vb = vertices[b];
                HS_Vector vc = vertices[c];
                //Print(va);
                //Print(vb);
                BeginShape();
                Vertex(va.xf, va.yf, va.zf);
                Vertex(vb.xf, vb.yf, vb.zf);
                Vertex(vc.xf, vc.yf, vc.zf);
                //Line(va.xf, va.yf, va.zf, vb.xf, vb.yf, vb.zf);
                //Line(vb.xf, vb.yf, vb.zf, vc.xf, vc.yf, vc.zf);
                //Line(vc.xf, vc.yf, vc.zf, va.xf, va.yf, va.zf);
                EndShape();
            }

        }
    }
}
