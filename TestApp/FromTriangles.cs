using Flowing;
using Hsy.Geo;
using Hsy.GyresMesh;
using Hsy.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class FromTriangles:IApp
    {
        static void Main(string[] args)
        {
            IApp.main();
        }
        CamController cam;
        GE_Render render;
        GE_Mesh mesh;
        public override void SetUp()
        {
            Size(1000, 1200);
            cam = new CamController(this);
            render = new GE_Render(this);
            int count = 100;
            HS_Point[] points = new HS_Point[count * count];
            int index = 0;

            for (int j = 0; j < count; j++)
            {
                for (int i = 0; i < count; i++)
                {
                    //                points[index] = new HS_Point(i * 40, j * 40, sin(TWO_PI / 20 * i) * 40 + cos(TWO_PI / 10 * j) * 40);
                    points[index] = new HS_Point(-200 + i * 40 + (((i != 0) && (i != 10)) ? random(-20, 20) : 0), -200 + j * 40 + (((j != 0) && (j != 10)) ? random(-20, 20) : 0), Sin(TWO_PI / 20 * i) * 100 + Cos(TWO_PI / 10 * j) * 100);
                    index++;
                }
            }

            //create triangles from point grid
            HS_Triangle[] tris = new HS_Triangle[2 * (count - 1) * (count - 1)];

            for (int i = 0; i < (count - 1); i++)
            {
                for (int j = 0; j < (count - 1); j++)
                {
                    tris[2 * (i + (count - 1) * j)] = new HS_Triangle(points[i + count * j], points[i + 1 + count * j], points[i + count * j + count]);
                    tris[2 * (i + (count - 1) * j) + 1] = new HS_Triangle(points[i + 1 + count * j], points[i + count * j + count + 1], points[i + count * j + count]);
                }
            }

            GEC_FromTriangles creator = new GEC_FromTriangles();

            creator.setTriangles(tris);
            //alternatively tris can be any Collection<HS_Triangle>
            mesh = new GE_Mesh(creator);
            Print(mesh);
            //foreach (GE_Vertex v in mesh.GetVertices())
            //{
            //    Print(v);
            //}
            //foreach (GE_Halfedge he in mesh.GetHalfedges())
            //{
            //    Print(he);
            //}
        }

        public override void Draw()
        {
            Background(255);
            cam.DrawSystem(this, 200);
            //Fill(255, 0, 0);
            //Stroke(0);
            //render.displayHeMeshWithDegree(mesh, cam.CurrentView);
            //NoStroke();
            Stroke(0);
            Fill(255, 0, 0);
            foreach (GE_Face f in mesh.GetFaces())
            {
                render.drawFace(f);
            }
        }
    }
}
