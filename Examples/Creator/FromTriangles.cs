using Flowing;
using Hsy.Geo;
using Hsy.GyresMesh;
using Hsy.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    class FromTriangles:IApp
    {
        static void Main(string[] args)
        {
            IApp.main();
        }
        CamController cam;
        GE_Mesh mesh;
        GE_Render render;
        public override void SetUp()
        {
            Size(1000, 1200);
            cam = new CamController(this);
            render = new GE_Render(this);
            int count = 5;
            HS_Point[] points = new HS_Point[count * count];
            int index = 0;
            for (int i= 0; i < count; i++)
            {
                for(int j = 0; j < count; j++)
                {
                    points[index] = new HS_Point(-200 + i * 40 + (((i != 0) && (i != 10)) ? random(-20, 20) : 0), -200 + j * 40 + (((j != 0) && (j != 10)) ? random(-20, 20) : 0), Sin(TWO_PI / 20 * i) * 100 + Cos(TWO_PI / 10 * j) * 100);
                    index++;
                }
                
            }
            HS_Triangle[] tris = new HS_Triangle[2 * (count - 1) * (count - 1)];
            for(int i = 0; i < count - 1; i++)
            {
                for(int j = 0; j < count - 1; j++)
                {
                    tris[2 * (i + (count - 1) * j)] = new HS_Triangle(points[i + count * j], points[i + count * j + 1], points[i + count * j + count]);
                    tris[2 * (i + (count - 1) * j) + 1] = new HS_Triangle(points[i + count * j + 1], points[i + count * j + count + 1], points[i + count * j + count]);
                }
            }

            GEC_FromTriangles creator = new GEC_FromTriangles();
            creator.setTriangles(tris);
            mesh = new GE_Mesh(creator);   
        }

        public override void Draw()
        {
            Background(255);

            Fill(255, 0, 0);

            Stroke(0);
            render.displayHeMeshWithDegree(mesh, cam.CurrentView);
        }


    }
}
