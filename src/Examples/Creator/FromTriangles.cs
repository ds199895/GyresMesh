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

namespace Examples
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
        double starttime;
        double lasttime;
        public override void SetUp()
        {
            Size(800, 600);
            cam = new CamController(this);
            render = new GE_Render(this);

            int count =500;
            HS_Point[] points = new HS_Point[count * count];
            int index = 0;
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            starttime= stopwatch.ElapsedMilliseconds; 
            lasttime = stopwatch.ElapsedMilliseconds;
            stopwatch.Start();
            for (int j = 0; j < count; j++)
            {
                for (int i = 0; i < count; i++)
                {
                    //                points[index] = new HS_Point(i * 40, j * 40, sin(TWO_PI / 20 * i) * 40 + cos(TWO_PI / 10 * j) * 40);
                    points[index] = new HS_Point(-200 + i * 40 + (((i != 0) && (i != 10)) ? random(-20, 20) : 0), -200 + j * 40 + (((j != 0) && (j != 10)) ? random(-20, 20) : 0), Sin(TWO_PI / 20 * i) * 100 + Cos(TWO_PI / 10 * j) * 100);
                    index++;
                }
            }

            Console.WriteLine("创建points总用时   " + (stopwatch.ElapsedMilliseconds - lasttime) + "ms");
            lasttime = stopwatch.ElapsedMilliseconds;
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
            Console.WriteLine("创建tri总用时   " + (stopwatch.ElapsedMilliseconds - lasttime) + "ms");

            GEC_FromTriangles creator = new GEC_FromTriangles();

            creator.setTriangles(tris);
            //alternatively tris can be any Collection<HS_Triangle>
            mesh = new GE_Mesh(creator);
            Console.WriteLine("创建Mesh总用时   " + (stopwatch.ElapsedMilliseconds - starttime) + "ms");
            lasttime = stopwatch.ElapsedMilliseconds;
            Print(mesh);
            //foreach (GE_Vertex v in mesh.GetVertices())
            //{
            //    Print(v);
            //}
            //foreach (GE_Halfedge he in mesh.GetHalfedges())
            //{
            //    Print(he);
            //}
           
            aabb = mesh.getAABB();
            Console.WriteLine("获取aabb总用时   " + (stopwatch.ElapsedMilliseconds -lasttime) + "ms");
        }

        public override void Draw()
        {
            Background(255);
            cam.DrawSystem(this, 200);
            //Fill(255, 0, 0);
            //Stroke(0);
            render.displayHeMeshWithDegree(mesh, cam.CurrentView,detail);
            //NoStroke();
            Stroke(0);
            Fill(255, 0, 0);
            //foreach (GE_Face f in mesh.GetFaces())
            //{
            //    render.drawFace(f);
            //}
            //PushStyle();
            //NoFill();
            //Stroke(0, 0, 255);
            //StrokeWeight(4);
            //render.drawAABB(mesh.getAABB());
            //PopStyle();
        }
        HS_AABB aabb;
        
        HS_Point cen;
        Vector3 normal;
        double targetLength;
        double dis = 0;
        bool detail = false;
        public override void KeyReleased()
        {
            if (key == "F")
            {
                Focus(aabb.getLimits(), false);
            }
            if (key == "Q")
            {
                double length = targetLength;
                Print("Length: "+targetLength);
                
                double times = (cam.CurrentView.Position - cam.CurrentView.target).X / normal.X;
                //Print("height times pro: "+(double)this.window.Height /times);
                Print("width: " +this.window.Width+" height: "+this.window.Height);
                Print("width times pro: " + (double)this.window.Width / times);
                Print("aspectRatio: "+(double)this.window.Width / (double)this.window.Height);
                Print("length pro: " + targetLength / times);

                Print("times: " + times);
                Print("target: " + cam.CurrentView.target);
            }else if (key == "D")
            {
                detail = !detail;
            }


        }


        public void Focus(double[] limits, bool fit = true)
        {

            double dis = 0;
            Vector3 cen = new Vector3((float)(0.5 * (limits[0] + limits[3])), (float)(0.5 * (limits[1] + limits[4])), (float)(0.5 * (limits[2] + limits[5])));
            double lw = (limits[3] - limits[0]);
            double lh = (limits[4] - limits[1]);
            double ld = (limits[5] - limits[2]);


            double diagonalLength2 = Math.Sqrt(lw * lw + lh * lh);
            double diagonalLength3 = Math.Sqrt(lw * lw + lh * lh + ld * ld);


            Vector3 normal;
            if (fit)
            {
                if (cam.CurrentView.perspective)
                {

                    //dis = diagonalLength2 / 0.9D / cam.CurrentView.AspectRatio;
                    dis = Math.Max(cam.CurrentView.near * diagonalLength2 / (2 * this.window.Height), cam.CurrentView.near * diagonalLength3 / (2 * this.window.Width));

                    normal = new Vector3((float)-lw, (float)-lh, (float)ld).Normalized();
                    cam.CurrentView.Position = cen + (float)dis * normal;
                    cam.CurrentView.target = cen;
                    cam.CurrentView.iniCoordinateSystem(cam.CurrentView.Position, cam.CurrentView.target);
                }
                else
                {
                    if (!cam.CurrentView.is2D)
                    {

                        dis = Math.Max(cam.CurrentView.near * diagonalLength2 / (2 * this.window.Height), cam.CurrentView.near * diagonalLength3 / (2 * this.window.Width));
                        normal = new Vector3((float)-lw, (float)-lh, (float)ld).Normalized();
                        cam.CurrentView.Position = cen + (float)dis * normal;
                        cam.CurrentView.target = cen;
                        cam.CurrentView.iniCoordinateSystem(cam.CurrentView.Position, cam.CurrentView.target);

                    }
                    else
                    {
                        dis = Math.Max((float)(cam.CurrentView.near * lw / (this.window.Width - 100)), (float)(cam.CurrentView.near * lh / (this.window.Height - 100)));
                        dis = Math.Max((float)(cam.CurrentView.near * lw / (2 * this.window.Height)), (float)(cam.CurrentView.near * lh / (this.window.Width)));
                        normal = new Vector3(0, 0, 1);
                        cam.CurrentView.Position = cen + (float)dis * normal;
                        cam.CurrentView.target = cen;
                    }
                }
            }
            else
            {


                //            dis=max((float) (996*aabb.getWidth()/(width-100)),(float) (996*aabb.getHeight()/(height-100)));
                if (cam.CurrentView.is2D)
                {
                    dis = Math.Max((float)(800 * lw / (this.window.Width - 50)), (float)(800 * lh / (this.window.Height - 50)));
                }
                else
                {
                    dis = (float)(996 * diagonalLength3 / (this.window.Width - 100));
                }
                //            dis=max((float) (996*diagonal/(width-100)),(float) (996*diagonal/(height-100)));
                if (cam.CurrentView.perspective)
                {

                    //this = new thisController(this, dis);
                    double zAxis = Math.Max((float)dis, (float)limits[5]);
                    Vector3 newPos = new Vector3(cen.X, cen.Y, (float)zAxis);

                    cam.CurrentView.Position = new Vector3(newPos.X - (float)(dis * Math.Sin(Math.PI / 3)), newPos.Y - (float)(dis * Math.Cos(Math.PI / 3)), (float)dis);
                    cam.CurrentView.target = new Vector3(newPos.X, newPos.Y, cen.Z + 5);
                    cam.CurrentView.iniCoordinateSystem(cam.CurrentView.Position, cam.CurrentView.target);
                }
                else
                {
                    if (!cam.CurrentView.is2D)
                    {
                        //this = new thisController(this, dis);
                        double zAxis = Math.Max((float)dis, (float)limits[5]);
                        Vector3 newPos = new Vector3(cen.X, cen.Y, (float)zAxis);
                        cam.CurrentView.Position = new Vector3(newPos.X - (float)(dis * Math.Sin(Math.PI / 3)), newPos.Y - (float)(dis * Math.Cos(Math.PI / 3)), (float)dis);
                        cam.CurrentView.target = new Vector3(newPos.X, newPos.Y, cen.Z + 5);
                        cam.CurrentView.iniCoordinateSystem(cam.CurrentView.Position, cam.CurrentView.target);
                    }
                    else
                    {

                        //this = new thisController(this, dis);
                        //this.Top();
                        Vector3 newPos = new Vector3(cen.X, cen.Y, (float)dis);

                        cam.CurrentView.Position = newPos;
                        cam.CurrentView.target = new Vector3(newPos.X, newPos.Y, 0);
                        cam.CurrentView.iniCoordinateSystem(cam.CurrentView.Position, cam.CurrentView.target);
                    }
                }
            }

        }

    }
}
