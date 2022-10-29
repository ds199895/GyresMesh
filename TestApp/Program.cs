using Flowing;
using Hsy.Geo;
using Hsy.GyresMesh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        GE_Face face = new GE_Face();
        GE_Mesh mesh = new GE_Mesh();
        public override void SetUp()
        {
            Size(1200, 1000);
            
            for (int i = 0; i < 10; i++)
            {
                float y = 0;
                if (i % 2 == 0)
                {
                    y = 20;
                }
                int nxt = (i + 1) % 10;

                GE_Vertex vs = new GE_Vertex(new HS_Point(500+(float)Math.Sin(i * Math.PI * 2 / 10)*100, 500+(float)Math.Cos(i * Math.PI * 2 / 10)*100, 0));
                GE_Vertex ve = new GE_Vertex(new HS_Point(500+(float)Math.Sin(nxt * Math.PI * 2 / 10)*100, 500+(float)Math.Cos(nxt * Math.PI * 2 / 10)*100, 0));

             
                GE_Halfedge h = new GE_Halfedge();
                GE_Halfedge hp = new GE_Halfedge();
                h.SetVertex(vs);
                hp.SetVertex(ve);
                h.SetPair(hp);

                hp.SetFace(face);

                hes.Add(h);
                heps.Add(hp);
                Print(vs.GetPosition());
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
        }

        public override void Draw()
        {
            Background(255);
            Fill(0);
            BeginShape();
            foreach(GE_Halfedge he in mesh.GetHalfedges()){
                Vertex(he.GetStart().X(), he.GetStart().Y(), he.GetStart().Z());
            }

            EndShape();


        }
    }
}
