﻿using Flowing;
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

namespace TestApp
{
    class Program:IApp
    {
        static void Main(string[] args)
        {
            IApp.main();
        }
        List<GE_Vertex> vers = new List<GE_Vertex>();
        List<GE_Halfedge> hes = new List<GE_Halfedge>();
        List<GE_Halfedge> heps = new List<GE_Halfedge>();
        List<HS_Coord>ps= new List<HS_Coord>();

        GE_Face face = new GE_Face();
        GE_Mesh mesh = new GE_Mesh();
        HS_Polygon hsPoly;
        Polygon JtsPoly;
        CamController cam;
        HS_Vector[] vertices = new HS_Vector[9];
       
        public override void SetUp()
        {
            Size(1200, 1000);
            cam = new CamController(this);
            //HS_Coord c = new HS_Coord();
            this.vertices[0] = new HS_Vector(-400, 600);
            this.vertices[1] = new HS_Vector(0, 200);
            this.vertices[2] = new HS_Vector(200, 500);
            this.vertices[3] = new HS_Vector(700, 0);
            this.vertices[4] = new HS_Vector(500, -600);
            this.vertices[5] = new HS_Vector(300, 300);
            this.vertices[6] = new HS_Vector(0,-500);
            this.vertices[7] = new HS_Vector(-600, 0);
            this.vertices[8] = new HS_Vector(-200, 100);

            for (int i = 0; i < 10; i++)
            {
                float y = 0;
                if (i % 2 == 0)
                {
                    y = 20;
                }
                int nxt = (i + 1) % 10;

                HS_Point p1 = new HS_Point(500 + (float)Math.Sin(i * Math.PI * 2 / 10) * 100, 500 + (float)Math.Cos(i * Math.PI * 2 / 10) * 100, 0);
                HS_Point p2 = new HS_Point(500 + (float)Math.Sin(nxt * Math.PI * 2 / 10) * 100, 500 + (float)Math.Cos(nxt * Math.PI * 2 / 10) * 100, 0);


                GE_Vertex vs = new GE_Vertex(p1);
                GE_Vertex ve = new GE_Vertex(p2);

                GE_Halfedge h = new GE_Halfedge();
                GE_Halfedge hp = new GE_Halfedge();
                h.SetVertex(vs);
                hp.SetVertex(ve);
                h.SetPair(hp);

                hp.SetFace(face);

                hes.Add(h);
                heps.Add(hp);
                Print("vertex: "+vs.GetPosition());

                mesh.Add(vs);
            }
            for(int i = 0; i < hes.Count; i++)
            {
                int nxt = (i + 1) % 10;
                int pre = (i - 1 + 10) % 10;

                GE_Halfedge h = hes[i];
                GE_Halfedge hp = hes[pre];
                GE_Halfedge hn= hes[nxt];

                h.SetNext(hn);
                h.SetPrev(hp);

                h.Pair().SetNext(hp.Pair());
                h.Pair().SetPrev(hn.Pair());
            }
            face.SetHalfedge(hes[0]);
            

            for (int i = 0; i <hes.Count; i++)
            {
                Print(hes[i]);
                Print(hes[i].GetStart());
                mesh.Add(hes[i]);
                mesh.Add(hes[i].Pair());
            }
            Print(face);
            mesh.Add(face);
            Print(mesh);
            Print(HS_Math.fastInvSqrt(6.25f));

            hsPoly = HS_GeometryFactory.instance().CreateSimplePolygon(ps);
            //JtsPoly = HS_JTS.toJTSPolygon2D(hsPoly);
            //Print(JtsPoly.Coordinates.Length);
            //cam.Front();
        }

        public override void Draw()
        {
            Background(255);
            Fill(0);
            cam.DrawSystem(this,200);
            //BeginShape(OpenTK.Graphics.OpenGL.PrimitiveType.TriangleFan);
            ////foreach (GE_Halfedge he in mesh.GetHalfedges())
            ////{
            ////    Vertex(he.GetStart().xf, he.GetStart().yf, he.GetStart().zf);
            ////}
            //foreach (HS_Vector v in vertices)
            //{
            //    Vertex(v.xf, v.yf, v.zf);
            //}
            GL.Begin(PrimitiveType.TriangleStrip);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.Color4(Color4.Black);
            foreach(HS_Vector v in vertices)
            {
                GL.Vertex2(v.xd, v.yd);
            }
            GL.End();
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
    }
}
