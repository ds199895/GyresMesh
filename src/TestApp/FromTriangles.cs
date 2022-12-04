using Flowing;
using Hsy.Geo;
using Hsy.GyresMesh;
using Hsy.Render;
using OpenTK;
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
            Size(800, 600);
            cam = new CamController(this);
            render = new GE_Render(this);
            int count = 30;
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

        public override void KeyReleased()
        {
            if (key == "T")
            {
                cam.Top();
            }
            if (key == "P")
            {
                cam.Perspective();
            }
            if (key == "Z")
            {
                cam.CurrentView.SetPerspective(!cam.CurrentView.perspective);
            }
            if (key == "F")
            {
                HS_AABB aabb = mesh.getAABB();
                HS_Point cen = aabb.getCenter();
                double dis = 0;
                
                if (cam.CurrentView.perspective)
                {
                    //dis = cam.CurrentView.near* 2*aabb.getDiagonalLength2()*aabb.getDepth()/aabb.getDiagonalLength3()/ this.window.Height;

                    dis = cam.CurrentView.near*2 * aabb.getDiagonalLength3() * Sin(2 * Asin((float)(aabb.getDepth() / aabb.getDiagonalLength3()))) / 2 / this.window.Height;
                    //Vector3 newPos = new Vector3(mesh.getAABB().getCenter().xf, mesh.getAABB().getCenter().yf, (float)dis);
                    //Print(newPos);
                    Vector3 normal = new Vector3((float)-aabb.getWidth(), (float)-aabb.getHeight(), (float)aabb.getDepth()).Normalized();
                    cam.CurrentView.Position = new Vector3(cen.xf, cen.yf, cen.zf)+(float)dis*normal;
                    cam.CurrentView.target = new Vector3(cen.xf, cen.yf, cen.zf);
                    cam.CurrentView.Up = new Vector3(0, 0, 1);
                }
                else
                {
                    Print("ok");
                    if (!cam.CurrentView.is2D)
                    {
                        Print("2D");
                        //cam = new CamController(this, dis);

                        //Vector3 newPos = new Vector3(cen.xf,cen.yf, (float)dis);
                        //Print(newPos);
                        dis = cam.CurrentView.near* aabb.getDiagonalLength3() * Sin(2*Asin((float)(aabb.getDepth()/aabb.getDiagonalLength3())))/2/ this.window.Height;
                        cam.CurrentView.Position = new Vector3(cen.xf - (float)dis, cen.yf -(float)dis, (float)dis);
                        cam.CurrentView.target = new Vector3(cen.xf, cen.yf, cen.zf);
                        cam.CurrentView.Up = new Vector3(0, 0, 1);
                        //cam.CurrentView.Update(this);
                        //cam.CurrentView.SetPerspective(false);
                        Print("pos: " + cam.CurrentView.Position);
                        Print("target: " + cam.CurrentView.target);
                    }
                    else
                    {
                        dis = max((float)(cam.CurrentView.near/2 * aabb.getWidth() / (this.window.Width - 50)), (float)(cam.CurrentView.near/2*aabb.getHeight() / (this.window.Height - 50)));
                        Vector3 newPos = new Vector3(mesh.getAABB().getCenter().xf, mesh.getAABB().getCenter().yf, (float)dis);


                        Print(newPos);
                        cam.CurrentView.Position=newPos;
                        cam.CurrentView.target=new Vector3(newPos.X, newPos.Y, 0);
                        cam.CurrentView.Up = new Vector3(0, 1, 0);
                    }
                }
            }
        }
    }
}
